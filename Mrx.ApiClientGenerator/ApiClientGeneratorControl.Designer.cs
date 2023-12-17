namespace Mrx.ApiClientGenerator
{
    partial class ApiClientGeneratorControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtProfileName = new System.Windows.Forms.TextBox();
            this.lblProfileName = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.txtGeneratePath = new System.Windows.Forms.TextBox();
            this.lblGeneratePath = new System.Windows.Forms.Label();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cbbLanguage = new System.Windows.Forms.ComboBox();
            this.txtBaseUrl = new System.Windows.Forms.TextBox();
            this.lblBaseUrl = new System.Windows.Forms.Label();
            this.txtClientBaseClass = new System.Windows.Forms.TextBox();
            this.lblClientBaseClass = new System.Windows.Forms.Label();
            this.chkClientBaseClass = new System.Windows.Forms.CheckBox();
            this.txtExtensionCode = new System.Windows.Forms.TextBox();
            this.lblExtensionCode = new System.Windows.Forms.Label();
            this.chkUseGetBaseUrlMethod = new System.Windows.Forms.CheckBox();
            this.chkUseTransformOptionsMethod = new System.Windows.Forms.CheckBox();
            this.chkUseTransformResultMethod = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblLoading = new System.Windows.Forms.Label();
            this.cbbTypeScriptDateTimeType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(819, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(48, 45);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "×";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtProfileName
            // 
            this.txtProfileName.Location = new System.Drawing.Point(146, 32);
            this.txtProfileName.Name = "txtProfileName";
            this.txtProfileName.Size = new System.Drawing.Size(300, 20);
            this.txtProfileName.TabIndex = 0;
            this.txtProfileName.TextChanged += new System.EventHandler(this.txtProfileName_TextChanged);
            // 
            // lblProfileName
            // 
            this.lblProfileName.AutoSize = true;
            this.lblProfileName.Location = new System.Drawing.Point(64, 35);
            this.lblProfileName.Name = "lblProfileName";
            this.lblProfileName.Size = new System.Drawing.Size(76, 13);
            this.lblProfileName.TabIndex = 2;
            this.lblProfileName.Text = "Profile Name : ";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(146, 71);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(300, 20);
            this.txtUrl.TabIndex = 1;
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(40, 74);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(100, 13);
            this.lblUrl.TabIndex = 2;
            this.lblUrl.Text = "Url (swagger.json) : ";
            // 
            // txtGeneratePath
            // 
            this.txtGeneratePath.Location = new System.Drawing.Point(146, 110);
            this.txtGeneratePath.Name = "txtGeneratePath";
            this.txtGeneratePath.Size = new System.Drawing.Size(300, 20);
            this.txtGeneratePath.TabIndex = 2;
            // 
            // lblGeneratePath
            // 
            this.lblGeneratePath.AutoSize = true;
            this.lblGeneratePath.Location = new System.Drawing.Point(24, 113);
            this.lblGeneratePath.Name = "lblGeneratePath";
            this.lblGeneratePath.Size = new System.Drawing.Size(116, 13);
            this.lblGeneratePath.TabIndex = 2;
            this.lblGeneratePath.Text = "Generate Path (cs,ts) : ";
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(76, 152);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(64, 13);
            this.lblLanguage.TabIndex = 2;
            this.lblLanguage.Text = "Language : ";
            // 
            // cbbLanguage
            // 
            this.cbbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbLanguage.FormattingEnabled = true;
            this.cbbLanguage.Location = new System.Drawing.Point(146, 149);
            this.cbbLanguage.Name = "cbbLanguage";
            this.cbbLanguage.Size = new System.Drawing.Size(300, 21);
            this.cbbLanguage.TabIndex = 3;
            // 
            // txtBaseUrl
            // 
            this.txtBaseUrl.Location = new System.Drawing.Point(146, 189);
            this.txtBaseUrl.Name = "txtBaseUrl";
            this.txtBaseUrl.Size = new System.Drawing.Size(300, 20);
            this.txtBaseUrl.TabIndex = 4;
            // 
            // lblBaseUrl
            // 
            this.lblBaseUrl.AutoSize = true;
            this.lblBaseUrl.Location = new System.Drawing.Point(36, 191);
            this.lblBaseUrl.Name = "lblBaseUrl";
            this.lblBaseUrl.Size = new System.Drawing.Size(104, 13);
            this.lblBaseUrl.TabIndex = 2;
            this.lblBaseUrl.Text = "Base Url (site.com) : ";
            // 
            // txtClientBaseClass
            // 
            this.txtClientBaseClass.Location = new System.Drawing.Point(146, 228);
            this.txtClientBaseClass.Name = "txtClientBaseClass";
            this.txtClientBaseClass.Size = new System.Drawing.Size(300, 20);
            this.txtClientBaseClass.TabIndex = 6;
            this.txtClientBaseClass.TextChanged += new System.EventHandler(this.txtClientBaseClass_TextChanged);
            // 
            // lblClientBaseClass
            // 
            this.lblClientBaseClass.AutoSize = true;
            this.lblClientBaseClass.Location = new System.Drawing.Point(49, 230);
            this.lblClientBaseClass.Name = "lblClientBaseClass";
            this.lblClientBaseClass.Size = new System.Drawing.Size(91, 13);
            this.lblClientBaseClass.TabIndex = 2;
            this.lblClientBaseClass.Text = "ClientBaseClass : ";
            // 
            // chkClientBaseClass
            // 
            this.chkClientBaseClass.AutoSize = true;
            this.chkClientBaseClass.Checked = true;
            this.chkClientBaseClass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClientBaseClass.Location = new System.Drawing.Point(28, 230);
            this.chkClientBaseClass.Name = "chkClientBaseClass";
            this.chkClientBaseClass.Size = new System.Drawing.Size(15, 14);
            this.chkClientBaseClass.TabIndex = 5;
            this.chkClientBaseClass.UseVisualStyleBackColor = true;
            this.chkClientBaseClass.CheckedChanged += new System.EventHandler(this.chkClientBaseClass_CheckedChanged);
            // 
            // txtExtensionCode
            // 
            this.txtExtensionCode.Location = new System.Drawing.Point(146, 267);
            this.txtExtensionCode.Name = "txtExtensionCode";
            this.txtExtensionCode.Size = new System.Drawing.Size(300, 20);
            this.txtExtensionCode.TabIndex = 7;
            // 
            // lblExtensionCode
            // 
            this.lblExtensionCode.AutoSize = true;
            this.lblExtensionCode.Location = new System.Drawing.Point(53, 269);
            this.lblExtensionCode.Name = "lblExtensionCode";
            this.lblExtensionCode.Size = new System.Drawing.Size(87, 13);
            this.lblExtensionCode.TabIndex = 2;
            this.lblExtensionCode.Text = "ExtensionCode : ";
            // 
            // chkUseGetBaseUrlMethod
            // 
            this.chkUseGetBaseUrlMethod.AutoSize = true;
            this.chkUseGetBaseUrlMethod.Location = new System.Drawing.Point(27, 323);
            this.chkUseGetBaseUrlMethod.Name = "chkUseGetBaseUrlMethod";
            this.chkUseGetBaseUrlMethod.Size = new System.Drawing.Size(135, 17);
            this.chkUseGetBaseUrlMethod.TabIndex = 8;
            this.chkUseGetBaseUrlMethod.Text = "UseGetBaseUrlMethod";
            this.chkUseGetBaseUrlMethod.UseVisualStyleBackColor = true;
            // 
            // chkUseTransformOptionsMethod
            // 
            this.chkUseTransformOptionsMethod.AutoSize = true;
            this.chkUseTransformOptionsMethod.Location = new System.Drawing.Point(172, 323);
            this.chkUseTransformOptionsMethod.Name = "chkUseTransformOptionsMethod";
            this.chkUseTransformOptionsMethod.Size = new System.Drawing.Size(164, 17);
            this.chkUseTransformOptionsMethod.TabIndex = 9;
            this.chkUseTransformOptionsMethod.Text = "UseTransformOptionsMethod";
            this.chkUseTransformOptionsMethod.UseVisualStyleBackColor = true;
            // 
            // chkUseTransformResultMethod
            // 
            this.chkUseTransformResultMethod.AutoSize = true;
            this.chkUseTransformResultMethod.Location = new System.Drawing.Point(346, 323);
            this.chkUseTransformResultMethod.Name = "chkUseTransformResultMethod";
            this.chkUseTransformResultMethod.Size = new System.Drawing.Size(158, 17);
            this.chkUseTransformResultMethod.TabIndex = 10;
            this.chkUseTransformResultMethod.Text = "UseTransformResultMethod";
            this.chkUseTransformResultMethod.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(711, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(102, 45);
            this.btnStart.TabIndex = 11;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(739, 18);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(50, 13);
            this.lblLoading.TabIndex = 2;
            this.lblLoading.Text = "loading...";
            this.lblLoading.Visible = false;
            // 
            // cbbTypeScriptDateTimeType
            // 
            this.cbbTypeScriptDateTimeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTypeScriptDateTimeType.FormattingEnabled = true;
            this.cbbTypeScriptDateTimeType.Location = new System.Drawing.Point(465, 149);
            this.cbbTypeScriptDateTimeType.Name = "cbbTypeScriptDateTimeType";
            this.cbbTypeScriptDateTimeType.Size = new System.Drawing.Size(130, 21);
            this.cbbTypeScriptDateTimeType.TabIndex = 3;
            // 
            // ApiClientGeneratorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkUseTransformResultMethod);
            this.Controls.Add(this.chkUseTransformOptionsMethod);
            this.Controls.Add(this.chkUseGetBaseUrlMethod);
            this.Controls.Add(this.chkClientBaseClass);
            this.Controls.Add(this.cbbTypeScriptDateTimeType);
            this.Controls.Add(this.cbbLanguage);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.lblExtensionCode);
            this.Controls.Add(this.lblClientBaseClass);
            this.Controls.Add(this.lblBaseUrl);
            this.Controls.Add(this.lblGeneratePath);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.lblProfileName);
            this.Controls.Add(this.txtExtensionCode);
            this.Controls.Add(this.txtClientBaseClass);
            this.Controls.Add(this.txtBaseUrl);
            this.Controls.Add(this.txtGeneratePath);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.txtProfileName);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnDelete);
            this.Name = "ApiClientGeneratorControl";
            this.Size = new System.Drawing.Size(870, 385);
            this.Load += new System.EventHandler(this.ApiClientGeneratorControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtProfileName;
        private System.Windows.Forms.Label lblProfileName;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.TextBox txtGeneratePath;
        private System.Windows.Forms.Label lblGeneratePath;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.ComboBox cbbLanguage;
        private System.Windows.Forms.TextBox txtBaseUrl;
        private System.Windows.Forms.Label lblBaseUrl;
        private System.Windows.Forms.TextBox txtClientBaseClass;
        private System.Windows.Forms.Label lblClientBaseClass;
        private System.Windows.Forms.CheckBox chkClientBaseClass;
        private System.Windows.Forms.TextBox txtExtensionCode;
        private System.Windows.Forms.Label lblExtensionCode;
        private System.Windows.Forms.CheckBox chkUseGetBaseUrlMethod;
        private System.Windows.Forms.CheckBox chkUseTransformOptionsMethod;
        private System.Windows.Forms.CheckBox chkUseTransformResultMethod;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.ComboBox cbbTypeScriptDateTimeType;
    }
}
