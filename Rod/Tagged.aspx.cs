using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace Rod
{
    public partial class Tagged : System.Web.UI.Page
    {
        public static string cs = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Bind();
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
            }
            SqlConnection con = new SqlConnection(cs);

                con.Open();

                string TagsQuestion = @"select [User].id as UserId,[User].username,[User].reputation,[User].profileImage,
            [Post].id as PostId, [Post].title, [Post].creationDate,answerCount,
                CONVERT(int ,upvoteCount) + CONVERT(int ,downvoteCount) as totalVote,[TagInfo].tagName
            from Post 
            join [User] on [Post].userId = [User].id 
            join [PostTags] on [Post].id =[PostTags].postId
            join [TagInfo] on tagId = [TagInfo].id
            where tagId = @tagId
            order by [Post].answerCount";

                SqlCommand cmd = new SqlCommand(TagsQuestion, con);
                cmd.Parameters.AddWithValue("@tagId", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Post");
                questionsListView.DataSource = ds.Tables[0];
                questionsListView.DataBind();
                if (questionsListView.Items.Count == 0)
                {
                noResult.Visible = true;
                }
                con.Close();
        }
        protected void questionsListView_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            (questionsListView.FindControl("DataPager") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
          
            
                Bind();
            

        }

        protected void questionsListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HtmlGenericControl tagsDiv = e.Item.FindControl("tags") as HtmlGenericControl;
            HiddenField questionId = e.Item.FindControl("questionId") as HiddenField;
            HiddenField tagName = e.Item.FindControl("tagNameHiddenField") as HiddenField;
            Page.Title = string.Format(tagName.Value.ToString());
            tagNameH2.InnerText = tagName.Value.ToString();
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
                    tagsDiv.InnerHtml = "<span class='tagplaceholder'> <a href='#" + dr.GetValue(0) + "'>" + dr.GetValue(1).ToString() + "</a></span>";
                }
            }
            con.Close();
        }
    }
}