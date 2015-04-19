//-----------------------------------------------------------------------
// <copyright file="RemoteScoreStore.cs" company="Wonderful Components">
//     Copyright (c) Faculty of Informatics, University of Eötvös Loránd,
//     Budapest, Hungary. All rights reserved.
// </copyright>
// <author>David Hornyák</author>
//-----------------------------------------------------------------------

namespace RemoteScoreClient.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using DomainModel;
    using DomainModel.Contracts;

    /// <summary>
    /// Represents a score store which uses a remote HTTP endpoint the retrieve data and store leaderboard entries.
    /// </summary>
    public class RemoteScoreStore : IScoreStore
    {
        /// <summary>
        /// URI of the HTTP endpoint.
        /// </summary>
        private readonly string _endpointUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteScoreStore"/> class.
        /// </summary>
        /// <param name="leaderboardsEndpointUrl">URI of the HTTP endpoint.</param>
        public RemoteScoreStore(string leaderboardsEndpointUrl)
        {
            this._endpointUrl = leaderboardsEndpointUrl;
        }

        /// <summary>
        /// Gets the leaderboard for a specific game type and a scope by sending a GET request
        /// to the HTTP endpoint with the appropriate query parameters.
        /// </summary>
        /// <param name="gameType">Type of the game.</param>
        /// <param name="scope">Scope of the leaderboard in time.</param>
        /// <returns>A leaderboard containing the results.</returns>
        public async Task<IEnumerable<LeaderboardEntry>> GetLeaderboardAsync(GameTypes gameType, Scopes scope)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(this.BuildGetRequestUri(gameType, scope));

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<LeaderboardEntry[]>();
                }
                
                throw new InvalidOperationException("An error occurred while querying the HTTP endpoint; " + response.Content);
            }
        }

        /// <summary>
        /// Adds a new entry to the leaderboards by sending a POST request to the
        /// HTTP endpoint with a <see cref="LeaderboardEntry"/> object in the request body.
        /// </summary>
        /// <param name="entry">New entry to be added.</param>
        /// <returns>Void result of task execution.</returns>
        public async Task AddLeaderboardEntryAsync(LeaderboardEntry entry)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(this._endpointUrl, entry);

                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException("An error occurred while posting to the HTTP endpoint; " +
                                                        response.Content);
                }
            }
        }

        /// <summary>
        /// Builds the request URI for the GET request. The methods add the specific
        /// game type and scope identifier to the query string.
        /// </summary>
        /// <param name="gameType">Type of the game.</param>
        /// <param name="scope">Scope of the leaderboard in time.</param>
        /// <returns>A URI which the endpoint can be requested with.</returns>
        private Uri BuildGetRequestUri(GameTypes gameType, Scopes scope)
        {
            var builder = new UriBuilder(this._endpointUrl);

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["gameType"] = ((int)gameType).ToString();
            query["scope"] = ((int)scope).ToString();

            builder.Query = query.ToString();
            return builder.Uri;
        }
    }
}
