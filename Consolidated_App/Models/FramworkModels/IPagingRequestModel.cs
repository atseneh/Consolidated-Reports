﻿namespace Consolidated_App.Models.FramworkModels
{
    /// <summary>
    /// Represents a paging request model
    /// </summary>
    public partial interface IPagingRequestModel
    {
        /// <summary>
        /// Gets a page number
        /// </summary>
        int Page { get; }

        /// <summary>
        /// Gets a page size
        /// </summary>
        int PageSize { get; }
    }
}