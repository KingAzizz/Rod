<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Question.aspx.cs" Inherits="Rod.Question" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script defer src="./js/question.js"></script>
    <script defer src="./js/home.js"></script>
    <link href="./css/question.css" rel="stylesheet" type="text/css">
     <script src="https://kit.fontawesome.com/f933819f72.js" crossorigin="anonymous"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <section class="questionContainer" id="questionContainer" runat="server">
         <!--question POST-->
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
       <div class="questionHeader">

            <div class="questionHead">
                <h1 id="postTitle" runat="server"></h1>
                <div>
                    <a href="#">اسأل</a>
                </div>
                
            </div>
            <div>
                <span>انسأل</span>
                <p id="postCreation" runat="server"></p>
            </div>
        </div>
        <section class="post">
            <div class="vote postVote">
                <div>
                    <span>
                        <i class="fa-solid fa-angle-up"></i> <input type="button"/>
                    </span>
                    <span id="postUpvotedown" runat="server">0</span>
                    <span>
                        <i class="fa-solid fa-angle-down"></i> <input type="button" />
                    </span>
                                        
                </div>
            
            </div>
            <div class="questionPost">
                <p id="postBody" runat="server"></p>
                
            </div>
          </section>
          <div id="postTagsDiv" runat="server">
            
            
          </div>

          <div class="userPostDetails">
              <div>
                <input type="button" value="متابعة"/>
                <button  onclick="copyToClipboard(window.location.href)">نشر</button>
                  <button>الأجابة</button>
            </div>
            <div>
                <div id="postCreationUser" runat="server"></div>
                <div>
                <asp:Image ID="userProfileImagePost" runat="server" />
                
                    <asp:HyperLink ID="userLinkProfilePost" runat="server"></asp:HyperLink>
                </div>

            </div>
          </div>
          <!--question POST End HERE-->

      

          <div class="answers">
            <p>الأجابات</p>
          </div>
          <section class="postAnswer" id="answersPost" runat="server">
            
          
    </section>
         </section>
    
</asp:Content>
