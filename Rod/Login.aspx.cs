using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Rod
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["loggedIn"] != null)
            {
                Response.Redirect("~/Home.aspx");
            }
        }
        protected void Login_Click(object sender, EventArgs e)
        {
            string username = usernameTxt.Text;
            string password = passwordTxt.Text;

            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
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
                    break;
                }
                Session["username"] = username;
                Session["loggedIn"] = "true";
                Response.Redirect("~/Home.aspx");
            }
            else
            {
                invalidCredentials.Text = "اسم المستخدم او كلمة المرور غير صحيحة";
            }



        }
    }
}