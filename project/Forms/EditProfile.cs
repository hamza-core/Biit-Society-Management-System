using project.DataHandlers;
using project.DataHandlers.Auth;
using project.Models;
using project.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project.Forms
{
    public partial class EditProfile : Form
    {
        public EditProfile()
        {
            InitializeComponent();
        }
        public EditProfile(Student s)
        {
            InitializeComponent();


            txtName.Text = s.Name;
            txtUserName.Text = s.UserName;
            txtAridNo.Text = s.AridNo;
            txtPhoneNo.Text = s.Phone.Substring(4); // Assuming phone is stored as "+92XXXXXXXXXX"
            txtEmail.Text = s.Email;
            txtPassword.Text = s.Password;
            txtConfirmPassword.Text = s.Password;
            txtAridNo.Enabled = false; // Assuming ARID number should not be changed
            txtName.Enabled = false; // Assuming username should not be changed
            

        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
           

        

            if (
               Validations.ValidUserName(txtUserName.Text).isValid &&
               Validations.ValidPhoneNo(txtPhoneNo.Text).isValid &&
               Validations.ValidEmail(txtEmail.Text).isValid &&
               Validations.ValidPassword(txtPassword.Text).isValid &&
               txtConfirmPassword.Text == txtPassword.Text

               )
            {
                var x = StudentViewModel.UpdateStudent(new Student
                {
                    Name = txtName.Text,
                    UserName = txtUserName.Text,
                    Password = txtPassword.Text,
                    AridNo = txtAridNo.Text,
                    Phone = "+92" + txtPhoneNo.Text,
                    Email = txtEmail.Text,
                    IsDeleted = false

                });
                if (!x.isValid)
                {
                    MessageBoxHelper.ShowError(x.errMsg);
                }
                else
                {

                    MessageBoxHelper.ShowInfo("Profile Updated");
                    this.Hide();
                }

            }
            else
            {
                MessageBoxHelper.ShowInfo("Please Fill All Fields Correctly");
            }

        
     }

        private void EditProfile_Load(object sender, EventArgs e)
        {

            lblErrorAridNo.Visible = false;
            lblErrorEmail.Visible = false;
            lblErrorName.Visible = false;
            lblErrorPassword.Visible = false;
            lblErrorConfirmPassword.Visible = false;
            lblErrorUserName.Visible = false;
            lblErrorPhoneNo.Visible = false;

        }



        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtPhoneNo_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
        }

        private void TxtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

         
                var x = Validations.ValidUserName(txtUserName.Text);

                if (!x.isValid)
                {
                    lblErrorUserName.Text = x.errorMsg;
                    lblErrorUserName.Visible = true;
                }
                else
                {
                    lblErrorUserName.Visible = false;
                
            }
        }

        private void txtEmail_TextChanged_1(object sender, EventArgs e)
        {
            var x = Validations.ValidEmail(txtEmail.Text);

            if (!x.isValid)
            {
                lblErrorEmail.Text = x.errorMsg;
                lblErrorEmail.Visible = true;
            }
            else
            {
                lblErrorEmail.Visible = false;
            }
        }

        private void txtPhoneNo_TextChanged_1(object sender, EventArgs e)
        {

            var x = Validations.ValidPhoneNo(txtPhoneNo.Text);

            if (!x.isValid)
            {
                lblErrorPhoneNo.Text = x.errorMsg;
                lblErrorPhoneNo.Visible = true;
            }
            else
            {
                lblErrorPhoneNo.Visible = false;
            }
        }

        private void txtPassword_TextChanged_1(object sender, EventArgs e)
        {

            var x = Validations.ValidPassword(txtPassword.Text);

            if (!x.isValid)
            {
                lblErrorPassword.Text = x.errorMsg;
                lblErrorPassword.Visible = true;
            }
            else
            {
                lblErrorPassword.Visible = false;
            }
        }

        private void txtConfirmPassword_TextChanged_1(object sender, EventArgs e)
        {
            if (txtPassword.Text == txtConfirmPassword.Text)
            {
                lblErrorConfirmPassword.Visible = false;
            }
            else
            {
                lblErrorConfirmPassword.Text = "Password doesn't match ";
                lblErrorConfirmPassword.Visible = true;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
