using Mrx.ApiClientGenerator.Helpers;
using Mrx.ApiClientGenerator.Models;
using NJsonSchema.CodeGeneration.TypeScript;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mrx.ApiClientGenerator
{
    public partial class ApiClientGeneratorControl : UserControl
    {
        private string id;
        public ProfileModel Model
        {
            get
            {
                var clientBaseClassStatus = chkClientBaseClass.Checked;
                return new ProfileModel
                {
                    Id = id,
                    Name = txtProfileName.Text,
                    Url = txtUrl.Text,
                    GeneratePath = txtGeneratePath.Text,
                    Language = (Language)cbbLanguage.SelectedItem,
                    TypeScriptDateTimeType = (TypeScriptDateTimeType)cbbTypeScriptDateTimeType.SelectedItem,
                    BaseUrl = txtBaseUrl.Text,
                    ClientBaseClassStatus = clientBaseClassStatus,
                    ClientBaseClass = clientBaseClassStatus ? txtClientBaseClass.Text : null,
                    ExtensionCode = clientBaseClassStatus ? txtExtensionCode.Text : null,
                    UseGetBaseUrlMethod = clientBaseClassStatus && chkUseGetBaseUrlMethod.Checked,
                    UseTransformOptionsMethod = clientBaseClassStatus && chkUseTransformOptionsMethod.Checked,
                    UseTransformResultMethod = clientBaseClassStatus && chkUseTransformResultMethod.Checked,
                };
            }
            set
            {
                if (value is null) return;
                id = value.Id;
                txtProfileName.Text = value.Name;
                txtUrl.Text = value.Url;
                txtGeneratePath.Text = value.GeneratePath;
                cbbLanguage.SelectedItem = value.Language;
                cbbTypeScriptDateTimeType.SelectedItem = value.TypeScriptDateTimeType;
                txtBaseUrl.Text = value.BaseUrl;
                chkClientBaseClass.Checked = value.ClientBaseClassStatus;
                txtClientBaseClass.Text = value.ClientBaseClass;
                txtExtensionCode.Text = value.ExtensionCode;
                chkUseGetBaseUrlMethod.Checked = value.UseGetBaseUrlMethod;
                chkUseTransformOptionsMethod.Checked = value.UseTransformOptionsMethod;
                chkUseTransformResultMethod.Checked = value.UseTransformResultMethod;
            }
        }
        public ApiClientGeneratorControl()
        {
            InitializeComponent();
            cbbLanguage.DataSource = Enum.GetValues(typeof(Language));
            cbbTypeScriptDateTimeType.DataSource = Enum.GetValues(typeof(TypeScriptDateTimeType));
        }

        private void ApiClientGeneratorControl_Load(object sender, EventArgs e)
        {
            txtProfileName.Text = Parent.Text;
        }

        private void txtProfileName_TextChanged(object sender, EventArgs e)
        {
            if (Parent != null)
                Parent.Text = txtProfileName.Text;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Profile delete ?", "Delete confirm", MessageBoxButtons.YesNo) == DialogResult.Yes
                && Parent is TabPage tab
                && Parent.Parent is TabControl tabControlProfiles)
                tabControlProfiles.TabPages.Remove(tab);
        }

        private void txtClientBaseClass_TextChanged(object sender, EventArgs e)
        {
            txtExtensionCode.Text = $"import {{ {txtClientBaseClass.Text} }} from './{txtClientBaseClass.Text}';";
        }

        private void chkClientBaseClass_CheckedChanged(object sender, EventArgs e)
        {
            lblClientBaseClass.Enabled =
            txtClientBaseClass.Enabled =
            lblExtensionCode.Enabled =
            txtExtensionCode.Enabled =
            chkUseGetBaseUrlMethod.Enabled =
            chkUseTransformOptionsMethod.Enabled =
            chkUseTransformResultMethod.Enabled =
            chkClientBaseClass.Checked;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Hide();
            lblLoading.Show();
            var r = await ApiClientGeneratorHelper.Start(Model);
            lblLoading.Hide();
            btnStart.Show();
            if (r)
                await MainForm.SuccessNotify();
            else
                await MainForm.FailureNotify();
        }
    }
}
