<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="Rod.Tags" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%= Page.ResolveUrl("~/css/tags.css")%>'rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        
        <h1 class="sectionHeader">الأقسام</h1>
      
        <!--<div class="controlserch">
            
            <asp:TextBox ID="TextBox1" CssClass="serhtxt" runat="server" placeholder="ابحث عن القسم"></asp:TextBox>
            <i class="fa-solid fa-magnifying-glass"></i>
        </div>-->

        <hr />
        <div class="tagsSection" runat="server">
        <asp:ListView ID="tagsListView" runat="server">

            <ItemTemplate>
                
                    <asp:HyperLink CssClass="sectionbtn" runat="server"
                        NavigateUrl='<%# Eval("id","~/tagged/{0}")%>'>
                        <%#Eval("tagName") %>
                    </asp:HyperLink>

            </ItemTemplate>
            
        </asp:ListView>
        </div> 
     
</asp:Content>
