﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using CR.ViewModels.Core;

namespace CR.ViewModels.Persitance.ApplicationState
{
    public class ApplicationStateViewModelReader : IViewModelReader
    {
        private HttpApplicationStateBase AppState { get; set; }

        public ApplicationStateViewModelReader(HttpApplicationStateBase appState)
        {
            AppState = appState;
        }

        public TEntity GetByKey<TEntity>(string key) where TEntity : class
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (key == "")
                throw new ArgumentException("key must not be an empty string", "key");

            var entities = GetEntities<TEntity>();

            TEntity result;
            return entities.TryGetValue(key, out result) ? result : null;
        }

        /*
        public IEnumerable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            var entities = GetEntities<TEntity>();
            return entities == null ? new List<TEntity>() : entities.Values.Where(predicate.Compile());
        }
        */

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            var entities = GetEntities<TEntity>();
            return entities.Values.AsQueryable();
        }

        private Dictionary<String, TEntity> GetEntities<TEntity>()
        {
            var appStateKey = typeof(TEntity).FullName;
            var entities = AppState[appStateKey];
            return entities == null ? new Dictionary<string, TEntity>() : (Dictionary<string, TEntity>)entities; 
        }
    }
}
