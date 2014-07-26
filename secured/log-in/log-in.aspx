<%@ Page Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true" Inherits="Public_Log_On" Title="log-in please" Codebehind="log-in.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceholder" runat="Server">
    <h1 class="title-regular clearfix">
		Welcome To Jarrad's Login Page</h1>
    <asp:Literal runat="server" EnableViewState="False" ID="labelMessage"></asp:Literal>
    <p>
		Welcome to my Technical Evaluation!</p>
    <div class="notice">
		Username: any | Password: any</div>
    <p class="inline">
        <label for="name">
            Username
        </label>
        <br />
        <input runat="server" name="name" id="txtUserName" type="text" class="text" /><br />
        <label for="password">
            Password</label><br />
        <input runat="server" name="password" id="txtPassword" type="password" class="text" /><br />
        <label for="check1">
            <input runat="server" title="remember" type="checkbox" name="checkboxRemember" id="checkBoxRemember"
                value="" />
            Remember me</label><br />
    </p>
    <p>
        <asp:Button ID="buttonLogOn" SkinID="AltButton" runat="server" Text="Log in as Member"
            OnClick="ButtonLogOn_Click" />
        <asp:Button ID="buttonAdminLogOn" SkinID="Button" runat="server" Text="Log in as Admin"
            OnClick="ButtonAdminLogOn_Click" />
    </p>
    <hr />
</asp:Content>
