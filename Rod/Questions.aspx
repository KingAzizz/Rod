<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Questions.aspx.cs" Inherits="Rod.Questions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%= Page.ResolveUrl("~/css/questions.css")%>'rel="stylesheet" type="text/css">
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
     <section class="content">
            <div>
                
           <asp:Button ID="monthFilter" runat="server"  Text="الشهر"  />
           <asp:Button ID="commonFilter" runat="server" Text="الشائع" />
           

            </div>
        
            <div>
                <h1>جميع ألاسئلة</h1>
                <button id="askQuestion" runat="server">إسأل</button>
             

            </div>
            <div id="totalQuestion" class="totalQuestion" runat="server">

         </div>
        </section>
      <section class="sectionQuestions" id="sectionQuestions" runat="server">
     <asp:ListView ID="questionsListView" runat="server" OnPagePropertiesChanging="questionsListView_PagePropertiesChanging" ItemPlaceholderID="itemPlaceholder" GroupPlaceholderID="groupPlaceholder">
            <LayoutTemplate>
                            <asp:PlaceHolder runat="server" ID="groupPlaceholder"></asp:PlaceHolder>

                  <asp:DataPager ID="DataPager"  runat="server" PagedControlID="questionsListView" PageSize="10" >
                    <Fields>
                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false"
                             ShowPreviousPageButton="true" ButtonCssClass="prev" ShowNextPageButton="false" PreviousPageText="Previous"/>
                       
                        <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ButtonCssClass="next" ShowLastPageButton="false" ShowPreviousPageButton="false"  />
                        
                    </Fields>
                </asp:DataPager>
            </LayoutTemplate>
            <GroupTemplate>
                      <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>

            </GroupTemplate>
            <ItemTemplate>
                <asp:HiddenField ID="questionsCount" runat="server" Value='<%# Eval("totalQuestions") %>' />
             <div class="question">
                    <div class="votesAnswers">

                      <h3><span> <%# Eval("totalVote") %> </span> التقييم</h3> 
                    <div class="answersContainer"> 
                    <h3><span> <%# Eval("answerCount") %> </span> الأجابات</h3>
                     </div>  </div>


                    <div class="questionTitle">
                       <a style="color: #0173CC;" href="/question/">
                          <%# Eval("title") %></a> </div>

                   <div class="usernameQuestionDetails">
                    <h2><span> <%# Eval("username") %></span>   <span> <%# Eval("reputation") %></span></h2> 
                   <p> <%# RelativeDate(Convert.ToDateTime(Eval("creationDate"))) %></p>

                   </div>

             </div>
            </ItemTemplate>
        </asp:ListView>
          </section>

</asp:Content>
