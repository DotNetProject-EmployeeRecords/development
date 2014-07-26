<%@ Page Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true"
    Inherits="Eisk.Install" Title="Database Installation" CodeBehind="install-db.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceholder" runat="Server">
    <h1 class="title-regular clearfix">
        Install Database</h1>
    <p>
		You can install the database needed to run Jarrad's Technical Evaluation Application below.
        <br />
		Provide the database server information to create Jarrad's employee database, which you will utilize moving forward.
        <br />
		Once the database installs successfully, you will be able to use my application.
    </p>
    <asp:ScriptManager ID="scriptManager" runat="server" />
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <asp:Label runat="server" EnableViewState="False" ID="labelMessage" ForeColor="Red"></asp:Label>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <img src="../../images/loader-life.gif" alt="Loading Data" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <fieldset>
				<legend>Install Jarrad's Database</legend>
                <label>
                    Your database server address:
                </label>
                <asp:TextBox runat="server" ID="txtServerAddress" Text="localhost" Width="300px"></asp:TextBox>
                <br />
                <asp:CheckBox runat="server" ID="chkUseIntegratedSecurity" AutoPostBack="true" Checked="true"
                    Text="Use Windows Integrated Security" OnCheckedChanged="chkUseIntegratedSecurity_CheckedChanged" /><br />
                <asp:Panel runat="server" ID="pnlUserCredential" Visible="true">
                    <label>
                        User Name:
                    </label>
                    <asp:TextBox CssClass="text" runat="server" ID="txtUsername" Width="300px"></asp:TextBox><br />
                    <label>
                        Password:
                    </label>
                    <asp:TextBox runat="server" TextMode="Password" CssClass="text" ID="txtPassword"
                        Width="300px"></asp:TextBox>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlDbName" Visible="false">
                    <label>
                        Name:
                    </label>
                    <asp:TextBox Text="EmployeeInfo_SK_5_0" runat="server" ID="txtDbName" Width="300px"></asp:TextBox>
                    <br />
                </asp:Panel>
                <br />
                <asp:Button ID="buttonTestConnection" SkinID="Button" runat="server" Text="Test Connection"
                    OnClick="buttonTestConnection_Click" />
                <asp:Button Visible="false" ID="buttonCreateDatabase" SkinID="AltButton" runat="server"
                    Text="Create Database" OnClick="buttonCreateDatabase_Click" />
                <asp:Button Visible="false" ID="buttonInstall" SkinID="Button" runat="server" Text="Install Schema and Data"
                    OnClick="buttonInstall_Click" />
                <br />
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
