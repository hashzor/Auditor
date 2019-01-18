<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Actions.aspx.cs" Inherits="Auditor.Actions" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function gvActions_Init(s, e) {
            AdjustGridViewHeight(s, e, 120);
        }
        function gvActions_EndCallback(s, e) {
            if (s.cp_refresh != null) {
                s.cp_refresh = null;
                s.Refresh();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxGridView ID="gvActions" ClientInstanceName="gvActions" runat="server" AutoGenerateColumns="False" DataSourceID="sdsActions" KeyFieldName="id" Style="margin: 0 auto; margin-top: 10px;" Width="100%"
        OnInit="gvActions_Init" OnRowUpdating="gvActions_RowUpdating" OnCustomButtonInitialize="gvActions_CustomButtonInitialize" OnCustomButtonCallback="gvActions_CustomButtonCallback">
        <ClientSideEvents Init="gvActions_Init" EndCallback="gvActions_EndCallback" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="id" ReadOnly="True" VisibleIndex="0" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewBandColumn Caption="AUDIT" VisibleIndex="1">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="ID" FieldName="audit_id" ShowInCustomizationForm="True" VisibleIndex="0" Width="70px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="NAME" FieldName="audit_type_name" ShowInCustomizationForm="True" VisibleIndex="1" Width="100px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="TARGET" FieldName="audit_target" ShowInCustomizationForm="True" VisibleIndex="2" Width="110px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn Caption="DATE" FieldName="audit_date" ShowInCustomizationForm="True" VisibleIndex="3" Width="90px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataDateColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="PROBLEM" VisibleIndex="4">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="GROUP" FieldName="group_name" ShowInCustomizationForm="True" VisibleIndex="0" Width="100px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="QUESTION" FieldName="question" ShowInCustomizationForm="True" VisibleIndex="1">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="ANS." FieldName="answer" ShowInCustomizationForm="True" VisibleIndex="2" Width="80px">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="COMMENT" FieldName="comment" ShowInCustomizationForm="True" VisibleIndex="3">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewDataTextColumn FieldName="responsible_full_name" VisibleIndex="10" Caption="RESPONSIBLE" Width="130">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="action" VisibleIndex="11" Caption="ACTION">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="planned_term" VisibleIndex="12" Caption="PLAN. IMPL. DATE" Width="110">
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn FieldName="term" VisibleIndex="13" Caption="IMPL. DATE" Width="110">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn FieldName="last_edit_date" VisibleIndex="15" Visible="False">
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="responsible_login" VisibleIndex="16" Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn VisibleIndex="17" Width="100" ButtonRenderMode="Image">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="btnConfirm" Text="IMPLEMENTATION">
                        <Image Url="Images/task-complete.png" AlternateText="IMPLEMENTATION" ToolTip="IMPLEMENTATION" Width="25px"
                            Height="25px" />
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="btnClear" Text="CLEAR">
                        <Image Url="Images/back-arrow.png" AlternateText="CLEAR" ToolTip="CLEAR" Width="25px"
                            Height="25px" />
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="sdsActions" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
        SelectCommand="SELECT
                               [act].[id],
                               [act].[audit_id],
                               [act].[audit_detail_id],
                               [act].[responsible_login],
                               [act].[responsible_full_name],
                               [act].[action],
                               [act].[planned_term],
                               [act].[term],
                               [act].[last_edit_date],
                               [a].[audit_type],
                               [a].[audit_type_name],
                               [a].[audit_target],
                               [a].[end_date] AS [audit_date],
                               [a].[score] AS    [audit_score],
                               [ad].[group_position],
                               [ad].[group_name],
                               [ad].[question_position],
                               [ad].[question],
                               [ad].[answer],
                               [ad].[comment]
                        FROM   [actions] AS [act]
                               LEFT JOIN [audits] AS [a] ON [a].[id] = [act].[audit_id]
                               LEFT JOIN [audit_details] AS [ad] ON [ad].[id] = [act].[audit_detail_id]
                        ORDER BY
                                 [act].[id] DESC;"
        UpdateCommand="UPDATE [actions]
                        SET [action] = @action,
                            [planned_term] = @planned_term,
                            [last_edit_date] = GETDATE()
                        WHERE [id] = @id
                            AND [responsible_login] = @responsible_login
                            AND [action] IS NULL
                            AND [planned_term]  IS NULL">
        <UpdateParameters>
            <asp:Parameter Name="action" Type="String" />
            <asp:Parameter Name="planned_term" Type="DateTime" />
            <asp:Parameter Name="responsible_login" Type="String" />
            <asp:Parameter Name="id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>