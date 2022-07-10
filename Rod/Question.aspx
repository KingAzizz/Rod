﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Question.aspx.cs" Inherits="Rod.Question" %>
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
                  <asp:Button ID="postUserFollower" runat="server" Text="متابعة" onClick="PostUserFollower_Click" />
                  <asp:HiddenField ID="userId" runat="server" />
                  <asp:Button ID="unFollowUserPost" runat="server" Text="الغاء المتابعة" OnClick="UnFollowUserPost_Click"  />
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
           
              <asp:DataList ID="Datalist" runat="server"  OnItemDataBound="Datalist_ItemDataBound"
                onItemCommand ="Datalist_ItemCommand">
              <ItemTemplate>
                  <section class='postAnswer'>
                    <div class='vote postVote'>
                    <div>
                    <span>
                        <i class='fa-solid fa-angle-up'></i> <input type = 'button' />
                    </span>
                    <span><%#Eval("answer_upvoteCount") %> </span>
                    <span>
                        <i class='fa-solid fa-angle-down'></i> <input type = 'button' />
                    </ span >


                </div>


            </div>
            <div class='questionPost'>
                <p style='font-size:100%'><%# Eval("answerText") %></p>
                
            </div>
          </section>
          
          <div class='userPostDetails'>
              <div>
              
                  <asp:Button ID="followBtn" runat="server" Text="متابعة" CommandName="Follow" CommandArgument='<%# Eval("userAnswerId")  %>' Visible='<%#Followed(Convert.ToInt32(Session["id"]) ,Convert.ToInt32(Eval("userAnswerId")) )  %>' />
                  <asp:Button ID="unFollowBtn" runat="server" Text="الغاء المتابعة" Visible=false CommandName="Unfollow" CommandArgument='<%# Eval("userAnswerId")  %>'  />
               
                  
                <button onclick = 'copyToClipboard(window.location.href)' > نشر </button>
            </div>
            <div>
                <div id='answeCreation'>
                    <%# RelativeDate(Convert.ToDateTime(Eval("answer_creationDate"))) %>
                </div>
                <div>
               
                
                    <asp:Image ID="answerImages" runat="server" ImageUrl='<%# Eval("answer_profileImage") %>' AlternateText="no image" />
                    <asp:HyperLink ID="answerProfileLink" runat="server" Text='<%#Eval("answer_username") %>'></asp:HyperLink>
                    
                </div>

            </div>
          </div>
              </ItemTemplate>
                
            </asp:DataList>
          
         </section>
         </section>
    
</asp:Content>
