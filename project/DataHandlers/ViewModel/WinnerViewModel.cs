using System;
using System.Collections.Generic;
using System.Data;
using project.Models;

namespace project.DataHandlers
{
    public class WinnerViewModel
    {

        public bool AddWinner(Winner winner)
        {
            string query = @"INSERT INTO Winners (EventID, AridNo, Position, IsDeleted)
                             VALUES (@EventID, @AridNo, @Position, 0)";

            var parameters = new Dictionary<string, object>
            {
                { "@EventID", winner.EventID },
                { "@AridNo", winner.AridNo },
                { "@Position", winner.Position }
            };

            return DatabaseHelper.ExecuteInsert(query, parameters) > 0;
        }

        public List<Winner> GetWinnersByEvent(int eventId)
        {
            string query = "SELECT * FROM Winners WHERE EventID = @EventID AND IsDeleted = 0";
            var parameters = new Dictionary<string, object> { { "@EventID", eventId } };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            var winners = new List<Winner>();

            foreach (DataRow row in dt)
            {
                winners.Add(new Winner
                {
                    WinnerID = Convert.ToInt32(row["WinnerID"]),
                    EventID = Convert.ToInt32(row["EventID"]),
                    AridNo = row["AridNo"].ToString(),
                    Position = row["Position"].ToString(),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return winners;
        }

        public bool UpdateWinner(Winner winner)
        {
            string query = @"UPDATE Winners 
                             SET Position = @Position 
                             WHERE WinnerID = @WinnerID";

            var parameters = new Dictionary<string, object>
            {
                { "@Position", winner.Position },
                { "@WinnerID", winner.WinnerID }
            };

            return DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
        }
        public bool DeleteWinner(Winner winner)
        {
            string query = @"UPDATE Winners 
                             SET isDeleted = 1 
                             WHERE WinnerID = @WinnerID";

            var parameters = new Dictionary<string, object>
            {
        
                { "@WinnerID", winner.WinnerID }
            };

            return DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
        }


    }
}