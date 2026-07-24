using project.DataHandlers.ViewModel;
using project.Models;
using project.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project.Forms.UserCotrols
{
    public partial class Notifications : UserControl
    {
        public Notifications()
        {
            InitializeComponent();
        }
        Notification n;
        public Notifications(Notification notification ,Delegate @delegate )
        {   this.n = notification;
            InitializeComponent();
            label1.Text = notification.Title;
            label2.Text = notification.Message;
            label3.Text = notification.CreatedAt.ToString();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool x = NotificationViewModel.MarkAsRead(n.NotificationID);
            if (x)
            {
                MessageBoxHelper.ShowInfo("Notification marked as read successfully.","Notification");

            }
            else
            {
                MessageBox.Show("Error marking notification as read. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
