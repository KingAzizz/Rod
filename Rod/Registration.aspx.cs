using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Net.Mail;
using System.Configuration;

namespace Rod
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = string.Format("التسجيل");
        }
        public static string hashPassword(string password)
        {
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();
            byte[] password_bytes = Encoding.ASCII.GetBytes(password);
            byte[] encrypted_bytes = SHA1.ComputeHash(password_bytes);
            return Convert.ToBase64String(encrypted_bytes);
        }
        protected void Reg(object sender, EventArgs e)
        {
           
            string cs = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                try
                {
                    connection.Open();
                    var sql = "INSERT INTO [User](username, email , password,creationDate) VALUES(@username, @email , @password,@creationDate)";
                    using (var cmd = new SqlCommand(sql, connection))

                    {
                        cmd.Parameters.AddWithValue("@username", usernameTxt.Text);
                        cmd.Parameters.AddWithValue("@email", EmailTxt.Text);
                        cmd.Parameters.AddWithValue("@password", hashPassword(passwordTxt.Text));
                        cmd.Parameters.AddWithValue("@creationDate", DateTime.Now);

                        cmd.ExecuteNonQuery();

                        connection.Close();
                    }

                      Response.Redirect("Login.aspx");
                }

              catch (Exception ee)
                {
                    badReg.Visible = true;
                }
                   
            }
        }
    }
}