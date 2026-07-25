using project.DataHandlers;
using project.DataHandlers.ViewModel;
using project.Forms.StudentSocietyControlM.EventM;
using project.Models;
using project.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;

namespace project.Forms.UserCotrols.StudentSocietyControlM.EventM
{
    public partial class EventMainControl : UserControl
    {
        public EventMainControl()
        {
            InitializeComponent();
        }

        List<SocietyMember> societyMembers;
        List<Society> societies = new List<Society>();
        List<SocietyMember> memberRequest = new List<SocietyMember>();
        List<SocietyMember> searchList = new List<SocietyMember>();
        List<SocietyTeam> team = new List<SocietyTeam>();
        private SocietyMember selectedMember;
        Student s;

        public EventMainControl(List<SocietyMember> members, Student s)
        {
            InitializeComponent();
            this.societyMembers = members;
            this.s = s;

            foreach (var r in members)
            {
                societies.Add(SocietyViewModel.GetById(r.SocietyID.ToString()));
            }
        }

        private void EventMainControl_Load(object sender, EventArgs e)
        {
            loadSocieties();


        }

        public void loadSocieties()
        {
            comboBox1.DataSource = societies;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "SocietyID";
            comboBox5.DataSource = societies;
            comboBox5.DisplayMember = "Name";
            comboBox5.ValueMember = "SocietyID";
            cmbTeamSociety.DataSource = societies;
            cmbTeamSociety.DisplayMember = "Name";
            cmbTeamSociety.ValueMember = "SocietyID";

        }

        public void loadComboBoxteams(int id)
        {
            team = SocietyTeamViewModel.GetTeamsBySociety(id);
            cmbTeam.DataSource = team;
            cmbTeam.DisplayMember = "TeamName";
            cmbTeam.ValueMember = "TeamID";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Society s = (Society)comboBox1.SelectedItem;
            if (s != null)
            {
                LoadMembers(s.SocietyID);
                LoadRequestMembers(s.SocietyID);
                searchList = SocietyMemberViewModel.GetBySociety(s.SocietyID);
                textBox1.Clear();
                ClearMemberDetails();
            }
        }

        public void LoadMembers(int id)
        {
            var sm = SocietyMemberViewModel.GetBySociety(id);
            societyMembers = sm;

            var data = sm.Select(x =>
            {
                Student s = StudentViewModel.GetByAridNo(x.AridNo);
                return new
                {
                    MemberID = x.MemberID,
                    Name = s.Name,
                    AridNo = x.AridNo,
                    Email = s.Email,
                    PhoneNo = s.Phone,
                    Role = x.Role,
                    JoiningDate = x.JoiningDate
                };
            }).ToList();
            cmbMember.DataSource = sm;
            cmbMember.DisplayMember = "AridNo";
            cmbMember.ValueMember = "AridNo";
            dataGridView1.DataSource = data;
        }
        public void LoadTeams(int id)
        {
            var Team = SocietyTeamViewModel.GetTeamsBySociety(id);

            var data = Team.Select(x => new
            {
                TeamId = x.TeamID,
                name = x.TeamName,
                Description = x.Description,
            }).ToList();
            dgvTeam.DataSource = data;
        }

        public void LoadRequestMembers(int id)
        {
            memberRequest = SocietyMemberViewModel.GetAllMembersRequests(id.ToString());
            comboBox2.DataSource = null;
            comboBox2.DataSource = memberRequest;
            comboBox2.DisplayMember = "AridNo";
            comboBox2.ValueMember = "AridNo";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SocietyMember sm = (SocietyMember)comboBox2.SelectedItem;
            if (sm != null)
            {
                Student s = StudentViewModel.GetByAridNo(sm.AridNo);
                lblName.Text = s.Name;
                lblAridno.Text = s.AridNo;
                lblPhone.Text = s.Phone;
                lblEmail.Text = s.Email;
                lblDep.Text = s.Department;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SocietyMember sm = (SocietyMember)comboBox2.SelectedItem;
            if (sm != null)
            {
                SocietyMemberViewModel.approveMember(sm.MemberID, sm.SocietyID);
                LoadMembers(sm.SocietyID);
                LoadRequestMembers(sm.SocietyID);
                searchList = SocietyMemberViewModel.GetBySociety(sm.SocietyID);
                MessageBox.Show("Member approved.");
                NotificationService.NotifyJoinApproved(sm.AridNo, SocietyMemberViewModel.GetPresident(sm.SocietyID).AridNo,sm.SocietyID);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                ClearMemberDetails();
                return;
            }

            var matchedMember = searchList.FirstOrDefault(m =>
                m.AridNo.EndsWith(searchText, StringComparison.OrdinalIgnoreCase) ||
                StudentViewModel.GetByAridNo(m.AridNo).Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
            );

            if (matchedMember != null)
            {
                Student student = StudentViewModel.GetByAridNo(matchedMember.AridNo);
                lblnam2.Text = student.Name;
                lblArid2.Text = student.AridNo;
                lblEmail2.Text = student.Email;
                lblphone2.Text = student.Phone;
                lblDep2.Text = student.Department;
                textBox2.Text = matchedMember.Role;
                selectedMember = matchedMember;
            }
            else
            {
                ClearMemberDetails();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedMember != null && comboBox3.SelectedIndex != -1)
            {
                selectedMember.Role = comboBox3.SelectedItem.ToString();
                bool updated = SocietyMemberViewModel.UpdateMemberRoleOrSociety(
                    selectedMember.MemberID,
                    selectedMember.Role,
                    selectedMember.SocietyID
                );

                if (updated)
                {
                    MessageBox.Show("Role updated successfully.");
                    NotificationService.NotifyRoleChange(selectedMember.AridNo, comboBox3.SelectedItem.ToString(), SocietyMemberViewModel.GetPresident(selectedMember.SocietyID).AridNo, selectedMember.SocietyID);
                    LoadMembers(selectedMember.SocietyID);
                    HighlightUpdatedRow(selectedMember.AridNo);
                    textBox1_TextChanged(textBox1, EventArgs.Empty); // Refresh UI
                }
                else
                {
                    MessageBox.Show("Update failed.");
                }
            }
            else
            {
                MessageBox.Show("No member selected or role is empty.");
            }
        }

        private void HighlightUpdatedRow(string aridNo)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["AridNo"].Value.ToString() == aridNo)
                {
                    row.Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }

