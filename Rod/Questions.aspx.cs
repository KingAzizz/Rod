using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Rod
{
    public partial class Questions : System.Web.UI.Page
    {

        public static string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\Rodgit\Rod\App_Data\Rod.mdf;Integrated Security=True";

        public static string RelativeDate(DateTime theDate)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - theDate.Ticks);
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
                if(Request.QueryString["tab"] == null)
                {
                    Bind("default");
                }
                else {
                    Bind(Request.QueryString["tab"].ToString());  
                }
            HiddenField questionCount = questionsListView.Items[0].FindControl("questionsCount") as HiddenField;
                totalQuestion.InnerText = "[" + questionCount.Value + "] سؤال";
            }
        }

        public void Bind(string tab)
        {
            SqlConnection con = new SqlConnection(cs);

            if(tab == "default")
            {
                con.Open();
                string questionsQuery = @"select [Post].id,[Post].title,tag,[Post].creationDate,answerCount,
                CONVERT(int ,upvoteCount) + CONVERT(int ,downvoteCount) as totalVote ,[User].id as userId,username,reputation,
                (select COUNT(id) from Post) as totalQuestions
                from Post
                inner join [User] on [User].id = [Post].userId
                group by  [Post].id,[Post].title,tag,[Post].creationDate,answerCount,
                upvoteCount ,downvoteCount ,[User].id,username,reputation
                order by [Post].creationDate DESC";
                SqlCommand cmd = new SqlCommand(questionsQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Post");
                questionsListView.DataSource = ds.Tables[0];
                questionsListView.DataBind();
                /*if (askListView.Items.Count == 0)
                {
                    questionsDivD.Visible = false;
                }*/
                con.Close();
            }
            else if(tab == "Month")
            {
                con.Open();
                string questionsQueryByMonth = @"select [Post].id,[Post].title,tag,[Post].creationDate,answerCount,
                CONVERT(int ,upvoteCount) + CONVERT(int ,downvoteCount) as totalVote,[User].id as userId,username,reputation,
                (select COUNT(id) from Post) as totalQuestions
                from Post
                inner join [User] on [User].id = [Post].userId
                where [Post].creationDate between '" + DateTime.UtcNow.Year.ToString() + "-" + DateTime.UtcNow.Month.ToString() + "-01'" + " and '" + DateTime.UtcNow.Year.ToString() + "-" + DateTime.UtcNow.Month.ToString() + "-30'" +
                @"
                group by  [Post].id,[Post].title,tag,[Post].creationDate,answerCount,
                upvoteCount ,downvoteCount ,[User].id,username,reputation
                order by [Post].creationDate DESC;";
                SqlCommand cmd = new SqlCommand(questionsQueryByMonth, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Post");
                questionsListView.DataSource = ds.Tables[0];
                questionsListView.DataBind();
                /*if (askListView.Items.Count == 0)
                {
                    questionsDivD.Visible = false;
                }*/
                con.Close();
            }
            else if (tab == "Common")
            {
                     con.Open();
                string questionsQuery = @"select[Post].id,[Post].title,tag,[Post].creationDate,answerCount,
                CONVERT(int, upvoteCount) + CONVERT(int, downvoteCount) as totalVote,[User].id as userId,username,reputation,
                (select COUNT(id) from Post) as totalQuestions
                from Post
                inner join [User] on [User].id = [Post].userId
                group by  [Post].id,[Post].title,tag,[Post].creationDate,answerCount,
                upvoteCount ,downvoteCount,[User].id,username,reputation
                order by[Post].upvoteCount DESC,[Post].answerCount DESC";
                SqlCommand cmd = new SqlCommand(questionsQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Post");
                questionsListView.DataSource = ds.Tables[0];
                questionsListView.DataBind();
                /*if (askListView.Items.Count == 0)
                {
                    questionsDivD.Visible = false;
                }*/
                con.Close();
            }
            else if(tab == "unAnswerd")
            {
                con.Open();
                string questionsQuery = @"select[Post].id,[Post].title,tag,[Post].creationDate,answerCount,
                CONVERT(int, upvoteCount) + CONVERT(int, downvoteCount) as totalVote,[User].id as userId,username,reputation,
                (select COUNT(id) from Post) as totalQuestions
                from Post
                inner join [User] on [User].id = [Post].userId
                where answerCount = 0
                group by  [Post].id,[Post].title,tag,[Post].creationDate,answerCount,
                upvoteCount ,downvoteCount ,[User].id,username,reputation
                order by[Post].creationDate DESC";
                SqlCommand cmd = new SqlCommand(questionsQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Post");
                questionsListView.DataSource = ds.Tables[0];
                questionsListView.DataBind();
                /*if (askListView.Items.Count == 0)
                {
                    questionsDivD.Visible = false;
                }*/
                con.Close();
            }
            else
            {
                Response.Redirect("~/questions");
            }
         

        }
        protected void questionsListView_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            (questionsListView.FindControl("DataPager") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            if(Request.QueryString["tab"] == null)
            {
            Bind("default");
            }
            else
            {
                Bind(Request.QueryString["tab"].ToString());
            }
        }

        protected void questionsListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HtmlGenericControl tagsDiv = e.Item.FindControl("tags") as HtmlGenericControl;
            HiddenField questionId = e.Item.FindControl("questionId") as HiddenField;
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string queryTags = @"select tagId,tagName from PostTags
            inner join TagInfo on tagId = TagInfo.id
            where postId = @postId";
            SqlCommand cmd = new SqlCommand(queryTags, con);
            cmd.Parameters.AddWithValue("@postId", questionId.Value);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tagsDiv.InnerHtml = "<span class='tagplaceholder'> <a href='#" + dr.GetValue(0) +"'>" + dr.GetValue(1).ToString() + "</a></span>";
                }
            }
            con.Close();
        }

        protected void MonthFilter(object sender, EventArgs e)
        {
            Response.Redirect("~/questions?tab=Month");
        }

        protected void CommonFilter(object sender, EventArgs e)
        {
            Response.Redirect("~/questions?tab=Common");
        }
        protected void UnanswerdFilter(object sender, EventArgs e)
        {
            Response.Redirect("~/questions?tab=unAnswerd");
        }
    }
}