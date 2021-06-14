using EspOledTemp.Functions;
using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Navigation;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace EspOledTemp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MqttData mqttData = new MqttData();
        DateTime updateTime;

        public MainWindow()
        {
            InitializeComponent();

            lblEotDate.DataContext = mqttData;
            lblEotTime.DataContext = mqttData;
            lblEotTemp.DataContext = mqttData;
            lblEotHumidity.DataContext = mqttData;
            lblEotUptime.DataContext = mqttData;
            lblEotUptimeHuman.DataContext = mqttData;
            lblEotIp.DataContext = mqttData;
            lblStatus.DataContext = mqttData;

            // Update status label.
            mqttData.StsStatus = "Wait...";
            MqttSubscribe();
        }

        public class MqttData : INotifyPropertyChanged
        {
            private string _eotDate;
            private string _eotTime;
            private string _eotTemp;
            private string _eotHumidity;
            private string _eotUptime;
            private string _eotUptimeHuman;
            private string _eotIp;
            private DateTime _updateTime;
            private string _stsStatus;

            public string EotDate
            {
                get { return _eotDate; }
                set
                {
                    _eotDate = value;
                    OnPropertyChanged();
                }
            }
            public string EotTime
            {
                get { return _eotTime; }
                set
                {
                    _eotTime = value;
                    OnPropertyChanged();
                }
            }
            public string EotTemp
            {
                get { return _eotTemp; }
                set
                {
                    _eotTemp = value;
                    OnPropertyChanged();
                }
            }
            public string EotHumidity
            {
                get { return _eotHumidity; }
                set
                {
                    _eotHumidity = value;
                    OnPropertyChanged();
                }
            }
            public string EotUptime
            {
                get { return _eotUptime; }
                set
                {
                    _eotUptime = value;
                    OnPropertyChanged();
                }
            }
            public string EotUptimeHuman
            {
                get { return _eotUptimeHuman; }
                set
                {
                    _eotUptimeHuman = value;
                    OnPropertyChanged();
                }
            }
            public string EotIp
            {
                get { return _eotIp; }
                set
                {
                    _eotIp = value;
                    OnPropertyChanged();
                }
            }
            public DateTime UpdateTime
            {
                get { return _updateTime; }
                set
                {
                    _updateTime = value;
                    OnPropertyChanged();
                }
            }
            public string StsStatus
            {
                get { return _stsStatus; }
                set
                {
                    _stsStatus = value;
                    OnPropertyChanged();
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

         void MqttSubscribe()
        {
            //MessageBox.Show(Properties.Settings.Default.MqttServer, "Start");

            string mqttIp = Properties.Settings.Default.MqttServer;
            IPAddress address;

            if (IPAddress.TryParse(mqttIp, out address))
            {
                //    MessageBox.Show(mqttIp, address.AddressFamily.ToString());
                // Create client instance.
                MqttClient client = new MqttClient(mqttIp);

                try
                {
                    // Register to message received.
                    client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
                }
                catch
                {
                    MessageBox.Show("Mqtt error");
                }


                string clientId = Guid.NewGuid().ToString();

                try
                {
                    client.Connect(clientId);
                }
                catch (Exception)
                {
                    MessageBox.Show("Connection to Mqtt server failed!", "Connection failed", MessageBoxButton.OK);
                }
                //MessageBox.Show("Connected to Mqtt server", "", MessageBoxButton.OK);

                client.Subscribe(new string[] { Properties.Settings.Default.MqttTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            }
        }

        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // Handle message. 
            var curData = Encoding.UTF8.GetString(e.Message);
            updateTime = DateTime.Now;

            Console.WriteLine ($"{updateTime} - Received = {curData} on topic {e.Topic}");
            mqttData.StsStatus = $"{updateTime} - Data received";

            switch (e.Topic)
            {
                case "EspOledTemp/date":
                    mqttData.EotDate = "Date: " + curData;
                    break;
                case "EspOledTemp/time":
                    mqttData.EotTime = "Time: " + curData;
                    break;
                case "EspOledTemp/temp":
                    mqttData.EotTemp = "Temp: " + curData;
                    break;
                case "EspOledTemp/humidity":
                    mqttData.EotHumidity = "Humidity: " + curData;
                    break;
                case "EspOledTemp/uptime":
                    mqttData.EotUptime = "Uptime: " + curData;
                    break;
                case "EspOledTemp/uptimeHuman":
                    mqttData.EotUptimeHuman = "UptimeHuman: " + curData;
                    break;
                case "EspOledTemp/ip":
                    mqttData.EotIp = "Hardware's Ip-address: " + curData;
                    break;
            }
        }
        private void Hyperlink_RequestNavigatePaheco(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
        private void mnuSettings_Click(object sender, RoutedEventArgs e)
        {
            var sve = new handleSettings();
            sve.mnuhandleSettingsClicked();
        }
        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            var ext = new exit();
            ext.mnuExitClicked();
        }
    }
}
