<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="Auditor.Settings" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function gv_Init(s, e) {
            AdjustGridViewHeight(s, e, 160);
        }
        function gvUsersInRole_Init(s, e) {
            AdjustGridViewHeight(s, e, 360);
        }

        function gvUsers_EndCallback(s, e) {
            if (s.cp_refresh != null) {
                s.cp_refresh = null;
                s.Refresh();
            }
            if (s.cp_unlock != null) {
                s.cp_unlock = null;
                alert("User unlocked!");
            }
            if (s.cp_reset != null) {
                s.cp_reset = null;
                alert("User password reseted!");
            }
        }
        function gvUsers_ToolbarItemClick(s, e) {
            var focusedRowIndex = s.GetFocusedRowIndex();
            var focusedRowKey = s.GetRowKey(s.GetFocusedRowIndex());
            var button = e.item.name;
            switch (button) {
                case '<%=Auditor.ToolbarButtons.Unlock %>':
                    e.processOnServer = true;
                    break;
                case '<%=Auditor.ToolbarButtons.Reset %>':
                    e.processOnServer = true;
                    break;
                default:
                    break;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxPageControl ID="pageControl" ClientInstanceName="pageControl" runat="server" ActiveTabIndex="0" Width="100%" Style="margin-top: 10px;" TabAlign="Center">
        <TabPages>
            <dx:TabPage Name="tab1" Text="AUDITS / TARGETS">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table style="margin: 0 auto;">
                            <tr>
                                <td>
                                    <dx:ASPxGridView ID="gvAuditTypes" ClientInstanceName="gvAuditTypes" runat="server" AutoGenerateColumns="False" DataSourceID="sdsAuditTypes" KeyFieldName="id" Width="400px"
                                        OnInit="gvAuditTypes_Init" OnRowInserting="gvAuditTypes_RowInserting" OnRowUpdating="gvAuditTypes_RowUpdating" OnCellEditorInitialize="gvAuditTypes_CellEditorInitialize">
                                        <ClientSideEvents Init="gv_Init" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="id" ReadOnly="True" VisibleIndex="0" Caption="ID" Width="55">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="audit_type" ShowInCustomizationForm="True" VisibleIndex="1" Caption="SYSTEM NAME">
                                                <PropertiesComboBox>
                                                    <ClearButton DisplayMode="Always" />
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn FieldName="name" ShowInCustomizationForm="True" VisibleIndex="2" Caption="NAME DESC.">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                    <asp:SqlDataSource ID="sdsAuditTypes" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                                        SelectCommand="SELECT [id], [audit_type], [name] FROM [setts_audit_types] ORDER BY [audit_type]"
                                        DeleteCommand="DELETE FROM [setts_audit_types] WHERE [id] = @id"
                                        InsertCommand="INSERT INTO [setts_audit_types] ([audit_type], [name]) VALUES (@audit_type, @name)"
                                        UpdateCommand="UPDATE [setts_audit_types] SET [audit_type] = @audit_type, [name] = @name WHERE [id] = @id">
                                        <DeleteParameters>
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </DeleteParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="audit_type" Type="String" />
                                            <asp:Parameter Name="name" Type="String" />
                                        </InsertParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="audit_type" Type="String" />
                                            <asp:Parameter Name="name" Type="String" />
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </UpdateParameters>
                                    </asp:SqlDataSource>
                                </td>
                                <td>
                                    <dx:ASPxGridView ID="gvAuditTargets" ClientInstanceName="gvAuditTargets" runat="server" AutoGenerateColumns="False" DataSourceID="sdsAuditTargets" KeyFieldName="id" Width="850px"
                                        OnInit="gvAuditTargets_Init" OnRowInserting="gvAuditTargets_RowInserting" OnRowUpdating="gvAuditTargets_RowUpdating">
                                        <ClientSideEvents Init="gv_Init" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="id" ReadOnly="True" VisibleIndex="0" Caption="ID" Width="55">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="audit_type" VisibleIndex="1" Caption="AUDIT TYPE">
                                                <PropertiesComboBox DataSourceID="sdsDistinctAuditTypes" ValueField="audit_type" TextField="name">
                                                    <ClearButton DisplayMode="Always" />
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn FieldName="audit_target" ShowInCustomizationForm="True" VisibleIndex="2" Caption="AUDIT TARGET">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="area" ShowInCustomizationForm="True" VisibleIndex="3" Caption="AREA">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="subarea" ShowInCustomizationForm="True" VisibleIndex="4" Caption="SUBAREA">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="section" ShowInCustomizationForm="True" VisibleIndex="5" Caption="SECTION">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="supervisor_login" ShowInCustomizationForm="True" VisibleIndex="6" Caption="SUPERVISOR" Width="200">
                                                <PropertiesComboBox DataSourceID="sdsDistinctUsers" ValueField="user_login" TextFormatString="{0} | {1}">
                                                    <ClearButton DisplayMode="Always" />
                                                    <Columns>
                                                        <dx:ListBoxColumn FieldName="user_login" Caption="LOGIN" />
                                                        <dx:ListBoxColumn FieldName="user_fullname" Caption="PERSON" />
                                                    </Columns>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn FieldName="supervisor" ShowInCustomizationForm="True" VisibleIndex="7" Visible="false">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                    <asp:SqlDataSource ID="sdsAuditTargets" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                                        SelectCommand="SELECT [id], [audit_type], [audit_target], [area], [subarea], [section], [supervisor_login], [supervisor] FROM [setts_audit_targets] ORDER BY [audit_type], [audit_target]"
                                        DeleteCommand="DELETE FROM [setts_audit_targets] WHERE [id] = @id"
                                        InsertCommand="INSERT INTO [setts_audit_targets] ([audit_type], [audit_target], [area], [subarea], [section], [supervisor_login], [supervisor]) VALUES (@audit_type, @audit_target, @area, @subarea, @section, @supervisor_login, @supervisor)"
                                        UpdateCommand="UPDATE [setts_audit_targets] SET [audit_type] = @audit_type, [audit_target] = @audit_target, [area] = @area, [subarea] = @subarea, [section] = @section, [supervisor_login] = @supervisor_login, [supervisor] = @supervisor WHERE [id] = @id">
                                        <DeleteParameters>
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </DeleteParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="audit_type" Type="String" />
                                            <asp:Parameter Name="audit_target" Type="String" />
                                            <asp:Parameter Name="area" Type="String" />
                                            <asp:Parameter Name="subarea" Type="String" />
                                            <asp:Parameter Name="section" Type="String" />
                                            <asp:Parameter Name="supervisor_login" Type="String" />
                                            <asp:Parameter Name="supervisor" Type="String" />
                                        </InsertParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="audit_type" Type="String" />
                                            <asp:Parameter Name="audit_target" Type="String" />
                                            <asp:Parameter Name="area" Type="String" />
                                            <asp:Parameter Name="subarea" Type="String" />
                                            <asp:Parameter Name="section" Type="String" />
                                            <asp:Parameter Name="supervisor_login" Type="String" />
                                            <asp:Parameter Name="supervisor" Type="String" />
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </UpdateParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="tab2" Text="QUESTIONS">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table style="margin: 0 auto;">
                            <tr>
                                <td>
                                    <dx:ASPxGridView ID="gvAuditQuestionGroups" ClientInstanceName="gvAuditQuestionGroups" runat="server" AutoGenerateColumns="False" DataSourceID="sdsAuditQuestionGroups" KeyFieldName="id" Width="500px"
                                        OnInit="gvAuditQuestionGroups_Init" OnRowInserting="gvAuditQuestionGroups_RowInserting" OnRowUpdating="gvAuditQuestionGroups_RowUpdating">
                                        <ClientSideEvents Init="gv_Init" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="id" ReadOnly="True" VisibleIndex="0" Caption="ID" Width="55">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="audit_type" VisibleIndex="1" Caption="AUDIT TYPE">
                                                <PropertiesComboBox DataSourceID="sdsDistinctAuditTypes" ValueField="audit_type" TextField="name">
                                                    <ClearButton DisplayMode="Always" />
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn FieldName="group_position" VisibleIndex="2" Caption="NO" Width="55">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="group_name" VisibleIndex="3" Caption="GROUP">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="group_name_ENG" VisibleIndex="4" Caption="GROUP ENG">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                    <asp:SqlDataSource ID="sdsAuditQuestionGroups" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                                        DeleteCommand="DELETE FROM [setts_audit_question_groups] WHERE [id] = @id"
                                        InsertCommand="INSERT INTO [setts_audit_question_groups] ([audit_type], [group_position], [group_name], [group_name_ENG]) VALUES (@audit_type, @group_position, @group_name, @group_name_ENG)"
                                        UpdateCommand="UPDATE [setts_audit_question_groups] SET [audit_type] = @audit_type, [group_position] = @group_position, [group_name] = @group_name, [group_name_ENG] = @group_name_ENG WHERE [id] = @id"
                                        SelectCommand="SELECT [id], [audit_type], [group_position], [group_name], [group_name_ENG] FROM [setts_audit_question_groups] ORDER BY [audit_type], [group_position]">
                                        <DeleteParameters>
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </DeleteParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="audit_type" Type="String" />
                                            <asp:Parameter Name="group_position" Type="Int32" />
                                            <asp:Parameter Name="group_name" Type="String" />
                                            <asp:Parameter Name="group_name_ENG" Type="String" />
                                        </InsertParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="audit_type" Type="String" />
                                            <asp:Parameter Name="group_position" Type="Int32" />
                                            <asp:Parameter Name="group_name" Type="String" />
                                            <asp:Parameter Name="group_name_ENG" Type="String" />
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </UpdateParameters>
                                    </asp:SqlDataSource>
                                </td>
                                <td>
                                    <dx:ASPxGridView ID="gvAuditQuestion" ClientInstanceName="gvAuditQuestion" runat="server" AutoGenerateColumns="False" DataSourceID="sdsAuditQuestions" KeyFieldName="id" Width="800px"
                                        OnInit="gvAuditQuestion_Init" OnRowInserting="gvAuditQuestion_RowInserting" OnRowUpdating="gvAuditQuestion_RowUpdating">
                                        <ClientSideEvents Init="gv_Init" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="id" ReadOnly="True" VisibleIndex="0" Caption="ID" Width="55">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="group_id" VisibleIndex="1" Caption="GROUP" Width="100">
                                                <PropertiesComboBox DataSourceID="sdsDistinctQuestionGroups" ValueField="id" TextFormatString="{0} | {1} | {2}">
                                                    <ClearButton DisplayMode="Always" />
                                                    <Columns>
                                                        <dx:ListBoxColumn FieldName="audit_name" Caption="AUDIT" />
                                                        <dx:ListBoxColumn FieldName="group_position" Caption="NO" />
                                                        <dx:ListBoxColumn FieldName="group_name" Caption="GROUP" />
                                                    </Columns>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn FieldName="question_position" VisibleIndex="2" Caption="NO" Width="55">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="question" VisibleIndex="3" Caption="QUESTION">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="question_ENG" VisibleIndex="4" Caption="QUESTION ENG">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewBandColumn Caption="ANSWERS" VisibleIndex="5">
                                                <Columns>
                                                    <dx:GridViewDataCheckColumn FieldName="answer_OK" VisibleIndex="0" Caption="OK" Width="55">
                                                    </dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn FieldName="answer_NOK" VisibleIndex="1" Caption="NOK" Width="55">
                                                    </dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn FieldName="answer_NA" VisibleIndex="2" Caption="NA" Width="55">
                                                    </dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn FieldName="answer_NC" VisibleIndex="3" Caption="NC" Width="55">
                                                    </dx:GridViewDataCheckColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                    <asp:SqlDataSource ID="sdsAuditQuestions" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                                        SelectCommand="SELECT [saq].[id], [saq].[group_id], [saq].[question_position], [saq].[question], [saq].[question_ENG], [saq].[answer_OK], [saq].[answer_NOK], [saq].[answer_NA], [saq].[answer_NC]
                                                        FROM [setts_audit_questions] [saq]
                                                             LEFT JOIN [setts_audit_question_groups] [saqg] ON [saqg].[id] = [saq].[group_id]
                                                        ORDER BY [saqg].[audit_type], [saqg].[group_position], [saq].[question_position]"
                                        DeleteCommand="DELETE FROM [setts_audit_questions] WHERE [id] = @id"
                                        InsertCommand="INSERT INTO [setts_audit_questions] ([group_id], [question_position], [question], [question_ENG], [answer_OK], [answer_NOK], [answer_NA], [answer_NC]) VALUES (@group_id, @question_position, @question, @question_ENG, @answer_OK, @answer_NOK, @answer_NA, @answer_NC)"
                                        UpdateCommand="UPDATE [setts_audit_questions] SET [group_id] = @group_id, [question_position] = @question_position, [question] = @question, [question_ENG] = @question_ENG, [answer_OK] = @answer_OK, [answer_NOK] = @answer_NOK, [answer_NA] = @answer_NA, [answer_NC] = @answer_NC WHERE [id] = @id">
                                        <DeleteParameters>
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </DeleteParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="group_id" Type="Int32" />
                                            <asp:Parameter Name="question_position" Type="Int32" />
                                            <asp:Parameter Name="question" Type="String" />
                                            <asp:Parameter Name="question_ENG" Type="String" />
                                            <asp:Parameter Name="answer_OK" Type="Boolean" />
                                            <asp:Parameter Name="answer_NOK" Type="Boolean" />
                                            <asp:Parameter Name="answer_NA" Type="Boolean" />
                                            <asp:Parameter Name="answer_NC" Type="Boolean" />
                                        </InsertParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="group_id" Type="Int32" />
                                            <asp:Parameter Name="question_position" Type="Int32" />
                                            <asp:Parameter Name="question" Type="String" />
                                            <asp:Parameter Name="question_ENG" Type="String" />
                                            <asp:Parameter Name="answer_OK" Type="Boolean" />
                                            <asp:Parameter Name="answer_NOK" Type="Boolean" />
                                            <asp:Parameter Name="answer_NA" Type="Boolean" />
                                            <asp:Parameter Name="answer_NC" Type="Boolean" />
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </UpdateParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="tab3" Text="AUDITORS / SCHEDULES">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table style="margin: 0 auto;">
                            <tr>
                                <td>
                                    <dx:ASPxGridView ID="gvAuditors" ClientInstanceName="gvAuditors" runat="server" AutoGenerateColumns="False" DataSourceID="sdsAuditors" KeyFieldName="id" Width="500px"
                                        OnInit="gvAuditors_Init" OnRowInserting="gvAuditors_RowInserting" OnRowUpdating="gvAuditors_RowUpdating">
                                        <ClientSideEvents Init="gv_Init" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="id" ReadOnly="True" VisibleIndex="0" Caption="ID" Width="55">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="audit_type" VisibleIndex="1" Caption="AUDIT TYPE" Width="200">
                                                <PropertiesComboBox DataSourceID="sdsDistinctAuditTypes" ValueField="audit_type" TextField="name">
                                                    <ClearButton DisplayMode="Always" />
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="auditor_login" VisibleIndex="2" Caption="AUDITOR">
                                                <PropertiesComboBox DataSourceID="sdsDistinctUsers" ValueField="user_login" TextFormatString="{0} | {1}">
                                                    <ClearButton DisplayMode="Always" />
                                                    <Columns>
                                                        <dx:ListBoxColumn FieldName="user_login" Caption="LOGIN" />
                                                        <dx:ListBoxColumn FieldName="user_fullname" Caption="PERSON" />
                                                    </Columns>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                    <asp:SqlDataSource ID="sdsAuditors" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                                        SelectCommand="SELECT [id], [audit_type], [auditor_login] FROM [setts_auditors] ORDER BY [audit_type], [auditor_login]"
                                        DeleteCommand="DELETE FROM [setts_auditors] WHERE [id] = @id"
                                        InsertCommand="INSERT INTO [setts_auditors] ([audit_type], [auditor_login]) VALUES (@audit_type, @auditor_login)"
                                        UpdateCommand="UPDATE [setts_auditors] SET [audit_type] = @audit_type, [auditor_login] = @auditor_login WHERE [id] = @id">
                                        <DeleteParameters>
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </DeleteParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="audit_type" Type="String" />
                                            <asp:Parameter Name="auditor_login" Type="String" />
                                        </InsertParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="audit_type" Type="String" />
                                            <asp:Parameter Name="auditor_login" Type="String" />
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </UpdateParameters>
                                    </asp:SqlDataSource>
                                </td>
                                <td>
                                    <dx:ASPxGridView ID="gvSchedules" ClientInstanceName="gvSchedules" runat="server" AutoGenerateColumns="False" DataSourceID="sdsSchedules" KeyFieldName="id" Width="500px"
                                        OnInit="gvSchedules_Init" OnRowInserting="gvSchedules_RowInserting" OnRowUpdating="gvSchedules_RowUpdating" OnCellEditorInitialize="gvSchedules_CellEditorInitialize">
                                        <ClientSideEvents Init="gv_Init" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="id" ReadOnly="True" VisibleIndex="0" Caption="ID" Width="55">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="schedule_name" ShowInCustomizationForm="True" VisibleIndex="1" Caption="SYSTEM NAME">
                                                <PropertiesComboBox>
                                                    <ClearButton DisplayMode="Always" />
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn FieldName="schedule_description" ShowInCustomizationForm="True" VisibleIndex="2" Caption="NAME DESC">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                    <asp:SqlDataSource ID="sdsSchedules" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                                        SelectCommand="SELECT [id], [schedule_name], [schedule_description] FROM [setts_schedules]"
                                        DeleteCommand="DELETE FROM [setts_schedules] WHERE [id] = @id"
                                        InsertCommand="INSERT INTO [setts_schedules] ([schedule_name], [schedule_description]) VALUES (@schedule_name, @schedule_description)"
                                        UpdateCommand="UPDATE [setts_schedules] SET [schedule_name] = @schedule_name, [schedule_description] = @schedule_description WHERE [id] = @id">
                                        <DeleteParameters>
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </DeleteParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="schedule_name" Type="String" />
                                            <asp:Parameter Name="schedule_description" Type="String" />
                                        </InsertParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="schedule_name" Type="String" />
                                            <asp:Parameter Name="schedule_description" Type="String" />
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </UpdateParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="tab4" Text="OTHERS">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table style="margin: 0 auto;">
                            <tr>
                                <td>
                                    <dx:ASPxGridView ID="gvShifts" ClientInstanceName="gvShifts" runat="server" AutoGenerateColumns="False" DataSourceID="sdsShifts" KeyFieldName="id" Width="400px"
                                        OnInit="gvShifts_Init" OnRowInserting="gvShifts_RowInserting" OnRowUpdating="gvShifts_RowUpdating" OnCellEditorInitialize="gvShifts_CellEditorInitialize">
                                        <ClientSideEvents Init="gv_Init" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="id" ReadOnly="True" VisibleIndex="0" Caption="ID" Width="55">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="shift_name" ShowInCustomizationForm="True" VisibleIndex="1" Caption="NAME">
                                                <PropertiesComboBox>
                                                    <ClearButton DisplayMode="Always" />
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                    <asp:SqlDataSource ID="sdsShifts" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
                                        SelectCommand="SELECT [id], [shift_name] FROM [setts_shifts] ORDER BY [shift_name]"
                                        DeleteCommand="DELETE FROM [setts_shifts] WHERE [id] = @id"
                                        InsertCommand="INSERT INTO [setts_shifts] ([shift_name]) VALUES (@shift_name)"
                                        UpdateCommand="UPDATE [setts_shifts] SET [shift_name] = @shift_name WHERE [id] = @id">
                                        <DeleteParameters>
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </DeleteParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="shift_name" Type="String" />
                                        </InsertParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="shift_name" Type="String" />
                                            <asp:Parameter Name="id" Type="Int32" />
                                        </UpdateParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="tab5" Text="USERS / ROLES">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table style="margin: 0 auto;">
                            <tr>
                                <td>
                                    <dx:ASPxGridView ID="gvUsers" ClientInstanceName="gvUsers" runat="server" AutoGenerateColumns="False" DataSourceID="sdsUsers" KeyFieldName="username" Width="700px"
                                        OnInit="gvUsers_Init" OnRowInserting="gvUsers_RowInserting" OnRowUpdating="gvUsers_RowUpdating" OnRowDeleting="gvUsers_RowDeleting" OnToolbarItemClick="gvUsers_ToolbarItemClick">
                                        <ClientSideEvents Init="gv_Init" EndCallback="gvUsers_EndCallback" ToolbarItemClick="gvUsers_ToolbarItemClick" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="username" VisibleIndex="0" Caption="USERNAME">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="name" VisibleIndex="1" Caption="NAME">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="lastname" VisibleIndex="2" Caption="LAST NAME">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="email" VisibleIndex="3" Caption="EMAIL" Width="200px">
                                                <EditFormSettings ColumnSpan="2" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataCheckColumn FieldName="islockedout" VisibleIndex="4" Caption="BLOCKED" Width="100px" ReadOnly="true">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataCheckColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                    <asp:SqlDataSource ID="sdsUsers" runat="server" ConnectionString="<%$ ConnectionStrings:csServices %>"
                                        SelectCommand="SELECT UPPER([a].[loweredusername]) AS [username],
                                                              [a].[name],
                                                              [a].[lastname],
                                                              [a].[email],
                                                              [b].[islockedout]
                                                    FROM [aspnet_Users] [a]
                                                         LEFT JOIN [aspnet_Membership] [b] ON [a].[UserId] = [b].[UserId]
                                                    ORDER BY [a].[LoweredUserName];"
                                        UpdateCommand="SELECT GETDATE();"
                                        DeleteCommand="SELECT GETDATE();"
                                        InsertCommand="SELECT GETDATE();">
                                        <InsertParameters>
                                            <asp:Parameter Name="username" DbType="String" />
                                            <asp:Parameter Name="name" DbType="String" />
                                            <asp:Parameter Name="lastname" DbType="String" />
                                            <asp:Parameter Name="email" DbType="String" />
                                        </InsertParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="username" DbType="String" />
                                            <asp:Parameter Name="name" DbType="String" />
                                            <asp:Parameter Name="lastname" DbType="String" />
                                            <asp:Parameter Name="email" DbType="String" />
                                        </UpdateParameters>
                                        <DeleteParameters>
                                            <asp:Parameter Name="username" DbType="String" />
                                        </DeleteParameters>
                                    </asp:SqlDataSource>
                                </td>
                                <td>
                                    <dx:ASPxGridView ID="gvRoles" ClientInstanceName="gvRoles" runat="server" AutoGenerateColumns="False" DataSourceID="sdsRoles" KeyFieldName="rolename" Width="600px"
                                        OnInit="gvRoles_Init" OnRowInserting="gvRoles_RowInserting" OnRowUpdating="gvRoles_RowUpdating" OnRowDeleting="gvRoles_RowDeleting">
                                        <ClientSideEvents Init="gv_Init" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="rolename" VisibleIndex="0" Caption="ROLE NAME" Width="200px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="description" VisibleIndex="1" Caption="DESCRIPTION">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Templates>
                                            <DetailRow>
                                                <dx:ASPxGridView ID="gvUsersInRole" ClientInstanceName="gvUsersInRole" runat="server" AutoGenerateColumns="False" DataSourceID="sdsUsersInRole" KeyFieldName="username" Width="300px"
                                                    OnInit="gvUsersInRole_Init" OnRowInserting="gvUsersInRole_RowInserting" OnRowDeleting="gvUsersInRole_RowDeleting" OnBeforePerformDataSelect="gvUsersInRole_BeforePerformDataSelect"
                                                    Style="margin: 0 auto;">
                                                    <ClientSideEvents Init="gvUsersInRole_Init" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="rolename" VisibleIndex="0" Visible="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataComboBoxColumn FieldName="username" VisibleIndex="1" Caption="USER">
                                                            <PropertiesComboBox DataSourceID="sdsDistinctUsers" ValueField="user_login" TextFormatString="{0} | {1}">
                                                                <ClearButton DisplayMode="Always" />
                                                                <Columns>
                                                                    <dx:ListBoxColumn FieldName="user_login" Caption="LOGIN" />
                                                                    <dx:ListBoxColumn FieldName="user_fullname" Caption="PERSON" />
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </DetailRow>
                                        </Templates>
                                    </dx:ASPxGridView>
                                    <asp:SqlDataSource ID="sdsRoles" runat="server" ConnectionString="<%$ ConnectionStrings:csServices %>"
                                        SelectCommand="SELECT   UPPER([loweredrolename]) AS [rolename],
                                                                [description]
                                                        FROM [aspnet_Roles]
                                                        ORDER BY [loweredrolename];"
                                        UpdateCommand="SELECT GETDATE();"
                                        DeleteCommand="SELECT GETDATE();"
                                        InsertCommand="SELECT GETDATE();">
                                        <InsertParameters>
                                            <asp:Parameter Name="rolename" DbType="String" />
                                            <asp:Parameter Name="description" DbType="String" />
                                        </InsertParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="rolename" DbType="String" />
                                            <asp:Parameter Name="description" DbType="String" />
                                        </UpdateParameters>
                                        <DeleteParameters>
                                            <asp:Parameter Name="rolename" DbType="String" />
                                        </DeleteParameters>
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="sdsUsersInRole" runat="server" ConnectionString="<%$ ConnectionStrings:csServices %>"
                                        SelectCommand="SELECT  UPPER([r].[loweredrolename]) AS [rolename],
	                                                          UPPER([u].[loweredusername]) AS [username]
                                                        FROM [aspnet_UsersInRoles] [uir]
                                                        LEFT JOIN [aspnet_Roles] [r] ON [r].[RoleId]=[uir].[RoleId]
                                                        LEFT JOIN [aspnet_Users] [u] ON [u].[UserId]=[uir].[UserId]
                                                        WHERE [r].[loweredrolename] = @rolename;"
                                        DeleteCommand="SELECT GETDATE();"
                                        InsertCommand="SELECT GETDATE();">
                                        <SelectParameters>
                                            <asp:Parameter Name="rolename" />
                                        </SelectParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="rolename" DbType="String" />
                                            <asp:Parameter Name="username" DbType="String" />
                                        </InsertParameters>
                                        <DeleteParameters>
                                            <asp:Parameter Name="rolename" DbType="String" />
                                            <asp:Parameter Name="username" DbType="String" />
                                        </DeleteParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    <asp:SqlDataSource ID="sdsDistinctAuditTypes" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
        SelectCommand="SELECT [id], [audit_type], [name] FROM [setts_audit_types] ORDER BY [name]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsDistinctQuestionGroups" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
        SelectCommand="SELECT [aqg].[id], [aqg].[audit_type], [aqg].[group_position], [aqg].[group_name], [aqg].[group_name_ENG], [typ].[name] AS [audit_name]
                        FROM [setts_audit_question_groups] [aqg]
                            LEFT JOIN [setts_audit_types] [typ] ON [typ].[audit_type] = [aqg].[audit_type]
                        ORDER BY [aqg].[audit_type], [aqg].[group_position]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsDistinctUsers" runat="server" ConnectionString="<%$ ConnectionStrings:csApp %>"
        SelectCommand="SELECT [user_login], [user_fullname] FROM [vUsers] ORDER BY [user_login]"></asp:SqlDataSource>
</asp:Content>