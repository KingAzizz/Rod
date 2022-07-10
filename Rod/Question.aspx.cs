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
    public partial class Question : System.Web.UI.Page
    {
        

        public static bool Followed(int user1, int user2)
        {
          if(HttpContext.Current.Session["id"] != null)
            {
                string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                string checkIfFollow = @"Select * from [Following]
                where [Following].userId =" + user1 +  "and [followingID] =" + user2;
                SqlCommand cmd = new SqlCommand(checkIfFollow, con);

                SqlDataReader dr = cmd.ExecuteReader();

              


                if (dr.HasRows)
                {
                   
                    con.Close();
                    return false;
                }
                else
                {
                    
                    con.Close();
                    return true;
                }
               
            }
            else
            {
                HttpContext.Current.Response.Write("u must be logged in");
            }
          
            return true;
        }
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
                return ts.Seconds == 1 ? " قبل ثانية " :   " ثانية "+ ts.Seconds +" قبل ";

            if (delta < 2 * MINUTE)
                return " قبل دقيقة ";

            if (delta < 45 * MINUTE)
                return  " دقيقة "+ ts.Minutes + " قبل  ";

            if (delta < 90 * MINUTE)
                return "قبل ساعة";

            if (delta < 24 * HOUR)
                return  " ساعه " + ts.Hours + " قبل  ";

            if (delta < 48 * HOUR)
                return "امس";

            if (delta < 30 * DAY)
                return " قبل يوم " + ts.Days;

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? " شهر واحد " :  " شهور "+ months +" قبل  ";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "قبل سنة" :" سنوات " + years + " قبل  ";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
           
            if (!Page.IsPostBack)
            {
                
                string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                string viewQuestionIfItHasAnswers = @" SELECT *
                    FROM [Post]

                    JOIN [User]
                      ON [Post].userId = [User].id 
 
                    JOIN Answer
                      ON [Post].id = [Answer].postId
                      JOIN [User] as UserAnswer
                      on Answer.userId = [UserAnswer].id
                       where [Post].id = "+ Request.QueryString["question"].ToString();

                SqlCommand cmd = new SqlCommand(viewQuestionIfItHasAnswers, con);

                SqlDataReader dr = cmd.ExecuteReader();
                bool triggerd = false; 
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        
                        if(triggerd != true)
                        {
                            if(Convert.ToInt32(Session["id"]) != Convert.ToInt32(dr.GetValue(1)))
                            {

                            userId.Value = dr.GetValue(1).ToString();
                            postUserFollower.Visible = Followed(Convert.ToInt32(Session["id"]), Convert.ToInt32(dr.GetValue(1)));
                            bool userpostUserFollower = postUserFollower.Visible;
                            unFollowUserPost.Visible = !userpostUserFollower;
                            }
                            else
                            {
                                postUserFollower.Visible = false;
                               
                                unFollowUserPost.Visible = false;
                            }
                            postTitle.InnerText = dr.GetValue(2).ToString();
                            postBody.InnerText = dr.GetValue(3).ToString();
                            postCreation.InnerText = RelativeDate(Convert.ToDateTime(dr.GetValue(5)));

                            postCreationUser.InnerText = RelativeDate(Convert.ToDateTime(dr.GetValue(5))) + "انسأل قبل";
                            postTagsDiv.InnerHtml = "<a href=#TAGGHERE>" + dr.GetValue(4).ToString() + "</a>";
                            userProfileImagePost.ImageUrl = dr.GetValue(24).ToString();
                            userLinkProfilePost.NavigateUrl = "#";
                            userLinkProfilePost.Text = dr.GetValue(11).ToString();
                            postUpvotedown.InnerText = dr.GetValue(8).ToString();
                            Bind();
                        }
                        triggerd = true;

                        
            
                    }
                }
                else
                {
                    con.Close();
                    con.Open();
                    Datalist.Visible = false;
                    string viewQuestionIfItDosentHasAnswers = @"SELECT *
                    FROM[Post]

                    JOIN[User]
                      ON[Post].userId = [User].id
                      where[Post].id ="+ Request.QueryString["question"].ToString();
                    SqlCommand cmdAnswerNotFound = new SqlCommand(viewQuestionIfItDosentHasAnswers, con);

                    SqlDataReader drAnswerNotFound = cmdAnswerNotFound.ExecuteReader();

                    if (drAnswerNotFound.HasRows)
                    {
                        while (drAnswerNotFound.Read())
                        {

                            if (triggerd != true)
                            {
                                userId.Value = dr.GetValue(1).ToString();
                                postUserFollower.Visible = Followed(Convert.ToInt32(Session["id"]), Convert.ToInt32(dr.GetValue(1)));
                                bool userpostUserFollower = postUserFollower.Visible;
                                unFollowUserPost.Visible = !userpostUserFollower;
                                postTitle.InnerText = drAnswerNotFound.GetValue(2).ToString();
                                postBody.InnerText = drAnswerNotFound.GetValue(3).ToString();
                                postCreation.InnerText = RelativeDate(Convert.ToDateTime(drAnswerNotFound.GetValue(5)));

                                postCreationUser.InnerText = RelativeDate(Convert.ToDateTime(drAnswerNotFound.GetValue(5))) + "انسأل قبل";
                                postTagsDiv.InnerHtml = "<a href=#TAGGHERE>" + drAnswerNotFound.GetValue(4).ToString() + "</a>";
                                userProfileImagePost.ImageUrl = "https://i.pinimg.com/564x/a1/85/45/a185459b5804d2b96231521f0b333d9b.jpg";
                                userLinkProfilePost.NavigateUrl = "#";
                                userLinkProfilePost.Text = drAnswerNotFound.GetValue(11).ToString();
                                postUpvotedown.InnerText = drAnswerNotFound.GetValue(8).ToString();
                            }
                            triggerd = true;

                            answersPost.InnerHtml = @"<div class='noAnswerDiv'>           <p>لاتوجد أجابات</p>
                                </div>";

                        }
                    }
                }

                con.Close();
            }
        }
        protected void Datalist_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            Button Button = e.Item.FindControl("followBtn") as Button;
            if(Button.Visible == false)
            {
                e.Item.FindControl("unFollowBtn").Visible = true;
            }
          
        }
        protected void Datalist_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);


            if (e.CommandName == "Follow")
            {
                con.Open();

                string followInsert = @"
         insert into [Following] ([userId],[followingID]) values("+ Session["id"] +"," +e.CommandArgument + ");" +
            "insert into [Followers] ([userId],[followerID]) values("+e.CommandArgument +"," + Session["id"]+");";
                SqlCommand cmd = new SqlCommand(followInsert, con);
                cmd.ExecuteNonQuery();
                e.Item.FindControl("followBtn").Visible = false;
                e.Item.FindControl("unFollowBtn").Visible = true;
                con.Close();
                

            }
            if(e.CommandName == "Unfollow")
            {
                con.Open();

                string unFollowDelete = @"
                delete from [Following] where [userId] ="+ Session["id"] + @" and [followingID] ="+ e.CommandArgument + @";
                delete from [Followers] where [userId] =" + e.CommandArgument + @"and [followerID] =" + Session["id"] +" ;";
                SqlCommand cmd = new SqlCommand(unFollowDelete, con);
                cmd.ExecuteNonQuery();
                e.Item.FindControl("unFollowBtn").Visible = false;
                e.Item.FindControl("followBtn").Visible = true;
                con.Close();
            }
        }
        public void Bind()
        {
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);
            string answersQuery = @"SELECT  Post.*, Answer.*,
            [User].id as userPostId,
            [User].username as post_username,
            useranswer.username as answer_username,
            Answer.id as userAnswerId,
            Answer.upvoteCount as answer_upvoteCount,
            Answer.downvoteCount as answer_downvoteCount,
            Answer.creationDate as answer_creationDate,
            useranswer.profileImage as answer_profileImage

            FROM[Post]
            JOIN[User]  ON[Post].userId = [User].id
            JOIN Answer ON[Post].id = [Answer].postId
            JOIN[User] as UserAnswer on Answer.userId = [UserAnswer].id
            where[Post].id =" + Request.QueryString["question"].ToString();
            SqlCommand cmd = new SqlCommand(answersQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            
            DataSet ds = new DataSet();
            da.Fill(ds, "Post");
            Datalist.DataSource = ds.Tables[0];
            Datalist.DataBind();
          
        }

        protected void PostUserFollower_Click(object sender, EventArgs e)
        {
            if(Session["id"] != null)
            {

            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string followInsert = @"
         insert into [Following] ([userId],[followingID]) values(" + Session["id"] + "," + Convert.ToInt32(userId.Value) + ");" +
        "insert into [Followers] ([userId],[followerID]) values(" + Convert.ToInt32(userId.Value) + "," + Session["id"] + ");";
            SqlCommand cmd = new SqlCommand(followInsert, con);
            cmd.ExecuteNonQuery();
                
                postUserFollower.Visible = false;
           
                unFollowUserPost.Visible = true;
                con.Close();
            }
        }

        protected void UnFollowUserPost_Click(object sender, EventArgs e)
        {
            if(Session["id"] != null)
            {

            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            

            string unFollowDelete = @"
                delete from [Following] where [userId] =" + Session["id"] + @" and [followingID] =" + Convert.ToInt32(userId.Value) + @";
                delete from [Followers] where [userId] =" + Convert.ToInt32(userId.Value) + @"and [followerID] =" + Session["id"] + " ;";
            SqlCommand cmd = new SqlCommand(unFollowDelete, con);
            cmd.ExecuteNonQuery();
            postUserFollower.Visible = true;

            unFollowUserPost.Visible = false;
            con.Close();
            }
        }
    }
}


