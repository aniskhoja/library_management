<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookManager.aspx.cs" Inherits="Library_Management_System.BookManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; overflow: hidden;">
        <p>
         <asp:Button ID="Logout" OnClick="Logout_Click" runat="server" Text="Logout" />
            </p>
        <div style="width: 50%; margin-top: -7px; float: left;">
        <p>
            <asp:Label ID="AddnewBook" runat="server" Text="New Book" Font-Bold="True"></asp:Label>
        </p>
        <asp:Label ID="BookTitleLabel" runat="server" Text="BookTitle"></asp:Label>
        <asp:TextBox ID="BookTitle" runat="server"></asp:TextBox>
        <p>
            <asp:Label ID="Label3" runat="server" Text="Book Quantity"></asp:Label>
            <asp:TextBox ID="BookQuantity" runat="server"></asp:TextBox>
        </p>
        <asp:ListBox ID="Authorlist" Width="314px" SelectionMode="Multiple" runat="server" Height="124px"></asp:ListBox>
        <p>
            <asp:Button ID="AddBookbtn" runat="server" Text="Add Book" OnClick="AddBookbtn_Click" />
        </p>
            <p>
        <asp:Label ID="Label4" runat="server" Text="New Author" Font-Bold="True"></asp:Label>
                </p>
        <p>
            <asp:Label ID="Label5" runat="server" Text="Author Name"></asp:Label>
            <asp:TextBox ID="AuthorName" runat="server"></asp:TextBox>
        </p>
        <asp:Label ID="Label6" runat="server" Text="Author Email"></asp:Label>
        <asp:TextBox ID="AuthorEmail" runat="server"></asp:TextBox>
        <p>
            <asp:Button ID="Addauthor" runat="server" Text="Add Author" OnClick="Addauthor_Click" />
        </p>
            </div>
        <div style="margin-left: 50%; margin-top: 25px;">
            <p>
            <asp:Label ID="Label7" runat="server" Text="Book Record" Font-Bold="True"></asp:Label>
           </p>
            
        <asp:GridView ID="BookManagerGrid" runat="server" AllowSorting="True" AutoGenerateColumns="false" AllowPaging="true" OnSorting="BookGrid_Sorting"
            OnPageIndexChanging="OnPageBookGridChanging" PageSize="4"
            OnRowCancelingEdit="BookManagerGrid_RowCancelingEdit" OnRowDeleting="BookManagerGrid_RowDeleting"
            OnRowEditing="BookManagerGrid_RowEditing" OnRowUpdating="BookManagerGrid_RowUpdating">
            <Columns>
                 
               <asp:BoundField ReadOnly="true"  DataField="Id" HeaderText="Id" />

                <asp:BoundField ReadOnly="true" DataField="Title" HeaderText="Book Title" SortExpression="title" />
                <asp:BoundField ReadOnly="true" DataField="Available" HeaderText="Availablity" SortExpression="available" />
                <asp:BoundField DataField="Quantity" HeaderText="Quanitity" SortExpression="quantity" />

                <asp:TemplateField HeaderText="Auther">
                    <ItemTemplate>
                        
                        <asp:Listbox ID="AuthorList" runat="server" DataSource='<%#Library_Management_System.SQLOperations.BookData.GetAuthorsbyBookId((int)Eval("Id"))%>' DataTextField="NameEmail">
                            
                        </asp:Listbox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="User">
                    <ItemTemplate>
                        
                        <asp:Listbox ID="UserList" runat="server" DataSource='<%#Library_Management_System.SQLOperations.BookData.GetUsersbyBookId((int)Eval("Id"))%>' DataTextField="NameEmail">
                            
                        </asp:Listbox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:CommandField ShowEditButton="true" />
                <asp:CommandField ShowDeleteButton="true" />

            </Columns>

        </asp:GridView>
           
            <p>
                <asp:Label ID="Error_msg" runat="server" Text="You couldn't able set less quantity because book availability is zero ." ForeColor="Red" Visible="False"></asp:Label>
            </p>
            </div>
       
         
       
    </form>

    
</body>
</html>
