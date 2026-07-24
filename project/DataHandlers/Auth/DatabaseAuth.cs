using project.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace project.DataHandlers.Auth
{
    internal class DatabaseAuth
    {
        public static (bool, String Role) Login(String email, String password)
        {
            DataRow result = DatabaseHelper.ExecuteSelectSingle("Select * from [user] where (RelatedId =@email or email = @email  or Username = @email and  password = @password)", new Dictionary<string, object>
            {
                { "@email", email },
                { "@password", password }

            });

            if (result == null)
            {
                return (false, null);
            }
            return (true, result[4] as String);




        }


        public static (bool isValid, String data) SignUp(Student s)
        {
            bool AridNoExist = DatabaseHelper.ExecuteExists("Select top 1 * from  [User] where RelatedId = @AridNo",new Dictionary<string, object> { {"@AridNo",s.AridNo } });
            bool UserNameExist = DatabaseHelper.ExecuteExists("Select top 1 * from  [User] where Username = @UserName", new Dictionary<string, object> { { "@UserName", s.UserName } });
            bool PhoneNoExist = DatabaseHelper.ExecuteExists("Select top 1 * from [User] where PhoneNo = @PhoneNo", new Dictionary<string, object> { { "@PhoneNo", s.Phone } });
            bool EmailExist = DatabaseHelper.ExecuteExists("Select top 1 * from  [User] where Email = @Email", new Dictionary<string, object> { { "@Email", s.Email } });


            if (AridNoExist)
            {
                return (false, "Arid No Already Registered");
            }
            if (UserNameExist)
            {
                return (false, "UserName Already taken");
            }
            if (PhoneNoExist)
            {
                return (false, "PhoneNo  Already Regisered with Another Account");

            }
            if(EmailExist)
            {
                return (false, "Email Already Registered with Another Account");
            }

           int x=  DatabaseHelper.ExecuteInsert("Insert into Student (AridNo , Name , Email , PhoneNo ,Department , UserName, password , isDeleted) values(@AridNo , @Name ,@Email, @PhoneNo, @Department , @UserName , @Password , 0)" , new Dictionary<string, object>
            {
                { "@AridNo",s.AridNo },
                {"@Name",s.Name },
                {"@Email",s.Email },
                {"@PhoneNo",s.Phone },
                {"@Department",s.Department },
                {"@UserName",s.UserName },
                {"@Password" ,s.Password }
            });
            if (x < 0)
            {
                return (false, "Error");
            }
          int y =  DatabaseHelper.ExecuteInsert("Insert into [user] (userName , Email , PhoneNo , password ,Role, RelatedId) values( @UserName ,@Email, @PhoneNo, @password, @role , @AridNo)", new Dictionary<string, object>
            {
                { "@AridNo",s.AridNo },
                {"@Email",s.Email },
                {"@PhoneNo",s.Phone },
              {"@role" ,"Student" },
                {"@UserName",s.UserName },
                {"@Password" ,s.Password }
            });

           if( y > 0)
            {
                MessageBoxHelper.ShowInfo("Account SuccesFully Created You Can Now Login ");
                return (true, "");
            }
           
            return (false, "InsertionError");

        }
    }
}
