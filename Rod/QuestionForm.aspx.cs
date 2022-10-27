using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Rod
{
    public partial class QuestionForm : System.Web.UI.Page
    {
        public static string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\Rodgit\Rod\App_Data\Rod.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e) { }

        protected void submitQuestion_Click(object sender, EventArgs e)
        {
            if(Session["id"] != null)
            {
               
          
            string title = titletxt.Text;
            string subject = subjecttxt.Text;
            string section = sectiontxt.Text;

            SqlConnection con = new SqlConnection(cs);




            con.Open();

            string findTags = @"select id from tagInfo
                    where tagName = N'" + section + "'";

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

                string insertQuestion = @"insert into [Post] ([userId],[title],[body],[tag],[creationDate])
            values(@userId, @title, @body, @tag, @creationDate)";
                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                SqlCommand cmd = new SqlCommand(insertQuestion, con);

                cmd.Parameters.AddWithValue("@userId", Session["id"]);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@body", subject);
                cmd.Parameters.AddWithValue("@tag", section);
                cmd.Parameters.AddWithValue("@creationDate", sqlFormattedDate);

                cmd.ExecuteNonQuery();
                con.Close();
                    con.Open();
                    string insertTags = @"insert into [PostTags] ([postId],[tagId])
                        values(IDENT_CURRENT('Post') - 1 + 1,@tagId);";

                    SqlCommand insertTagsCmd = new SqlCommand(insertTags, con);
                    insertTagsCmd.Parameters.AddWithValue("@tagId", tagId);
                    insertTagsCmd.ExecuteNonQuery();
                    con.Close();
                    con.Open();
                SqlCommand redirectQuestionCMD = new SqlCommand("select IDENT_CURRENT('Post')", con);

                SqlDataReader dr = redirectQuestionCMD.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Response.Redirect("~/question/" + dr.GetValue(0));
                    }
                }
                con.Close();
                }
                else
                {
                    Response.Write("nope");
                }
            }




        }
    }
}