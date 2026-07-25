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
    public partial class StudentSignUpForm : Form
    {
        Form prev;
        public StudentSignUpForm()
        {
            InitializeComponent();
        }
        public StudentSignUpForm(Form p)
        {
            InitializeComponent();
            prev = p;
        }



        private void StudentSignUpForm_Load(object sender, EventArgs e)
        {
            lblErrorAridNo.Visible = false;
            lblErrorEmail.Visible = false;
            lblErrorName.Visible = false;
            lblErrorPassword.Visible = false;
            lblErrorConfirmPassword.Visible = false;
            lblErrorUserName.Visible = false; 
            lblErrorPhoneNo.Visible = false;
        }
        String department = "";
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            department = cmbDepartment.SelectedItem.ToString();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            prev.Show();
            this.Hide();
        }

        private void txtAridNo_TextChanged(object sender, EventArgs e)
        {
          var x =   Validations.ValidAridNo(txtAridNo.Text);

            if (!x.isValid)
            {
                lblErrorAridNo.Text = x.errorMsg;
                lblErrorAridNo.Visible = true;
            }
            else
            {
                lblErrorAridNo.Visible = false;
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (Validations.ValidName(txtName.Text).isValid &&
                Validations.ValidAridNo(txtAridNo.Text).isValid &&
                Validations.ValidUserName(txtUserName.Text).isValid &&
                Validations.ValidPhoneNo(txtPhoneNo.Text).isValid &&
                Validations.ValidEmail(txtEmail.Text).isValid &&
                Validations.ValidPassword(txtPassword.Text).isValid &&
                txtConfirmPassword.Text == txtPassword.Text
                && cmbDepartment.SelectedIndex != -1
                )
            {
               var x =  DatabaseAuth.SignUp(new Student
                {
                    Name = txtName.Text,
                    UserName = txtUserName.Text,
                    Password = txtPassword.Text,
                    AridNo = txtAridNo.Text,
                    Department = cmbDepartment.SelectedItem.ToString(),
                    Phone = "+92" + txtPhoneNo.Text,
                    Email = txtEmail.Text,
                    IsDeleted = false

                });
                if(!x.isValid){
                    MessageBoxHelper.ShowError(x.data);
                }
               

            
            }
            else{
                MessageBoxHelper.ShowError("Please Fill The required Field First");
            }


        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            var x = Validations.ValidName(txtName.Text);

            if (!x.isValid)
            {
                lblErrorName.Text = x.errorMsg;
                lblErrorName.Visible = true;
            }
            else
            {
                lblErrorName.Visible = false;
            }
        
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

        private void txtEmail_TextChanged(object sender, EventArgs e)
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

        private void txtPhoneNo_TextChanged(object sender, EventArgs e)
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

        private void txtPassword_TextChanged(object sender, EventArgs e)
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

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if(txtPassword.Text == txtConfirmPassword.Text)
            {
                lblErrorConfirmPassword.Visible = false;
            }
            else
            {
                lblErrorConfirmPassword.Text = "Password doesn't match ";
                lblErrorConfirmPassword.Visible = true;

            }
        }
    }
}
