namespace AntilockUtility
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.j2534ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eLM327ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readDTCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearDTCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetECUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eCUSelfTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectVariantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fGFalconGithubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.j2534comauToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.CONNECT_J2534 = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.WRITE_ABS_CONFIG = new System.Windows.Forms.Button();
            this.ABS_SELECT_VARIANT = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.connectionToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.j2534ToolStripMenuItem,
            this.eLM327ToolStripMenuItem});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
            this.connectionToolStripMenuItem.Text = "Vehicle Interface";
            // 
            // j2534ToolStripMenuItem
            // 
            this.j2534ToolStripMenuItem.Name = "j2534ToolStripMenuItem";
            this.j2534ToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.j2534ToolStripMenuItem.Text = "J2534";
            // 
            // eLM327ToolStripMenuItem
            // 
            this.eLM327ToolStripMenuItem.Name = "eLM327ToolStripMenuItem";
            this.eLM327ToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.eLM327ToolStripMenuItem.Text = "ELM327";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readDTCToolStripMenuItem,
            this.clearDTCToolStripMenuItem,
            this.resetECUToolStripMenuItem,
            this.eCUSelfTestToolStripMenuItem,
            this.selectVariantToolStripMenuItem});
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.infoToolStripMenuItem.Text = "Ecu Tools";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // readDTCToolStripMenuItem
            // 
            this.readDTCToolStripMenuItem.Name = "readDTCToolStripMenuItem";
            this.readDTCToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.readDTCToolStripMenuItem.Text = "Read DTC";
            // 
            // clearDTCToolStripMenuItem
            // 
            this.clearDTCToolStripMenuItem.Name = "clearDTCToolStripMenuItem";
            this.clearDTCToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.clearDTCToolStripMenuItem.Text = "Clear DTC";
            // 
            // resetECUToolStripMenuItem
            // 
            this.resetECUToolStripMenuItem.Name = "resetECUToolStripMenuItem";
            this.resetECUToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.resetECUToolStripMenuItem.Text = "Reset ECU";
            // 
            // eCUSelfTestToolStripMenuItem
            // 
            this.eCUSelfTestToolStripMenuItem.Name = "eCUSelfTestToolStripMenuItem";
            this.eCUSelfTestToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.eCUSelfTestToolStripMenuItem.Text = "ECU Self Test";
            // 
            // selectVariantToolStripMenuItem
            // 
            this.selectVariantToolStripMenuItem.Name = "selectVariantToolStripMenuItem";
            this.selectVariantToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.selectVariantToolStripMenuItem.Text = "Select Variant";
            this.selectVariantToolStripMenuItem.Click += new System.EventHandler(this.selectVariantToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fGFalconGithubToolStripMenuItem,
            this.j2534comauToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // fGFalconGithubToolStripMenuItem
            // 
            this.fGFalconGithubToolStripMenuItem.Name = "fGFalconGithubToolStripMenuItem";
            this.fGFalconGithubToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.fGFalconGithubToolStripMenuItem.Text = "FG Falcon Github";
            // 
            // j2534comauToolStripMenuItem
            // 
            this.j2534comauToolStripMenuItem.Name = "j2534comauToolStripMenuItem";
            this.j2534comauToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.j2534comauToolStripMenuItem.Text = "J2534.com.au";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(25, 46);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.CONNECT_J2534);
            this.splitContainer1.Panel1.Controls.Add(this.button3);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.WRITE_ABS_CONFIG);
            this.splitContainer1.Panel2.Controls.Add(this.ABS_SELECT_VARIANT);
            this.splitContainer1.Panel2.Controls.Add(this.progressBar1);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(929, 236);
            this.splitContainer1.SplitterDistance = 309;
            this.splitContainer1.TabIndex = 2;
            // 
            // CONNECT_J2534
            // 
            this.CONNECT_J2534.AccessibleRole = System.Windows.Forms.AccessibleRole.DropList;
            this.CONNECT_J2534.AllowDrop = true;
            this.CONNECT_J2534.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.CONNECT_J2534.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.CONNECT_J2534.FormattingEnabled = true;
            this.CONNECT_J2534.Location = new System.Drawing.Point(0, 42);
            this.CONNECT_J2534.Name = "CONNECT_J2534";
            this.CONNECT_J2534.Size = new System.Drawing.Size(187, 23);
            this.CONNECT_J2534.TabIndex = 1;
            this.CONNECT_J2534.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(193, 42);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Connect J2534";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Antilock Braking Module Variant Utility";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(410, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(178, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Read Current ABS Config";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // WRITE_ABS_CONFIG
            // 
            this.WRITE_ABS_CONFIG.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.WRITE_ABS_CONFIG.Location = new System.Drawing.Point(410, 72);
            this.WRITE_ABS_CONFIG.Name = "WRITE_ABS_CONFIG";
            this.WRITE_ABS_CONFIG.Size = new System.Drawing.Size(178, 23);
            this.WRITE_ABS_CONFIG.TabIndex = 2;
            this.WRITE_ABS_CONFIG.Text = "Write Data to ABS ECU";
            this.WRITE_ABS_CONFIG.UseVisualStyleBackColor = true;
            this.WRITE_ABS_CONFIG.Click += new System.EventHandler(this.button1_Click);
            // 
            // ABS_SELECT_VARIANT
            // 
            this.ABS_SELECT_VARIANT.AccessibleRole = System.Windows.Forms.AccessibleRole.DropList;
            this.ABS_SELECT_VARIANT.AllowDrop = true;
            this.ABS_SELECT_VARIANT.AutoCompleteCustomSource.AddRange(new string[] {
            "XTG1G2SportIRS          ",
            "XTSportIRS              ",
            "GTurbo                  ",
            "G6SedanDLPGXTG6G6ESport ",
            "PoliceSedanDLPGXTSport  ",
            "UteDLPGREBXR612t        ",
            "F6PursultUTE12t4pot     ",
            "F6PursultUTE12t6pot     ",
            "XT                      ",
            "G6SedanDLPGXTStd        ",
            "UteI6RDLPGXL34t         ",
            "F6Force64potBrakes      ",
            "F6Force66potBrakes      ",
            "XR6                     ",
            "XR6Turbo                ",
            "XR6TurboPolice          ",
            "GTForce4pOtBrakes       ",
            "GTGTPForces6potBrakes   ",
            "XR8                     ",
            "XR8Police               ",
            "UteI67DLPG1t            ",
            "UteI6                   ",
            "G6SedanDLPGXTHDSus      ",
            "XTPoliceHDFrontSusandIRS",
            "XTHDFrontSusandIRS     "});
            this.ABS_SELECT_VARIANT.FormattingEnabled = true;
            this.ABS_SELECT_VARIANT.Items.AddRange(new object[] {
            "XTG1G2SportIRS          ",
            "XTSportIRS              ",
            "GTurbo                  ",
            "G6SedanDLPGXTG6G6ESport ",
            "PoliceSedanDLPGXTSport  ",
            "UteDLPGREBXR612t        ",
            "F6PursultUTE12t4pot     ",
            "F6PursultUTE12t6pot     ",
            "XT                      ",
            "G6SedanDLPGXTStd        ",
            "UteI6RDLPGXL34t         ",
            "F6Force64potBrakes      ",
            "F6Force66potBrakes      ",
            "XR6                     ",
            "XR6Turbo                ",
            "XR6TurboPolice          ",
            "GTForce4pOtBrakes       ",
            "GTGTPForces6potBrakes   ",
            "XR8                     ",
            "XR8Police               ",
            "UteI67DLPG1t            ",
            "UteI6                   ",
            "G6SedanDLPGXTHDSus      ",
            "XTPoliceHDFrontSusandIRS",
            "XTHDFrontSusandIRS     "});
            this.ABS_SELECT_VARIANT.Location = new System.Drawing.Point(121, 72);
            this.ABS_SELECT_VARIANT.Name = "ABS_SELECT_VARIANT";
            this.ABS_SELECT_VARIANT.Size = new System.Drawing.Size(283, 23);
            this.ABS_SELECT_VARIANT.TabIndex = 1;
            this.ABS_SELECT_VARIANT.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            this.ABS_SELECT_VARIANT.Click += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(23, 343);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(576, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 320);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Vehicle Iterface";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Current Configuration:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Select New Config ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 342);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "He";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripMenuItem connectionToolStripMenuItem;
        private ToolStripMenuItem infoToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem readDTCToolStripMenuItem;
        private ToolStripMenuItem clearDTCToolStripMenuItem;
        private ToolStripMenuItem resetECUToolStripMenuItem;
        private ToolStripMenuItem eCUSelfTestToolStripMenuItem;
        private ToolStripMenuItem selectVariantToolStripMenuItem;
        private ToolStripMenuItem j2534ToolStripMenuItem;
        private ToolStripMenuItem eLM327ToolStripMenuItem;
        private ToolStripMenuItem fGFalconGithubToolStripMenuItem;
        private ToolStripMenuItem j2534comauToolStripMenuItem;
        private SplitContainer splitContainer1;
        private Button WRITE_ABS_CONFIG;
        private ComboBox ABS_SELECT_VARIANT;
        private ProgressBar progressBar1;
        private StatusStrip statusStrip1;
        private Button button2;
        private ComboBox CONNECT_J2534;
        private Button button3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
