using project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.DataHandlers.ViewModel
{
    public class EventImageViewModel
    {

        public static bool AddEventImage(EventImage eventImage)
        {

            string query = @"INSERT INTO EventImages (EventId, ImageId, ImagePath,ImageName)
                             VALUES (@EventId, @ImageId, @ImagePath,@ImageName)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@EventId", eventImage.EventId },
                { "@ImageId", eventImage.ImageId },
                { "@ImagePath", eventImage.ImagePath },
                { "@ImageName", eventImage.ImageName }
            };

            return DatabaseHelper.ExecuteInsert(query, parameters) > 0;

        }

        public static EventImage GetEventImageById(int eventId)
        {
            string query = "SELECT * FROM EventImages WHERE EventId = @EventId";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@EventId", eventId }
            };

            DataRow row = DatabaseHelper.ExecuteSelectSingle(query, parameters);
            if (row == null) return null;

            return new EventImage
            {
                EventId = Convert.ToInt32(row["EventId"]),
                ImageId = Convert.ToInt32(row["ImageId"]),
                ImagePath = row["ImagePath"].ToString(),
                ImageName = row["ImageName"].ToString()
            };
        }
        public static  List<EventImage> GetAllImagesOfEvent(int EventId )
        {
            string query = "SELECT * FROM EventImages WHERE EventId = @EventId";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@EventId", EventId }
            };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);

            List<EventImage> eventImages = new List<EventImage>();
            foreach (DataRow row in dt)
            {
                eventImages.Add(new EventImage
                {
                    EventId = Convert.ToInt32(row["EventId"]),
                    ImageId = Convert.ToInt32(row["ImageId"]),
                    ImagePath = row["ImagePath"].ToString(),
                    ImageName = row["ImageName"].ToString()
                });
            }

            return eventImages;
        
        }

        public static bool DeleteEventImage(int eventId, int imageId)
        {
            string query = "DELETE FROM EventImages WHERE EventId = @EventId AND ImageId = @ImageId";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@EventId", eventId },
                { "@ImageId", imageId }
            };

            return DatabaseHelper.ExecuteDelete(query, parameters) > 0;
        }

        public static void UpdateEventImage(EventImage eventImage)
        {
            string query = @"UPDATE EventImages 
                             SET ImagePath = @ImagePath, ImageName = @ImageName 
                             WHERE EventId = @EventId AND ImageId = @ImageId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@EventId", eventImage.EventId },
                { "@ImageId", eventImage.ImageId },
                { "@ImagePath", eventImage.ImagePath },
                { "@ImageName", eventImage.ImageName }
            };

            DatabaseHelper.ExecuteUpdate(query, parameters);
        }       

        public static List<EventImage> GetAllEventImages()
        {
            string query = "SELECT * FROM EventImages";
          List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, null);

            List<EventImage> eventImages = new List<EventImage>();
            foreach (DataRow row in dt)
            {
                eventImages.Add(new EventImage
                {
                    EventId = Convert.ToInt32(row["EventId"]),
                    ImageId = Convert.ToInt32(row["ImageId"]),
                    ImagePath = row["ImagePath"].ToString(),
                    ImageName = row["ImageName"].ToString()
                });
            }

            return eventImages;
        }   

        public static List<EventImage> GetEventImagesByName(String imageName)
        {

            string query = "SELECT * FROM EventImages WHERE ImageName LIKE @ImageName";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@ImageName", "%" + imageName + "%" }
            };

           List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);

            List<EventImage> eventImages = new List<EventImage>();
            foreach (DataRow row in dt)
            {
                eventImages.Add(new EventImage
                {
                    EventId = Convert.ToInt32(row["EventId"]),
                    ImageId = Convert.ToInt32(row["ImageId"]),
                    ImagePath = row["ImagePath"].ToString(),
                    ImageName = row["ImageName"].ToString()
                });
            }

            return eventImages;
        }

        

    }
}
