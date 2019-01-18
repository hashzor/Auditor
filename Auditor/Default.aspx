<%@ Page Title="Auditor" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Auditor._Default" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function cbAuditType_ValueChanged(s, e) {
            var audit_type = s.GetValue();
            cbPanelAudit.PerformCallback(audit_type);
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <dx:ASPxCallbackPanel ID="cbPanelAudit" runat="server" ClientInstanceName="cbPanelAudit" OnCallback="cbPanelAudit_Callback">
        <PanelCollection>
            <dx:PanelContent>
                <table style="margin: 0 auto; margin-top: 30px; text-align: center; align-items: center;">
                    <tr style="height: 80px;">
                        <td colspan="2">
                            <dx:ASPxLabel ID="lblGreetings" runat="server" Text="" Font-Size="20px"></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr style="height: 100px;">
                        <td>
                            <dx:ASPxComboBox ID="cbAuditType" ClientInstanceName="cbAuditType" runat="server" ValueType="System.String" DataSourceID="sdsAuditTypes" ValueField="audit_type" TextField="name"
                                Font-Size="20px" Width="500px" Height="50px" Caption="Audit Type">
                                <ClientSideEvents ValueChanged="cbAuditType_ValueChanged" />
                                <CaptionSettings Position="Top" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </dx:ASPxComboBox>
                        </td>
                        <td rowspan="2" style="padding-left: 100px;">
                            <dx:ASPxButton ID="btnStartAudit" runat="server" Text="START AUDIT" OnClick="btnStartAudit_Click" Width="250" Height="80" Font-Size="20px">
                                <Image Url="Images/power-button.png" Height="30"></Image>
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr style="height: 100px;">
                        <td>
                            <dx:ASPxComboBox ID="cbAuditTarget" ClientInstanceName="cbAuditTarget" runat="server" ValueType="System.String" DataSourceID="sdsAuditTargets" ValueField="audit_target"
                                TextField="audit_target" Caption="Audit Target" Width="500px" Height="50px" Font-Size="20px" TextFormatString="{0}">
                                <Columns>
                                    <dx:ListBoxColumn FieldName="audit_target" Caption="TARGET" Name="audit_target" Width="150" />
                                    <dx:ListBoxColumn FieldName="area" Caption="AREA" Name="area" Width="150" />
                                    <dx:ListBoxColumn FieldName="subarea" Caption="SUBAREA" Name="subarea" Width="150" />
                                    <dx:ListBoxColumn FieldName="section" Caption="SECTION" Name="section" Width="150" />
                                    <dx:ListBoxColumn FieldName="supervisor" Caption="SUPERVISOR" Name="supervisor" Width="200" />
                                </Columns>
                                <CaptionSettings Position="Top" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr style="height: 100px;">
                        <td>
                            <dx:ASPxComboBox ID="cbAuditShiftName" ClientInstanceName="cbShift" runat="server" ValueType="System.String" DataSourceID="sdsShifts" ValueField="shift_name"
                                TextField="shift_name" Caption="Shift" Width="100px" Height="50px" Font-Size="20px" Style="margin: 0 auto;">
                                <CaptionSettings Position="Top" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr style="height: 80px;">
                        <td colspan="2">
                            <dx:ASPxLabel ID="lblInfo" runat="server" Text="" Font-Size="20px" ForeColor="Red"></dx:ASPxLabel>
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="sdsAuditTypes" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                    SelectCommand="SELECT [id], [audit_type], [name]
                                    FROM [setts_audit_types]
                                    ORDER BY [name]"></asp:SqlDataSource>
                <asp:SqlDataSource ID="sdsAuditTargets" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                    SelectCommand="SELECT [id], [audit_type], [audit_target], [area], [subarea], [section], [supervisor]
                                    FROM [setts_audit_targets]
                                    WHERE [audit_type] = @audit_type
                                    ORDER BY [audit_target];">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="cbAuditType" Name="audit_type" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="sdsShifts" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                    SelectCommand="SELECT [id], [shift_name]
                                    FROM [setts_shifts]
                                    ORDER BY [shift_name]"></asp:SqlDataSource>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</asp:Content>