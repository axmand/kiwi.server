﻿namespace Engine.NLP.Forms
{
    partial class NLPConfigForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.StanfordNLP_Server_Port_textBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.StanfordNLP_Server_Url_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.OK_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GloVe_File_button = new System.Windows.Forms.Button();
            this.GloVe_File_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.BaiduAI_AppId_textBox = new System.Windows.Forms.TextBox();
            this.BaiduAI_APIKey_textBox = new System.Windows.Forms.TextBox();
            this.BaiduAI_Secret_Key_textBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.StanfordNLP_Server_Port_textBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.StanfordNLP_Server_Url_textBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(831, 207);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stanford NLP Core";
            // 
            // StanfordNLP_Server_Port_textBox
            // 
            this.StanfordNLP_Server_Port_textBox.Location = new System.Drawing.Point(252, 126);
            this.StanfordNLP_Server_Port_textBox.Name = "StanfordNLP_Server_Port_textBox";
            this.StanfordNLP_Server_Port_textBox.Size = new System.Drawing.Size(494, 28);
            this.StanfordNLP_Server_Port_textBox.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(233, 18);
            this.label5.TabIndex = 3;
            this.label5.Text = "Stanford NLP Server Port:";
            // 
            // StanfordNLP_Server_Url_textBox
            // 
            this.StanfordNLP_Server_Url_textBox.Location = new System.Drawing.Point(252, 45);
            this.StanfordNLP_Server_Url_textBox.Name = "StanfordNLP_Server_Url_textBox";
            this.StanfordNLP_Server_Url_textBox.Size = new System.Drawing.Size(494, 28);
            this.StanfordNLP_Server_Url_textBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stanford NLP Server:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.OK_button);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.GloVe_File_button);
            this.groupBox2.Controls.Add(this.GloVe_File_textBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 426);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(831, 285);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Words Emebdding";
            // 
            // OK_button
            // 
            this.OK_button.Location = new System.Drawing.Point(665, 231);
            this.OK_button.Name = "OK_button";
            this.OK_button.Size = new System.Drawing.Size(84, 30);
            this.OK_button.TabIndex = 8;
            this.OK_button.Text = "保存";
            this.OK_button.UseVisualStyleBackColor = true;
            this.OK_button.Click += new System.EventHandler(this.OK_button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(172, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "[可选]:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(663, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 30);
            this.button1.TabIndex = 6;
            this.button1.Text = "设置";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(252, 153);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(405, 28);
            this.textBox2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "训练语料地址";
            // 
            // GloVe_File_button
            // 
            this.GloVe_File_button.Location = new System.Drawing.Point(663, 71);
            this.GloVe_File_button.Name = "GloVe_File_button";
            this.GloVe_File_button.Size = new System.Drawing.Size(84, 30);
            this.GloVe_File_button.TabIndex = 3;
            this.GloVe_File_button.Text = "设置";
            this.GloVe_File_button.UseVisualStyleBackColor = true;
            // 
            // GloVe_File_textBox
            // 
            this.GloVe_File_textBox.Location = new System.Drawing.Point(252, 72);
            this.GloVe_File_textBox.Name = "GloVe_File_textBox";
            this.GloVe_File_textBox.Size = new System.Drawing.Size(405, 28);
            this.GloVe_File_textBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "GloVe文件地址:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BaiduAI_Secret_Key_textBox);
            this.groupBox3.Controls.Add(this.BaiduAI_APIKey_textBox);
            this.groupBox3.Controls.Add(this.BaiduAI_AppId_textBox);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 207);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(831, 219);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "BaiduAI NLP";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(115, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 18);
            this.label6.TabIndex = 0;
            this.label6.Text = "BaiduAI AppId:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(106, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 18);
            this.label7.TabIndex = 1;
            this.label7.Text = "BaiduAI APIKey:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(71, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(179, 18);
            this.label8.TabIndex = 2;
            this.label8.Text = "BaiduAI Secret Key:";
            // 
            // BaiduAI_AppId_textBox
            // 
            this.BaiduAI_AppId_textBox.Location = new System.Drawing.Point(253, 41);
            this.BaiduAI_AppId_textBox.Name = "BaiduAI_AppId_textBox";
            this.BaiduAI_AppId_textBox.Size = new System.Drawing.Size(494, 28);
            this.BaiduAI_AppId_textBox.TabIndex = 3;
            // 
            // BaiduAI_APIKey_textBox
            // 
            this.BaiduAI_APIKey_textBox.Location = new System.Drawing.Point(253, 101);
            this.BaiduAI_APIKey_textBox.Name = "BaiduAI_APIKey_textBox";
            this.BaiduAI_APIKey_textBox.Size = new System.Drawing.Size(494, 28);
            this.BaiduAI_APIKey_textBox.TabIndex = 4;
            // 
            // BaiduAI_Secret_Key_textBox
            // 
            this.BaiduAI_Secret_Key_textBox.Location = new System.Drawing.Point(253, 163);
            this.BaiduAI_Secret_Key_textBox.Name = "BaiduAI_Secret_Key_textBox";
            this.BaiduAI_Secret_Key_textBox.Size = new System.Drawing.Size(494, 28);
            this.BaiduAI_Secret_Key_textBox.TabIndex = 5;
            // 
            // NLPConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(831, 711);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "NLPConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NLPConfigForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox StanfordNLP_Server_Url_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox GloVe_File_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button GloVe_File_button;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox StanfordNLP_Server_Port_textBox;
        private System.Windows.Forms.Button OK_button;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox BaiduAI_Secret_Key_textBox;
        private System.Windows.Forms.TextBox BaiduAI_APIKey_textBox;
        private System.Windows.Forms.TextBox BaiduAI_AppId_textBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}