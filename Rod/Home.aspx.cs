using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            if (Session["loggedIn"] == null)
            {
                HideIfUserLoggedIn.Visible = true;
                homeLoggedIn.Visible = false;
                
            }
            else
            {
                homeLoggedIn.Visible = true;
                HideIfUserLoggedIn.Visible = false;

                string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";

                SqlConnection con = new SqlConnection(cs);

                con.Open();

                string questionsQuery = @"SELECT TOP 40 *
                FROM [User]
                INNER JOIN [Post]
                ON [User].id = [Post].userId;";

                SqlCommand cmd = new SqlCommand(questionsQuery, con);

                SqlDataReader dr = cmd.ExecuteReader();
             
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                            int totalVotePost = Convert.ToInt32(dr.GetValue(32)) + Convert.ToInt32(dr.GetValue(33));
                            sectionQuestions.InnerHtml += " " +
                       " <div class='question'>" +
                         "<div class='votesAnswers'>" +

                          "<h3><span>" + totalVotePost.ToString() + "</span> التقييم</h3>" +
                        "<div class='answersContainer'>" +
                        "<h3><span>" + dr.GetValue(31).ToString() + "</span> الأجابات</h3>" +
                         "</div>  </div>" +


                       " <div class='questionTitle'>" +
                           "<a class='title' href='/question/" + dr.GetValue(24) + "'>" + dr.GetValue(26).ToString() + "</a> </div>" +

                       " <div class='usernameQuestionDetails'>" +
                        "<h2><span><a class='username' href='/users/profile/" +dr.GetValue(0).ToString() +"'>" + dr.GetValue(1).ToString() + "</a></span>   <span>" + dr.GetValue(15).ToString() + "</span></h2>" +
                       " <p>" + RelativeDate(Convert.ToDateTime(dr.GetValue(29)))  + "</p></div></div>";
                    }
                }
                con.Close();

            }
            }
        }

        protected void MonthFilter(object sender, EventArgs e)
        {
            sectionQuestions.InnerHtml = "";
            commonFilter.Style.Remove("border-top");
            weekFilter.Style.Remove("border-top");
            monthFilter.Style.Add("border-top", "3px solid #4F6BFF");

            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";

            SqlConnection con = new SqlConnection(cs);

            con.Open();

            string questionsQueryByMonth = "SELECT TOP 40 * FROM[User] INNER JOIN[Post] ON[User].id = [Post].userId where[Post].creationDate between '" + DateTime.UtcNow.Year.ToString() + "-" + DateTime.UtcNow.Month.ToString() + "-01'" + " and '" + DateTime.UtcNow.Year.ToString() + "-" + DateTime.UtcNow.Month.ToString() + "-30'";



            SqlCommand cmd = new SqlCommand(questionsQueryByMonth, con);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                   
                    sectionQuestions.InnerHtml += " " +
                   " <div class='question'>" +
                     "<div class='votesAnswers'>" +

                      "<h3><span>" + dr.GetValue(32).ToString() + "</span> التقييم</h3>" +
                    "<div class='answersContainer'>" +
                    "<h3><span>" + dr.GetValue(31).ToString() + "</span> الأجابات</h3>" +
                     "</div>  </div>" +


                   " <div class='questionTitle'>" +
                       "<a class='title' href='/question/" + dr.GetValue(24) + "'>" + dr.GetValue(26).ToString() + "</a> </div>" +

                   " <div class='usernameQuestionDetails'>" +
                    "<h2><span><a class='username' href='/users/profile/" + dr.GetValue(0).ToString() + "'>" + dr.GetValue(1).ToString() + "</a></span>   <span>" + dr.GetValue(15).ToString() + "</span></h2>" +
                   " <p>" + RelativeDate(Convert.ToDateTime(dr.GetValue(29))) + "</p></div></div>";
                }
            }
           

            con.Close();
        }

        protected void CommonFilter(object sender, EventArgs e)
        {
            sectionQuestions.InnerHtml = "";
            monthFilter.Style.Remove("border-top");
            weekFilter.Style.Remove("border-top");
            commonFilter.Style.Add("border-top", "3px solid #4F6BFF");

            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";

            SqlConnection con = new SqlConnection(cs);

            con.Open();

            string questionsQueryByCommon = "SELECT TOP 40 * FROM [User] INNER JOIN [Post] ON [User].id = [Post].userId ORDER BY [Post].upvoteCount DESC;";



            SqlCommand cmd = new SqlCommand(questionsQueryByCommon, con);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    
                    sectionQuestions.InnerHtml += " " +
                   " <div class='question'>" +
                     "<div class='votesAnswers'>" +

                      "<h3><span>" + dr.GetValue(32).ToString() + "</span> التقييم</h3>" +
                    "<div class='answersContainer'>" +
                    "<h3><span>" + dr.GetValue(31).ToString() + "</span> الأجابات</h3>" +
                     "</div>  </div>" +


                   " <div class='questionTitle'>" +
                       "<a class='title' href='/question/" + dr.GetValue(24) + "'>" + dr.GetValue(26).ToString() + "</a> </div>" +

                   " <div class='usernameQuestionDetails'>" +
                    "<h2><span><a class='username' href='/users/profile/" + dr.GetValue(0).ToString() + "'>" + dr.GetValue(1).ToString() + "</a></span>   <span>" + dr.GetValue(15).ToString() + "</span></h2>" +
                   " <p>" + RelativeDate(Convert.ToDateTime(dr.GetValue(29))) + "</p></div></div>";
                }
            }


            con.Close();

        }

        protected void WeekFilter(object sender, EventArgs e)
        {
            sectionQuestions.InnerHtml = "";
            monthFilter.Style.Remove("border-top");
            commonFilter.Style.Remove("border-top");
            weekFilter.Style.Add("border-top", "3px solid #4F6BFF");

            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";

            SqlConnection con = new SqlConnection(cs);

            con.Open();

            string questionsQueryByWeek = @"SET DATEFIRST 1 
        SELECT TOP 40 * from [User]
        inner join [Post] on[Post].userId = [User].id 
        where [Post].creationDate >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) 
        AND [Post].creationDate <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate()))
        order by [Post].creationDate DESC";



            SqlCommand cmd = new SqlCommand(questionsQueryByWeek, con);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    sectionQuestions.InnerHtml += " " +
                   " <div class='question'>" +
                     "<div class='votesAnswers'>" +

                      "<h3><span>" + dr.GetValue(32).ToString() + "</span> التقييم</h3>" +
                    "<div class='answersContainer'>" +
                    "<h3><span>" + dr.GetValue(31).ToString() + "</span> الأجابات</h3>" +
                     "</div>  </div>" +


                   " <div class='questionTitle'>" +
                       "<a class='title' href='/question/" + dr.GetValue(24) + "'>" + dr.GetValue(26).ToString() + "</a> </div>" +

                   " <div class='usernameQuestionDetails'>" +
                    "<h2><span><a class='username' href='/users/profile/" + dr.GetValue(0).ToString() + "'>" + dr.GetValue(1).ToString() + "</a></span>   <span>" + dr.GetValue(15).ToString() + "</span></h2>" +
                   " <p>" + RelativeDate(Convert.ToDateTime(dr.GetValue(29))) + "</p></div></div>";
                }
            }


            con.Close();

        }
    }
}