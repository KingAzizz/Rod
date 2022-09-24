using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Rod
{
    public partial class TopUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind();
            }
        }
        public void Bind()
        {
           
                string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pc\Documents\Rod\Rod\App_Data\Rod.mdf;Integrated Security=True";
                SqlConnection con = new SqlConnection(cs);

            string topUsersQuery = @"SELECT id,username,displayName,CAST(profileImage as nvarchar(255)) as profileImage,[location],[reputation],
              (SELECT TOP 2 tagName + ',' FROM TagFollowers 
               inner join [TagInfo] on tagId = TagInfo.id
               WHERE TagFollowers.userId = [User].id
               FOR XML PATH('')) [tagsFollowed]
               FROM [User]
               GROUP BY  id,username,displayName,CAST(profileImage as nvarchar(255)),[location],[reputation]
               ORDER BY [User].reputation DESC;";

            SqlCommand cmd = new SqlCommand(topUsersQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "User");
            usersListView.DataSource = ds.Tables[0];
            usersListView.DataBind();
        }
        public string CheckIfEmpty(string username,string displayName)
        {
            if(displayName != "")
            {
                return displayName;
            }
            else
            {
                return username;
            }
        }
    }
}