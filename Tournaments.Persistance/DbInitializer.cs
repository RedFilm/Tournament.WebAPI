﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournaments.Persistance
{
	public static class DbInitializer
	{
		public static void Initialize(TournamentsDbContext context)
		{
			context.Database.EnsureCreated();
		}
	}
}
