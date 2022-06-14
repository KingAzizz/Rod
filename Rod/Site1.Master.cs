using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rod
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["loggedIn"] == null)
            {
                profileImage.Visible = false;
             
                inboxLink.Text = "تسجيل";
                inboxLink.NavigateUrl = "Registration.aspx";
                inboxLink.Style.Add("color", "#4F6BFF");

                profileImageLink.Text = "دخول";
                profileImageLink.NavigateUrl = "Login.aspx";
                profileImageLink.Style.Add("color", "#4F6BFF");


            }
        }
    }
}