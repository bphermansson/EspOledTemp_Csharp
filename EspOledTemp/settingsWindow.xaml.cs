
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace EspOledTemp
{
    /// <summary>
    /// Interaction logic for settingsWindow.xaml
    /// </summary>
    public partial class settingsWindow : Window
    {
        string MqttServerIp = Properties.Settings.Default.MqttServer;
        public settingsWindow()
        {
            InitializeComponent();
            txtMqttServerSetting.Text = MqttServerIp;
        }

        void settingsWindow_Closing(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.MqttServer = txtMqttServerSetting.Text;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }
        void btnSaveClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MqttServer = txtMqttServerSetting.Text;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
            btnSave.IsEnabled = false;
        }
        void btnCancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
