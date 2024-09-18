using System.Diagnostics;
using HASS.Agent.Functions;
using HASS.Agent.Models.Internal;

namespace HASS.Agent.Controls.Configuration
{
    public partial class ConfigTrayIcon : UserControl
    {
        public ConfigTrayIcon()
        {
            InitializeComponent();
        }

        private void ConfigTrayIcon_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TbWebViewUrl.Text)) TbWebViewUrl.Text = Variables.AppSettings.HassUri;
            var displays = Screen.AllScreens;
            int ix = 0;
            int primaryIx = -1;

            // Add screens to updownControl
            foreach (var display in displays)
            {
                string label = display.DeviceName;
                if (display.Primary)
                {
                    label += " (Primary)";
                    primaryIx = ix;
                }
                domainUpDown1.Items.Add(label);
                ix++;
            }

            // set preselected to first entry or primary screen entry
            if (primaryIx < 0)
            {
                domainUpDown1.SelectedIndex = 0;
            } else
            {
                domainUpDown1.SelectedIndex = primaryIx;
            }
        }

        private void CbDefaultMenu_CheckedChanged(object sender, EventArgs e)
        {
            CbShowWebView.Checked = !CbDefaultMenu.Checked;
        }

        private void CbShowWebView_CheckedChanged(object sender, EventArgs e)
        {
            CbDefaultMenu.Checked = !CbShowWebView.Checked;

            TbWebViewUrl.Enabled = CbShowWebView.Checked;
            NumWebViewWidth.Enabled = CbShowWebView.Checked;
            NumWebViewHeight.Enabled = CbShowWebView.Checked;
            BtnShowWebViewPreview.Enabled = CbShowWebView.Checked;
            BtnWebViewReset.Enabled = CbShowWebView.Checked;
            CbWebViewKeepLoaded.Enabled = CbShowWebView.Checked;
            CbWebViewShowMenuOnLeftClick.Enabled = CbShowWebView.Checked;
            LblInfo2.Enabled = CbShowWebView.Checked;
        }

        private void BtnShowWebViewPreview_Click(object sender, EventArgs e)
        {
            var webView = new WebViewInfo
            {
                Url = TbWebViewUrl.Text,
                Height = (int)NumWebViewHeight.Value,
                Width = (int)NumWebViewWidth.Value,
                IsTrayIconWebView = true,
                IsTrayIconPreview = true
            };

            HelperFunctions.LaunchTrayIconWebView(webView, domainUpDown1.SelectedIndex);
        }

        private void BtnWebViewReset_Click(object sender, EventArgs e)
        {
            NumWebViewWidth.Value = 700;
            NumWebViewHeight.Value = 560;
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }
    }
}
