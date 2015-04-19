//-----------------------------------------------------------------------
// <copyright file="LeaderboardEntry.cs" company="Wonderful Components">
//     Copyright (c) Faculty of Informatics, University of Eötvös Loránd,
//     Budapest, Hungary. All rights reserved.
// </copyright>
// <author>David Hornyák</author>
//-----------------------------------------------------------------------

namespace DomainModel
{
    using System;
    using System.Linq;

    /// <summary>
    /// Represents an entry in a leaderboard.
    /// </summary>
    public class LeaderboardEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeaderboardEntry"/> class.
        /// </summary>
        public LeaderboardEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaderboardEntry"/> class.
        /// </summary>
        /// <param name="rawCsv">A semicolon separated string containing the field values.</param>
        public LeaderboardEntry(string rawCsv)
        {
            var fields = rawCsv.Split(';');
            var index = 0;

            foreach (var prop in typeof(LeaderboardEntry).GetProperties())
            {
                var convertedValue = prop.PropertyType.IsEnum ? Enum.Parse(prop.PropertyType, fields[index]) : Convert.ChangeType(fields[index], prop.PropertyType);
                this.GetType().GetProperty(prop.Name).SetValue(this, convertedValue, null);
                ++index;
            }
        }

        /// <summary>
        /// Gets or sets the identifier of the entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the entry.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the game type of the entry.
        /// </summary>
        public GameTypes GameType { get; set; }

        /// <summary>
        /// Gets or sets the date of the entry.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the game session identifier of the entry.
        /// </summary>
        public string GameSessionId { get; set; }

        /// <summary>
        /// Gets or sets the score of the entry.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets the header used in csv files containing leaderboard entries.
        /// </summary>
        /// <returns>A semicolon separated string of the property names.</returns>
        public static string GetCsvHeader()
        {
            return typeof(LeaderboardEntry).GetProperties()
                .Select(i => i.Name)
                .Aggregate((i, j) => i + ';' + j);
        }

        /// <summary>
        /// Converts the object to a semicolon separated string containing the field values.
        /// </summary>
        /// <returns>A semicolon separated string representation oh the object.</returns>
        public string ToCsvString()
        {
            return typeof(LeaderboardEntry).GetProperties()
                .Select(info => this.GetType().GetProperty(info.Name).GetValue(this, null).ToString())
                .Aggregate((i, j) => i + ';' + j);
        }
    }
}
