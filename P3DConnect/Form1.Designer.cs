namespace P3DConnect
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonFlaps = new System.Windows.Forms.Button();
            this.buttonAP = new System.Windows.Forms.Button();
            this.buttonLights = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.labelAlt = new System.Windows.Forms.Label();
            this.labelCom = new System.Windows.Forms.Label();
            this.labelFlaps = new System.Windows.Forms.Label();
            this.groupRead = new System.Windows.Forms.GroupBox();
            this.labelAutoP = new System.Windows.Forms.Label();
            this.textBoxHDG = new System.Windows.Forms.TextBox();
            this.labelRight = new System.Windows.Forms.Label();
            this.textBoxRight = new System.Windows.Forms.TextBox();
            this.labelLeft = new System.Windows.Forms.Label();
            this.textBoxAlt = new System.Windows.Forms.TextBox();
            this.textBoxCom = new System.Windows.Forms.TextBox();
            this.textBoxFlaps = new System.Windows.Forms.TextBox();
            this.textBoxNose = new System.Windows.Forms.TextBox();
            this.textBoxLeft = new System.Windows.Forms.TextBox();
            this.textBoxIAS = new System.Windows.Forms.TextBox();
            this.labelNose = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.textBoxSetHDG = new System.Windows.Forms.TextBox();
            this.buttonSet = new System.Windows.Forms.Button();
            this.groupToggle = new System.Windows.Forms.GroupBox();
            this.groupSet = new System.Windows.Forms.GroupBox();
            this.buttonSetSpd = new System.Windows.Forms.Button();
            this.textBoxSetSpd = new System.Windows.Forms.TextBox();
            this.buttonCOM = new System.Windows.Forms.Button();
            this.textBoxSetCom = new System.Windows.Forms.TextBox();
            this.groupRead.SuspendLayout();
            this.groupToggle.SuspendLayout();
            this.groupSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(351, 23);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(114, 26);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect P3D";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(351, 59);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(114, 26);
            this.buttonDisconnect.TabIndex = 2;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonFlaps
            // 
            this.buttonFlaps.Location = new System.Drawing.Point(6, 19);
            this.buttonFlaps.Name = "buttonFlaps";
            this.buttonFlaps.Size = new System.Drawing.Size(114, 23);
            this.buttonFlaps.TabIndex = 6;
            this.buttonFlaps.Text = "Flaps Up/Dwn";
            this.buttonFlaps.UseVisualStyleBackColor = true;
            this.buttonFlaps.Click += new System.EventHandler(this.buttonFlaps_Click);
            // 
            // buttonAP
            // 
            this.buttonAP.Location = new System.Drawing.Point(7, 77);
            this.buttonAP.Name = "buttonAP";
            this.buttonAP.Size = new System.Drawing.Size(114, 23);
            this.buttonAP.TabIndex = 7;
            this.buttonAP.Text = "AutoPilot On/Off";
            this.buttonAP.UseVisualStyleBackColor = true;
            this.buttonAP.Click += new System.EventHandler(this.buttonAP_Click);
            // 
            // buttonLights
            // 
            this.buttonLights.Location = new System.Drawing.Point(7, 48);
            this.buttonLights.Name = "buttonLights";
            this.buttonLights.Size = new System.Drawing.Size(113, 22);
            this.buttonLights.TabIndex = 8;
            this.buttonLights.Text = "Gear Up/Dwn";
            this.buttonLights.UseVisualStyleBackColor = true;
            this.buttonLights.Click += new System.EventHandler(this.buttonLights_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 30);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(60, 13);
            this.labelStatus.TabIndex = 10;
            this.labelStatus.Text = "Status: Idle";
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(6, 26);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(30, 13);
            this.labelSpeed.TabIndex = 12;
            this.labelSpeed.Text = "IAS: ";
            // 
            // labelAlt
            // 
            this.labelAlt.AutoSize = true;
            this.labelAlt.Location = new System.Drawing.Point(6, 47);
            this.labelAlt.Name = "labelAlt";
            this.labelAlt.Size = new System.Drawing.Size(48, 13);
            this.labelAlt.TabIndex = 13;
            this.labelAlt.Text = "Altitude: ";
            // 
            // labelCom
            // 
            this.labelCom.AutoSize = true;
            this.labelCom.Location = new System.Drawing.Point(6, 73);
            this.labelCom.Name = "labelCom";
            this.labelCom.Size = new System.Drawing.Size(43, 13);
            this.labelCom.TabIndex = 14;
            this.labelCom.Text = "COM1: ";
            // 
            // labelFlaps
            // 
            this.labelFlaps.AutoSize = true;
            this.labelFlaps.Location = new System.Drawing.Point(6, 99);
            this.labelFlaps.Name = "labelFlaps";
            this.labelFlaps.Size = new System.Drawing.Size(38, 13);
            this.labelFlaps.TabIndex = 15;
            this.labelFlaps.Text = "Flaps: ";
            // 
            // groupRead
            // 
            this.groupRead.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupRead.Controls.Add(this.labelAutoP);
            this.groupRead.Controls.Add(this.textBoxHDG);
            this.groupRead.Controls.Add(this.labelRight);
            this.groupRead.Controls.Add(this.textBoxRight);
            this.groupRead.Controls.Add(this.labelLeft);
            this.groupRead.Controls.Add(this.textBoxAlt);
            this.groupRead.Controls.Add(this.textBoxCom);
            this.groupRead.Controls.Add(this.textBoxFlaps);
            this.groupRead.Controls.Add(this.textBoxNose);
            this.groupRead.Controls.Add(this.textBoxLeft);
            this.groupRead.Controls.Add(this.textBoxIAS);
            this.groupRead.Controls.Add(this.labelNose);
            this.groupRead.Controls.Add(this.labelSpeed);
            this.groupRead.Controls.Add(this.labelFlaps);
            this.groupRead.Controls.Add(this.labelAlt);
            this.groupRead.Controls.Add(this.labelCom);
            this.groupRead.Location = new System.Drawing.Point(12, 97);
            this.groupRead.Name = "groupRead";
            this.groupRead.Size = new System.Drawing.Size(210, 298);
            this.groupRead.TabIndex = 16;
            this.groupRead.TabStop = false;
            this.groupRead.Text = "P3D  data";
            // 
            // labelAutoP
            // 
            this.labelAutoP.AutoSize = true;
            this.labelAutoP.Location = new System.Drawing.Point(6, 203);
            this.labelAutoP.Name = "labelAutoP";
            this.labelAutoP.Size = new System.Drawing.Size(59, 13);
            this.labelAutoP.TabIndex = 29;
            this.labelAutoP.Text = "Autopilot H";
            // 
            // textBoxHDG
            // 
            this.textBoxHDG.Location = new System.Drawing.Point(69, 200);
            this.textBoxHDG.Name = "textBoxHDG";
            this.textBoxHDG.ReadOnly = true;
            this.textBoxHDG.Size = new System.Drawing.Size(58, 20);
            this.textBoxHDG.TabIndex = 28;
            // 
            // labelRight
            // 
            this.labelRight.AutoSize = true;
            this.labelRight.Location = new System.Drawing.Point(6, 176);
            this.labelRight.Name = "labelRight";
            this.labelRight.Size = new System.Drawing.Size(59, 13);
            this.labelRight.TabIndex = 27;
            this.labelRight.Text = "Rightgear: ";
            // 
            // textBoxRight
            // 
            this.textBoxRight.Location = new System.Drawing.Point(69, 174);
            this.textBoxRight.Name = "textBoxRight";
            this.textBoxRight.ReadOnly = true;
            this.textBoxRight.Size = new System.Drawing.Size(58, 20);
            this.textBoxRight.TabIndex = 26;
            // 
            // labelLeft
            // 
            this.labelLeft.AutoSize = true;
            this.labelLeft.Location = new System.Drawing.Point(6, 151);
            this.labelLeft.Name = "labelLeft";
            this.labelLeft.Size = new System.Drawing.Size(52, 13);
            this.labelLeft.TabIndex = 25;
            this.labelLeft.Text = "Leftgear: ";
            // 
            // textBoxAlt
            // 
            this.textBoxAlt.Location = new System.Drawing.Point(69, 44);
            this.textBoxAlt.Name = "textBoxAlt";
            this.textBoxAlt.ReadOnly = true;
            this.textBoxAlt.Size = new System.Drawing.Size(58, 20);
            this.textBoxAlt.TabIndex = 24;
            // 
            // textBoxCom
            // 
            this.textBoxCom.Location = new System.Drawing.Point(69, 70);
            this.textBoxCom.Name = "textBoxCom";
            this.textBoxCom.ReadOnly = true;
            this.textBoxCom.Size = new System.Drawing.Size(58, 20);
            this.textBoxCom.TabIndex = 23;
            // 
            // textBoxFlaps
            // 
            this.textBoxFlaps.Location = new System.Drawing.Point(69, 96);
            this.textBoxFlaps.Name = "textBoxFlaps";
            this.textBoxFlaps.ReadOnly = true;
            this.textBoxFlaps.Size = new System.Drawing.Size(58, 20);
            this.textBoxFlaps.TabIndex = 22;
            // 
            // textBoxNose
            // 
            this.textBoxNose.Location = new System.Drawing.Point(69, 122);
            this.textBoxNose.Name = "textBoxNose";
            this.textBoxNose.ReadOnly = true;
            this.textBoxNose.Size = new System.Drawing.Size(58, 20);
            this.textBoxNose.TabIndex = 21;
            // 
            // textBoxLeft
            // 
            this.textBoxLeft.Location = new System.Drawing.Point(69, 148);
            this.textBoxLeft.Name = "textBoxLeft";
            this.textBoxLeft.ReadOnly = true;
            this.textBoxLeft.Size = new System.Drawing.Size(58, 20);
            this.textBoxLeft.TabIndex = 20;
            // 
            // textBoxIAS
            // 
            this.textBoxIAS.Location = new System.Drawing.Point(69, 19);
            this.textBoxIAS.Name = "textBoxIAS";
            this.textBoxIAS.ReadOnly = true;
            this.textBoxIAS.Size = new System.Drawing.Size(58, 20);
            this.textBoxIAS.TabIndex = 19;
            // 
            // labelNose
            // 
            this.labelNose.AutoSize = true;
            this.labelNose.Location = new System.Drawing.Point(6, 125);
            this.labelNose.Name = "labelNose";
            this.labelNose.Size = new System.Drawing.Size(59, 13);
            this.labelNose.TabIndex = 17;
            this.labelNose.Text = "Nosegear: ";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(13, 66);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(43, 13);
            this.labelTitle.TabIndex = 18;
            this.labelTitle.Text = "Plane:  ";
            // 
            // textBoxSetHDG
            // 
            this.textBoxSetHDG.Location = new System.Drawing.Point(81, 23);
            this.textBoxSetHDG.MaxLength = 3;
            this.textBoxSetHDG.Name = "textBoxSetHDG";
            this.textBoxSetHDG.Size = new System.Drawing.Size(68, 20);
            this.textBoxSetHDG.TabIndex = 29;
            this.textBoxSetHDG.Text = "000";
            // 
            // buttonSet
            // 
            this.buttonSet.Location = new System.Drawing.Point(6, 17);
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.Size = new System.Drawing.Size(68, 26);
            this.buttonSet.TabIndex = 30;
            this.buttonSet.Text = "Set HDG";
            this.buttonSet.UseVisualStyleBackColor = true;
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // groupToggle
            // 
            this.groupToggle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupToggle.Controls.Add(this.buttonFlaps);
            this.groupToggle.Controls.Add(this.buttonLights);
            this.groupToggle.Controls.Add(this.buttonAP);
            this.groupToggle.Location = new System.Drawing.Point(338, 97);
            this.groupToggle.Name = "groupToggle";
            this.groupToggle.Size = new System.Drawing.Size(155, 114);
            this.groupToggle.TabIndex = 31;
            this.groupToggle.TabStop = false;
            this.groupToggle.Text = "Toggle (on/off)";
            // 
            // groupSet
            // 
            this.groupSet.BackColor = System.Drawing.SystemColors.Info;
            this.groupSet.Controls.Add(this.buttonCOM);
            this.groupSet.Controls.Add(this.textBoxSetCom);
            this.groupSet.Controls.Add(this.buttonSetSpd);
            this.groupSet.Controls.Add(this.textBoxSetSpd);
            this.groupSet.Controls.Add(this.buttonSet);
            this.groupSet.Controls.Add(this.textBoxSetHDG);
            this.groupSet.Location = new System.Drawing.Point(338, 228);
            this.groupSet.Name = "groupSet";
            this.groupSet.Size = new System.Drawing.Size(155, 136);
            this.groupSet.TabIndex = 32;
            this.groupSet.TabStop = false;
            this.groupSet.Text = "Set Data";
            // 
            // buttonSetSpd
            // 
            this.buttonSetSpd.Location = new System.Drawing.Point(6, 49);
            this.buttonSetSpd.Name = "buttonSetSpd";
            this.buttonSetSpd.Size = new System.Drawing.Size(68, 26);
            this.buttonSetSpd.TabIndex = 32;
            this.buttonSetSpd.Text = "Set SPD";
            this.buttonSetSpd.UseVisualStyleBackColor = true;
            this.buttonSetSpd.Click += new System.EventHandler(this.buttonSetSpd_Click);
            // 
            // textBoxSetSpd
            // 
            this.textBoxSetSpd.Location = new System.Drawing.Point(81, 53);
            this.textBoxSetSpd.MaxLength = 3;
            this.textBoxSetSpd.Name = "textBoxSetSpd";
            this.textBoxSetSpd.Size = new System.Drawing.Size(68, 20);
            this.textBoxSetSpd.TabIndex = 31;
            this.textBoxSetSpd.Text = "000";
            // 
            // buttonCOM
            // 
            this.buttonCOM.Location = new System.Drawing.Point(6, 81);
            this.buttonCOM.Name = "buttonCOM";
            this.buttonCOM.Size = new System.Drawing.Size(68, 26);
            this.buttonCOM.TabIndex = 34;
            this.buttonCOM.Text = "Set COM";
            this.buttonCOM.UseVisualStyleBackColor = true;
            this.buttonCOM.Click += new System.EventHandler(this.buttonCOM_Click);
            // 
            // textBoxSetCom
            // 
            this.textBoxSetCom.Location = new System.Drawing.Point(81, 85);
            this.textBoxSetCom.MaxLength = 6;
            this.textBoxSetCom.Multiline = true;
            this.textBoxSetCom.Name = "textBoxSetCom";
            this.textBoxSetCom.Size = new System.Drawing.Size(68, 20);
            this.textBoxSetCom.TabIndex = 33;
            this.textBoxSetCom.Text = "123,45";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 407);
            this.Controls.Add(this.groupSet);
            this.Controls.Add(this.groupToggle);
            this.Controls.Add(this.groupRead);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.buttonConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupRead.ResumeLayout(false);
            this.groupRead.PerformLayout();
            this.groupToggle.ResumeLayout(false);
            this.groupSet.ResumeLayout(false);
            this.groupSet.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonFlaps;
        private System.Windows.Forms.Button buttonAP;
        private System.Windows.Forms.Button buttonLights;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label labelAlt;
        private System.Windows.Forms.Label labelCom;
        private System.Windows.Forms.Label labelFlaps;
        private System.Windows.Forms.GroupBox groupRead;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelNose;
        private System.Windows.Forms.TextBox textBoxAlt;
        private System.Windows.Forms.TextBox textBoxCom;
        private System.Windows.Forms.TextBox textBoxFlaps;
        private System.Windows.Forms.TextBox textBoxNose;
        private System.Windows.Forms.TextBox textBoxLeft;
        private System.Windows.Forms.TextBox textBoxIAS;
        private System.Windows.Forms.Label labelRight;
        private System.Windows.Forms.TextBox textBoxRight;
        private System.Windows.Forms.Label labelLeft;
        private System.Windows.Forms.Label labelAutoP;
        private System.Windows.Forms.TextBox textBoxHDG;
        private System.Windows.Forms.TextBox textBoxSetHDG;
        private System.Windows.Forms.Button buttonSet;
        private System.Windows.Forms.GroupBox groupToggle;
        private System.Windows.Forms.GroupBox groupSet;
        private System.Windows.Forms.Button buttonSetSpd;
        private System.Windows.Forms.TextBox textBoxSetSpd;
        private System.Windows.Forms.Button buttonCOM;
        private System.Windows.Forms.TextBox textBoxSetCom;
    }
}

