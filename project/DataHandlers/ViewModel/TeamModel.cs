using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using project.Models;

namespace project.DataHandlers
{
    public class TeamViewModel
    {
        public static bool AddTeam(Team team)
        {
            string query = @"INSERT INTO Team (EventID, TeamName, IsDeleted)
                             VALUES (@EventID, @TeamName, 0)";
            var parameters = new Dictionary<string, object>
            {
                { "@EventID", team.EventID },
                { "@TeamName", team.TeamName }
            };

            return DatabaseHelper.ExecuteInsert(query, parameters) > 0;
        }

        public static bool UpdateTeam(Team team)
        {
            string query = @"UPDATE Team 
                     SET EventID = @EventID, 
                         TeamName = @TeamName 
                     WHERE TeamID = @TeamID AND IsDeleted = 0";

            var parameters = new Dictionary<string, object>
    {
        { "@EventID", team.EventID },
        { "@TeamName", team.TeamName },
        { "@TeamID", team.TeamID }
    };

            return DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
        }


        public static List<Team> GetTeamsByEvent(int eventId)
        {
            string query = "SELECT * FROM Team WHERE EventID = @EventID AND IsDeleted = 0";
            var parameters = new Dictionary<string, object> { { "@EventID", eventId } };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            var teams = new List<Team>();

            foreach (DataRow row in dt)
            {
                teams.Add(new Team
                {
                    TeamID = Convert.ToInt32(row["TeamID"]),
                    EventID = Convert.ToInt32(row["EventID"]),
                    TeamName = row["TeamName"].ToString(),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return teams;
        }

        public static bool DeleteTeam(int teamId)
        {
            string query = "UPDATE Team SET IsDeleted = 1 WHERE TeamID = @TeamID";
            var parameters = new Dictionary<string, object> { { "@TeamID", teamId } };
            return DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
        }
    }
}
