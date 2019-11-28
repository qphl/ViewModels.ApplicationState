// <copyright file="ApplicationStateViewModelReader.cs" company="Corsham Science">
// Copyright (c) Corsham Science. All rights reserved.
// </copyright>

namespace CorshamScience.ViewModels.ApplicationState
{
    using System;
    using System.Linq;
    using System.Web;
    using CorshamScience.ViewModels.Core;

    /// <inheritdoc />
    /// <summary>
    /// A implementation of the <see cref="IViewModelReader" /> interface that uses
    /// HttpApplicationState from System.Web to retrieve view models.
    /// </summary>
    public class ApplicationStateViewModelReader : IViewModelReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationStateViewModelReader"/> class, using a provided <see cref="HttpApplicationStateBase"/> to access view models.
        /// </summary>
        /// <param name="appState">The application state the view models should be retrieved from.</param>
        public ApplicationStateViewModelReader(HttpApplicationStateBase appState) => AppState = appState;

        private HttpApplicationStateBase AppState { get; }

        /// <inheritdoc />
        public TEntity GetByKey<TEntity>(string key)
            where TEntity : class
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Key must not be null.");
            }

            if (key == string.Empty)
            {
                throw new ArgumentException("Key must not be an empty string.", nameof(key));
            }

            return AppState.GetEntities<TEntity>().TryGetValue(key, out var result) ? result : null;
        }

        /// <inheritdoc />
        public IQueryable<TEntity> Query<TEntity>()
            where TEntity : class => AppState.GetEntities<TEntity>().Values.AsQueryable();
    }
}
