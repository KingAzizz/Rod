using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Rod
{
    public partial class Tagged : System.Web.UI.Page
    {
        public static string cs = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Page.RouteData.Values["id"];

            int num = -1;
            if (id != null)
            {
                if (!int.TryParse(id.ToString(), out num))
                {
                    Response.Redirect("~/");


                }
            }

            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string TagsQuestion = @"select [User].id as UserId,[User].username,[User].profileImage,
            [Post].id as PostId, [Post].title, [Post].creationDate
            from Post 
            join [User] on [Post].userId = [User].id 
            join [PostTags] on [Post].id =[PostTags].postId
            where tagId =@tagId";

            SqlCommand cmd = new SqlCommand(TagsQuestion, con);
            cmd.Parameters.AddWithValue("@tagId", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}