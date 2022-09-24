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
                   <asp:HyperLink ID="selected" runat="server" NavigateUrl="~/">الصفحة الرئيسية</asp:HyperLink>
                </li>
                <li>
                     <asp:HyperLink ID="TagsLink" runat="server" NavigateUrl="~/tags">الأقسام</asp:HyperLink>
                    </li>
                <li>
                     <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/top/users">
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
               <p id="resultCount" runat="server">نتيجة</p>
          
            </div>
           
           
    </section>
    <section class="searchResult" id="searchResult" runat="server">
        <asp:ListView ID="searchResultListView" runat="server">
            <ItemTemplate>
       <div class="question">
                         <div class="votesAnswers">

                          <h3><span><%# Eval("totalVote") %></span> التقييم</h3>
                        <div class="answersContainer">
                        <h3><span><%# Eval("answerCount") %></span> الأجابات</h3>
                         </div>

                         </div>


                       <div class="questionTitle">
                           
                            <asp:HyperLink ID="questionLink" runat="server" NavigateUrl='<%# Eval("id","~/question/{0}") %>' ForeColor="#0173CC">
                                <%# Eval("title") %>
                            </asp:HyperLink>
                       </div>
                       <div class="usernameQuestionDetails">
                        <h2>
                            <span>
                                    <asp:HyperLink ID="profileLink" runat="server" NavigateUrl='<%# Eval("userIdU","~/users/profile/{0}") %>'>
                                        <%# Eval("username") %>
                                    </asp:HyperLink>
                            </span>   
                            <span><%# Eval("reputation") %></span> 
                        </h2>
                       <p>
                           <%# RelativeDate(Convert.ToDateTime(Eval("creationDate"))) %>
                       </p></div></div>
                 </ItemTemplate>
        </asp:ListView>
    </section>
</asp:Content>
