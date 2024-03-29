﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Rod
{
    public partial class Tags : System.Web.UI.Page
    {
        public void Bind()
        {
            string cs = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string viewTags = "select * from [TagInfo]";

            SqlCommand cmd = new SqlCommand(viewTags, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "TagInfo");
            tagsListView.DataSource = ds.Tables[0];
            tagsListView.DataBind();
            con.Close();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = string.Format("الاقسام");
            if (!Page.IsPostBack)
            {
                Bind();
            }
        }
    }
}