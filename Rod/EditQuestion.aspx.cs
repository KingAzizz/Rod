﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Rod
{
    public partial class EditQuestion : System.Web.UI.Page
    {
        public static string cs = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = string.Format("تعديل السؤال");
            if (!Page.IsPostBack)
            {

            
            var id = Page.RouteData.Values["id"];
  
            SqlConnection con = new SqlConnection(cs);

            
            int num = -1;
            if (id != null && Session["id"] != null)
            {
                if (!int.TryParse(id.ToString(), out num))
                {
                    Response.Redirect("~/");


                }

                con.Open();
                string selectEditedQuestion = @"select [id],[userId],[title],[body],[tag]
                    from [Post]
                    where id =" + id.ToString();
                SqlCommand cmd = new SqlCommand(selectEditedQuestion, con);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        titleText.Text = dr.GetValue(2).ToString();
                        bodyText.Text = dr.GetValue(3).ToString();
                        currentTag.Text =  dr.GetValue(4).ToString();
                        if (Session["id"].ToString() != dr.GetValue(1).ToString())
                        {
                            Response.Redirect("~/");
                        }
                    }
                 
                }

            }
                else
                {
                    Response.Redirect("~/login");
                }
            }
        }
    

            protected void SaveChanges(object sender, EventArgs e)
             {
            SqlConnection con = new SqlConnection(cs);
            var id = Page.RouteData.Values["id"];
            int num = -1;
            if (id != null)
            {
                if (!int.TryParse(id.ToString(), out num))
                {
                    Response.Redirect("~/");


                }
                if (tagseditDropdownlist.SelectedValue == "0")
                {
                    con.Open();

                    string editInsert = @"update [Post] 
                    set [title] = @title,
                    [body] = @body
                    where id =" + id.ToString();
                    SqlCommand cmd = new SqlCommand(editInsert, con);
                    cmd.Parameters.AddWithValue("@title", titleText.Text);
                    cmd.Parameters.AddWithValue("@body", bodyText.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("~/question/" + id.ToString());
                }
                else
                {
                    con.Open();
                    string deleteTag = @"delete from [PostTags]
                    where [postId] = @postId;
                    UPDATE [Post] SET tag = '' where [Post].id= @postId;";

                    SqlCommand cmdDelete = new SqlCommand(deleteTag, con);
                    cmdDelete.Parameters.AddWithValue("@postId", id.ToString());
                    cmdDelete.ExecuteNonQuery();
                    con.Close();
                    string str = tagseditDropdownlist.SelectedValue;

                    con.Open();
                    
                 
                    string findTags = @"select id from tagInfo
                    where tagName = N'" + str + "'";

                    SqlCommand findTagsCmd = new SqlCommand(findTags, con);
                    SqlDataReader findTagsDr = findTagsCmd.ExecuteReader();
                    if (findTagsDr.HasRows)
                    {
                        string tagId = "";
                        while (findTagsDr.Read())
                        {
                            
                           tagId = findTagsDr.GetValue(0).ToString();
                            break;
                         
                        }
                        con.Close();
                        con.Open();
                        string insertTags = @"insert into [PostTags] ([postId],[tagId])
                        values(@postId,@tagId);";
                       
                        SqlCommand insertTagsCmd = new SqlCommand(insertTags, con);
                        insertTagsCmd.Parameters.AddWithValue("@postId", id);
                        insertTagsCmd.Parameters.AddWithValue("@tagId", tagId);
                        insertTagsCmd.ExecuteNonQuery();
                        con.Close();

                    }
                    else
                    {
                        Response.Write("invaild tag");
                    }


                
                con.Close();
                con.Open();
                
                string editInsert = @"update [Post] 
                set [title] = @title,
                [body] = @body,
                [tag] = @tag
                where id =" + id.ToString();
                SqlCommand cmd = new SqlCommand(editInsert, con);
                cmd.Parameters.AddWithValue("@title", titleText.Text);
                cmd.Parameters.AddWithValue("@body", bodyText.Text);
                cmd.Parameters.AddWithValue("@tag", str);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/question/" + id.ToString());
                }
            }
        }

        protected void CancelChanges(object sender, EventArgs e)
        {
            var id = Page.RouteData.Values["id"];
            int num = -1;
            if (id != null)
            {
                if (!int.TryParse(id.ToString(), out num))
                {
                    Response.Redirect("~/");


                }
                Response.Redirect("~/question/" + id.ToString());
            }
               
        }
    }
}