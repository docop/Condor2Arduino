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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxBankdeg = new System.Windows.Forms.TextBox();
            this.textBoxCompass = new System.Windows.Forms.TextBox();
            this.textBoxPitch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSpeedkmh = new System.Windows.Forms.TextBox();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.textBoxGforce = new System.Windows.Forms.TextBox();
            this.textBoxEvario = new System.Windows.Forms.TextBox();
            this.textBoxIntegrated = new System.Windows.Forms.TextBox();
            this.textBoxVario = new System.Windows.Forms.TextBox();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.labelEvario = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelGforce = new System.Windows.Forms.Label();
            this.labelIntegrated = new System.Windows.Forms.Label();
            this.labelVario = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.textBoxTimeconv = new System.Windows.Forms.TextBox();
            this.textBoxPitchdeg = new System.Windows.Forms.TextBox();
            this.textBoxBank = new System.Windows.Forms.TextBox();
            this.textBoxSpeed = new System.Windows.Forms.TextBox();
            this.groupBoxSerial = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonZEROdwn = new System.Windows.Forms.Button();
            this.buttonZEROup = new System.Windows.Forms.Button();
            this.textBoxTestData = new System.Windows.Forms.TextBox();
            this.buttonCom = new System.Windows.Forms.Button();
            this.textBoxBaud = new System.Windows.Forms.TextBox();
            this.comboBoxCom = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxSerial.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.btnConnect.Location = new System.Drawing.Point(64, 18);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(107, 20);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxBankdeg);
            this.groupBox2.Controls.Add(this.textBoxCompass);
            this.groupBox2.Controls.Add(this.textBoxPitch);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxSpeedkmh);
            this.groupBox2.Controls.Add(this.textBoxTime);
            this.groupBox2.Controls.Add(this.textBoxGforce);
            this.groupBox2.Controls.Add(this.textBoxEvario);
            this.groupBox2.Controls.Add(this.textBoxIntegrated);
            this.groupBox2.Controls.Add(this.textBoxVario);
            this.groupBox2.Controls.Add(this.textBoxHeight);
            this.groupBox2.Controls.Add(this.labelEvario);
            this.groupBox2.Controls.Add(this.labelTime);
            this.groupBox2.Controls.Add(this.labelGforce);
            this.groupBox2.Controls.Add(this.labelIntegrated);
            this.groupBox2.Controls.Add(this.labelVario);
            this.groupBox2.Controls.Add(this.labelHeight);
            this.groupBox2.Controls.Add(this.labelSpeed);
            this.groupBox2.Location = new System.Drawing.Point(301, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(366, 314);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Conversie";
            // 
            // textBoxBankdeg
            // 
            this.textBoxBankdeg.Location = new System.Drawing.Point(139, 190);
            this.textBoxBankdeg.Name = "textBoxBankdeg";
            this.textBoxBankdeg.Size = new System.Drawing.Size(70, 20);
            this.textBoxBankdeg.TabIndex = 22;
            // 
            // textBoxCompass
            // 
            this.textBoxCompass.Location = new System.Drawing.Point(139, 242);
            this.textBoxCompass.Name = "textBoxCompass";
            this.textBoxCompass.Size = new System.Drawing.Size(70, 20);
            this.textBoxCompass.TabIndex = 21;
            // 
            // textBoxPitch
            // 
            this.textBoxPitch.Location = new System.Drawing.Point(139, 216);
            this.textBoxPitch.Name = "textBoxPitch";
            this.textBoxPitch.Size = new System.Drawing.Size(70, 20);
            this.textBoxPitch.TabIndex = 20;
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
            // textBoxSpeedkmh
            // 
            this.textBoxSpeedkmh.Location = new System.Drawing.Point(139, 33);
            this.textBoxSpeedkmh.Name = "textBoxSpeedkmh";
            this.textBoxSpeedkmh.Size = new System.Drawing.Size(70, 20);
            this.textBoxSpeedkmh.TabIndex = 14;
            // 
            // textBoxTime
            // 
            this.textBoxTime.Location = new System.Drawing.Point(139, 268);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.Size = new System.Drawing.Size(70, 20);
            this.textBoxTime.TabIndex = 13;
            // 
            // textBoxGforce
            // 
            this.textBoxGforce.Location = new System.Drawing.Point(139, 163);
            this.textBoxGforce.Name = "textBoxGforce";
            this.textBoxGforce.Size = new System.Drawing.Size(70, 20);
            this.textBoxGforce.TabIndex = 12;
            // 
            // textBoxEvario
            // 
            this.textBoxEvario.Location = new System.Drawing.Point(139, 137);
            this.textBoxEvario.Name = "textBoxEvario";
            this.textBoxEvario.Size = new System.Drawing.Size(70, 20);
            this.textBoxEvario.TabIndex = 11;
            // 
            // textBoxIntegrated
            // 
            this.textBoxIntegrated.Location = new System.Drawing.Point(139, 111);
            this.textBoxIntegrated.Name = "textBoxIntegrated";
            this.textBoxIntegrated.Size = new System.Drawing.Size(70, 20);
            this.textBoxIntegrated.TabIndex = 10;
            // 
            // textBoxVario
            // 
            this.textBoxVario.Location = new System.Drawing.Point(139, 85);
            this.textBoxVario.Name = "textBoxVario";
            this.textBoxVario.Size = new System.Drawing.Size(70, 20);
            this.textBoxVario.TabIndex = 9;
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(139, 59);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(70, 20);
            this.textBoxHeight.TabIndex = 8;
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
            this.labelTime.Size = new System.Drawing.Size(30, 13);
            this.labelTime.TabIndex = 5;
            this.labelTime.Text = "Time";
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
            // textBoxTimeconv
            // 
            this.textBoxTimeconv.Location = new System.Drawing.Point(117, 207);
            this.textBoxTimeconv.Name = "textBoxTimeconv";
            this.textBoxTimeconv.Size = new System.Drawing.Size(70, 20);
            this.textBoxTimeconv.TabIndex = 25;
            // 
            // textBoxPitchdeg
            // 
            this.textBoxPitchdeg.Location = new System.Drawing.Point(117, 233);
            this.textBoxPitchdeg.Name = "textBoxPitchdeg";
            this.textBoxPitchdeg.Size = new System.Drawing.Size(70, 20);
            this.textBoxPitchdeg.TabIndex = 23;
            // 
            // textBoxBank
            // 
            this.textBoxBank.Location = new System.Drawing.Point(41, 207);
            this.textBoxBank.Name = "textBoxBank";
            this.textBoxBank.Size = new System.Drawing.Size(70, 20);
            this.textBoxBank.TabIndex = 19;
            // 
            // textBoxSpeed
            // 
            this.textBoxSpeed.Location = new System.Drawing.Point(41, 233);
            this.textBoxSpeed.Name = "textBoxSpeed";
            this.textBoxSpeed.Size = new System.Drawing.Size(70, 20);
            this.textBoxSpeed.TabIndex = 7;
            // 
            // groupBoxSerial
            // 
            this.groupBoxSerial.Controls.Add(this.textBoxPitchdeg);
            this.groupBoxSerial.Controls.Add(this.textBoxTimeconv);
            this.groupBoxSerial.Controls.Add(this.groupBox3);
            this.groupBoxSerial.Controls.Add(this.textBoxTestData);
            this.groupBoxSerial.Controls.Add(this.buttonCom);
            this.groupBoxSerial.Controls.Add(this.textBoxBaud);
            this.groupBoxSerial.Controls.Add(this.comboBoxCom);
            this.groupBoxSerial.Controls.Add(this.textBoxBank);
            this.groupBoxSerial.Controls.Add(this.textBoxSpeed);
            this.groupBoxSerial.Location = new System.Drawing.Point(301, 320);
            this.groupBoxSerial.Name = "groupBoxSerial";
            this.groupBoxSerial.Size = new System.Drawing.Size(365, 273);
            this.groupBoxSerial.TabIndex = 6;
            this.groupBoxSerial.TabStop = false;
            this.groupBoxSerial.Text = "Serial Connect";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonZEROdwn);
            this.groupBox3.Controls.Add(this.buttonZEROup);
            this.groupBox3.Location = new System.Drawing.Point(218, 66);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(117, 103);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Homing Speed";
            // 
            // buttonZEROdwn
            // 
            this.buttonZEROdwn.Location = new System.Drawing.Point(21, 64);
            this.buttonZEROdwn.Name = "buttonZEROdwn";
            this.buttonZEROdwn.Size = new System.Drawing.Size(70, 24);
            this.buttonZEROdwn.TabIndex = 7;
            this.buttonZEROdwn.Text = "Stop";
            this.buttonZEROdwn.UseVisualStyleBackColor = true;
            this.buttonZEROdwn.Click += new System.EventHandler(this.buttonZEROdwn_Click);
            // 
            // buttonZEROup
            // 
            this.buttonZEROup.Location = new System.Drawing.Point(21, 34);
            this.buttonZEROup.Name = "buttonZEROup";
            this.buttonZEROup.Size = new System.Drawing.Size(70, 24);
            this.buttonZEROup.TabIndex = 6;
            this.buttonZEROup.Text = "Forward";
            this.buttonZEROup.UseVisualStyleBackColor = true;
            this.buttonZEROup.Click += new System.EventHandler(this.buttonZEROup_Click);
            // 
            // textBoxTestData
            // 
            this.textBoxTestData.Location = new System.Drawing.Point(15, 66);
            this.textBoxTestData.Multiline = true;
            this.textBoxTestData.Name = "textBoxTestData";
            this.textBoxTestData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxTestData.Size = new System.Drawing.Size(194, 58);
            this.textBoxTestData.TabIndex = 3;
            // 
            // buttonCom
            // 
            this.buttonCom.Location = new System.Drawing.Point(243, 29);
            this.buttonCom.Name = "buttonCom";
            this.buttonCom.Size = new System.Drawing.Size(70, 21);
            this.buttonCom.TabIndex = 2;
            this.buttonCom.Text = "Connect";
            this.buttonCom.UseVisualStyleBackColor = true;
            this.buttonCom.Click += new System.EventHandler(this.buttonCom_Click);
            // 
            // textBoxBaud
            // 
            this.textBoxBaud.Location = new System.Drawing.Point(139, 29);
            this.textBoxBaud.Name = "textBoxBaud";
            this.textBoxBaud.Size = new System.Drawing.Size(70, 20);
            this.textBoxBaud.TabIndex = 1;
            this.textBoxBaud.Text = "9600";
            // 
            // comboBoxCom
            // 
            this.comboBoxCom.FormattingEnabled = true;
            this.comboBoxCom.Location = new System.Drawing.Point(15, 29);
            this.comboBoxCom.Name = "comboBoxCom";
            this.comboBoxCom.Size = new System.Drawing.Size(96, 21);
            this.comboBoxCom.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 644);
            this.Controls.Add(this.groupBoxSerial);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "C2A Interface";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxSerial.ResumeLayout(false);
            this.groupBoxSerial.PerformLayout();
            this.groupBox3.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox textBoxTime;
        private System.Windows.Forms.TextBox textBoxGforce;
        private System.Windows.Forms.TextBox textBoxEvario;
        private System.Windows.Forms.TextBox textBoxIntegrated;
        private System.Windows.Forms.TextBox textBoxVario;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.TextBox textBoxSpeed;
        private System.Windows.Forms.Label labelEvario;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.TextBox textBoxSpeedkmh;
        private System.Windows.Forms.TextBox textBoxPitchdeg;
        private System.Windows.Forms.TextBox textBoxBankdeg;
        private System.Windows.Forms.TextBox textBoxCompass;
        private System.Windows.Forms.TextBox textBoxPitch;
        private System.Windows.Forms.TextBox textBoxBank;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTimeconv;
        private System.Windows.Forms.GroupBox groupBoxSerial;
        private System.Windows.Forms.ComboBox comboBoxCom;
        private System.Windows.Forms.TextBox textBoxTestData;
        private System.Windows.Forms.Button buttonCom;
        private System.Windows.Forms.TextBox textBoxBaud;
        private System.Windows.Forms.Button buttonZEROdwn;
        private System.Windows.Forms.Button buttonZEROup;
        public System.Windows.Forms.Timer timer1;
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

