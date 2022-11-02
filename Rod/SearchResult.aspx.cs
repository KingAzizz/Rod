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
    public partial class SearchResult : System.Web.UI.Page
    {
        public static string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pc\Documents\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";

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
            Page.Title = string.Format("بحثك عن: {0}",Request.QueryString["searched"].ToString());
            if (!Page.IsPostBack)
            {
                if(Request.QueryString["tab"] != null)
                {
                    Bind(Request.QueryString["tab"].ToString());
                }
                else
                {
                    Bind("default");
                }
            }
        }
        public void Bind(string tab)
        {
            SqlConnection con = new SqlConnection(cs);
            if (tab == "default")
            {
                con.Open();
                string searchedItemQuery = @"SELECT COUNT(*) OVER(),[User].username,[User].reputation,[Post].id,[Post].title,[Post].creationDate,CONVERT(int ,[Post].upvoteCount) + CONVERT(int ,[Post].downvoteCount) as totalVote,[Post].answerCount,[User].id as userIdU
                    FROM [User]
                    INNER JOIN [Post]
                    ON [User].id = [Post].userId
                    where [Post].title LIKE N'%" + Request.QueryString["searched"].ToString() + "%'";
                SqlCommand cmd = new SqlCommand(searchedItemQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Post");
                searchResultListView.DataSource = ds.Tables[0];
                searchResultListView.DataBind();
                searchedItemText.InnerText = " النتائج " + "[" + Request.QueryString["searched"].ToString() + "]";
                resultCount.InnerText = "[" + searchResultListView.Items.Count + "]" + " النتائج";
                con.Close();
                if(searchResultListView.Items.Count == 0)
                {
                  
                    noresultLbl.Visible = true;
                    noresultLbl.InnerText = "لايوجد نتائج";
                }
                else
                {
                    noresultLbl.Visible = false;
                }
            }
            if(tab == "Rating")
            {
                con.Open();

                string searchedItemQuery = @"SELECT COUNT(*) OVER(),[User].username,[User].reputation,[Post].id,[Post].title,[Post].creationDate,CONVERT(int ,[Post].upvoteCount) + CONVERT(int ,[Post].downvoteCount) as totalVote,[Post].answerCount,[User].id
                    FROM [User]
                    INNER JOIN [Post]
                    ON [User].id = [Post].userId
                    where [Post].title LIKE N'%" + Request.QueryString["searched"].ToString() + "%' order by [Post].upvoteCount DESC;";
                SqlCommand cmd = new SqlCommand(searchedItemQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Post");
                searchResultListView.DataSource = ds.Tables[0];
                searchResultListView.DataBind();
                searchedItemText.InnerText = " النتائج " + "[" + Request.QueryString["searched"].ToString() + "]";
                resultCount.InnerText = "[" + searchResultListView.Items.Count + "]" + " النتائج";
                con.Close();
            }
            if(tab == "Newest")
            {
                con.Open();

                string searchedItemQuery = @"SELECT COUNT(*) OVER(),[User].username,[User].reputation,[Post].id,[Post].title,[Post].creationDate,CONVERT(int ,[Post].upvoteCount) + CONVERT(int ,[Post].downvoteCount) as totalVote,[Post].answerCount,[User].id as userIdU
                    FROM [User]
                    INNER JOIN [Post]
                    ON [User].id = [Post].userId
                    where [Post].title LIKE N'%" + Request.QueryString["searched"].ToString() + "%' order by [Post].creationDate DESC;";
                SqlCommand cmd = new SqlCommand(searchedItemQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Post");
                searchResultListView.DataSource = ds.Tables[0];
                searchResultListView.DataBind();
                searchedItemText.InnerText = " النتائج " + "[" + Request.QueryString["searched"].ToString() + "]";
                resultCount.InnerText = "[" + searchResultListView.Items.Count + "]" + " النتائج";

                con.Close();
            }
        }

        protected void HighRatingFilter(object sender, EventArgs e)
        {
            Response.Redirect("~/SearchResult.aspx?searched=" + Request.QueryString["searched"].ToString() + "&tab=Rating");
        }
        protected void NewestFilter(object sender, EventArgs e)
        {
            Response.Redirect("~/SearchResult.aspx?searched=" + Request.QueryString["searched"].ToString() + "&tab=Newest");
        }
    }
}