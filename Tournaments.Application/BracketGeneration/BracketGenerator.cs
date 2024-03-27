using Tournaments.Domain.Entities;
using Tournaments.Domain.Models;

namespace Tournaments.Application.BracketGeneration
{
	public class BracketGenerator
	{
		// Точка входа для генерации сетки
		public Bracket GenerateNewBracket(ICollection<Team> teams)
		{
			var teamIds = teams.Select(t => t.Id).ToList();

			var stagesCount = (int)Math.Ceiling(Math.Log2(teams.Count)); // Вычисление общего количества стадий

			var stages = new List<Stage>(stagesCount); // Список стадий

			// Если кол-во игроков равно степени двойки, то заполняются все стадии пустыми матчами, кроме последней.
			if (int.PopCount(teams.Count) == 1)
			{
				// Создание пустых матчей для каждой стадии
				FillStagesWithDefault(stagesCount - 1, stages);
				// Создание матчей для последней стадии
				FillLastStages(stagesCount - 1, stagesCount - 1, teams.Count, stages, teamIds);
			}
			// Если кол-во игроков не равно степени двойки, то заполняются все стадии пустыми матчами, кроме последней и предпоследней.
			else
			{
				FillStagesWithDefault(stagesCount - 2, stages);
				FillLastStages(stagesCount - 2, stagesCount - 1, teams.Count, stages, teamIds);
			}

			return new Bracket { Stages = stages };
		}

		// Заполнение стадий турнира пустыми матчами (матчи, которые будут сыграны позже)
		private void FillStagesWithDefault(int endStageIndex, ICollection<Stage> stages)
		{
			for (int stageNumber = 0; stageNumber < endStageIndex; stageNumber++)
			{
				var matches = new List<Match>();

				// 1 << stageNumber
				var matchesCount = (int)Math.Pow(2, stageNumber);

				for (int i = 0; i < matchesCount; i++)
					matches.Add(new Match { Identifier = i });

				stages.Add(new Stage { StageNumber = stageNumber, Matches = matches });
			}
		}

		// Заполнение последних стадий турнира
		private void FillLastStages(int startStageIndex, int totalStagesCount, int totalTeamsCount, IList<Stage> stages, IList<long> teamIds)
		{
			for (int stageNumber = startStageIndex; stageNumber <= totalStagesCount; stageNumber++)
			{
				var matches = new List<Match>();

				// Проверяется количество стадий для заполнения. Если разница > 0, то стадии 2: предпоследняя и последняя,
				// для них по-разному считается кол-во матчей.
				if (totalStagesCount - stageNumber > 0)
				{
					// Расчет кол-ва команд и матчей для предпоследней стадии
					var teamsInStageCount = (int)Math.Pow(2, totalStagesCount + 1) - totalTeamsCount;
					var matchesCount = (int)Math.Pow(2, stageNumber);

					// Флаг, который показывает было ли обнуление i
					var hasResetToZero = false;

					// Происходит заполнение матчей в стадии последовательно по одной команде на первую позицию Team1 в каждый матч, пока не кончатся пустые матчи.
					// Далее происходит однуление i и заполняем матчи снова по одной команде на вторую позицию Team2.
					for (int i = 0; i < teamsInStageCount || i < matchesCount && !hasResetToZero; i++)
					{
						if (i >= teamsInStageCount)
							matches.Add(new Match { Identifier = i });
						else
						{
							// Если заполнил каждый матч по 1 команде и остались ещё команды
							if (i >= matchesCount)
							{
								hasResetToZero = true;
								// Уменьшаем количество команд на число матчей, которые уже заполнили по 1 команде
								teamsInStageCount -= matchesCount;
								// Обнуляем, чтобы пройтись по каждому матчу еще раз и дозаполнить
								i = 0;
							}
							// Если не прошел круг заполнения матчей командами, то создаем новый матч с одной командой
							if (!hasResetToZero)
								matches.Add(CreateMatch(i, CreateCount.OneTeam, teamIds));
							// Если круг был пройден, то достаем уже добавленные матчи и дозаполняем их
							else
							{
								var match = matches[i];//GetMatch(i, matches);

								if (match is not null)
									AddTeamToMatch(match, teamIds);
								else
									throw new NullReferenceException();
							}
						}
					}
				}
				// Когда заполняется последняя стадия, формула расчета кол-ва матчей меняется.
				// Заполнение последней стадии зависит от того, как заполнена предыдущая.
				else
				{
					var totalMatchesInStageCount = (int)Math.Pow(2, stageNumber);

					// Распределение команд по матчам в стадии
					for (int i = 0; i < totalMatchesInStageCount; i++)
					{
						// Частный случай. Команды только 2, соответственно номер стадии будет 0.
						if (stageNumber == 0)
						{
							var match = CreateMatch(i, CreateCount.TwoTeam, teamIds);
							matches.Add(match);
							i++;
						}
						// Если на следующей стадии соответствующий матч уже заполнен, то смещаем i = identifier на 2 и продолжаем проверку
						else if (stages[stageNumber - 1].Matches[i / 2].Team1Id is not null &&
							stages[stageNumber - 1].Matches[i / 2].Team2Id is not null)
						{
							i += 2;
							continue;
						}
						// Если на следующей стадии одна из команд уже проставлена, то создаем один матч для определения второй команды и смещаем индекс на 1
						else if (stages[stageNumber - 1].Matches[i / 2].Team1Id is not null ^
								 stages[stageNumber - 1].Matches[i / 2].Team2Id is not null)
						{
							var match = CreateMatch(i, CreateCount.TwoTeam, teamIds);
							matches.Add(match);
							i++;
						}
						// Если на следующей стадии соответствующий матч полностью пустой, то создаем 2 матча на текущем этапе, которые будут ему соответствовать
						else
						{
							var match = CreateMatch(i, CreateCount.TwoTeam, teamIds);
							matches.Add(match);
						}
					}
				}

				stages.Add(new Stage { StageNumber = stageNumber, Matches = matches });
			}
		}

