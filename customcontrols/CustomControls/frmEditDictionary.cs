using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomControls
{
    /// <summary>
    /// Dialog box for editing custom dictionary entries. 
    /// NB edits are applied as they are made, there is no 
    /// ability to cancel a session of editing
    /// </summary>
    public partial class frmEditDictionary : Form
    {
        private bool autochange = false;

        /// <summary>
        /// class constructor
        /// </summary>
        public frmEditDictionary()
        {
            InitializeComponent();
            ListRefresh();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textStemInput_TextChanged(object sender, EventArgs e)
        {
            ListRefresh();
        }

        private void ListRefresh()
        {
            listStems.Items.Clear();
            listSuggestions.Items.Clear();

            if (!TextBoxPredictive.KeyExistsInCustom("-" + textStemInput.Text))
            {
                string[] custom = TextBoxPredictive.GetCustomEntry(textStemInput.Text);
                string[] prediction = TextBoxPredictive.GetPredictionEntry(textStemInput.Text);

                if (custom != null)
                {
                    textStemInput.BackColor = Color.LightGreen;
                    for (int i = 0; i < custom.Length; i++)
                    {
                        if (custom[i] != null)
                        {
                            listSuggestions.Items.Add(custom[i]);
                        }
                    }
                }
                else if (prediction != null)
                {
                    textStemInput.BackColor = Color.White;
                    for (int i = 0; i < prediction.Length; i++)
                    {
                        if (prediction[i] != null)
                        {
                            listSuggestions.Items.Add(prediction[i]);
                        }
                    }
                }
            }
            else
            {
                textStemInput.BackColor = Color.Pink;
            }

            if (listSuggestions.Items.Count > 0)
            {
                for (int i = 0; i < Math.Min(listSuggestions.Items.Count, 12); i++)
                {
                    listStems.Items.Add(textStemInput.Text);
                }

                Graphics g = textStemInput.CreateGraphics();
                SizeF textSize = g.MeasureString(textStemInput.Text, textStemInput.Font, textStemInput.ClientRectangle.Width);

                listStems.Width = (int)textSize.Width;

                textStem.Width = (int)textSize.Width + 2;
                textStem.Text = textStemInput.Text;

                textNewSugg.Left = textStem.Right + 2;
                
                listSuggestions.Left = listStems.Right;
                listSuggestions.Width = 330 - listStems.Width;
            }
            else
            {
                listStems.Width = 0;
                listSuggestions.Left = listStems.Right;
                listSuggestions.Width = 330;

                Graphics g = textStemInput.CreateGraphics();
                SizeF textSize = g.MeasureString(textStemInput.Text, textStemInput.Font, textStemInput.ClientRectangle.Width);

                textStem.Text = textStemInput.Text;
                textStem.Width = (int)textSize.Width + 2;
                textNewSugg.Left = textStem.Right + 2;
            }
        }

        private void CommitToCustom(bool remove)
        {
            string[] suggs = null;

            string key = textStemInput.Text; 
            
            if (!remove)
            {
                suggs = new string[listSuggestions.Items.Count];
                listSuggestions.Items.CopyTo(suggs, 0);
            }
            else
            {
                key = "-" + key;
            }
            
            TextBoxPredictive.AddCustomEntry(key, suggs);
            ListRefresh();
        }
        
        private void buttonModify_Click(object sender, EventArgs e)
        {
            if (listSuggestions.SelectedIndex >= 0)
            {
                autochange = true;
                listSuggestions.Items.RemoveAt(listSuggestions.SelectedIndex);
                listSuggestions.Items.Add(textNewSugg.Text);
                autochange = false;
            }

            CommitToCustom(false);
        }

        private void listSuggestions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!autochange)
                textNewSugg.Text = listSuggestions.Text;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            RemoveCurrent();
        }

        private void RemoveCurrent()
        {
            if (listSuggestions.SelectedIndex >= 0)
            {
                autochange = true;
                listSuggestions.Items.RemoveAt(listSuggestions.SelectedIndex);
                autochange = false;
                CommitToCustom(false);
            }
        
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            autochange = true;
            listSuggestions.Items.Add(textNewSugg.Text);
            listSuggestions.Text = textNewSugg.Text;
            autochange = false;
            CommitToCustom(false);
        }

        private void buttonRemoveAll_Click(object sender, EventArgs e)
        {
            autochange = true;
            listStems.Items.Clear();
            listSuggestions.Items.Clear();
            autochange = false;

            CommitToCustom(true);
        }

        private void listSuggestions_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveCurrent();
            }
        }
    }
}
