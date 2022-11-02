using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Timers;
using System.Threading;

namespace Rod
{
    public partial class Question : System.Web.UI.Page
    {
        public static string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pc\Documents\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
        public static bool Followed(int user1, int user2)
        {
          if(HttpContext.Current.Session["id"] != null)
            {
                
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
                return true;
            }
          
           
        }
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
                return " قبل " + ts.Days + " ايام ";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? " قبل شهر واحد  " : " قبل " + months + " شهور  ";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "قبل سنة" : " قبل " + years + " سنوات  ";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            var id = Page.RouteData.Values["id"];

            int num = -1;
            if (id != null)
            {
                if (!int.TryParse(id.ToString(), out num))
                {
                    Response.Redirect("~/");

                    
                }
                
                

                  
                
           
            if (!Page.IsPostBack)
            {
                
                SqlConnection con = new SqlConnection(cs);
              
                con.Open();

                string viewQuestionIfItHasAnswers = @" SELECT *
                    FROM [Post]

                    JOIN [User]
                      ON [Post].userId = [User].id 
                       where [Post].id =" + id.ToString();

                SqlCommand cmd = new SqlCommand(viewQuestionIfItHasAnswers, con);

                SqlDataReader dr = cmd.ExecuteReader();
                bool triggerd = false; 
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        
                        if(triggerd != true)
                        {

                            int totalVotePost = Convert.ToInt32(dr.GetValue(8)) + Convert.ToInt32(dr.GetValue(9));
                            postUpvotedown.InnerText = totalVotePost.ToString();
                            if (Convert.ToInt32(Session["id"]) != Convert.ToInt32(dr.GetValue(1)))
                            {

                            
                            postUserFollower.Visible = Followed(Convert.ToInt32(Session["id"]), Convert.ToInt32(dr.GetValue(1)));
                            bool userpostUserFollower = postUserFollower.Visible;
                            unFollowUserPost.Visible = !userpostUserFollower;
                                deletePost.Visible = false;
                            }
                            else
                            {
                                editButton.Visible = true;
                                editButton.NavigateUrl = "~/question/edit/" + id.ToString();
                                deletePost.Visible = true;
                                postUserFollower.Visible = false;
                                unFollowUserPost.Visible = false;
                            }
                            userId.Value = dr.GetValue(1).ToString();
                            postId.Value = dr.GetValue(0).ToString();
                            postTitle.InnerText = dr.GetValue(2).ToString();
                            postBody.InnerText = dr.GetValue(3).ToString();
                            postCreation.InnerText = RelativeDate(Convert.ToDateTime(dr.GetValue(5)));

                            postCreationUser.InnerText = " انسأل " + RelativeDate(Convert.ToDateTime(dr.GetValue(5)));
                            postTagsDiv.InnerHtml = "<a href=#TAGGHERE>" + dr.GetValue(4).ToString() + "</a>";
                            userProfileImagePost.ImageUrl = dr.GetValue(24).ToString();
                            userLinkProfilePost.NavigateUrl = "~/users/profile/"+ dr.GetValue(1);
                            userLinkProfilePost.Text = dr.GetValue(11).ToString();
                           
                            Bind();
                        }
                        triggerd = true;
                        
                        

                    }
                    con.Close();
                    if(Session["id"] != null)
                    {

                   
                    con.Open();
                    string checkIfUserAlreadyVoted = @"select * from [Vote]
                         where [postId] =" + postId.Value + "and [userId] =" + Session["id"] + ";";
                    SqlCommand cmdIfUserVoted = new SqlCommand(checkIfUserAlreadyVoted, con);
                    SqlDataReader drIfUserVoted = cmdIfUserVoted.ExecuteReader();
                    if (drIfUserVoted.HasRows)
                    {
                        while (drIfUserVoted.Read())
                        {
                            if (Convert.ToInt32(drIfUserVoted.GetValue(4)) == 1)
                            {

                                upvoteIconPost.Attributes.Add("style", "color:orange");
                            }
                            else
                            {

                                downvoteIconPost.Attributes.Add("style", "color:orange");
                            }
                        }
                    }
                    }
                        con.Close();

                }
                else if(!dr.HasRows)
                {
                        con.Close();
                        con.Open();
                    Datalist.Visible = false;
                    string viewQuestionIfItDosentHasAnswers = @"SELECT *
                    FROM[Post]

                    JOIN[User]
                      ON[Post].userId = [User].id
                      where[Post].id ="+ id.ToString();
                    SqlCommand cmdAnswerNotFound = new SqlCommand(viewQuestionIfItDosentHasAnswers, con);

                    SqlDataReader drAnswerNotFound = cmdAnswerNotFound.ExecuteReader();

                    if (drAnswerNotFound.HasRows)
                    {
                        while (drAnswerNotFound.Read())
                        {

                            if (triggerd != true)
                            {
                                int totalVotePost = Convert.ToInt32(dr.GetValue(8)) + Convert.ToInt32(dr.GetValue(9));
                                postUpvotedown.InnerText = totalVotePost.ToString();
                                if (Convert.ToInt32(Session["id"]) != Convert.ToInt32(dr.GetValue(1)))
                                {
                                   
                                   
                                    postUserFollower.Visible = Followed(Convert.ToInt32(Session["id"]), Convert.ToInt32(dr.GetValue(1)));
                                    bool userpostUserFollower = postUserFollower.Visible;
                                    unFollowUserPost.Visible = !userpostUserFollower;
                                    deletePost.Visible = false;
                                }
                                else
                                {
                                    postUserFollower.Visible = false;
                                    deletePost.Visible = true;
                                    unFollowUserPost.Visible = false;
                                }
                                userId.Value = dr.GetValue(1).ToString();
                                postId.Value = drAnswerNotFound.GetValue(0).ToString();
                                postTitle.InnerText = drAnswerNotFound.GetValue(2).ToString();
                                postBody.InnerText = drAnswerNotFound.GetValue(3).ToString();
                                postCreation.InnerText = RelativeDate(Convert.ToDateTime(drAnswerNotFound.GetValue(5)));

                                postCreationUser.InnerText = RelativeDate(Convert.ToDateTime(drAnswerNotFound.GetValue(5))) + "انسأل قبل";
                                postTagsDiv.InnerHtml = "<a href=#TAGGHERE>" + drAnswerNotFound.GetValue(4).ToString() + "</a>";
                                userProfileImagePost.ImageUrl = "~/"+dr.GetValue(24);
                    userLinkProfilePost.NavigateUrl = "~/users/profile/" + dr.GetValue(1);
                                    userLinkProfilePost.Text = drAnswerNotFound.GetValue(11).ToString();
                                postUpvotedown.InnerText = drAnswerNotFound.GetValue(8).ToString();
                            }
                            triggerd = true;

                            answersPost.InnerHtml = @"<div class='noAnswerDiv'>           <p>لاتوجد أجابات</p>
                                </div>";

                        }
                        }else
                        {
                            Response.Redirect("~/");
                        }
                    }
                  

