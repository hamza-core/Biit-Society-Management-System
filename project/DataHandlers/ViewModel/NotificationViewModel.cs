using project.Forms.UserCotrols;
using project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.DataHandlers.ViewModel
{
    internal class NotificationViewModel
    {
        public static bool AddNotification(Notification n)
        {

            string query = "INSERT INTO Notifications (Title, Message, RecipientStudentID, SocietyID,SenderId, EventId, CreatedAt, isDeleted) " +
                           "VALUES (@Title, @Message, @RecipientAridNo, @SocietyID, @senderId, @EventId, @CreatedAt, 0)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@Title", n.Title},
                {"@Message",n.Message},
                {"@RecipientAridNo", n.RecipientAridNo},
                {"@SocietyID", n.SocietyID},
                {"@senderId", n.SenderId},
                {"@EventId", n.EventId},
                {"@CreatedAt", n.CreatedAt },
        
            };

            return DatabaseHelper.ExecuteInsert(query, parameters) > 0;
        }
        public static void NotifyAllStudents(Notification n)
        {
            // 1. Get all students
            string getStudentsQuery = "SELECT AridNo FROM Student";
            List<DataRow> studentsTable = DatabaseHelper.ExecuteSelect(getStudentsQuery ,null);

            // 2. Loop through students and insert notification for each
            foreach (DataRow row in studentsTable)
            {
                string studentAridNo = row["AridNo"].ToString();

                string insertQuery = "INSERT INTO Notifications " +
                                     "(Title, Message, RecipientStudentID, SocietyID, SenderId, EventId, CreatedAt, IsDeleted) " +
                                     "VALUES (@Title, @Message, @RecipientAridNo, @SocietyID, @SenderId, @EventId, @CreatedAt, 0)";

                Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            {"@Title", n.Title},
            {"@Message", n.Message},
            {"@RecipientAridNo", studentAridNo},
            {"@SocietyID", n.SocietyID},
            {"@SenderId", n.SenderId},
            {"@EventId", n.EventId},
            {"@CreatedAt", DateTime.Now}
        };

                DatabaseHelper.ExecuteInsert(insertQuery, parameters);
            }
        }

        public static List<Notification> GetAllNotifications(string aridNo)
        {
            string query = "SELECT * FROM Notifications WHERE RecipientStudentID = @RecipientAridNo AND isDeleted = 0";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@RecipientAridNo", aridNo}
            };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            List<Notification> notifications = new List<Notification>();

            foreach (DataRow row in dt)
            {
                notifications.Add(new Notification
                {
                    NotificationID = Convert.ToInt32(row["NotificationID"]),
                    Title = row["Title"].ToString(),
                    Message = row["Message"].ToString(),
                    RecipientAridNo = row["RecipientStudentID"].ToString(),
                     SenderId = row["SenderId"].ToString(),
                    EventId = row["EventId"].ToString(),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    IsRead = Convert.ToBoolean(row["IsRead"]),
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    IsDeleted = Convert.ToBoolean(row["isDeleted"])
                });
            }

            return notifications;
        }

        public static List<Notification> GetAllUnreadNotification() {

            string query = "SELECT * FROM Notifications WHERE IsRead = 0 AND isDeleted = 0";
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, null);
            List<Notification> notifications = new List<Notification>();

            foreach (DataRow row in dt)
            {
                notifications.Add(new Notification
                {
                    NotificationID = Convert.ToInt32(row["NotificationID"]),
                    Title = row["Title"].ToString(),
                    Message = row["Message"].ToString(),
                    RecipientAridNo = row["RecipientStudentID"].ToString(),
                    SenderId = row["SenderId"].ToString(),
                    EventId = row["EventId"].ToString(),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    IsRead = Convert.ToBoolean(row["IsRead"]),
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    IsDeleted = Convert.ToBoolean(row["isDeleted"])
                });
            }

            return notifications;
        }
        public static bool MarkAsRead(int notificationId)
        {
            string query = "UPDATE Notifications SET IsRead = 1 WHERE NotificationID = @NotificationID";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@NotificationID", notificationId}
            };

            return DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
        }
        public static bool DeleteNotification(int notificationId)
        {
            string query = "UPDATE Notifications SET isDeleted = 1 WHERE NotificationID = @NotificationID";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@NotificationID", notificationId}
            };

            return DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
        }
        public static List<Notification> GetNotificationsBySociety(int societyId)
        {
            string query = "SELECT * FROM Notifications WHERE SocietyID = @SocietyID AND isDeleted = 0";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@SocietyID", societyId}
            };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            List<Notification> notifications = new List<Notification>();

            foreach (DataRow row in dt)
            {
                notifications.Add(new Notification
                {
                    NotificationID = Convert.ToInt32(row["NotificationID"]),
                    Title = row["Title"].ToString(),
                    Message = row["Message"].ToString(),
                    RecipientAridNo = row["RecipientStudentID"].ToString(),
                    SenderId = row["SenderId"].ToString(),
                    EventId = row["EventId"].ToString(),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    IsRead = Convert.ToBoolean(row["IsRead"]),
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    IsDeleted = Convert.ToBoolean(row["isDeleted"])
                });
            }

            return notifications;
        }
        public static List<Notification> GetNotificationsByEvent(string eventId)
        {
            string query = "SELECT * FROM Notifications WHERE EventId = @EventId AND isDeleted = 0";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@EventId", eventId}
            };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            List<Notification> notifications = new List<Notification>();

            foreach (DataRow row in dt)
            {
                notifications.Add(new Notification
                {
                    NotificationID = Convert.ToInt32(row["NotificationID"]),
                    Title = row["Title"].ToString(),
                    Message = row["Message"].ToString(),
                    RecipientAridNo = row["RecipientStudentID"].ToString(),
                    SenderId = row["SenderId"].ToString(),
                    EventId = row["EventId"].ToString(),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    IsRead = Convert.ToBoolean(row["IsRead"]),
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    IsDeleted = Convert.ToBoolean(row["isDeleted"])
                });
            }

            return notifications;
        }
        public static Notification GetNotificationById(int notificationId)
        {
            string query = "SELECT * FROM Notifications WHERE NotificationID = @NotificationID AND isDeleted = 0";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@NotificationID", notificationId}
            };

            DataRow row = DatabaseHelper.ExecuteSelectSingle(query, parameters);
            if (row == null) return null;

            return new Notification
            {
                NotificationID = Convert.ToInt32(row["NotificationID"]),
                Title = row["Title"].ToString(),
                Message = row["Message"].ToString(),
                RecipientAridNo = row["RecipientStudentID"].ToString(),
                SenderId = row["SenderId"].ToString(),
                EventId = row["EventId"].ToString(),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                IsRead = Convert.ToBoolean(row["IsRead"]),
                SocietyID = Convert.ToInt32(row["SocietyID"]),
                IsDeleted = Convert.ToBoolean(row["isDeleted"])
            };
        }
        public static List<Notification> GetUnreadNotifications(string aridNo)
        {
            string query = "SELECT * FROM Notifications WHERE RecipientStudentID = @RecipientAridNo AND IsRead = 0 AND isDeleted = 0";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@RecipientAridNo", aridNo}
            };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            List<Notification> notifications = new List<Notification>();

            foreach (DataRow row in dt)
            {
                notifications.Add(new Notification
                {
                    NotificationID = Convert.ToInt32(row["NotificationID"]),
                    Title = row["Title"].ToString(),
                    Message = row["Message"].ToString(),
                    RecipientAridNo = row["RecipientStudentID"].ToString(),
                    SenderId = row["SenderId"].ToString(),
                        EventId = row["EventId"].ToString(),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    IsRead = Convert.ToBoolean(row["IsRead"]),
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    IsDeleted = Convert.ToBoolean(row["isDeleted"])
                });
            }

            return notifications;
        }


    }
}
    