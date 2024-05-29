<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="NewBooks.aspx.cs" Inherits="Bibliotheque.NewBooks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    
    <div style="padding-left:200px">
        Search book:
        <asp:Textbox ID="search" runat="server"></asp:Textbox>
        <asp:LinkButton ID="btnSearch" 
            runat="server" 
            CssClass="btn btn-primary"    
            OnClick="btnSearch_Click">
            <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
        </asp:LinkButton>
    </div>

    <asp:GridView ID="grid1" runat="server" AutoGenerateColumns="False" CellPadding="4"
        HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
        CssClass="table table-condensed" Width="98%" 
        PagerSettings-FirstPageText="First Page" PagerSettings-NextPageText="Next Page&nbsp;"
        PagerSettings-PreviousPageText="&nbsp;Previous Page &nbsp; "
        PagerSettings-LastPageText="&nbsp; Last Page &nbsp;"
        PagerSettings-Mode=" NextPreviousFirstLast"
        OnRowDataBound="grid1_RowDataBound">  
        <AlternatingRowStyle BackColor="White" />  
       
         <Columns>  
            <asp:TemplateField HeaderText="ID book">  
                <ItemTemplate>  
                    <asp:Label ID="idlivre" runat="server" Text='<%#Bind("idlivre") %>'></asp:Label>  
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
                    <asp:Label ID="description" runat="server" Text='<%#Bind("description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <asp:Label ID="category" runat="server"  Text='<%#Bind("category") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <asp:Image ID="image" runat="server" Height="100px" Width="75px"  
                             AlternateText=" " 
                             ImageUrl='<%# "ImageHandler.ashx?IdLivre="+ Eval("idlivre") %>' 
                             DescriptionUrl='<%# Eval("photo") %>'>
                    </asp:Image>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price">
                <ItemTemplate>
                    <asp:Label ID="price" runat="server" Text='<%#Bind("price") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Pages">
                <ItemTemplate>
                    <asp:Label ID="pages" runat="server" Text='<%#Bind("pages") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="availability">
                <ItemTemplate>
                    <asp:Label ID="availability" runat="server" Text='<%#Bind("availability") %>'></asp:Label>
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
</asp:Content>
