
namespace SportsLibrary.Interfaces
{
    public interface IPerson : IEntity
    {
        #region First Name

        /// <summary>
        /// A Person's, public get and protected set, string first name.
        /// </summary>
        public string? FirstName { get; protected set; }

        /// <summary>
        /// Sets a person's first name.
        /// </summary>
        /// <param name="firstName">The first name to be set.</param>
        public void SetFirstName(string firstName);

        #endregion

        #region Last Name

        /// <summary>
        /// A Person's, public get and protected set, string last name.
        /// </summary>
        public string? LastName { get; protected set; }
        public void SetLastName(string lastName);

        #endregion

        #region Age

        /// <summary>
        /// A Person's, public get and protected set, integer age name.
        /// </summary>
        public int? Age { get; protected set; }

        /// <summary>
        /// Sets a person's age.
        /// </summary>
        /// <param name="age">The age to be set.</param>
        public void SetAge(int age);

        #endregion

        #region Position

        /// <summary>
        /// A Worker's, public get and protected set, string position(occupation).
        /// </summary>
        public string? Position { get; protected set; }

        /// <summary>
        /// Sets a person's position.
        /// </summary>
        /// <param name="position">The position to be set.</param>
        public void SetPosition(string position);

        #endregion

        #region Duties

        /// <summary>
        /// A Worker's, public get and protected set, List<string> of their duties(responsibilities).
        /// </summary>
        public ICollection<string>? Duties { get; protected set; }

        /// <summary>
        /// Sets a person's duties.
        /// </summary>
        /// <param name="duties">The duties to be set.</param>
        public void SetDuties(ICollection<string> duties);

        #endregion

        #region Salary

        /// <summary>
        /// A Worker's, public get and protected set, decimal salary(pay).
        /// </summary>
        public decimal? Salary { get; protected set; }

        /// <summary>
        /// Sets a person's salary.
        /// </summary>
        /// <param name="salary">The salary to be set.</param>
        public void SetSalary(decimal salary);

        #endregion
    }

    public interface IStaff : IPerson 
    {
        #region Team

        /// <summary>
        /// A Player's, public get and protected set, ITeam team.
        /// </summary>
        public ITeam Team { get; protected set; }

        /// <summary>
        /// Sets a player's team.
        /// </summary>
        /// <param name="team">The team to be set.</param>
        public void SetTeam(ITeam team);

        #endregion

    }

    public interface IPlayer : IPerson
    {
        #region Team

        /// <summary>
        /// A Player's, public get and protected set, ITeam team.
        /// </summary>
        public ITeam Team { get; protected set; }

        /// <summary>
        /// Sets a player's team.
        /// </summary>
        /// <param name="team">The team to be set.</param>
        public void SetTeam(ITeam team);

        #endregion

        #region Stats

        /// <summary>
        /// A Player's, public get and protected set, Dictionary<string, object> stats.
        /// </summary>
        public ICollection<IStat<object>> Stats { get; protected set; }

        /// <summary>
        /// Sets a player's stats.
        /// </summary>
        /// <param name="stats">The stats to be set.</param>
        public void SetStats(ICollection<IStat<object>> stats);

        #endregion

    }
}
