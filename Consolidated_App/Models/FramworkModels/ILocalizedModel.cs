﻿namespace Consolidated_App.Models.FramworkModels
{
    /// <summary>
    /// Represents localized model
    /// </summary>
    public interface ILocalizedModel
    {
    }

    /// <summary>
    /// Represents generic localized model
    /// </summary>
    /// <typeparam name="TLocalizedModel">Localized model type</typeparam>
    public interface ILocalizedModel<TLocalizedModel> : ILocalizedModel
    {
        /// <summary>
        /// Gets or sets localized locale models
        /// </summary>
        IList<TLocalizedModel> Locales { get; set; }
    }
}