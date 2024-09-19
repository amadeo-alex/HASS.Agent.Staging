using System.Diagnostics;
using HASS.Agent.Functions;
using HASS.Agent.Models.Internal;
using Microsoft.UI;

namespace HASS.Agent.Controls.Configuration
{
    public partial class ConfigTrayIcon : UserControl
    {
        private int _selectedScreen = 0;

        internal int SelectedScreen { get => _selectedScreen; set { _selectedScreen = value; } }

        public ConfigTrayIcon()
        {
            InitializeComponent();
        }

        private void ConfigTrayIcon_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TbWebViewUrl.Text)) TbWebViewUrl.Text = Variables.AppSettings.HassUri;
            InitMultiScreenConfig();
        }

        private void InitMultiScreenConfig()
        {
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
                NumWebViewScreen.Items.Add(label);
                ix++;
            }

            NumWebViewScreen.SelectedIndex = 0;
            SelectedScreen = SelectedScreen;

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
            Debug.WriteLine("X-Coordinate " + webView.X);
            HelperFunctions.LaunchTrayIconWebView(webView, NumWebViewScreen.SelectedIndex);
        }

        private void BtnWebViewReset_Click(object sender, EventArgs e)
        {
            NumWebViewWidth.Value = 700;
            NumWebViewHeight.Value = 560;
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            SelectedScreen = (sender as DomainUpDown).SelectedIndex;
        }
    }
}
