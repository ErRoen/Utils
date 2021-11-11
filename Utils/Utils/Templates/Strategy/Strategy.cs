using System;
using System.Collections.Generic;

namespace Utils.Templates.Strategy
{
    public interface IStrategy
    {
        void SomeMethod(object paramOne, object paramTwo);
        void SomeOtherMethod(object paramOne, object paramTwo);
    }

    // Allows for default/shared logic to be available to all concrete classes
    public abstract class AbstractConcreteStrategy : IStrategy
    {
        public void SomeMethod(object paramOne, object paramTwo)
        {
            // Some shared implementation
        }

        // No default, MUST be implemented in concrete classes
        public abstract void SomeOtherMethod(object paramOne, object paramTwo);
    }

    public class DefaultStrategy : IStrategy
    {
        public void SomeMethod(object paramOne, object paramTwo) { throw new NotImplementedException(); }
        public void SomeOtherMethod(object paramOne, object paramTwo) { throw new NotImplementedException(); }
    }

    public class ConcreteStrategy : AbstractConcreteStrategy
    {
        public override void SomeOtherMethod(object paramOne, object paramTwo) { throw new NotImplementedException(); }
    }

    public class AnotherConcreteStrategy : IStrategy
    {
        public void SomeMethod(object paramOne, object paramTwo) { throw new NotImplementedException(); }
        public void SomeOtherMethod(object paramOne, object paramTwo) { throw new NotImplementedException(); }
    }


    public class StrategyFactory
    {
        public Dictionary<object, Lazy<IStrategy>> Strategies = new Dictionary<object, Lazy<IStrategy>>();
        private readonly IStrategy _defaultStrategy = new Lazy<IStrategy>(() => new DefaultStrategy()).Value;

        public StrategyFactory()
        {
            Strategies.Add(new object(), new Lazy<IStrategy>(() => new ConcreteStrategy()));
            Strategies.Add(new object(), new Lazy<IStrategy>(() => new AnotherConcreteStrategy()));
            //  Add more concrete strategies here.
        }

        public IStrategy GetStrategy(Guid key)
        {
            return Strategies.TryGetValue(key, out var output)
                ? output.Value
                : _defaultStrategy;
        }
    }
}
