<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="RoleManager.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false">
        <LayoutTemplate>
            <table class="accountTable">
                <tr>
                    <td rowspan="3" style="text-align: center; border-right: 1px dotted #e5e5e5; padding-right: 10px;">
                        <p>
                            <b>Enter account details</b>
                        </p>
                        <img alt="lock" src="../Images/lock.png" />
                        <br />
                        <img alt="authbase" src="../Images/authbase.png" />
                    </td>
                    <td style="padding: 5px; padding-left: 10px;">
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Login:</asp:Label>
                    </td>
                    <td style="padding: 5px;">
                        <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px; padding-left: 10px;">
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                    </td>
                    <td style="padding: 5px;">
                        <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding: 5px;">
                        <p class="submitButton">
                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup" Width="200" Height="50" />
                        </p>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="padding: 5px;">
                        <center>
                        <span class="failureNotification">
                            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                        </span>
                        </center>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
    </asp:Login>
</asp:Content>