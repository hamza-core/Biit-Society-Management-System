using project.DataHandlers;
using project.DataHandlers.ViewModel;
using project.Forms.StudentControlM;
using project.Forms.StudentSocietyControlM.EventM;
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

namespace project.Forms.StudentSocietyControlM
{
    public partial class SocietyCard : UserControl
    {
        public SocietyCard()
        {
            InitializeComponent();
        }
        Society society;
        Student student;
        public SocietyCard(Society s, Student student)
        {
            society = s;
            this.student = student;
            InitializeComponent();



            GetData(society);
        }
        private void GetData(Society s)
        {

            lblSocietyTitle.Text = s.Name;
            lblDescription.Text = s.Description;
            lblNoOfMember.Text += SocietyMemberViewModel.GetBySociety(s.SocietyID).Count;
            lblPresidentname.Text += StudentViewModel.GetByAridNo(SocietyMemberViewModel.GetByRole("Leader", s.SocietyID)[0].AridNo).Name;
            lblMentorName.Text += SocietyViewModel.getMentorOfSociety(s.SocietyID).Name;

            List<Event> events = EventViewModel.GetEventsBySociety(s.SocietyID);
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Margin = new Padding(20);
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            if (events.Count > 0)
            {
                foreach (Event e in events)
                {
                    flowLayoutPanel1.Controls.Add(new
                    UpcomingEventControl(e));
                }
            }
            else
            {
                flowLayoutPanel1.Controls.Add(
                    new System.Windows.Forms.Panel
                    {
                        BackColor = System.Drawing.Color.DarkSlateGray,
                        Size = new System.Drawing.Size(180, 100),
                        Controls =
                        {
                            new System.Windows.Forms.Label
                            {
                                AutoSize = false,
                                Text = $"{s.Name} Has no Event Registered",
                                ForeColor = System.Drawing.Color.White,
                                Dock = DockStyle.Fill,
                                TextAlign = ContentAlignment.MiddleCenter,

                                Font = new Font("Arial", 16, FontStyle.Bold)
                            }
                        }   

                    });
            }


        }

        private void btnLeaveSociety_Click(object sender, EventArgs e)
        {
         SocietyMember sm =    SocietyMemberViewModel.GetByAridAndSociety(student.AridNo, society.SocietyID);

            if (sm != null)
            {
                DialogResult result = MessageBox.Show("Are you Sure you Want to Leave The Society ","Society Leave",MessageBoxButtons.YesNo);

                if (DialogResult.Yes == result)
                {
                    SocietyMemberViewModel.RemoveMember(sm.MemberID);

                    SocietyTeamMembershipViewModel.DeleteMembershipFromAllTeam(sm.MemberID);

                    MessageBoxHelper.ShowInfo("You Have Left the Society");

                    GetData(society);
                }
            }


        }



        private void SocietyCard_Load(object sender, EventArgs e)
        {

        }
    }
}
