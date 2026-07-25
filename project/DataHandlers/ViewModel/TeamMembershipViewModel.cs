using System;
using System.Collections.Generic;
using System.Data;
using project.Models;

namespace project.DataHandlers
{
    public class TeamMembershipViewModel
    {
        public bool AddMemberToTeam(TeamMembership membership)
        {
            // Check if already added
            string checkQuery = "SELECT * FROM TeamMembership WHERE TeamID = @TeamID AND AridNo = @AridNo AND IsDeleted = 0";
            var checkParams = new Dictionary<string, object>
            {
                { "@TeamID", membership.TeamID },
                { "@AridNo", membership.AridNo }
            };
            var existing = DatabaseHelper.ExecuteSelect(checkQuery, checkParams);
            if (existing.Count > 0) return false;

            string query = @"INSERT INTO TeamMembership (TeamID, AridNo, Role,  IsDeleted)
                             VALUES (@TeamID, @AridNo, @Role, 0)";
            var parameters = new Dictionary<string, object>
            {
                { "@TeamID", membership.TeamID },
                { "@AridNo", membership.AridNo },
                { "@Role", membership.Role },
                  };

            return DatabaseHelper.ExecuteInsert(query, parameters) > 0;
        }

        public List<TeamMembership> GetMembersByTeam(int teamId)
        {
            string query = "SELECT * FROM TeamMembership WHERE TeamID = @TeamID AND IsDeleted = 0";
            var parameters = new Dictionary<string, object> { { "@TeamID", teamId } };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            var members = new List<TeamMembership>();

            foreach (DataRow row in dt)
            {
                members.Add(new TeamMembership
                {
                    MembershipID = Convert.ToInt32(row["MembershipID"]),
                    TeamID = Convert.ToInt32(row["TeamID"]),
                    AridNo = row["AridNo"].ToString(),
                    Role = row["Role"].ToString(),
                        IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return members;
        }

        public bool RemoveMember(int membershipId)
        {
            string query = "UPDATE TeamMembership SET IsDeleted = 1 WHERE MembershipID = @MembershipID";
            var parameters = new Dictionary<string, object> { { "@MembershipID", membershipId } };
            return DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
        }

        public List<TeamMembership> GetMembersByRole(string role)
        {
            string query = "SELECT * FROM TeamMembership WHERE Role = @Role AND IsDeleted = 0";
            var parameters = new Dictionary<string, object> { { "@Role", role } };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            var members = new List<TeamMembership>();

            foreach (DataRow row in dt)
            {
                members.Add(new TeamMembership
                {
                    MembershipID = Convert.ToInt32(row["MembershipID"]),
                    TeamID = Convert.ToInt32(row["TeamID"]),
                    AridNo = row["AridNo"].ToString(),
                    Role = row["Role"].ToString(),
                        IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return members;
        }

        public bool UpdateTeamMembership(TeamMembership membership)
        {
            string query = @"UPDATE TeamMembership 
                     SET TeamID = @TeamID, 
                         AridNo = @AridNo, 
                         Role = @Role, 
                       
                     WHERE MembershipID = @MembershipID AND IsDeleted = 0";

            var parameters = new Dictionary<string, object>
    {
        { "@TeamID", membership.TeamID },
        { "@AridNo", membership.AridNo },
        { "@Role", membership.Role },
             { "@MembershipID", membership.MembershipID }
    };

            return DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
        }

    }
}
