using SportsLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Models
{
    public class BasePerson : BaseEntity, IPerson
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? Position { get; set; }
        public ICollection<string>? Duties { get; set; }
        public decimal? Salary { get; set; }


        #region Setters

        public void SetFirstName(string firstName) => FirstName = firstName;

        public void SetLastName(string lastName) => LastName = lastName;

        public void SetAge(int age) => Age = age;

        public void SetPosition(string position) => Position = position;

        public void SetDuties(ICollection<string> duties) => Duties = duties;

        public void SetSalary(decimal salary) => Salary = salary;

        #endregion

        public BasePerson() : base() { }

        public BasePerson(string? name) : base(name) { }

        public BasePerson(string? firstName, string? lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = 0;
            Position = string.Empty;
            Duties = new List<string>();
            Salary = 0m;
        }

        public BasePerson(string? firstName, string? lastName, int? age, string? position, List<string>? duties, decimal? salary)
        {
            FirstName = firstName ?? string.Empty;
            LastName = lastName ?? string.Empty;
            Name = $"{FirstName} {LastName}";
            Age = age ?? 0;

            Position = position ?? string.Empty;
            Duties = duties;
            Salary = salary ?? 0m;
        }

    }

    public class Staff : BasePerson, IStaff
    {
        public ITeam Team { get; set; }
        public void SetTeam(ITeam organization) => Team = organization;

        public Staff() : base() { }
        public Staff(string? name) : base(name) { }
        public Staff (string? firstName, string? lastName) : base(firstName, lastName) { }
        public Staff(string? firstName, string? lastName, int? age, string? position, List<string>? duties, decimal? salary)
            : base(firstName, lastName, age, position, duties, salary) { }

        public Staff(string? firstName, string? lastName, int? age, string? position, List<string>? duties, decimal? salary, ITeam team)
            : base(firstName, lastName, age, position, duties, salary) 
        {
            Team = team;
        }
    }

    public class Player : BasePerson, IPlayer
    {
        public ITeam Team { get; set; }
        public void SetTeam(ITeam organization) => Team = organization;
        public ICollection<IStat<object>> Stats { get; set; }
        public void SetStats(ICollection<IStat<object>> stats) => Stats = stats;

        public Player() : base() { }
        public Player(string? name) : base(name) { }
        public Player(string? firstName, string? lastName) : base(firstName, lastName) { }
        public Player(string? firstName, string? lastName, int? age, string? position, List<string>? duties, decimal? salary)
            : base(firstName, lastName, age, position, duties, salary) { }

        public Player(string? name, string? firstName, string? lastName, int? age, string? position, List<string>? duties, decimal? salary,
            ITeam team, ICollection<IStat<object>> stats) : base(firstName, lastName, age, position, duties, salary)
        {
            Team = team;
            Stats = stats;
        }
    }
}
