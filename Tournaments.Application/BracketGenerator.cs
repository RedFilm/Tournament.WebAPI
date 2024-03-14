using Tournaments.Domain.Entities;

namespace Tournaments.Application
{
	public class BracketGenerator
	{
		// Точка входа для генерации сетки
		public Bracket GenerateNewBracket(List<Team> teams, long tournamentId)
		{
			List<long> teamIds = new List<long>();

			FillTeamIds(teams, teamIds);

			int stagesCount = (int)Math.Ceiling(Math.Log2(teams.Count)); // Вычисление общего количества стадий

			List<Stage> stages = new List<Stage>(); // Список стадий
			//int.PopCount()
			// Если кол-во игроков равно степени двойки, то заполняются все стадии пустыми матчами, кроме последней.
			if (Math.Log2(teams.Count) == stagesCount)
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

			return new Bracket
			{
				Stages = stages,
				//TournamentId = tournamentId
			};
		}

		// Заполнение стадий турнира пустыми матчами (матчи, которые будут сыграны позже)
		private void FillStagesWithDefault(int endStageIndex, List<Stage> stages)
		{
			for (int stageNumber = 0; stageNumber < endStageIndex; stageNumber++)
			{
				List<Match> matches = new List<Match>();

				// 1 << stageNumber
				var matchesCount = (int)Math.Pow(2, stageNumber);

				for (int i = 0; i < matchesCount; i++)
				{
					var match = CreateEmptyMatch(i);
					matches.Add(match);
				}

				stages.Add(new Stage { StageNumber = stageNumber, Matches = matches });
			}
		}

		// Заполнение последних стадий турнира
		private void FillLastStages(int startStageIndex, int totalStagesCount, int totalTeamsCount, List<Stage> stages, List<long> teamIds)
		{
			for (int stageNumber = startStageIndex; stageNumber <= totalStagesCount; stageNumber++)
			{
				var teamsInStageCount = 0;
				var matchesCount = 0;
				List<Match> matches = new List<Match>();

				// Если stageCount = 2, то стадий для заполнения 2, предпоследняя и последняя соответственно,
				// для них по-разному считается кол-во матчей.
				if (totalStagesCount - stageNumber > 0)
				{
					// Расчет кол-ва матчей для предпоследней стадии
					teamsInStageCount = (int)Math.Pow(2, totalStagesCount + 1) - totalTeamsCount;
					matchesCount = (int)Math.Pow(2, stageNumber);

					// Флаг, который показывает было ли обнуление i
					var hasResetToZero = false;
					for (int i = 0; i < teamsInStageCount || i < matchesCount && !hasResetToZero; i++)
					{
						if (i >= teamsInStageCount)
							matches.Add(CreateEmptyMatch(i));
						else
						{
							if (i >= matchesCount)
							{
								hasResetToZero = true;
								teamsInStageCount -= matchesCount;
								i = 0;
							}
							if (!hasResetToZero)
								matches.Add(CreateMatch(i, CreateCount.OneTeam, teamIds));
							else
							{
								var match = GetMatch(i, matches);

								if (match is not null)
									AddTeamToMatch(match, teamIds);
								else
									throw new NullReferenceException();
							}
						}
					}
				}
				// Когда заполняется последняя стадия, stageCount = 1, формула расчета кол-ва матчей меняется.
				else
				{
					// Расчет кол-ва матчей для последней стадии
					//teamsInStageCount = 2 * totalTeamsCount - (int)Math.Pow(2, stageNumber + 1);
					//matchesCount = teamsInStageCount / 2;

					var totalMatchesInStageCount = (int)Math.Pow(2, stageNumber);

					// Распределение команд по матчам в стадии
					for (int i = 0; i < totalMatchesInStageCount; i++)
					{
						// Если на следующей стадии соответствующий матч уже заполнен, то смещаем i = identifier на 2 и продолжаем проверку
						if (stages[stageNumber - 1].Matches[i / 2].Team1Id is not null &&
							stages[stageNumber - 1].Matches[i / 2].Team2Id is not null)
						{
							i = i + 2;
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
		private Match CreateMatch(int identifier, CreateCount count, List<long> teamIds)
		{
			Random random = new Random();

			if (count == CreateCount.TwoTeam)
			{
				var team1Id = teamIds[random.Next(teamIds.Count)];
				var team2Id = teamIds[random.Next(teamIds.Count)];

				teamIds?.Remove(team1Id);
				teamIds?.Remove(team2Id);

				return new Match
				{
					Identifier = identifier,
					Team1Id = team1Id,
					Team2Id = team2Id,
					WinnerId = null,
				};
			}
			else
			{
				var team1Id = teamIds[random.Next(teamIds.Count)];
				teamIds?.Remove(team1Id);

				return new Match
				{
					Identifier = identifier,
					Team1Id = team1Id,
					Team2Id = null, // Один игрок, второй не определен
					WinnerId = null,
				};
			}
		}

		// Создание пустого матча
		private Match CreateEmptyMatch(int identifier)
		{
			return new Match
			{
				Identifier = identifier,
				Team1Id = null,
				Team2Id = null,
				WinnerId = null,
			};
		}

		// Добавление команды в существующий матч
		private void AddTeamToMatch(Match match, List<long> teamIds)
		{
			Random random = new Random();

			// Добавить вторую команду, если одна из них null
			if (match.Team1Id == null)
			{
				var team1Id = teamIds[random.Next(teamIds.Count)];
				teamIds?.Remove(team1Id);

				match.Team1Id = team1Id;
			}
			else if (match.Team2Id == null)
			{
				var team2Id = teamIds[random.Next(teamIds.Count)];
				teamIds?.Remove(team2Id);

				match.Team2Id = team2Id;
			}
		}

		public Match? GetMatch(int identifier, List<Match> matches)
		{
			return matches.FirstOrDefault(match => match.Identifier == identifier);
		}

		private void FillTeamIds(List<Team> teams, List<long> teamIds)
		{
			foreach (var team in teams)
			{
				teamIds.Add(team.Id);
			}
		}

		// Dictionary<long,long> - MatchId, WinnerId
		public void Update(Bracket bracket, Dictionary<long, long> results)
		{

		}
	}

	public enum CreateCount
	{
		OneTeam,
		TwoTeam,
	}
}
