using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Interfaces
{
    /// <summary>
    /// Interface representing a sport team, which contracts <see cref="IEntity"/>.
    /// </summary>
    public interface ITeam : IEntity
    {
  
        #region Scores

        #region Wins

        /// <summary>
        /// A Team's, public get and private set, int wins.
        /// </summary>
        public int? Wins { get; protected set; }

        /// <summary>
        /// Sets a team's wins.
        /// </summary>
        /// <param name="wins">The wins to be set.</param>
        public void SetWins(int? wins);

        #endregion

        #region Losses

        /// <summary>
        /// A Team's, public get and private set, int losses.
        /// </summary>
        public int? Loses { get; protected set; }

        /// <summary>
        /// Sets a team's loses.
        /// </summary>
        /// <param name="loses">The loses to be set.</param>
        public void SetLoses(int? loses);

        #endregion

        /// <summary>
        /// Gets a team's win loss ratio.
        /// </summary>
        /// <returns>Returns a double ratio.</returns>
        public double WinLossRatio();

        #endregion
    }
}
