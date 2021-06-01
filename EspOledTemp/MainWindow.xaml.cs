﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace WpfApp1
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

            // Display something while waiting for data.
            mqttData.EotTemp = "Wait...";

            Subscribe();
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
            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            MessageBox.Show("Subscribed to Mqtt topic.");
        }
         void Subscribe()
        {
            // Create client instance.
            MqttClient client = new MqttClient("192.168.1.190");

            // Register to message received.
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);

            // subscribe to topics "EspOledTemp/#" with QoS 2 
            client.Subscribe(new string[] { "EspOledTemp/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // Handle message. 
            var curData = Encoding.UTF8.GetString(e.Message);
            Console.WriteLine("Received = " + curData + " on topic " + e.Topic);

            updateTime = DateTime.Now;
            Console.WriteLine("Time: = " + updateTime);

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
    }
}
