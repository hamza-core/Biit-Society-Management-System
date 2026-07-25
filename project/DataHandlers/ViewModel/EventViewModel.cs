using project.Models;
using project.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace project.DataHandlers.ViewModel
{
    public class EventViewModel
    {
        public static bool AddEvent(Event ev)
        {
            string clashQuery = @"SELECT COUNT(*) FROM Event WHERE EventDate = @EventDate AND Venue = @Venue AND IsDeleted = 0";

            var clashParams = new Dictionary<string, object>
            {
                { "@EventDate", ev.EventDate },
                { "@Venue", ev.Venue }
            };

            List<DataRow> clashResult = DatabaseHelper.ExecuteSelect(clashQuery, clashParams);
            if (clashResult.Count > 0 && Convert.ToInt32(clashResult[0][0]) > 0)
                return false;

            string query = @"INSERT INTO Event 
            (Title, Description, SocietyID, EventDate, Venue, RequiresFee, FeeAmount, CreatedByStudentAridNo, 
             ApprovedByTeacherID, IsDeleted, EventApprovalStatus, EventApprovalDate)
            VALUES 
            (@Title, @Description, @SocietyID, @EventDate, @Venue, @RequiresFee, @FeeAmount, @CreatedByStudentAridNo, 
             @ApprovedByTeacherID, 0, 'Pending', @EventApprovalDate)";

            var parameters = new Dictionary<string, object>
            {
                { "@Title", ev.Title },
                { "@Description", ev.Description },
                { "@SocietyID", ev.SocietyID },
                { "@EventDate", ev.EventDate },
                { "@Venue", ev.Venue },
                { "@RequiresFee", ev.RequiresFee },
                { "@FeeAmount", ev.FeeAmount },
                { "@CreatedByStudentAridNo", ev.CreatedByStudentAridNo },
                { "@ApprovedByTeacherID", string.IsNullOrEmpty(ev.ApprovedByTeacherID) ? DBNull.Value : (object)ev.ApprovedByTeacherID },
                { "@EventApprovalDate", ev.EventApprovalDate == default ? DBNull.Value : (object)ev.EventApprovalDate }
            };

            return DatabaseHelper.ExecuteInsert(query, parameters) > 0;
        }

        public static List<Event> GetAllEvents()
        {
            string query = "SELECT * FROM Event WHERE IsDeleted = 0";
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query);
            return MapEventsFromDataTable(dt);
        }

        public static bool UpdateEventFormStructure(int eventId, string formJson)
        {
            string query = "UPDATE [Event] SET FormStructure = @FormJson WHERE EventID = @EventID";
            var parameters = new Dictionary<string, object>
            {
                { "@FormJson", formJson },
                { "@EventID", eventId }
            };
            return DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
        }

        public static int InsertEventAndGetId(Event ev)
        {
            string query = @"INSERT INTO [Event] 
                (Title, Description, SocietyID, EventDate, Venue, RequiresFee, FeeAmount, CreatedByStudentAridNo, 
                 IsDeleted, EventApprovalStatus, EventApprovalDate)
                VALUES 
                (@Title, @Description, @SocietyID, @EventDate, @Venue, @RequiresFee, @FeeAmount, @CreatedByStudentAridNo, 
                 0, 'Pending', @EventApprovalDate);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            var parameters = new Dictionary<string, object>
            {
                { "@Title", ev.Title },
                { "@Description", ev.Description },
                { "@SocietyID", ev.SocietyID },
                { "@EventDate", ev.EventDate },
                { "@Venue", ev.Venue },
                { "@RequiresFee", ev.RequiresFee },
                { "@FeeAmount", ev.FeeAmount },
                { "@CreatedByStudentAridNo", ev.CreatedByStudentAridNo },
                { "@EventApprovalDate", ev.EventApprovalDate == default ? DBNull.Value : (object)ev.EventApprovalDate }
            };

             SqlConnection conn = new SqlConnection(@"Data Source=DERIK\SQLEXPRESS01; Initial Catalog=project; Integrated Security=true");
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            foreach (var param in parameters)
                cmd.Parameters.AddWithValue(param.Key, param.Value);

            object result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : -1;
        }

        public static Event GetEventByTitle(string title)
        {
            string query = "SELECT * FROM Event WHERE Title = @Title AND IsDeleted = 0";
            var parameters = new Dictionary<string, object> { { "@Title", title } };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            if (dt.Count > 0)
            {
                DataRow row = dt[0];
                return new Event
                {
                    EventID = Convert.ToInt32(row["EventID"]),
                    Title = row["Title"].ToString(),
                    Description = row["Description"].ToString(),
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    EventDate = Convert.ToDateTime(row["EventDate"]),
                    Venue = row["Venue"].ToString(),
                    RequiresFee = Convert.ToBoolean(row["RequiresFee"]),
                    FeeAmount = row["FeeAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FeeAmount"]),
                    CreatedByStudentAridNo = row["CreatedByStudentAridNo"].ToString(),
                    ApprovedByTeacherID = row["ApprovedByTeacherID"] == DBNull.Value ? "" : row["ApprovedByTeacherID"].ToString(),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"]),
                    EventApprovalStatus = row["EventApprovalStatus"].ToString(),
                    EventApprovalDate = row["EventApprovalDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["EventApprovalDate"])
                    ,TeamRequired = row["TeamRequirement"] == DBNull.Value? "" : row["TeamRequirement"].ToString()
                };
            }
            return null;
        }

        public static List<Event> GetEventsByStatus(string status)
        {
            string query = @"SELECT * FROM Event 
                             WHERE EventApprovalStatus = @status AND IsDeleted = 0 
                             ORDER BY EventDate ASC";

            var parameters = new Dictionary<string, object> { { "@status", status } };
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            return MapEventsFromDataTable(dt);
        }

        public static List<Event> GetUpcomingEvents(string aridNo)
        {
            string query = @"
            SELECT e.*
            FROM Event e
            INNER JOIN SocietyMember sm ON e.SocietyID = sm.SocietyID
            WHERE sm.AridNo = @AridNo AND e.EventApprovalStatus = 'Approved'
            AND e.EventDate >= CAST(GETDATE() AS DATE)
            AND e.IsDeleted = 0 AND sm.IsDeleted = 0
            ORDER BY e.EventDate ASC;";

            var parameters = new Dictionary<string, object> { { "@AridNo", aridNo } };
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            return MapEventsFromDataTable(dt);
        }

        public static List<Event> GetUpcomingEvents()
        {
            string query = @"SELECT * FROM Event 
                             WHERE EventDate >= CAST(GETDATE() AS DATE) AND EventApprovalStatus = 'Approved' AND IsDeleted = 0 
                             ORDER BY EventDate ASC";

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query);
            return MapEventsFromDataTable(dt);
        }

        public static List<Event> GetUpcomingEventsBySociety(int societyId)
        {
            string query = @"SELECT * FROM Event 
                             WHERE SocietyID = @SocietyID AND EventDate >= CAST(GETDATE() AS DATE) 
                             AND EventApprovalStatus = 'Approved' AND IsDeleted = 0 ORDER BY EventDate DESC";

            var parameters = new Dictionary<string, object> { { "@SocietyID", societyId } };
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            return MapEventsFromDataTable(dt);
        }

        public static List<Event> GetUpcomingEventsBySocietyRequireTeam(int societyId)
        {
            string query = @"SELECT * FROM Event 
                             WHERE SocietyID = @SocietyID AND EventDate >= CAST(GETDATE() AS DATE) 
                             AND EventApprovalStatus = 'Approved'AND TeamRequirement in ('Optional','Required') AND IsDeleted = 0 ORDER BY EventDate DESC";

            var parameters = new Dictionary<string, object> { { "@SocietyID", societyId } };
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            return MapEventsFromDataTable   (dt);
        }

        public static List<Event> GetAllEventsBySociety(int societyId)
        {
            string query = @"SELECT * FROM Event 
                             WHERE SocietyID = @SocietyID AND IsDeleted = 0 ORDER BY EventDate DESC";

            var parameters = new Dictionary<string, object> { { "@SocietyID", societyId } };
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            return MapEventsFromDataTable(dt);
        }

        public static List<Event> GetEventsBySociety(int societyId)
        {
            string query = @"SELECT * FROM Event 
                             WHERE SocietyID = @SocietyID AND EventApprovalStatus = 'Approved' AND IsDeleted = 0 
                             ORDER BY EventDate DESC";

            var parameters = new Dictionary<string, object> { { "@SocietyID", societyId } };
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            return MapEventsFromDataTable(dt);
        }

        public static List<Event> GetUnPendingEventsBySociety(int societyId)
        {
            string query = @"SELECT * FROM Event 
                             WHERE SocietyID = @SocietyID AND EventApprovalStatus != 'Approved' AND IsDeleted = 0 
                             ORDER BY EventDate DESC";

            var parameters = new Dictionary<string, object> { { "@SocietyID", societyId } };
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            return MapEventsFromDataTable(dt);
        }

        public static bool ApproveEvent(int eventId, int teacherId, DateTime approvalDate)
        {
            string query = @"UPDATE Event 
                             SET EventApprovalStatus = 'Approved', 
                                 ApprovedByTeacherID = @TeacherID,
                                 EventApprovalDate = @ApprovalDate
                             WHERE EventID = @EventID";

            var parameters = new Dictionary<string, object>
            {
                { "@TeacherID", teacherId },
                { "@ApprovalDate", approvalDate },
                { "@EventID", eventId }
            };

            return DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
        }

        public static bool DeleteEvent(int eventId)
        {
            string query = "UPDATE Event SET IsDeleted = 1 WHERE EventID = @EventID";
            var parameters = new Dictionary<string, object> { { "@EventID", eventId } };
            return DatabaseHelper.ExecuteDelete(query, parameters) > 0;
        }

        private static List<Event> MapEventsFromDataTable(List<DataRow> dt)
        {
            var events = new List<Event>();
            foreach (DataRow row in dt)
            {
                events.Add(new Event
                {
                    EventID = Convert.ToInt32(row["EventID"]),
                    Title = row["Title"].ToString(),
                    Description = row["Description"].ToString(),
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    EventDate = Convert.ToDateTime(row["EventDate"]),
                    Venue = row["Venue"].ToString(),
                    RequiresFee = row["RequiresFee"] != DBNull.Value && Convert.ToBoolean(row["RequiresFee"]),
                    FeeAmount = row["FeeAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FeeAmount"]),
                    CreatedByStudentAridNo = row["CreatedByStudentAridNo"].ToString(),
                    ApprovedByTeacherID = row["ApprovedByTeacherID"] == DBNull.Value ? "" : row["ApprovedByTeacherID"].ToString(),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"]),
                    EventApprovalStatus = row["EventApprovalStatus"].ToString(),
                    EventApprovalDate = row["EventApprovalDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["EventApprovalDate"]),
                    FormStructure = row["FormStructure"] == DBNull.Value ? null : (List<FormField>) row["FormStructure"]
                 ,
                    TeamRequired = row["TeamRequirement"] == DBNull.Value ? "" : row["TeamRequirement"].ToString()

                }) ;
            }
            return events;
        }
    }
}
