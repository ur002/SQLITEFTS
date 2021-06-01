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
            this.bUPDHASH = new System.Windows.Forms.ToolStripDropDownButton();
            this.pntp = new System.Windows.Forms.Panel();
            this.chUPDContentIfHashDiff = new System.Windows.Forms.CheckBox();
            this.lpath = new System.Windows.Forms.Label();
            this.lmask = new System.Windows.Forms.Label();
            this.chdatafromDB = new System.Windows.Forms.CheckBox();
            this.txtText4SEarch = new System.Windows.Forms.TextBox();
            this.lfilrs = new System.Windows.Forms.ListBox();
            this.pnplace = new System.Windows.Forms.Panel();
            this.pntext = new System.Windows.Forms.Panel();
            this.pnmanipul = new System.Windows.Forms.Panel();
            this.bnext = new System.Windows.Forms.Button();
            this.bprev = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.splitter = new System.Windows.Forms.Splitter();
            this.statusStrip1.SuspendLayout();
            this.pntp.SuspendLayout();
            this.pnplace.SuspendLayout();
            this.pnmanipul.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtpath4src
            // 
            this.txtpath4src.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtpath4src.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txtpath4src.Location = new System.Drawing.Point(121, 13);
            this.txtpath4src.Name = "txtpath4src";
            this.txtpath4src.Size = new System.Drawing.Size(561, 17);
            this.txtpath4src.TabIndex = 0;
            this.txtpath4src.Text = "path for search";
            // 
            // txtmask
            // 
            this.txtmask.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtmask.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txtmask.Location = new System.Drawing.Point(121, 36);
            this.txtmask.Name = "txtmask";
            this.txtmask.Size = new System.Drawing.Size(76, 17);
            this.txtmask.TabIndex = 1;
            this.txtmask.Text = "*.*";
            this.txtmask.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtText4SEarch_KeyDown);
            // 
            // badd
            // 
            this.badd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.badd.Location = new System.Drawing.Point(688, 9);
            this.badd.Name = "badd";
            this.badd.Size = new System.Drawing.Size(75, 23);
            this.badd.TabIndex = 2;
            this.badd.Text = "Добавить";
            this.badd.UseVisualStyleBackColor = true;
            this.badd.Click += new System.EventHandler(this.badd_Click);
            // 
            // rlog
            // 
            this.rlog.BackColor = System.Drawing.SystemColors.Control;
            this.rlog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rlog.Cursor = System.Windows.Forms.Cursors.Help;
            this.rlog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rlog.Location = new System.Drawing.Point(0, 483);
            this.rlog.Multiline = true;
            this.rlog.Name = "rlog";
            this.rlog.ReadOnly = true;
            this.rlog.Size = new System.Drawing.Size(1237, 20);
            this.rlog.TabIndex = 3;
            this.rlog.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.rlog_MouseDoubleClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainstatuslabel,
            this.pb1,
            this.bUPDHASH});
            this.statusStrip1.Location = new System.Drawing.Point(0, 503);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1237, 22);
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
            // bUPDHASH
            // 
            this.bUPDHASH.BackgroundImage = global::SQLITEFTS.Properties.Resources.document_hash_tag;
            this.bUPDHASH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bUPDHASH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bUPDHASH.Image = global::SQLITEFTS.Properties.Resources.document_hash_tag;
            this.bUPDHASH.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bUPDHASH.Name = "bUPDHASH";
            this.bUPDHASH.Size = new System.Drawing.Size(29, 20);
            this.bUPDHASH.Text = "toolStripDropDownButton1";
            this.bUPDHASH.Click += new System.EventHandler(this.bUPDHASH_Click);
            // 
            // pntp
            // 
            this.pntp.Controls.Add(this.chUPDContentIfHashDiff);
            this.pntp.Controls.Add(this.lpath);
            this.pntp.Controls.Add(this.lmask);
            this.pntp.Controls.Add(this.chdatafromDB);
            this.pntp.Controls.Add(this.txtmask);
            this.pntp.Controls.Add(this.txtpath4src);
            this.pntp.Controls.Add(this.badd);
            this.pntp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pntp.Location = new System.Drawing.Point(0, 0);
            this.pntp.Name = "pntp";
            this.pntp.Size = new System.Drawing.Size(1237, 63);
            this.pntp.TabIndex = 5;
            // 
            // chUPDContentIfHashDiff
            // 
            this.chUPDContentIfHashDiff.AutoSize = true;
            this.chUPDContentIfHashDiff.Checked = true;
            this.chUPDContentIfHashDiff.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chUPDContentIfHashDiff.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chUPDContentIfHashDiff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chUPDContentIfHashDiff.Location = new System.Drawing.Point(768, 11);
            this.chUPDContentIfHashDiff.Name = "chUPDContentIfHashDiff";
            this.chUPDContentIfHashDiff.Size = new System.Drawing.Size(331, 21);
            this.chUPDContentIfHashDiff.TabIndex = 8;
            this.chUPDContentIfHashDiff.Text = "Обновлять содержимое, если оно изменилось";
            this.chUPDContentIfHashDiff.UseVisualStyleBackColor = true;
            // 
            // lpath
            // 
            this.lpath.AutoSize = true;
            this.lpath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lpath.Location = new System.Drawing.Point(2, 12);
            this.lpath.Name = "lpath";
            this.lpath.Size = new System.Drawing.Size(117, 17);
            this.lpath.TabIndex = 6;
            this.lpath.Text = "Путь для поиска";
            // 
            // lmask
            // 
            this.lmask.AutoSize = true;
            this.lmask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lmask.Location = new System.Drawing.Point(16, 35);
            this.lmask.Name = "lmask";
            this.lmask.Size = new System.Drawing.Size(103, 17);
            this.lmask.TabIndex = 5;
            this.lmask.Text = "Маска файлов";
            // 
            // chdatafromDB
            // 
            this.chdatafromDB.AutoSize = true;
            this.chdatafromDB.Checked = true;
            this.chdatafromDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chdatafromDB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chdatafromDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chdatafromDB.Location = new System.Drawing.Point(204, 35);
            this.chdatafromDB.Name = "chdatafromDB";
            this.chdatafromDB.Size = new System.Drawing.Size(315, 21);
            this.chdatafromDB.TabIndex = 3;
            this.chdatafromDB.Text = "Подгружать содержимое из БД(V)/из файла";
            this.chdatafromDB.UseVisualStyleBackColor = true;
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
            this.txtText4SEarch.Size = new System.Drawing.Size(1237, 19);
            this.txtText4SEarch.TabIndex = 6;
            this.txtText4SEarch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtText4SEarch_KeyDown);
            // 
            // lfilrs
            // 
            this.lfilrs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lfilrs.Dock = System.Windows.Forms.DockStyle.Top;
            this.lfilrs.FormattingEnabled = true;
            this.lfilrs.Location = new System.Drawing.Point(0, 82);
            this.lfilrs.Name = "lfilrs";
            this.lfilrs.ScrollAlwaysVisible = true;
            this.lfilrs.Size = new System.Drawing.Size(1237, 65);
            this.lfilrs.TabIndex = 7;
            this.lfilrs.SelectedIndexChanged += new System.EventHandler(this.lfilrs_SelectedIndexChanged);
            // 
            // pnplace
            // 
            this.pnplace.Controls.Add(this.pntext);
            this.pnplace.Controls.Add(this.pnmanipul);
            this.pnplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnplace.Location = new System.Drawing.Point(0, 150);
            this.pnplace.Name = "pnplace";
            this.pnplace.Size = new System.Drawing.Size(1237, 333);
            this.pnplace.TabIndex = 10;
            // 
            // pntext
            // 
            this.pntext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pntext.Location = new System.Drawing.Point(0, 24);
            this.pntext.Name = "pntext";
            this.pntext.Size = new System.Drawing.Size(1237, 309);
            this.pntext.TabIndex = 1;
            // 
            // pnmanipul
            // 
            this.pnmanipul.Controls.Add(this.bnext);
            this.pnmanipul.Controls.Add(this.bprev);
            this.pnmanipul.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnmanipul.Location = new System.Drawing.Point(0, 0);
            this.pnmanipul.Name = "pnmanipul";
            this.pnmanipul.Size = new System.Drawing.Size(1237, 24);
            this.pnmanipul.TabIndex = 0;
            // 
            // bnext
            // 
            this.bnext.AutoSize = true;
            this.bnext.Dock = System.Windows.Forms.DockStyle.Left;
            this.bnext.Location = new System.Drawing.Point(32, 0);
            this.bnext.Name = "bnext";
            this.bnext.Size = new System.Drawing.Size(32, 24);
            this.bnext.TabIndex = 1;
            this.bnext.Text = ">";
            this.bnext.UseVisualStyleBackColor = true;
            this.bnext.Click += new System.EventHandler(this.bnext_Click);
            // 
            // bprev
            // 
            this.bprev.AutoSize = true;
            this.bprev.Dock = System.Windows.Forms.DockStyle.Left;
            this.bprev.Location = new System.Drawing.Point(0, 0);
            this.bprev.Name = "bprev";
            this.bprev.Size = new System.Drawing.Size(32, 24);
            this.bprev.TabIndex = 0;
            this.bprev.Text = "<";
            this.bprev.UseVisualStyleBackColor = true;
            this.bprev.Click += new System.EventHandler(this.bprev_Click);
            // 
            // splitter
            // 
            this.splitter.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter.Location = new System.Drawing.Point(0, 147);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(1237, 3);
            this.splitter.TabIndex = 0;
            this.splitter.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 525);
            this.Controls.Add(this.pnplace);
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
            this.pnplace.ResumeLayout(false);
            this.pnmanipul.ResumeLayout(false);
            this.pnmanipul.PerformLayout();
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
        private System.Windows.Forms.Panel pnplace;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.Splitter splitter;
        private System.Windows.Forms.CheckBox chdatafromDB;
        private System.Windows.Forms.Label lmask;
        private System.Windows.Forms.Label lpath;
        private System.Windows.Forms.Panel pnmanipul;
        private System.Windows.Forms.Button bprev;
        private System.Windows.Forms.Button bnext;
        private System.Windows.Forms.Panel pntext;
        private System.Windows.Forms.ToolStripDropDownButton bUPDHASH;
        private System.Windows.Forms.CheckBox chUPDContentIfHashDiff;
    }
}

