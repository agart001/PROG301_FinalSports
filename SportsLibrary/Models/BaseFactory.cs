using SportsLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Models
{
    public abstract class BaseFactory<TResult> : BaseEntity, IFactory<TResult>
    {
        internal readonly Func<TResult>? factory;

        public BaseFactory()
        {

        }

        public BaseFactory(string? filter)
        {
        }

        public BaseFactory(Func<TResult> _factory)
        {
            factory = _factory;
        }

        public abstract TResult Create();

        public abstract ICollection<TResult> CreateMultiple();
    }

    public abstract class StatFactory<IStat> : BaseFactory<IStat>
    {
        public StatFactory() : base() { }
        public StatFactory(Func<IStat> _factory) : base(_factory) { }
        public override IStat Create() => factory();
    }

    public class MMAStatFactory : StatFactory<IStat<object>>
    {
        public MMAStatFactory() : base() { }

        public MMAStatFactory(Func<IStat<object>> _factory) : base(_factory) { }

        public override ICollection<IStat<object>> CreateMultiple() =>
            new List<IStat<object>>()
            {
                new Stat<object>("KO","Opponents defeated by knockout", 0),
                new Stat<object>("TKO","Opponents defeated by technical knockout", 0),
                new Stat<object>("Submissions", "Opponents defeated by submission", 0),
                new Stat<object>("Decisions", "Opponents defeated by decision", 0)
            };
    }

    public class WrestlingStatFactory : StatFactory<IStat<object>>
    {
        public WrestlingStatFactory() : base() { }

        public WrestlingStatFactory(Func<IStat<object>> _factory) : base(_factory) { }

        public override ICollection<IStat<object>> CreateMultiple() =>
            new List<IStat<object>>()
            {
                new Stat<object>("Pin", "Opponents defeated by pin", 0),
                new Stat<object>("TF", "Opponents defeated by technical fall", 0),
                new Stat<object>("TD", "In-match takedowns", 0),
                new Stat<object>("Esc", "In-match escapes", 0),
                new Stat<object>("Rev", "In-match reversals", 0)
            };
    }
}
