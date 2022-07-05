using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Rod
{
    public partial class Question : System.Web.UI.Page
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
                            
                            postTitle.InnerText = dr.GetValue(2).ToString();
                            postBody.InnerText = dr.GetValue(3).ToString();
                            postCreation.InnerText = RelativeDate(Convert.ToDateTime(dr.GetValue(5)));

                            postCreationUser.InnerText = RelativeDate(Convert.ToDateTime(dr.GetValue(5))) + "انسأل قبل";
                            postTagsDiv.InnerHtml = "<a href=#TAGGHERE>" + dr.GetValue(4).ToString() + "</a>";
                            userProfileImagePost.ImageUrl = dr.GetValue(24).ToString();
                            userLinkProfilePost.NavigateUrl = "#";
                            userLinkProfilePost.Text = dr.GetValue(11).ToString();
                            postUpvotedown.InnerText = dr.GetValue(8).ToString();
                        }
                        triggerd = true;

                        answersPost.InnerHtml +=
                          @"<section class='postAnswer'>
                    <div class='vote postVote'>
                    <div>
                    <span>
                        <i class='fa-solid fa-angle-up'></i> <input type = 'button' />
                    </span>
                    <span>" + dr.GetValue(39).ToString() + @" </span>
                    <span>
                        <i class='fa-solid fa-angle-down'></i> <input type = 'button' />
                    </ span >


                </div>


            </div>
            <div class='questionPost'>
                <p style='font-size:100%'>" + dr.GetValue(37).ToString() + @"</p>
                
            </div>
          </section>
          
          <div class='userPostDetails'>
              <div>
                <input type = 'button' value='متابعة'/>
                <button onclick = 'copyToClipboard(window.location.href)' > نشر </button>
            </div>
            <div>
                <div id='answeCreation'>"+ RelativeDate(Convert.ToDateTime(dr.GetValue(46)))+ @" </div>
                <div>
               
                <img src= '"+dr.GetValue(56).ToString() +@"' alt='no image' />
                
                    <a href='#userLink'>"+ dr.GetValue(43).ToString() + @" </a>
                </div>

            </div>
          </div>";
            
                    }
                }
                else
                {
                    con.Close();
                    con.Open();
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
    }
}


/* */








/*  "<section class='post'>" +
                         "<div class='vote postVote'>" +
                        "<div>" +
                        "<span>" +
                        "<i class='fa-solid fa-angle-up'></i> <input type = 'button' /> </span>" +

                    "<span>" + dr.GetValue(39).ToString() + " </ span >" +
                    "<span> <i class='fa-solid fa-angle-down'></i> <input type = 'button' />  </span></div>  </div>" +

             "<div class='questionPost'>" +
                "<p style='width:600px;'>" + dr.GetValue(37).ToString() + "</p></div></section>" +
          "<div class='userPostDetails'>" +
            "<div>" +
              "<input type = 'button' value='متابعة'/><button onclick = 'copyToClipboard(window.location.href)' > نشر </ button ></div>" +
          "<div>" +
              "<div> انسأل قبل 2 شهر</div>" +
              "<div>" +
              "<img src = './pro.jpeg' alt='user profile image' />" +
              "<a href = '#' >" + dr.GetValue(39).ToString() + " </a></div></div>" + "</div>";*/