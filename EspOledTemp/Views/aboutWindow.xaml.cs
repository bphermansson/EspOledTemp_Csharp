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
        }

        private void aboutClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Close();
        }
        private void Hyperlink_RequestNavigateAbout(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
    }
}
