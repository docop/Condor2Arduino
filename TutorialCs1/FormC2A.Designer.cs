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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxSerial = new System.Windows.Forms.GroupBox();
            this.buttonCom = new System.Windows.Forms.Button();
            this.textBoxBaud = new System.Windows.Forms.TextBox();
            this.comboBoxCom = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxyawstring = new System.Windows.Forms.TextBox();
            this.labelEvario = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelGforce = new System.Windows.Forms.Label();
            this.labelIntegrated = new System.Windows.Forms.Label();
            this.labelVario = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Button_Arduino = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBoxSerial.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(52, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "55278";
            // 
            // btnConnect
            // 
            this.btnConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConnect.Location = new System.Drawing.Point(64, 14);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(107, 24);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(188, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Status";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBoxSerial);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnConnect);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBoxDecodeBank);
            this.groupBox2.Controls.Add(this.textBoxDecodeCompass);
            this.groupBox2.Controls.Add(this.textBoxDecodePitch);
            this.groupBox2.Controls.Add(this.textboxDecodeSpeed);
            this.groupBox2.Controls.Add(this.textBoxDecodeGforce);
            this.groupBox2.Controls.Add(this.textBoxDecodevarElec);
            this.groupBox2.Controls.Add(this.textBoxDecodeVarint);
            this.groupBox2.Controls.Add(this.textBoxDecodeVarraw);
            this.groupBox2.Controls.Add(this.textBoxDecodeAlt);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxyawstring);
            this.groupBox2.Controls.Add(this.labelEvario);
            this.groupBox2.Controls.Add(this.labelTime);
            this.groupBox2.Controls.Add(this.labelGforce);
            this.groupBox2.Controls.Add(this.labelIntegrated);
            this.groupBox2.Controls.Add(this.labelVario);
            this.groupBox2.Controls.Add(this.labelHeight);
            this.groupBox2.Controls.Add(this.labelSpeed);
            this.groupBox2.Location = new System.Drawing.Point(304, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 312);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Conversie";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Decoded Bytes";
            // 
            // textBoxDecodeBank
            // 
            this.textBoxDecodeBank.Location = new System.Drawing.Point(113, 190);
            this.textBoxDecodeBank.Name = "textBoxDecodeBank";
            this.textBoxDecodeBank.Size = new System.Drawing.Size(70, 20);
            this.textBoxDecodeBank.TabIndex = 32;
            // 
            // textBoxDecodeCompass
            // 
            this.textBoxDecodeCompass.Location = new System.Drawing.Point(113, 242);
            this.textBoxDecodeCompass.Name = "textBoxDecodeCompass";
            this.textBoxDecodeCompass.Size = new System.Drawing.Size(70, 20);
            this.textBoxDecodeCompass.TabIndex = 31;
            // 
            // textBoxDecodePitch
            // 
            this.textBoxDecodePitch.Location = new System.Drawing.Point(113, 216);
            this.textBoxDecodePitch.Name = "textBoxDecodePitch";
            this.textBoxDecodePitch.Size = new System.Drawing.Size(70, 20);
            this.textBoxDecodePitch.TabIndex = 30;
            // 
            // textboxDecodeSpeed
            // 
            this.textboxDecodeSpeed.Location = new System.Drawing.Point(113, 33);
            this.textboxDecodeSpeed.Name = "textboxDecodeSpeed";
            this.textboxDecodeSpeed.Size = new System.Drawing.Size(70, 20);
            this.textboxDecodeSpeed.TabIndex = 29;
            // 
            // textBoxDecodeGforce
            // 
            this.textBoxDecodeGforce.Location = new System.Drawing.Point(113, 163);
            this.textBoxDecodeGforce.Name = "textBoxDecodeGforce";
            this.textBoxDecodeGforce.Size = new System.Drawing.Size(70, 20);
            this.textBoxDecodeGforce.TabIndex = 27;
            // 
            // textBoxDecodevarElec
            // 
            this.textBoxDecodevarElec.Location = new System.Drawing.Point(113, 137);
            this.textBoxDecodevarElec.Name = "textBoxDecodevarElec";
            this.textBoxDecodevarElec.Size = new System.Drawing.Size(70, 20);
            this.textBoxDecodevarElec.TabIndex = 26;
            // 
            // textBoxDecodeVarint
            // 
            this.textBoxDecodeVarint.Location = new System.Drawing.Point(113, 111);
            this.textBoxDecodeVarint.Name = "textBoxDecodeVarint";
            this.textBoxDecodeVarint.Size = new System.Drawing.Size(70, 20);
            this.textBoxDecodeVarint.TabIndex = 25;
            // 
            // textBoxDecodeVarraw
            // 
            this.textBoxDecodeVarraw.Location = new System.Drawing.Point(113, 85);
            this.textBoxDecodeVarraw.Name = "textBoxDecodeVarraw";
            this.textBoxDecodeVarraw.Size = new System.Drawing.Size(70, 20);
            this.textBoxDecodeVarraw.TabIndex = 24;
            // 
            // textBoxDecodeAlt
            // 
            this.textBoxDecodeAlt.Location = new System.Drawing.Point(113, 59);
            this.textBoxDecodeAlt.Name = "textBoxDecodeAlt";
            this.textBoxDecodeAlt.Size = new System.Drawing.Size(70, 20);
            this.textBoxDecodeAlt.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 245);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Compass";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Pitch";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Bank";
            // 
            // textBoxyawstring
            // 
            this.textBoxyawstring.Location = new System.Drawing.Point(113, 268);
            this.textBoxyawstring.Name = "textBoxyawstring";
            this.textBoxyawstring.Size = new System.Drawing.Size(70, 20);
            this.textBoxyawstring.TabIndex = 13;
            // 
            // labelEvario
            // 
            this.labelEvario.AutoSize = true;
            this.labelEvario.Location = new System.Drawing.Point(17, 140);
            this.labelEvario.Name = "labelEvario";
            this.labelEvario.Size = new System.Drawing.Size(80, 13);
            this.labelEvario.TabIndex = 6;
            this.labelEvario.Text = "Vario electronic";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(17, 268);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(79, 13);
            this.labelTime.TabIndex = 5;
            this.labelTime.Text = "Yawstringangle";
            // 
            // labelGforce
            // 
            this.labelGforce.AutoSize = true;
            this.labelGforce.Location = new System.Drawing.Point(17, 166);
            this.labelGforce.Name = "labelGforce";
            this.labelGforce.Size = new System.Drawing.Size(42, 13);
            this.labelGforce.TabIndex = 4;
            this.labelGforce.Text = "G-force";
            // 
            // labelIntegrated
            // 
            this.labelIntegrated.AutoSize = true;
            this.labelIntegrated.Location = new System.Drawing.Point(17, 114);
            this.labelIntegrated.Name = "labelIntegrated";
            this.labelIntegrated.Size = new System.Drawing.Size(82, 13);
            this.labelIntegrated.TabIndex = 3;
            this.labelIntegrated.Text = "Vario Integrated";
            // 
            // labelVario
            // 
            this.labelVario.AutoSize = true;
            this.labelVario.Location = new System.Drawing.Point(17, 88);
            this.labelVario.Name = "labelVario";
            this.labelVario.Size = new System.Drawing.Size(84, 13);
            this.labelVario.TabIndex = 2;
            this.labelVario.Text = "Vario Pneumatic";
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(17, 62);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(42, 13);
            this.labelHeight.TabIndex = 1;
            this.labelHeight.Text = "Altitude";
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(17, 36);
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
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(304, 375);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(59, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // Button_Arduino
            // 
            this.Button_Arduino.Location = new System.Drawing.Point(465, 375);
            this.Button_Arduino.Name = "Button_Arduino";
            this.Button_Arduino.Size = new System.Drawing.Size(88, 21);
            this.Button_Arduino.TabIndex = 7;
            this.Button_Arduino.Text = "Connect";
            this.Button_Arduino.UseVisualStyleBackColor = true;
            this.Button_Arduino.Click += new System.EventHandler(this.buttonCom_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "19200",
            "9600"});
            this.comboBox2.Location = new System.Drawing.Point(369, 376);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(87, 21);
            this.comboBox2.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 644);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.Button_Arduino);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "C2A Interface";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxSerial.ResumeLayout(false);
            this.groupBoxSerial.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelGforce;
        private System.Windows.Forms.Label labelIntegrated;
        private System.Windows.Forms.Label labelVario;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.TextBox textBoxyawstring;
        private System.Windows.Forms.Label labelEvario;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
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
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button Button_Arduino;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}

