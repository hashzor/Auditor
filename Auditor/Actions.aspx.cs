using DevExpress.Web;
using System;

namespace Auditor
{
    public partial class Actions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void gvActions_Init(object sender, EventArgs e)
        {
            GridViewUtils.GridViewDefaultInit(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;

            gridview.SettingsEditing.Mode = GridViewEditingMode.Batch;
            gridview.SettingsDataSecurity.AllowEdit = ActiveUser.IsAuthenticated;
            gridview.SettingsText.Title = "ACTIONS";
            gridview.SettingsExport.FileName = $"Actions_{DateTime.Now.ToString("yyyyMMdd")}";
            gridview.SettingsPager.PageSize = 50;

            GridViewUtils.GridViewToolbarInit(sender, e);
            var toolbarItem = gridview.Toolbars.FindByName(GridViewUtils.ToolbarItem);
            if (toolbarItem != null)
            {
                toolbarItem.Visible = false;
            }
        }

        protected void gvActions_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var responsibleLogin = Utils.ConvertToTrimmedString(e.OldValues["responsible_login"]);
            var action = Utils.ConvertToTrimmedString(e.OldValues["action"]);
            var plannedTerm = Utils.ConvertToNullableDateTime(e.OldValues["planned_term"]);
            var newAction = Utils.ConvertToTrimmedString(e.NewValues["action"]);
            var newPlannedTerm = Utils.ConvertToNullableDateTime(e.NewValues["planned_term"]);

            if (responsibleLogin == null)
            {
                e.Cancel = true;
                throw new Exception("System error!");
            }
            responsibleLogin = responsibleLogin.ToUpper();
            if (!ActiveUser.IsAuthenticated)
            {
                e.Cancel = true;
                throw new Exception("Log in!");
            }
            var activeUser = new ActiveUser();
            if (!(responsibleLogin == activeUser.UserName || ActiveUser.IsInRole(AppRoles.AuditorAdmin)))
            {
                e.Cancel = true;
                throw new Exception("Only the responsible person can assign the action!");
            }

            if (action != null || plannedTerm != null)
            {
                e.Cancel = true;
                throw new Exception("You can not edit the completed action! Contact the administrator!");
            }

            if (newAction == null || newPlannedTerm == null)
            {
                e.Cancel = true;
                throw new Exception("Complete the action and the planned implementation date!");
            }
            if (newAction.Length > 200)
            {
                e.Cancel = true;
                throw new Exception("Action too long! Allowed 200 characters!");
            }

            e.NewValues["action"] = newAction;
            e.NewValues["planned_term"] = newPlannedTerm;
            e.NewValues["responsible_login"] = responsibleLogin;
        }

        protected void gvActions_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            ASPxGridView gridview = (ASPxGridView)sender;
            if (e.CellType == GridViewTableCommandCellType.Filter) return;
            if (e.VisibleIndex == -1) return;
            e.Visible = DevExpress.Utils.DefaultBoolean.False;

            var responsibleLogin = Utils.ConvertToTrimmedString(gridview.GetRowValues(e.VisibleIndex, "responsible_login"));

            var action = Utils.ConvertToTrimmedString(gridview.GetRowValues(e.VisibleIndex, "action"));
            var plannedTerm = Utils.ConvertToNullableDateTime(gridview.GetRowValues(e.VisibleIndex, "planned_term"));
            var term = Utils.ConvertToNullableDateTime(gridview.GetRowValues(e.VisibleIndex, "term"));

            if (ActiveUser.IsAuthenticated && responsibleLogin != null)
            {
                var activeUserLogin = (new ActiveUser()).UserName;
                responsibleLogin = responsibleLogin.ToUpper();
                if (e.ButtonID == "btnConfirm")
                {
                    e.Visible = (ActiveUser.IsInRole(AppRoles.AuditorAdmin) || responsibleLogin == activeUserLogin) && term == null && plannedTerm != null && action != null ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                }
                if (e.ButtonID == "btnClear")
                {
                    e.Visible = ActiveUser.IsInRole(AppRoles.AuditorAdmin) ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }

        protected void gvActions_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            ASPxGridView gridview = (ASPxGridView)sender;
            bool refresh = true;
            var rowId = Utils.ConvertToNullableInt(gridview.GetRowValues(e.VisibleIndex, gridview.KeyFieldName));
            if (rowId == null || !Action.ActionExist(rowId.Value))
            {
                throw new Exception("System error!");
            }

            var action = new Action(rowId.Value);

            var gridAction = Utils.ConvertToTrimmedString(gridview.GetRowValues(e.VisibleIndex, "action"));
            var gridPlannedTerm = Utils.ConvertToNullableDateTime(gridview.GetRowValues(e.VisibleIndex, "planned_term"));
            var gridTerm = Utils.ConvertToNullableDateTime(gridview.GetRowValues(e.VisibleIndex, "term"));

            if (gridAction != action.ActionText || gridPlannedTerm != action.PlannedTerm || gridTerm != action.Term)
            {
                throw new Exception("Error! Refresh page!");
            }

            if (!ActiveUser.IsAuthenticated)
            {
                throw new Exception("Log in!");
            }

            if (e.ButtonID == "btnConfirm")
            {
                action.Confirm();
            }
            if (e.ButtonID == "btnClear")
            {
                action.Clear();
            }
            if (refresh)
            {
                gridview.JSProperties["cp_refresh"] = true;
            }
        }
    }
}