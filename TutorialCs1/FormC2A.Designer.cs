namespace Condor2Arduino
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
            this.textBoxPortCondor = new System.Windows.Forms.TextBox();
            this.btnConnectCondor = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxSerial = new System.Windows.Forms.GroupBox();
            this.buttonCom = new System.Windows.Forms.Button();
            this.textBoxBaud = new System.Windows.Forms.TextBox();
            this.comboBoxCom = new System.Windows.Forms.ComboBox();
            this.groupBoxConversie = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDecodeBank = new System.Windows.Forms.TextBox();
            this.textBoxDecodeCompass = new System.Windows.Forms.TextBox();
            this.textBoxDecodePitch = new System.Windows.Forms.TextBox();
            this.textboxDecodeSpeed = new System.Windows.Forms.TextBox();
            this.textBoxDecodeGforce = new System.Windows.Forms.TextBox();
            this.textBoxDecodevarElec = new System.Windows.Forms.TextBox();
            this.textBoxDecodeVarint = new System.Windows.Forms.TextBox();
            this.textBoxDecodeVarraw = new System.Windows.Forms.TextBox();
            this.textBoxDecodeAlt = new System.Windows.Forms.TextBox();
            this.labelCompass = new System.Windows.Forms.Label();
            this.labelPitch = new System.Windows.Forms.Label();
            this.labelBank = new System.Windows.Forms.Label();
            this.textBoxyawstring = new System.Windows.Forms.TextBox();
            this.labelVarioE = new System.Windows.Forms.Label();
            this.labelYawstring = new System.Windows.Forms.Label();
            this.labelGforce = new System.Windows.Forms.Label();
            this.labelVarioI = new System.Windows.Forms.Label();
            this.labelVarioR = new System.Windows.Forms.Label();
            this.labelAltitude = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.comboBoxComPort = new System.Windows.Forms.ComboBox();
            this.BtnConnectArduino = new System.Windows.Forms.Button();
            this.comboBoxBaudrate = new System.Windows.Forms.ComboBox();
            this.labelStatusArduino = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBoxSerial.SuspendLayout();
            this.groupBoxConversie.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxPortCondor
            // 
            this.textBoxPortCondor.Location = new System.Drawing.Point(6, 19);
            this.textBoxPortCondor.Name = "textBoxPortCondor";
            this.textBoxPortCondor.Size = new System.Drawing.Size(52, 20);
            this.textBoxPortCondor.TabIndex = 0;
            this.textBoxPortCondor.Text = "55278";
            // 
            // btnConnectCondor
            // 
            this.btnConnectCondor.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConnectCondor.Location = new System.Drawing.Point(64, 19);
            this.btnConnectCondor.Name = "btnConnectCondor";
            this.btnConnectCondor.Size = new System.Drawing.Size(85, 19);
            this.btnConnectCondor.TabIndex = 1;
            this.btnConnectCondor.Text = "Connect";
            this.btnConnectCondor.UseVisualStyleBackColor = true;
            this.btnConnectCondor.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // textBox2
            // 
            this.textBox2.AcceptsReturn = true;
            this.textBox2.Location = new System.Drawing.Point(6, 45);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(273, 587);
            this.textBox2.TabIndex = 2;
            this.textBox2.WordWrap = false;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(155, 22);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(37, 13);
            this.labelStatus.TabIndex = 3;
            this.labelStatus.Text = "Status";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBoxSerial);
            this.groupBox1.Controls.Add(this.textBoxPortCondor);
            this.groupBox1.Controls.Add(this.labelStatus);
            this.groupBox1.Controls.Add(this.btnConnectCondor);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Location = new System.Drawing.Point(4, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 638);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Condor data";
            // 
            // groupBoxSerial
            // 
            this.groupBoxSerial.Controls.Add(this.buttonCom);
            this.groupBoxSerial.Controls.Add(this.textBoxBaud);
            this.groupBoxSerial.Controls.Add(this.comboBoxCom);
            this.groupBoxSerial.Location = new System.Drawing.Point(285, 551);
            this.groupBoxSerial.Name = "groupBoxSerial";
            this.groupBoxSerial.Size = new System.Drawing.Size(365, 79);
            this.groupBoxSerial.TabIndex = 6;
            this.groupBoxSerial.TabStop = false;
            this.groupBoxSerial.Text = "Serial Connect";
            // 
            // buttonCom
            // 
            this.buttonCom.Location = new System.Drawing.Point(273, 29);
            this.buttonCom.Name = "buttonCom";
            this.buttonCom.Size = new System.Drawing.Size(70, 21);
            this.buttonCom.TabIndex = 2;
            this.buttonCom.Text = "Connect";
            this.buttonCom.UseVisualStyleBackColor = true;
            this.buttonCom.Click += new System.EventHandler(this.buttonCom_Click);
            // 
            // textBoxBaud
            // 
            this.textBoxBaud.Location = new System.Drawing.Point(180, 29);
            this.textBoxBaud.Name = "textBoxBaud";
            this.textBoxBaud.Size = new System.Drawing.Size(70, 20);
            this.textBoxBaud.TabIndex = 1;
            this.textBoxBaud.Text = "19200";
            // 
            // comboBoxCom
            // 
            this.comboBoxCom.FormattingEnabled = true;
            this.comboBoxCom.Location = new System.Drawing.Point(15, 29);
            this.comboBoxCom.Name = "comboBoxCom";
            this.comboBoxCom.Size = new System.Drawing.Size(96, 21);
            this.comboBoxCom.TabIndex = 0;
            // 
            // groupBoxConversie
            // 
            this.groupBoxConversie.Controls.Add(this.label2);
            this.groupBoxConversie.Controls.Add(this.textBoxDecodeBank);
            this.groupBoxConversie.Controls.Add(this.textBoxDecodeCompass);
            this.groupBoxConversie.Controls.Add(this.textBoxDecodePitch);
            this.groupBoxConversie.Controls.Add(this.textboxDecodeSpeed);
            this.groupBoxConversie.Controls.Add(this.textBoxDecodeGforce);
            this.groupBoxConversie.Controls.Add(this.textBoxDecodevarElec);
            this.groupBoxConversie.Controls.Add(this.textBoxDecodeVarint);
            this.groupBoxConversie.Controls.Add(this.textBoxDecodeVarraw);
            this.groupBoxConversie.Controls.Add(this.textBoxDecodeAlt);
            this.groupBoxConversie.Controls.Add(this.labelCompass);
            this.groupBoxConversie.Controls.Add(this.labelPitch);
            this.groupBoxConversie.Controls.Add(this.labelBank);
            this.groupBoxConversie.Controls.Add(this.textBoxyawstring);
            this.groupBoxConversie.Controls.Add(this.labelVarioE);
            this.groupBoxConversie.Controls.Add(this.labelYawstring);
            this.groupBoxConversie.Controls.Add(this.labelGforce);
            this.groupBoxConversie.Controls.Add(this.labelVarioI);
            this.groupBoxConversie.Controls.Add(this.labelVarioR);
            this.groupBoxConversie.Controls.Add(this.labelAltitude);
            this.groupBoxConversie.Controls.Add(this.labelSpeed);
            this.groupBoxConversie.Location = new System.Drawing.Point(304, 2);
            this.groupBoxConversie.Name = "groupBoxConversie";
            this.groupBoxConversie.Size = new System.Drawing.Size(250, 347);
            this.groupBoxConversie.TabIndex = 5;
            this.groupBoxConversie.TabStop = false;
            this.groupBoxConversie.Text = "Conversie";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(99, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Decoded Bytes";
            // 
            // textBoxDecodeBank
            // 
            this.textBoxDecodeBank.Location = new System.Drawing.Point(102, 227);
            this.textBoxDecodeBank.Name = "textBoxDecodeBank";
            this.textBoxDecodeBank.Size = new System.Drawing.Size(70, 20);
            this.textBoxDecodeBank.TabIndex = 32;
            // 
            // textBoxDecodeCompass
            // 
            this.textBoxDecodeCompass.Location = new System.Drawing.Point(102, 279);
            this.textBoxDecodeCompass.Name = "textBoxDecodeCompass";
            this.textBoxDecodeCompass.Size = new System.Drawing.Size(70, 20);
            this.textBoxDecodeCompass.TabIndex = 31;
            // 
            // textBoxDecodePitch
            // 
            this.textBoxDecodePitch.Location = new System.Drawing.Point(102, 253);
            this.textBoxDecodePitch.Name = "textBoxDecodePitch";
            this.textBoxDecodePitch.Size = new System.Drawing.Size(70, 20);
            this.textBoxDecodePitch.TabIndex = 30;
            // 
            // textboxDecodeSpeed
            // 
            this.textboxDecodeSpeed.Location = new System.Drawing.Point(102, 70);
            this.textboxDecodeSpeed.Name = "textboxDecodeSpeed";
            this.textboxDecodeSpeed.Size = new System.Drawing.Size(70, 20);
            this.textboxDecodeSpeed.TabIndex = 29;
            // 
            // textBoxDecodeGforce
            // 
            this.textBoxDecodeGforce.Location = new System.Drawing.Point(102, 200);
            this.textBoxDecodeGforce.Name = "textBoxDecodeGforce";
            this.textBoxDecodeGforce.Size = new System.Drawing.Size(70, 20);
            this.textBoxDecodeGforce.TabIndex = 27;
            // 
            // textBoxDecodevarElec
            // 
            this.textBoxDecodevarElec.Location = new System.Drawing.Point(102, 174);
            this.textBoxDecodevarElec.Name = "textBoxDecodevarElec";
            this.textBoxDecodevarElec.Size = new System.Drawing.Size(70, 20);
            this.textBoxDecodevarElec.TabIndex = 26;
            // 
            // textBoxDecodeVarint
            // 
            this.textBoxDecodeVarint.Location = new System.Drawing.Point(102, 148);
            this.textBoxDecodeVarint.Name = "textBoxDecodeVarint";
            this.textBoxDecodeVarint.Size = new System.Drawing.Size(70, 20);
            this.textBoxDecodeVarint.TabIndex = 25;
            // 
            // textBoxDecodeVarraw
            // 
            this.textBoxDecodeVarraw.Location = new System.Drawing.Point(102, 122);
            this.textBoxDecodeVarraw.Name = "textBoxDecodeVarraw";
            this.textBoxDecodeVarraw.Size = new System.Drawing.Size(70, 20);
            this.textBoxDecodeVarraw.TabIndex = 24;
            // 
            // textBoxDecodeAlt
            // 
            this.textBoxDecodeAlt.Location = new System.Drawing.Point(102, 96);
            this.textBoxDecodeAlt.Name = "textBoxDecodeAlt";
            this.textBoxDecodeAlt.Size = new System.Drawing.Size(70, 20);
            this.textBoxDecodeAlt.TabIndex = 23;
            // 
            // labelCompass
            // 
            this.labelCompass.AutoSize = true;
            this.labelCompass.Location = new System.Drawing.Point(6, 282);
            this.labelCompass.Name = "labelCompass";
            this.labelCompass.Size = new System.Drawing.Size(50, 13);
            this.labelCompass.TabIndex = 18;
            this.labelCompass.Text = "Compass";
            // 
            // labelPitch
            // 
            this.labelPitch.AutoSize = true;
            this.labelPitch.Location = new System.Drawing.Point(6, 256);
            this.labelPitch.Name = "labelPitch";
            this.labelPitch.Size = new System.Drawing.Size(31, 13);
            this.labelPitch.TabIndex = 17;
            this.labelPitch.Text = "Pitch";
            // 
            // labelBank
            // 
            this.labelBank.AutoSize = true;
            this.labelBank.Location = new System.Drawing.Point(6, 230);
            this.labelBank.Name = "labelBank";
            this.labelBank.Size = new System.Drawing.Size(32, 13);
            this.labelBank.TabIndex = 16;
            this.labelBank.Text = "Bank";
            // 
            // textBoxyawstring
            // 
            this.textBoxyawstring.Location = new System.Drawing.Point(102, 305);
            this.textBoxyawstring.Name = "textBoxyawstring";
            this.textBoxyawstring.Size = new System.Drawing.Size(70, 20);
            this.textBoxyawstring.TabIndex = 13;
            // 
            // labelVarioE
            // 
            this.labelVarioE.AutoSize = true;
            this.labelVarioE.Location = new System.Drawing.Point(6, 177);
            this.labelVarioE.Name = "labelVarioE";
            this.labelVarioE.Size = new System.Drawing.Size(80, 13);
            this.labelVarioE.TabIndex = 6;
            this.labelVarioE.Text = "Vario electronic";
            // 
            // labelYawstring
            // 
            this.labelYawstring.AutoSize = true;
            this.labelYawstring.Location = new System.Drawing.Point(6, 308);
            this.labelYawstring.Name = "labelYawstring";
            this.labelYawstring.Size = new System.Drawing.Size(79, 13);
            this.labelYawstring.TabIndex = 5;
            this.labelYawstring.Text = "Yawstringangle";
            // 
            // labelGforce
            // 
            this.labelGforce.AutoSize = true;
            this.labelGforce.Location = new System.Drawing.Point(6, 203);
            this.labelGforce.Name = "labelGforce";
            this.labelGforce.Size = new System.Drawing.Size(42, 13);
            this.labelGforce.TabIndex = 4;
            this.labelGforce.Text = "G-force";
            // 
            // labelVarioI
            // 
            this.labelVarioI.AutoSize = true;
            this.labelVarioI.Location = new System.Drawing.Point(6, 151);
            this.labelVarioI.Name = "labelVarioI";
            this.labelVarioI.Size = new System.Drawing.Size(82, 13);
            this.labelVarioI.TabIndex = 3;
            this.labelVarioI.Text = "Vario Integrated";
            // 
            // labelVarioR
            // 
            this.labelVarioR.AutoSize = true;
            this.labelVarioR.Location = new System.Drawing.Point(6, 125);
            this.labelVarioR.Name = "labelVarioR";
            this.labelVarioR.Size = new System.Drawing.Size(84, 13);
            this.labelVarioR.TabIndex = 2;
            this.labelVarioR.Text = "Vario Pneumatic";
            // 
            // labelAltitude
            // 
            this.labelAltitude.AutoSize = true;
            this.labelAltitude.Location = new System.Drawing.Point(6, 99);
            this.labelAltitude.Name = "labelAltitude";
            this.labelAltitude.Size = new System.Drawing.Size(42, 13);
            this.labelAltitude.TabIndex = 1;
            this.labelAltitude.Text = "Altitude";
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(6, 73);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(38, 13);
            this.labelSpeed.TabIndex = 0;
            this.labelSpeed.Text = "Speed";
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // comboBoxComPort
            // 
            this.comboBoxComPort.FormattingEnabled = true;
            this.comboBoxComPort.Location = new System.Drawing.Point(304, 376);
            this.comboBoxComPort.Name = "comboBoxComPort";
            this.comboBoxComPort.Size = new System.Drawing.Size(59, 21);
            this.comboBoxComPort.TabIndex = 6;
            // 
            // BtnConnectArduino
            // 
            this.BtnConnectArduino.Location = new System.Drawing.Point(465, 375);
            this.BtnConnectArduino.Name = "BtnConnectArduino";
            this.BtnConnectArduino.Size = new System.Drawing.Size(88, 21);
            this.BtnConnectArduino.TabIndex = 7;
            this.BtnConnectArduino.Text = "Connect";
            this.BtnConnectArduino.UseVisualStyleBackColor = true;
            this.BtnConnectArduino.Click += new System.EventHandler(this.buttonCom_Click);
            // 
            // comboBoxBaudrate
            // 
            this.comboBoxBaudrate.FormattingEnabled = true;
            this.comboBoxBaudrate.Items.AddRange(new object[] {
            "19200",
            "9600"});
            this.comboBoxBaudrate.Location = new System.Drawing.Point(369, 376);
            this.comboBoxBaudrate.Name = "comboBoxBaudrate";
            this.comboBoxBaudrate.Size = new System.Drawing.Size(87, 21);
            this.comboBoxBaudrate.TabIndex = 8;
            // 
            // labelStatusArduino
            // 
            this.labelStatusArduino.AutoSize = true;
            this.labelStatusArduino.Location = new System.Drawing.Point(301, 410);
            this.labelStatusArduino.Name = "labelStatusArduino";
            this.labelStatusArduino.Size = new System.Drawing.Size(38, 13);
            this.labelStatusArduino.TabIndex = 9;
            this.labelStatusArduino.Text = "status:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 644);
            this.Controls.Add(this.labelStatusArduino);
            this.Controls.Add(this.comboBoxBaudrate);
            this.Controls.Add(this.BtnConnectArduino);
            this.Controls.Add(this.comboBoxComPort);
            this.Controls.Add(this.groupBoxConversie);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "C2A Interface";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxSerial.ResumeLayout(false);
            this.groupBoxSerial.PerformLayout();
            this.groupBoxConversie.ResumeLayout(false);
            this.groupBoxConversie.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPortCondor;
        private System.Windows.Forms.Button btnConnectCondor;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxConversie;
        private System.Windows.Forms.Label labelGforce;
        private System.Windows.Forms.Label labelVarioI;
        private System.Windows.Forms.Label labelVarioR;
        private System.Windows.Forms.Label labelAltitude;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.TextBox textBoxyawstring;
        private System.Windows.Forms.Label labelVarioE;
        private System.Windows.Forms.Label labelYawstring;
        private System.Windows.Forms.Label labelCompass;
        private System.Windows.Forms.Label labelPitch;
        private System.Windows.Forms.Label labelBank;
        private System.Windows.Forms.GroupBox groupBoxSerial;
        private System.Windows.Forms.ComboBox comboBoxCom;
        private System.Windows.Forms.Button buttonCom;
        private System.Windows.Forms.TextBox textBoxBaud;
        public System.Windows.Forms.Timer timer1;
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.TextBox textBoxDecodeBank;
        private System.Windows.Forms.TextBox textBoxDecodeCompass;
        private System.Windows.Forms.TextBox textBoxDecodePitch;
        private System.Windows.Forms.TextBox textboxDecodeSpeed;
        private System.Windows.Forms.TextBox textBoxDecodeGforce;
        private System.Windows.Forms.TextBox textBoxDecodevarElec;
        private System.Windows.Forms.TextBox textBoxDecodeVarint;
        private System.Windows.Forms.TextBox textBoxDecodeVarraw;
        private System.Windows.Forms.TextBox textBoxDecodeAlt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxComPort;
        private System.Windows.Forms.Button BtnConnectArduino;
        private System.Windows.Forms.ComboBox comboBoxBaudrate;
        private System.Windows.Forms.Label labelStatusArduino;
    }
}

