using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rod
{
    public partial class EditProfile : System.Web.UI.Page
    {
        public static string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pc\Documents\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) { 
            var id = Page.RouteData.Values["id"];

            int num = -1;
            if (id != null)
            {
                if (!int.TryParse(id.ToString(), out num))
                {
                    Response.Redirect("~/");


                }
                if(Session["id"] != null)
                {
                   
                    SqlConnection con = new SqlConnection(cs);
                    con.Open();

                    string editProfileQuery = @"select [id],[displayName],[title],[aboutMe],[education],[location],[websiteUrl],[twitterUrl],[githubUrl],[profileImage]
                      from [User] where id = @id";

                    SqlCommand cmd = new SqlCommand(editProfileQuery, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if(dr.GetValue(0).ToString() != Session["id"].ToString())
                            {
                                Response.Redirect("~/");
                            }
                            displayNameInput.Text =dr.GetValue(1).ToString();
                            titleInput.Text = dr.GetValue(2).ToString();
                            aboutInput.Text = dr.GetValue(3).ToString();
                            educationInput.Text = dr.GetValue(4).ToString();
                            locationInput.Text = dr.GetValue(5).ToString();
                            websiteInput.Text = dr.GetValue(6).ToString();
                            twitterInput.Text = dr.GetValue(7).ToString();
                            githubInput.Text = dr.GetValue(8).ToString();
                            profileImageEdit.ImageUrl = "~/" + dr.GetValue(9).ToString();
                        }
                    }
                    con.Close();
                    }
                }
            }
        }

        protected void SaveEdit(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
          
           
            if (imageUpload.HasFile)
            {
                con.Open();
                imageUpload.SaveAs(Server.MapPath("~/uploads/") + System.IO.Path.GetFileName(imageUpload.FileName));
                string linkedPath = "uploads/" + System.IO.Path.GetFileName(imageUpload.FileName);

                string updateProfile = @"update [User]
             set displayName = @displayName, title = @title,aboutMe =@aboutMe ,education = @education,
             [location] = @location, websiteUrl = @websiteUrl,twitterUrl = @twitterUrl,githubUrl = @githubUrl,profileImage=@profileImage
             where id = @userId;";
                SqlCommand cmd = new SqlCommand(updateProfile, con);
                cmd.Parameters.AddWithValue("@displayName", displayNameInput.Text);
                cmd.Parameters.AddWithValue("@title", titleInput.Text);
                cmd.Parameters.AddWithValue("@aboutMe", aboutInput.Text);
                cmd.Parameters.AddWithValue("@education", educationInput.Text);
                cmd.Parameters.AddWithValue("@location", locationInput.Text);
                cmd.Parameters.AddWithValue("@websiteUrl", websiteInput.Text);
                cmd.Parameters.AddWithValue("@twitterUrl", twitterInput.Text);
                cmd.Parameters.AddWithValue("@githubUrl",  githubInput.Text);
                cmd.Parameters.AddWithValue("@profileImage", linkedPath);
                cmd.Parameters.AddWithValue("@userId", Session["id"]);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                con.Open();
                string updateProfile = @"update [User]
             set displayName = @displayName, title = @title,aboutMe = @aboutMe ,education = @education,
             [location] = @location, websiteUrl = @websiteUrl,twitterUrl = @twitterUrl,githubUrl = @githubUrl
             where id = @userId;";
                SqlCommand cmd = new SqlCommand(updateProfile, con);
                cmd.Parameters.AddWithValue("@displayName", displayNameInput.Text);
                cmd.Parameters.AddWithValue("@title", titleInput.Text);
                cmd.Parameters.AddWithValue("@aboutMe", aboutInput.Text);
                cmd.Parameters.AddWithValue("@education", educationInput.Text);
                cmd.Parameters.AddWithValue("@location", locationInput.Text);
                cmd.Parameters.AddWithValue("@websiteUrl", websiteInput.Text);
                cmd.Parameters.AddWithValue("@twitterUrl", twitterInput.Text);
                cmd.Parameters.AddWithValue("@githubUrl", githubInput.Text);
                cmd.Parameters.AddWithValue("@userId", Session["id"]);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            Response.Redirect("~/profile");
        }

        protected void CancelEdit(object sender, EventArgs e)
        {
            Response.Redirect("~/profile");
        }
    }
}