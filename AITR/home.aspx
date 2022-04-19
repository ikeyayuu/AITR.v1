<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="AITR.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="loginbox">
            <img src="images/logo.png" alt="logo" class="logo" />
            <h2>Welcome to AITResearch platform! <br />
                Please fill up the survey so we can customise our services! 
            </h2>
            <div class="btnStart"> <a href="./respondent/StartSurvey.aspx"><input id="start_survey" type="button" value="Start the survey" /></a></div>
            <div class="btnStaff"><input id="staff" type="button" value="Staff Login" /></div>
        </div>
</asp:Content>
