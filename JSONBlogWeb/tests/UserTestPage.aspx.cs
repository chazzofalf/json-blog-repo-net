using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class tests_UserTestPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CreateUser_Click(object sender, EventArgs e)
    {
        JSONBlog.JSONBlogUserCollection collection = new JSONBlog.JSONBlogUserCollection();
        collection.addUser(CreateUserName.Text, CreateUserPassword.Text);
    }
}