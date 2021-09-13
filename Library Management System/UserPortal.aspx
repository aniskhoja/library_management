<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPortal.aspx.cs" Inherits="Library_Management_System.UserProtal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        #UserRentedList tr th:first-child,#UserRentedList tr td:first-child,#UserRentedList tr th:nth-child(2),#UserRentedList tr td:nth-child(2){
            display:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="Logout" OnClick="Logout_Click" runat="server" Text="LogOut" />
        <p>
            <asp:Label ID="Label1" runat="server" Text="List of Book" Font-Bold="True"></asp:Label>
        </p>
        <asp:GridView ID="UserPortalGrid" runat="server" AllowSorting="True" AutoGenerateColumns="false" AllowPaging="true" OnSorting="UserPortalGrid_Sorting"
            PageSize="4" OnPageIndexChanging="UserPortalGrid_PageIndexChanging" OnSelectedIndexChanging="UserPortalGrid_SelectedIndexChanging">
            <Columns>
                <asp:BoundField ReadOnly="true" DataField="Id" HeaderText="Id" />
                <asp:BoundField ReadOnly="true" DataField="Title" HeaderText="Book Title" SortExpression="title" />
                <asp:BoundField ReadOnly="true" DataField="Available" HeaderText="Availablity" SortExpression="available" />
                <asp:BoundField DataField="Quantity" HeaderText="Quanitity" SortExpression="quantity" />

                <asp:TemplateField HeaderText="Auther">
                    <ItemTemplate>

                        <asp:ListBox ID="AuthorList" runat="server" DataSource='<%#Library_Management_System.SQLOperations.BookData.GetAuthorsbyBookId((int)Eval("Id"))%>' DataTextField="Name"></asp:ListBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:CommandField SelectText="Rent a Book" ShowSelectButton="true" />


            </Columns>

        </asp:GridView>

        <asp:Label ID="Error_msg" runat="server" ForeColor="#FF3300" Text="Book is not Available for rent" Visible="False"></asp:Label>


        <p>
            <asp:Label ID="Label2" runat="server" Text="List of Rented Book" Font-Bold="True"></asp:Label>
        </p>
        <asp:GridView ID="UserRentedList" runat="server" AutoGenerateColumns="false"
            OnSelectedIndexChanging="UserRentedList_SelectedIndexChanging">
            <Columns>
                <asp:BoundField ReadOnly="true" DataField="Id" HeaderText="Id" />
                <asp:BoundField ReadOnly="true" DataField="BookId" HeaderText="Book Id" />
                <asp:BoundField ReadOnly="true" DataField="Title" HeaderText="Book Title" />
                <asp:BoundField DataField="BorrowDate" HeaderText="Borrow Date and Time" />



                <asp:CommandField SelectText="Retrun this Book" ShowSelectButton="true" />


            </Columns>

        </asp:GridView>
    </form>
</body>
</html>
