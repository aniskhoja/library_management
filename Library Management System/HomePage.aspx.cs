using Library_Management_System.Model;
using Library_Management_System.SQLOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library_Management_System
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Registration_Btn_Click(object sender, EventArgs e)
        {
            if (UsersData.GetByEmail(UserEmail.Text).Email == null)
            {
                User user = new User();
                user.Name = UserName.Text;
                user.Email = UserEmail.Text;

                UsersData.Add(user);
                Signupmsg.Visible = true;

            }
            else
            {
                registrationerror.Visible = true;
            }


        }

        protected void Sign_In_Click(object sender, EventArgs e)
        {
            User user = UsersData.GetByEmail(loginemail.Text);
            if (user.Email != null)
            {
                Session["UserId"] = user.Id;
                Response.Redirect("UserPortal.aspx");
            }
            else
            {
                yourarenotregister.Visible = true;
            }
        }

        protected void Admin_Page_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookManager.aspx");

        }
    }
}