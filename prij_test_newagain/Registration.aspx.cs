using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace prij_test_newagain
{
    public partial class Registration : System.Web.UI.Page
    {
       
        private List<string> validationErrors = new List<string>();
        SecurePasswordHandler SecurePassword = new SecurePasswordHandler();
        

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void RegisterEventMethod(object sender, EventArgs e)
        {


            /// change to  hash and salt in registration
            if (checkUserExists(emailTextBox.Text.ToString()) == false)
            {
                if (SecurePassword.ValidatePassword(passWordTextBox.Text, ref validationErrors, emailTextBox.Text)) // Validate password before registering
                {
                    if (EmailIsValid(emailTextBox.Text))
                    {
                        RegisterUser();
                        Session.Abandon();
                        Response.BufferOutput = true;
                        Response.Redirect("Default.aspx", false);
                    }
                    else
                    {
                        errorLabel.Text = "Email is not valid";
                        errorLabel.Visible = true;
                        errorLabel.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {

                    // Show error message to the user
                    errorLabel.Text = string.Join("<br/>", validationErrors);
                    errorLabel.Visible = true;
                    errorLabel.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                errorLabel.Text = "Email is already exists";
                errorLabel.Visible = true;
                errorLabel.ForeColor = System.Drawing.Color.Red;
            }

        }


       

        public bool EmailIsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private void RegisterUser()
        {


            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();
            SecurePasswordHandler SecurePassword = new SecurePasswordHandler();
            var (hashedSaltPassword, salt) = SecurePassword.CreatePasswordHash(passWordTextBox.Text);

            // Use a using block to ensure proper disposal of the connection
            using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connString))
            {
                conn.Open();

                string queryStr1 = "INSERT INTO webapp.new_tableuserregistration (firstname, lastname, username, email, password_hash, salt) " +
                                  "VALUES (@FirstName, @LastName, @UserName, @Email, @password_Hash, @salt)";
                                  

                string queryStr2 = "INSERT INTO webapp.new_user_hash_salt_data (Email, password_hash, salt) " +
                           "VALUES (@Email, @password_Hash, @salt)";

                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(queryStr1, conn))
                {

                    // Use parameters to securely add user input
                    cmd.Parameters.AddWithValue("@FirstName", firstNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@LastName", lastNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@UserName", userNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@Email", emailTextBox.Text);
                    cmd.Parameters.AddWithValue("@password_Hash", hashedSaltPassword);
                    cmd.Parameters.AddWithValue("@Salt", salt);

                    
                    
                    // Execute the command
                    cmd.ExecuteNonQuery();   
                }
                // Execute the second INSERT statement
                using (var cmd2 = new MySql.Data.MySqlClient.MySqlCommand(queryStr2, conn))
                {
                    cmd2.Parameters.AddWithValue("@Email", emailTextBox.Text);
                    cmd2.Parameters.AddWithValue("@password_Hash", hashedSaltPassword);
                    cmd2.Parameters.AddWithValue("@salt", salt);

                    cmd2.ExecuteNonQuery();
                }
            }
        }

        private bool checkUserExists(string email)
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

            using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT userID FROM new_tableuserregistration WHERE email LIKE @Email";
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    object result = cmd.ExecuteScalar();
                    if (result == null) { return false; }
                    else return true;

                }

            }
        }    

            protected void BackToLogInEventMethod(object sender, EventArgs e)
            {
                Session.Abandon();
                Response.BufferOutput = true;
                Response.Redirect("Default.aspx", false);
            }
        }
    } 
