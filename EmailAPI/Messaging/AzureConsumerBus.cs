using Azure.Messaging.ServiceBus;
using EmailAPI.Contracts;
using EmailAPI.DTO;
using EmailAPI.Services;
using EmailAPI.Util;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Text;

namespace EmailAPI.Messaging
{
    public class AzureConsumerBus : IAzureConsumerBus
    {

        private readonly IEmailService _emailService;
        private readonly string _serviceBusConnectionString = Constants.serviceBusConnectionString;
        private readonly string _emailQueue = Constants.emailQueueName;
        // if we want to listen to a queue
        private ServiceBusProcessor _emailProcessor;

        public AzureConsumerBus(IEmailService emailService)
        {
            var client = new ServiceBusClient(_serviceBusConnectionString);

            _emailProcessor = client.CreateProcessor(_emailQueue);
            _emailService = emailService;

        }

        public async Task Start()
        {
            _emailProcessor.ProcessMessageAsync += OnEmailRequestReceived;
            _emailProcessor.ProcessErrorAsync += ErrorHandler;

            await _emailProcessor.StartProcessingAsync();
            
        }

        public async Task Stop()
        {
            await _emailProcessor.StopProcessingAsync();

            await _emailProcessor.DisposeAsync();

        }

        private Task ErrorHandler(ProcessErrorEventArgs arg)
        {
            Console.WriteLine(arg.Exception.ToString());

            return Task.CompletedTask;
        }

        private async Task OnEmailRequestReceived(ProcessMessageEventArgs arg)
        {
            var message = arg.Message;

            var body = Encoding.UTF8.GetString(message.Body);

            AddUserDto obj = JsonConvert.DeserializeObject<AddUserDto>(body);

            try
            {
                // try to log to email and send out email

                
                await _emailService.EmailAndLogEmailAsync(obj);
                await arg.CompleteMessageAsync(arg.Message);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
