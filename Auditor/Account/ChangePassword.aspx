<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="RoleManager.ChangePassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ChangePassword ID="ChangeUserPassword" runat="server" CancelDestinationPageUrl="~/"
        EnableViewState="false" RenderOuterTable="false" SuccessPageUrl="ChangePasswordSuccess.aspx">
        <ChangePasswordTemplate>
            <table class="accountTable">
                <tr>
                    <td rowspan="7" style="text-align: center; border-right: 1px dotted #e5e5e5; padding: 10px;">
                        <p>
                            <b>Password change</b>
                        </p>
                        <img alt="lock" src="../Images/lock.png" />
                        <br />
                        <img alt="auth" src="../Images/authbase.png" />
                    </td>
                    <td style="padding: 5px; padding-left: 10px;">
                        <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Old Password:</asp:Label>
                    </td>
                    <td style="padding: 5px;">
                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px; padding-left: 10px;" colspan="2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CurrentPassword"
                            CssClass="failureNotification" ErrorMessage="Old password is required." ToolTip="Old password is required."
                            ValidationGroup="ChangeUserPasswordValidationGroup">This field is required.</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px; padding-left: 10px;">
                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label>
                    </td>
                    <td style="padding: 5px;">
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px; padding-left: 10px;" colspan="2">
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                            CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required."
                            ValidationGroup="ChangeUserPasswordValidationGroup">This field is required.</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px; padding-left: 10px;">
                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label>
                    </td>
                    <td style="padding: 5px;">
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px; padding-left: 10px;" colspan="2">
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="NewPassword"
                            Display="Dynamic" ControlToValidate="ConfirmNewPassword" CssClass="failureNotification"
                            ErrorMessage="Password confirmation error." ValidationGroup="ChangeUserPasswordValidationGroup">Password confirmation error.</asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                            CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required."
                            ValidationGroup="ChangeUserPasswordValidationGroup">This field is required.</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px;" colspan="2">
                        <p class="submitButton">
                            <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                                Text="Change Password" ClientIDMode="Static" ValidationGroup="ChangeUserPasswordValidationGroup" Width="150" Height="50"></asp:Button>
                            <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" Width="100" Height="50"
                                Text="Cancel"></asp:Button>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px;" colspan="3">
                        <center>
                        <span class="failureNotification">
                            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                        </span>
                        </center>
                    </td>
                </tr>
            </table>
        </ChangePasswordTemplate>
    </asp:ChangePassword>
</asp:Content>