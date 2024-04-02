using System.Runtime.InteropServices;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Models;

namespace Tournaments.Application.BracketGeneration
{
	public class BracketGenerator
	{
		// Заполнение происходит начиная с финала.
		// 1. Заполняются стадии с пустыми матчами, результаты которых будут известны позже.
		// 2. Заполняются первые 2 или одна стадия, в зависимости от числа участников.
		// Порядок важен, поэтому сначала заполняется вторая стадия, потом первая.
		public Bracket GenerateNewBracket(List<Team> teams, bool shuffle)
		{
			if (shuffle)
				Random.Shared.Shuffle(CollectionsMarshal.AsSpan(teams));

			int teamIndex = 0;

			var totalStagesCount = (int)Math.Ceiling(Math.Log2(teams.Count));
			var stages = new List<Stage>(totalStagesCount);

			var isPowerOfTwo = int.PopCount(teams.Count) == 1;
			var emptyStagesCount = isPowerOfTwo ? totalStagesCount - 1 : totalStagesCount - 2;

			FillStagesWithDefault(emptyStagesCount, stages);

			if (!isPowerOfTwo)
				FillSecondStage(teams, stages, emptyStagesCount, totalStagesCount, ref teamIndex);

			FillFirstStage(teams, stages, totalStagesCount - 1, ref teamIndex);

			return new Bracket { Stages = stages };
		}

		private void FillStagesWithDefault(int endStageIndex, ICollection<Stage> stages)
		{
			for (int stageNumber = 0; stageNumber < endStageIndex; stageNumber++)
			{
				var matchesCount = (int)Math.Pow(2, stageNumber);
				var matches = new List<Match>(matchesCount);

				for (int i = 0; i < matchesCount; i++)
					matches.Add(new Match { Identifier = i });

				stages.Add(new Stage { StageNumber = stageNumber, Matches = matches });
			}
		}

		private void FillFirstStage(IList<Team> teams, IList<Stage> stages, int firstStageIndex, ref int teamIndex)
		{
			var matches = new List<Match>();
			var totalMatchesInStageCount = (int)Math.Pow(2, firstStageIndex);

			for (int i = 0; i < totalMatchesInStageCount; i += 2)
			{
				if (firstStageIndex == 0)
				{
					matches.Add(new Match { Identifier = 0, Team1Id = teams[teamIndex].Id, Team2Id = teams[teamIndex + 1].Id });
					teamIndex += 2;
					break;
				}

				var previousMatch = stages[firstStageIndex - 1].Matches.Skip(i / 2).FirstOrDefault() 
					?? throw new NullReferenceException("Expected match");

				if (previousMatch.Team1Id is not null ^ previousMatch.Team2Id is not null)
				{
					matches.Add(new Match { Identifier = i, Team1Id = teams[teamIndex].Id, Team2Id = teams[teamIndex + 1].Id });
					teamIndex += 2;
				}
				if (previousMatch.Team1Id is null && previousMatch.Team2Id is null)
				{
					matches.Add(new Match { Identifier = i, Team1Id = teams[teamIndex].Id, Team2Id = teams[teamIndex + 1].Id });
					teamIndex += 2;
					i--;
				}
			}
			stages.Add(new Stage { StageNumber = firstStageIndex, Matches = matches });
		}

		private void FillSecondStage(IList<Team> teams, IList<Stage> stages, int secondStageIndex, int totalStagesCount, ref int teamIndex)
		{
			var matches = new List<Match>();

			var teamsInStageCount = (int)Math.Pow(2, totalStagesCount) - teams.Count;
			var matchesCount = (int)Math.Pow(2, secondStageIndex);
			var greaterCount = Math.Max(teamsInStageCount, matchesCount);

			var isSecondCircle = false;

			for (int i = 0; i < greaterCount; i++)
			{
				if (i >= matchesCount)
				{
					isSecondCircle = true;
					greaterCount -= matchesCount;
					i = 0;
				}
				if (i >= teamsInStageCount)
				{
					matches.Add(new Match { Identifier = i });
					continue;
				}
				if (isSecondCircle)
				{
					var match = matches[i] is not null ? matches[i] : throw new NullReferenceException("Match wasn't found");
					AddTeamToMatch(match, teams[teamIndex]);
					teamIndex++;
				}
				else
				{
					matches.Add(new Match { Identifier = i, Team1Id = teams[teamIndex].Id });
					teamIndex++;
				}
			}
			stages.Add(new Stage { StageNumber = secondStageIndex, Matches = matches });
		}

		private void AddTeamToMatch(Match match, Team team)
		{
			if (match.Team1Id is null)
				match.Team1Id = team.Id;
			else
				match.Team2Id = team.Id;
		}
	}
}
