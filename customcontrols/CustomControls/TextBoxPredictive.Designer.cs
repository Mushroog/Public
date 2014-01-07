namespace CustomControls
{
    partial class TextBoxPredictive
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TextBoxPredictive
            // 
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBoxPredictive_MouseClick);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TextBoxPredictive_PreviewKeyDown);
            this.Leave += new System.EventHandler(this.TextBoxPredictive_Leave);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxPredictive_KeyPress);
            this.TextChanged += new System.EventHandler(this.TextBoxPredictive_TextChanged);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
