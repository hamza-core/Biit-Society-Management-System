using System;
using System.Collections.Generic;
using System.Data;
using project.Models;

namespace project.DataHandlers
{
    public class UserViewModel
    {
  
        public bool AddUser(User user)
        {
            string query = @"INSERT INTO Users (UserName, Password, Email, Role, RelatedId)
                             VALUES (@UserName, @Password, @Email, @Role, @RelatedId)";

            var parameters = new Dictionary<string, object>
            {
                { "@UserName", user.UserName },
                { "@Password", user.Password }, 
                { "@Email", user.Email },
                { "@Role", user.Role },
                { "@RelatedId", user.RelatedId }
            };

            return DatabaseHelper.ExecuteInsert(query, parameters) > 0;
        }

        public static User Authenticate(string username, string password)
        {
            string query = @"SELECT * FROM [User] WHERE (UserName = @UserName OR EMAIL = @UserName OR RELATEDID = @USERNAME or PhoneNo = @USERNAME) AND Password = @Password";
            var parameters = new Dictionary<string, object>
            {
                { "@UserName", username.Trim() },
                { "@Password", password.Trim() }
            };

            DataRow row = DatabaseHelper.ExecuteSelectSingle(query, parameters);
            if (row == null) return null;

            return new User
            {
                UserId = Convert.ToInt32(row["UserId"]),
                UserName = row["UserName"].ToString(),
                Password = row["Password"].ToString(),
                Email = row["Email"].ToString(),
                Role = row["Role"].ToString(),
                RelatedId = row["RelatedId"].ToString()
            };
        }

        public User GetByEmail(string email)
        {
            string query = @"SELECT * FROM Users WHERE Email = @Email";
            var parameters = new Dictionary<string, object>
            {
                { "@Email", email }
            };

            DataRow row = DatabaseHelper.ExecuteSelectSingle(query, parameters);
            if (row == null) return null;

            return new User
            {
                UserId = Convert.ToInt32(row["UserId"]),
                UserName = row["UserName"].ToString(),
                Password = row["Password"].ToString(),
                Email = row["Email"].ToString(),
                Role = row["Role"].ToString(),
                RelatedId = row["RelatedId"].ToString()
            };
        }
       public List<User> GetUsersByRole(string role)
        {
            string query = @"SELECT * FROM Users WHERE Role = @Role";
            var parameters = new Dictionary<string, object>
            {
                { "@Role", role }
            };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            List<User> users = new List<User>();

            foreach (DataRow row in dt)
            {
                users.Add(new User
                {
                    UserId = Convert.ToInt32(row["UserId"]),
                    UserName = row["UserName"].ToString(),
                    Password = row["Password"].ToString(),
                    Email = row["Email"].ToString(),
                    Role = row["Role"].ToString(),
                    RelatedId = row["RelatedId"].ToString()
                });
            }

            return users;
        }
    }
}
