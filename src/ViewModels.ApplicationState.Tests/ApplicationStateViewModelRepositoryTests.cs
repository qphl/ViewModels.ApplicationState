// <copyright file="ApplicationStateViewModelRepositoryTests.cs" company="Corsham Science">
// Copyright (c) Corsham Science. All rights reserved.
// </copyright>

namespace CorshamScience.ViewModels.Core.Tests
{
    using System;
    using System.Web;
    using CorshamScience.ViewModels.ApplicationState;
    using NUnit.Framework;

    /// <inheritdoc />
    [TestFixture]
    internal sealed class ApplicationStateViewModelRepositoryTests : ViewModelRepositoryTestFixture
    {
        /// <summary>
        /// Setup for the test fixture.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            var state = new HttpApplicationStateWrapper((HttpApplicationState)Activator.CreateInstance(typeof(HttpApplicationState), true));
            Writer = new ApplicationStateViewModelWriter(state);
            Reader = new ApplicationStateViewModelReader(state);
        }
    }
}
