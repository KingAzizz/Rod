using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
namespace Rod
{
    public partial class SearchResult : System.Web.UI.Page
    {
        public static string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";

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
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return " قبل " + ts.Minutes + " دقايق  ";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return " قبل " + ts.Hours + " ساعات ";

            if (delta < 48 * HOUR)
                return "yesterday";

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
                searchedItemText.InnerText = " النتائج " +  "[" + Request.QueryString["searched"].ToString() +"]";

                SqlConnection con = new SqlConnection(cs);

                con.Open();
                
                string SearchResult = @"SELECT COUNT(*) OVER(),[User].username,[User].reputation,[Post].id,[Post].title,[Post].creationDate,[Post].upvoteCount,[Post].answerCount,[User].id
                    FROM [User]
                    INNER JOIN [Post]
                    ON [User].id = [Post].userId
                    where [Post].title LIKE N'%" + Request.QueryString["searched"].ToString() + "%'";
               
                SqlCommand cmd = new SqlCommand(SearchResult, con);

                SqlDataReader dr = cmd.ExecuteReader();

                bool count = false;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if(count == false)
                        {
                            resultCount.InnerText = "["+dr.GetValue(0).ToString()+"]" + " النتائج";
                        }
                        count = true;
                       
                        searchResult.InnerHtml +=  " " +
                       " <div class='question'>" +
                         "<div class='votesAnswers'>" +

                          "<h3><span>" + dr.GetValue(6).ToString() + "</span> التقييم</h3>" +
                        "<div class='answersContainer'>" +
                        "<h3><span>" + dr.GetValue(7).ToString() + "</span> الأجابات</h3>" +
                         "</div>  </div>" +


                       " <div class='questionTitle'>" +
                           "<a style='color: #0173CC;' href='/question/" + dr.GetValue(3) + "'>" + dr.GetValue(4).ToString() + "</a> </div>" +

                       " <div class='usernameQuestionDetails'>" +
                        "<h2><span><a href='/users/profile/" + dr.GetValue(8).ToString() + "'>" + dr.GetValue(1).ToString() + "</a></span>   <span>" + dr.GetValue(2).ToString() + "</span></h2>" +
                       " <p>" + RelativeDate(Convert.ToDateTime(dr.GetValue(5))) + "</p></div></div>";
                    }
                }
                con.Close();
            }
        }

        protected void HighRatingFilter(object sender, EventArgs e)
        {
            searchResult.InnerHtml = "";
            searchedItemText.InnerText = " النتائج " + "[" + Request.QueryString["searched"].ToString() + "]";

            SqlConnection con = new SqlConnection(cs);

            con.Open();
            string SearchResultByUpvote = @"SELECT COUNT(*) OVER(),[User].username,[User].reputation,[Post].id,[Post].title,[Post].creationDate,[Post].upvoteCount,[Post].answerCount,[User].id
                    FROM [User]
                    INNER JOIN [Post]
                    ON [User].id = [Post].userId
                    where [Post].title LIKE N'%" + Request.QueryString["searched"].ToString() + "%' order by [Post].upvoteCount DESC;";

            SqlCommand cmd = new SqlCommand(SearchResultByUpvote, con);

            SqlDataReader dr = cmd.ExecuteReader();

            bool count = false;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (count == false)
                    {
                        resultCount.InnerText = "[" + dr.GetValue(0).ToString() + "]" + " النتائج";
                    }
                    count = true;

                    searchResult.InnerHtml += " " +
                    " <div class='question'>" +
                      "<div class='votesAnswers'>" +

                       "<h3><span>" + dr.GetValue(6).ToString() + "</span> التقييم</h3>" +
                     "<div class='answersContainer'>" +
                     "<h3><span>" + dr.GetValue(7).ToString() + "</span> الأجابات</h3>" +
                      "</div>  </div>" +


                    " <div class='questionTitle'>" +
                        "<a style='color: #0173CC;' href='/question/" + dr.GetValue(3) + "'>" + dr.GetValue(4).ToString() + "</a> </div>" +

                    " <div class='usernameQuestionDetails'>" +
                     "<h2><span><a href='/users/profile/" + dr.GetValue(8).ToString() + "'>" + dr.GetValue(1).ToString() + "</a></span>   <span>" + dr.GetValue(2).ToString() + "</span></h2>" +
                    " <p>" + RelativeDate(Convert.ToDateTime(dr.GetValue(5))) + "</p></div></div>";
                }
            }
            con.Close();
        }
        protected void NewestFilter(object sender, EventArgs e)
        {
            searchResult.InnerHtml = "";
            searchedItemText.InnerText = " النتائج " + "[" + Request.QueryString["searched"].ToString() + "]";

            SqlConnection con = new SqlConnection(cs);

            con.Open();
            string SearchResultByNewest = @"SELECT COUNT(*) OVER(),[User].username,[User].reputation,[Post].id,[Post].title,[Post].creationDate,[Post].upvoteCount,[Post].answerCount,[User].id
                    FROM [User]
                    INNER JOIN [Post]
                    ON [User].id = [Post].userId
                    where [Post].title LIKE N'%" + Request.QueryString["searched"].ToString() + "%' order by [Post].creationDate DESC;";

            SqlCommand cmd = new SqlCommand(SearchResultByNewest, con);

            SqlDataReader dr = cmd.ExecuteReader();

            bool count = false;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (count == false)
                    {
                        resultCount.InnerText = "[" + dr.GetValue(0).ToString() + "]" + " النتائج";
                    }
                    count = true;

                    searchResult.InnerHtml += " " +
                    " <div class='question'>" +
                      "<div class='votesAnswers'>" +

                       "<h3><span>" + dr.GetValue(6).ToString() + "</span> التقييم</h3>" +
                     "<div class='answersContainer'>" +
                     "<h3><span>" + dr.GetValue(7).ToString() + "</span> الأجابات</h3>" +
                      "</div>  </div>" +


                    " <div class='questionTitle'>" +
                        "<a style='color: #0173CC;' href='/question/" + dr.GetValue(3) + "'>" + dr.GetValue(4).ToString() + "</a> </div>" +

                    " <div class='usernameQuestionDetails'>" +
                     "<h2><span><a href='/users/profile/" + dr.GetValue(8).ToString() + "'>" + dr.GetValue(1).ToString() + "</a></span>   <span>" + dr.GetValue(2).ToString() + "</span></h2>" +
                    " <p>" + RelativeDate(Convert.ToDateTime(dr.GetValue(5))) + "</p></div></div>";
                }
            }
            con.Close();
        }
    }
}