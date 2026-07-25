using project.DataHandlers.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using project.Models;

namespace project.Forms.UserCotrols
{
    public partial class StudentNotificationControl : UserControl
    {
        public delegate void NotificationUpdatedEventHandler();
        public StudentNotificationControl()
        {
            InitializeComponent();
        }
        Student s;
        public StudentNotificationControl(Student student  )
        {
            InitializeComponent();
            this.s = student;
            LoadNotification();
        }
        public void LoadNotification()
        {
            flowLayoutPanel1.Controls.Clear();

            NotificationUpdatedEventHandler notificationUpdatedEventHandler = new NotificationUpdatedEventHandler(LoadNotification);
            NotificationViewModel.GetUnreadNotifications(s.AridNo).ForEach(n =>
            {

                flowLayoutPanel1.Controls.Add(new Notifications(n , notificationUpdatedEventHandler));
            });
        }


    }
}
