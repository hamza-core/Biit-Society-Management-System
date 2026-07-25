using System;
using System.Collections.Generic;
using System.Data;
using project.Models;

namespace project.DataHandlers
    {
        public class SocietyTeamViewModel
        {
            public static bool AddTeam(SocietyTeam team)
            {
                string query = @"INSERT INTO SocietyTeam (SocietyID, TeamName, Description, IsDeleted) 
                             VALUES (@SocietyID, @TeamName, @Description, 0)";
                var parameters = new Dictionary<string, object>
            {
                {"@SocietyID", team.SocietyID},
                {"@TeamName", team.TeamName},
                {"@Description", team.Description}
            };

                return DatabaseHelper.ExecuteInsert(query, parameters) > 0;
            }

            public static bool UpdateTeam(SocietyTeam team)
            {
                string query = @"UPDATE SocietyTeam SET TeamName = @TeamName, Description = @Description 
                             WHERE TeamID = @TeamID AND IsDeleted = 0";
                var parameters = new Dictionary<string, object>
            {
                {"@TeamID", team.TeamID},
                {"@TeamName", team.TeamName},
                {"@Description", team.Description}
            };

                return DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
            }

            public static bool DeleteTeam(int teamId)
            {
                string query = "UPDATE SocietyTeam SET IsDeleted = 1 WHERE TeamID = @TeamID";
                var parameters = new Dictionary<string, object>
            {
                {"@TeamID", teamId}
            };

                return DatabaseHelper.ExecuteDelete(query, parameters) > 0;
            }

            public static List<SocietyTeam> GetTeamsBySociety(int societyId)
            {
                string query = "SELECT * FROM SocietyTeam WHERE SocietyID = @SocietyID AND IsDeleted = 0";
                var parameters = new Dictionary<string, object> { { "@SocietyID", societyId } };

                List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
                List<SocietyTeam> teams = new List<SocietyTeam>();

                foreach (DataRow row in dt)
                {
                    teams.Add(new SocietyTeam
                    {
                        TeamID = Convert.ToInt32(row["TeamID"]),
                        SocietyID = Convert.ToInt32(row["SocietyID"]),
                        TeamName = row["TeamName"].ToString(),
                        Description = row["Description"].ToString(),
                        IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                    });
                }

                return teams;
            }

            public static SocietyTeam GetTeamById(int teamId)
            {
                string query = "SELECT * FROM SocietyTeam WHERE TeamID = @TeamID AND IsDeleted = 0";
                var parameters = new Dictionary<string, object> { { "@TeamID", teamId } };

                DataRow row = DatabaseHelper.ExecuteSelectSingle(query, parameters);
                if (row == null) return null;

                return new SocietyTeam
                {
                    TeamID = Convert.ToInt32(row["TeamID"]),
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    TeamName = row["TeamName"].ToString(),
                    Description = row["Description"].ToString(),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                };
            }
        public static SocietyTeam GetTeamBySocientyAndName(int societyID ,String teamName)
        {
            string query = "SELECT * FROM SocietyTeam WHERE TeamName = @teamName AND SocietyId = @societyId AND IsDeleted = 0";
            var parameters = new Dictionary<string, object> { { "@TeamID", teamName },{"societyId",societyID } };

            DataRow row = DatabaseHelper.ExecuteSelectSingle(query, parameters);
            if (row == null) return null;

            return new SocietyTeam
            {
                TeamID = Convert.ToInt32(row["TeamID"]),
                SocietyID = Convert.ToInt32(row["SocietyID"]),
                TeamName = row["TeamName"].ToString(),
                Description = row["Description"].ToString(),
                IsDeleted = Convert.ToBoolean(row["IsDeleted"])
            };
        }
    }
    }


