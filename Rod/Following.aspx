<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Following.aspx.cs" Inherits="Rod.Following" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href='<%= Page.ResolveUrl("~/css/followersAndfollowing.css")%>'rel="stylesheet" type="text/css">
      <script defer src='<%= Page.ResolveUrl("~/js/home.js")%>' charset="utf-8"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="hamburgerMenu"> 
             <button class="hamburgerButton" id="triggerSideNav" type="button" ><i class="fa fa-bars"></i></button> </div>
        <nav class="sidenav" id="sidenav" runat="server">
            <ul>
                <li>
                   <asp:HyperLink ID="selected" runat="server" NavigateUrl="~/">الصفحة الرئيسية</asp:HyperLink>
                </li>
                <li>
                     <asp:HyperLink ID="TagsLink" runat="server" NavigateUrl="~/Tags.aspx">الأقسام</asp:HyperLink>
                    </li>
                <li>
                     <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/TopUsers.aspx">
                         المستخدمين المميزيين
                     </asp:HyperLink>
                    </li>
            </ul>
        </nav>
    <section class="followContainer">
        <div>
            <div>
                <asp:HyperLink ID="following" runat="server" Text="المتابعين" CssClass="selectedWindow" 
                    NavigateUrl="~/profile/following">
                </asp:HyperLink>
                <asp:HyperLink ID="follower" runat="server" Text="التابعين" 
                    NavigateUrl="~/profile/followers">
                </asp:HyperLink>
            </div>
            <asp:ListView ID="FollowedProfileListView" runat="server" OnItemCommand="FollowedProfileListView_ItemCommand" OnItemDataBound="FollowedProfileListView_ItemDataBound">
                <ItemTemplate>

            <div class="followProfile" dir="rtl">
             
                    <asp:HyperLink ID="linkToProfile" runat="server" 
                        NavigateUrl='<%# Eval("followingID","~/users/profile/{0}") %>'>
                <div class="profileContent">
                    <asp:Image ID="followProfileImage" runat="server" CssClass="followProfileImage" 
                        ImageUrl='<%# Eval("profileImage","~/{0}") %>' />
                    <div class="profileDetails">
                        <h2 class="followName"><%# Eval("displayName") %></h2>
                        <h3><%# Eval("username") %>@</h3>
                        <p><%# Eval("aboutMe") %></p>
                    </div>
                </div>
            </asp:HyperLink>
                 <asp:HiddenField ID="followerID" runat="server" Value='<%# Eval("followingID") %>' />
                <div class="unfollowDiv">
                     <asp:Button ID="unFollow" runat="server" Text="الغاء المتابعة"  
                         CommandName="unFollow" CommandArgument='<%# Eval("followingID") %>' Visible="false" />
                    <asp:Button ID="follow" runat="server" Text="متابعة" Visible="false" CommandName="follow" CommandArgument='<%# Eval("followingID") %>'/>
                </div>
            </div>
                    
                </ItemTemplate>
            </asp:ListView>
            
        </div>
    </section>
</asp:Content>
