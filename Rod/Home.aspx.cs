using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Rod
{
    public partial class Home : System.Web.UI.Page
    {
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
                return months <= 1 ? " قبل شهر واحد " : " قبل " + months + " شهور  ";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "قبل سنة" : " قبل " + years + " سنوات  ";
            }
        }
        public string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\Rodgit\Rod\App_Data\Rod.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            if (Session["id"] == null)
            {
                HideIfUserLoggedIn.Visible = true;
                homeLoggedIn.Visible = false;
                  
                
            }
            else
            {
                homeLoggedIn.Visible = true;
                HideIfUserLoggedIn.Visible = false;

                    if(Request.QueryString["tab"] != null)
                    {
                        Bind(Request.QueryString["tab"]);
                    }
                    else
                    {
                        Bind("default");
                    }
            

            }
            }
        }

        public void Bind(string tab)
        {
                SqlConnection con = new SqlConnection(cs);
            if (tab == "default")
            {
                con.Open();

                string questionsQuery = @"SELECT TOP 40 *,[User].id as idUser,[Post].id as questionId,[Post].title as questionTitle,[Post].creationDate as postCreationDate ,CONVERT(int ,upvoteCount) + CONVERT(int ,downvoteCount) as totalVote
                FROM [User]
                INNER JOIN [Post]
                ON [User].id = [Post].userId;";
                SqlCommand cmd = new SqlCommand(questionsQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Post");
                HomePageListView.DataSource = ds.Tables[0];
                HomePageListView.DataBind();
                con.Close();
            }
            if (tab == "Month")
            {
                commonFilter.Style.Remove("border-top");
                weekFilter.Style.Remove("border-top");
                monthFilter.Style.Add("border-top", "3px solid #4F6BFF");
                con.Open();
                string questionsQueryByMonth = @"SELECT TOP 40 *,[User].id as idUser,[Post].id as questionId,[Post].title as questionTitle,[Post].creationDate as postCreationDate ,CONVERT(int ,upvoteCount) + CONVERT(int ,downvoteCount) as totalVote FROM[User] INNER JOIN[Post] ON[User].id = [Post].userId where[Post].creationDate between '" + DateTime.UtcNow.Year.ToString() + "-" + DateTime.UtcNow.Month.ToString() + "-01'" + " and '" + DateTime.UtcNow.Year.ToString() + "-" + DateTime.UtcNow.Month.ToString() + "-30'";
                SqlCommand cmd = new SqlCommand(questionsQueryByMonth, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Post");
                HomePageListView.DataSource = ds.Tables[0];
                HomePageListView.DataBind();


                con.Close();
            }
            if(tab == "Common")
            {
                monthFilter.Style.Remove("border-top");
                weekFilter.Style.Remove("border-top");
                commonFilter.Style.Add("border-top", "3px solid #4F6BFF");
                con.Open();
                string questionsQueryByCommon = @"SELECT TOP 40 *,[User].id as idUser,[Post].id as questionId,[Post].title as questionTitle,[Post].creationDate as postCreationDate ,CONVERT(int ,upvoteCount) + CONVERT(int ,downvoteCount) as totalVote FROM [User] INNER JOIN [Post] ON [User].id = [Post].userId ORDER BY [Post].upvoteCount DESC;";
                SqlCommand cmd = new SqlCommand(questionsQueryByCommon, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Post");
                HomePageListView.DataSource = ds.Tables[0];
                HomePageListView.DataBind();

                con.Close();
            }

            if(tab == "Week")
            {
                monthFilter.Style.Remove("border-top");
                commonFilter.Style.Remove("border-top");
                weekFilter.Style.Add("border-top", "3px solid #4F6BFF");

                con.Open();

                string questionsQueryByWeek = @"SET DATEFIRST 1 
                SELECT TOP 40 *,[User].id as idUser,[Post].id as questionId,[Post].title as questionTitle,[Post].creationDate as postCreationDate ,CONVERT(int ,upvoteCount) + CONVERT(int ,downvoteCount) as totalVote from [User]
                inner join [Post] on[Post].userId = [User].id 
                where [Post].creationDate >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) 
                AND [Post].creationDate <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate()))
                order by [Post].creationDate DESC";
                SqlCommand cmd = new SqlCommand(questionsQueryByWeek, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Post");
                HomePageListView.DataSource = ds.Tables[0];
                HomePageListView.DataBind();

                con.Close();
            }
            

        }

        protected void MonthFilter(object sender, EventArgs e)
        {
            Response.Redirect("/?tab=Month");
        }

        protected void CommonFilter(object sender, EventArgs e)
        {

            Response.Redirect("/?tab=Common");
        }

        protected void WeekFilter(object sender, EventArgs e)
        {
            Response.Redirect("/?tab=Week");
        }
    }
}