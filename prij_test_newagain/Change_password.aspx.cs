using System;
using System.Collections.Generic;


namespace prij_test_newagain
{
    public partial class Change_password : System.Web.UI.Page

    {
        protected System.Web.UI.WebControls.TextBox lastPassword;
        protected System.Web.UI.WebControls.TextBox newPassword;
        protected System.Web.UI.WebControls.TextBox newPasswordAgain;
        protected System.Web.UI.WebControls.Button submitPassword;
        protected System.Web.UI.WebControls.Label Message;
        protected System.Web.UI.WebControls.PlaceHolder resetPasswordPlaceHolder;
        protected System.Web.UI.WebControls.Label errorLabel;

        private List<string> validationErrors = new List<string>();
        SecurePasswordHandler SecurePassword = new SecurePasswordHandler();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BackToLoginEventMethod(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.BufferOutput = true;
            Response.Redirect("Default.aspx", false);
        }


        protected void Submit_New_Password_EventMethod(object sender, EventArgs e)
        {
            string passemail;
            string userEmail;

            passemail = (string)(Session["passemail"]);
            userEmail = passemail;


            if (!SecurePassword.VerifyHashPassword(userEmail, lastPassword.Text))
            {
                errorLabel.Text = "Current password is incorrect";
                errorLabel.Visible = true;
                errorLabel.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if ((newPassword.Text == newPasswordAgain.Text) && SecurePassword.ValidatePassword(newPassword.Text,ref validationErrors, userEmail))
            {
                resetPasswordPlaceHolder.Controls.Clear();
                lastPassword.Visible = false;
                newPassword.Visible = false;
                newPasswordAgain.Visible = false;
                submitPassword.Visible = false;

                Message.Text = "Password reset complete!";
                Message.ForeColor = System.Drawing.Color.Green;
                Message.Visible = true;

                resetPasswordPlaceHolder.Controls.Add(Message);
                string connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();
                var (hashedSaltPassword, salt) = SecurePassword.CreatePasswordHash(newPassword.Text);

                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connString))
                {

                    conn.Open();

                    string updateQuery = @"UPDATE webapp.new_tableuserregistration SET password_hash = @Password_Hash,salt = @Salt WHERE email = @Email";
                    string queryStr2 = "INSERT INTO webapp.new_user_hash_salt_data (Email, password_hash, salt) " +
                           "VALUES (@Email, @password_Hash, @salt)";

                    try
                    {
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", userEmail);
                        cmd.Parameters.AddWithValue("@Password_Hash", hashedSaltPassword);
                        cmd.Parameters.AddWithValue("@Salt", salt);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd2 = new MySql.Data.MySqlClient.MySqlCommand(queryStr2, conn))
                    {
                        cmd2.Parameters.AddWithValue("@Email", userEmail);
                        cmd2.Parameters.AddWithValue("@password_Hash", hashedSaltPassword);
                        cmd2.Parameters.AddWithValue("@salt", salt);

                        cmd2.ExecuteNonQuery();
                    }


                    string script = "setTimeout(function() { window.location.href = 'Logged_in.aspx'; }, 2000);";
                    ClientScript.RegisterStartupScript(this.GetType(), "RedirectAfterDelay", script, true);

                    }
                    catch 
                    {
                        Message.Text = "An error occurred while resetting your password. Please try again.";
                        Message.ForeColor = System.Drawing.Color.Red;
                        Message.Visible = true;
                    }
                }
            }
            else
            {
                errorLabel.Text = string.Join("<br/>", validationErrors);
                errorLabel.Visible = true;
                errorLabel.ForeColor = System.Drawing.Color.Red;
      
            }

        }
    }
}