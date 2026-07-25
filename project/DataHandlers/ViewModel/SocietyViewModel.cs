using project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace project.DataHandlers
{
    public class SocietyViewModel
    {
        public static Student GetMentor(int societyId)
        {
            string query = "SELECT MentorId FROM Societies WHERE SocietyID = @SocietyID";
            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@SocietyID", societyId }
    };

            DataRow row = DatabaseHelper.ExecuteSelectSingle(query, parameters);
            if (row == null || row["MentorId"] == DBNull.Value) return null;

            string mentorAridNo = row["MentorId"].ToString();
            return StudentViewModel.GetByAridNo(mentorAridNo);
        }

        public static bool AddSociety(Society society)
        {
            string query = @"INSERT INTO Society (Name, Description, MentorID, IsDeleted)
                             VALUES (@Name, @Description, @MentorID, @IsDeleted)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Name", society.Name },
                { "@Description", society.Description },
                { "@MentorID", society.MentorID },
                { "@IsDeleted", society.IsDeleted }
            };

            return DatabaseHelper.ExecuteInsert(query, parameters) > 0;
        }

        public static List<Society> GetAllSocieties()
        {
            string query = "SELECT * FROM Society WHERE IsDeleted = 0";
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query);

            List<Society> societies = new List<Society>();
            foreach (DataRow row in dt)
            {
                societies.Add(new Society
                {
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    Name = row["Name"].ToString(),
                    Description = row["Description"].ToString(),
                    MentorID = int.Parse(row["MentorID"].ToString()),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return societies;
        }

        public static Society GetByName(string name)
        {
            string query = "SELECT * FROM Society WHERE Name = @Name AND IsDeleted = 0";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Name", name }
            };

            DataRow row = DatabaseHelper.ExecuteSelectSingle(query, parameters);
            if (row == null) return null;

            return new Society
            {
                SocietyID = Convert.ToInt32(row["SocietyID"]),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                MentorID = int.Parse(row["MentorID"].ToString()),
                IsDeleted = Convert.ToBoolean(row["IsDeleted"])
            };
        }

        public static int GetCountOfSocietyJoinedByStudent(String AridNo)
        {
            string query = "select COUNT(*) from Society s join SocietyMember sm on s.SocietyID = sm.SocietyID where Aridno = @AridNo and s.isDeleted = 0 and sm.isDeleted = 0  and isApproved = 1" ;
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@AridNo", AridNo }
            };

            DataRow row = DatabaseHelper.ExecuteSelectSingle(query, parameters);
            if (row != null && int.TryParse(row[0]?.ToString(), out int count))
            {
                return count;
            }

            return 0;
        }
        public static List<Society> GetSocietiesJoinedByStudent(String AridNo)
        {
            string query = "select * from Society s join SocietyMember sm on s.SocietyID = sm.SocietyID where sm.Aridno = @AridNo and s.isDeleted = 0 and sm.isDeleted = 0";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@AridNo", AridNo }
            };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);

            List<Society> societies = new List<Society>();
            foreach (DataRow row in dt)
            {
                societies.Add(new Society
                {
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    Name = row["Name"].ToString(),
                    Description = row["Description"].ToString(),
                    MentorID = int.Parse( row["MentorID"].ToString()),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }
            return societies;
        }

        public  static Society GetById(string id)
        {
            string query = "SELECT * FROM Society WHERE SocietyId = @id AND IsDeleted = 0";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };

            DataRow row = DatabaseHelper.ExecuteSelectSingle(query, parameters);
            if (row == null) return null;

            return new Society
            {
                SocietyID = Convert.ToInt32(row["SocietyID"]),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                MentorID = int.Parse(row["MentorID"].ToString()),
                IsDeleted = Convert.ToBoolean(row["IsDeleted"])
            };
        }

        public static List<Society> GetByMentor(int mentorId)
        {
            string query = "SELECT * FROM Society WHERE MentorID = @MentorID AND IsDeleted = 0";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@MentorID", mentorId }
            };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);

            List<Society> societies = new List<Society>();
            foreach (DataRow row in dt)
            {
                societies.Add(new Society
                {
                    SocietyID = Convert.ToInt32(row["SocietyID"]),
                    Name = row["Name"].ToString(),
                    Description = row["Description"].ToString(),
                    MentorID = int.Parse(row["MentorID"].ToString()),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return societies;
        }


        public static Teacher getMentorOfSociety(int id)
        {
            string query = @"
        SELECT T.* 
        FROM Teacher T
        INNER JOIN Society S ON S.MentorID = T.TeacherID
        WHERE S.SocietyID = @SocietyID AND S.IsDeleted = 0 AND T.IsDeleted = 0";

            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@SocietyID", id }
    };

            DataRow row = DatabaseHelper.ExecuteSelectSingle(query, parameters);
            if (row == null) return null;

            return new Teacher
            {
                TeacherID = row["TeacherID"].ToString(),
                Name = row["Name"].ToString(),
                Email = row["Email"].ToString(),
                PhoneNo = row["PhoneNo"].ToString(),
                Password = row["Password"].ToString(),
                Department = row["Department"].ToString(),
                IsMentor = Convert.ToBoolean(row["IsMentor"]),
                IsFinancePanelMember = Convert.ToBoolean(row["IsFinancePanelMember"]),
                IsDeleted = Convert.ToBoolean(row["IsDeleted"])
            };
        }

        public static bool DeleteSociety(int societyId)
        {
            string query = "UPDATE Societies SET IsDeleted = 1 WHERE SocietyID = @SocietyID";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@SocietyID", societyId }
            };

            return DatabaseHelper.ExecuteDelete(query, parameters) > 0;
        }
    }
}
