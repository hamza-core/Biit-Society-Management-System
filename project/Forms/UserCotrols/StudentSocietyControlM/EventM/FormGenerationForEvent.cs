using project.DataHandlers.ViewModel;
using project.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
namespace project.Forms.UserCotrols.StudentSocietyControlM.EventM
{
    public partial class FormGenerationForEvent : Form
    {
        // List to hold field options for ComboBox
        List<string> options = new List<string>();

        // List to hold all created fields
        List<FormField> fieldList = new List<FormField>();

        public FormGenerationForEvent()
        {
            InitializeComponent();

            listBox1.Enabled = false;
            lblOptions.Enabled = false;
            button3.Enabled = false;
            txtOptions.Enabled = false;

            
        }
        int eventId = -1; // This should be set to the actual EventID when creating the form
        public FormGenerationForEvent(Event e)
        {
            InitializeComponent();
            eventId = e.EventID;
            listBox1.Enabled = false;
            lblOptions.Enabled = false;
            button3.Enabled = false;
            txtOptions.Enabled = false;


        }

        private void FormGenerationForEvent_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e) // Add Field
        {
            if (string.IsNullOrWhiteSpace(txtFieldName.Text) || cmbFieldType.SelectedItem == null)
            {
                MessageBox.Show("Please enter a field name and select a field type.");
                return;
            }

            string fieldType = cmbFieldType.SelectedItem.ToString();
            string optionsString = "";

            if (fieldType == "ComboBox")
            {
                if (options.Count == 0)
                {
                    MessageBox.Show("Please add at least one option for the ComboBox.");
                    return;
                }
                optionsString = string.Join(",", options);
            }

            var field = new FormField
            {
                FieldName = txtFieldName.Text.Trim(),
                FieldType = fieldType,
                IsRequired = chkRequired.Checked,
                FieldOptions = optionsString
            };

            fieldList.Add(field);
            RefreshFieldPreview();

            txtFieldName.Clear();
            cmbFieldType.SelectedIndex = -1;
            txtOptions.Clear();
            chkRequired.Checked = false;
            listBox1.Items.Clear();
            options.Clear();

            listBox1.Enabled = false;
            lblOptions.Enabled = false;
            button3.Enabled = false;
            txtOptions.Enabled = false;
        }

        private void RefreshFieldPreview()
        {
            lstFields.Items.Clear();
            foreach (var f in fieldList)
            {
                string line = $"{f.FieldName} [{f.FieldType}] {(f.IsRequired ? "(Required)" : "")}";
                if (f.FieldType == "ComboBox")
                    line += $" | Options: {f.FieldOptions}";
                lstFields.Items.Add(line);
            }
        }

        private void cmbFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
           }

        private void button3_Click(object sender, EventArgs e) // Add Option
        {
            if (!string.IsNullOrWhiteSpace(txtOptions.Text))
            {
                string option = txtOptions.Text.Trim();
                options.Add(option);
                listBox1.Items.Add(option);
                txtOptions.Clear();
            }
        }

        private void cmbFieldType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            bool showOptions = cmbFieldType.SelectedItem?.ToString() == "ComboBox";

            listBox1.Enabled = showOptions;
            lblOptions.Enabled = showOptions;
            button3.Enabled = showOptions;
            txtOptions.Enabled = showOptions;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fieldList.Count == 0)
            {
                MessageBox.Show("No fields added to form.");
                return;
            }

            // Serialize form structure
            string json = JsonConvert.SerializeObject(fieldList,Newtonsoft.Json.Formatting.Indented);

            // Get the selected EventID (must come from UI like dropdown or tag)
            int selectedEventId = eventId; // Replace this with actual logic

            // Save to DB
            bool success = EventViewModel.UpdateEventFormStructure(selectedEventId, json);

            if (success)
            {
                MessageBox.Show("Form saved with event successfully!");
                this.Hide();
            }
            else
                MessageBox.Show("Failed to update event form.");
        

    }
    }
}
