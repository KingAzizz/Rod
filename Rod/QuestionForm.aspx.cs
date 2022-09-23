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
        public static string cs = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void submitQuestion_Click(object sender, EventArgs e)
        {
            string title = titletxt.Text;
            string subject = subjecttxt.Text;
            string section = sectiontxt.Text;

            SqlConnection con = new SqlConnection(cs);
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
        }
    }
}