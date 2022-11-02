using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Text;

namespace Rod
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                Response.Redirect("~/");
            }
            if (!Page.IsPostBack)
            {

                if (Request.Cookies["userCredentials"] != null)
                {
                    usernameTxt.Text = Request.Cookies["userCredentials"]["username"];

                    rememberMe.Checked = true;
                }
            }
        }
        public static string hashPassword(string password)
        {
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();
            byte[] password_bytes = Encoding.ASCII.GetBytes(password);
            byte[] encrypted_bytes = SHA1.ComputeHash(password_bytes);
            return Convert.ToBase64String(encrypted_bytes);
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            string username = usernameTxt.Text;
            string password = hashPassword(passwordTxt.Text);

            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pc\Documents\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string loginQuery = @"select * from [User] where [username] = '" + username + "' and CONVERT(VARCHAR,[password]) = '" + password + "'";

            SqlCommand cmd = new SqlCommand(loginQuery, con);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Session["id"] = dr.GetValue(0);
                    if (rememberMe.Checked)
                    {
                        HttpCookie cookie = new HttpCookie("userCredentials");
                        cookie.Values.Add("username", dr.GetValue(1).ToString());
                        cookie.Expires = DateTime.Now.AddDays(15);
                        Response.Cookies.Add(cookie);
                    }
                    break;
                }
                Response.Redirect("~/");
            }
            else
            {
                invalidCredentials.Text = "اسم المستخدم او كلمة المرور غير صحيحة";
            }



        }
    }
}