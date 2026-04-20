namespace MemTestLogExtracotr
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTarget = new System.Windows.Forms.TextBox();
            this.buttonAddFolder = new System.Windows.Forms.Button();
            this.buttonAddFiles = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonOpenOutputFile = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.buttonClear = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelMid = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelMid.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-1, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drag the folder, files here or select a path.\r\n";
            // 
            // textBoxTarget
            // 
            this.textBoxTarget.AllowDrop = true;
            this.textBoxTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTarget.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.textBoxTarget.Location = new System.Drawing.Point(0, 0);
            this.textBoxTarget.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxTarget.Multiline = true;
            this.textBoxTarget.Name = "textBoxTarget";
            this.textBoxTarget.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxTarget.Size = new System.Drawing.Size(704, 146);
            this.textBoxTarget.TabIndex = 1;
            this.textBoxTarget.WordWrap = false;
            this.textBoxTarget.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox_DragDropMulti);
            this.textBoxTarget.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextBox_DragEnter);
            // 
            // buttonAddFolder
            // 
            this.buttonAddFolder.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.buttonAddFolder.Location = new System.Drawing.Point(180, 4);
            this.buttonAddFolder.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddFolder.Name = "buttonAddFolder";
            this.buttonAddFolder.Size = new System.Drawing.Size(150, 29);
            this.buttonAddFolder.TabIndex = 3;
            this.buttonAddFolder.Text = "Add Folder (Alt + &F)";
            this.buttonAddFolder.UseVisualStyleBackColor = true;
            this.buttonAddFolder.Click += new System.EventHandler(this.buttonAddFolder_Click);
            // 
            // buttonAddFiles
            // 
            this.buttonAddFiles.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.buttonAddFiles.Location = new System.Drawing.Point(2, 4);
            this.buttonAddFiles.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddFiles.Name = "buttonAddFiles";
            this.buttonAddFiles.Size = new System.Drawing.Size(150, 29);
            this.buttonAddFiles.TabIndex = 2;
            this.buttonAddFiles.Text = "Add Files (Alt + &A)";
            this.buttonAddFiles.UseVisualStyleBackColor = true;
            this.buttonAddFiles.Click += new System.EventHandler(this.buttonAddFiles_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.buttonStart.Location = new System.Drawing.Point(2, 45);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(140, 75);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "Start\r\n(Alt + &S)";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonOpenOutputFile
            // 
            this.buttonOpenOutputFile.Enabled = false;
            this.buttonOpenOutputFile.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.buttonOpenOutputFile.Location = new System.Drawing.Point(150, 45);
            this.buttonOpenOutputFile.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOpenOutputFile.Name = "buttonOpenOutputFile";
            this.buttonOpenOutputFile.Size = new System.Drawing.Size(140, 75);
            this.buttonOpenOutputFile.TabIndex = 5;
            this.buttonOpenOutputFile.Text = "Open Output File\r\n(Alt + &O)";
            this.buttonOpenOutputFile.UseVisualStyleBackColor = true;
            this.buttonOpenOutputFile.Click += new System.EventHandler(this.buttonOpenOutputFile_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelBottom, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelTop, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelMid, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(714, 326);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.buttonClear);
            this.panelBottom.Controls.Add(this.buttonAddFiles);
            this.panelBottom.Controls.Add(this.buttonOpenOutputFile);
            this.panelBottom.Controls.Add(this.buttonStart);
            this.panelBottom.Controls.Add(this.buttonAddFolder);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottom.Location = new System.Drawing.Point(3, 199);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(708, 124);
            this.panelBottom.TabIndex = 2;
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.buttonClear.Location = new System.Drawing.Point(400, 4);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(150, 29);
            this.buttonClear.TabIndex = 6;
            this.buttonClear.Text = "Clear Input Box (Alt + &C)";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTop.Location = new System.Drawing.Point(5, 5);
            this.panelTop.Margin = new System.Windows.Forms.Padding(5);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(704, 30);
            this.panelTop.TabIndex = 3;
            // 
            // panelMid
            // 
            this.panelMid.Controls.Add(this.textBoxTarget);
            this.panelMid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMid.Location = new System.Drawing.Point(5, 45);
            this.panelMid.Margin = new System.Windows.Forms.Padding(5);
            this.panelMid.Name = "panelMid";
            this.panelMid.Size = new System.Drawing.Size(704, 146);
            this.panelMid.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 326);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MemTestLogExtracotr";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMid.ResumeLayout(false);
            this.panelMid.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTarget;
        private System.Windows.Forms.Button buttonAddFolder;
        private System.Windows.Forms.Button buttonAddFiles;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonOpenOutputFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelMid;
        private System.Windows.Forms.Button buttonClear;
    }
}

