namespace CustomControls
{
    partial class frmEditDictionary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditDictionary));
            this.buttonOK = new System.Windows.Forms.Button();
            this.textStemInput = new System.Windows.Forms.TextBox();
            this.listStems = new System.Windows.Forms.ListBox();
            this.listSuggestions = new System.Windows.Forms.ListBox();
            this.buttonModify = new System.Windows.Forms.Button();
            this.textNewSugg = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.textStem = new System.Windows.Forms.TextBox();
            this.buttonRemoveAll = new System.Windows.Forms.Button();
            this.labelStem = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Location = new System.Drawing.Point(146, 427);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(102, 33);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "Close";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textStemInput
            // 
            this.textStemInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textStemInput.Location = new System.Drawing.Point(31, 33);
            this.textStemInput.Name = "textStemInput";
            this.textStemInput.Size = new System.Drawing.Size(231, 26);
            this.textStemInput.TabIndex = 0;
            this.textStemInput.TextChanged += new System.EventHandler(this.textStemInput_TextChanged);
            // 
            // listStems
            // 
            this.listStems.Enabled = false;
            this.listStems.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listStems.FormattingEnabled = true;
            this.listStems.ItemHeight = 20;
            this.listStems.Location = new System.Drawing.Point(34, 73);
            this.listStems.Name = "listStems";
            this.listStems.Size = new System.Drawing.Size(25, 244);
            this.listStems.TabIndex = 5;
            // 
            // listSuggestions
            // 
            this.listSuggestions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listSuggestions.FormattingEnabled = true;
            this.listSuggestions.ItemHeight = 20;
            this.listSuggestions.Location = new System.Drawing.Point(65, 73);
            this.listSuggestions.Name = "listSuggestions";
            this.listSuggestions.Size = new System.Drawing.Size(296, 244);
            this.listSuggestions.Sorted = true;
            this.listSuggestions.TabIndex = 2;
            this.listSuggestions.SelectedIndexChanged += new System.EventHandler(this.listSuggestions_SelectedIndexChanged);
            this.listSuggestions.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listSuggestions_KeyUp);
            // 
            // buttonModify
            // 
            this.buttonModify.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonModify.Location = new System.Drawing.Point(258, 378);
            this.buttonModify.Name = "buttonModify";
            this.buttonModify.Size = new System.Drawing.Size(102, 33);
            this.buttonModify.TabIndex = 6;
            this.buttonModify.Text = "Modify";
            this.buttonModify.UseVisualStyleBackColor = true;
            this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
            // 
            // textNewSugg
            // 
            this.textNewSugg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNewSugg.Location = new System.Drawing.Point(34, 332);
            this.textNewSugg.Name = "textNewSugg";
            this.textNewSugg.Size = new System.Drawing.Size(327, 26);
            this.textNewSugg.TabIndex = 8;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.Location = new System.Drawing.Point(34, 378);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(102, 33);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRemove.Location = new System.Drawing.Point(146, 378);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(102, 33);
            this.buttonRemove.TabIndex = 5;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // textStem
            // 
            this.textStem.Enabled = false;
            this.textStem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textStem.Location = new System.Drawing.Point(34, 332);
            this.textStem.Name = "textStem";
            this.textStem.Size = new System.Drawing.Size(10, 26);
            this.textStem.TabIndex = 3;
            // 
            // buttonRemoveAll
            // 
            this.buttonRemoveAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRemoveAll.Location = new System.Drawing.Point(268, 30);
            this.buttonRemoveAll.Name = "buttonRemoveAll";
            this.buttonRemoveAll.Size = new System.Drawing.Size(102, 33);
            this.buttonRemoveAll.TabIndex = 1;
            this.buttonRemoveAll.Text = "Remove All";
            this.buttonRemoveAll.UseVisualStyleBackColor = true;
            this.buttonRemoveAll.Click += new System.EventHandler(this.buttonRemoveAll_Click);
            // 
            // labelStem
            // 
            this.labelStem.AutoSize = true;
            this.labelStem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStem.Location = new System.Drawing.Point(31, 10);
            this.labelStem.Name = "labelStem";
            this.labelStem.Size = new System.Drawing.Size(47, 20);
            this.labelStem.TabIndex = 13;
            this.labelStem.Text = "Stem";
            // 
            // frmEditDictionary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 476);
            this.Controls.Add(this.labelStem);
            this.Controls.Add(this.buttonRemoveAll);
            this.Controls.Add(this.textStem);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textNewSugg);
            this.Controls.Add(this.buttonModify);
            this.Controls.Add(this.listSuggestions);
            this.Controls.Add(this.listStems);
            this.Controls.Add(this.textStemInput);
            this.Controls.Add(this.buttonOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEditDictionary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Auto-complete Dictionary Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textStemInput;
        private System.Windows.Forms.ListBox listStems;
        private System.Windows.Forms.ListBox listSuggestions;
        private System.Windows.Forms.Button buttonModify;
        private System.Windows.Forms.TextBox textNewSugg;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.TextBox textStem;
        private System.Windows.Forms.Button buttonRemoveAll;
        private System.Windows.Forms.Label labelStem;
    }
}