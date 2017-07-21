using NServiceBus;

namespace SimpleDemo.Messages
{
    public class DemoCommand : ICommand
    {
        public string Message { get; set; }
    }
}