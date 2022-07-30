﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.HtmlControls;

namespace Rod
{
    public static class DataRecordExtensions
    {
        public static bool HasColumn(this IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }
    public partial class Profile : System.Web.UI.Page
    {
        public string GetMonthDay(string dataValue)
        {
            String sDate = dataValue;
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            String mn = datevalue.Month.ToString();
            String yy = datevalue.Month.ToString();
            String dd = datevalue.Day.ToString();

            string monthName = new DateTime(Convert.ToInt32(yy), Convert.ToInt32(mn), 1).ToString("MMM", CultureInfo.InvariantCulture);

            return monthName + "," + dd;

        }
      
        protected void Page_Load(object sender, EventArgs e)
        {

           
            if (!Page.IsPostBack)
            {
                if (Session["id"] != null)
                {
                    string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
                    SqlConnection con = new SqlConnection(cs);
                    con.Open();
                    string userProfile = @" IF EXISTS( SELECT * from [Post] 
                    inner join [Answer] on Answer.userId = Post.userId
                    where Post.userId ="+ Session["id"] + 
                    @"  )
                            BEGIN
                            SELECT  [User].id,[User].username ,[User].creationDate ,[User].displayName ,CAST([User].aboutMe as nvarchar(1300)) as aboutMe ,[User].education,[User].[location] ,CAST([User].websiteUrl as nvarchar(600)) as websiteUrl,CAST([user].twitterUrl as nvarchar(600)) as twitterUrl,CAST([user].githubUrl as nvarchar(600)) as githubUrl,CAST([User].profileImage as nvarchar(600)) as profileImage,[User].reputation,[User].followers,[User].[following],[User].[views],COUNT([Post].id) as postCount, COUNT([Answer].id) as answerCount
           

                            FROM[User]
                                JOIN[Post]  ON[Post].userId = [User].id
                                JOIN Answer ON [Answer].userId= [User].id
                                where[User].id = " + Session["id"] +
                                @" 
                                group by  [User].id,[User].username ,[User].creationDate ,[User].displayName ,CAST([User].aboutMe as nvarchar(1300)) ,[User].education,[User].[location] ,CAST([User].websiteUrl as nvarchar(600)),CAST([User].twitterUrl as nvarchar(600)),CAST([User].githubUrl as nvarchar(600)),CAST([User].profileImage as nvarchar(600)),[User].reputation,[User].followers,[User].[following],[User].[views]
                        END;
                        ELSE IF EXISTS (select * from [Post] where userId =" + Session["id"] + 
                        @" )
                        BEGIN
                          SELECT  [User].id,[User].username ,[User].creationDate ,[User].displayName ,CAST([User].aboutMe as nvarchar(1300)) as aboutMe ,[User].education,[User].[location] ,CAST([User].websiteUrl as nvarchar(600)) as websiteUrl,CAST([User].twitterUrl as nvarchar(600)) as twitterUrl,CAST([User].githubUrl as nvarchar(600)) as githubUrl,CAST([user].profileImage as nvarchar(600)) as profileImage,[user].reputation,[User].followers,[User].[following],[User].[views],COUNT([Post].id) as postCount
        
                            FROM[User]
                                JOIN[Post]  ON[Post].userId = [User].id
            
                                where[User].id =" + Session["id"] + 
                                @" 
                                
                                group by  [User].id,[User].username ,[User].creationDate ,[User].displayName ,CAST([User].aboutMe as nvarchar(1300)) ,[User].education,[User].[location] ,CAST([User].websiteUrl as nvarchar(600)),CAST([User].twitterUrl as nvarchar(600)),CAST([User].githubUrl as nvarchar(600)),CAST([User].profileImage as nvarchar(600)),[User].reputation,[User].followers,[User].[following],[User].[views]
                        END
                             ELSE IF EXISTS (select * from [Answer] where userId =" + Session["id"] + 
                             @" )
                        BEGIN
                          SELECT  [User].id,[User].username ,[User].creationDate ,[User].displayName ,CAST([User].aboutMe as nvarchar(1300)) as aboutMe ,[User].education,[User].[location] ,CAST([User].websiteUrl as nvarchar(600)) as websiteUrl,CAST([User].twitterUrl as nvarchar(600)) as twitterUrl,CAST([User].githubUrl as nvarchar(600)) as githubUrl,CAST([User].profileImage as nvarchar(600)) as profileImage,[User].reputation,[User].followers,[User].[following],[User].[views],COUNT([Answer].id) as answerCount
        
                            FROM[User]
                                JOIN[Answer]  ON[Answer].userId = [User].id
            
                                where[User].id =" + Session["id"] + 
                                @" 
                                
                                group by  [User].id,[User].username ,[User].creationDate ,[User].displayName ,CAST([User].aboutMe as nvarchar(1300)) ,[User].education,[User].[location] ,CAST([User].websiteUrl as nvarchar(600)),CAST([User].twitterUrl as nvarchar(600)),CAST([User].githubUrl as nvarchar(600)),CAST([User].profileImage as nvarchar(600)),[User].reputation,[User].followers,[User].[following],[User].[views]
                        END
                        ELSE
                        BEGIN
                          SELECT id,username,creationDate,displayName,aboutMe,education,[location],websiteUrl,twitterUrl,githubUrl,profileImage,reputation,followers,[following],[views] from [User] where id =" + Session["id"]+ " END;";

                    SqlCommand cmd = new SqlCommand(userProfile, con);

                    SqlDataReader dr = cmd.ExecuteReader();
                   
                    if (dr.HasRows)
                    {
                       
                      
                        while (dr.Read())
                        {
                           
                            String sDate = dr.GetValue(2).ToString();
                            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
                            String mn = datevalue.Month.ToString();
                            String yy = datevalue.Year.ToString();
                          
                            username.InnerText = dr.GetValue(1).ToString() + "@";
                            creationDate.InnerText = "عضو من" + yy + "," + mn;
                            displayName.InnerText = dr.GetValue(3).ToString();
                            userAboutMe.InnerText = dr.GetValue(4).ToString();
                            education.InnerText = dr.GetValue(5).ToString();
                            location.InnerText = dr.GetValue(6).ToString();
                            websiteUrl.NavigateUrl = dr.GetValue(7).ToString();
                            twitter.NavigateUrl = dr.GetValue(8).ToString();
                            github.NavigateUrl = dr.GetValue(9).ToString();
                            userProfileImage.ImageUrl = dr.GetValue(10).ToString();
                            userReputPoint.InnerText = dr.GetValue(11).ToString();
                            followers.InnerText = dr.GetValue(12).ToString();
                            following.InnerText = dr.GetValue(13).ToString();
                            userViews.InnerText = dr.GetValue(14).ToString();
                            userQuestions.InnerText = "0";
                            userAnswers.InnerText = "0";

                            if (dr.FieldCount == 17)
                            {
                                userQuestions.InnerText = dr.GetValue(15).ToString();
                                userAnswers.InnerText = dr.GetValue(16).ToString();
                               

                            }
                            else
                            {
                                if (DataRecordExtensions.HasColumn(dr, "postCount"))
                                {
                                    userQuestions.InnerText = dr.GetValue(15).ToString();
                                    questionsDivD.Style.Remove("width");
                                }
                                if (DataRecordExtensions.HasColumn(dr, "answerCount"))
                                {
                                    userAnswers.InnerText = dr.GetValue(15).ToString();
                                }
                            }

                        }
                        if(Request.QueryString["tab"] == null)
                        {
                          
                        Bind("default");
                        }
                        else
                        {
                            defaultTap.Visible = false;
                            if (Request.QueryString["tab"].ToString() == "questions")
                            {
                                Bind("questions");
                             
                                questionTabDatalist.Visible = true;
                                tabsLabel.Visible = true;
                                profileContainer.Style["display"]= "block !important";
                              
                            }
                            if (Request.QueryString["tab"].ToString() == "answers")
                            {
                                defaultTap.Visible = false;
                                Bind("answers");
                              
                                answerTabDatalist.Visible = true;
                                tabsLabel.Visible = true;
                               


                                profileContainer.Style["display"] = "block !important";

                            }
                            if (Request.QueryString["tab"].ToString() == "tags")
                            {
                                defaultTap.Visible = false;
                               
                                Bind("tags");
                               
                                TagTabListView.Visible = true;
                                tagsSection.Visible = true;
                                tabsLabel.Visible = true;//change
                            

                                
                                profileContainer.Style["display"] = "block !important";

                            }
                            if (Request.QueryString["tab"].ToString() == "badges")
                            {
                                defaultTap.Visible = false;

                                Bind("badges");
                              
                                BadgesTabListView.Visible = true;
                                badgesSection.Visible = true;
                                tabsLabel.Visible = true;
                                profileContainer.Style["display"] = "block !important";

                            }

                        }
                        
                       
                    }

                }
            
                
                if (education.InnerText == "")
                {
                    eduP.Visible = false;
                }
                if (location.InnerText == "")
                {
                    locationP.Visible = false;
                }
                if(websiteUrl.NavigateUrl == "")
                {
                    websiteUrl.Visible = false;
                    
                }
                if (github.NavigateUrl == "")
                {
                    github.Visible = false;
                }
                if (twitter.NavigateUrl == "")
                {
                    twitter.Visible = false;
                }

            }
        }
        public void Bind(string tab)
        {
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);
            if(tab == "default")
            {


                con.Open();
                string badgesQuery = @"select [name],[description],[badgeImage],[acquiredDate] 
                from Badge
                inner join [BadgesUser]
                on [BadgesUser].badgeId = Badge.id
                where userId =" + Session["id"];
                SqlCommand cmd = new SqlCommand(badgesQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Badge");
                BadgesDataList.DataSource = ds.Tables[0];
                BadgesDataList.DataBind();
            if(BadgesDataList.Items.Count == 0)
            {
                userBadges.Visible = false;
            }
                con.Close();

            con.Open();
            string questionsQuery = @"select id,title,tag,creationDate,CONVERT(int ,upvoteCount) + CONVERT(int ,downvoteCount) as totalVote from Post where userId =" + Session["id"];
            SqlCommand cmdQuestion = new SqlCommand(questionsQuery, con);
            SqlDataAdapter daQuestion = new SqlDataAdapter(cmdQuestion);

            DataSet dsQuestion = new DataSet();
            daQuestion.Fill(dsQuestion, "Post");
            QuestionsDataList.DataSource = dsQuestion.Tables[0];
            QuestionsDataList.DataBind();
            if (QuestionsDataList.Items.Count == 0)
            {
                questionsDivD.Visible = false;
            }
            con.Close();
            con.Open();
            string answersQuery = @"select id,postId,answerText,creationDate, CONVERT(int ,upvoteCount) + CONVERT(int ,downvoteCount) as totalVote
            from Answer where userId =" + Session["id"];
            SqlCommand cmdAnswer = new SqlCommand(answersQuery, con);
            SqlDataAdapter daAnswer = new SqlDataAdapter(cmdAnswer);

            DataSet dsAnswer = new DataSet();
            daAnswer.Fill(dsAnswer, "Answer");
            AnswersDataList.DataSource = dsAnswer.Tables[0];
            AnswersDataList.DataBind();
            if (AnswersDataList.Items.Count == 0)
            {
                answersDivD.Visible = false;
            }
            con.Close();
   

            }
            if(tab == "questions")
            {
               
                con.Open();
              
                string questionsQuery = @"select id,title,body,tag,creationDate,CONVERT(int, upvoteCount) - CONVERT(int, downvoteCount) as totalVote,
                (select Count(id) from Post where userId = @userId)  as howManyPost 
                from Post 
                where userId = @userId
                group by id,title,body,tag,creationDate, upvoteCount ,downvoteCount";
                SqlCommand cmd = new SqlCommand(questionsQuery, con);
                cmd.Parameters.AddWithValue("@userId", Session["id"]);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Post");
                questionTabDatalist.DataSource = ds.Tables[0];
                questionTabDatalist.DataBind();
                if (questionTabDatalist.Items.Count == 0)
                {
                    noResult.Visible = true; 
                }
                else
                {
                    HiddenField hidden = questionTabDatalist.Items[0].FindControl("howManyPostHd") as HiddenField;
                    tabsLabel.InnerText = "[" + hidden.Value + "] الاسئلة";
                }
                con.Close();
               
              
            }
            if(tab == "answers")
            {
                con.Open();

                string answersQuery = @"
                select id,postId,Cast(answerText as NVARCHAR(100)) as answerText,creationDate,
                CONVERT(int, upvoteCount) - CONVERT(int, downvoteCount) as totalVote,
                (select Count(id) as howManyPost from Answer where userId = @userId) as howManyPost 
                from Answer 
                where userId = @userId
                group by id,postId,Cast(answerText as NVARCHAR(100)),creationDate, upvoteCount ,downvoteCount";
                SqlCommand cmd = new SqlCommand(answersQuery, con);
                cmd.Parameters.AddWithValue("@userId", Session["id"]);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Answer");
                answerTabDatalist.DataSource = ds.Tables[0];
                answerTabDatalist.DataBind();
                if (answerTabDatalist.Items.Count == 0)
                {
                    noResult.Visible = true;
                }
                else
                {
                    HiddenField hidden = answerTabDatalist.Items[0].FindControl("howManyPostHd") as HiddenField;
                    tabsLabel.InnerText = "[" + hidden.Value + "] الأجابات";
                }
                con.Close();
            }
            if(tab == "tags")
            {
               con.Open();

                string tagsFollowQuery = @"select [TagFollowers].id,tagName,
              (select Count(id) from TagFollowers where userId = @userId) as howManyTags 
               from TagFollowers
               inner join TagInfo on TagInfo.id = tagId
               where userId = @userId
               group by tagName,[TagFollowers].id";
                SqlCommand cmd = new SqlCommand(tagsFollowQuery, con);
                cmd.Parameters.AddWithValue("@userId", Session["id"]);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "TagFollowers");
                TagTabListView.DataSource = ds.Tables[0];
                TagTabListView.DataBind();
                if (TagTabListView.Items.Count == 0)
                {
                    noResult.Visible = true;
                }
                else
                {
                    HiddenField hidden = TagTabListView.Items[0].FindControl("howManyTagsHd") as HiddenField;
                    tabsLabel.InnerText = "[" + hidden.Value + "] الاقسام";
                }
                con.Close();
            }
            if(tab == "badges")
            {
                con.Open();

                string badgesQuery = @"select [name],Cast([description] as NVARCHAR(100)) as [description],Cast(badgeImage as NVARCHAR(600)) as badgeImage,
                (select COUNT(id) from BadgesUser where userId = @userId) as howManyBadges
                    from Badge
                    inner join BadgesUser on [BadgesUser].badgeId = [Badge].id 
                    where userId = @userId
                    group by [name],Cast([description] as NVARCHAR(100)),Cast(badgeImage as NVARCHAR(600))";
                SqlCommand cmd = new SqlCommand(badgesQuery, con);
                cmd.Parameters.AddWithValue("@userId", Session["id"]);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Badge");
                BadgesTabListView.DataSource = ds.Tables[0];
                BadgesTabListView.DataBind();
                if (BadgesTabListView.Items.Count == 0)
                {
                    noResult.Visible = true;
                }
                else
                {
                    HiddenField hidden = BadgesTabListView.Items[0].FindControl("howManyBadgesHd") as HiddenField;
                    tabsLabel.InnerText = "[" + hidden.Value + "] الأوسمة";
                }
                con.Close();
            }


        }
        protected void TagTabListView_OnItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);

            if (e.CommandName == "UnFollowTag")
            {
                con.Open();

                string unFollow = "delete from [TagFollowers] where id = @tagFollowId and userId = @userId";
                e.Item.Visible = false;
                SqlCommand unFollowCommand = new SqlCommand(unFollow, con);
                unFollowCommand.Parameters.AddWithValue("@tagFollowId", e.CommandArgument);
                unFollowCommand.Parameters.AddWithValue("@userId", Session["id"]);

                unFollowCommand.ExecuteNonQuery();
                e.Item.Visible = false;                    
                con.Close();
            }
        }

            protected void profileNav_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/profile.aspx");
        }

        protected void questionNav_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/profile.aspx?tab=questions");

        }

        protected void answerNav_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/profile.aspx?tab=answers");
        }

        protected void tagNav_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/profile.aspx?tab=tags");
        }

        protected void badgeNav_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/profile.aspx?tab=badges");
        }
    }
}
