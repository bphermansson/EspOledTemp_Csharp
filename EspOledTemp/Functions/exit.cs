using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EspOledTemp.Functions
{
    public class exit
    {
        public void mnuExitClicked()
        {
            DialogResult res = MessageBox.Show("Are you sure you want to exit", "Really exit?", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                if (Application.MessageLoop)
                    Application.Exit();
                else
                    Environment.Exit(1);
            }
        }
    }
}
