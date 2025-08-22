namespace VRCFT_ReverseEyes
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
            this.extensiveLog = new System.Windows.Forms.CheckBox();
            this.outIPTXT = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.outPortTXT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.inPortTXT = new System.Windows.Forms.TextBox();
            this.startBTN = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pimaxFixToggle = new System.Windows.Forms.CheckBox();
            this.delayTxtBX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.delLogsBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // extensiveLog
            // 
            this.extensiveLog.AutoSize = true;
            this.extensiveLog.Location = new System.Drawing.Point(204, 154);
            this.extensiveLog.Name = "extensiveLog";
            this.extensiveLog.Size = new System.Drawing.Size(164, 24);
            this.extensiveLog.TabIndex = 0;
            this.extensiveLog.Text = "Extensive Logging";
            this.extensiveLog.UseVisualStyleBackColor = true;
            this.extensiveLog.CheckedChanged += new System.EventHandler(this.extensiveLog_CheckedChanged);
            // 
            // outIPTXT
            // 
            this.outIPTXT.Location = new System.Drawing.Point(342, 49);
            this.outIPTXT.Name = "outIPTXT";
            this.outIPTXT.Size = new System.Drawing.Size(121, 26);
            this.outIPTXT.TabIndex = 1;
            this.outIPTXT.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(267, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP: ";
            // 
            // outPortTXT
            // 
            this.outPortTXT.Location = new System.Drawing.Point(342, 81);
            this.outPortTXT.Name = "outPortTXT";
            this.outPortTXT.Size = new System.Drawing.Size(62, 26);
            this.outPortTXT.TabIndex = 3;
            this.outPortTXT.Text = "9000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(267, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Port:";
            // 
            // inPortTXT
            // 
            this.inPortTXT.Location = new System.Drawing.Point(92, 49);
            this.inPortTXT.Name = "inPortTXT";
            this.inPortTXT.Size = new System.Drawing.Size(73, 26);
            this.inPortTXT.TabIndex = 5;
            this.inPortTXT.Text = "9111";
            // 
            // startBTN
            // 
            this.startBTN.Location = new System.Drawing.Point(74, 119);
            this.startBTN.Name = "startBTN";
            this.startBTN.Size = new System.Drawing.Size(112, 32);
            this.startBTN.TabIndex = 7;
            this.startBTN.Text = "Start";
            this.startBTN.UseVisualStyleBackColor = true;
            this.startBTN.Click += new System.EventHandler(this.startBTN_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Inbound (Set Out In VRCFT)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(294, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Outbound (To VRC)";
            // 
            // pimaxFixToggle
            // 
            this.pimaxFixToggle.AutoSize = true;
            this.pimaxFixToggle.Checked = true;
            this.pimaxFixToggle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pimaxFixToggle.Location = new System.Drawing.Point(204, 119);
            this.pimaxFixToggle.Name = "pimaxFixToggle";
            this.pimaxFixToggle.Size = new System.Drawing.Size(99, 24);
            this.pimaxFixToggle.TabIndex = 10;
            this.pimaxFixToggle.Text = "Flip Eyes";
            this.pimaxFixToggle.UseVisualStyleBackColor = true;
            this.pimaxFixToggle.CheckedChanged += new System.EventHandler(this.pimaxFixToggle_CheckedChanged);
            // 
            // delayTxtBX
            // 
            this.delayTxtBX.Location = new System.Drawing.Point(229, 190);
            this.delayTxtBX.Name = "delayTxtBX";
            this.delayTxtBX.Size = new System.Drawing.Size(100, 26);
            this.delayTxtBX.TabIndex = 11;
            this.delayTxtBX.Text = "1";
            this.delayTxtBX.TextChanged += new System.EventHandler(this.delayTxtBX_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(136, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Delay(ms):";
            // 
            // delLogsBTN
            // 
            this.delLogsBTN.Location = new System.Drawing.Point(15, 184);
            this.delLogsBTN.Name = "delLogsBTN";
            this.delLogsBTN.Size = new System.Drawing.Size(112, 32);
            this.delLogsBTN.TabIndex = 13;
            this.delLogsBTN.Text = "Delete Logs";
            this.delLogsBTN.UseVisualStyleBackColor = true;
            this.delLogsBTN.Click += new System.EventHandler(this.delLogsBTN_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 233);
            this.Controls.Add(this.delLogsBTN);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.delayTxtBX);
            this.Controls.Add(this.pimaxFixToggle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.startBTN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.inPortTXT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.outPortTXT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outIPTXT);
            this.Controls.Add(this.extensiveLog);
            this.Name = "Form1";
            this.Text = "VRCFT Reverse Eyes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox extensiveLog;
        private System.Windows.Forms.TextBox outIPTXT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox outPortTXT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox inPortTXT;
        private System.Windows.Forms.Button startBTN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox pimaxFixToggle;
        private System.Windows.Forms.TextBox delayTxtBX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button delLogsBTN;
    }
}

