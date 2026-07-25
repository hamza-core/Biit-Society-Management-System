using System;
using System.Collections.Generic;
using System.Data;
using project.Models;  // Assuming EventParticipation class is here

namespace project.DataHandlers.ViewModel
{
    public class EventParticipationViewModel
    {
        // Add participation
        private const int MaxParticipants = 25;

        public static bool AddParticipation(EventParticipation participation)
        {
            if (GetParticipantCount(participation.EventID) >= MaxParticipants)
            {
                // Max participants reached, do not allow new registration
                return false;
            }
            string query = @"
INSERT INTO EventParticipation 
(EventID, AridNo, Role, FeePaid, PaymentDate, AdditionalData, IsDeleted) 
VALUES 
(@EventID, @AridNo, @Role, @FeePaid, @PaymentDate, @AdditionalData, 0)";

            var parameters = new Dictionary<string, object>
        {
            { "@EventID", participation.EventID },
            { "@AridNo", participation.AridNo },
            { "@Role", participation.Role },
            { "@FeePaid", participation.FeePaid },
            { "@PaymentDate", participation.PaymentDate },
                {"@AdditionalData",participation.AdditionalData }
        };

            return DatabaseHelper.ExecuteInsert(query, parameters) > 0;
        }

        public static int GetParticipantCount(int eventId)
        {
            string query = "SELECT COUNT(*) FROM EventParticipation WHERE EventID = @EventID AND IsDeleted = 0";
            var parameters = new Dictionary<string, object> { { "@EventID", eventId } };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);

            if (dt.Count > 0)
            {
                return Convert.ToInt32(dt[0][0]);
            }

            return 0;
        }


        // Get participations by event
        public static List<EventParticipation> GetByEvent(int eventId)
        {
            string query = "SELECT * FROM EventParticipation WHERE EventID = @EventID AND IsDeleted = 0";
            var parameters = new Dictionary<string, object> { { "@EventID", eventId } };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);

            List<EventParticipation> list = new List<EventParticipation>();
            foreach (DataRow row in dt)
            {
                list.Add(new EventParticipation
                {
                    ParticipationID = Convert.ToInt32(row["ParticipationID"]),
                    EventID = Convert.ToInt32(row["EventID"]),
                    AridNo = row["AridNo"].ToString(),
                    Role = row["Role"].ToString(),
                    FeePaid = Convert.ToBoolean(row["FeePaid"]),
                    PaymentDate = Convert.ToDateTime(row["PaymentDate"]),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"]),
                    AdditionalData = row["AdditionalData"].ToString()
                });
            }
            return list;
        }

        // Get participations by student
        public static int GetNoOfEventsParticipatedByStudent(string aridNo)
        {
            string query = "SELECT * FROM EventParticipation WHERE AridNo = @AridNo And Role = @part AND IsDeleted = 0";
            var parameters = new Dictionary<string, object> { { "@AridNo", aridNo } ,{"@part", "Participant"} };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);

            List<EventParticipation> list = new List<EventParticipation>();
            foreach (DataRow row in dt)
            {
                list.Add(new EventParticipation
                {
                    ParticipationID = Convert.ToInt32(row["ParticipationID"]),
                    EventID = Convert.ToInt32(row["EventID"]),
                    AridNo = row["AridNo"].ToString(),
                    Role = row["Role"].ToString(),
                    FeePaid = Convert.ToBoolean(row["FeePaid"]),
                    PaymentDate = Convert.ToDateTime(row["PaymentDate"]),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"]),

                    AdditionalData = row["AdditionalData"].ToString()
                });
            }
            return list.Count;
        }

        // Update FeePaid and PaymentDate
        public static bool UpdatePaymentStatus(int participationId, bool feePaid, DateTime paymentDate)
        {
            string query = @"
                UPDATE EventParticipation
                SET FeePaid = @FeePaid, PaymentDate = @PaymentDate
                WHERE ParticipationID = @ParticipationID";

            var parameters = new Dictionary<string, object>
            {
                { "@FeePaid", feePaid },
                { "@PaymentDate", paymentDate },
                { "@ParticipationID", participationId }
            };

            return DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
        }

        // Soft delete participation
        public bool DeleteParticipation(int participationId)
        {
            string query = "UPDATE EventParticipation SET IsDeleted = 1 WHERE ParticipationID = @ParticipationID";

            var parameters = new Dictionary<string, object>
            {
                { "@ParticipationID", participationId }
            };

            return DatabaseHelper.ExecuteDelete(query, parameters) > 0;
        }
    }
}
