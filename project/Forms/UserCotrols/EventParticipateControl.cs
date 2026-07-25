using project.DataHandlers;
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
    public partial class EventParticipateControl : UserControl
    {
        public EventParticipateControl()
        {
            InitializeComponent();
        }
        
        Student currentStudent;
        Event selectedEvent;
        List<FormField> dynamicFields;

        public EventParticipateControl(Student student)
        {
            InitializeComponent();
            currentStudent = student;
        }

        private void EventParticipateControl_Load(object sender, EventArgs e)
        {
            cmbSociety.DataSource = SocietyViewModel.GetSocietiesJoinedByStudent(currentStudent.AridNo);
            cmbSociety.DisplayMember = "Name";
            cmbSociety.ValueMember = "SocietyID";
        }

        private void cmbSociety_SelectedIndexChanged(object sender, EventArgs e)
        {
            Society selected = (Society)cmbSociety.SelectedItem;
            if (selected != null)
            {
                cmbEvent.DataSource = EventViewModel.GetUpcomingEventsBySociety(selected.SocietyID);
                cmbEvent.DisplayMember = "Title";
                cmbEvent.ValueMember = "EventID";
            }
        }

        private void cmbEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedEvent = (Event)cmbEvent.SelectedItem;

            if (selectedEvent != null)
            {
                LoadTeamDropdown(selectedEvent.EventID);
                btnParticipate.Enabled = true;
            }
            else
            {
                btnParticipate.Enabled = false;
            }
        }

        private void LoadTeamDropdown(int eventId)
        {
            List<Team> teams = TeamViewModel.GetTeamsByEvent(eventId);
            var membershipViewModel = new TeamMembershipViewModel();
            
            List<Team> myTeams = teams.Where(t =>
                membershipViewModel.GetMembersByTeam(t.TeamID)
                .Any(m => m.MemberAridNo == currentStudent.AridNo)).ToList();

            comboBox1.DataSource = teams;
            comboBox1.DisplayMember = "TeamName";
            comboBox1.ValueMember = "TeamID";
        }

        private void LoadDynamicForm(string json)
        {
            tableDynamicForm.Controls.Clear();
            tableDynamicForm.RowCount = 0;

            if (string.IsNullOrEmpty(json)) return;

            dynamicFields = DeserializeFormFields(json);

            int row = 0;

            foreach (var field in dynamicFields)
            {
                Label label = new Label
                {
                    Text = field.FieldName,
                    AutoSize = true,
                    Anchor = AnchorStyles.Right,
                    TextAlign = ContentAlignment.MiddleRight,
                    Margin = new Padding(3, 6, 3, 6),
                    Padding = new Padding(left: 0, right: 20, top: 0, bottom: 0),
                };

                Control control = null;

                switch (field.FieldType.ToLower())
                {
                    case "textbox":
                        control = new TextBox
                        {
                            Name = "txt_" + field.FieldName,
                            Width = 200,
                            Anchor = AnchorStyles.Left
                        };
                        break;

                    case "combobox":
                        ComboBox cmb = new ComboBox
                        {
                            Name = "cmb_" + field.FieldName,
                            Width = 200,
                            DropDownStyle = ComboBoxStyle.DropDownList,
                            Anchor = AnchorStyles.Left
                        };
                        cmb.Items.AddRange(field.FieldOptions.Split(','));
                        control = cmb;
                        break;
                        
                    case "date":
                        control = new DateTimePicker
                        {
                            Name = "dt_" + field.FieldName,
                            Width = 200,
                            Format = DateTimePickerFormat.Short,
                            Anchor = AnchorStyles.Left
                        };
                        break;
                        
                    case "checkbox":
                        control = new CheckBox
                        {
                            Name = "chk_" + field.FieldName,
                            Text = field.FieldName,
                            Anchor = AnchorStyles.Left
                        };
                        break;
                        
                    case "radiobutton":
                        FlowLayoutPanel panel = new FlowLayoutPanel
                        {
                            Name = "rad_" + field.FieldName,
                            FlowDirection = FlowDirection.LeftToRight,
                            AutoSize = true,
                            WrapContents = true
                        };

                        foreach (var option in field.FieldOptions.Split(','))
                        {
                            RadioButton rb = new RadioButton
                            {
                                Text = option.Trim(),
                                Name = "rb_" + option.Trim(),
                                AutoSize = true
                            };
                            panel.Controls.Add(rb);
                        }

                        control = panel;
                        break;
                }

                if (control != null)
                {
                    tableDynamicForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    tableDynamicForm.Controls.Add(label, 0, row);
                    tableDynamicForm.Controls.Add(control, 1, row);
                    row++;
                }
            }

            // Add submit button in last row
            Button btn = new Button
            {
                Text = "Submit",
                Width = 100,
                Height = 30,
                Name = "btnSubmit",
                Anchor = AnchorStyles.Left
            };
            btn.Click += btn_Click;

            tableDynamicForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableDynamicForm.Controls.Add(btn, 1, row);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (selectedEvent == null) return;

            Dictionary<string, string> formData = new Dictionary<string, string>();

            // 1. Validate and collect all dynamic inputs
            foreach (var field in dynamicFields)
            {
                string key = field.FieldName;
                string value = "";

                var control = tableDynamicForm.Controls.Find("txt_" + key, true).FirstOrDefault() ??
                              tableDynamicForm.Controls.Find("cmb_" + key, true).FirstOrDefault() ??
                              tableDynamicForm.Controls.Find("dt_" + key, true).FirstOrDefault() ??
                              tableDynamicForm.Controls.Find("chk_" + key, true).FirstOrDefault() ??
                              tableDynamicForm.Controls.Find("rad_" + key, true).FirstOrDefault();

                if (control is TextBox txt)
                    value = txt.Text;
                else if (control is ComboBox cmb)
                    value = cmb.SelectedItem?.ToString();
                else if (control is DateTimePicker dt)
                    value = dt.Value.ToShortDateString();
                else if (control is CheckBox cb)
                    value = cb.Checked.ToString();
                else if (control is FlowLayoutPanel panel)
                {
                    var selectedRadio = panel.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                    value = selectedRadio?.Text ?? "";
                }

                if (field.IsRequired && string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show($"Please fill required field: {key}");
                    return;
                }

                formData[key] = value;
            }

            // 2. Serialize form data
            string jsonData = SerializeFormData(formData);

            // 3. Check if this is a team event
            if (selectedEvent.TeamRequired != "None")
            {
                var selectedTeam = (Team)comboBox1.SelectedItem;
                if (selectedTeam == null)
                {
                    MessageBox.Show("Please select a team.");
                    return;
                }

                // Validate that current student is the leader of that team
                var teamMembers = new TeamMembershipViewModel().GetMembersByTeam(selectedTeam.TeamID);
                var leader = teamMembers.FirstOrDefault(m => m.Role.ToLower() == "leader");

                if (leader == null || leader.MemberAridNo != currentStudent.AridNo)
                {
                    MessageBox.Show("Only the team leader can submit participation.");
                    return;
                }

                // Insert participation (only once for the team leader)
                EventParticipationViewModel.AddParticipation(new EventParticipation
                {
                    EventID = selectedEvent.EventID,
                    AridNo = currentStudent.AridNo,
                    Role = "Leader",
                    FeePaid = false,
                    PaymentDate = DateTime.Now,
                    IsDeleted = false,
                    AdditionalData = jsonData
                });

                MessageBox.Show("Team participation recorded.");
            }
            else
            {
                // 4. Individual Participation
                EventParticipationViewModel.AddParticipation(new EventParticipation
                {
                    EventID = selectedEvent.EventID,
                    AridNo = currentStudent.AridNo,
                    Role = "Participant",
                    FeePaid = false,
                    PaymentDate = DateTime.Now,
                    IsDeleted = false,
                    AdditionalData = jsonData
                });

                MessageBox.Show("Individual participation recorded.");
            }
        }

        private void btnParticipate_Click(object sender, EventArgs e)
        {
            if (selectedEvent == null)
            {
                MessageBox.Show("Please select an event.");
                return;
            }

            // Team Required
            if (selectedEvent.TeamRequired != "None")
            {
                var selectedTeam = (Team)comboBox1.SelectedItem;

                if (selectedTeam == null)
                {
                    MessageBox.Show("You are not part of any team for this event.");
                    return;
                }

                var teamMembers = new TeamMembershipViewModel().GetMembersByTeam(selectedTeam.TeamID);
                var leader = teamMembers.FirstOrDefault(m => m.Role.ToLower() == "leader");

                // Not leader
                if (leader == null || leader.MemberAridNo != currentStudent.AridNo)
                {
                    ShowTeamDetailsInGroupBox(selectedTeam, teamMembers);
                    MessageBox.Show("Only the team leader can submit participation.");
                    return;
                }

                // Is leader, show form
                ShowTeamDetailsInGroupBox(selectedTeam, teamMembers);
                MessageBoxHelper.ShowInfo("You are the leader. Please fill the form to participate.");
                
                // Serialize FormStructure list to JSON before passing
                string formJson = SerializeFormStructure(selectedEvent.FormStructure);
                LoadDynamicForm(formJson);
            }
            else
            {
                // Individual participation
                MessageBoxHelper.ShowInfo("Please fill the form to participate.");
                
                // Serialize FormStructure list to JSON before passing
                string formJson = SerializeFormStructure(selectedEvent.FormStructure);
                LoadDynamicForm(formJson);
            }
        }

        private void ShowTeamDetailsInGroupBox(Team team, List<TeamMembership> members)
        {
            groupBox1.Text = $"Team: {team.TeamName}";
            flowLayoutPanel1.Controls.Clear();

            foreach (var member in members)
            {
                var student = StudentViewModel.GetByAridNo(member.MemberAridNo);
                if (student == null) continue;

                Label lbl = new Label
                {
                    Text = $"{student.Name} ({student.AridNo}) - {member.Role}",
                    Width = 280,
                    Height = 25,
                    Padding = new Padding(4),
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(3)
                };

                flowLayoutPanel1.Controls.Add(lbl);
            }

            groupBox1.Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedTeam = (Team)comboBox1.SelectedItem;
            if (selectedTeam == null) return;
            
            var teamMembers = new TeamMembershipViewModel().GetMembersByTeam(selectedTeam.TeamID);
            ShowTeamDetailsInGroupBox(selectedTeam, teamMembers);
        }

        private List<FormField> DeserializeFormFields(string json)
        {
            var fields = new List<FormField>();
            if (string.IsNullOrEmpty(json)) return fields;

            json = json.Trim('[', ']');
            if (string.IsNullOrEmpty(json)) return fields;

            var objects = json.Split(new[] { "},{" }, StringSplitOptions.None);
            foreach (var obj in objects)
            {
                var field = new FormField();
                if (obj.Contains("\"FieldName\""))
                {
                    var start = obj.IndexOf("\"FieldName\":\"") + 13;
                    var end = obj.IndexOf("\"", start);
                    if (start > 12 && end > start)
                        field.FieldName = obj.Substring(start, end - start);
                }
                if (obj.Contains("\"FieldType\""))
                {
                    var start = obj.IndexOf("\"FieldType\":\"") + 13;
                    var end = obj.IndexOf("\"", start);
                    if (start > 12 && end > start)
                        field.FieldType = obj.Substring(start, end - start);
                }
                if (obj.Contains("\"IsRequired\""))
                {
                    field.IsRequired = obj.Contains("\"IsRequired\":true");
                }
                if (obj.Contains("\"Label\""))
                {
                    var start = obj.IndexOf("\"Label\":\"") + 9;
                    var end = obj.IndexOf("\"", start);
                    if (start > 8 && end > start)
                        field.Label = obj.Substring(start, end - start);
                }
                if (obj.Contains("\"FieldOptions\""))
                {
                    var start = obj.IndexOf("\"FieldOptions\":\"") + 16;
                    var end = obj.IndexOf("\"", start);
                    if (start > 15 && end > start)
                        field.FieldOptions = obj.Substring(start, end - start);
                }
                fields.Add(field);
            }
            return fields;
        }

        private string SerializeFormStructure(List<FormField> formStructure)
        {
            if (formStructure == null || formStructure.Count == 0)
                return "[]";

            var sb = new StringBuilder();
            sb.Append("[");
            bool first = true;
            foreach (var field in formStructure)
            {
                if (!first) sb.Append(",");
                sb.Append("{");
                sb.Append($"\"FieldName\":\"{field.FieldName}\"");
                sb.Append($",\"FieldType\":\"{field.FieldType}\"");
                if (!string.IsNullOrEmpty(field.FieldOptions))
                    sb.Append($",\"FieldOptions\":\"{field.FieldOptions}\"");
                sb.Append($",\"IsRequired\":{(field.IsRequired ? "true" : "false")}");
                if (!string.IsNullOrEmpty(field.Label))
                    sb.Append($",\"Label\":\"{field.Label}\"");
                sb.Append("}");
                first = false;
            }
            sb.Append("]");
            return sb.ToString();
        }

        private string SerializeFormData(Dictionary<string, string> formData)
        {
            var sb = new StringBuilder();
            sb.Append("{");
            bool first = true;
            foreach (var kvp in formData)
            {
                if (!first) sb.Append(",");
                sb.Append($"\"{kvp.Key}\":\"{kvp.Value}\"");
                first = false;
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}
