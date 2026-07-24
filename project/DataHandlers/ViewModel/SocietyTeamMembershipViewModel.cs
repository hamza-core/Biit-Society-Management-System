using System;
using System.Collections.Generic;
using System.Data;
using project.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace project.DataHandlers
{
    public class SocietyTeamMembershipViewModel
    {
        public static bool AddMemberToSocietyTeam(SocietyTeamMembership membership)
        {
            string query = @"INSERT INTO SocietyTeamMembership 
                             (TeamID, MemberID, Role, JoiningDate, IsDeleted) 
                             VALUES (@TeamID, @MemberID, @Role, @JoiningDate, 0)";
            var parameters = new Dictionary<string, object>
            {
                { "@TeamID", membership.TeamID },
                { "@MemberID", membership.MemberID },
                { "@Role", membership.Role },
                { "@JoiningDate", membership.JoiningDate }
            };

            return DatabaseHelper.ExecuteInsert(query, parameters) > 0;
        }

        public static List<SocietyTeamMembership> getTeamsJoinedByStudent(String aridNo)
        {
            String query = @"
            SELECT* FROM SocietyMember sm
JOIN SocietyTeamMembership stm ON sm.MemberID = stm.MemberID
WHERE sm.AridNo = @AridNo AND sm.isDeleted = 0 AND stm.IsDeleted = 0;
";
            var parameters = new Dictionary<string, object> { { "@AridNo", aridNo } };

            DataTable dt = DatabaseHelper.ExecuteSelect(query, parameters);
            List<SocietyTeamMembership> members = new List<SocietyTeamMembership>();

            foreach (DataRow row in dt.Rows)
            {
                members.Add(new SocietyTeamMembership
                {
                    MembershipID = Convert.ToInt32(row["MembershipID"]),
                    TeamID = Convert.ToInt32(row["TeamID"]),
                    MemberID = row["MemberID"].ToString(),
                    Role = row["Role"].ToString(),
                    JoiningDate = Convert.ToDateTime(row["JoiningDate"]),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return members;

        }


        public static List<SocietyTeamMembership> GetMembersByTeam(int teamId)
        {
            string query = "SELECT * FROM SocietyTeamMembership WHERE TeamID = @TeamID AND IsDeleted = 0";
            var parameters = new Dictionary<string, object> { { "@TeamID", teamId } };

            DataTable dt = DatabaseHelper.ExecuteSelect(query, parameters);
            List<SocietyTeamMembership> members = new List<SocietyTeamMembership>();

            foreach (DataRow row in dt.Rows)
            {
                members.Add(new SocietyTeamMembership
                {
                    MembershipID = Convert.ToInt32(row["MembershipID"]),
                    TeamID = Convert.ToInt32(row["TeamID"]),
                    MemberID = row["MemberID"].ToString(),
                    Role = row["Role"].ToString(),
                    JoiningDate = Convert.ToDateTime(row["JoiningDate"]),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return members;
        }

        public static bool UpdateRole(int membershipId, string newRole)
        {
            string query = "UPDATE SocietyTeamMembership SET Role = @Role WHERE MembershipID = @MembershipID AND IsDeleted = 0";
            var parameters = new Dictionary<string, object>
            {
                { "@MembershipID", membershipId },
                { "@Role", newRole }
            };

            return DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
        }

        public static bool DeleteMembership(int membershipId)
        {
            string query = "UPDATE SocietyTeamMembership SET IsDeleted = 1 WHERE MembershipID = @MembershipID";
            var parameters = new Dictionary<string, object>
            {
                { "@MembershipID", membershipId }
            };

            return DatabaseHelper.ExecuteDelete(query, parameters) > 0;
        }
        public static bool DeleteMembershipFromAllTeam(int membershipId)
        {
            string query = "UPDATE SocietyTeamMembership SET IsDeleted = 1 WHERE MemberID = @MemberID";
            var parameters = new Dictionary<string, object>
            {
                { "@MemberID", membershipId }
            };

            return DatabaseHelper.ExecuteDelete(query, parameters) > 0;
        }
        public static List<SocietyTeamMembership> GetByRole(int teamId, string role)
        {
            string query = @"SELECT * FROM SocietyTeamMembership 
                             WHERE TeamID = @TeamID AND Role = @Role AND IsDeleted = 0";
            var parameters = new Dictionary<string, object>
            {
                { "@TeamID", teamId },
                { "@Role", role }
            };

            DataTable dt = DatabaseHelper.ExecuteSelect(query, parameters);
            List<SocietyTeamMembership> filtered = new List<SocietyTeamMembership>();

            foreach (DataRow row in dt.Rows)
            {
                filtered.Add(new SocietyTeamMembership
                {
                    MembershipID = Convert.ToInt32(row["MembershipID"]),
                    TeamID = Convert.ToInt32(row["TeamID"]),
                    MemberID = row["MemberID"].ToString(),
                    Role = row["Role"].ToString(),
                    JoiningDate = Convert.ToDateTime(row["JoiningDate"]),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return filtered;
        }
    }
}
