﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Rod
{
    public partial class Following : System.Web.UI.Page
    {
        public static string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aziz\source\repos\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var id = Page.RouteData.Values["id"];

                int num = -1;
                if (id != null)
                {
                    if (!int.TryParse(id.ToString(), out num))
                    {
                        Response.Redirect("~/");
                    }
                    if(Session["id"] == id)
                    {
                        Response.Redirect("~/profile/following");
                    }
                    follower.NavigateUrl = "~/users/profile/"+ id +"/followers";
                    following.NavigateUrl = "~/users/profile/"+ id +"/following";
                   
                }
                Bind();
            }
        }
        public void Bind()
        {
            var id = Page.RouteData.Values["id"];

            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string followersQuery = @"select userId,followingID,username,displayName,aboutMe, profileImage 
            from [Following]
            inner join [User] on [User].id = followingID
            where userId = @userId;";
            SqlCommand cmd = new SqlCommand(followersQuery, con);
            if(id == null)
            {

            cmd.Parameters.AddWithValue("@userId", Session["id"]);
            }
            else
            {
                cmd.Parameters.AddWithValue("@userId", id);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "Following");
            FollowedProfileListView.DataSource = ds.Tables[0];
            FollowedProfileListView.DataBind();
            /*if (askListView.Items.Count == 0)
            {
                questionsDivD.Visible = false;
            }*/
            con.Close();

        }
        protected void FollowedProfileListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HiddenField followerID = e.Item.FindControl("followerID") as HiddenField;
            Button unFollow = e.Item.FindControl("unFollow") as Button;
            Button follow = e.Item.FindControl("follow") as Button;
            if (Session["id"] != null)
            {
                if (Session["id"].ToString() == followerID.Value.ToString())
                {
                    follow.Visible = false;
                    unFollow.Visible = false;
                }
                else
                {

             
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                string checkIfUserFollow = @"select * from [Following]
            where userId = @userId and followingID = @followingID";
                SqlCommand cmd = new SqlCommand(checkIfUserFollow, con);
                cmd.Parameters.AddWithValue("@userId", Session["id"]);
                cmd.Parameters.AddWithValue("@followingID", followerID.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    unFollow.Visible = true;
                }
                else
                {
                    follow.Visible = true;
                }
                con.Close();
                }
            }
            else
            {
                follow.Visible = true;
            }
        }

        protected void FollowedProfileListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            Button unFollow = e.Item.FindControl("unFollow") as Button;
            Button follow = e.Item.FindControl("follow") as Button;
            SqlConnection con = new SqlConnection(cs);
            if (e.CommandArgument != Session["id"])
            {
                if (e.CommandName == "unFollow")
                {
                    con.Open();
                    string unFollowStatement = @"
                delete from [Following]
                where userId = @userId and followingID = @followerID;
                delete from [Followers]
                where userId = @followerID and followerID = @userId;";
                    SqlCommand cmd = new SqlCommand(unFollowStatement, con);
                    cmd.Parameters.AddWithValue("@userId", Session["id"]);
                    cmd.Parameters.AddWithValue("@followerID", e.CommandArgument);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    follow.Visible = true;
                    unFollow.Visible = false;
                }
                if (e.CommandName == "follow")
                {
                    if (Session["id"] != null)
                    {
                        con.Open();
                        string followStatement = @"
                insert into [Following] (userId,followingID) 
                values(@userId,@followerID);
                insert into [Followers] (userId,followerID)
                values(@followerID,@userId)";
                        SqlCommand cmd = new SqlCommand(followStatement, con);
                        cmd.Parameters.AddWithValue("@userId", Session["id"]);
                        cmd.Parameters.AddWithValue("@followerID", e.CommandArgument);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        follow.Visible = false;
                        unFollow.Visible = true;
                    }
                }
            }
        }
    }
}