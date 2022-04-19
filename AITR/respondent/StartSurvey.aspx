<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="StartSurvey.aspx.cs" Inherits="AITR.survey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginbox">
        <img src="../images/logo.png" alt="logo" class="logo" />
        <h2>Welcome to the AITR survey!</h2>
        <p>
            You can participate in this survey anonymously or register your personal information. <br />
            Registering can improve our platform, delivering an optmised experience!
        </p>
        <br />
        <br />
        <div class="anonymous__radio">
            <asp:RadioButton ID="anonymous_yes" runat="server" Text=" Anonymous" CssClass="radio" GroupName="anonymous" Checked="true" />
            <asp:RadioButton ID="anonymous_no" runat="server" Text=" Register" CssClass="radio" GroupName="anonymous"/>        
        </div>
        <br />
        <br />
        <div class="anonymous__buttons">
            <a href="home.aspx"><input id="anonymous__cancel" type="button" value="Cancel" /></a>
            <asp:Button ID="ButtonNext" runat="server" Text="Next" OnClick="ButtonNext_Click" />
        </div>
    </div>
    
    
</asp:Content>
