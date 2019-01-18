<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ChangePasswordSuccess.aspx.cs" Inherits="RoleManager.ChangePasswordSuccess" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table class="accountTable">
        <tr>
            <td style="text-align: center">
                <p>
                    <b>Password changed!</b>
                </p>
                <img alt="authimg" src="../Images/authbase.png" />
            </td>
        </tr>
    </table>
</asp:Content>