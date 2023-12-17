using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Interfaces
{
    public interface ISport : IEntity
    {
        #region Category

        /// <summary>
        /// A Sport's, public get and protected set, ICategory category.
        /// </summary>
        public string Category { get; protected set; }

        /// <summary>
        /// Sets the sport's category.
        /// </summary>
        /// <param name="category">The category to be set.</param>
        public void SetCategory(string category);

        #endregion

        #region Rules

        /// <summary>
        /// A Sport's, public get and protected set, List<string> rules.
        /// </summary>
        public ICollection<string> Rules { get; protected set; }

        /// <summary>
        /// Sets a sport's rules.
        /// </summary>
        /// <param name="rules">The rules to be set.</param>
        public void SetRules(ICollection<string> rules);

        #endregion
    }

    public interface ITeam : IEntity
    {
        #region Symbol

        /// <summary>
        /// A Team's, public get and private set, string symbol.
        /// </summary>
        public string? Symbol { get; protected set; }

        /// <summary>
        /// Sets a team's symbol.
        /// </summary>
        /// <param name="symbol">The symbol to be set.</param>
        public void SetSymbol(string symbol);

        #endregion

        #region Location

        /// <summary>
        /// A Team's, public get and private set, string location.
        /// </summary>
        public string Location { get; protected set; }

        /// <summary>
        /// Sets a team's location.
        /// </summary>
        /// <param name="location">The location to be set.</param>
        public void SetLocation(string location);

        #endregion

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
