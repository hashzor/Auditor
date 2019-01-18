<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Camera.aspx.cs" Inherits="Auditor.Camera" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CAMERA</title>
    <link href="~/Styles/Loader.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .dxfm-uploadPanelTableBCell a {
            display: none;
        }
    </style>
</head>
<body onload="BodyLoad()" onunload="BodyUnload()">
    <div id="loaderBox">
        <div class="loaderCircle"></div>
    </div>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jquery-3.3.1.min.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/CustomUtilities.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/WebCam/webcam.js") %>"></script>
    <script type="text/javascript">
        var _pageUrl = '<%=Page.ResolveClientUrl(Auditor.Pages.Camera) %>';
        var _returnUrl = '<%=Page.ResolveClientUrl(Auditor.Pages.Default) %>';

        function btnConnect_Click(s, e) {
            Webcam.set({
                width: 900,
                height: 508,
                dest_width: 1280,
                dest_height: 720,
                image_format: "png"
            });
            Webcam.attach("camera");
            btnConnect.SetVisible(false);
            btnDisconnect.SetVisible(true);
            btnTakeSnapshot.SetVisible(true);
        }
        function btnDisconnect_Click(s, e) {
            Webcam.reset();
            btnConnect.SetVisible(true);
            btnDisconnect.SetVisible(false);
            btnTakeSnapshot.SetVisible(false);
            btnTakeAnother.SetVisible(false);
            btnSave.SetVisible(false);
        }
        function btnBack_Click(s, e) {
            Webcam.reset();
            window.location.replace(_returnUrl);
        }
        function btnTakeSnapshot_Click(s, e) {
            Webcam.freeze();
            btnTakeSnapshot.SetVisible(false);
            btnTakeAnother.SetVisible(true);
            btnSave.SetVisible(true);
        }
        function btnTakeAnother_Click(s, e) {
            Webcam.unfreeze();
            btnTakeSnapshot.SetVisible(true);
            btnTakeAnother.SetVisible(false);
            btnSave.SetVisible(false);
        }
        function btnSave_Click(s, e) {
            var data;
            Webcam.snap(function (data_uri) {
                data = data_uri;
                btnTakeSnapshot.SetVisible(true);
                btnTakeAnother.SetVisible(false);
                btnSave.SetVisible(false);
            });
            Webcam.upload(data, _pageUrl, function (code, text) {
                dxFileManager.Refresh();
            });
        }
        function btnRefresh_Click(s, e) {
            dxFileManager.Refresh();
        }

        function dxFileManager_SelectedFileOpened(s, e) {
            if (e.file) {
                var url = s.cp_root + e.file.name;
                picCallback.PerformCallback(url);
            }
        }
        function picCallback_CallbackComplete(s, e) {
            var data = e.result.split("|");
            if (data.length >= 3) {
                var width = data[0];
                var height = data[1];
                var url = data[2];
                var position_hor = ((getWidth() - width) / 2) - 10;
                var position_vert = 20;
                ShowPopup(url, width, height, position_hor, position_vert);
            }
            else {
                alert("Display error!");
            }
        }
        function ShowPopup(url, width, height, position_hor, position_vert) {
            if (picPopup.GetClientVisible()) picPopup.Hide();
            picPopup.ShowAtPos(position_hor, position_vert);
            picPopup.SetHeight(height);
            picPopup.SetWidth(width);
            imgPic.SetSize(width, height);
            imgPic.SetImageUrl(url);
        }
	</script>
    <form runat="server">
        <table style="margin: 0 auto; margin-top: 30px;">
            <tr style="vertical-align: top;">
                <td colspan="2">
                    <div id="camera" style="background-color: black; width: 900px; height: 508px;">
                    </div>
                </td>
                <td>
                    <dx:ASPxFileManager ID="dxFileManager" ClientInstanceName="dxFileManager" runat="server" Style="margin: 0 auto;" Width="330px" Height="508px" Font-Size="16px">
                        <ClientSideEvents SelectedFileOpened="dxFileManager_SelectedFileOpened" />
                        <SettingsToolbar>
                            <Items>
                                <dx:FileManagerToolbarDeleteButton />
                            </Items>
                        </SettingsToolbar>
                    </dx:ASPxFileManager>
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td style="text-align: left;">
                    <dx:ASPxButton ID="btnTakeSnapshot" ClientInstanceName="btnTakeSnapshot" runat="server" Text="" ClientVisible="false" AutoPostBack="false" Width="150" Height="60">
                        <ClientSideEvents Click="btnTakeSnapshot_Click" />
                        <Image Url="Images/camera.png" Width="40" Height="40" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnSave" ClientInstanceName="btnSave" runat="server" Text="" ClientVisible="false" AutoPostBack="false" Width="150" Height="60">
                        <ClientSideEvents Click="btnSave_Click" />
                        <Image Url="Images/save.png" Width="40" Height="40" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnTakeAnother" ClientInstanceName="btnTakeAnother" runat="server" Text="" ClientVisible="false" AutoPostBack="false" Width="150" Height="60">
                        <ClientSideEvents Click="btnTakeAnother_Click" />
                        <Image Url="Images/back.png" Width="40" Height="40" />
                    </dx:ASPxButton>
                </td>
                <td style="text-align: right;">
                    <dx:ASPxButton ID="btnConnect" ClientInstanceName="btnConnect" runat="server" Text="" AutoPostBack="false" Width="150" Height="60">
                        <ClientSideEvents Click="btnConnect_Click" />
                        <Image Url="Images/start.png" Width="40" Height="40" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnDisconnect" ClientInstanceName="btnDisconnect" runat="server" Text="" ClientVisible="false" AutoPostBack="false" Width="150" Height="60">
                        <ClientSideEvents Click="btnDisconnect_Click" />
                        <Image Url="Images/stop.png" Width="40" Height="40" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnBack" ClientInstanceName="btnBack" runat="server" Text="" AutoPostBack="false" Width="150" Height="60">
                        <ClientSideEvents Click="btnBack_Click" />
                        <Image Url="Images/exit.png" Width="40" Height="40" />
                    </dx:ASPxButton>
                </td>
                <td style="text-align: center;">
                    <dx:ASPxButton ID="btnRefresh" ClientInstanceName="btnRefresh" runat="server" Text="" AutoPostBack="false" Width="150" Height="60">
                        <ClientSideEvents Click="btnRefresh_Click" />
                        <Image Url="Images/refresh.png" Width="40" Height="40" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
        <dx:ASPxCallback ID="picCallback" ClientInstanceName="picCallback" runat="server" OnCallback="picCallback_Callback">
            <ClientSideEvents CallbackComplete="picCallback_CallbackComplete" />
        </dx:ASPxCallback>
        <dx:ASPxPopupControl ID="picPopup" runat="server" ClientInstanceName="picPopup" CloseAction="OuterMouseClick" ShowHeader="false" Modal="true">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                    <div style="margin: 0 auto;">
                        <dx:ASPxImage runat="server" ID="imgPic" ClientInstanceName="imgPic">
                        </dx:ASPxImage>
                    </div>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </form>
</body>
</html>