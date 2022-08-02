using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rod
{
    public partial class Questions : System.Web.UI.Page
    {
        public static string RelativeDate(DateTime theDate)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - theDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? " قبل ثانية " : " قبل " + ts.Seconds + " ثانية ";

            if (delta < 2 * MINUTE)
                return " قبل دقيقة ";

            if (delta < 45 * MINUTE)
                return " قبل " + ts.Minutes + " دقايق  ";

            if (delta < 90 * MINUTE)
                return "قبل ساعة";

            if (delta < 24 * HOUR)
                return " قبل " + ts.Hours + " ساعات ";

            if (delta < 48 * HOUR)
                return "امس";

            if (delta < 30 * DAY)
                return "قبل " + ts.Days + " يوم ";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? " شهر واحد " : " قبل " + months + " شهور  ";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "قبل سنة" : " قبل " + years + " سنوات  ";
            }
        }
        public string Truncate(string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            
                this.Bind();
            HiddenField questionCount = questionsListView.Items[0].FindControl("questionsCount") as HiddenField;
                totalQuestion.InnerText = "[" + questionCount.Value + "] سؤال";
            }
        }

        public void Bind()
        {
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
              string questionsQuery = @"select [Post].id,[Post].title,tag,[Post].creationDate,answerCount,
                CONVERT(int ,upvoteCount) + CONVERT(int ,downvoteCount) as totalVote ,username,reputation,
                (select COUNT(id) from Post) as totalQuestions
                from Post
                inner join [User] on [User].id = [Post].userId
                group by  [Post].id,[Post].title,tag,[Post].creationDate,answerCount,
                upvoteCount ,downvoteCount ,username,reputation
                order by [Post].creationDate DESC";
            SqlCommand cmdQuestion = new SqlCommand(questionsQuery, con);
            SqlDataAdapter daQuestion = new SqlDataAdapter(cmdQuestion);

            DataSet dsQuestion = new DataSet();
            daQuestion.Fill(dsQuestion, "Post");
            questionsListView.DataSource = dsQuestion.Tables[0];
            questionsListView.DataBind();
            /*if (askListView.Items.Count == 0)
            {
                questionsDivD.Visible = false;
            }*/
            con.Close();

        }
        protected void questionsListView_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            (questionsListView.FindControl("DataPager") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            this.Bind();
        }
    }
}