//-----------------------------------------------------------------------
// <copyright file="LeaderboardsController.cs" company="Wonderful Components">
//     Copyright (c) Faculty of Informatics, University of Eötvös Loránd,
//     Budapest, Hungary. All rights reserved.
// </copyright>
// <author>David Hornyák</author>
//-----------------------------------------------------------------------

namespace WebApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using DomainModel;
    using DomainModel.Contracts;

    /// <summary>
    /// Represents a controller class which serves HTTP requests of getting leaderboards
    /// and storing leaderboard entries.
    /// </summary>
    public class LeaderboardsController : ApiController
    {
        /// <summary>
        /// Underlying score store.
        /// </summary>
        private readonly IScoreStore _store;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaderboardsController"/> class.
        /// </summary>
        /// <param name="store">Resolved store to be used.</param>
        public LeaderboardsController(IScoreStore store)
        {
            this._store = store;
        }

        /// <summary>
        /// Serves GET requests of getting leaderboards.
        /// </summary>
        /// <param name="gameType">Type of the game.</param>
        /// <param name="scope">Scope of the leaderboard in time.</param>
        /// <returns>The filtered leaderboard in the response body and status code 200, or 400.</returns>
        public async Task<IHttpActionResult> Get(GameTypes gameType, Scopes scope)
        {
            try
            {
                return this.Ok(await this._store.GetLeaderboardAsync(gameType, scope));
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Serves POST requests of storing a new leaderboard entry in the store.
        /// </summary>
        /// <param name="entry">New entry to be stored.</param>
        /// <returns>The created resource in the response body and status code 201, or 400.</returns>
        public async Task<IHttpActionResult> Post([FromBody] LeaderboardEntry entry)
        {
            try
            {
                await this._store.AddLeaderboardEntryAsync(entry);
                return this.Created(string.Empty, entry);
            }
            catch (Exception ex)
            {
                return this.BadRequest("An error occured while saving the leaderboard entry; " + ex.Message);
            }
        }
    }
}
