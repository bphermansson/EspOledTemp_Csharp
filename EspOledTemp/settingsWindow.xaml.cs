
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
        public settingsWindow()
        {
            //var MqttServerSetting = "33";
            MessageBox.Show(Properties.Settings.Default.MqttServer, "Show settings");

            InitializeComponent();
        }

        void settingsWindow_Closing(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.MqttServer = txtMqttServerSetting.Text;
            
            Properties.Settings.Default.Save();

            MessageBox.Show(Properties.Settings.Default.MqttServer, "Closing called!!");

        }
        
        public class SettingsData : INotifyPropertyChanged
        {
            private string _mqttServerIp;

            public string MqttServerIp
            {
                get { return _mqttServerIp; }
                set
                {
                    _mqttServerIp = value;
                    OnPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
    }
