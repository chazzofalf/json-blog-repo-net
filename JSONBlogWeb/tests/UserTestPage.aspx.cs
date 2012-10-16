using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
public partial class tests_UserTestPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CreateUser_Click(object sender, EventArgs e)
    {
        JSONBlog.JSONBlogUserCollection collection = new JSONBlog.JSONBlogUserCollection();
        try
        {
            collection.addUser(CreateUserName.Text, CreateUserPassword.Text);
            CreateUserStatus.ForeColor = Color.Black;
            CreateUserStatus.Text = "User successfully created";
        }
        catch (Exception e2)
        {
            CreateUserStatus.ForeColor = Color.Red;
            CreateUserStatus.Text = e2.Message;
        }
        
    }
    protected void Login_Click(object sender, EventArgs e)
    {
        JSONBlog.JSONBlogUserCollection collection = new JSONBlog.JSONBlogUserCollection();
        try
        {
            collection.login(LoginUserName.Text, LoginPassword.Text);
        }
        catch (Exception e2)
        {
            LoginStatus.ForeColor = Color.Red;
            LoginStatus.Text =  e2.Message;
        }
    }
}