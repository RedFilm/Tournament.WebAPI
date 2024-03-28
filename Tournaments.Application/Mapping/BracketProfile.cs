using AutoMapper;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Models;
using Tournaments.Domain.Models.BracketModels;

namespace Tournaments.Domain.Mapping
{
	public class BracketProfile : Profile
	{
		public BracketProfile()
		{
			CreateMap<Match, MatchModel>();
			CreateMap<Stage, StageModel>()
				.ForMember(stageModel => stageModel.Matches, opt => opt.MapFrom(stage => stage.Matches));
			CreateMap<Bracket, BracketModel>()
				.ForMember(bracketModel => bracketModel.Stages, opt => opt.MapFrom(bracket => bracket.Stages));
		}
	}
}
