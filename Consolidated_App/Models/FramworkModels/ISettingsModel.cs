﻿namespace Consolidated_App.Models.FramworkModels
{
    /// <summary>
    /// Represents a settings model
    /// </summary>
    public partial interface ISettingsModel
    {
        /// <summary>
        /// Gets or sets an active store scope configuration (store identifier)
        /// </summary>
        int ActiveStoreScopeConfiguration { get; set; }
    }
}