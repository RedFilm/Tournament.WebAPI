﻿using Tournaments.Domain.Enums.Statuses;

namespace Tournaments.Domain.Models.TeamModels
{
    public class TeamModel
    {
        public long OwnerId { get; set; }

        public string TeamName { get; set; } = null!;
        public TournamentTeamStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
