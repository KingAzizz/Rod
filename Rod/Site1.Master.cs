﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace Rod
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
   
            if(Session["id"] == null)
            {
                profileImage.Visible = false;
             
                inboxLink.Text = "تسجيل";
                inboxLink.NavigateUrl = "~/register";
                inboxLink.Style.Add("color", "#4F6BFF");

                loginLink.Visible = true;
                profile.Visible = false;
                loginHyLink.Style.Add("color", "#4F6BFF");
                

            }
            else {
                    loginLink.Visible = false;
                    profile.Visible = true;
                    string cs = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                    SqlConnection con = new SqlConnection(cs);
                con.Open();

                string getImage = @"select [profileImage] from [User] where [id] =" + Session["id"].ToString();

                SqlCommand cmd = new SqlCommand(getImage, con);

                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows){
                    while (dr.Read())
                    {
                        profileImage.ImageUrl = "~/" + dr.GetValue(0).ToString();
                    }
                }

                }
            }
        }
        protected void SearchTrigger(object sender, EventArgs e)
        {
            Response.Redirect("~/SearchResult.aspx?searched=" + searchText.Text);
        }
        protected void Logout(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/login");
        }
    }
}