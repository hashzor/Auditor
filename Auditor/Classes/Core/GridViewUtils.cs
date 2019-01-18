using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Drawing;

namespace Auditor
{
    public static class GridViewUtils
    {
        public static string ToolbarGrid => "TOOLBAR_GRID";

        public static string ToolbarItem => "TOOLBAR_ITEM";

        public static string ToolbarBatch => "TOOLBAR_BATCH";

        public static int ImageSize => 20;

        public static int ImageToolbarSize => 16;

        public static void ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            e.Column.HeaderStyle.BackColor = ReferenceEquals(e.Criteria, null) ? ColorTranslator.FromHtml("#DCDCDC") : ColorTranslator.FromHtml("#87C4EE");
        }

        public static void ExporterRenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
            if (e.RowType == GridViewRowType.Header)
            {
                e.Text = e.Text.Replace("<br />", " ");
                e.Text = e.Text.Replace("<br/>", " ");
            }
        }

        public static XlsxExportOptionsEx ExporterOptions => new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG };

        public static void GridViewDefaultInit(object sender, EventArgs e)
        {
            ASPxGridView gridview = sender as ASPxGridView;

            gridview.SettingsDataSecurity.AllowInsert = false;
            gridview.SettingsDataSecurity.AllowEdit = false;
            gridview.SettingsDataSecurity.AllowDelete = false;

            gridview.SettingsCommandButton.RenderMode = GridCommandButtonRenderMode.Image;
            gridview.SettingsCommandButton.NewButton.Image.AlternateText = "NEW";
            gridview.SettingsCommandButton.NewButton.Image.ToolTip = gridview.SettingsCommandButton.NewButton.Image.AlternateText;
            gridview.SettingsCommandButton.NewButton.Image.Height = GridViewUtils.ImageSize;
            gridview.SettingsCommandButton.NewButton.Image.Width = GridViewUtils.ImageSize;
            gridview.SettingsCommandButton.NewButton.Image.Url = "Images/add.png";
            gridview.SettingsCommandButton.EditButton.Image.AlternateText = "EDIT";
            gridview.SettingsCommandButton.EditButton.Image.ToolTip = gridview.SettingsCommandButton.EditButton.Image.AlternateText;
            gridview.SettingsCommandButton.EditButton.Image.Height = GridViewUtils.ImageSize;
            gridview.SettingsCommandButton.EditButton.Image.Width = GridViewUtils.ImageSize;
            gridview.SettingsCommandButton.EditButton.Image.Url = "Images/edit.png";
            gridview.SettingsCommandButton.DeleteButton.Image.AlternateText = "DELETE";
            gridview.SettingsCommandButton.DeleteButton.Image.ToolTip = gridview.SettingsCommandButton.DeleteButton.Image.AlternateText;
            gridview.SettingsCommandButton.DeleteButton.Image.Height = GridViewUtils.ImageSize;
            gridview.SettingsCommandButton.DeleteButton.Image.Width = GridViewUtils.ImageSize;
            gridview.SettingsCommandButton.DeleteButton.Image.Url = "Images/trash.png";
            gridview.SettingsCommandButton.UpdateButton.Image.AlternateText = "SAVE";
            gridview.SettingsCommandButton.UpdateButton.Image.ToolTip = gridview.SettingsCommandButton.UpdateButton.Image.AlternateText;
            gridview.SettingsCommandButton.UpdateButton.Image.Height = GridViewUtils.ImageSize;
            gridview.SettingsCommandButton.UpdateButton.Image.Width = GridViewUtils.ImageSize;
            gridview.SettingsCommandButton.UpdateButton.Image.Url = "Images/ok.png";
            gridview.SettingsCommandButton.CancelButton.Image.AlternateText = "CANCEL";
            gridview.SettingsCommandButton.CancelButton.Image.ToolTip = gridview.SettingsCommandButton.CancelButton.Image.AlternateText;
            gridview.SettingsCommandButton.CancelButton.Image.Height = GridViewUtils.ImageSize;
            gridview.SettingsCommandButton.CancelButton.Image.Width = GridViewUtils.ImageSize;
            gridview.SettingsCommandButton.CancelButton.Image.Url = "Images/nok.png";

            gridview.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
            gridview.Settings.ShowFilterRowMenu = true;
            gridview.Settings.ShowHeaderFilterButton = true;
            gridview.Settings.ShowTitlePanel = true;
            gridview.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;

            gridview.SettingsBehavior.ConfirmDelete = true;
            gridview.SettingsBehavior.AllowFocusedRow = true;

            gridview.KeyboardSupport = true;

            gridview.SettingsLoadingPanel.Mode = GridViewLoadingPanelMode.ShowAsPopup;

            gridview.SettingsPager.AlwaysShowPager = true;
            gridview.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            gridview.SettingsPager.PageSize = 500;

            gridview.SettingsSearchPanel.AllowTextInputTimer = true;
            gridview.SettingsSearchPanel.Delay = 5000;

            gridview.SettingsPopup.EditForm.AllowResize = false;
            gridview.SettingsPopup.EditForm.HorizontalAlign = PopupHorizontalAlign.WindowCenter;
            gridview.SettingsPopup.EditForm.VerticalAlign = PopupVerticalAlign.WindowCenter;
            gridview.SettingsPopup.EditForm.Modal = true;

            gridview.SettingsExport.ExcelExportMode = ExportType.WYSIWYG;
            gridview.SettingsExport.EnableClientSideExportAPI = true;
            gridview.SettingsExport.FileName = "DATA";

            gridview.Styles.AlternatingRow.Enabled = DevExpress.Utils.DefaultBoolean.True;
            gridview.Styles.Cell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            gridview.Styles.Header.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            gridview.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;
            gridview.Styles.TitlePanel.Font.Bold = true;
            gridview.StylesEditors.ReadOnly.BackColor = Color.Black;
            gridview.StylesEditors.ReadOnly.ForeColor = Color.White;
            gridview.Font.Size = new System.Web.UI.WebControls.FontUnit("10px");
            gridview.Styles.TitlePanel.BackColor = ColorTranslator.FromHtml("#64645B");
            gridview.Styles.FocusedRow.BackColor = ColorTranslator.FromHtml("#0080FF");
            gridview.Styles.BatchEditModifiedCell.ForeColor = Color.Black;

            gridview.Styles.LoadingPanel.BackColor = ColorTranslator.FromHtml("#FFFFB0");
            gridview.Styles.LoadingPanel.ForeColor = ColorTranslator.FromHtml("#0080FF");
            gridview.Styles.LoadingPanel.Font.Bold = true;
            gridview.Styles.LoadingPanel.Font.Size = new System.Web.UI.WebControls.FontUnit("12px");
        }

        public static void GridViewToolbarInit(object sender, EventArgs e)
        {
            ASPxGridView gridview = sender as ASPxGridView;

            var exportExcel = new GridViewToolbarItem();
            exportExcel.Command = GridViewToolbarCommand.ExportToXlsx;
            var search = new GridViewToolbarItem();
            search.Command = GridViewToolbarCommand.ShowSearchPanel;
            var refresh = new GridViewToolbarItem();
            refresh.Command = GridViewToolbarCommand.Refresh;
            var clear = new GridViewToolbarItem();
            clear.Command = GridViewToolbarCommand.ClearFilter;
            var newItem = new GridViewToolbarItem();
            newItem.Command = GridViewToolbarCommand.New;
            var deleteItem = new GridViewToolbarItem();
            deleteItem.Command = GridViewToolbarCommand.Delete;
            var editItem = new GridViewToolbarItem();
            editItem.Command = GridViewToolbarCommand.Edit;
            var update = new GridViewToolbarItem();
            update.Command = GridViewToolbarCommand.Update;
            update.Image.Url = "Images/ok.png";
            update.Image.Height = GridViewUtils.ImageToolbarSize;
            update.Image.Width = GridViewUtils.ImageToolbarSize;
            var cancel = new GridViewToolbarItem();
            cancel.Command = GridViewToolbarCommand.Cancel;
            cancel.Image.Url = "Images/nok.png";
            cancel.Image.Height = GridViewUtils.ImageToolbarSize;
            cancel.Image.Width = GridViewUtils.ImageToolbarSize;
            var filter = new GridViewToolbarItem();
            filter.Command = GridViewToolbarCommand.ShowFilterRow;
            filter.Image.Url = "Images/filter.png";
            filter.Image.Height = GridViewUtils.ImageToolbarSize;
            filter.Image.Width = GridViewUtils.ImageToolbarSize;

            var toolbarGrid = new GridViewToolbar();
            toolbarGrid.Name = GridViewUtils.ToolbarGrid;
            toolbarGrid.Position = GridToolbarPosition.Top;
            toolbarGrid.Items.Add(refresh);
            toolbarGrid.Items.Add(search);
            toolbarGrid.Items.Add(filter);
            toolbarGrid.Items.Add(clear);
            toolbarGrid.Items.Add(exportExcel);
            if (toolbarGrid.Items.Count > 0)
            {
                gridview.Toolbars.Add(toolbarGrid);
            }

            var toolbarItem = new GridViewToolbar();
            toolbarItem.Name = GridViewUtils.ToolbarItem;
            toolbarItem.Position = GridToolbarPosition.Top;
            if (gridview.SettingsDataSecurity.AllowInsert)
            {
                toolbarItem.Items.Add(newItem);
            }
            if (gridview.SettingsDataSecurity.AllowEdit)
            {
                toolbarItem.Items.Add(editItem);
            }
            if (gridview.SettingsDataSecurity.AllowDelete)
            {
                toolbarItem.Items.Add(deleteItem);
            }
            if (toolbarItem.Items.Count > 0)
            {
                gridview.Toolbars.Add(toolbarItem);
            }

            if (gridview.SettingsEditing.Mode == GridViewEditingMode.Batch
                && (gridview.SettingsDataSecurity.AllowInsert
                    || gridview.SettingsDataSecurity.AllowEdit
                    || gridview.SettingsDataSecurity.AllowDelete))
            {
                var toolbarBatch = new GridViewToolbar();
                toolbarBatch.Name = GridViewUtils.ToolbarBatch;
                toolbarBatch.Position = GridToolbarPosition.Bottom;
                toolbarBatch.Items.Add(update);
                toolbarBatch.Items.Add(cancel);
                gridview.Settings.ShowStatusBar = GridViewStatusBarMode.Hidden;
                gridview.Toolbars.Add(toolbarBatch);
            }
        }
    }
}