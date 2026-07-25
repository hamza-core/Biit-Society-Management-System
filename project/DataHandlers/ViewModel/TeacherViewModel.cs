using project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace project.DataHandlers
{
    public class TeacherViewModel
    {
     
        public bool AddTeacher(Teacher teacher)
        {
            string query = @"INSERT INTO Teacher 
                (TeacherID, Name, Email, PhoneNo, Password, Department, IsMentor, IsFinancePanelMember, IsDeleted)
                VALUES 
                (@TeacherID, @Name, @Email, @PhoneNo, @Password, @Department, @IsMentor, @IsFinancePanelMember, @IsDeleted)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@TeacherID", teacher.TeacherID },
                { "@Name", teacher.Name },
                { "@Email", teacher.Email },
                { "@PhoneNo", teacher.PhoneNo },
                { "@Password", teacher.Password },
                { "@Department", teacher.Department },
                { "@IsMentor", teacher.IsMentor },
                { "@IsFinancePanelMember", teacher.IsFinancePanelMember },
                { "@IsDeleted", teacher.IsDeleted }
            };

            int c = DatabaseHelper.ExecuteInsert(query, parameters) ;

            new UserViewModel().AddUser(new User
            {
                UserName = teacher.TeacherID,
                Email = teacher.Email,
                Password = teacher.Password,
                Role = "Teacher",
                RelatedId = teacher.TeacherID,
            });
            return c > 0;
        
        }

        public List<Teacher> GetAllTeachers()
        {
            string query = "SELECT * FROM Teacher WHERE IsDeleted = 0";
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, null);

            List<Teacher> teachers = new List<Teacher>();
            foreach (DataRow row in dt)
            {
                teachers.Add(new Teacher
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
                });
            }

            return teachers;
        }

        public List<Teacher> GetTeachersByName(string name)
        {
            string query = "SELECT * FROM Teacher WHERE Name = @name and  IsDeleted = 0";

            Dictionary<String, object> parameters = new Dictionary<string, object>
            {
                { "@name" ,name },
            };
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);

            List<Teacher> teachers = new List<Teacher>();
            foreach (DataRow row in dt)
            {
                teachers.Add(new Teacher
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
                });
            }

            return teachers;
        }

        public Teacher GetByEmail(string email)
        {
            string query = "SELECT * FROM Teacher WHERE Email = @Email AND IsDeleted = 0";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Email", email }
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

        public static Teacher GetTeacherById(string id)
        {
            string query = "SELECT * FROM Teacher WHERE TeacherId = @id AND IsDeleted = 0";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", id }
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


        public List<Teacher> GetByDepartment(string department)
        {
            string query = "SELECT * FROM Teacher WHERE Department = @Department AND IsDeleted = 0";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Department", department }
            };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            List<Teacher> teachers = new List<Teacher>();

            foreach (DataRow row in dt)
            {
                teachers.Add(new Teacher
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
                });
            }

            return teachers;
        }

        public bool DeleteTeacher(string teacherId)
        {
            string query = "UPDATE Teacher SET IsDeleted = 1 WHERE TeacherID = @TeacherID";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@TeacherID", teacherId }
            };

            return DatabaseHelper.ExecuteDelete(query, parameters) > 0;
        }
        public static List<Teacher> GetFinancePanelMembers()
        {
            string query = "SELECT * FROM Teacher WHERE IsFinancePanelMember = 1 AND IsDeleted = 0";
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query);

            List<Teacher> members = new List<Teacher>();
            foreach (DataRow row in dt)
            {
                members.Add(new Teacher
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
                });
            }

            return members;
        }

        public static Teacher GetMentor(int societyId)
        {
            string query = "SELECT MentorId FROM Societies WHERE SocietyID = @SocietyID";
            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@SocietyID", societyId }
    };

            DataRow row = DatabaseHelper.ExecuteSelectSingle(query, parameters);
            if (row == null || row["MentorId"] == DBNull.Value)
                return null;

            string mentorId = row["MentorId"].ToString();
            return TeacherViewModel.GetTeacherById(mentorId);
        }

    }
}
