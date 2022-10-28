using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Rod
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Login", "login", "~/Login.aspx");
            routes.MapPageRoute("Registration", "register", "~/Registration.aspx");
            routes.MapPageRoute("Home", "", "~/Home.aspx");
            routes.MapPageRoute("askquestion", "ask/question", "~/QuestionForm.aspx");
            routes.MapPageRoute("postId", "question/{id}", "~/Question.aspx");
            routes.MapPageRoute("editPostId", "question/edit/{id}", "~/EditQuestion.aspx");
            routes.MapPageRoute("Tags", "tags", "~/Tags.aspx");
            routes.MapPageRoute("tagged", "tagged/{id}", "~/Tagged.aspx");
            routes.MapPageRoute("profile", "profile", "~/Profile.aspx");
            routes.MapPageRoute("editprofile", "profile/edit/{id}", "~/EditProfile.aspx");
            routes.MapPageRoute("viewUserProfile", "users/profile/{id}", "~/ViewUserProfile.aspx");
            routes.MapPageRoute("viewUserfollowing", "users/profile/{id}/following", "~/Following.aspx");
            routes.MapPageRoute("viewUserfollowers", "users/profile/{id}/followers", "~/Followers.aspx");
            routes.MapPageRoute("questions", "questions", "~/Questions.aspx");
            routes.MapPageRoute("following", "profile/following", "~/Following.aspx");
            routes.MapPageRoute("followers", "profile/followers", "~/Followers.aspx");
            routes.MapPageRoute("topUsers", "top/users", "~/TopUsers.aspx");
            routes.MapPageRoute("help", "help", "~/HelpCenter.aspx");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}