using System;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

namespace prij_test_newagain
{
   
    /// Central security class that prevents XSS attacks by encoding potentially malicious content before storage or display
   
    public static class SecurityUtilities
    {
        // Converts dangerous characters like < > " ' into their safe HTML entity equivalents to prevent script execution
        public static string SanitizeInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return HttpUtility.HtmlEncode(input);
        }

        // Protects against XSS in GridView displays by encoding all cell contents before they are rendered to the user
        public static void SecureGridView(GridView gridView)
        {
            if (gridView == null || gridView.Rows == null)
                return;

            foreach (GridViewRow row in gridView.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    if (cell.Text != "&nbsp;")
                    {
                        cell.Text = HttpUtility.HtmlEncode(HttpUtility.HtmlDecode(cell.Text));
                    }
                }
            }
        }

        // Prevents stored XSS attacks by encoding all data retrieved from the database before it reaches the presentation layer
        public static DataTable SecureDataTable(DataTable dt)
        {
            if (dt == null)
                return null;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    if (row[col] != DBNull.Value)
                    {
                        row[col] = HttpUtility.HtmlEncode(row[col].ToString());
                    }
                }
            }
            return dt;
        }
    }
}