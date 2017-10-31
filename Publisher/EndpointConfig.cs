
using System;
using Messages.Commands;

namespace Publisher
{
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint, IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }
        public void Customize(BusConfiguration configuration)
        {
            configuration.Conventions().DefiningMessagesAs(t => t.Namespace != null && t.Namespace.Contains("Messages.Commands"));
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseDataBus<FileShareDataBus>().BasePath("C:\\FileShare");
        }

        public void Start()
        {
            SendDummyOrder(Bus);
        }

        private void SendDummyOrder(IBus bus)
        {
            while (true)
            {
                Console.WriteLine("Press 'Y' to submit a order or enter any key to exit");

                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.Y)
                {
                    Random rnd = new Random();
                    int batch = rnd.Next(1, 9999);
                    Bus.Send("Host", new SubmitOrder
                    {
                        BatchNumber = batch,
                        EnteredOn = DateTime.Now,
                        Id = Guid.NewGuid(),
                        SourceSystemName = "test"
                    });

                    Console.WriteLine();
                    Console.WriteLine("Order with batch # : " + batch.ToString() + " submitted successfully");
                }
                else
                {
                    break;
                }
            }
            Environment.Exit(0);
        }



        public void Stop()
        {
            //throw new System.NotImplementedException();
        }
    }
}
