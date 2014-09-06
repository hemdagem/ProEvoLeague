﻿namespace ProEvoCanary.Repositories.Interfaces
{
    public interface ICacheResultsRepository : IResultRepository    {
        void AddToCache(string key, object value, int cacheHours);
    }
}