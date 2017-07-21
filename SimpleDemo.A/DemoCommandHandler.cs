using System;
using System.Threading.Tasks;
using NServiceBus;
using SimpleDemo.Messages;

namespace SimpleDemo.A
{
    public class DemoCommandHandler : IHandleMessages<DemoCommand>
    {
        public Task Handle(DemoCommand message, IMessageHandlerContext context)
        {
            Console.WriteLine($"{DateTime.Now:T}\tReceived {nameof(DemoCommand)}");

            return Task.CompletedTask;
        }
    }
}