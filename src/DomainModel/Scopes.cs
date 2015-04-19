//-----------------------------------------------------------------------
// <copyright file="Scopes.cs" company="Wonderful Components">
//     Copyright (c) Faculty of Informatics, University of Eötvös Loránd,
//     Budapest, Hungary. All rights reserved.
// </copyright>
// <author>David Hornyák</author>
//-----------------------------------------------------------------------

namespace DomainModel
{
    /// <summary>
    /// Represents the enumeration of supported leaderboard scopes.
    /// </summary>
    public enum Scopes
    {
        /// <summary>
        /// Scope of all time leaderboards.
        /// </summary>
        AllTime = 1,

        /// <summary>
        /// Scope of weekly leaderboards.
        /// </summary>
        Weekly = 2,

        /// <summary>
        /// Scope of daily leaderboards.
        /// </summary>
        Daily = 3
    }
}
