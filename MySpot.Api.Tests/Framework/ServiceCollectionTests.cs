using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Sdk;
using Shouldly;

namespace MySpot.Api.Tests.Framework
{
    public class ServiceCollectionTests
    {
        [Fact]
        public void test()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IMessenger, Messenger>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var messenger = serviceProvider.GetRequiredService<IMessenger>();
            messenger.Send();

            var messanger2 = serviceProvider.GetService<IMessenger>();
            messanger2.Send();

            messenger.ShouldNotBeNull();
            messanger2.ShouldNotBeNull();

            messenger.ShouldBe(messanger2);
        }

        private interface IMessenger
        {
            void Send();
        }

        private class Messenger : IMessenger
        {
            private readonly Guid _id = Guid.NewGuid();
            public void Send() => Console.WriteLine($"Sending a massage...[{_id}]");
        }
    }
}
