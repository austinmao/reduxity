using System;
using System.Collections.Generic;
using Zenject;
using NUnit.Framework;
using System.Linq;
using ModestTree;
using Assert=ModestTree.Assert;

namespace Zenject.Tests
{
    [TestFixture]
    public class TestAutoMocking
    {
        DiContainer _container;

        [SetUp]
        public void Setup()
        {
            _container = new DiContainer();
        }

        [Test]
        public void Test1()
        {
            _container.Bind<IFoo>().FromMock();

            _container.Bind<Bar>().AsSingle();

            _container.Resolve<Bar>().Run();
        }

        public class Bar
        {
            readonly IFoo _foo;

            public Bar(IFoo foo)
            {
                _foo = foo;
            }

            public void Run()
            {
                _foo.DoSomething();

                var result = _foo.GetTest();

                Assert.IsNull(result);
            }
        }

        public interface IFoo
        {
            string GetTest();

            void DoSomething();
        }
    }
}



