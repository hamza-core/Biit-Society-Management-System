using project.DataHandlers;
using project.DataHandlers.ViewModel;
using project.Models;
using project.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project.Forms.StudentControlM
{
    public partial class UpcomingEventControl : UserControl
    {
        public UpcomingEventControl()
        {
            InitializeComponent();
        }
        Event ex;
        public UpcomingEventControl(Event e)
        {
            InitializeComponent();

            ex = e;
            Society society = SocietyViewModel.GetById(e.SocietyID.ToString());


            lblEventName.Text = e.Title;
            lblOrganizedBy.Text += society.Name;
            lblDate.Text += e.EventDate.ToString();
            lblAmount.Text += e.FeeAmount;
            lblDescription.Text += e.Description;
            lblVenue.Text += e.Venue;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<EventFile> files = EventFilesViewModel.GetEventFilesByEventId(ex.EventID)
             ; if (files.Count > 0)
            {

                if (files.Count == 1)
                {
                    Process.Start(new ProcessStartInfo(files[0].FilePath));
                }
                else
                {
                    MessageBoxHelper.ShowInfo("Multiple Files Exist\nOpening Files....");
                    foreach (EventFile file in files)
                    {
                        Process.Start(new ProcessStartInfo(file.FilePath));
                    }
                }
            }
            else
            {
                MessageBoxHelper.ShowInfo("No instruction Is Uploded for the Event");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog foldererDialog1 = new FolderBrowserDialog();
            List <EventFile> files = EventFilesViewModel.GetEventFilesByEventId(ex.EventID)
          ; if (files.Count > 0)
            {

                if (files.Count == 1)
                {
                    if (DialogResult.OK == foldererDialog1.ShowDialog())
                    {
                        string fileName = files[0].FileName;
                        string destinationPath =Path.Combine(foldererDialog1.SelectedPath, files[0].FileName );

                        File.Copy(files[0].FilePath, destinationPath);
                        MessageBoxHelper.ShowInfo("File Downloaded Successfully ");


                    }

                }
                else
                {
                    MessageBoxHelper.ShowInfo("Multiple File Exists ");
                    if (DialogResult.OK == foldererDialog1.ShowDialog())
                    {

                        foreach (var file in files)
                        {
                            string fileName = file.FileName;
                            string destinationPath = Path.Combine(foldererDialog1.SelectedPath, fileName);

                            File.Copy(destinationPath, file.FilePath);
                            MessageBoxHelper.ShowInfo("All Files Downloaded Successfully ");



                        }
                    }
                }

            }
            else
            {
                MessageBoxHelper.ShowError("No file Exist To Download")
            ;
            }
        }
    }
}
