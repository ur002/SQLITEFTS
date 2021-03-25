namespace SQLITEFTS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtpath4src = new System.Windows.Forms.TextBox();
            this.txtmask = new System.Windows.Forms.TextBox();
            this.badd = new System.Windows.Forms.Button();
            this.rlog = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mainstatuslabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pb1 = new System.Windows.Forms.ToolStripProgressBar();
            this.pntp = new System.Windows.Forms.Panel();
            this.txtText4SEarch = new System.Windows.Forms.TextBox();
            this.lfilrs = new System.Windows.Forms.ListBox();
            this.faTabStripItem1 = new FarsiLibrary.Win.FATabStripItem();
            this.pntext = new System.Windows.Forms.Panel();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.splitter = new System.Windows.Forms.Splitter();
            this.chdatafromDB = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.pntp.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtpath4src
            // 
            this.txtpath4src.Location = new System.Drawing.Point(12, 9);
            this.txtpath4src.Name = "txtpath4src";
            this.txtpath4src.Size = new System.Drawing.Size(642, 20);
            this.txtpath4src.TabIndex = 0;
            this.txtpath4src.Text = "\\\\192.168.65.11\\c2\\DS\\DS_hw\\";
            // 
            // txtmask
            // 
            this.txtmask.Location = new System.Drawing.Point(12, 35);
            this.txtmask.Name = "txtmask";
            this.txtmask.Size = new System.Drawing.Size(167, 20);
            this.txtmask.TabIndex = 1;
            this.txtmask.Text = "*.py; *.ipynb";
            // 
            // badd
            // 
            this.badd.Location = new System.Drawing.Point(657, 8);
            this.badd.Name = "badd";
            this.badd.Size = new System.Drawing.Size(75, 23);
            this.badd.TabIndex = 2;
            this.badd.Text = "add2index";
            this.badd.UseVisualStyleBackColor = true;
            this.badd.Click += new System.EventHandler(this.badd_Click);
            // 
            // rlog
            // 
            this.rlog.BackColor = System.Drawing.SystemColors.Control;
            this.rlog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rlog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rlog.Location = new System.Drawing.Point(0, 483);
            this.rlog.Multiline = true;
            this.rlog.Name = "rlog";
            this.rlog.Size = new System.Drawing.Size(1019, 20);
            this.rlog.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainstatuslabel,
            this.pb1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 503);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1019, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mainstatuslabel
            // 
            this.mainstatuslabel.Name = "mainstatuslabel";
            this.mainstatuslabel.Size = new System.Drawing.Size(13, 17);
            this.mainstatuslabel.Text = "S";
            // 
            // pb1
            // 
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(100, 16);
            this.pb1.Visible = false;
            // 
            // pntp
            // 
            this.pntp.Controls.Add(this.chdatafromDB);
            this.pntp.Controls.Add(this.txtmask);
            this.pntp.Controls.Add(this.txtpath4src);
            this.pntp.Controls.Add(this.badd);
            this.pntp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pntp.Location = new System.Drawing.Point(0, 0);
            this.pntp.Name = "pntp";
            this.pntp.Size = new System.Drawing.Size(1019, 63);
            this.pntp.TabIndex = 5;
            // 
            // txtText4SEarch
            // 
            this.txtText4SEarch.BackColor = System.Drawing.SystemColors.Info;
            this.txtText4SEarch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtText4SEarch.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtText4SEarch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtText4SEarch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtText4SEarch.Location = new System.Drawing.Point(0, 63);
            this.txtText4SEarch.Name = "txtText4SEarch";
            this.txtText4SEarch.Size = new System.Drawing.Size(1019, 19);
            this.txtText4SEarch.TabIndex = 6;
            this.txtText4SEarch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtText4SEarch_KeyDown);
            // 
            // lfilrs
            // 
            this.lfilrs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lfilrs.Dock = System.Windows.Forms.DockStyle.Top;
            this.lfilrs.FormattingEnabled = true;
            this.lfilrs.Location = new System.Drawing.Point(0, 82);
            this.lfilrs.Name = "lfilrs";
            this.lfilrs.ScrollAlwaysVisible = true;
            this.lfilrs.Size = new System.Drawing.Size(1019, 67);
            this.lfilrs.TabIndex = 7;
            this.lfilrs.SelectedIndexChanged += new System.EventHandler(this.lfilrs_SelectedIndexChanged);
            // 
            // faTabStripItem1
            // 
            this.faTabStripItem1.IsDrawn = true;
            this.faTabStripItem1.Name = "faTabStripItem1";
            this.faTabStripItem1.Selected = true;
            this.faTabStripItem1.Size = new System.Drawing.Size(798, 142);
            this.faTabStripItem1.TabIndex = 0;
            this.faTabStripItem1.Title = "TabStrip Page 1";
            // 
            // pntext
            // 
            this.pntext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pntext.Location = new System.Drawing.Point(0, 152);
            this.pntext.Name = "pntext";
            this.pntext.Size = new System.Drawing.Size(1019, 331);
            this.pntext.TabIndex = 10;
            // 
            // splitter
            // 
            this.splitter.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter.Location = new System.Drawing.Point(0, 149);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(1019, 3);
            this.splitter.TabIndex = 0;
            this.splitter.TabStop = false;
            // 
            // chdatafromDB
            // 
            this.chdatafromDB.AutoSize = true;
            this.chdatafromDB.Checked = true;
            this.chdatafromDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chdatafromDB.Location = new System.Drawing.Point(186, 38);
            this.chdatafromDB.Name = "chdatafromDB";
            this.chdatafromDB.Size = new System.Drawing.Size(253, 17);
            this.chdatafromDB.TabIndex = 3;
            this.chdatafromDB.Text = "Подгружать содержимое из БД(V)/из файла";
            this.chdatafromDB.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 525);
            this.Controls.Add(this.pntext);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.lfilrs);
            this.Controls.Add(this.txtText4SEarch);
            this.Controls.Add(this.pntp);
            this.Controls.Add(this.rlog);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "FTS5 Engine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pntp.ResumeLayout(false);
            this.pntp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtpath4src;
        private System.Windows.Forms.TextBox txtmask;
        private System.Windows.Forms.Button badd;
        private System.Windows.Forms.TextBox rlog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel mainstatuslabel;
        private System.Windows.Forms.Panel pntp;
        private System.Windows.Forms.TextBox txtText4SEarch;
        private System.Windows.Forms.ListBox lfilrs;
        private System.Windows.Forms.ToolStripProgressBar pb1;
        private FarsiLibrary.Win.FATabStripItem faTabStripItem1;
        private System.Windows.Forms.Panel pntext;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.Splitter splitter;
        private System.Windows.Forms.CheckBox chdatafromDB;
    }
}

