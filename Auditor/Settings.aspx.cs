using DevExpress.Web;
using System;

namespace Auditor
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(ActiveUser.IsInRole(AppRoles.AuditorAdmin) || ActiveUser.IsInRole(AppRoles.UserAdmin)))
            {
                Response.Redirect(Pages.AccessDenied);
            }
        }

        protected void gvAuditQuestionGroups_Init(object sender, EventArgs e)
        {
            GridViewUtils.GridViewDefaultInit(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.SettingsEditing.Mode = GridViewEditingMode.Batch;
            gridview.SettingsDataSecurity.AllowInsert = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsDataSecurity.AllowEdit = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsDataSecurity.AllowDelete = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsText.Title = "QUESTION GROUPS";
            gridview.SettingsExport.FileName = $"QuestionGroups_{DateTime.Now.ToString("yyyyMMdd")}";
            GridViewUtils.GridViewToolbarInit(sender, e);
        }

        protected void gvAuditQuestionGroups_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            var newAuditType = Utils.ConvertToTrimmedString(e.NewValues["audit_type"]);
            var newGroupPosition = Utils.ConvertToNullableInt(e.NewValues["group_position"]);
            var newGroupName = Utils.ConvertToTrimmedString(e.NewValues["group_name"]);
            var newGroupNameEng = Utils.ConvertToTrimmedString(e.NewValues["group_name_ENG"]);

            if (newAuditType == null
                || newGroupPosition == null
                || newGroupName == null
                || newGroupNameEng == null)
            {
                e.Cancel = true;
                throw new Exception("Complete all fields!");
            }
            if (newGroupPosition.Value < 0)
            {
                e.Cancel = true;
                throw new Exception("NO must be a positive number!");
            }

            e.NewValues["audit_type"] = newAuditType;
            e.NewValues["group_position"] = newGroupPosition;
            e.NewValues["group_name"] = newGroupName;
            e.NewValues["group_name_ENG"] = newGroupNameEng;
        }

        protected void gvAuditQuestionGroups_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var newAuditType = Utils.ConvertToTrimmedString(e.NewValues["audit_type"]);
            var newGroupPosition = Utils.ConvertToNullableInt(e.NewValues["group_position"]);
            var newGroupName = Utils.ConvertToTrimmedString(e.NewValues["group_name"]);
            var newGroupNameEng = Utils.ConvertToTrimmedString(e.NewValues["group_name_ENG"]);

            if (newAuditType == null
                || newGroupPosition == null
                || newGroupName == null
                || newGroupNameEng == null)
            {
                e.Cancel = true;
                throw new Exception("Complete all fields!");
            }
            if (newGroupPosition.Value < 0)
            {
                e.Cancel = true;
                throw new Exception("NO must be a positive number!");
            }

            e.NewValues["audit_type"] = newAuditType;
            e.NewValues["group_position"] = newGroupPosition;
            e.NewValues["group_name"] = newGroupName;
            e.NewValues["group_name_ENG"] = newGroupNameEng;
        }

        protected void gvAuditQuestion_Init(object sender, EventArgs e)
        {
            GridViewUtils.GridViewDefaultInit(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.SettingsEditing.Mode = GridViewEditingMode.Batch;
            gridview.SettingsDataSecurity.AllowInsert = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsDataSecurity.AllowEdit = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsDataSecurity.AllowDelete = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsText.Title = "QUESTIONS";
            gridview.SettingsExport.FileName = $"Questions_{DateTime.Now.ToString("yyyyMMdd")}";
            GridViewUtils.GridViewToolbarInit(sender, e);
        }

        protected void gvAuditQuestion_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            var newGroupId = Utils.ConvertToNullableInt(e.NewValues["group_id"]);
            var newQuestionPosition = Utils.ConvertToNullableInt(e.NewValues["question_position"]);
            var newQuestion = Utils.ConvertToTrimmedString(e.NewValues["question"]);
            var newQuestionEng = Utils.ConvertToTrimmedString(e.NewValues["question_ENG"]);
            var newAnswerOK = Utils.ConvertToNullableBool(e.NewValues["answer_OK"]);
            var newAnswerNOK = Utils.ConvertToNullableBool(e.NewValues["answer_NOK"]);
            var newAnswerNA = Utils.ConvertToNullableBool(e.NewValues["answer_NA"]);
            var newAnswerNC = Utils.ConvertToNullableBool(e.NewValues["answer_NC"]);

            newAnswerOK = newAnswerOK == null ? false : newAnswerOK.Value;
            newAnswerNOK = newAnswerNOK == null ? false : newAnswerNOK.Value;
            newAnswerNA = newAnswerNA == null ? false : newAnswerNA.Value;
            newAnswerNC = newAnswerNC == null ? false : newAnswerNC.Value;

            if (newGroupId == null
                || newQuestionPosition == null
                || newQuestion == null
                || newQuestionEng == null)
            {
                e.Cancel = true;
                throw new Exception("Complete all fields!");
            }
            if (newQuestionPosition.Value < 0)
            {
                e.Cancel = true;
                throw new Exception("NO must be a positive number!");
            }

            e.NewValues["group_id"] = newGroupId;
            e.NewValues["question_position"] = newQuestionPosition;
            e.NewValues["question"] = newQuestion;
            e.NewValues["question_ENG"] = newQuestionEng;
            e.NewValues["answer_OK"] = newAnswerOK;
            e.NewValues["answer_NOK"] = newAnswerNOK;
            e.NewValues["answer_NA"] = newAnswerNA;
            e.NewValues["answer_NC"] = newAnswerNC;
        }

        protected void gvAuditQuestion_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var newGroupId = Utils.ConvertToNullableInt(e.NewValues["group_id"]);
            var newQuestionPosition = Utils.ConvertToNullableInt(e.NewValues["question_position"]);
            var newQuestion = Utils.ConvertToTrimmedString(e.NewValues["question"]);
            var newQuestionEng = Utils.ConvertToTrimmedString(e.NewValues["question_ENG"]);
            var newAnswerOK = Utils.ConvertToNullableBool(e.NewValues["answer_OK"]);
            var newAnswerNOK = Utils.ConvertToNullableBool(e.NewValues["answer_NOK"]);
            var newAnswerNA = Utils.ConvertToNullableBool(e.NewValues["answer_NA"]);
            var newAnswerNC = Utils.ConvertToNullableBool(e.NewValues["answer_NC"]);

            newAnswerOK = newAnswerOK == null ? false : newAnswerOK.Value;
            newAnswerNOK = newAnswerNOK == null ? false : newAnswerNOK.Value;
            newAnswerNA = newAnswerNA == null ? false : newAnswerNA.Value;
            newAnswerNC = newAnswerNC == null ? false : newAnswerNC.Value;

            if (newGroupId == null
                || newQuestionPosition == null
                || newQuestion == null
                || newQuestionEng == null)
            {
                e.Cancel = true;
                throw new Exception("Complete all fields!");
            }
            if (newQuestionPosition.Value < 0)
            {
                e.Cancel = true;
                throw new Exception("NO must be a positive number!");
            }

            e.NewValues["group_id"] = newGroupId;
            e.NewValues["question_position"] = newQuestionPosition;
            e.NewValues["question"] = newQuestion;
            e.NewValues["question_ENG"] = newQuestionEng;
            e.NewValues["answer_OK"] = newAnswerOK;
            e.NewValues["answer_NOK"] = newAnswerNOK;
            e.NewValues["answer_NA"] = newAnswerNA;
            e.NewValues["answer_NC"] = newAnswerNC;
        }

        protected void gvAuditors_Init(object sender, EventArgs e)
        {
            GridViewUtils.GridViewDefaultInit(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.SettingsEditing.Mode = GridViewEditingMode.Batch;
            gridview.SettingsDataSecurity.AllowInsert = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsDataSecurity.AllowEdit = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsDataSecurity.AllowDelete = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsText.Title = "AUDITORS";
            gridview.SettingsExport.FileName = $"Auditors_{DateTime.Now.ToString("yyyyMMdd")}";
            GridViewUtils.GridViewToolbarInit(sender, e);
        }

        protected void gvAuditors_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            var newAuditType = Utils.ConvertToTrimmedString(e.NewValues["audit_type"]);
            var newAuditorLogin = Utils.ConvertToTrimmedString(e.NewValues["auditor_login"]);

            if (newAuditType == null
                || newAuditorLogin == null)
            {
                e.Cancel = true;
                throw new Exception("Complete all fields!");
            }

            string message = SettsUtils.AuditorInsertVerify(newAuditType, newAuditorLogin);
            if (message != null)
            {
                e.Cancel = true;
                throw new Exception(message);
            }

            e.NewValues["audit_type"] = newAuditType;
            e.NewValues["auditor_login"] = newAuditorLogin.ToUpper();
        }

        protected void gvAuditors_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var newAuditType = Utils.ConvertToTrimmedString(e.NewValues["audit_type"]);
            var newAuditorLogin = Utils.ConvertToTrimmedString(e.NewValues["auditor_login"]);

            if (newAuditType == null
                || newAuditorLogin == null)
            {
                e.Cancel = true;
                throw new Exception("Complete all fields!");
            }

            var id = Utils.ConvertToTrimmedString(e.Keys["id"]);
            if (id == null)
            {
                e.Cancel = true;
                throw new Exception("System error!");
            }
            string message = SettsUtils.AuditorUpdateVerify(newAuditType, newAuditorLogin, id);
            if (message != null)
            {
                e.Cancel = true;
                throw new Exception(message);
            }

            e.NewValues["audit_type"] = newAuditType;
            e.NewValues["auditor_login"] = newAuditorLogin.ToUpper();
        }

        protected void gvSchedules_Init(object sender, EventArgs e)
        {
            GridViewUtils.GridViewDefaultInit(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.SettingsEditing.Mode = GridViewEditingMode.Batch;
            gridview.SettingsDataSecurity.AllowInsert = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsDataSecurity.AllowEdit = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsDataSecurity.AllowDelete = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsText.Title = "SCHEDULES";
            gridview.SettingsExport.FileName = $"Schedules_{DateTime.Now.ToString("yyyyMMdd")}";
            GridViewUtils.GridViewToolbarInit(sender, e);
        }

        protected void gvSchedules_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "schedule_name")
            {
                ASPxComboBox combobox = e.Editor as ASPxComboBox;
                combobox.DataSource = Schedules.SchedulesList;
                combobox.DataBind();
            }
        }

        protected void gvSchedules_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            var newScheduleName = Utils.ConvertToTrimmedString(e.NewValues["schedule_name"]);
            var newScheduleDescription = Utils.ConvertToTrimmedString(e.NewValues["schedule_description"]);

            if (newScheduleName == null
                || newScheduleDescription == null)
            {
                e.Cancel = true;
                throw new Exception("Complete all fields!");
            }

            string message = SettsUtils.ScheduleInsertVerify(newScheduleName);
            if (message != null)
            {
                e.Cancel = true;
                throw new Exception(message);
            }

            e.NewValues["schedule_name"] = newScheduleName;
            e.NewValues["schedule_description"] = newScheduleDescription;
        }

        protected void gvSchedules_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var newScheduleName = Utils.ConvertToTrimmedString(e.NewValues["schedule_name"]);
            var newScheduleDescription = Utils.ConvertToTrimmedString(e.NewValues["schedule_description"]);

            if (newScheduleName == null
                || newScheduleDescription == null)
            {
                e.Cancel = true;
                throw new Exception("Complete all fields!");
            }

            var id = Utils.ConvertToTrimmedString(e.Keys["id"]);
            if (id == null)
            {
                e.Cancel = true;
                throw new Exception("System error!");
            }
            string message = SettsUtils.ScheduleUpdateVerify(newScheduleName, id);
            if (message != null)
            {
                e.Cancel = true;
                throw new Exception(message);
            }

            e.NewValues["schedule_name"] = newScheduleName;
            e.NewValues["schedule_description"] = newScheduleDescription;
        }

        protected void gvAuditTypes_Init(object sender, EventArgs e)
        {
            GridViewUtils.GridViewDefaultInit(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.SettingsEditing.Mode = GridViewEditingMode.Batch;
            gridview.SettingsDataSecurity.AllowInsert = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsDataSecurity.AllowEdit = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsDataSecurity.AllowDelete = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsText.Title = "AUDITS";
            gridview.SettingsExport.FileName = $"Audits_{DateTime.Now.ToString("yyyyMMdd")}";
            GridViewUtils.GridViewToolbarInit(sender, e);
        }

        protected void gvAuditTypes_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "audit_type")
            {
                ASPxComboBox combobox = e.Editor as ASPxComboBox;
                combobox.DataSource = AuditTypes.AuditTypesList;
                combobox.DataBind();
            }
        }

        protected void gvAuditTypes_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            var newAuditType = Utils.ConvertToTrimmedString(e.NewValues["audit_type"]);
            var newName = Utils.ConvertToTrimmedString(e.NewValues["name"]);

            if (newAuditType == null
                || newName == null)
            {
                e.Cancel = true;
                throw new Exception("Complete all fields!");
            }

            string message = SettsUtils.AuditTypeInsertVerify(newAuditType);
            if (message != null)
            {
                e.Cancel = true;
                throw new Exception(message);
            }

            e.NewValues["audit_type"] = newAuditType;
            e.NewValues["name"] = newName;
        }

        protected void gvAuditTypes_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var newAuditType = Utils.ConvertToTrimmedString(e.NewValues["audit_type"]);
            var newName = Utils.ConvertToTrimmedString(e.NewValues["name"]);

            if (newAuditType == null
                || newName == null)
            {
                e.Cancel = true;
                throw new Exception("Complete all fields!");
            }

            var id = Utils.ConvertToTrimmedString(e.Keys["id"]);
            if (id == null)
            {
                e.Cancel = true;
                throw new Exception("System error!");
            }
            string message = SettsUtils.AuditTypeUpdateVerify(newAuditType, id);
            if (message != null)
            {
                e.Cancel = true;
                throw new Exception(message);
            }

            e.NewValues["audit_type"] = newAuditType;
            e.NewValues["name"] = newName;
        }

        protected void gvAuditTargets_Init(object sender, EventArgs e)
        {
            GridViewUtils.GridViewDefaultInit(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.SettingsEditing.Mode = GridViewEditingMode.Batch;
            gridview.SettingsDataSecurity.AllowInsert = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsDataSecurity.AllowEdit = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsDataSecurity.AllowDelete = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsText.Title = "TARGETS";
            gridview.SettingsExport.FileName = $"Targets_{DateTime.Now.ToString("yyyyMMdd")}";
            GridViewUtils.GridViewToolbarInit(sender, e);
        }

        protected void gvAuditTargets_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            var newAuditType = Utils.ConvertToTrimmedString(e.NewValues["audit_type"]);
            var newAuditTarget = Utils.ConvertToTrimmedString(e.NewValues["audit_target"]);
            var newArea = Utils.ConvertToTrimmedString(e.NewValues["area"]);
            var newSubarea = Utils.ConvertToTrimmedString(e.NewValues["subarea"]);
            var newSection = Utils.ConvertToTrimmedString(e.NewValues["section"]);
            var newSupervisorLogin = Utils.ConvertToTrimmedString(e.NewValues["supervisor_login"]);
            string newSupervisor = null;

            if (newAuditType == null
                || newAuditTarget == null)
            {
                e.Cancel = true;
                throw new Exception("Complete TYPE and TARGET fields!");
            }

            string message = SettsUtils.AuditTargetInsertVerify(newAuditType, newAuditTarget);
            if (message != null)
            {
                e.Cancel = true;
                throw new Exception(message);
            }

            message = AuditTypes.VerifyAuditTargetEntry(newAuditType, newArea, newSubarea, newSection, newSupervisorLogin);
            if (message != null)
            {
                e.Cancel = true;
                throw new Exception(message);
            }

            if (newSupervisorLogin != null)
            {
                var user = new AppUser(newSupervisorLogin);
                if (user.Exist)
                {
                    newSupervisor = user.FullName;
                }
                else
                {
                    e.Cancel = true;
                    throw new Exception("User does not exist (SUPERVISOR)!");
                }
            }

            e.NewValues["audit_type"] = newAuditType;
            e.NewValues["audit_target"] = newAuditTarget;
            e.NewValues["area"] = newAuditType;
            e.NewValues["subarea"] = newSubarea;
            e.NewValues["section"] = newSection;
            e.NewValues["supervisor_login"] = newSupervisorLogin;
            e.NewValues["supervisor"] = newSupervisor;
        }

        protected void gvAuditTargets_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var newAuditType = Utils.ConvertToTrimmedString(e.NewValues["audit_type"]);
            var newAuditTarget = Utils.ConvertToTrimmedString(e.NewValues["audit_target"]);
            var newArea = Utils.ConvertToTrimmedString(e.NewValues["area"]);
            var newSubarea = Utils.ConvertToTrimmedString(e.NewValues["subarea"]);
            var newSection = Utils.ConvertToTrimmedString(e.NewValues["section"]);
            var newSupervisorLogin = Utils.ConvertToTrimmedString(e.NewValues["supervisor_login"]);
            string newSupervisor = null;

            if (newAuditType == null
                || newAuditTarget == null)
            {
                e.Cancel = true;
                throw new Exception("Complete TYPE and TARGET fields!");
            }

            var id = Utils.ConvertToTrimmedString(e.Keys["id"]);
            if (id == null)
            {
                e.Cancel = true;
                throw new Exception("System error!");
            }
            string message = SettsUtils.AuditTargetUpdateVerify(newAuditType, newAuditTarget, id);
            if (message != null)
            {
                e.Cancel = true;
                throw new Exception(message);
            }

            message = AuditTypes.VerifyAuditTargetEntry(newAuditType, newArea, newSubarea, newSection, newSupervisorLogin);
            if (message != null)
            {
                e.Cancel = true;
                throw new Exception(message);
            }

            if (newSupervisorLogin != null)
            {
                var user = new AppUser(newSupervisorLogin);
                if (user.Exist)
                {
                    newSupervisor = user.FullName;
                }
                else
                {
                    e.Cancel = true;
                    throw new Exception("User does not exist (SUPERVISOR)!");
                }
            }

            e.NewValues["audit_type"] = newAuditType;
            e.NewValues["audit_target"] = newAuditTarget;
            e.NewValues["area"] = newAuditType;
            e.NewValues["subarea"] = newSubarea;
            e.NewValues["section"] = newSection;
            e.NewValues["supervisor_login"] = newSupervisorLogin;
            e.NewValues["supervisor"] = newSupervisor;
        }

        protected void gvShifts_Init(object sender, EventArgs e)
        {
            GridViewUtils.GridViewDefaultInit(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.SettingsEditing.Mode = GridViewEditingMode.Batch;
            gridview.SettingsDataSecurity.AllowInsert = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsDataSecurity.AllowEdit = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsDataSecurity.AllowDelete = ActiveUser.IsInRole(AppRoles.AuditorAdmin);
            gridview.SettingsText.Title = "SHIFTS";
            gridview.SettingsExport.FileName = $"Shifts_{DateTime.Now.ToString("yyyyMMdd")}";
            GridViewUtils.GridViewToolbarInit(sender, e);
        }

        protected void gvShifts_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "shift_name")
            {
                ASPxComboBox combobox = e.Editor as ASPxComboBox;
                combobox.DataSource = Shifts.ShiftsList;
                combobox.DataBind();
            }
        }

        protected void gvShifts_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            var newShiftName = Utils.ConvertToTrimmedString(e.NewValues["shift_name"]);

            if (newShiftName == null)
            {
                e.Cancel = true;
                throw new Exception("Complete all fields!");
            }

            string message = SettsUtils.ShiftInsertVerify(newShiftName);
            if (message != null)
            {
                e.Cancel = true;
                throw new Exception(message);
            }

            e.NewValues["shift_name"] = newShiftName;
        }

        protected void gvShifts_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var newShiftName = Utils.ConvertToTrimmedString(e.NewValues["shift_name"]);

            if (newShiftName == null)
            {
                e.Cancel = true;
                throw new Exception("Complete all fields!");
            }

            var id = Utils.ConvertToTrimmedString(e.Keys["id"]);
            if (id == null)
            {
                e.Cancel = true;
                throw new Exception("System error!");
            }
            string message = SettsUtils.ShiftUpdateVerify(newShiftName, id);
            if (message != null)
            {
                e.Cancel = true;
                throw new Exception(message);
            }

            e.NewValues["shift_name"] = newShiftName;
        }

        protected void gvUsers_Init(object sender, EventArgs e)
        {
            GridViewUtils.GridViewDefaultInit(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.SettingsDataSecurity.AllowInsert = ActiveUser.IsInRole(AppRoles.UserAdmin);
            gridview.SettingsDataSecurity.AllowEdit = ActiveUser.IsInRole(AppRoles.UserAdmin);
            gridview.SettingsDataSecurity.AllowDelete = ActiveUser.IsInRole(AppRoles.UserAdmin);
            gridview.SettingsEditing.EditFormColumnCount = 3;
            gridview.SettingsText.Title = "USERS";
            gridview.SettingsExport.FileName = $"Users_{DateTime.Now.ToString("yyyyMMdd")}";
            GridViewUtils.GridViewToolbarInit(sender, e);

            var unlock = new GridViewToolbarItem();
            unlock.Command = GridViewToolbarCommand.Custom;
            unlock.Name = ToolbarButtons.Unlock;
            unlock.Text = "Unlock User";
            unlock.Image.Url = "Images/unlock.png";
            unlock.Image.AlternateText = unlock.Text;
            unlock.Image.ToolTip = unlock.Text;
            unlock.Image.Height = GridViewUtils.ImageToolbarSize;
            unlock.Image.Width = GridViewUtils.ImageToolbarSize;

            var reset = new GridViewToolbarItem();
            reset.Command = GridViewToolbarCommand.Custom;
            reset.Name = ToolbarButtons.Reset;
            reset.Text = "Reset Password";
            reset.Image.Url = "Images/key.png";
            reset.Image.AlternateText = unlock.Text;
            reset.Image.ToolTip = unlock.Text;
            reset.Image.Height = GridViewUtils.ImageToolbarSize;
            reset.Image.Width = GridViewUtils.ImageToolbarSize;

            var toolbarGrid = gridview.Toolbars.FindByName(GridViewUtils.ToolbarGrid);
            if (toolbarGrid != null && ActiveUser.IsInRole(AppRoles.UserAdmin))
            {
                toolbarGrid.Items.Add(unlock);
                toolbarGrid.Items.Add(reset);
            }
        }

        protected void gvUsers_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            var newUsername = Utils.ConvertToTrimmedString(e.NewValues["username"]);
            var newName = Utils.ConvertToTrimmedString(e.NewValues["name"]);
            var newLastName = Utils.ConvertToTrimmedString(e.NewValues["lastname"]);
            var newEmail = Utils.ConvertToTrimmedString(e.NewValues["email"]);

            if (newUsername == null
                || newName == null
                || newLastName == null
                || newEmail == null)
            {
                throw new Exception("Complete all fields!");
            }

            newUsername = newUsername.ToLower();
            newEmail = newEmail.ToLower();

            if (!MailUtils.EmailAddressValid(newEmail))
            {
                throw new Exception("Email is not valid!");
            }

            UserManagement.UserRegister(newUsername, newName, newLastName, newEmail);
        }

        protected void gvUsers_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var newUsername = Utils.ConvertToTrimmedString(e.NewValues["username"]);
            var oldUsername = Utils.ConvertToTrimmedString(e.OldValues["username"]);
            var newName = Utils.ConvertToTrimmedString(e.NewValues["name"]);
            var newLastName = Utils.ConvertToTrimmedString(e.NewValues["lastname"]);
            var newEmail = Utils.ConvertToTrimmedString(e.NewValues["email"]);

            if (oldUsername == null)
            {
                throw new Exception("System error!");
            }

            if (newUsername == null
                || newName == null
                || newLastName == null
                || newEmail == null)
            {
                throw new Exception("Complete all fields!");
            }

            oldUsername = oldUsername.ToLower();
            newUsername = newUsername.ToLower();
            newEmail = newEmail.ToLower();

            if (newUsername != oldUsername)
            {
                throw new Exception("Username cannot be changed!");
            }

            if (!MailUtils.EmailAddressValid(newEmail))
            {
                throw new Exception("Email is not valid!");
            }

            UserManagement.UserUpdate(newUsername, newName, newLastName, newEmail);
        }

        protected void gvUsers_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var username = Utils.ConvertToTrimmedString(e.Values["username"]);

            username = username.ToLower();

            UserManagement.UserDelete(username);
        }

        protected void gvUsers_ToolbarItemClick(object source, DevExpress.Web.Data.ASPxGridViewToolbarItemClickEventArgs e)
        {
            ASPxGridView gridview = source as ASPxGridView;
            var username = Utils.ConvertToTrimmedString(gridview.GetRowValues(gridview.FocusedRowIndex, "username"));
            bool refresh = false;

            if (username == null)
            {
                throw new Exception("Select user!");
            }

            username = username.ToLower();

            switch (e.Item.Name)
            {
                case ToolbarButtons.Unlock:
                    UserManagement.UserUnlock(username);
                    gridview.JSProperties["cp_unlock"] = true;
                    refresh = true;
                    break;

                case ToolbarButtons.Reset:
                    UserManagement.UserResetPassword(username);
                    gridview.JSProperties["cp_reset"] = true;
                    refresh = true;
                    break;
            }
            if (refresh)
            {
                gridview.JSProperties["cp_refresh"] = true;
            }
        }

        protected void gvRoles_Init(object sender, EventArgs e)
        {
            GridViewUtils.GridViewDefaultInit(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.SettingsDataSecurity.AllowInsert = ActiveUser.IsInRole(AppRoles.UserAdmin);
            gridview.SettingsDataSecurity.AllowEdit = ActiveUser.IsInRole(AppRoles.UserAdmin);
            gridview.SettingsDataSecurity.AllowDelete = ActiveUser.IsInRole(AppRoles.UserAdmin);
            gridview.SettingsEditing.EditFormColumnCount = 2;
            gridview.SettingsDetail.AllowOnlyOneMasterRowExpanded = true;
            gridview.SettingsDetail.ShowDetailRow = true;
            gridview.SettingsText.Title = "ROLES";
            gridview.SettingsExport.FileName = $"Roles_{DateTime.Now.ToString("yyyyMMdd")}";
            GridViewUtils.GridViewToolbarInit(sender, e);
        }

        protected void gvRoles_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            var newRoleName = Utils.ConvertToTrimmedString(e.NewValues["rolename"]);
            var newDescription = Utils.ConvertToTrimmedString(e.NewValues["description"]);

            if (newRoleName == null
                || newDescription == null)
            {
                throw new Exception("Complete all fields!");
            }

            newRoleName = newRoleName.ToLower();

            RoleManagement.RoleRegister(newRoleName, newDescription);
        }

        protected void gvRoles_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var oldRoleName = Utils.ConvertToTrimmedString(e.OldValues["rolename"]);
            var newRoleName = Utils.ConvertToTrimmedString(e.NewValues["rolename"]);
            var newDescription = Utils.ConvertToTrimmedString(e.NewValues["description"]);

            if (newRoleName == null
                || newDescription == null)
            {
                throw new Exception("Complete all fields!");
            }

            if (newRoleName != oldRoleName)
            {
                throw new Exception("Role name cannot be changed!");
            }

            newRoleName = newRoleName.ToLower();

            RoleManagement.RoleUpdate(newRoleName, newDescription);
        }

        protected void gvRoles_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var roleName = Utils.ConvertToTrimmedString(e.Values["rolename"]);

            roleName = roleName.ToLower();

            RoleManagement.RoleDelete(roleName);
        }

        protected void gvUsersInRole_Init(object sender, EventArgs e)
        {
            GridViewUtils.GridViewDefaultInit(sender, e);
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.SettingsDataSecurity.AllowInsert = ActiveUser.IsInRole(AppRoles.UserAdmin);
            gridview.SettingsDataSecurity.AllowEdit = false;
            gridview.SettingsDataSecurity.AllowDelete = ActiveUser.IsInRole(AppRoles.UserAdmin);
            gridview.SettingsEditing.EditFormColumnCount = 2;
            gridview.SettingsText.Title = $"USERS IN ROLE ({gridview.GetMasterRowFieldValues("rolename").ToString()})";
            gridview.SettingsExport.FileName = $"UserInRole_{DateTime.Now.ToString("yyyyMMdd")}";
            GridViewUtils.GridViewToolbarInit(sender, e);
        }

        protected void gvUsersInRole_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView gridview = sender as ASPxGridView;
            var newRoleName = gridview.GetMasterRowFieldValues("rolename").ToString().ToLower();
            var newUsername = Utils.ConvertToTrimmedString(e.NewValues["username"]);

            if (newRoleName == null
                || newUsername == null)
            {
                throw new Exception("Complete all fields!");
            }

            newRoleName = newRoleName.ToLower();
            newUsername = newUsername.ToLower();

            UserManagement.UserAddRole(newUsername, newRoleName);
        }

        protected void gvUsersInRole_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var roleName = Utils.ConvertToTrimmedString(e.Values["rolename"]);
            var username = Utils.ConvertToTrimmedString(e.Values["username"]);

            roleName = roleName.ToLower();
            username = username.ToLower();

            UserManagement.UserRemoveRole(username, roleName);
        }

        protected void gvUsersInRole_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView gridview = sender as ASPxGridView;
            sdsUsersInRole.SelectParameters["rolename"].DefaultValue = gridview.GetMasterRowKeyValue().ToString();
        }
    }
}