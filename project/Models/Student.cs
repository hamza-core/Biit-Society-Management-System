using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Models
{
    public class Student
    {
        public String AridNo { get; set; }
        
        public String Name { get; set; }

        public String UserName { get; set; }

        public String Email { get; set; }

        public String Phone { get; set; }

        public String Password { get; set; }

        public String Department { get; set; }

        public bool IsDeleted  { get; set; }


        public override string ToString()
        {
            return "AridNo : "+AridNo+"\nName : "
                +Name + "\nEmail : "+Email+"\nPhone No : "+Phone+
                "\nPassword : "+Password+"\nDepartment : "+Department
                +"\nIsDeleted : "+IsDeleted;
        }

    }

  
}
