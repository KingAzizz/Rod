<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Rod.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <script defer src='<%= Page.ResolveUrl("~/js/profile.js")%>' charset="utf-8"></script>
    <link href='<%= Page.ResolveUrl("~/css/profile.css")%>'rel="stylesheet" type="text/css">
     <script src="https://kit.fontawesome.com/f933819f72.js" crossorigin="anonymous"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="hamburgerMenu"> <button class="hamburgerButton" id="triggerSideNav" type="button"><i class="fa fa-bars"></i></button> </div>
    <nav class="sidenav" id="sidenav">
        <ul>
            <li><a href="#home" id="selectedNav">الملف الشخصي</a></li>
            <li><a href="#home">الأسئلة</a></li>
            <li><a href="#home">الأجابات</a></li>
            <li><a href="#home">الأقسام</a></li>
            <li><a href="#home">الأوسمة</a></li>
        </ul>
    </nav>
 
    <section class="profileContainer" dir="rtl">
        <div class="userDetails">
            <div class="userImage"><asp:Image ID="userProfileImage" runat="server" AlternateText="no image"  /></div>
            <div>
                <p id="displayName" runat="server"></p>
                <p style="color:gray;" id="username" runat="server"></p>
                <p style="color:gray;" id="creationDate" runat="server"></p>
                <div>
                  
                    <asp:HyperLink ID="twitter" runat="server"><i class="fa-brands fa-twitter"></i></asp:HyperLink>
                   
                    <asp:HyperLink ID="github" runat="server"><i class="fa-brands fa-github"></i></asp:HyperLink>
                   
                    <asp:HyperLink ID="websiteUrl" runat="server">
                        <i class="fa-solid fa-link" style="margin-left: 5px;"></i>
                    </asp:HyperLink>
                    <p id="locationP" runat="server"><i class="fa-solid fa-location-dot"></i> <span id="location" runat="server"></span> </p>
                    <p id="eduP" runat="server"><i class="fa-solid fa-graduation-cap" style="margin-left:4px;"></i> <span id="education" runat="server"></span></p>
                </div>
            </div>
    
            <div class="editProfileContainer">
                <button><i class="fa-solid fa-gear"></i></button>
                <a href="#" >تعديل الملف الشخصي</a>
            </div>
        </div>
        <div class="userFollow">
            <p>التابعين <span style="font-weight: bold;" id="followers" runat="server"></span></p>
            <p>المتابعين <span style="font-weight: bold;" id="following" runat="server"></span></p>
        </div>

        <div class="aboutUser">
            <div>
                <h1>نبذه</h1>
                <p id="userAboutMe" runat="server"></p>
            </div>
            <div>
                <h1>احصائيات</h1>
                <div>
                    <div>
                        <p>النقاط <span id="userReputPoint" runat="server"></span></p>
                        <p>وصول <span id="userViews" runat="server"></span></p>
                    </div>
                    <div>
                        <p>الأجابات <span id="userAnswers" runat="server"></span> </p>
                        <p>اسئله <span id="userQuestions" runat="server"></span></p>
                    </div>
                </div>
            </div>
        </div>
        <div class="userBadges" id="userBadges" runat="server">
            <h1 style="position: relative; left: 10px;">الأوسمة</h1>
            <div>
                <asp:DataList ID="BadgesDataList" runat="server" RepeatDirection="Horizontal">
                    <ItemTemplate>

                <span>
                <img src='<%# Eval("badgeImage") %>' alt='<%# Eval("description") %>' title='<%# Eval("name") %>' class="badgeImage">

                </span>
                
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div class="questionsAnswers">
            <!-- first Row -->
          <div id="questionsDivD" runat="server">
            <div><label>الاسئلة</label></div>
            <div id="expectionCss1" runat="server">
                <asp:DataList ID="QuestionsDataList" runat="server">
                    <ItemTemplate>

                <section class="questionContainerD">
                    <div class="votes"><%# Eval("totalVote") %></div>
                    <div class="questionTitleUser">
                        <asp:HyperLink ID="questionTitleText" runat="server"><%# Eval("title") %></asp:HyperLink>
                    </div>
                    <div class="date"><%# GetMonthDay(Eval("creationDate").ToString()) %></div>
                </section>
                    </ItemTemplate>
                </asp:DataList>
            </div>

          </div>
             <!-- first Row end -->

                   <!-- second Row  -->
          <div id="answersDivD" runat="server">
            <div><label>الأجابات</label></div>
            <div id="expectionCss2" runat="server">
                <asp:DataList ID="AnswersDataList" runat="server">
                    <ItemTemplate>

             <section class="questionContainerD">
                <div class="votes"><%# Eval("totalVote") %></div>
                <div class="questionTitleUser"><asp:HyperLink ID="answerTitleText" runat="server"><%# Eval("answerText") %></asp:HyperLink></div>
                <div class="date"><%# GetMonthDay(Eval("creationDate").ToString()) %></div>
             </section>

                    </ItemTemplate>
                </asp:DataList>
            </div>
          </div>
        </div>
         <!-- second Row end -->

    </section>
        
</asp:Content>
