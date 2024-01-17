using System.Windows;

namespace DesktopAlarm
{
    // MQTT Message Alarm Window
    public partial class NotificationWindow : Window
    {
        public NotificationWindow(string message)
        {
            InitializeComponent();
            
            // Set Message
            MessageTextBlock.Text = message;

            // Set Close Button
            ConfirmBtn.Click += CloseButton_Click;
        }

        // Button onclick event handler
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
} 