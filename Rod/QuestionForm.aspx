<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="QuestionForm.aspx.cs" Inherits="Rod.QuestionForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%= Page.ResolveUrl("~/css/questionForm.css")%>'rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div class="div">
            <div class="placenav">
            <h1 class="nav">اسأل</h1>
        </div>
        </div>
 
        <div class="control">
             <div class="askform">
            <h1 class="subject">العنوان</h1>
            <h6 class="subinfo">دقة العنوان تساعد على إيجاد الأجابه</h6>
                 <div class="Movesubjecttxt">
                     <asp:TextBox ID="titletxt" CssClass="titletxt" runat="server"></asp:TextBox>
                     
                 </div>
                 <asp:RegularExpressionValidator runat="server" ControlToValidate="titletxt" ValidationExpression="\w{15,30}" ErrorMessage="err" ForeColor="Red" Font-Size="X-Small">
                     </asp:RegularExpressionValidator>

            <h1 class="subject">الموضوع</h1>
                 <h6 class="subinfo">يرجى ارفاق كل المعلومات المطلوبه</h6>
             <div class="Movesubjecttxt">
                     <asp:TextBox ID="subjecttxt" CssClass="subjecttxt" runat="server"></asp:TextBox>
      
                 </div>
                 <asp:RegularExpressionValidator runat="server" ControlToValidate="titletxt" ValidationExpression="\w{30,80}" ErrorMessage="err" ForeColor="Red" Font-Size="X-Small">
                     </asp:RegularExpressionValidator>

            <h1 class="subject">القسم</h1>
                 <h6 class="subinfo">ادخل القسم المتعلق بالسؤال</h6>
             <div class="Movesubjecttxt">
                     <asp:TextBox ID="sectiontxt" CssClass="titletxt" runat="server"></asp:TextBox>
                 </div>
                 <div class="btncontrol">
            <asp:Button CssClass="reviewbtn" Text="مراجعة السؤال" runat="server" OnClick="submitQuestion_Click"/>
                     
        </div>
        </div>
        </div>
</asp:Content>
