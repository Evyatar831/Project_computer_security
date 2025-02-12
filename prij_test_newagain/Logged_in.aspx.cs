using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace prij_test_newagain
{

    /// Manages customer data with comprehensive XSS and sql injection protection at input, storage, and display stages

    public partial class Logged_in : System.Web.UI.Page
    {
        // Securely stores database connection string for customer data operations
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["WebAppConnString"].ToString();

        // Initializes page and validates user session with XSS protection for displayed content
        protected void Page_Load(object sender, EventArgs e)
        {
            string name;
            string uemail;
            string passemail;
            name = (string)(Session["uname"]);
            uemail = (string)(Session["uemail"]);
            passemail = uemail;
            Session["passemail"] = uemail;

            if (!IsPostBack)
            {
                PopulateCustomerTable();
            }

            if (name == null)
            {
                Response.BufferOutput = true;
                Response.Redirect("Default.aspx", false);
            }
            else
            {
                userlabel.Text = SecurityUtilities.SanitizeInput(name);
            }
        }

        // Securely terminates user session and redirects to login page
        protected void LogOutEventMethod(object sender, EventArgs e)
        {
            Session["uname"] = null;
            Session.Abandon();
            Response.BufferOutput = true;
            Response.Redirect("Default.aspx", false);
        }

        // Safely redirects user to password change page while maintaining session security
        protected void ChangePasswordEventMethod(object sender, EventArgs e)
        {
            Response.BufferOutput = true;
            Response.Redirect("Change_Password.aspx", false);
        }

        // Processes customer data with XSS protection before storing in database
        protected void SaveCustomerEventMethod(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddCustomerID.Text) ||
                string.IsNullOrWhiteSpace(txtAddCustomerName.Text) ||
                string.IsNullOrWhiteSpace(txtAddCustomerEmail.Text) ||
                string.IsNullOrWhiteSpace(txtAddCustomerPhone.Text) ||
                string.IsNullOrWhiteSpace(txtAddCustomerAddress.Text) ||
                string.IsNullOrWhiteSpace(txtAddPackageType.Text) ||
                string.IsNullOrWhiteSpace(txtAddPackagePrice.Text))
            {
                addMessage.Text = SecurityUtilities.SanitizeInput("All fields are required");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "keepModalOpen",
                    "$('#addCustomerModal').modal('show');", true);
                return;
            }

            string saveQuery = @"INSERT INTO webapp.customers 
                (customer_ID, name, email, phone, address, package_type, package_price) 
                VALUES (@CustomerID, @Name, @Email, @Phone, @Address, @PackageType, @PackagePrice)";

            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(saveQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", SecurityUtilities.SanitizeInput(txtAddCustomerID.Text));
                        cmd.Parameters.AddWithValue("@Name", SecurityUtilities.SanitizeInput(txtAddCustomerName.Text));
                        cmd.Parameters.AddWithValue("@Email", SecurityUtilities.SanitizeInput(txtAddCustomerEmail.Text));
                        cmd.Parameters.AddWithValue("@Phone", SecurityUtilities.SanitizeInput(txtAddCustomerPhone.Text));
                        cmd.Parameters.AddWithValue("@Address", SecurityUtilities.SanitizeInput(txtAddCustomerAddress.Text));
                        cmd.Parameters.AddWithValue("@PackageType", SecurityUtilities.SanitizeInput(txtAddPackageType.Text));
                        cmd.Parameters.AddWithValue("@PackagePrice", SecurityUtilities.SanitizeInput(txtAddPackagePrice.Text));

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            PopulateCustomerTable();
                            addMessage.Text = "";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                                "alert('Customer added successfully!'); $('#addCustomerModal').modal('hide');", true);
                        }
                        else
                        {
                            addMessage.Text = SecurityUtilities.SanitizeInput("Failed to add customer");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "keepModalOpen",
                                "$('#addCustomerModal').modal('show');", true);
                        }
                    }
                }
            }
            catch
            {
                addMessage.Text = SecurityUtilities.SanitizeInput("Error adding customer! Check input fields/User with selected ID already exists");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "keepModalOpen",
                    "$('#addCustomerModal').modal('show');", true);
            }
        }

        // Updates existing customer data with XSS protection and parameter sanitization
        protected void UpdateCustomerEventMethod(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUpdateCustomerID.Text) ||
                string.IsNullOrWhiteSpace(txtUpdateCustomerName.Text) ||
                string.IsNullOrWhiteSpace(txtUpdateCustomerEmail.Text) ||
                string.IsNullOrWhiteSpace(txtUpdateCustomerPhone.Text) ||
                string.IsNullOrWhiteSpace(txtUpdateCustomerAddress.Text) ||
                string.IsNullOrWhiteSpace(txtUpdatePackageType.Text) ||
                string.IsNullOrWhiteSpace(txtUpdatePackagePrice.Text))
            {
                updateMessage.Text = SecurityUtilities.SanitizeInput("All fields are required");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "keepModalOpen",
                    "$('#updateCustomerModal').modal('show');", true);
                return;
            }

            string updateQuery = @"UPDATE webapp.customers SET name = @Name, email = @Email, 
                phone = @Phone, address = @Address, package_type = @PackageType, 
                package_price = @PackagePrice WHERE customer_ID = @CustomerID";
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", SecurityUtilities.SanitizeInput(txtUpdateCustomerID.Text));
                        cmd.Parameters.AddWithValue("@Name", SecurityUtilities.SanitizeInput(txtUpdateCustomerName.Text));
                        cmd.Parameters.AddWithValue("@Email", SecurityUtilities.SanitizeInput(txtUpdateCustomerEmail.Text));
                        cmd.Parameters.AddWithValue("@Phone", SecurityUtilities.SanitizeInput(txtUpdateCustomerPhone.Text));
                        cmd.Parameters.AddWithValue("@Address", SecurityUtilities.SanitizeInput(txtUpdateCustomerAddress.Text));
                        cmd.Parameters.AddWithValue("@PackageType", SecurityUtilities.SanitizeInput(txtUpdatePackageType.Text));
                        cmd.Parameters.AddWithValue("@PackagePrice", SecurityUtilities.SanitizeInput(txtUpdatePackagePrice.Text));

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            PopulateCustomerTable();
                            updateMessage.Text = "";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                                "alert('Customer updated successfully!'); $('#updateCustomerModal').modal('hide');", true);
                        }
                        else
                        {
                            updateMessage.Text = SecurityUtilities.SanitizeInput("No customer found with this ID");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "keepModalOpen",
                                "$('#updateCustomerModal').modal('show');", true);
                        }
                    }
                }
            }
            catch
            {
                updateMessage.Text = SecurityUtilities.SanitizeInput("Error updating customer");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "keepModalOpen",
                    "$('#updateCustomerModal').modal('show');", true);
            }
        }

        // Securely removes customer data with parameter sanitization to prevent SQL injection
        protected void DeleteCustomerEventMethod(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDeleteCustomerID.Text))
            {
                deleteMessage.Text = SecurityUtilities.SanitizeInput("Customer ID is required");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "keepModalOpen",
                    "$('#deleteCustomerModal').modal('show');", true);
                return;
            }

            string delquery = "DELETE FROM webapp.customers WHERE customer_ID = @CustomerID";
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(delquery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", SecurityUtilities.SanitizeInput(txtDeleteCustomerID.Text));
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            PopulateCustomerTable();
                            deleteMessage.Text = "";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                                "alert('Customer deleted successfully!'); $('#deleteCustomerModal').modal('hide');", true);
                        }
                        else
                        {
                            deleteMessage.Text = SecurityUtilities.SanitizeInput("No customer found with this ID");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "keepModalOpen",
                                "$('#deleteCustomerModal').modal('show');", true);
                        }
                    }
                }
            }
            catch
            {
                deleteMessage.Text = SecurityUtilities.SanitizeInput("Error deleting customer");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "keepModalOpen",
                    "$('#deleteCustomerModal').modal('show');", true);
            }
        }

        // Retrieves and displays customer data with XSS protection for all displayed fields
        private void PopulateCustomerTable()
        {
            string query = "SELECT customer_ID, name, email, phone, address, package_type, package_price FROM webapp.customers";
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                    {
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dt = SecurityUtilities.SecureDataTable(dt);
                            CustomerGridView.DataSource = dt;
                            CustomerGridView.DataBind();
                            SecurityUtilities.SecureGridView(CustomerGridView);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error",
                    "alert('Error loading customer data.');", true);
            }
        }

        // Provides additional XSS protection during GridView data binding
        protected void CustomerGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Apply security encoding to each cell in the row
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cell.Text != "&nbsp;")
                    {
                        // Additional encoding layer for cell content
                        cell.Text = HttpUtility.HtmlEncode(HttpUtility.HtmlDecode(cell.Text));
                    }
                }
            }
        }
    }
}