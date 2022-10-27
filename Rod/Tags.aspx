<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="Rod.Tags" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%= Page.ResolveUrl("~/css/tags.css")%>'rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div class="sectionHeader">
            <h1>الأقسام</h1>
        </div>
       <div class="main">
            <asp:ListView ID="tagsListView" runat="server">
                <ItemTemplate>
                        <div class="cards_item">
                            <div class="card">
                                <div class="card_image">

                                    <img src="<%#Eval("TagImage")%>"/>


                                </div>
                                <div class="card_content">
                                    <h2 class="card_title">
                                        <%#Eval("tagName") %>
                                    </h2>
                                    <p class="card_text">
                                        <%#Eval("tagDescription") %>
                                    </p>
                                    <asp:HyperLink runat="server" CssClass="tagbtn"
                                        NavigateUrl='<%# Eval("id","~/tagged/{0}")%>'>
                                        أنتقل
                                    </asp:HyperLink>
                                </div>
                            </div>
                        </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
     
</asp:Content>
