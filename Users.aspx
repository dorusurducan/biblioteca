<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Bibliotheque.Users" %>
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
        <h1>Users activity</h1>
    </div>
    <div style="padding-left:200px" id="divSearch" runat="server">
        Search user:<asp:Textbox ID="txtSearchUser" runat="server"></asp:Textbox>
        search book:<asp:Textbox ID="txtSearchBook" runat="server"></asp:Textbox>
        <asp:LinkButton ID="btnSearch" 
            runat="server" 
            CssClass="btn btn-primary"    
            OnClick="btnSearch_Click">
            <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
        </asp:LinkButton>
    </div>
    <div>
      <asp:GridView ID="gridBorrowActivity" runat="server" AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Center" 
            ForeColor="#333333" GridLines="None" CssClass="table table-condensed" Width="98%" 
            OnRowDataBound="GridViewBooks_RowDataBound" AllowPaging="true" OnPageIndexChanging="gridview_PageIndexChanging"
            OnRowCommand="gridview_OnRowCommand" PagerSettings-FirstPageText="First Page" PagerSettings-NextPageText="Next Page&nbsp;" 
            PagerSettings-PreviousPageText="&nbsp;Previous Page &nbsp; " PagerSettings-LastPageText="&nbsp; Last Page &nbsp;" 
            PagerSettings-Mode=" NextPreviousFirstLast">  
        <AlternatingRowStyle BackColor="White" />  
        <Columns>
            <asp:TemplateField HeaderText="Username">   
                <ItemTemplate>  
                    <asp:Label ID="username" runat="server" Text='<%#Bind("username") %>'></asp:Label>  
                </ItemTemplate>  
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="Class">   
                <ItemTemplate>  
                    <asp:Label ID="clasa" runat="server" Text='<%#Bind("clasa") %>'></asp:Label>  
                </ItemTemplate>  
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ID Book">   
                <ItemTemplate>  
                    <asp:Label ID="idlivre" runat="server" Text='<%#Bind("idlivre") %>'></asp:Label>  
                </ItemTemplate>  
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="Code book">   
                    <ItemTemplate>  
                        <asp:Label ID="idinitialcode" runat="server" Text='<%#Bind("initialcode") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>   
            <asp:TemplateField HeaderText="Title">  
                <ItemTemplate>  
                    <asp:Label ID="title" runat="server" Text='<%#Bind("title") %>'></asp:Label>  
                </ItemTemplate>  
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="Author">
                <ItemTemplate>
                    <asp:Label ID="author" runat="server" Text='<%#Bind("author") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:Label ID="description" runat="server"  Text='<%#Bind("description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <asp:Image ID="image" runat="server" AlternateText=" " 
                        ImageUrl='<%# "ImageHandler.ashx?IdLivre="+ Eval("idlivre") %>' Height="100px" Width="75px"
                        DescriptionUrl='<%# Eval("photo") %>'>
                    </asp:Image>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Availability">
                <ItemTemplate>
                    <asp:Label ID="lblAvailability" runat="server" Text='<%#Bind("availability") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Borrow date">
                <ItemTemplate>
                    <asp:Label ID="lblBorrowdate" runat="server" Text='<%#Bind("creationdate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Return date">
                <ItemTemplate>
                    <asp:Label ID="lblReturndate" runat="server" Text='<%#Bind("returndate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Real return date">
                <ItemTemplate>
                    <asp:Label ID="lblRealeturndate" runat="server" Text='<%#Bind("realreturndate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Borrow days left">
                <ItemTemplate>
                    <asp:Label ID="lblDaysfeft" runat="server" Text='<%#Bind("daysleft") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>  
        <EditRowStyle BackColor="#2461BF" />  
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
     </div>
</asp:Content>
