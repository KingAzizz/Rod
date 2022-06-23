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
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
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

                string cs = @"Server=tcp:roddatabase.database.windows.net,1433;Initial Catalog=Rod;Persist Security Info=False;User ID=rodADMIN;Password=rodipa2022@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                SqlConnection con = new SqlConnection(cs);

                con.Open();

                string viewdata = @"SELECT TOP 40 *
                FROM [User]
                INNER JOIN [Post]
                ON [User].id = [Post].userId;";

                SqlCommand cmd = new SqlCommand(viewdata, con);

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
                           " <p>" + dr.GetValue(26).ToString() + "</p> </div>" +

                       " <div class='usernameQuestionDetails'>" +
                        "<h2><span>" + dr.GetValue(1).ToString() + "</span>   <span>" + dr.GetValue(15).ToString() + "</span></h2>" +
                       " <p>" + RelativeDate(Convert.ToDateTime(dr.GetValue(29)))  + "</p></div></div>";
                    }
                }
                con.Close();

            }
        }

        protected void MonthFilter(object sender, EventArgs e)
        {
            sectionQuestions.InnerHtml = "";

            monthFilter.Style.Add("border-top", "6px solid #4F6BFF");

            string cs = @"Server=tcp:roddatabase.database.windows.net,1433;Initial Catalog=Rod;Persist Security Info=False;User ID=rodADMIN;Password=rodipa2022@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            SqlConnection con = new SqlConnection(cs);

            con.Open();

            string viewByMonth = "SELECT TOP 40 * FROM[User] INNER JOIN[Post] ON[User].id = [Post].userId where[Post].creationDate between '" + DateTime.UtcNow.Year.ToString() + "-" + DateTime.UtcNow.Month.ToString() + "-01'" + " and '" + DateTime.UtcNow.Year.ToString() + "-" + DateTime.UtcNow.Month.ToString() + "-30'";



            SqlCommand cmd = new SqlCommand(viewByMonth, con);

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
                       " <p>" + dr.GetValue(26).ToString() + "</p> </div>" +

                   " <div class='usernameQuestionDetails'>" +
                    "<h2><span>" + dr.GetValue(1).ToString() + "</span>   <span>" + dr.GetValue(15).ToString() + "</span></h2>" +
                   " <p>" + RelativeDate(Convert.ToDateTime(dr.GetValue(29))) + "</p></div></div>";
                }
            }
           

            con.Close();
        }
    }
}