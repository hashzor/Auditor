using DevExpress.Web;
using System;
using System.Drawing;
using System.Web.UI.WebControls;

namespace Auditor
{
    public partial class PerformAudit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ActiveUser.IsAuthenticated)
            {
                Response.Redirect(Pages.Login);
            }
            var activeAudit = Audit.GetUserActiveAudit(new ActiveUser().UserName);
            if (activeAudit == null)
            {
                Response.Redirect(Pages.Default);
            }
            SetupGridview(activeAudit);
        }

        protected void gvAudit_Init(object sender, EventArgs e)
        {
            ASPxGridView gridview = sender as ASPxGridView;
            string activeLang = (Session["lang"] == null) ? Languages.Default : Utils.ConvertToTrimmedString(Session["lang"]);

            int imageSize = 40;
            int imageSizeToolbar = 40;
            var toolbarButtonColor = ColorTranslator.FromHtml("#D6D6D6");
            var toolbarButtonHooverColor = ColorTranslator.FromHtml("#FFFFFF");

            gridview.SettingsDataSecurity.AllowInsert = false;
            gridview.SettingsDataSecurity.AllowEdit = true;
            gridview.SettingsDataSecurity.AllowDelete = false;

            #region Image Buttons

            gridview.SettingsCommandButton.RenderMode = GridCommandButtonRenderMode.Image;
            gridview.SettingsCommandButton.NewButton.Image.AlternateText = "NEW";
            gridview.SettingsCommandButton.NewButton.Image.ToolTip = gridview.SettingsCommandButton.NewButton.Image.AlternateText;
            gridview.SettingsCommandButton.NewButton.Image.Height = imageSize;
            gridview.SettingsCommandButton.NewButton.Image.Width = imageSize;
            gridview.SettingsCommandButton.NewButton.Image.Url = "Images/add.png";
            gridview.SettingsCommandButton.EditButton.Image.AlternateText = "EDIT";
            gridview.SettingsCommandButton.EditButton.Image.ToolTip = gridview.SettingsCommandButton.EditButton.Image.AlternateText;
            gridview.SettingsCommandButton.EditButton.Image.Height = imageSize;
            gridview.SettingsCommandButton.EditButton.Image.Width = imageSize;
            gridview.SettingsCommandButton.EditButton.Image.Url = "Images/edit.png";
            gridview.SettingsCommandButton.DeleteButton.Image.AlternateText = "DELETE";
            gridview.SettingsCommandButton.DeleteButton.Image.ToolTip = gridview.SettingsCommandButton.DeleteButton.Image.AlternateText;
            gridview.SettingsCommandButton.DeleteButton.Image.Height = imageSize;
            gridview.SettingsCommandButton.DeleteButton.Image.Width = imageSize;
            gridview.SettingsCommandButton.DeleteButton.Image.Url = "Images/trash.png";
            gridview.SettingsCommandButton.UpdateButton.Image.AlternateText = "SAVE";
            gridview.SettingsCommandButton.UpdateButton.Image.ToolTip = gridview.SettingsCommandButton.UpdateButton.Image.AlternateText;
            gridview.SettingsCommandButton.UpdateButton.Image.Height = imageSize;
            gridview.SettingsCommandButton.UpdateButton.Image.Width = imageSize;
            gridview.SettingsCommandButton.UpdateButton.Image.Url = "Images/ok.png";
            gridview.SettingsCommandButton.CancelButton.Image.AlternateText = "CANCEL";
            gridview.SettingsCommandButton.CancelButton.Image.ToolTip = gridview.SettingsCommandButton.CancelButton.Image.AlternateText;
            gridview.SettingsCommandButton.CancelButton.Image.Height = imageSize;
            gridview.SettingsCommandButton.CancelButton.Image.Width = imageSize;
            gridview.SettingsCommandButton.CancelButton.Image.Url = "Images/nok.png";

            #endregion Image Buttons

            gridview.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
            gridview.Settings.ShowFilterRowMenu = false;
            gridview.Settings.ShowHeaderFilterButton = false;
            gridview.Settings.ShowTitlePanel = true;
            gridview.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
            gridview.SettingsBehavior.ConfirmDelete = true;
            gridview.SettingsBehavior.AllowFocusedRow = true;
            gridview.SettingsBehavior.AllowSort = false;
            gridview.SettingsBehavior.AllowDragDrop = false;
            gridview.KeyboardSupport = true;
            gridview.SettingsLoadingPanel.Mode = GridViewLoadingPanelMode.ShowAsPopup;
            gridview.SettingsPager.AlwaysShowPager = false;
            gridview.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            gridview.SettingsPopup.EditForm.AllowResize = false;
            gridview.SettingsPopup.EditForm.HorizontalAlign = PopupHorizontalAlign.WindowCenter;
            gridview.SettingsPopup.EditForm.VerticalAlign = PopupVerticalAlign.WindowCenter;
            gridview.SettingsPopup.EditForm.VerticalOffset = -200;
            gridview.SettingsPopup.EditForm.Modal = true;
            gridview.SettingsPopup.EditForm.Width = new Unit("800px");
            gridview.SettingsPopup.EditForm.ShowCloseButton = false;
            gridview.SettingsEditing.Mode = GridViewEditingMode.PopupEditForm;
            gridview.SettingsText.PopupEditFormCaption = "EDITION";
            gridview.Styles.FocusedRow.BackColor = ColorTranslator.FromHtml("#0080FF");
            gridview.Styles.Cell.HorizontalAlign = HorizontalAlign.Center;
            gridview.Styles.Header.HorizontalAlign = HorizontalAlign.Center;
            gridview.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;
            gridview.StylesEditors.ReadOnly.BackColor = Color.Black;
            gridview.StylesEditors.ReadOnly.ForeColor = Color.White;
            gridview.Styles.TitlePanel.BackColor = ColorTranslator.FromHtml("#64645B");
            gridview.Font.Size = new FontUnit("18px");
            gridview.StylesToolbar.Style.ItemSpacing = new Unit("60px");
            gridview.StylesToolbar.Style.Paddings.PaddingTop = new Unit("10px");
            gridview.StylesToolbar.Style.Paddings.PaddingBottom = new Unit("10px");

            gridview.Styles.LoadingPanel.BackColor = ColorTranslator.FromHtml("#FFFFB0");
            gridview.Styles.LoadingPanel.ForeColor = ColorTranslator.FromHtml("#0080FF");
            gridview.Styles.LoadingPanel.Font.Bold = true;
            gridview.Styles.LoadingPanel.Font.Size = new System.Web.UI.WebControls.FontUnit("12px");

            #region TopToolbar

            var print = new GridViewToolbarItem();
            print.Command = GridViewToolbarCommand.Custom;
            print.Name = ToolbarButtons.PrintAudit;
            print.Text = "Print";
            print.Image.Url = "Images/printer.png";
            var endAudit = new GridViewToolbarItem();
            endAudit.Command = GridViewToolbarCommand.Custom;
            endAudit.Name = ToolbarButtons.EndAudit;
            endAudit.Text = "End";
            endAudit.Image.Url = "Images/task-complete.png";
            var deleteAudit = new GridViewToolbarItem();
            deleteAudit.Command = GridViewToolbarCommand.Custom;
            deleteAudit.Name = ToolbarButtons.DeleteAudit;
            deleteAudit.Text = "Delete";
            deleteAudit.Image.Url = "Images/trash.png";
            var nextPage = new GridViewToolbarItem();
            nextPage.Command = GridViewToolbarCommand.Custom;
            nextPage.Name = ToolbarButtons.NextPage;
            nextPage.Text = "Next";
            nextPage.Image.Url = "Images/next.png";

            var prevPage = new GridViewToolbarItem();
            prevPage.Command = GridViewToolbarCommand.Custom;
            prevPage.Name = ToolbarButtons.PrevPage;
            prevPage.Text = "Prev";
            prevPage.Image.Url = "Images/prev.png";
            var polish = new GridViewToolbarItem();
            polish.Command = GridViewToolbarCommand.Custom;
            polish.Name = ToolbarButtons.LangPol;
            polish.Text = "POL";
            polish.Image.Url = "Images/poland.png";
            var english = new GridViewToolbarItem();
            english.Command = GridViewToolbarCommand.Custom;
            english.Name = ToolbarButtons.LangEng;
            english.Text = "ENG";
            english.Image.Url = "Images/england.png";

            var toolbarTop = new GridViewToolbar();
            gridview.Toolbars.Add(toolbarTop);
            toolbarTop.Position = GridToolbarPosition.Top;
            toolbarTop.ItemAlign = GridToolbarItemAlign.Center;
            switch (activeLang)
            {
                case Languages.English:
                    toolbarTop.Items.Add(polish);
                    break;

                case Languages.Polish:
                    toolbarTop.Items.Add(english);
                    break;
            }
            toolbarTop.Items.Add(print);
            toolbarTop.Items.Add(endAudit);
            toolbarTop.Items.Add(deleteAudit);
            toolbarTop.Items.Add(prevPage);
            toolbarTop.Items.Add(nextPage);
            foreach (GridViewToolbarItem item in toolbarTop.Items)
            {
                item.Image.AlternateText = item.Text;
                item.Image.ToolTip = item.Text;
                item.Image.Height = imageSizeToolbar;
                item.Image.Width = imageSizeToolbar;
                item.ItemStyle.BackColor = toolbarButtonColor;
                item.ItemStyle.HoverStyle.BackColor = toolbarButtonHooverColor;
            }

            #endregion TopToolbar

            #region BotToolbar

            var editItem = new GridViewToolbarItem();
            editItem.Command = GridViewToolbarCommand.Custom;
            editItem.Name = ToolbarButtons.Edit;
            editItem.Text = "Comment";
            editItem.Image.Url = "Images/chat.png";
            var answerOK = new GridViewToolbarItem();
            answerOK.Command = GridViewToolbarCommand.Custom;
            answerOK.Name = ToolbarButtons.AnswerOK;
            answerOK.Text = "OK";
            answerOK.Image.Url = "Images/ans_ok.png";
            var answerNOK = new GridViewToolbarItem();
            answerNOK.Command = GridViewToolbarCommand.Custom;
            answerNOK.Name = ToolbarButtons.AnswerNOK;
            answerNOK.Text = "NOK";
            answerNOK.Image.Url = "Images/ans_nok.png";
            var answerNC = new GridViewToolbarItem();
            answerNC.Command = GridViewToolbarCommand.Custom;
            answerNC.Name = ToolbarButtons.AnswerNC;
            answerNC.Text = "NC";
            answerNC.Image.Url = "Images/ans_nc.png";
            var answerNA = new GridViewToolbarItem();
            answerNA.Command = GridViewToolbarCommand.Custom;
            answerNA.Name = ToolbarButtons.AnswerNA;
            answerNA.Text = "NA";
            answerNA.Image.Url = "Images/ans_na.png";
            var camera = new GridViewToolbarItem();
            camera.Command = GridViewToolbarCommand.Custom;
            camera.Name = ToolbarButtons.Camera;
            camera.Text = "Camera";
            camera.Image.Url = "Images/camera.png";

            var toolbarBot = new GridViewToolbar();
            gridview.Toolbars.Add(toolbarBot);
            toolbarBot.Position = GridToolbarPosition.Bottom;
            toolbarBot.ItemAlign = GridToolbarItemAlign.Center;
            toolbarBot.Items.Add(answerOK);
            toolbarBot.Items.Add(answerNOK);
            toolbarBot.Items.Add(answerNC);
            toolbarBot.Items.Add(answerNA);
            toolbarBot.Items.Add(editItem);
            toolbarBot.Items.Add(camera);
            foreach (GridViewToolbarItem item in toolbarBot.Items)
            {
                item.Image.AlternateText = item.Text;
                item.Image.ToolTip = item.Text;
                item.Image.Height = imageSizeToolbar;
                item.Image.Width = imageSizeToolbar;
                item.ItemStyle.BackColor = toolbarButtonColor;
                item.ItemStyle.HoverStyle.BackColor = toolbarButtonHooverColor;
            }

            #endregion BotToolbar
        }

        protected void gvAudit_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                e.Row.Height = Unit.Pixel(50);
            }
        }

        protected void gvAudit_ToolbarItemClick(object source, DevExpress.Web.Data.ASPxGridViewToolbarItemClickEventArgs e)
        {
            ASPxGridView gridview = source as ASPxGridView;
            string activeLang = (Session["lang"] == null) ? Languages.Default : Utils.ConvertToTrimmedString(Session["lang"]);
            var auditId = Utils.ConvertToNullableInt(gridview.GetRowValues(gridview.FocusedRowIndex, "audit_id"));
            var auditDetailsId = Utils.ConvertToNullableInt(gridview.GetRowValues(gridview.FocusedRowIndex, "id"));
            var allowedOK = Utils.ConvertToNullableBool(gridview.GetRowValues(gridview.FocusedRowIndex, "answer_OK"));
            var allowedNOK = Utils.ConvertToNullableBool(gridview.GetRowValues(gridview.FocusedRowIndex, "answer_NOK"));
            var allowedNC = Utils.ConvertToNullableBool(gridview.GetRowValues(gridview.FocusedRowIndex, "answer_NC"));
            var allowedNA = Utils.ConvertToNullableBool(gridview.GetRowValues(gridview.FocusedRowIndex, "answer_NA"));

            if (allowedNA == null || allowedNC == null || allowedNOK == null || allowedOK == null || auditId == null || auditDetailsId == null)
            {
                throw new Exception("System error!");
            }
            var audit = new Audit((int)auditId);
            if (!audit.AuditExist() || audit.EndDate != null)
            {
                throw new Exception("Audit does not exist / has already been completed!");
            }
            bool refresh = false;
            bool move = false;
            switch (e.Item.Name)
            {
                case ToolbarButtons.NextPage:
                    ChangePage(ToolbarButtons.NextPage);
                    gridview.FocusedRowIndex = 0;
                    refresh = true;
                    break;

                case ToolbarButtons.PrevPage:
                    ChangePage(ToolbarButtons.PrevPage);
                    gridview.FocusedRowIndex = 0;
                    refresh = true;
                    break;

                case ToolbarButtons.LangPol:
                    Session["lang"] = Languages.Polish;
                    refresh = true;
                    break;

                case ToolbarButtons.LangEng:
                    Session["lang"] = Languages.English;
                    refresh = true;
                    break;

                case ToolbarButtons.AnswerOK:
                    ProcessAnswer(auditDetailsId, Answers.OK, allowedOK, allowedNOK, allowedNC, allowedNA);
                    refresh = true;
                    move = true;
                    break;

                case ToolbarButtons.AnswerNOK:
                    ProcessAnswer(auditDetailsId, Answers.NOK, allowedOK, allowedNOK, allowedNC, allowedNA);
                    refresh = true;
                    break;

                case ToolbarButtons.AnswerNC:
                    ProcessAnswer(auditDetailsId, Answers.NC, allowedOK, allowedNOK, allowedNC, allowedNA);
                    refresh = true;
                    break;

                case ToolbarButtons.AnswerNA:
                    ProcessAnswer(auditDetailsId, Answers.NA, allowedOK, allowedNOK, allowedNC, allowedNA);
                    refresh = true;
                    move = true;
                    break;

                case ToolbarButtons.Camera:
                    Session["audit_id"] = auditId;
                    Session["audit_detail_id"] = auditDetailsId;
                    gridview.JSProperties["cp_launch_camera"] = true;
                    break;

                case ToolbarButtons.DeleteAudit:
                    audit.Delete();
                    gridview.JSProperties["cp_escape_page"] = true;
                    Session["lang"] = null;
                    Session["page"] = null;
                    Session["audit_id"] = null;
                    Session["audit_detail_id"] = null;
                    Session["message"] = "Audit removed!";
                    break;

                case ToolbarButtons.PrintAudit:
                    audit.SendEmailWithPdf(activeLang);
                    break;

                case ToolbarButtons.EndAudit:
                    if (audit.AuditQuestionAnsweredCount != audit.AuditQuestionCount)
                    {
                        throw new Exception("Answer all questions!");
                    }
                    audit.End();
                    Session["lang"] = null;
                    Session["page"] = null;
                    Session["audit_id"] = null;
                    Session["audit_detail_id"] = null;
                    Session["message"] = $"Audit completed with score {audit.AuditScoreCalculated.ToString("#.##")}%!";
                    gridview.JSProperties["cp_escape_page"] = true;
                    break;
            }
            if (refresh)
            {
                gridview.JSProperties["cp_refresh"] = true;
            }
            if (move)
            {
                gridview.JSProperties["cp_move"] = true;
            }
        }

        protected void gvAudit_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView gridview = sender as ASPxGridView;
            var newComment = Utils.ConvertToTrimmedString((gridview.FindEditFormTemplateControl("commentEditor") as ASPxMemo).Text);
            if (newComment != null && newComment.Length > 200)
            {
                e.Cancel = true;
                throw new Exception("Too long comment! Allowed 200 characters!");
            }
            e.NewValues["comment"] = newComment;
        }

        protected void ProcessAnswer(int? auditDetailsId, string answer, bool? allowedOK, bool? allowedNOK, bool? allowedNC, bool? allowedNA)
        {
            bool answerPossible = (answer == Answers.OK && allowedOK == true)
                                   || (answer == Answers.NOK && allowedNOK == true)
                                   || (answer == Answers.NC && allowedNC == true)
                                   || (answer == Answers.NA && allowedNA == true);
            if (answerPossible)
            {
                Audit.AnswerQuestion((int)auditDetailsId, answer);
            }
            else
            {
                var message = "This question can not be answered like that! Allowed:";
                if (allowedOK == true) message += $" {Answers.OK}";
                if (allowedNOK == true) message += $" {Answers.NOK}";
                if (allowedNC == true) message += $" {Answers.NC}";
                if (allowedNA == true) message += $" {Answers.NA}";
                message += "!";
                throw new Exception(message);
            }
        }

        protected void ChangePage(string type)
        {
            string activeLang = (Session["lang"] == null) ? Languages.Default : Utils.ConvertToTrimmedString(Session["lang"]);
            int activePage = (Session["page"] == null) ? 0 : int.Parse(Session["page"].ToString());
            var activeAudit = Audit.GetUserActiveAudit(new ActiveUser().UserName);
            var groups = activeAudit.AuditQuestionGroups;
            int groups_count = groups.Rows.Count;
            if (groups != null)
            {
                switch (type)
                {
                    case ToolbarButtons.NextPage:
                        activePage++;
                        if (activePage >= groups_count) activePage = 0;
                        break;

                    case ToolbarButtons.PrevPage:
                        activePage--;
                        if (activePage < 0) activePage = groups_count - 1;
                        break;
                }
                Session["page"] = activePage;
            }
        }

        protected void SetupGridview(Audit activeAudit)
        {
            sdsAudit.SelectParameters["audit_id"].DefaultValue = Utils.ConvertToTrimmedString(activeAudit.Id);
            string activeLang = (Session["lang"] == null) ? Languages.Default : Utils.ConvertToTrimmedString(Session["lang"]);
            int activePage = (Session["page"] == null) ? 0 : int.Parse(Session["page"].ToString());
            var groups = activeAudit.AuditQuestionGroups;
            string groupPosition = groups.Rows[activePage]["group_position"].ToString();
            gvAudit.AutoFilterByColumn(gvAudit.Columns["group_position"], groupPosition);
            string groupName = "";
            switch (activeLang)
            {
                case Languages.English:
                    gvAudit.Columns["question"].Visible = false;
                    gvAudit.Columns["question_ENG"].Visible = true;
                    groupName = groups.Rows[activePage]["group_name_ENG"].ToString();
                    break;

                case Languages.Polish:
                    gvAudit.Columns["question"].Visible = true;
                    gvAudit.Columns["question_ENG"].Visible = false;
                    groupName = groups.Rows[activePage]["group_name"].ToString();
                    break;
            }
            gvAudit.Caption = AuditTypes.GetAuditGridCaption(groupPosition, groupName);
            gvAudit.SettingsText.Title = AuditTypes.GetAuditGridTitle(activeAudit);
        }

        protected void gvAudit_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView gridview = sender as ASPxGridView;
            string activeLang = (Session["lang"] == null) ? Languages.Default : Utils.ConvertToTrimmedString(Session["lang"]);
            var auditId = Utils.ConvertToNullableInt(gridview.GetRowValues(gridview.FocusedRowIndex, "audit_id"));
            var auditDetailsId = Utils.ConvertToNullableInt(gridview.GetRowValues(gridview.FocusedRowIndex, "id"));
            var audit = new Audit((int)auditId);
            bool refresh = false;
            if (!audit.AuditExist() || audit.EndDate != null)
            {
                throw new Exception("Audit does not exist / has already been completed!");
            }
            switch (e.Parameters)
            {
                case ToolbarButtons.NextPage:
                    ChangePage(ToolbarButtons.NextPage);
                    refresh = true;
                    break;
            }
            if (refresh)
            {
                gridview.JSProperties["cp_refresh"] = true;
            }
        }
    }
}