        private void ClearMemberDetails()
        {
            lblnam2.Text = lblArid2.Text = lblEmail2.Text = lblphone2.Text = lblDep2.Text = "---";
            textBox2.Clear();
            comboBox3.SelectedIndex = -1;
            selectedMember = null;
        }

        private void cmbTeamSociety_SelectedIndexChanged(object sender, EventArgs e)
        {
            Society society = (Society)cmbTeamSociety.SelectedItem;
            if (society != null) {
                LoadTeams(society.SocietyID);
                loadComboBoxteams(society.SocietyID);
                LoadMembers(society.SocietyID);
                //    DisplaySingleTeam(society.SocietyID);

            }


        }

        private void btnaddTeam_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtTeamName.Text))
            {
                MessageBoxHelper.ShowError("Please Enter Team Name")
          ; return;
            }
            else
            {
                Society s = (Society)cmbTeamSociety.SelectedItem;


                bool res = SocietyTeamViewModel.AddTeam(new SocietyTeam
                {
                    SocietyID = s.SocietyID,
                    TeamName = txtTeamName.Text,
                    Description = txtTeamDes.Text,
                });
                if (res) { MessageBoxHelper.ShowInfo("Team Cretaed"); }
                else { MessageBoxHelper.ShowError("Something went wrong"); }


            }
        }

        private void cmbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTeam.SelectedItem is SocietyTeam selectedTeam)
            {
                DisplaySingleTeam(selectedTeam.TeamID);
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            SocietyMember sm = (SocietyMember)cmbMember.SelectedItem;
            SocietyTeam st = (SocietyTeam)cmbTeam.SelectedItem;
            bool res = SocietyTeamMembershipViewModel.AddMemberToSocietyTeam(

                  new SocietyTeamMembership
                  {
                      Role = comboBox4.SelectedItem.ToString(),
                      TeamID = st.TeamID,
                      MemberID = sm.MemberID,
                      JoiningDate = dateTimePicker1.Value,

                  });

            if (res)
            {
                MessageBoxHelper.ShowInfo("Member Added To team");
                NotificationService.NotifyMemberAddedToTeam(sm.AridNo, sm.SocietyID, s.AridNo, st.TeamName);
            }
            else
            {
                MessageBoxHelper.ShowInfo("Something Went Wrong");
            }
            DisplaySingleTeam(st.TeamID);
        }
        private void DisplaySingleTeam(int teamId)
        {
            flowLayoutPanel2.Controls.Clear();
            var team = SocietyTeamViewModel.GetTeamById(teamId);
            if (team == null) return;

            // GroupBox to represent the team

            groupBox3.Text = team.TeamName;

            // Team Description Label

            label9.Text = $"Description: {team.Description}";


            var members = SocietyTeamMembershipViewModel.GetMembersByTeam(team.TeamID);

            foreach (var member in members)
            {
                SocietyMember sm = SocietyMemberViewModel.GetByMemberId(int.Parse(member.MemberID.ToString()))[0];
                Student s = StudentViewModel.GetByAridNo(sm.AridNo);

                Label l = new Label
                {
                    AutoSize = false,
                    Size = new Size(width: 350, height: 30),


                    Text = $"{sm.AridNo} - {s.Name} ({member.Role})\n"
                };
                flowLayoutPanel2.Controls.Add(l);
            }
        }
        // Add 


        private void cmbMember_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox5.SelectedItem != null)
            {
                AddEvent ev = new AddEvent((Society)comboBox5.SelectedItem,
             s);

                ev.Show();
            }
            else
            {
                MessageBoxHelper.ShowError("Select Society From Drop Down");
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUpcomingEvents();

        }

        public void LoadUpcomingEvents()
        {
            var s = (Society)comboBox5.SelectedItem;
            if (comboBox5.SelectedItem != null)
            {
                var data = EventViewModel.GetUpcomingEventsBySociety(s.SocietyID).Select(x => new
                {
                    EventId = x.EventID,
                    Title = x.Title,
                    Description = x.Description,
                    Venue = x.Venue,
                    EventDate = x.EventDate,
                    EventApprovalDate = x.EventApprovalDate,
                    FeeAmount = x.FeeAmount,
                    Name = SocietyViewModel.GetById(x.SocietyID.ToString()).Name,
                    File = EventFilesViewModel.GetEventFilesByEventId(x.EventID)
             .FirstOrDefault()?.FileName

                }).ToList();

                dataGridView2.DataSource = data;
                AddInstructionButtonColumn();

            }
        }


        public void LoadUnApprovedEvents()
        {
            var s = (Society)comboBox5.SelectedItem;
            if (comboBox5.SelectedItem != null)
            {
                var data = EventViewModel.GetUnPendingEventsBySociety(s.SocietyID).Select(x => new
                {
                    EventId = x.EventID,
                    Title = x.Title,
                    Description = x.Description,
                    Venue = x.Venue,
                    EventDate = x.EventDate,
                    EventApprovalDate = x.EventApprovalDate,
                    FeeAmount = x.FeeAmount,
                    Name = SocietyViewModel.GetById(x.SocietyID.ToString()).Name,
                    File = EventFilesViewModel.GetEventFilesByEventId(x.EventID)
             .FirstOrDefault()?.FileName

                }).ToList();
                dataGridView2.DataSource = data;
                AddInstructionButtonColumn();

            }
        }

        public void LoadAllEvents()
        {
            var s = (Society)comboBox5.SelectedItem;
            if (comboBox5.SelectedItem != null)
            {
                var data = EventViewModel.GetEventsBySociety(s.SocietyID).Select(x => new
                {   EventId = x.EventID,
                    Title = x.Title,
                    Description = x.Description,
                    Venue = x.Venue,
                    EventDate = x.EventDate,
                    EventApprovalDate = x.EventApprovalDate,
                    FeeAmount = x.FeeAmount,
                    Name = SocietyViewModel.GetById(x.SocietyID.ToString()).Name,
                    File = EventFilesViewModel.GetEventFilesByEventId(x.EventID)
             .FirstOrDefault()?.FileName
                }).ToList();
                dataGridView2.DataSource = data;
                AddInstructionButtonColumn();

            }
        }

        private void AddInstructionButtonColumn()
        {
            // Check if button already exists
            if (!dataGridView2.Columns.Contains("btnInstruction"))
            {
                DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
                btnCol.HeaderText = "File";
                btnCol.Name = "btnInstruction";
                btnCol.Text = "View";
                btnCol.UseColumnTextForButtonValue = true;
                dataGridView2.Columns.Add(btnCol);
            }
            if (!dataGridView2.Columns.Contains("btnUpload"))
            {
                DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
                btnCol.HeaderText = "UploadFile";
                btnCol.Name = "btnUpload";
                btnCol.Text = "Upload";
                btnCol.UseColumnTextForButtonValue = true;
                dataGridView2.Columns.Add(btnCol);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadAllEvents();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadUpcomingEvents();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadUnApprovedEvents();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView2.Columns[e.ColumnIndex].Name == "btnInstruction")
                {
                    // You can store file path (or file ID) in hidden column or data object
                    var row = dataGridView2.Rows[e.RowIndex];
                    var eventTitle = row.Cells["Title"].Value.ToString();

                    // Example: Get event by title and open file (or use ID in real case)
                    var eventObj = EventViewModel.GetEventByTitle(eventTitle); // replace this with proper logic (like event ID)

                    // Get instruction file path from DB
                    var instructionFile = EventFilesViewModel.GetEventFilesByEventId(eventObj.EventID)
                        .FirstOrDefault();

                    if (instructionFile != null && System.IO.File.Exists(instructionFile.FilePath))
                    {
                        System.Diagnostics.Process.Start(instructionFile.FilePath); // open file
                    }
                    else
                    {
                        MessageBox.Show("File not found.");
                    }
                }
            }
            if (dataGridView2.Columns[e.ColumnIndex].Name == "btnUpload")
            {
                // Step 1: Get EventID from selected row
                int eventId = Convert.ToInt32(dataGridView2.Rows[e.RowIndex]. Cells["EventID"].Value);
                //int eventID = 0;
                // Step 2: Open file dialog
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "All Files|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string originalPath = ofd.FileName;
                    string fileName = Path.GetFileName(originalPath);
                    string type = Path.GetExtension(originalPath);

                    // Target folder
                    string targetDir = Path.Combine("E://SocietyProject", "EventFiles");
                    Directory.CreateDirectory(targetDir);

                    // Full file path
                    string path = Path.Combine(targetDir, fileName);
                    File.Copy(originalPath, path, overwrite: true);

                    // Step 3: Insert file into DB
                    EventFilesViewModel.AddEventFile(new EventFile
                    {
                        EventId = eventId,
                        FileName = fileName,
                        FilePath = path,
                        FileType = type
                    });

                    MessageBox.Show("File uploaded and linked to event successfully.");
                }
            }



        }
    }
}
