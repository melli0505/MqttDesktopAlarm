using System.Windows;
using System.Windows.Threading;
using MQTTnet;
using MQTTnet.Client;

namespace DesktopAlarm
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            await ClientTest();
        }

        // MQTT Client setting
        public async Task ClientTest()
        {
            // Make Connection Setting
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder().WithTcpServer("BROKER_ADDRESS", 1883).Build();

            await mqttClient.ConnectAsync(options, CancellationToken.None);

            // Subscribe Topic
            var mqttSubscribeOptions = factory.CreateSubscribeOptionsBuilder().WithTopicFilter(
                f =>
                {
                    f.WithTopic("TOPIC");
                }
            ).Build();

            await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

            // On message handler
            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                var message = e.ApplicationMessage.ConvertPayloadToString();
                // Create Alarm window
                OnMessageReceived(message);
                Console.WriteLine("Received application message.");
                Console.WriteLine(message);
                return Task.CompletedTask;
            };

            Console.WriteLine("MQTT Client subscribed to topic.");
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();

        }
        public void OnMessageReceived(String message)
        {
            // Create UI Thread
            Dispatcher.Invoke(() =>
            {
                var notificationWindow = new NotificationWindow(message);
                notificationWindow.Show();
            });
        }

    }


}


