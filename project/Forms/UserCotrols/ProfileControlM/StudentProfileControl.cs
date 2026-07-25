using project.DataHandlers;
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
    public partial class StudentProfileControl : UserControl
    {
        public StudentProfileControl()
        {
            InitializeComponent();
        }
        Student s;
        int NoOfSociety;
        int NoOfEvent;
        int NoOfCertificate;

        public StudentProfileControl(Student s )
        {
            this.s = s;
            NoOfSociety = SocietyViewModel.GetCountOfSocietyJoinedByStudent(s.AridNo);
            NoOfEvent = EventParticipationViewModel.GetNoOfEventsParticipatedByStudent(s.AridNo);
            InitializeComponent();

            NoOfCertificate = 2;
          

        }
        private void button1_Click(object sender, EventArgs e)
        {
            EditProfile ep = new EditProfile(s);
            ep.ShowDialog();
        }
        

        private void StudentProfileControl_Load(object sender, EventArgs e)
        {

          
            lblName.Text = s.Name;
            lblArid.Text = s.AridNo;
            lblEmail.Text = s.Email;
            lblPhNo.Text = s.Phone;
            lblCertificate.Text = NoOfCertificate.ToString();
            lblSocityJoined.Text = NoOfSociety.ToString();
            lblEventPart.Text = NoOfEvent.ToString();
            lblPass.Text = s.Password;
            lblUSerName.Text = s.UserName;
            lblDep.Text = s.Department;
            




        }
    }
}
