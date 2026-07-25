using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project.Models;

namespace project.DataHandlers
{

    public class StudentViewModel
    {

        public static bool AddStudent(Student student)
        {
            string query = @"INSERT INTO Student (ARIDNo, FullName, Email, Password)
                         VALUES (@ARIDNo, @Name, @Email,,@PhoneNo,@Department, @Password,@Username)";

            Dictionary<String, object> parameters = new Dictionary<String, object>
            {
                { "@ARIDNo", student.AridNo },
                { "@Name", student.Name },
                { "@Email", student.Email },
                { "@PhoneNo", student.Phone },
                { "@Department", student.Department },
                { "@isDeleted", 1 },
                { "@Password", student.Password },
                { "@UserName", student.UserName }
             };




        int c=    DatabaseHelper.ExecuteInsert(query, parameters) ;

            new UserViewModel().AddUser(new User
            {
                UserName = student.UserName,
                Email = student.Email,
                Password = student.Password,
                Role ="Student",
                RelatedId = student.AridNo,


            });

            return c > 0;
        
        }


        public static (bool isValid , string errMsg)  UpdateStudent(Student student)


        {

            bool UserNameExist = DatabaseHelper.ExecuteExists("Select top 1 * from  [Student] where Username = @UserName AND AridNo !=@AridNo", new Dictionary<string, object> { { "@UserName", student.UserName },{"@AridNo",student.AridNo } });
            bool PhoneNoExist = DatabaseHelper.ExecuteExists("Select top 1 * from [Student] where PhoneNo = @PhoneNo AND AridNo !=@AridNo", new Dictionary<string, object> { { "@PhoneNo", student.Phone }, { "@AridNo", student.AridNo } });
            bool EmailExist = DatabaseHelper.ExecuteExists("Select top 1 * from  [Student] where Email = @Email AND AridNo !=@AridNo", new Dictionary<string, object> { { "@Email", student.Email },{ "@AridNo", student.AridNo } });


            if (UserNameExist)
            {
                return (false, "UserName Already taken");
            }
            if (PhoneNoExist)
            {
                return (false, "PhoneNo  Already Regisered with Another Account");

            }
            if (EmailExist)
            {
                return (false, "Email Already Registered with Another Account");
            }



            string query = @"UPDATE Student SET Name = @Name, Email = @Email, PhoneNo = @PhoneNo, Password = @Password, Username = @Username WHERE AridNo = @ARIDNo";

            Dictionary<String, object> parameters = new Dictionary<string, object>
            {
                { "@ARIDNo", student.AridNo },
                { "@Name", student.Name },
                { "@Email", student.Email },
                { "@PhoneNo", student.Phone },
                { "@Password", student.Password },
                { "@Username", student.UserName }
            };

            var x = DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
            if (x)
            {
                 query = @"UPDATE [User] SET Email = @Email, PhoneNo = @PhoneNo, Password = @Password, Username = @Username WHERE RelatedID = @RelatedID and isDeleted = 0";
                 parameters = new Dictionary<string, object>
            {
                { "@RelatedID", student.AridNo },
                { "@Email", student.Email },
                { "@PhoneNo", student.Phone },
                { "@Password", student.Password },
                { "@Username", student.UserName }
            };
                var Y = DatabaseHelper.ExecuteUpdate(query, parameters) > 0;

            }

            return (x, x ? "Profile Updated Successfully" : "Something Went Wrong While Updating Profile");
        }

        public static List<Student> GetAllStudents()
        {
            string query = "SELECT * FROM Student where isDeleted = 0";
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, null);

            List<Student> students = new List<Student>();
            foreach (DataRow row in dt)
            {
                students.Add(new Student
                {

                    AridNo = row["ARIDNo"].ToString(),
                    Name = row["Name"].ToString(),
                    UserName = row["Username"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["PhoneNo"].ToString(),
                    Password = row["Password"].ToString(),
                    Department = row["Department"].ToString(),
                    IsDeleted = bool.Parse( row["isDeleted"].ToString()),

                });
            }

            return students;
        }

        public static Student GetByEmail(string email)
        {
            string query = "SELECT * FROM Student WHERE Email = @Email and isDeleted = 0";
            Dictionary<String, object> parameters = new Dictionary<string, object>{
                { "@Email", email}
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
                IsDeleted = bool.Parse(row["isDeleted"].ToString()),
                Password = row["Password"].ToString()
            };
        }

        public static Student GetByAridNo(string Aridno)
        {
            string query = "SELECT * FROM Student WHERE AridNo = @AridNo and isDeleted = 0";
            Dictionary<String, object> parameters = new Dictionary<string, object>{
                { "@AridNo", Aridno}
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
                IsDeleted = bool.Parse(row["isDeleted"].ToString()),
                Password = row["Password"].ToString()
            };
        }

        public static List<Student> GetByName(string name)
        {
            string query = "SELECT * FROM Student WHERE Name = @name and isDeleted = 0";
            Dictionary<String, object> parameters = new Dictionary<string, object>{
                { "@name", name}
            };
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);

            List<Student> students = new List<Student>();
            foreach (DataRow row in dt)
            {
                students.Add(new Student
                {

                    AridNo = row["ARIDNo"].ToString(),
                    Name = row["Name"].ToString(),
                    UserName = row["Username"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["PhoneNo"].ToString(),
                    Password = row["Password"].ToString(),
                    Department = row["Department"].ToString(),
                    IsDeleted = bool.Parse(row["isDeleted"].ToString()),

                });
            }

            return students;

        }
        public static List<Student> GetByDepartment(string department)
        {
            string query = "SELECT * FROM Student WHERE Department = @Department and isDeleted = 0";
            Dictionary<String, object> parameters = new Dictionary<string, object>{
                { "@Department", department}
            };
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);

            List<Student> students = new List<Student>();
            foreach (DataRow row in dt)
            {
                students.Add(new Student
                {

                    AridNo = row["ARIDNo"].ToString(),
                    Name = row["Name"].ToString(),
                    UserName = row["Username"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["PhoneNo"].ToString(),
                    Password = row["Password"].ToString(),
                    Department = row["Department"].ToString(),
                    IsDeleted = bool.Parse(row["isDeleted"].ToString()),

                });
            }

            return students;

        }

        public static bool DeleteStudent(string aridNo)
        {

            string query = "UPDATE Student SET IsDeleted = 1 WHERE AridNo = @AridNo";
            Dictionary<String, object> parameter = new Dictionary<string, object>{
                      { "@AridNo", aridNo }
                  };
            return DatabaseHelper.ExecuteDelete(query, parameter) > 0;
        }

    }


}

