<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="Bibliotheque.AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
     <div style="text-align:center">
        <h1>Users management</h1>
    </div>
    <div style="padding-left:200px">
        Search:<asp:Textbox ID="search" runat="server"></asp:Textbox>
        <asp:LinkButton ID="btnSearch" 
            runat="server" 
            CssClass="btn btn-primary"    
            OnClick="btnSearch_Click">
            <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
        </asp:LinkButton>
    </div>
    
    <div>
        <asp:GridView ID="gridUsers" runat="server"
            AutoGenerateColumns="False"
            CellPadding="4" ForeColor="#333333" GridLines="None"
            CssClass="table table-condensed" Width="98%"
            DataKeyNames="iduser"
            AllowPaging="True"
            HorizontalAlign="Center"
            OnPageIndexChanging="gridview_PageIndexChanging"
            OnRowCancelingEdit="gridview_RowCancelingEdit"
            OnRowCommand="gridview_OnRowCommand"
            OnRowEditing="gridview_RowEditing"
            OnRowUpdating="gridview_RowUpdating"
            PagerSettings-FirstPageText="First Page"
            PagerSettings-NextPageText="Next Page&nbsp;"
            PagerSettings-PreviousPageText="&nbsp;Previous Page &nbsp; "
            PagerSettings-LastPageText="&nbsp; Last Page &nbsp;" 
            PagerSettings-Mode="NextPreviousFirstLast" OnRowDeleting="gridUsers_RowDeleting">
            <Columns>
                 <asp:TemplateField HeaderText="ID user">
                     <ItemTemplate>
                         <asp:Label ID="lblIdUser" runat="server" Text='<%# Bind("iduser") %>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:Label ID="lblIdUserEdit" runat="server" Text='<%# Bind("iduser") %>'></asp:Label>
                     </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="username">
                     <ItemTemplate>
                         <asp:Label ID="lblUsername" runat="server" Text='<%# Bind("username") %>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtUsername" runat="server" Text='<%# Bind("username") %>' Width="100px"></asp:TextBox>
                     </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Name">
                     <ItemTemplate>
                         <asp:Label ID="lblName" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("name") %>' Width="100px"></asp:TextBox>
                     </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Surname">
                     <ItemTemplate>
                         <asp:Label ID="lblSurname" runat="server" Text='<%# Bind("surname") %>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtSurname" runat="server" Text='<%# Bind("surname") %>' Width="100px"></asp:TextBox>
                     </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Class">
                     <ItemTemplate>
                         <asp:Label ID="lblClasa" runat="server" Text='<%# Bind("clasa") %>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtClasa" runat="server" Text='<%# Bind("clasa") %>'></asp:TextBox>
                     </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Password">
                     <ItemTemplate>
                         <asp:Button runat="server" ID="btnResetPass" Text="Reset" 
                             CommandName="ResetPassword" 
                             CommandArgument="<%# Container.DataItemIndex %>"/>
                     </ItemTemplate>
                     <EditItemTemplate>
                         New password: <asp:TextBox ID="txtPassword" runat="server" Width="90px"></asp:TextBox>
                         Confirm: <asp:TextBox ID="txtConfirmPassword" runat="server" Width="90px"></asp:TextBox>
                         <asp:Button runat="server" id="btnUpdatePassword" Text="Update" CommandName="UpdatePassword" CssClass="btn-link"/>
                         <asp:Button runat="server" ID="btnCancelReset" Text="Cancel" CommandName="CancelUpdate" CssClass="btn-link"/>
                     </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Role">
                     <ItemTemplate>
                         <asp:Label ID="lblRole" runat="server" Text='<%# Bind("rol") %>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:DropDownList ID="ddlRol" runat="server" class="selectpicker" 
                            DataSourceID="SqlDataSourceRoles" DataTextField="rol" DataValueField="rolValue" 
                            SelectedValue='<%# Bind("rol") %>'>
                        </asp:DropDownList>
                     </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Creation date">
                     <ItemTemplate>
                         <asp:Label ID="lblCreationdate" runat="server" Text='<%# Bind("creationdate") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Modify">
                    <ItemTemplate>
                        <asp:Button id="btnModify" runat="server" Text="Modify" 
                            CommandName="Modify" 
                            CommandArgument="<%# Container.DataItemIndex %>"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button id="btnUpdate" runat="server" Text="Update" 
                            CommandName="Update" 
                            CommandArgument="<%# Container.DataItemIndex %>"
                            CssClass="btn-link"/>
                         <asp:Button id="btnCancel" runat="server" Text="Cancel" 
                            CommandName="CancelUpdate" 
                            CommandArgument="<%# Container.DataItemIndex %>"
                            CssClass="btn-link"/>
                    </EditItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" 
                            CommandName="Delete"
                            CommandArgument="<%# Container.DataItemIndex %>"/>
                    </ItemTemplate>
                </asp:TemplateField>
             </Columns>
             <AlternatingRowStyle BackColor="White" />
             <EditRowStyle BackColor="#c1c5cc" />
             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
             <RowStyle BackColor="#EFF3FB" />
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
             <SortedAscendingCellStyle BackColor="#F5F7FB" />
             <SortedAscendingHeaderStyle BackColor="#6D95E1" />
             <SortedDescendingCellStyle BackColor="#E9EBEF" />
             <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceRoles" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connectionbiblio %>" 
            SelectCommand="SELECT 'Admin' as rol,       'admin' as rolValue UNION
                           SELECT 'Librarian' as rol,   'librarian' as rolValue UNION
                           SELECT 'User' as rol,        'user' as rolValue"></asp:SqlDataSource>
    </div>
    <div style="padding-left:10px">
        <asp:Button OnClick="Btn_CreateUser_Click" Text="Create user" ID="Create_user" runat="server" CssClass="btn btn-primary"/>
        <asp:Button ID="btn_UserActivity" runat="server" Text="Users activity" OnClick="btn_UserActivity_Click" CssClass="btn btn-primary"/>
        <asp:Button OnClick="Btn_AddBook_Click" Text="Add book" ID="Add_book" runat="server" CssClass="btn btn-primary"/>
     </div>
</asp:Content>
