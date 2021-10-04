using System;
using System.Windows;
using System.Windows.Navigation;

namespace EspOledTemp
{
    public partial class aboutWindow : Window
    {
        public aboutWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void aboutClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Close();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
        public string InfoText
        {
            get { return string.Format(
                "The base for this project is a NodeMCU,\n a Soc based on ESP8266. It has a sensor for temperature and" +
                " humidity connected, a HTU21D module.\nIt also have an OLED connected for showing measured values," +
                "these values are also sent via Mqtt to a Mqtt broker. The values are also presented via a dynamic webpage " +
                "where you can see the values in near realtime." +
                "This program is a Windows app that receives the Mqtt - values and display them in a window."
            ); }
        }
    }
}
