using project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace project.DataHandlers
{
    public class SocietyMemberViewModel
    {
    
        public static bool AddMember(SocietyMember member)
        {
            string query = @"INSERT INTO SocietyMember (AridNo, SocietyID, Role, JoiningDate,IsApproved, IsDeleted)
                             VALUES (@AridNo, @SocietyID, @Role, @JoiningDate,@IsApproved, @IsDeleted)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@AridNo", member.AridNo },
                { "@SocietyID", member.SocietyID },
                { "@Role", member.Role },
                { "@JoiningDate", member.JoiningDate },
                { "@IsDeleted", member.IsDeleted },
                {"@IsApproved", 0 }
            };

            return DatabaseHelper.ExecuteInsert(query, parameters) > 0;
        }

    /*    public static String  getRole(string AridNo)
        {
            

        }*/

        public static bool approveMember(int id,int sid)
        {
            string query = @"UPDATE SocietyMember 
                     SET isApproved = 1 
                     WHERE SocietyId = @sid and MemberID = @id ";

            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@id", id },
                {"@sid",sid }
    };

            return DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
        


    }
        public static List<SocietyMember> GetAllMembers()
        {
            string query = "SELECT * FROM SocietyMember WHERE IsDeleted = 0 and isApproved = 1";
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query ,null);

            List<SocietyMember> members = new List<SocietyMember>();
            foreach (DataRow row in dt)
            {
                members.Add(new SocietyMember
                {
                    MemberID = Convert.ToInt32(row["MemberID"]),
                    AridNo = row["AridNo"].ToString(),
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    Role = row["Role"].ToString(),
                    JoiningDate = Convert.ToDateTime(row["JoiningDate"]),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return members;
        }
        public static List<SocietyMember> GetAllMembersRequests(string id)
        {
            string query = "SELECT * FROM SocietyMember WHERE SocietyId = @id and IsDeleted = 0 and isApproved = 0";
          

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query,new Dictionary<string, object> { {"@id" ,id} });

            List<SocietyMember> members = new List<SocietyMember>();
            foreach (DataRow row in dt)
            {
                members.Add(new SocietyMember
                {
                    MemberID = Convert.ToInt32(row["MemberID"]),
                    AridNo = row["AridNo"].ToString(),
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    Role = row["Role"].ToString(),
                    JoiningDate = Convert.ToDateTime(row["JoiningDate"]),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return members;
        }

        public static List<SocietyMember> GetBySociety(int societyId)
        {
            string query = "SELECT * FROM SocietyMember WHERE SocietyID = @SocietyID AND IsDeleted = 0 and isApproved = 1";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@SocietyID", societyId }
            };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);

            List<SocietyMember> members = new List<SocietyMember>();
            foreach (DataRow row in dt)
            {
                members.Add(new SocietyMember
                {
                    MemberID = Convert.ToInt32(row["MemberID"]),
                    AridNo = row["AridNo"].ToString(),
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    Role = row["Role"].ToString(),
                    JoiningDate = Convert.ToDateTime(row["JoiningDate"]),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return members;
        }
        public static List<SocietyMember> GetByMemberId(int Id)
        {
            string query = "SELECT * FROM SocietyMember WHERE MemberID = @memID AND IsDeleted = 0 and isApproved = 1";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@memID", Id }
            };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);

            List<SocietyMember> members = new List<SocietyMember>();
            foreach (DataRow row in dt)
            {
                members.Add(new SocietyMember
                {
                    MemberID = Convert.ToInt32(row["MemberID"]),
                    AridNo = row["AridNo"].ToString(),
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    Role = row["Role"].ToString(),
                    JoiningDate = Convert.ToDateTime(row["JoiningDate"]),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return members;
        }
        public static List<SocietyMember> GetByRole(string role , int societyId)
        {
            string query = "SELECT * FROM SocietyMember WHERE Role = @Role And SocietyId = @SocietyId AND IsDeleted = 0 and isApproved = 1 ";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
        { "@Role", role },
                {"@SocietyId",societyId }
    };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            List<SocietyMember> members = new List<SocietyMember>();
            foreach (DataRow row in dt)
            {
                members.Add(new SocietyMember
                {
                    MemberID = Convert.ToInt32(row["MemberID"]),
                    AridNo = row["AridNo"].ToString(),
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    Role = row["Role"].ToString(),
                    JoiningDate = Convert.ToDateTime(row["JoiningDate"]),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return members;
        }
        /*     public List<SocietyMemberWithStudent> GetMembersWithStudentInfo(int societyId)
             {
                 string query = @"
             SELECT SM.AridNo, S.Name, S.Email, SM.SocietyID, SM.Role, SM.JoiningDate
             FROM SocietyMembers SM
             JOIN Students S ON SM.AridNo = S.ARIDNo
             WHERE SM.SocietyID = @SocietyID AND SM.IsDeleted = 0 AND S.IsDeleted = 1";

                 Dictionary<string, object> parameters = new Dictionary<string, object> {
             { "@SocietyID", societyId }
         };

                 List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
                 List<SocietyMemberWithStudent> result = new List<SocietyMemberWithStudent>();

                 foreach (DataRow row in dt)
                 {
                     result.Add(new SocietyMemberWithStudent
                     {
                         AridNo = row["AridNo"].ToString(),
                         Name = row["Name"].ToString(),
                         Email = row["Email"].ToString(),
                         SocietyID = Convert.ToInt32(row["SocietyID"]),
                         Role = row["Role"].ToString(),
                         JoiningDate = Convert.ToDateTime(row["JoiningDate"])
                     });
                 }

                 return result;
             }*/

        public static bool UpdateMemberRoleOrSociety(int memberId, string newRole, int newSocietyId)
        {
            string query = @"UPDATE SocietyMember 
                     SET Role = @Role, SocietyID = @SocietyID 
                     WHERE MemberID = @MemberID ";

            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@MemberID", memberId },
        { "@Role", newRole },
        { "@SocietyID", newSocietyId }
    };

            return DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
        }


        

        public static bool RemoveMember(int memberId)
        {
            string query = "UPDATE SocietyMember SET IsDeleted = 1 , isApproved = 0 WHERE MemberID = @MemberID ";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@MemberID", memberId }
            };

            return DatabaseHelper.ExecuteDelete(query, parameters) > 0;
        }

        public  static SocietyMember GetByAridAndSociety(string aridNo, int societyId)
        {
            string query = "SELECT * FROM SocietyMember WHERE AridNo = @AridNo AND SocietyID = @SocietyID and isApproved = 1 AND IsDeleted = 0";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@AridNo", aridNo },
                { "@SocietyID", societyId }
            };

            DataRow row = DatabaseHelper.ExecuteSelectSingle(query, parameters);
            if (row == null) return null;

            return new SocietyMember
            {
                MemberID = Convert.ToInt32(row["MemberID"]),
                AridNo = row["AridNo"].ToString(),
                SocietyID = Convert.ToInt32(row["SocietyID"]),
                Role = row["Role"].ToString(),
                JoiningDate = Convert.ToDateTime(row["JoiningDate"]),
                IsDeleted = Convert.ToBoolean(row["IsDeleted"])
            };
        }
        public static List<SocietyMember> GetByAridNo(string aridNo)
        {
            string query = "SELECT * FROM SocietyMember WHERE AridNo = @AridNo AND IsDeleted = 0and isApproved = 1";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@AridNo", aridNo },
            };

            List<DataRow>  dt= DatabaseHelper.ExecuteSelect(query, parameters);
            if (dt == null) return null;
            List<SocietyMember> members = new List<SocietyMember>();
            foreach (DataRow row in dt)
            {
                members.Add(new SocietyMember
                {
                    MemberID = Convert.ToInt32(row["MemberID"]),
                    AridNo = row["AridNo"].ToString(),
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    Role = row["Role"].ToString(),
                    JoiningDate = Convert.ToDateTime(row["JoiningDate"]),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }
            return members;
        }
        public static List<SocietyMember> GetByAridNoAndLeader(string aridNo)
        {
            string query = "SELECT * FROM SocietyMember WHERE AridNo = @AridNo AND [role] = 'Leader' AND IsDeleted = 0 and isApproved = 1";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@AridNo", aridNo },
            };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            if (dt == null) return null;
            List<SocietyMember> members = new List<SocietyMember>();
            foreach (DataRow row in dt)
            {
                members.Add(new SocietyMember
                {
                    MemberID = Convert.ToInt32(row["MemberID"]),
                    AridNo = row["AridNo"].ToString(),
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    Role = row["Role"].ToString(),
                    JoiningDate = Convert.ToDateTime(row["JoiningDate"]),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }
            return members;
        }
        public static Student GetPresident(int societyId)
        {
            string query = @"
        SELECT s.*
        FROM SocietyMember sm
        JOIN Student s ON sm.AridNo = s.ARIDNo
        WHERE sm.SocietyID = @SocietyID AND sm.Role = 'President' 
              AND sm.IsDeleted = 0 AND sm.isApproved = 1";

            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@SocietyID", societyId }
    };

            DataRow row = DatabaseHelper.ExecuteSelectSingle(query, parameters);
            if (row == null) return null;

            return new Student
            {
                AridNo = row["ARIDNo"].ToString(),
                Name = row["Name"].ToString(),
                UserName = row["UserName"].ToString(),
                Email = row["Email"].ToString(),
                Phone = row["PhoneNo"].ToString(),
                Department = row["Department"].ToString(),
                Password = row["Password"].ToString(),
                IsDeleted = bool.Parse(row["isDeleted"].ToString())
            };
        }

    }
}
