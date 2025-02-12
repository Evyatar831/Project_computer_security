using System;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;


namespace prij_test_newagain
{
    public partial class Forgot_password : System.Web.UI.Page
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        MySql.Data.MySqlClient.MySqlCommand cmd;
        MySql.Data.MySqlClient.MySqlDataReader reader;
        string queryStr;
        private static Random rand = new Random();

        // Public static field for the verification code
        public static int verification = rand.Next(111111, 999999);

        // Optional: Public static property for controlled access to the verification code
        public static int VerificationCode
        {
            get { return verification; }
            set { verification = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page load logic (e.g., showing any initial data or handling session)
        }

        protected void BackToLoginEventMethod(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.BufferOutput = true;
            Response.Redirect("Default.aspx", false);
        }

        protected void Verify_EventMethod(object sender, EventArgs e)
        {
            //string hashedInputCode = HashSHA1(verifyTextBox.Text);

            // Compare the entered code with the stored hashed verification code
            if (verifyTextBox.Text == HashSHA1(verification.ToString()))
            {
                Response.BufferOutput = true;
                Response.Redirect("Reset_password.aspx", false); // Redirect if code is correct
            }
            else
            {
                ShowMessage("Code is incorrect", System.Drawing.Color.Red);
                Message.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void Forgot_EventMethod(object sender, EventArgs e)
        {
            // Database connection string
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

            // Initialize database connection
            conn = new MySql.Data.MySqlClient.MySqlConnection(connString);
            conn.Open();

            // Generate a new verification code
            verification = rand.Next(111111, 999999);

            try
            {
                // Read email from the TextBox
                string recipientEmail = mailTextBox.Text;
                Session["userEmail"] = recipientEmail;

                // Validate email input and check if email exists in the database
                queryStr = "SELECT * FROM webapp.new_tableuserregistration WHERE email = @Email";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(queryStr, conn);

                // Use parameterized query to prevent SQL injection
                cmd.Parameters.AddWithValue("@Email", recipientEmail);

                // Execute the query
                reader = cmd.ExecuteReader();

                // If email is empty or does not exist in the database
                if (string.IsNullOrWhiteSpace(recipientEmail) || !reader.HasRows)
                {
                    ShowMessage("Please enter a valid email address.", System.Drawing.Color.Red);

                    //Message.Text = "Please enter a valid email address.";
                    Message.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Hash the verification code using SHA-1 before sending it
                string hashedVerificationCode = HashSHA1(verification.ToString());


                // Configure and send the email with the verification code
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("hitwebbapp@gmail.com");
                    mail.To.Add(recipientEmail);
                    mail.Subject = "Password Reset";
                    mail.Body = "Your verification code is: " + hashedVerificationCode;
                    mail.IsBodyHtml = true;

                    // Configure SMTP client
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("hitwebbapp@gmail.com", "ezly jpqn whgq nvqo");
                        smtp.EnableSsl = true;
                        smtp.Send(mail); // Send the email
                        ShowMessage("Verification email sent successfully!", System.Drawing.Color.Green);
                        //Message.Text = "Verification email sent successfully!";
                        Message.ForeColor = System.Drawing.Color.Green;
                    }
                }


                ButtonPlaceHolder.Controls.Clear(); // Remove all controls from the PlaceHolder
                TextBoxPlaceHolder.Controls.Clear();
                // Dynamically add the verifyButton in the same place
                ButtonPlaceHolder.Controls.Add(verifyButton);
                TextBoxPlaceHolder.Controls.Add(verifyTextBox);

                forgot_Button.Visible = false;  // Hide forgotButton
                verifyButton.Visible = true; // Make verifyButton visible
                mailTextBox.Visible = false;
                verifyTextBox.Visible = true;
            }
            catch 
            {
                Message.Text = "Error sending email"; // Handle errors
                Message.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                // Close the connection and reader            
                reader.Close();
                conn.Close();
            }
        }
        private string HashSHA1(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] data = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in data)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }

        }
        // To store the timeout ID and cancel the previous timeout if needed
        private static int timeoutId = -1;

        private void ShowMessage(string message, System.Drawing.Color color)
        {
            // Clear the previous timeout (if exists)
            if (timeoutId != -1)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "CancelPreviousTimeout", $"clearTimeout({timeoutId});", true);
            }

            // Reset the message content and visibility
            Message.Visible = false;
            Message.Text = string.Empty;

            // Set new message content and color
            Message.Text = message;
            Message.ForeColor = color;
            Message.Visible = true;

            // Create a timeout to hide the message after 2 seconds
            timeoutId = new Random().Next(); // Generate a new unique timeout ID
            string script = $"setTimeout(function() {{ document.getElementById('{Message.ClientID}').style.display = 'none'; }}, 2000);";

            // Register the timeout script with a unique identifier
            ClientScript.RegisterStartupScript(this.GetType(), $"HideMessage{timeoutId}", script, true);
        }

    }
}