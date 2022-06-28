<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SearchResult.aspx.cs" Inherits="Rod.SearchResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="./css/searchResult.css" rel="stylesheet" type="text/css" />
    <script src="./js/home.js" defer></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="hamburgerMenu"> 
             <button class="hamburgerButton" id="triggerSideNav" type="button" ><i class="fa fa-bars"></i></button> </div>
        <nav class="sidenav" id="sidenav" runat="server">
            <ul>
                <li>
                   <asp:HyperLink ID="selected" runat="server" NavigateUrl="~/Home.aspx">الصفحة الرئيسية</asp:HyperLink>
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
    <section id="searchFilters" runat="server" class="searchFilters">
               
         <div>
                <h1>نتائج البحث</h1>
                <asp:HyperLink ID="navToAskForm" runat="server" NavigateUrl="~/QuestionForm.aspx" Text="اسأل"></asp:HyperLink>
             
            </div>

        <div class="searchedItem">
            <p id="searchedItemText" runat="server"></p>
        </div>
          <div>   
              <div>
           <asp:Button ID="newestFilter" runat="server" Text="الجديد" onClick="NewestFilter" />
           <asp:Button ID="highRatingFilter" runat="server"  Text="الأعلى تقييمآ" OnClick="HighRatingFilter" />
                   </div>
               <p id="resultCount" runat="server">نتيجة 500</p>
          
            </div>
           
           
    </section>
    <section class="searchResult" id="searchResult" runat="server">
      
    </section>
</asp:Content>
