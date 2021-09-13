using Library_Management_System.Model;
using Library_Management_System.SQLOperations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library_Management_System
{
    public partial class UserProtal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserId"] == null)
            {
                Response.Redirect("HomePage.aspx");
            }

            if (!IsPostBack)
            {
                FillUserManager();
                FillUserRentedBook();
            }
        }
        private void FillUserManager()
        {

            DataTable dtrslt = BookData.GetAll();

            UserPortalGrid.DataSource = dtrslt;
            UserPortalGrid.DataBind();
            ViewState["dirState"] = dtrslt;
            ViewState["sortdr"] = "Asc";

        }







        protected void UserPortalGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dtrslt = (DataTable)ViewState["dirState"];
            UserPortalGrid.PageIndex = e.NewPageIndex;
            if (ViewState["SortColumn"] != null)
            {
                dtrslt.DefaultView.Sort = ViewState["SortColumn"] + " " + ViewState["sortdr"];
            }
            UserPortalGrid.DataSource = dtrslt;
            UserPortalGrid.DataBind();
        }



        protected void Logout_Click(object sender, EventArgs e)
        {
            ViewState["UserId"] = "";
            Response.Redirect("HomePage.aspx");
        }

        protected void UserPortalGrid_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtrslt = (DataTable)ViewState["dirState"];
            if (dtrslt.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["sortdr"]) == "Asc")
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
                    ViewState["SortColumn"] = e.SortExpression;
                    ViewState["sortdr"] = "Desc";
                }
                else
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
                    ViewState["SortColumn"] = e.SortExpression;
                    ViewState["sortdr"] = "Asc";
                }
                UserPortalGrid.DataSource = dtrslt;
                ViewState["dirState"] = dtrslt;
                UserPortalGrid.DataBind();


            }
        }

        protected void UserPortalGrid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = (GridViewRow)UserPortalGrid.Rows[e.NewSelectedIndex];
            int bookid = Convert.ToInt32(UserPortalGrid.Rows[e.NewSelectedIndex].Cells[0].Text);

            Book book = BookData.GetbyId(bookid);


            if (book.Available == 0)
            {
                Error_msg.Visible = true;
            }
            else
            {
                Error_msg.Visible = false;
                BookData.Update(bookid, book.Quantity, book.Available - 1);
                UsersData.RentaBook(Convert.ToInt32(UserPortalGrid.Rows[e.NewSelectedIndex].Cells[0].Text), Convert.ToInt32(Session["UserId"]));

            }
            // UserPortalGrid.EditIndex = -1;
            FillUserManager();
            FillUserRentedBook();
        }

        protected void UserRentedList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Error_msg.Visible = false;
            GridViewRow row = (GridViewRow)UserRentedList.Rows[e.NewSelectedIndex];
            int bookid = Convert.ToInt32(UserRentedList.Rows[e.NewSelectedIndex].Cells[1].Text);

            Book book = BookData.GetbyId(bookid);

            UsersData.RetrunBook(Convert.ToInt32(UserRentedList.Rows[e.NewSelectedIndex].Cells[0].Text));

            BookData.Update(bookid, book.Quantity, book.Available + 1);

            FillUserRentedBook();
            FillUserManager();
       


        }

        private void FillUserRentedBook()
        {

            DataTable UserRentedBookdtrslt = UsersData.GetRentBookById(Convert.ToInt32(Session["UserId"]));

            UserRentedList.DataSource = UserRentedBookdtrslt;
            UserRentedList.DataBind();
            ViewState["UserRentedBookdtrslt"] = UserRentedBookdtrslt;


        }
    }
}