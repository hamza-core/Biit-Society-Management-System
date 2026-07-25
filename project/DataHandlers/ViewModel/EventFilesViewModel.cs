using project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.DataHandlers.ViewModel
{
    public class EventFilesViewModel
    {
        public static void AddEventFile(EventFile eventFile)
        {
         
                string query = "INSERT INTO EventFiles (EventId, FileName, FilePath , FileType) VALUES (@EventId, @FileName, @FilePath,@FileType)";
            
            Dictionary<string ,object> parameters = new Dictionary<string, object>
            {
                { "@EventId", eventFile.EventId },
                { "@FileName", eventFile.FileName },
                { "@FilePath", eventFile.FilePath },
                { "@FileType", eventFile.FileType }
            };          
       
            DatabaseHelper.ExecuteInsert(query, parameters);
        }

        public static List<EventFile> GetEventFilesByEventId(int eventId)
        {
            string query = "SELECT * FROM EventFiles WHERE EventId = @EventId";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@EventId", eventId }
            };

            List<EventFile> eventFiles = new List<EventFile>();
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters)
            ;
                foreach(DataRow row in dt)
                {
                    EventFile eventFile = new EventFile
                    {
                        EventId = int.Parse( row["EventId"].ToString()),
                        FileName = row[1].ToString(),
                        FileType = row[2].ToString(),
                        FilePath = row[3].ToString()
                    };
                    eventFiles.Add(eventFile);
                }
            
            return eventFiles;
        }
        
        public static EventFile GetEventFileByName(string name)
        {
            string query = @"select * from EventFiles where FileName = @fileName";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@fileName",name },
            };

           List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            if (dt != null)
            {
                DataRow row = dt[0];

                return new EventFile
                {
                    EventId = int.Parse(row["EventId"].ToString()),
                    FileName = row[1].ToString(),
                    FilePath = row[2].ToString(),
                    FileType = row[3].ToString(),
                };
            }
            return null;

        }


    }
}
