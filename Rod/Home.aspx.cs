using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rod
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedIn"] == null)
            {
                HideIfUserLoggedIn.Visible = true;
                homeLoggedIn.Visible = false;
            }
            else
            {
                homeLoggedIn.Visible = true;
                HideIfUserLoggedIn.Visible = false;
            }
        }
    }
}