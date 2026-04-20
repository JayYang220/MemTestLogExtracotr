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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drag the folder, files here or select a path.\r\n";
            // 
            // textBoxTarget
            // 
            this.textBoxTarget.AllowDrop = true;
            this.textBoxTarget.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.textBoxTarget.Location = new System.Drawing.Point(33, 49);
            this.textBoxTarget.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxTarget.Multiline = true;
            this.textBoxTarget.Name = "textBoxTarget";
            this.textBoxTarget.Size = new System.Drawing.Size(627, 106);
            this.textBoxTarget.TabIndex = 1;
            this.textBoxTarget.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox_DragDropMulti);
            this.textBoxTarget.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextBox_DragEnter);
            // 
            // buttonAddFolder
            // 
            this.buttonAddFolder.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.buttonAddFolder.Location = new System.Drawing.Point(33, 163);
            this.buttonAddFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAddFolder.Name = "buttonAddFolder";
            this.buttonAddFolder.Size = new System.Drawing.Size(88, 29);
            this.buttonAddFolder.TabIndex = 2;
            this.buttonAddFolder.Text = "Add Folder";
            this.buttonAddFolder.UseVisualStyleBackColor = true;
            this.buttonAddFolder.Click += new System.EventHandler(this.buttonAddFolder_Click);
            // 
            // buttonAddFiles
            // 
            this.buttonAddFiles.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.buttonAddFiles.Location = new System.Drawing.Point(149, 163);
            this.buttonAddFiles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAddFiles.Name = "buttonAddFiles";
            this.buttonAddFiles.Size = new System.Drawing.Size(88, 29);
            this.buttonAddFiles.TabIndex = 3;
            this.buttonAddFiles.Text = "Add Files";
            this.buttonAddFiles.UseVisualStyleBackColor = true;
            this.buttonAddFiles.Click += new System.EventHandler(this.buttonAddFiles_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.buttonStart.Location = new System.Drawing.Point(33, 208);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(140, 75);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonOpenOutputFile
            // 
            this.buttonOpenOutputFile.Enabled = false;
            this.buttonOpenOutputFile.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.buttonOpenOutputFile.Location = new System.Drawing.Point(196, 208);
            this.buttonOpenOutputFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonOpenOutputFile.Name = "buttonOpenOutputFile";
            this.buttonOpenOutputFile.Size = new System.Drawing.Size(140, 75);
            this.buttonOpenOutputFile.TabIndex = 5;
            this.buttonOpenOutputFile.Text = "Open Output File";
            this.buttonOpenOutputFile.UseVisualStyleBackColor = true;
            this.buttonOpenOutputFile.Click += new System.EventHandler(this.buttonOpenOutputFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 326);
            this.Controls.Add(this.buttonOpenOutputFile);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonAddFiles);
            this.Controls.Add(this.buttonAddFolder);
            this.Controls.Add(this.textBoxTarget);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "MemTestLogExtracotr";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTarget;
        private System.Windows.Forms.Button buttonAddFolder;
        private System.Windows.Forms.Button buttonAddFiles;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonOpenOutputFile;
    }
}

