using FluentAssertions;
using NUnit.Framework;
using System;

namespace DotNet.Core.Runtime.Faker.Unit.Tests
{
    public class RuntimeFakerTests
    {
        private RuntimeFaker runtimeFaker;
        private Clock clock;
        private MyClock mock;

        [SetUp]
        public void SetUp()
        {
            runtimeFaker = new RuntimeFaker();
            clock = new Clock();
            runtimeFaker.Register(clock);

            mock = new MyClock(new DateTime());
        }

        [Test]
        public void GetFakerFromRegisteredFakersIfHasNoChangedBehaviour() =>
            runtimeFaker.Get<Clock>().Should().Be(clock);

        [Test]
        public void GetFakerFromChangedFakersIfHasChangedBehaviour()
        {
            runtimeFaker.Change<Clock>(mock);
            runtimeFaker.Get<Clock>().Should().Be(mock);
        }

        [Test]
        public void GetNullIfFakerIsNotRegistered() =>
            runtimeFaker.Get<MyClock>().Should().BeNull();

        [Test]
        public void RegisterFakerTwiceShouldOnlyConsiderTheLast()
        {
            runtimeFaker.Register<Clock>(mock);
            runtimeFaker.Get<Clock>().Should().Be(mock);
        }

        [Test]
        public void ChangeFakerNotRegisterdShouldThrowException()
        {
            Action action = () => runtimeFaker.Change(mock);
            action.Should().ThrowExactly<InvalidOperationException>()
                .WithMessage($"There is no fake registered from type {typeof(MyClock)}");
        }

        [Test]
        public void ChangeFakerTwiceShouldOnlyConsiderTheLast()
        {
            runtimeFaker.Change<Clock>(mock);
            var newClock = new Clock();
            runtimeFaker.Change(newClock);
            runtimeFaker.Get<Clock>().Should().Be(newClock);
        }

        [Test]
        public void ResetChangeShouldClearFakeSetup()
        {
            runtimeFaker.Change<Clock>(mock);
            runtimeFaker.ResetChange<Clock>();
            runtimeFaker.Get<Clock>().Should().Be(clock);
        }

        [Test]
        public void ResetAllChangesShouldClearAllFakeSetup()
        {
            runtimeFaker.Register(mock);
            runtimeFaker.Change<Clock>(new MyClock(new DateTime()));
            runtimeFaker.Change(new MyClock(new DateTime()));

            runtimeFaker.ResetAllChanges();

            runtimeFaker.Get<Clock>().Should().Be(clock);
            runtimeFaker.Get<MyClock>().Should().Be(mock);
        }

        public class MyClock : Clock
        {
            private readonly DateTime now;

            public MyClock(DateTime now) => this.now = now;

            public override DateTime Now() => now;
        }

        public class Clock
        {
            public virtual DateTime Now() => DateTime.Now;
        }
    }
}
