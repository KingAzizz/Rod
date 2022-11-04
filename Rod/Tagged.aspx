<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Tagged.aspx.cs" Inherits="Rod.Tagged" %>
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
                     <asp:HyperLink ID="TagsLink" runat="server" NavigateUrl="~/tags">الأقسام</asp:HyperLink>
                    </li>
                <li>
                     <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/top/users">
                         المستخدمين المميزيين
                     </asp:HyperLink>
                    </li>
            </ul>
        </nav>
     <section class="content">
         
            <div>
                <asp:HyperLink  runat="server" NavigateUrl="~/ask/question" Text="إسأل"></asp:HyperLink>
             
                
            </div>
            <div id="totalQuestion" class="totalQuestion" runat="server" style="width:100%; height:100%;">
                <h2 id="tagNameH2" runat="server" style=" margin-top:10px; margin-right:43px;"></h2>
         </div>
        </section>
      <section class="sectionQuestions" id="sectionQuestions" runat="server">
          <h1 id="noResult" runat="server" visible="false" style="display:flex; justify-content:center; margin-top:50px;">لاتوجد نتائج</h1>
     <asp:ListView ID="questionsListView" runat="server" OnPagePropertiesChanging="questionsListView_PagePropertiesChanging" ItemPlaceholderID="itemPlaceholder" GroupPlaceholderID="groupPlaceholder" OnItemDataBound="questionsListView_ItemDataBound">
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
                <asp:HiddenField ID="tagNameHiddenField" runat="server" Value='<%# Eval("tagName") %>' />
                <asp:HiddenField ID="questionId" runat="server" Value='<%# Eval("PostId") %>' />
             <div class="question">
                    <div class="votesAnswers">

                      <h3><span> <%# Eval("totalVote") %> </span> التقييم</h3> 
                    <div class="answersContainer"> 
                    <h3><span> <%# Eval("answerCount") %> </span> الأجابات</h3>
                     </div>  </div>


                    <div class="questionTitle">

                        <asp:HyperLink ID="title" runat="server" CssClass="title" NavigateUrl='<%# Eval("PostId","~/question/{0}") %>' Text='<%# Eval("title") %>'></asp:HyperLink>

                    </div>
                   <div id="tags" runat="server">
                   
                   </div>
                   <div class="usernameQuestionDetails">
                    <h2><span><asp:HyperLink runat="server" CssClass="username" NavigateUrl='<%# Eval("UserId","~/users/profile/{0}") %>'> <%# Eval("username") %></asp:HyperLink></span>   <span> <%# Eval("reputation") %></span></h2> 
                   <p> <%# RelativeDate(Convert.ToDateTime(Eval("creationDate"))) %></p>

                   </div>

             </div>
            </ItemTemplate>
        </asp:ListView>
          </section>
</asp:Content>
