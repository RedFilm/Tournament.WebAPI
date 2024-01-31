﻿namespace Tournaments.Domain.Options
{
	public class JwtOptions
	{
		public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
		public string Audience { get; set; } = null!;
    }
}