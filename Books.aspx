<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="Bibliotheque.Books" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <style>
        input[type=file]
        {
            width:90px;
            color:transparent;
        }
        </style>

    <div style="text-align:center">
        <h1>Caută carte</h1>
    </div>
    <div style="padding-left:200px">
        Caută:<asp:Textbox ID="search" runat="server"></asp:Textbox>
        Disponibile:<asp:CheckBox ID="chkAvailable" runat="server" Checked="true" />
        Împrumutate:<asp:CheckBox ID="chkBorrowed" runat="server" Checked="true" />
        Caută după categorie:  
        <asp:DropDownList ID="ddlBookCategory" runat="server" class="selectpicker">
        </asp:DropDownList>
        <asp:LinkButton ID="btnSearch" 
            runat="server" 
            CssClass="btn btn-primary"    
            OnClick="btnSearch_Click">
            <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
        </asp:LinkButton>
        
    </div>

    <div>
        <asp:GridView ID="gridBooks" runat="server"
            AutoGenerateColumns="False"
            CellPadding="4" ForeColor="#333333" GridLines="None"
            CssClass="table table-condensed" Width="98%"
            DataKeyNames="idlivre"
            AllowPaging="True"
            HorizontalAlign="Center"
            OnPageIndexChanging="gridview_PageIndexChanging"
            OnRowCancelingEdit="gridview_RowCancelingEdit"
            OnRowCommand="gridview_OnRowCommand"
            OnRowDataBound="gridBooks_RowDataBound"
            OnRowEditing="gridview_RowEditing"
            OnRowUpdating="gridview_RowUpdating"
            OnRowDeleting ="gridBooks_RowDeleting" 
            PagerSettings-FirstPageText="First Page"
            PagerSettings-NextPageText="Next Page&nbsp;"
            PagerSettings-PreviousPageText="&nbsp;Previous Page &nbsp; "
            PagerSettings-LastPageText="&nbsp; Last Page &nbsp;" 
            PagerSettings-Mode="NextPreviousFirstLast">
            <Columns>
                <asp:TemplateField HeaderText="ID book">
                    <ItemTemplate>
                        <asp:Label ID="lblIdBook" runat="server" Text='<%# Bind("idlivre") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Registered code" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate>
                         <asp:Label ID="lblCodeISBN" runat="server" Text='<%# Bind("codeISBN") %>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtcodeISBNEdit" runat="server" Text='<%# Bind("codeISBN") %>' Width="100px"></asp:TextBox>
                     </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Initial code">
                     <ItemTemplate>
                         <asp:Label ID="lblInitialCode" runat="server" Text='<%# Bind("initialcode") %>' ></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtInitialCode" runat="server" Text='<%# Bind("initialcode") %>' Width="50px"></asp:TextBox>
                     </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Title">
                     <ItemTemplate>
                         <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("title") %>' Width="100px"></asp:TextBox>*
                     </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Author">
                     <ItemTemplate>
                         <asp:Label ID="lblAuthor" runat="server" Text='<%# Bind("author") %>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtAuthor" runat="server" Text='<%# Bind("author") %>' Width="100px"></asp:TextBox>
                     </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Description">
                     <ItemTemplate>
                         <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("description") %>'></asp:TextBox>
                     </EditItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <asp:Label ID="lblCategory" runat="server" Text='<%#Bind("category") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlCategory" runat="server" class="selectpicker" 
                            DataSourceID="SqlDataSourceCategories" DataTextField="category" DataValueField="category" 
                            SelectedValue='<%# Bind("category") %>'>
                        </asp:DropDownList>
                    </EditItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Image">
                     <ItemTemplate>
                         <asp:Image ID="image" runat="server" Height="100px" Width="75px"  
                             AlternateText=" " 
                             ImageUrl='<%# "ImageHandler.ashx?IdLivre="+ Eval("idlivre") %>'
                             DescriptionUrl='<%# Eval("photo") %>'>
                    </asp:Image>
                     </ItemTemplate>
                     <EditItemTemplate>
                        <asp:FileUpload ID="FileUpload" runat="server"  />
                     </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Extra info">
                     <ItemTemplate>
                         <asp:Label ID="lblExtrainfo" runat="server" Text='<%# Bind("extrainfo") %>' ></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtExtrainfo" runat="server" Text='<%# Bind("extrainfo") %>' Width="150px"></asp:TextBox>
                     </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Price">
                     <ItemTemplate>
                         <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("price") %>' ></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtPrice" runat="server" Text='<%# Bind("price") %>' Width="30px"></asp:TextBox>
                     </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Pages">
                     <ItemTemplate>
                         <asp:Label ID="lblPages" runat="server" Text='<%# Bind("pages") %>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtPages" runat="server" Text='<%# Bind("pages") %>' Width="30px"></asp:TextBox>
                     </EditItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="Availaility">
                     <ItemTemplate>
                         <asp:Label ID="lblAvailability" runat="server" Text='<%# Bind("availability") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Modify">
                    <ItemTemplate>
                        <asp:Button id="btnModify" runat="server" Text="Modify" 
                            CommandName="Modify" 
                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button id="btnUpdate" runat="server" Text="Update" 
                            CommandName="Update" 
                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                            CssClass="btn-link"/>
                         <asp:Button id="btnCancel" runat="server" Text="Cancel" 
                            CommandName="CancelUpdate" 
                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                            CssClass="btn-link"/>
                    </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Delete">
                     <ItemTemplate>
                         <asp:Button ID="btnDelete" runat="server" 
                             CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                             CommandName="Delete"
                             Text="Delete" />
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Archive">
                     <ItemTemplate>
                         <asp:Button ID="btnCaseaza" runat="server" 
                             CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                             CommandName="SendToCasare"
                             Text="Archive" />
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
        <asp:Label ID="lblMessage" runat="server" />
        <asp:SqlDataSource ID="SqlDataSourceCategories" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connectionbiblio %>" 
            SelectCommand="EXEC [getUsedCategories] 0"></asp:SqlDataSource>
    </div>
    <asp:Hiddenfield ID="hfCarteCasata" runat="server" />
</asp:Content>
