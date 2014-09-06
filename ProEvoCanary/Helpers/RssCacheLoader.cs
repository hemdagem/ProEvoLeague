﻿using System.Collections.Generic;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;

namespace ProEvoCanary.Helpers
{
    public class RssCacheLoader : ICacheRssLoader
    {
        private readonly ICacheManager _cacheManager;

        public RssCacheLoader(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public RssCacheLoader() : this(new CachingManager()) { }

        public List<RssFeedModel> Load(string url)
        {
            return _cacheManager.Get(url) as List<RssFeedModel>;
        }

        public void AddToCache(string key, object value, int cacheHours)
        {
            _cacheManager.Add(key, value, cacheHours);
        }
    }
}