		// Создание матча с одной\двумя командами
		private Match CreateMatch(int identifier, CreateCount count, IList<long> teamIds)
		{
			switch (count)
			{
				case CreateCount.OneTeam:

					var team1Id = teamIds[Random.Shared.Next(teamIds.Count)];
					teamIds.Remove(team1Id);

					return new Match
					{
						Identifier = identifier,
						Team1Id = team1Id,
						Team2Id = null, // Один игрок, второй не определен
						WinnerId = null,
					};

				case CreateCount.TwoTeam:

					if (teamIds.Count < 2)
						throw new ArgumentException("To create a match, the number of teams must be at least 2");

					team1Id = teamIds[Random.Shared.Next(teamIds.Count)];
					teamIds.Remove(team1Id);

					var team2Id = teamIds![Random.Shared.Next(teamIds.Count)];
					teamIds.Remove(team2Id);

					return new Match
					{
						Identifier = identifier,
						Team1Id = team1Id,
						Team2Id = team2Id,
						WinnerId = null,
					};

				default:
					throw new ArgumentException($"Invalid parameter: {typeof(CreateCount)}");
			}
		}

		// Добавление команды в существующий матч
		private void AddTeamToMatch(Match match, IList<long> teamIds)
		{
			// Добавить вторую команду, если одна из них null
			if (match.Team1Id is null)
			{
				var team1Id = teamIds[Random.Shared.Next(teamIds.Count)];
				teamIds.Remove(team1Id);

				match.Team1Id = team1Id;
			}
			else if (match.Team2Id is null)
			{
				var team2Id = teamIds[Random.Shared.Next(teamIds.Count)];
				teamIds.Remove(team2Id);

				match.Team2Id = team2Id;
			}
		}

		public Bracket Update(Bracket bracket, List<MatchResultModel> matchesResult)
		{
			if (matchesResult[0].StageNumber == 0)
				return bracket;

			foreach (var t in matchesResult)
			{
				var currentStageMatch = bracket.Stages.SelectMany(s => s.Matches)
					.FirstOrDefault(m => m.MatchId == t.MatchId);

				if (currentStageMatch is null)
					continue;

				currentStageMatch.WinnerId = t.WinnerId;
				var nextStageMatchNumber = currentStageMatch.Identifier / 2;

				var nextStageMatch = bracket.Stages.SelectMany(s => s.Matches)
					.FirstOrDefault(m => m.Identifier == nextStageMatchNumber);

				if (nextStageMatch is null)
					continue;

				if (nextStageMatch.Team1Id is null)
					nextStageMatch.Team1Id = t.WinnerId;
				else nextStageMatch.Team2Id ??= t.WinnerId;
			}

			return bracket;
		}
	}
}
