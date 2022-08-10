<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TopUsers.aspx.cs" Inherits="Rod.TopUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script defer src='<%= Page.ResolveUrl("~/js/home.js")%>' charset="utf-8"></script>
    <link href='<%= Page.ResolveUrl("~/css/topUsers.css")%>'rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="hamburgerMenu"> 
             <button class="hamburgerButton" id="triggerSideNav" type="button" ><i class="fa fa-bars"></i></button> </div>
        <nav class="sidenav" id="sidenav" runat="server">
            <ul>
                <li>
                   <asp:HyperLink ID="homeLink" runat="server" NavigateUrl="~/">الصفحة الرئيسية</asp:HyperLink>
                </li>
                <li>
                     <asp:HyperLink ID="tagsLink" runat="server" NavigateUrl="~/Tags.aspx">الأقسام</asp:HyperLink>
                    </li>
                <li>
                     <asp:HyperLink ID="topUsersLink" runat="server" NavigateUrl="~/top/users">
                         المستخدمين المميزيين
                     </asp:HyperLink>
                    </li>
            </ul>
        </nav>
     <section class="topUsersSection" dir="rtl">
         <asp:ListView ID="usersListView" runat="server">
             <ItemTemplate>
                     <div class="userContent">
        <div>
            <asp:HyperLink ID="userProfile" runat="server"  NavigateUrl='<%# Eval("id","~/users/profile/{0}") %>'>
                <asp:Image ID="userProfileImage" CssClass="userProfileImage" runat="server"
                    ImageUrl='<%# Eval("profileImage","~/{0}") %>' />
            </asp:HyperLink>
        </div>
        <div>
            <asp:HyperLink ID="userProfile2" runat="server" NavigateUrl='<%# Eval("id","~/users/profile/{0}") %>'>
                   <p class="fitHeight"><%# CheckIfEmpty(Eval("username").ToString(),Eval("displayName").ToString()) %></p>
            </asp:HyperLink>
          <p class="fitHeight"><%# Eval("location") %></p>
          <p class="fitHeight"><%# Eval("reputation") %></p>
          <div>
            <span class="tagFollowed"><%# Eval("tagsFollowed") %></span>
          </div>
        </div>
      </div>
             </ItemTemplate>
         </asp:ListView>
  
    </section>
</asp:Content>
