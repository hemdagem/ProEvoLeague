﻿using System;
using System.Runtime.Caching;
using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Repositories
{
    public class CachingManager : ICacheManager
    {
        private readonly MemoryCache _memoryCache;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy();
        public CachingManager(MemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public CachingManager() : this(MemoryCache.Default) { }

        public void Add(string key, object value, int cacheHours)
        {
            _policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(cacheHours);
            _memoryCache.Add(key, value, _policy);
        }

        public object Get(string key)
        {
            return _memoryCache.Contains(key) ? _memoryCache.Get(key) : null;
        }
    }
}