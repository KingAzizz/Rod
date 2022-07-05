using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Rod
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
   
            if(Session["loggedIn"] == null)
            {
                profileImage.Visible = false;
             
                inboxLink.Text = "تسجيل";
                inboxLink.NavigateUrl = "Registration.aspx";
                inboxLink.Style.Add("color", "#4F6BFF");

                profileImageLink.Text = "دخول";
                profileImageLink.NavigateUrl = "Login.aspx";
                profileImageLink.Style.Add("color", "#4F6BFF");


            }
            else {

                string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                string getImage = @"select [profileImage] from [User] where [username] = '" + Session["username"].ToString() +"'" ;

                SqlCommand cmd = new SqlCommand(getImage, con);

                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows){
                    while (dr.Read())
                    {
                        profileImage.ImageUrl = dr.GetValue(0).ToString();
                    }
                }

                }
            }
        }
        protected void SearchTrigger(object sender, EventArgs e)
        {
            Response.Redirect("SearchResult.aspx?searched=" + searchText.Text);
        }
    }
}