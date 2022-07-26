using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

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

            Session["id"] = 1;
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
                            SELECT  [User].id,[user].username ,[user].creationDate ,[user].displayName ,CAST([user].aboutMe as nvarchar(1300)) as aboutMe ,[user].education,[user].[location] ,CAST([user].websiteUrl as nvarchar(600)) as websiteUrl,CAST([user].twitterUrl as nvarchar(600)) as twitterUrl,CAST([user].githubUrl as nvarchar(600)) as githubUrl,CAST([user].profileImage as nvarchar(600)) as profileImage,[user].reputation,[user].followers,[user].[following],[user].[views],COUNT([Post].id) as postCount, COUNT([Answer].id) as answerCount
           

                            FROM[User]
                                JOIN[Post]  ON[Post].userId = [User].id
                                JOIN Answer ON [Answer].userId= [User].id
                                where[User].id = " + Session["id"] + @" 
                                group by  [User].id,[user].username ,[User].creationDate ,[User].displayName ,CAST([User].aboutMe as nvarchar(1300)) ,[User].education,[User].[location] ,CAST([User].websiteUrl as nvarchar(600)),CAST([user].twitterUrl as nvarchar(600)),CAST([User].githubUrl as nvarchar(600)),CAST([User].profileImage as nvarchar(600)),[User].reputation,[user].followers,[User].[following],[user].[views]
                        END;
                        ELSE IF EXISTS (select * from [Post] where userId =" + Session["id"] + 
                        @" )
                        BEGIN
                          SELECT  [User].id,[user].username ,[user].creationDate ,[user].displayName ,CAST([user].aboutMe as nvarchar(1300)) as aboutMe ,[user].education,[user].[location] ,CAST([user].websiteUrl as nvarchar(600)) as websiteUrl,CAST([user].twitterUrl as nvarchar(600)) as twitterUrl,CAST([user].githubUrl as nvarchar(600)) as githubUrl,CAST([user].profileImage as nvarchar(600)) as profileImage,[user].reputation,[user].followers,[user].[following],[user].[views],COUNT([Post].id) as postCount
        
                            FROM[User]
                                JOIN[Post]  ON[Post].userId = [User].id
            
                                where[User].id =" + Session["id"] + @" 
                                
                                group by  [User].id,[user].username ,[user].creationDate ,[user].displayName ,CAST([user].aboutMe as nvarchar(1300)) ,[user].education,[user].[location] ,CAST([user].websiteUrl as nvarchar(600)),CAST([user].twitterUrl as nvarchar(600)),CAST([user].githubUrl as nvarchar(600)),CAST([user].profileImage as nvarchar(600)),[user].reputation,[user].followers,[user].[following],[user].[views]
                        END
                             ELSE IF EXISTS (select * from [Answer] where userId =" + Session["id"] + 
                             @" )
                        BEGIN
                          SELECT  [User].id,[user].username ,[user].creationDate ,[user].displayName ,CAST([user].aboutMe as nvarchar(1300)) as aboutMe ,[user].education,[user].[location] ,CAST([user].websiteUrl as nvarchar(600)) as websiteUrl,CAST([user].twitterUrl as nvarchar(600)) as twitterUrl,CAST([user].githubUrl as nvarchar(600)) as githubUrl,CAST([user].profileImage as nvarchar(600)) as profileImage,[user].reputation,[user].followers,[user].[following],[user].[views],COUNT([Answer].id) as answerCount
        
                            FROM[User]
                                JOIN[Answer]  ON[Answer].userId = [User].id
            
                                where[User].id =" + Session["id"] + 
                                @" 
                                
                                group by  [User].id,[user].username ,[user].creationDate ,[user].displayName ,CAST([user].aboutMe as nvarchar(1300)) ,[user].education,[user].[location] ,CAST([user].websiteUrl as nvarchar(600)),CAST([user].twitterUrl as nvarchar(600)),CAST([user].githubUrl as nvarchar(600)),CAST([user].profileImage as nvarchar(600)),[user].reputation,[user].followers,[user].[following],[user].[views]
                        END
                        ELSE
                        BEGIN
                          SELECT id,username,creationDate,displayName,aboutMe,education,[location],websiteUrl,twitterUrl,githubUrl,profileImage,reputation,followers,[following],[views] from [User] where id =" + Session["id"] + @" 
                        END;";

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
                          
                            username.InnerText = "@" + dr.GetValue(1).ToString();
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
                            /*
                            pageContainer.Style.Add("position", "relative");
                            pageContainer.Style.Add("bottom", "70px");*/

                            if (dr.FieldCount == 17)
                            {
                                userQuestions.InnerText = dr.GetValue(15).ToString();
                                userAnswers.InnerText = dr.GetValue(16).ToString();
                                /*pageContainer.Style.Remove("position");
                                pageContainer.Style.Remove("bottom");*/

                            }
                            else
                            {
                                if (DataRecordExtensions.HasColumn(dr, "postCount"))
                                {
                                    userQuestions.InnerText = dr.GetValue(15).ToString();
                                   
                                    /*expectionCss1.Style.Add("width", "fit-content !important");
                                    expectionCss2.Style.Add("width", "fit-content !important");*/
                                    questionsDivD.Style.Remove("width");

                                }
                                if (DataRecordExtensions.HasColumn(dr, "answerCount"))
                                {
                                    userAnswers.InnerText = dr.GetValue(15).ToString();

                                }
                            }

                        }
                        Bind();
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
        public void Bind()
        {
            
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
                SqlConnection con = new SqlConnection(cs);
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
            string questionsQuery = @"select id,title,creationDate,CONVERT(int ,upvoteCount) + CONVERT(int ,downvoteCount) as totalVote from Post where id =" + Session["id"];
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
            string answersQuery = @"select postId,answerText,creationDate, CONVERT(int ,upvoteCount) + CONVERT(int ,downvoteCount) as totalVote
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
    }
}
