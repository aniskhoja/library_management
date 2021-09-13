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
    public partial class BookManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                Authorlist.DataSource = AuthorData.GetAll();
                Authorlist.DataValueField = "Id";
                Authorlist.DataTextField = "NameEmail";
                Authorlist.DataBind();
                Authorlist.SelectionMode = ListSelectionMode.Multiple;

                FillBookManager();
            }

            
        }

        protected void AddBookbtn_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            book.Title = BookTitle.Text;
            book.Quantity = Convert.ToInt32(BookQuantity.Text);

            int Bookid = BookData.Add(book);

            foreach (int listboxIndex in Authorlist.GetSelectedIndices())
            {
                BookData.AddBookAuthor(Convert.ToInt32(Authorlist.Items[listboxIndex].Value), Bookid);

            }

            BookTitle.Text = "";
            BookQuantity.Text = "";
            FillBookManager();
        }

        protected void Addauthor_Click(object sender, EventArgs e)
        {
            Author author = new Author();
            author.Name = AuthorName.Text;
            author.Email = AuthorEmail.Text;

            AuthorData.Add(author);

            AuthorName.Text = "";
            AuthorEmail.Text = "";

            Authorlist.DataSource = AuthorData.GetAll();
            Authorlist.DataValueField = "Id";
            Authorlist.DataTextField = "NameEmail";
            Authorlist.DataBind();
            Authorlist.SelectionMode = ListSelectionMode.Multiple;
        }

        private void FillBookManager()
        {

            DataTable dtrslt = BookData.GetAll();

            BookManagerGrid.DataSource = dtrslt;
            BookManagerGrid.DataBind();
            ViewState["dirState"] = dtrslt;
            ViewState["sortdr"] = "Asc";

        }

        protected void BookManagerGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)BookManagerGrid.Rows[e.RowIndex];
           
            BookData.Delete(Convert.ToInt32(BookManagerGrid.Rows[e.RowIndex].Cells[0].Text));

            FillBookManager();
        }
        protected void BookManagerGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            BookManagerGrid.EditIndex = e.NewEditIndex;
            FillBookManager();
        }
        protected void BookManagerGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)BookManagerGrid.Rows[e.RowIndex];

            TextBox qty = (TextBox)row.Cells[3].Controls[0];

            int quantity = Convert.ToInt32(qty.Text);
            int bookId = Convert.ToInt32(BookManagerGrid.Rows[e.RowIndex].Cells[0].Text);
            Book book =BookData.GetbyId(bookId);

            int quantitydif = 0;
            int tempavailability = 0;
            if (book.Quantity > quantity && book.Available == 0 ) 
            {
                Error_msg.Text = "You couldn't able set less quantity because of book availability.";
                Error_msg.Visible = true;
                
            }
            else if (book.Quantity < quantity)
            {
                quantitydif = quantity - book.Quantity;

                tempavailability = book.Available + quantitydif;
                BookData.Update(bookId, quantity, tempavailability);
            }
            else
            {
                quantitydif = book.Quantity - quantity;
                tempavailability = book.Available - quantitydif;
                if(tempavailability<0)
                {
                    Error_msg.Text = "You couldn't able set less quantity because of book availability.";
                    Error_msg.Visible = true;
                }
                else
                {
                    BookData.Update(bookId, quantity, tempavailability);
                }
                
            }

           
            BookManagerGrid.EditIndex = -1;
            FillBookManager();

        }
        protected void BookManagerGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            BookManagerGrid.EditIndex = -1;
            FillBookManager();
        }

        protected void OnPageBookGridChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dtrslt = (DataTable)ViewState["dirState"];
            BookManagerGrid.PageIndex = e.NewPageIndex;
            if (ViewState["SortColumn"] != null)
            {
                dtrslt.DefaultView.Sort = ViewState["SortColumn"] + " " + ViewState["sortdr"];
            }
            BookManagerGrid.DataSource = dtrslt;
            BookManagerGrid.DataBind();
          
        }

        protected void BookGrid_Sorting(object sender, GridViewSortEventArgs e)
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
                BookManagerGrid.DataSource = dtrslt;
                ViewState["dirState"] = dtrslt;
                BookManagerGrid.DataBind();


            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");

        }

      
    }
}