                    con.Close();
            }
            }
            else
            {
                Response.Redirect("~/");
            }
        }
        protected void Datalist_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (Session["id"] != null)
            {
                HiddenField hiddenUserId = e.Item.FindControl("answerUserId") as HiddenField;
            if (Session["id"].ToString() == hiddenUserId.Value.ToString())
            {
                e.Item.FindControl("deleteAnswer").Visible = true;
                e.Item.FindControl("followBtn").Visible = false;
                e.Item.FindControl("unFollowBtn").Visible = false;
            }
            else
            {
                e.Item.FindControl("deleteAnswer").Visible = false;
            }
           
                HiddenField hiddenAnswerId = e.Item.FindControl("answerPostID") as HiddenField;
           
            Button Button = e.Item.FindControl("followBtn") as Button;
            if(Button.Visible == false)
            {
                    if(Session["id"].ToString() != hiddenUserId.Value.ToString())
                    {
                e.Item.FindControl("unFollowBtn").Visible = true;
                    }
            }
           

           
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            string checkIfUserAlreadyVoted = @"select * from [Vote]
            where [answerId] =" + hiddenAnswerId.Value + "and [userId] ="+Session["id"] + ";";
            SqlCommand cmd = new SqlCommand(checkIfUserAlreadyVoted, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if(Convert.ToInt32(dr.GetValue(4)) == 1)
                    {
                        HtmlGenericControl upvote = e.Item.FindControl("upvoteIcon") as HtmlGenericControl;
                        upvote.Attributes.Add("style", "color:orange");
                    }
                    else
                    {
                        HtmlGenericControl downvote = e.Item.FindControl("downvoteIcon") as HtmlGenericControl;
                        downvote.Attributes.Add("style", "color:orange");
                    }
                }
            }
            con.Close();
            }

        }
        protected void Datalist_ItemCommand(object source, DataListCommandEventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);

            HtmlGenericControl upvote = e.Item.FindControl("upvoteIcon") as HtmlGenericControl;
            HtmlGenericControl downvote = e.Item.FindControl("downvoteIcon") as HtmlGenericControl;
            HtmlGenericControl totalVotes = e.Item.FindControl("totalVotes") as HtmlGenericControl;
            HiddenField userAnswerIdHD = e.Item.FindControl("userAnswerIdHD") as HiddenField;
            int votes = Convert.ToInt32(totalVotes.InnerText);
            if (e.CommandArgument != Session["id"])
            {
                if (e.CommandName == "Follow")
                {
                    if (Session["id"] != null)
                    {

                        con.Open();

                        string followInsert = @"
                insert into [Following] ([userId],[followingID]) values(@userId,@followed);
                insert into [Followers] ([userId],[followerID]) values(@followed,@userId);";
                        SqlCommand cmd = new SqlCommand(followInsert, con);
                        cmd.Parameters.AddWithValue("@userId", Session["id"]);
                        cmd.Parameters.AddWithValue("@followed", e.CommandArgument);
                        cmd.ExecuteNonQuery();
                        e.Item.FindControl("followBtn").Visible = false;
                        e.Item.FindControl("unFollowBtn").Visible = true;
                        con.Close();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", @"
                    let alertDiv = document.getElementById('alertDiv');
                    let alertText = document.getElementById('alertText');
                    alertDiv.style.display = 'block';
                    alertText.innerText = 'لايمكنك المتابعة اذ لم تكن مسجل دخول';
                    alertDiv.classList.add('fadeAway');
                    setTimeout(() => {
                    alertDiv.style.display = 'none';
                    alertText.innerText = '';
                    alertDiv.classList.remove('fadeAway');
                    }, 4000)", true);
                    }


                }

                if (e.CommandName == "Unfollow")
                {
                    if (Session["id"] != null)
                    {

                        con.Open();

                        string unFollowDelete = @"
                delete from [Following] where [userId] =" + Session["id"] + @" and [followingID] =" + e.CommandArgument + @";
                delete from [Followers] where [userId] =" + e.CommandArgument + @"and [followerID] =" + Session["id"] + " ;";
                        SqlCommand cmd = new SqlCommand(unFollowDelete, con);
                        cmd.ExecuteNonQuery();
                        e.Item.FindControl("unFollowBtn").Visible = false;
                        e.Item.FindControl("followBtn").Visible = true;
                        con.Close();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", @"
                    let alertDiv = document.getElementById('alertDiv');
                    let alertText = document.getElementById('alertText');
                    alertDiv.style.display = 'block';
                    alertText.innerText = 'لايمكنك الغاء المتابعة اذ لم تكن مسجل دخول';
                    alertDiv.classList.add('fadeAway');
                    setTimeout(() => {
                    alertDiv.style.display = 'none';
                    alertText.innerText = '';
                    alertDiv.classList.remove('fadeAway');
                    }, 4000)", true);
                    }
                }
            }

            if(e.CommandName == "Upvote")
            {
                if(Session["id"] != null)
                {
                   

                    con.Open();
                        string checkifUseAlreadyUpvoted = @"select * from [Vote] where [answerId] =" + Convert.ToInt32(e.CommandArgument) +" and [userId] =" + Convert.ToInt32(Session["id"])+ "and [voteTypeId] = 1";
                    SqlCommand cmdIfUserAlreadyVoted = new SqlCommand(checkifUseAlreadyUpvoted, con);
                    SqlDataReader dr = cmdIfUserAlreadyVoted.ExecuteReader();
                    if (dr.HasRows)
                    {
                        con.Close();

                        con.Open();

                        string removeUpvote = @" update [Answer]
                        set[upvoteCount] = [upvoteCount] - 1
                        where id = " + e.CommandArgument + "; " 
                      + @"delete from [Vote] where [answerId] ="+ e.CommandArgument + " and [userId] =" + Session["id"] +";";
                        SqlCommand cmdRemoveUpvote = new SqlCommand(removeUpvote, con);
                        cmdRemoveUpvote.ExecuteNonQuery();
                       
                        upvote.Attributes.Add("style", "color:black");
                        con.Close();
                        con.Open();
                        string updateReputation = @"update [User]
                        set reputation = reputation - 2
                        where id = @userId; ";
                        SqlCommand updateReputCMD = new SqlCommand(updateReputation, con);
                        updateReputCMD.Parameters.AddWithValue("@userId", userAnswerIdHD.Value);
                        updateReputCMD.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        con.Close();

                        con.Open();

                        string checkIfUserDownVoted = @"select * from [Vote]
                        where [answerId] = @answerId and [userId] = @userId and [voteTypeId] = @voteTypeId;";
                        SqlCommand checkIfUserDownVotedCmd = new SqlCommand(checkIfUserDownVoted, con);
                        checkIfUserDownVotedCmd.Parameters.AddWithValue("@answerId", Convert.ToInt32(e.CommandArgument));
                        checkIfUserDownVotedCmd.Parameters.AddWithValue("@userId", Convert.ToInt32(Session["id"]));
                        checkIfUserDownVotedCmd.Parameters.AddWithValue("@voteTypeId", 2);
                        SqlDataReader checkIfUserDownVotedCmdDR = checkIfUserDownVotedCmd.ExecuteReader();
                   

                        if (checkIfUserDownVotedCmdDR.HasRows)
                        {
                            con.Close();
                            con.Open();
                            string deleteDownvote = @"delete from [Vote] 
                            where [answerId] =" + e.CommandArgument + " and [userId] ="+ Session["id"] + @";

                            update [Answer]
                            set [downvoteCount] = downvoteCount + 1
                            where id =" + e.CommandArgument + " ;";
                            SqlCommand deletedownVote = new SqlCommand(deleteDownvote, con);
                            deletedownVote.ExecuteNonQuery();
                           

                            downvote.Attributes.Add("style","color:black");

                        }

                        con.Close();
                        con.Open();
                    DateTime myDateTime = DateTime.Now;
                    string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                   
                string upVoteInsert = @"update [Answer]
                    set [upvoteCount] = [upvoteCount] + 1
                    where id =" + e.CommandArgument + ";" 
                    + @"insert into [Vote]([answerId],[userId],[voteTypeId],[creationDate])
                    values(@answerId,@userId,1,@creationDate);";
                    SqlCommand cmd = new SqlCommand(upVoteInsert, con);
                    cmd.Parameters.AddWithValue("@answerId", Convert.ToInt32(e.CommandArgument));
                    cmd.Parameters.AddWithValue("@userId", Convert.ToInt32(Session["id"]));
                    cmd.Parameters.AddWithValue("@creationDate", sqlFormattedDate);
                    cmd.ExecuteNonQuery();
                        
                        upvote.Attributes.Add("style", "color:orange");
                    con.Close();
                        con.Open();
                        string updateReputation = @"update [User]
                        set reputation = reputation + 2
                        where id = @userId; ";
                        SqlCommand updateReputCMD = new SqlCommand(updateReputation, con);
                        updateReputCMD.Parameters.AddWithValue("@userId", userAnswerIdHD.Value);
                        updateReputCMD.ExecuteNonQuery();
                        con.Close();
                       
                        Response.Redirect(Request.RawUrl);

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", @"
                    let alertDiv = document.getElementById('alertDiv');
                    let alertText = document.getElementById('alertText');
                    alertDiv.style.display = 'block';
                    alertText.innerText = 'لايمكنك التصويت اذ لم تكن مسجل دخول';
                    alertDiv.classList.add('fadeAway');
                    setTimeout(() => {
                    alertDiv.style.display = 'none';
                    alertText.innerText = '';
                    alertDiv.classList.remove('fadeAway');
                    }, 4000)", true);
                }
            }

            if(e.CommandName == "Downvote") {

                if (Session["id"] != null)
                {


                    con.Open();
                    string checkifUseAlreadyUpvoted = @"select * from [Vote] where [answerId] =" + Convert.ToInt32(e.CommandArgument) + " and [userId] =" + Convert.ToInt32(Session["id"]) + "and [voteTypeId] = 2";
                    SqlCommand cmdIfUserAlreadyVoted = new SqlCommand(checkifUseAlreadyUpvoted, con);
                    SqlDataReader dr = cmdIfUserAlreadyVoted.ExecuteReader();
                    if (dr.HasRows)
                    {
                        con.Close();

                        con.Open();

                        string removeDownvote = @" update [Answer]
                        set[downvoteCount] = [downvoteCount] + 1
                        where id = " + e.CommandArgument + "; "
                      + @"delete from [Vote] where [answerId] =" + e.CommandArgument + " and [userId] =" + Session["id"] + ";";
                        SqlCommand cmdRemoveUpvote = new SqlCommand(removeDownvote, con);
                        cmdRemoveUpvote.ExecuteNonQuery();
                     
                        downvote.Attributes.Add("style", "color:black");

                        con.Close();
                        con.Open();
                        string updateReputation = @"update [User]
                        set reputation = reputation + 3
                        where id = @userId; ";
                        SqlCommand updateReputCMD = new SqlCommand(updateReputation, con);
                        updateReputCMD.Parameters.AddWithValue("@userId", userAnswerIdHD.Value);
                        updateReputCMD.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        con.Close();

                        con.Open();

                        string checkIfUserUpVoted = @"select * from [Vote]
                        where [answerId] = @answerId and [userId] = @userId and [voteTypeId] = @voteTypeId;";
                        SqlCommand checkIfUserUpVotedCmd = new SqlCommand(checkIfUserUpVoted, con);
                        checkIfUserUpVotedCmd.Parameters.AddWithValue("@answerId", Convert.ToInt32(e.CommandArgument));
                        checkIfUserUpVotedCmd.Parameters.AddWithValue("@userId", Convert.ToInt32(Session["id"]));
                        checkIfUserUpVotedCmd.Parameters.AddWithValue("@voteTypeId", 1);
                        SqlDataReader checkIfUserUpVotedCmdDR = checkIfUserUpVotedCmd.ExecuteReader();


                        if (checkIfUserUpVotedCmdDR.HasRows)
                        {
                            con.Close();
                            con.Open();
                            string deleteUpvote = @"delete from [Vote] 
                            where [answerId] =" + e.CommandArgument + " and [userId] =" + Session["id"] + @";

                            update [Answer]
                            set [upvoteCount] = upvoteCount - 1
                            where id =" + e.CommandArgument + " ;";
                            SqlCommand deleteupVote = new SqlCommand(deleteUpvote, con);
                            deleteupVote.ExecuteNonQuery();


                            upvote.Attributes.Add("style", "color:black");



                        }

                        con.Close();
                        con.Open();
                        DateTime myDateTime = DateTime.Now;
                        string sqlFormattedDate = myDateTime.ToString("yyyy -MM-dd HH:mm:ss.fff");

                        string downVoteInsert = @"update [Answer]
                    set [downvoteCount] = [downvoteCount] - 1
                    where id =" + e.CommandArgument + ";"
                            + @"insert into [Vote]([answerId],[userId],[voteTypeId],[creationDate])
                    values(@answerId,@userId,2,@creationDate);";
                        SqlCommand cmd = new SqlCommand(downVoteInsert, con);
                        cmd.Parameters.AddWithValue("@answerId", Convert.ToInt32(e.CommandArgument));
                        cmd.Parameters.AddWithValue("@userId", Convert.ToInt32(Session["id"]));
                        cmd.Parameters.AddWithValue("@creationDate", sqlFormattedDate);
                        cmd.ExecuteNonQuery();
                        //totalVotes.InnerText = Convert.ToString(votes - 1); edited here
                        downvote.Attributes.Add("style", "color:orange");
                        upvote.Attributes.Add("style", "color:black");
                        con.Close();
                        con.Open();
                        string updateReputation = @"update [User]
                        set reputation = reputation - 3
                        where id = @userId; ";
                        SqlCommand updateReputCMD = new SqlCommand(updateReputation, con);
                        updateReputCMD.Parameters.AddWithValue("@userId", userAnswerIdHD.Value);
                        updateReputCMD.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect(Request.RawUrl);

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", @"
                    let alertDiv = document.getElementById('alertDiv');
                    let alertText = document.getElementById('alertText');
                    alertDiv.style.display = 'block';
                    alertText.innerText = 'لايمكنك التصويت اذ لم تكن مسجل دخول';
                    alertDiv.classList.add('fadeAway');
                    setTimeout(() => {
                    alertDiv.style.display = 'none';
                    alertText.innerText = '';
                    alertDiv.classList.remove('fadeAway');
                    }, 4000)", true);
                }


            }

            if(e.CommandName == "Delete")
            {
                if(Session["id"] != null)
                {

                
                HiddenField hiddenUserId = e.Item.FindControl("answerUserId") as HiddenField;
                    HtmlGenericControl answerTextP = e.Item.FindControl("answerTextP") as HtmlGenericControl;
                if (Session["id"].ToString() == hiddenUserId.Value.ToString())
                {
                        con.Open();

                        string deleteUserAnswer = @"delete from [Answer]
                        where [id] = @answerId and [userId] = @userId;
                        update [Post]
                        set [answerCount] = answerCount - 1
                        where id = @postId;";

                        SqlCommand cmd = new SqlCommand(deleteUserAnswer, con);

                        cmd.Parameters.AddWithValue("@answerId", e.CommandArgument);
                        cmd.Parameters.AddWithValue("@userId", Session["id"]);
                        cmd.Parameters.AddWithValue("@postId", postId.Value);
                        cmd.ExecuteNonQuery();
                        answerTextP.Attributes.Add("style", "text-decoration: line-through");
                        ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", @"
                        let alertDiv = document.getElementById('alertDiv');
                        let alertText = document.getElementById('alertText');
                        alertDiv.style.display = 'block';
                        alertText.innerText = 'تم الحذف بنجاح';
                        alertDiv.classList.add('fadeAway');
                        setTimeout(() => {
                        alertDiv.style.display = 'none';
                        alertText.innerText = '';
                        alertDiv.classList.remove('fadeAway');
                        }, 4000)", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", @"
                        let alertDiv = document.getElementById('alertDiv');
                        let alertText = document.getElementById('alertText');
                        alertDiv.style.display = 'block';
                        alertText.innerText = 'لست مصرح بالحذف';
                        alertDiv.classList.add('fadeAway');
                        setTimeout(() => {
                        alertDiv.style.display = 'none';
                        alertText.innerText = '';
                        alertDiv.classList.remove('fadeAway');
                        }, 4000)", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", @"
                        let alertDiv = document.getElementById('alertDiv');
                        let alertText = document.getElementById('alertText');
                        alertDiv.style.display = 'block';
                        alertText.innerText = 'بيانات المستخدم غير موجودة';
                        alertDiv.classList.add('fadeAway');
                        setTimeout(() => {
                        alertDiv.style.display = 'none';
                        alertText.innerText = '';
                        alertDiv.classList.remove('fadeAway');
                        }, 4000)", true);
                }
            }
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
                SqlConnection con = new SqlConnection(cs);
                string answersQuery = @"SELECT  Post.*, Answer.*,
            [User].id as userPostId,
            [User].username as post_username,
            useranswer.username as answer_username,
            Answer.id as Answer_PostId,
            Answer.userId as userAnswerId,
            Answer.upvoteCount as answer_upvoteCount,
            Answer.downvoteCount as answer_downvoteCount,
            CONVERT(int ,Answer.upvoteCount) + CONVERT(int ,Answer.downvoteCount) as totalVote,
            Answer.creationDate as answer_creationDate,
            useranswer.profileImage as answer_profileImage

            FROM[Post]
            JOIN[User]  ON[Post].userId = [User].id
            JOIN Answer ON[Post].id = [Answer].postId
            JOIN[User] as UserAnswer on Answer.userId = [UserAnswer].id
            where[Post].id =" + id.ToString();
                SqlCommand cmd = new SqlCommand(answersQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds, "Post");
                Datalist.DataSource = ds.Tables[0];
                Datalist.DataBind();
            }
          
        }

        protected void PostUserFollower_Click(object sender, EventArgs e)
        {
            if(Session["id"] != null && userId.Value != Session["id"].ToString())
            {

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
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", @"
                    let alertDiv = document.getElementById('alertDiv');
                    let alertText = document.getElementById('alertText');
                    alertDiv.style.display = 'block';
                    alertText.innerText = 'لايمكنك المتابعة اذ لم تكن مسجل دخول';
                    alertDiv.classList.add('fadeAway');
                    setTimeout(() => {
                    alertDiv.style.display = 'none';
                    alertText.innerText = '';
                    alertDiv.classList.remove('fadeAway');
                    }, 4000)", true);
            }
        }

        protected void UnFollowUserPost_Click(object sender, EventArgs e)
        {
            if(Session["id"] != null)
            {

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
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", @"
                    let alertDiv = document.getElementById('alertDiv');
                    let alertText = document.getElementById('alertText');
                    alertDiv.style.display = 'block';
                    alertText.innerText = 'لايمكنك الغاء المتابعة اذ لم تكن مسجل دخول';
                    alertDiv.classList.add('fadeAway');
                    setTimeout(() => {
                    alertDiv.style.display = 'none';
                    alertText.innerText = '';
                    alertDiv.classList.remove('fadeAway');
                    }, 4000)", true);
            }
        }

        protected void UpVoted_Click(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                string checkifUseAlreadyUpvoted = @"select * from [Vote] where [postId] =" + Convert.ToInt32(postId.Value) + " and [userId] =" + Convert.ToInt32(Session["id"]) + "and [voteTypeId] = 1";
                SqlCommand cmdIfUserAlreadyVoted = new SqlCommand(checkifUseAlreadyUpvoted, con);
                SqlDataReader dr = cmdIfUserAlreadyVoted.ExecuteReader();
                if (dr.HasRows)
                {
                    con.Close();

                    con.Open();

                    string removeUpvote = @" update [Post]
                        set[upvoteCount] = [upvoteCount] - 1
                        where id = " + postId.Value + "; "
                  + @"delete from [Vote] where [postId] =" + postId.Value + " and [userId] =" + Session["id"] + ";";
                    SqlCommand cmdRemoveUpvote = new SqlCommand(removeUpvote, con);
                    cmdRemoveUpvote.ExecuteNonQuery();
                    int totalVotePost = Convert.ToInt32(postUpvotedown.InnerText);
                    postUpvotedown.InnerText = Convert.ToString(totalVotePost - 1);

                    upvoteIconPost.Attributes.Add("style", "color:black");

                    con.Close();
                }
                else
                {
                    con.Close();

                    con.Open();

                    string checkIfUserDownVoted = @"select * from [Vote]
                        where [postId] = @postId and [userId] = @userId and [voteTypeId] = @voteTypeId;";
                    SqlCommand checkIfUserDownVotedCmd = new SqlCommand(checkIfUserDownVoted, con);
                    checkIfUserDownVotedCmd.Parameters.AddWithValue("@postId", Convert.ToInt32(postId.Value));
                    checkIfUserDownVotedCmd.Parameters.AddWithValue("@userId", Convert.ToInt32(Session["id"]));
                    checkIfUserDownVotedCmd.Parameters.AddWithValue("@voteTypeId", 2);
                    SqlDataReader checkIfUserDownVotedCmdDR = checkIfUserDownVotedCmd.ExecuteReader();


                    if (checkIfUserDownVotedCmdDR.HasRows)
                    {
                        con.Close();
                        con.Open();
                        string deleteDownvote = @"delete from [Vote] 
                            where [postId] =" + postId.Value + " and [userId] =" + Session["id"] + @";

                            update [Post]
                            set [downvoteCount] = downvoteCount + 1
                            where id =" + postId.Value + " ;";
                        SqlCommand deletedownVote = new SqlCommand(deleteDownvote, con);
                        deletedownVote.ExecuteNonQuery();


                        downvoteIconPost.Attributes.Add("style", "color:black");



                    }

                    con.Close();
                    con.Open();
                    DateTime myDateTime = DateTime.Now;
                    string sqlFormattedDate = myDateTime.ToString("yyyy -MM-dd HH:mm:ss.fff");

                    string upVoteInsert = @"update [Post]
                    set [upvoteCount] = [upvoteCount] + 1
                    where id =" + postId.Value + ";"
                        + @"insert into [Vote]([postId],[userId],[voteTypeId],[creationDate])
                    values(@postId,@userId,1,@creationDate);";
                    SqlCommand cmd = new SqlCommand(upVoteInsert, con);
                    cmd.Parameters.AddWithValue("@postId", Convert.ToInt32(postId.Value));
                    cmd.Parameters.AddWithValue("@userId", Convert.ToInt32(Session["id"]));
                    cmd.Parameters.AddWithValue("@creationDate", sqlFormattedDate);
                    cmd.ExecuteNonQuery();
                    int totalVotePost = Convert.ToInt32(postUpvotedown.InnerText);
                    postUpvotedown.InnerText = Convert.ToString(totalVotePost + 1);
                  
                    upvoteIconPost.Attributes.Add("style", "color:orange");

                    con.Close();

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", @"
                    let alertDiv = document.getElementById('alertDiv');
                    let alertText = document.getElementById('alertText');
                    alertDiv.style.display = 'block';
                    alertText.innerText = 'لايمكنك التصويت اذ لم تكن مسجل دخول';
                    alertDiv.classList.add('fadeAway');
                    setTimeout(() => {
                    alertDiv.style.display = 'none';
                    alertText.innerText = '';
                    alertDiv.classList.remove('fadeAway');
                    }, 4000)", true);
            }
        }

        protected void DownVoted_Click(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                SqlConnection con = new SqlConnection(cs);

                con.Open();
                string checkifUseAlreadyUpvoted = @"select * from [Vote] where [postId] =" + Convert.ToInt32(postId.Value) + " and [userId] =" + Convert.ToInt32(Session["id"]) + "and [voteTypeId] = 2";
                SqlCommand cmdIfUserAlreadyVoted = new SqlCommand(checkifUseAlreadyUpvoted, con);
                SqlDataReader dr = cmdIfUserAlreadyVoted.ExecuteReader();
                if (dr.HasRows)
                {
                    con.Close();

                    con.Open();

                    string removeDownvote = @" update [Post]
                        set[downvoteCount] = [downvoteCount] + 1
                        where id = " + postId.Value + "; "
                  + @"delete from [Vote] where [postId] =" + postId.Value + " and [userId] =" + Session["id"] + ";";
                    SqlCommand cmdRemoveUpvote = new SqlCommand(removeDownvote, con);
                    cmdRemoveUpvote.ExecuteNonQuery();
                    int totalVotePost = Convert.ToInt32(postUpvotedown.InnerText);
                    postUpvotedown.InnerText = Convert.ToString(totalVotePost + 1);
                    
                    downvoteIconPost.Attributes.Add("style", "color:black");

                    con.Close();
                }
                else
                {
                    con.Close();

                    con.Open();

                    string checkIfUserUpVoted = @"select * from [Vote]
                        where [postId] = @postId and [userId] = @userId and [voteTypeId] = @voteTypeId;";
                    SqlCommand checkIfUserUpVotedCmd = new SqlCommand(checkIfUserUpVoted, con);
                    checkIfUserUpVotedCmd.Parameters.AddWithValue("@postId", Convert.ToInt32(postId.Value));
                    checkIfUserUpVotedCmd.Parameters.AddWithValue("@userId", Convert.ToInt32(Session["id"]));
                    checkIfUserUpVotedCmd.Parameters.AddWithValue("@voteTypeId", 1);
                    SqlDataReader checkIfUserUpVotedCmdDR = checkIfUserUpVotedCmd.ExecuteReader();


                    if (checkIfUserUpVotedCmdDR.HasRows)
                    {
                        con.Close();
                        con.Open();
                        string deleteUpvote = @"delete from [Vote] 
                            where [postId] =" + postId.Value + " and [userId] =" + Session["id"] + @";

                            update [Post]
                            set [upvoteCount] = upvoteCount - 1
                            where id =" + postId.Value + " ;";
                        SqlCommand deleteupVote = new SqlCommand(deleteUpvote, con);
                        deleteupVote.ExecuteNonQuery();


                        upvoteIconPost.Attributes.Add("style", "color:black");



                    }

                    con.Close();
                    con.Open();
                    DateTime myDateTime = DateTime.Now;
                    string sqlFormattedDate = myDateTime.ToString("yyyy -MM-dd HH:mm:ss.fff");

                    string downVoteInsert = @"update [Post]
                    set [downvoteCount] = [downvoteCount] - 1
                    where id =" + postId.Value + ";"
                        + @"insert into [Vote]([PostId],[userId],[voteTypeId],[creationDate])
                    values(@PostId,@userId,2,@creationDate);";
                    SqlCommand cmd = new SqlCommand(downVoteInsert, con);
                    cmd.Parameters.AddWithValue("@PostId", Convert.ToInt32(postId.Value));
                    cmd.Parameters.AddWithValue("@userId", Convert.ToInt32(Session["id"]));
                    cmd.Parameters.AddWithValue("@creationDate", sqlFormattedDate);
                    cmd.ExecuteNonQuery();
                    int totalVotePost = Convert.ToInt32(postUpvotedown.InnerText);
                    postUpvotedown.InnerText = Convert.ToString(totalVotePost - 1);
                  
                    downvoteIconPost.Attributes.Add("style", "color:orange");
                    upvoteIconPost.Attributes.Add("style", "color:black");


                    con.Close();

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", @"
                    let alertDiv = document.getElementById('alertDiv');
                    let alertText = document.getElementById('alertText');
                    alertDiv.style.display = 'block';
                    alertText.innerText = 'لايمكنك التصويت اذ لم تكن مسجل دخول';
                    alertDiv.classList.add('fadeAway');
                    setTimeout(() => {
                    alertDiv.style.display = 'none';
                    alertText.innerText = '';
                    alertDiv.classList.remove('fadeAway');
                    }, 4000)", true);
            }
        }

        protected void DeletePost(object sender, EventArgs e)
        {
            if(Session["id"].ToString() == userId.Value.ToString())
            {

                SqlConnection con = new SqlConnection(cs);

                con.Open();

                string deleteUserPostAnswers = @"delete from [Answer] where [postId] = @postId;";

                SqlCommand cmdAnswer = new SqlCommand(deleteUserPostAnswers, con);

                cmdAnswer.Parameters.AddWithValue("@postId", postId.Value);

                cmdAnswer.ExecuteNonQuery();
                con.Close();


            con.Open();

            string deleteUserPost = @"delete from [Post] where [id] = @postId and [userId] = @userId";

            SqlCommand cmd = new SqlCommand(deleteUserPost, con);

            cmd.Parameters.AddWithValue("@postId", postId.Value);
            cmd.Parameters.AddWithValue("@userId", userId.Value);
            cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", @"
                    let alertDiv = document.getElementById('alertDiv');
                    let alertText = document.getElementById('alertText');
                    alertDiv.style.display = 'block';
                    alertText.innerText = 'لست مصرح بالحذف';
                    alertDiv.classList.add('fadeAway');
                    setTimeout(() => {
                    alertDiv.style.display = 'none';
                    alertText.innerText = '';
                    alertDiv.classList.remove('fadeAway');
                    }, 4000)", true);
            }

        }

        protected void Answer(object sender, EventArgs e)
        {
            if(Session["id"] != null)
            {

            
            SqlConnection con = new SqlConnection(cs);

            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy -MM-dd HH:mm:ss.fff");
            con.Open();

            string answerInsert = @"insert into [Answer] ([postId], [userId],[answerText],[creationDate])
                                    values(@postId,@userId,@answerText,@creationDate);
                                    update [Post]
                                    set [answerCount] = answerCount + 1
                                    where id = @postId;";
            SqlCommand cmd = new SqlCommand(answerInsert, con);
            cmd.Parameters.AddWithValue("@postId", postId.Value);
            cmd.Parameters.AddWithValue("@userId", Session["id"]);
            cmd.Parameters.AddWithValue("@answerText", answerText.Text);
            cmd.Parameters.AddWithValue("@creationDate", sqlFormattedDate);

            cmd.ExecuteNonQuery();
            con.Close();
            con.Open();
            string updateReputation = @"update [User]
            set reputation = reputation + 1
            where id = @userId; ";
              SqlCommand updateReputCMD = new SqlCommand(updateReputation, con);
                updateReputCMD.Parameters.AddWithValue("@userId", Session["id"]);
                updateReputCMD.ExecuteNonQuery();
                con.Close();
            Response.Redirect(Request.RawUrl);
            }
            else
            {
            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", @"
             let alertDiv = document.getElementById('alertDiv');
             let alertText = document.getElementById('alertText');
             alertDiv.style.display = 'block';
             alertText.innerText = 'لاتستطيع الأجابة ان لم تكن مسجل دخول';
             alertDiv.classList.add('fadeAway');
             setTimeout(() => {
             alertDiv.style.display = 'none';
             alertText.innerText = '';
             alertDiv.classList.remove('fadeAway');
             }, 4000)", true);
            }
        }
    }
}


