namespace Mrx.ApiClientGenerator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControlProfiles = new System.Windows.Forms.TabControl();
            this.btnNewProfile = new System.Windows.Forms.Button();
            this.btnDuplicate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblSuccess = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tabControlProfiles
            // 
            this.tabControlProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProfiles.Location = new System.Drawing.Point(0, 46);
            this.tabControlProfiles.Name = "tabControlProfiles";
            this.tabControlProfiles.SelectedIndex = 0;
            this.tabControlProfiles.Size = new System.Drawing.Size(884, 417);
            this.tabControlProfiles.TabIndex = 0;
            // 
            // btnNewProfile
            // 
            this.btnNewProfile.Location = new System.Drawing.Point(12, 12);
            this.btnNewProfile.Name = "btnNewProfile";
            this.btnNewProfile.Size = new System.Drawing.Size(108, 23);
            this.btnNewProfile.TabIndex = 2;
            this.btnNewProfile.Text = "New Profile";
            this.btnNewProfile.UseVisualStyleBackColor = true;
            this.btnNewProfile.Click += new System.EventHandler(this.btnNewProfile_Click);
            // 
            // btnDuplicate
            // 
            this.btnDuplicate.Location = new System.Drawing.Point(126, 12);
            this.btnDuplicate.Name = "btnDuplicate";
            this.btnDuplicate.Size = new System.Drawing.Size(108, 23);
            this.btnDuplicate.TabIndex = 2;
            this.btnDuplicate.Text = "Duplicate";
            this.btnDuplicate.UseVisualStyleBackColor = true;
            this.btnDuplicate.Click += new System.EventHandler(this.btnDuplicate_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(240, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblSuccess
            // 
            this.lblSuccess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblSuccess.Location = new System.Drawing.Point(772, 12);
            this.lblSuccess.Name = "lblSuccess";
            this.lblSuccess.Size = new System.Drawing.Size(100, 23);
            this.lblSuccess.TabIndex = 4;
            this.lblSuccess.Text = "Success";
            this.lblSuccess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSuccess.Visible = false;
            // 
            // lblError
            // 
            this.lblError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblError.Location = new System.Drawing.Point(772, 12);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(100, 23);
            this.lblError.TabIndex = 4;
            this.lblError.Text = "Error";
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblError.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblSuccess);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDuplicate);
            this.Controls.Add(this.btnNewProfile);
            this.Controls.Add(this.tabControlProfiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Mrx.ApiClientGenerator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlProfiles;
        private System.Windows.Forms.Button btnNewProfile;
        private System.Windows.Forms.Button btnDuplicate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSuccess;
        private System.Windows.Forms.Label lblError;
    }
}

