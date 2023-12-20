using SportsLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Models
{
    /// <summary>
    /// Represents a base class for a person. Inherits <see cref="BaseEntity"/> and implements <see cref="IPerson"/>.
    /// </summary>
    public class BasePerson : BaseEntity, IPerson
    {
        #region FirstName

        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Sets the first name of the person.
        /// </summary>
        /// <param name="firstName">The first name to be set.</param>
        public void SetFirstName(string firstName) => FirstName = firstName;

        #endregion

        #region LastName

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Sets the last name of the person.
        /// </summary>
        /// <param name="lastName">The last name to be set.</param>
        public void SetLastName(string lastName) => LastName = lastName;

        #endregion

        #region Age

        /// <summary>
        /// Gets or sets the age of the person.
        /// </summary>
        public int? Age { get; set; }


        /// <summary>
        /// Sets the age of the person.
        /// </summary>
        /// <param name="age">The age to be set.</param>
        public void SetAge(int age) => Age = age;

        #endregion

        #region Position

        /// <summary>
        /// Gets or sets the position of the person.
        /// </summary>
        public string? Position { get; set; }


        /// <summary>
        /// Sets the position of the person.
        /// </summary>
        /// <param name="position">The position to be set.</param>
        public void SetPosition(string position) => Position = position;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor for BasePerson.
        /// Initializes a new instance with default values.
        /// </summary>
        public BasePerson() : base() { }

        /// <summary>
        /// Constructor for BasePerson with a specified name.
        /// Initializes a new instance with the provided name and default values for other properties.
        /// </summary>
        /// <param name="name">The name of the person.</param>
        public BasePerson(string? name) : base(name) { }

        /// <summary>
        /// Constructor for BasePerson with specified first and last names.
        /// Initializes a new instance with the provided first and last names, generating the full name, and default values for other properties.
        /// </summary>
        /// <param name="firstName">The first name of the person.</param>
        /// <param name="lastName">The last name of the person.</param>
        public BasePerson(string? firstName, string? lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Name = $"{FirstName} {LastName}";
            Age = 0;
            Position = string.Empty;
        }

        /// <summary>
        /// Constructor for BasePerson with specified first and last names, age, and position.
        /// Initializes a new instance with the provided parameters, generating the full name, and default values for other properties if not provided.
        /// </summary>
        /// <param name="firstName">The first name of the person.</param>
        /// <param name="lastName">The last name of the person.</param>
        /// <param name="age">The age of the person.</param>
        /// <param name="position">The position of the person.</param>
        public BasePerson(string? firstName, string? lastName, int? age, string? position)
        {
            FirstName = firstName ?? string.Empty;
            LastName = lastName ?? string.Empty;
            Name = $"{FirstName} {LastName}";
            Age = age ?? 0;
            Position = position ?? string.Empty;
        }

        #endregion
    }

    /// <summary>
    /// Represents a staff member. Inherits from <see cref="BasePerson"/> and implements <see cref="IStaff"/>.
    /// </summary>
    public class Staff : BasePerson, IStaff
    {
        #region Team

        /// <summary>
        /// Gets or sets the team associated with the staff member.
        /// </summary>
        public ITeam? Team { get; set; }

        /// <summary>
        /// Sets the team for the staff member.
        /// </summary>
        /// <param name="organization">The team to be set.</param>
        public void SetTeam(ITeam organization) => Team = organization;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor for Staff.
        /// Initializes a new instance with default values.
        /// </summary>
        public Staff() : base() { }

        /// <summary>
        /// Constructor for Staff with a specified name.
        /// Initializes a new instance with the provided name and default values for other properties.
        /// </summary>
        /// <param name="name">The name of the staff member.</param>
        public Staff(string? name) : base(name) { }

        /// <summary>
        /// Constructor for Staff with specified first and last names.
        /// Initializes a new instance with the provided first and last names, generating the full name, and default values for other properties.
        /// </summary>
        /// <param name="firstName">The first name of the staff member.</param>
        /// <param name="lastName">The last name of the staff member.</param>
        public Staff(string? firstName, string? lastName) : base(firstName, lastName) { }

        /// <summary>
        /// Constructor for Staff with specified first and last names, age, and position.
        /// Initializes a new instance with the provided parameters, generating the full name, and default values for other properties if not provided.
        /// </summary>
        /// <param name="firstName">The first name of the staff member.</param>
        /// <param name="lastName">The last name of the staff member.</param>
        /// <param name="age">The age of the staff member.</param>
        /// <param name="position">The position of the staff member.</param>
        public Staff(string? firstName, string? lastName, int? age, string? position)
            : base(firstName, lastName, age, position) { }

        /// <summary>
        /// Constructor for Staff with specified first and last names, age, position, and team.
        /// Initializes a new instance with the provided parameters, generating the full name, and default values for other properties if not provided.
        /// Also sets the team for the staff member.
        /// </summary>
        /// <param name="firstName">The first name of the staff member.</param>
        /// <param name="lastName">The last name of the staff member.</param>
        /// <param name="age">The age of the staff member.</param>
        /// <param name="position">The position of the staff member.</param>
        /// <param name="team">The team associated with the staff member.</param>
        public Staff(string? firstName, string? lastName, int? age, string? position, ITeam team)
            : base(firstName, lastName, age, position)
        {
            Team = team;
        }

        #endregion
    }

    /// <summary>
    /// Represents a player. Inherits from <see cref="BasePerson"/> and implements <see cref="IPlayer"/>.
    /// </summary>
    public class Player : BasePerson, IPlayer
    {
        #region Team

        /// <summary>
        /// Gets or sets the team associated with the player.
        /// </summary>
        public ITeam? Team { get; set; }

        /// <summary>
        /// Sets the team for the player.
        /// </summary>
        /// <param name="organization">The team to be set.</param>
        public void SetTeam(ITeam organization) => Team = organization;

        #endregion

        #region Stats

        /// <summary>
        /// Gets or sets the collection of statistics associated with the player.
        /// </summary>
        public ICollection<IStat<object>>? Stats { get; set; }

        /// <summary>
        /// Sets the statistics for the player.
        /// </summary>
        /// <param name="stats">The collection of statistics to be set.</param>
        public void SetStats(ICollection<IStat<object>> stats) => Stats = stats;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for Player.
        /// Initializes a new instance with default values.
        /// </summary>
        public Player() : base() { }

        /// <summary>
        /// Constructor for Player with a specified name.
        /// Initializes a new instance with the provided name and default values for other properties.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        public Player(string? name) : base(name) { }

        /// <summary>
        /// Constructor for Player with specified first and last names.
        /// Initializes a new instance with the provided first and last names, generating the full name, and default values for other properties.
        /// </summary>
        /// <param name="firstName">The first name of the player.</param>
        /// <param name="lastName">The last name of the player.</param>
        public Player(string? firstName, string? lastName) : base(firstName, lastName) { }

        /// <summary>
        /// Constructor for Player with specified first and last names, age, and position.
        /// Initializes a new instance with the provided parameters, generating the full name, and default values for other properties if not provided.
        /// </summary>
        /// <param name="firstName">The first name of the player.</param>
        /// <param name="lastName">The last name of the player.</param>
        /// <param name="age">The age of the player.</param>
        /// <param name="position">The position of the player.</param>
        public Player(string? firstName, string? lastName, int? age, string? position)
            : base(firstName, lastName, age, position) { }

        /// <summary>
        /// Constructor for Player with specified name, first and last names, age, position, team, and stats.
        /// Initializes a new instance with the provided parameters, generating the full name, and default values for other properties if not provided.
        /// Also sets the team and statistics for the player.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="firstName">The first name of the player.</param>
        /// <param name="lastName">The last name of the player.</param>
        /// <param name="age">The age of the player.</param>
        /// <param name="position">The position of the player.</param>
        /// <param name="team">The team associated with the player.</param>
        /// <param name="stats">The collection of statistics associated with the player.</param>
        public Player(string? name, string? firstName, string? lastName, int? age, string? position,
            ITeam team, ICollection<IStat<object>> stats) : base(firstName, lastName, age, position)
        {
            Team = team;
            Stats = stats;
        }

        #endregion
    }
}
