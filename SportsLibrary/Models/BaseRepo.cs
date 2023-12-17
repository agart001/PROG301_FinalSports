using SportsLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Models
{
    public abstract class BaseRepo<TKey, TValue> : BaseEntity, IRepo<TKey, TValue> where TKey : notnull
    {
        #region Contents

        public Dictionary<TKey, ICollection<TValue>>? Contents { get; set; }
        public void SetContents(Dictionary<TKey, ICollection<TValue>>? contents) => Contents = contents;

        #endregion

        #region Get

        #region Values

        public ICollection<TValue>? GetValue(TKey key)
        {
            if (Contents == null) throw new NullReferenceException(nameof(Contents));
            return Contents[key];
        }

        public ICollection<ICollection<TValue>>? GetValues()
        {
            if (Contents == null) throw new NullReferenceException(nameof(Contents));
            return Contents.Values;
        }

        #endregion

        #region Keys

        public TKey? GetKey(ICollection<TValue> col)
        {
            if (Contents == null) throw new NullReferenceException(nameof(Contents));
            return Contents.FirstOrDefault(kvp => kvp.Value == col).Key;
        }

        public ICollection<TKey>? GetKeys()
        {
            if (Contents == null) throw new NullReferenceException(nameof(Contents));
            return Contents.Keys;
        }

        #endregion

        #endregion

        #region Add

        public void Add(TKey key, ICollection<TValue> col)
        {
            if(Contents == null) throw new NullReferenceException(nameof(Contents));
            Contents.Add(key, col);
        }

        public void ValueAdd(TKey key, TValue value)
        {
            if (Contents == null) throw new NullReferenceException(nameof(Contents));
            Contents[key].Add(value);
        }

        #endregion

        #region Remove

        public void Remove(TKey key)
        {
            if (Contents == null) throw new NullReferenceException(nameof(Contents));
            Contents.Remove(key);
        }

        public void ValueRemove(TKey key, TValue value)
        {
            if (Contents == null) throw new NullReferenceException(nameof(Contents));
            Contents[key].Remove(value);
        }

        #endregion

        #region Constructors

        public BaseRepo()
        {
            Contents = new Dictionary<TKey, ICollection<TValue>>();
        }

        public BaseRepo(string? name) : base(name) { }

        public BaseRepo(string? name, Dictionary<TKey, ICollection<TValue>>? contents) : base(name)
        {
            Contents = contents;
        }

        public BaseRepo(string? name, TKey[] keys, ICollection<TValue>[] values) : base(name)
        {
            Contents = new Dictionary<TKey, ICollection<TValue>>();
            for (int i = 0; i < keys.Length; i++)
            {
                Contents.Add(keys[i], values[i]);
            }
        }

        #endregion

    }

    public class TestRepo : BaseRepo<string, int>
    {
        public TestRepo()
        {
            Contents = new Dictionary<string, ICollection<int>>
            {
                {"test 1", new List<int> { 1, 2, 3, 4 , 5 } },
                { "test 2", new List<int> { 6, 7, 8, 9, 10 } }
            };
        }
    }

    public class SportRepo : BaseRepo<Guid, Sport>
    {
        public SportRepo()
        {
            Contents = new Dictionary<Guid, ICollection<Sport>>
            {
                {
                    Guid.NewGuid(), 
                    new List<Sport>
                    {
                        new Sport("MMA", "Combat", new Dictionary<Guid, ICollection<Team>>
                        {
                            {
                                Guid.NewGuid(), 
                                new List<Team>
                                {
                                    new Team("FireHawks"),
                                    new Team("SnowPigeons")
                                }
                            }
                        }),
                        new Sport("Wrestling", "Combat", new Dictionary<Guid, ICollection<Team>>
                        {
                            {
                                Guid.NewGuid(),
                                new List<Team>
                                {
                                    new Team("LTHS", new Dictionary<Type, ICollection<object>>
                                    {
                                        {typeof(Staff), new List<object>{ new Staff("Matt", "King") } },
                                        {typeof(Player), new List<object>{ new Player("Alex", "Gartner") } }
                                    }),
                                    new Team("HCHS")
                                }
                            }
                        })
                    }
                }
            };
        }
    }

    public class Sport : BaseRepo<Guid, Team>, ISport
    {
        public Sport() : base() { }

        public Sport(string? name) : base(name) { }

        public Sport(string? name, string category) : base(name) 
        {
            Category = category;
        }

        public Sport(string? name, Dictionary<Guid, ICollection<Team>>? contents) : base(name, contents) { }

        public Sport(string? name, string? category, Dictionary<Guid, ICollection<Team>>? contents) : base(name, contents) 
        {
            Category = category ?? throw new ArgumentNullException(nameof(category));
        }

        public Sport(string? name, string? category, string? description, Dictionary<Guid, ICollection<Team>>? contents) : base(name, contents)
        {
            Category = category ?? throw new ArgumentNullException(nameof(category));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public Sport(string? name, Guid[] keys, ICollection<Team>[] values) : base(name, keys, values) { }

        public Sport(string? name, string? category, Guid[] keys, ICollection<Team>[] values) : base(name, keys, values) 
        {
            Category = category ?? throw new ArgumentNullException(nameof(category));
        }

        public string Category { get; set; }
        public ICollection<string> Rules { get; set; }


        public void SetCategory(string category) => Category = category;

        public void SetRules(ICollection<string> rules) => Rules = rules;
    }

    public class Team : BaseRepo<Type, object>, ITeam
    {
        public Team() : base() { }
        public Team(string? name) : base(name) { }
        public Team(string? name, Dictionary<Type, ICollection<object>>? contents) : base(name, contents) { }
        
        public Team(string? name, Type[] keys, ICollection<object>[] values) : base(name, keys, values) { }

        public string? Symbol { get ; set; }

        public void SetSymbol(string? symbol) => Symbol = symbol;

        public string? Location { get; set; }
        public void SetLocation(string? location) => Location = location;

        public int? Wins { get; set; }
        public void SetWins(int? wins) => Wins = wins;

        public int? Loses { get; set; }
        public void SetLoses(int? loses) => Loses = loses;

        public double WinLossRatio()
        {
            if (Wins == null || Loses == null) throw new InvalidOperationException(nameof(WinLossRatio));
            return (double)Wins / (double)Loses;
        }
    }
}
