using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Rod
{
    public partial class EditQuestion : System.Web.UI.Page
    {
        public static string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USER\source\repos\Rod\Rod\App_Data\Database1.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            
            var id = Page.RouteData.Values["id"];
            SqlConnection con = new SqlConnection(cs);

            
            int num = -1;
            if (id != null)
            {
                if (!int.TryParse(id.ToString(), out num))
                {
                    Response.Redirect("~/");


                }

                con.Open();
                string selectEditedQuestion = @"select [id],[userId],[title],[body],[tag]
                    from [Post]
                    where id =" + id.ToString();
                SqlCommand cmd = new SqlCommand(selectEditedQuestion, con);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        titleText.Text = dr.GetValue(2).ToString();
                        bodyText.Text = dr.GetValue(3).ToString();
                        if(Session["id"].ToString() != dr.GetValue(1).ToString())
                        {
                            Response.Redirect("~/");
                        }
                    }
                    Bind();
                }

            }
        }
        public void Bind()
        {
            var id = Page.RouteData.Values["id"];

            int num = -1;
            if (id != null)
            {
                if (!int.TryParse(id.ToString(), out num))
                {
                    Response.Redirect("~/");


                }
                SqlConnection con = new SqlConnection(cs);
                string answersQuery = @"SELECT  [TagInfo].id,tagName

                    FROM[PostTags]
                     join [TagInfo] on [TagInfo].id = [PostTags].tagId
                    JOIN[Post] on [Post].id = [PostTags].postId
                    where[Post].id =" + id.ToString();
                SqlCommand cmd = new SqlCommand(answersQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "PostTags");
                Tags.DataSource = ds.Tables[0];
                Tags.DataBind();
            }

        }
        protected void Tags_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if(e.CommandName == "DeleteTag")
            {
                if(Session["id"] != null)
                {
                    SqlConnection con = new SqlConnection(cs);
                    var id = Page.RouteData.Values["id"];

                    con.Open();

                    string deleteTag = @"delete from [PostTags]
                    where [postId] = @postId and [tagId] = @tagId;
                    UPDATE [Post] SET tag = REPLACE(tag, N',@tagName', '');
                    UPDATE [Post] SET tag = REPLACE(tag, N'@tagName,', '');
                    UPDATE [Post] SET tag = REPLACE(tag, N'@tagName', '');";
                    SqlCommand cmd = new SqlCommand(deleteTag, con);
                    HiddenField tagNameHidden = e.Item.FindControl("tagNameHidden") as HiddenField;
                    HtmlGenericControl tags = e.Item.FindControl("existingTags") as HtmlGenericControl;
                    HtmlGenericControl deleteButton = e.Item.FindControl("deleteButton") as HtmlGenericControl;

                 
                    cmd.Parameters.AddWithValue("@postId", id.ToString());
                    cmd.Parameters.AddWithValue("@tagId", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@tagName", tagNameHidden.Value);
                    cmd.ExecuteNonQuery();
                    tags.Visible = false;
                    deleteButton.Visible= false;
                    con.Close();
                }
            }

        }

            protected void SaveChanges(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            var id = Page.RouteData.Values["id"];
            int num = -1;
            if (id != null)
            {
                if (!int.TryParse(id.ToString(), out num))
                {
                    Response.Redirect("~/");


                }
                string str = tagText.Text;
                String[] spearator = { "," };
            

                // using the method
                String[] strlist = str.Split(spearator,
                       StringSplitOptions.RemoveEmptyEntries);

                
                foreach (String s in strlist)
                {
                    con.Open();
                    
                 
                    string findTags = @"select id from tagInfo
                    where tagName = N'" + s + "'";

                    SqlCommand findTagsCmd = new SqlCommand(findTags, con);
                    SqlDataReader findTagsDr = findTagsCmd.ExecuteReader();
                    if (findTagsDr.HasRows)
                    {
                        string tagId = "";
                        while (findTagsDr.Read())
                        {
                            
                           tagId = findTagsDr.GetValue(0).ToString();
                            break;
                         
                        }
                        con.Close();
                        con.Open();
                        string insertTags = @"insert into [PostTags] ([postId],[tagId])
                        values(@postId,@tagId);";
                       
                        SqlCommand insertTagsCmd = new SqlCommand(insertTags, con);
                        insertTagsCmd.Parameters.AddWithValue("@postId", id);
                        insertTagsCmd.Parameters.AddWithValue("@tagId", tagId);
                        insertTagsCmd.ExecuteNonQuery();
                        con.Close();

                    }
                    else
                    {
                        Response.Write("invaild tag");
                    }


                }
                con.Close();
                con.Open();
                
                string editInsert = @"update [Post] 
                set [title] = @title,
                [body] = @body,
                [tag] = @tag
                where id =" + id.ToString();
                SqlCommand cmd = new SqlCommand(editInsert, con);
                cmd.Parameters.AddWithValue("@title", titleText.Text);
                cmd.Parameters.AddWithValue("@body", bodyText.Text);
                cmd.Parameters.AddWithValue("@tag", tagText.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/question/" + id.ToString());
            }
        }

        protected void CancelChanges(object sender, EventArgs e)
        {
            var id = Page.RouteData.Values["id"];
            int num = -1;
            if (id != null)
            {
                if (!int.TryParse(id.ToString(), out num))
                {
                    Response.Redirect("~/");


                }
                Response.Redirect("~/question/" + id.ToString());
            }
               
        }
    }
}