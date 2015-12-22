﻿using ProEvoCanary.Domain.Helpers.Interfaces;

namespace ProEvoCanary.Domain.Helpers
{
    public class Configuration : IConfiguration
    {
        const string ProEvoLeagueConnectionString = "ProEvoLeagueConnectionString";

        public string GetConfig()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[ProEvoLeagueConnectionString].ToString();
        }
    }
}