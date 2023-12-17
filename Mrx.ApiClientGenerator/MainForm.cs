using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Text.Json;
using Mrx.ApiClientGenerator.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mrx.ApiClientGenerator
{
    public partial class MainForm : Form
    {
        public static MainForm Instance { get; private set; }

        public string FilePath => Path.Combine(Application.StartupPath, "profiles.json");
        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            //for (int i = 0; i < 2; i++)
            //{
            //    btnNewProfile_Click(null, null);
            //}
        }

        private void btnNewProfile_Click(object sender, EventArgs e)
        {
            var profiles = GetProfiles().Select(p => p.Name).ToArray();
            string name = "";
            int number = 0;
            do
            {
                number++;
                name = $"profile {number}";
            } while (profiles.Contains(name));
            AddTab(new ProfileModel { Name = name });
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void Save()
        {
            File.WriteAllText(FilePath, JsonSerializer.Serialize(new FileModel
            {
                Profiles = GetProfiles(),
                SelectedIndexProfile = tabControlProfiles.SelectedIndex
            }));
        }

        private List<ProfileModel> GetProfiles()
        {
            return tabControlProfiles.TabPages
                .Cast<TabPage>()
                .Select(p => (p.Controls[0] as ApiClientGeneratorControl).Model)
                .ToList();
        }

        private void AddTab(ProfileModel model)
        {
            model.Id = model.Id ?? Guid.NewGuid().ToString();
            var tab = new TabPage(model.Name);
            tab.Controls.Add(new ApiClientGeneratorControl
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Model = model,
            });
            tabControlProfiles.TabPages.Add(tab);
            tabControlProfiles.SelectTab(tab);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(FilePath))
            {
                var data = JsonSerializer.Deserialize<FileModel>(File.ReadAllText(FilePath));
                foreach (var profile in data.Profiles)
                    AddTab(profile);
                try
                {
                    tabControlProfiles.SelectTab(data.SelectedIndexProfile.Value);
                }
                catch (Exception ex) { }
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (tabControlProfiles.SelectedTab == null) return;
            var model = (tabControlProfiles?.SelectedTab?.Controls?[0] as ApiClientGeneratorControl)?.Model;
            model.Id = null;
            model.Name += " 2";
            AddTab(model);
        }
        public static async Task SuccessNotify()
        {
            MainForm.Instance.lblSuccess.Visible = true;
            await Task.Delay(3000);
            MainForm.Instance.lblSuccess.Visible = false;
        }
        public static async Task FailureNotify()
        {
            MainForm.Instance.lblError.Visible = true;
            await Task.Delay(3000);
            MainForm.Instance.lblError.Visible = false;
        }
    }
}
