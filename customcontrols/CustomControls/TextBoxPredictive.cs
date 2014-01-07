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
    /// Extends RichTextBox to provide predictive text.</summary>
    /// <remarks>
    /// Predictive text suggestions are taken from a working dictionary. This is created from
    /// a raw dicitonary, made by processing words in text passages passed to AddToDictionary(),
    /// overridden by any entries in a custom dictionary, created by calling AddCustomEntry()
    /// </remarks>
    public partial class TextBoxPredictive : RichTextBox
    {
        // auto-complete support
        private static SortedList<string, string[]> dictionary = null;
        private static SortedList<string, string[]> custom_dictionary = null;
        /// <summary>
        /// CustomDictionary </summary>
        /// <value>
        /// The dictionary of customised stem-suffix[] pairs, which override the ones in the generated dictionary</value>        
        public static SortedList<string, string[]> CustomDictionary
        {
            get
            {
                if (custom_dictionary == null)
                { return new SortedList<string, string[]>(); }
                else
                { return custom_dictionary; }
            }
        }

        private static SortedList<string, string[]> raw_dictionary = null;

        private bool suggest_active = false;
        private int suggest_idx = 0;
        private int suggest_idx_max = 0;
        private string suggest_txt = "";
        private string suggest_stem = "";
        private string suggest_following = "";
        private string suggest_preceding = "";
        private bool autochange = false;

        private string text_back = "";

        private bool dictionary_active = true;
        /// <summary>
        /// Set to false to turn off predictive text. 
        /// </summary>
        public bool PredictionsOn
        {
            get { return dictionary_active; }
            set { dictionary_active = value; }
        }

        private bool make_backup = true;
        /// <summary>
        /// If true, each time the text is changed by keypress, a copy is stored, which can be copied to the
        /// Text by calling RestorePreviousText
        /// </summary>
        public bool MakeBackup
        {
            get { return make_backup; }
            set { make_backup = value; }
        }

        private static char[] trim_chars = new char[] { ':', ';', '\r', '\n', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '*', '(', ')', '\'', '\"', '\\', '/' };

        private bool use_altered_fonts = false;
        /// <summary>
        /// If true, suggestion suffixes are shown in bold if there is only one suggestion,
        /// or italic if there are many. (Use arrow up/down to go through alternatives.)
        /// </summary>
        public bool UseBoldAndItalic
        {
            get { return use_altered_fonts; }
            set { use_altered_fonts = value; }
        }

        private static int word_stem_min_length = 3;
        /// <summary>
        /// bla
        /// </summary>
        public static int WordStemMinLength
        {
            get { return word_stem_min_length; }
            set { word_stem_min_length = value; }
        }

        private static int word_stem_max_length = 8;
        /// <summary>
        /// bla
        /// </summary>
        public static int WordStemMaxLength
        {
            get { return word_stem_max_length; }
            set { word_stem_max_length = value; }
        }

        private static int word_min_length = 5;
        /// <summary>
        /// bla
        /// </summary>
        public static int WordMinLength
        {
            get { return word_min_length; }
            set { word_min_length = value; }
        }

        /// <summary>
        /// Number of stem entries in working dictionary. 
        /// NB One word generates more than one entry, eg
        /// Sha!rpest
        /// Shar!pest
        /// Sharp!est
        /// </summary>
        public static int DictionaryEntries
        {
            get {
                if (dictionary == null)
                    return 0;
                else
                    return dictionary.Count;
            }
        }

        /// <summary>
        /// Entries added to the custom dictionary override that stem in the raw dictionary.
        /// Prefix with a hyphen to prevent a stem from generating a suggestion.
        /// Note that "-sha" will mask all words beginning "sha" in the suggestions,
        /// but not "shar" or "sharp"
        /// </summary>
        /// <param name="key">stem</param>
        /// <param name="value">array of suffixes (suggestions)</param>
        /// <returns></returns>
        public static bool AddCustomEntry(string key, string[] value)
        {
            if (key != "")
            {
                if (custom_dictionary == null)
                {
                    custom_dictionary = new SortedList<string, string[]>();
                }

                if (custom_dictionary.ContainsKey(key))
                {
                    custom_dictionary.Remove(key);
                }
                
                if (custom_dictionary.ContainsKey("-" + key))
                {
                    custom_dictionary.Remove("-" + key);
                }

                custom_dictionary.Add(key, value);
                return true;
            }
            return false;
        }

        /// <summary>
        /// returns suffixes for a given stem, or null if stem is not in custom dictionary.
        /// NB "-sha" and "sha" are distinct keys
        /// </summary>
        /// <param name="key">stem</param>
        /// <returns>suffixes</returns>
        public static string[] GetCustomEntry(string key)
        {
            return GetEntry(key, true);
        }

        /// <summary>
        /// returns suffixes for a given stem, or null if stem is not in working dictionary
        /// </summary>
        /// <param name="key">stem</param>
        /// <returns>suffixes</returns>
        public static string[] GetPredictionEntry(string key)
        {
            return GetEntry(key, false);
        }

        private static string[] GetEntry(string key, bool use_custom)
        {
            SortedList<string, string[]> dict = (use_custom) ? custom_dictionary: dictionary;
            string[] retval = null;

            if (HasEntries(dict))
            {
                if (dict.ContainsKey(key))
                    dict.TryGetValue(key, out retval);
            }

            return retval;
        }

        /// <summary>
        /// Determines whether a stem exists in the custom dictionary
        /// NB "-sha" and "sha" are distinct keys
        /// </summary>
        /// <param name="key">stem</param>
        public static bool KeyExistsInCustom(string key)
        { 
            bool exists = false;

            if (HasEntries(custom_dictionary))
            {
                if (custom_dictionary.ContainsKey(key))
                {
                    exists = true;
                }
            }

            return exists;
        }

        /// <summary>
        /// Empties the working dictionary
        /// May be safely called if already empty.
        /// </summary>
        public static void ClearDictionary()
        {
            if (dictionary == null)
            {
                dictionary = new SortedList<string, string[]>();
            }
            else
            {
                dictionary.Clear();
            }
        }

        /// <summary>
        /// Empties the custom dictionary,
        /// May be safely called if already empty.
        /// </summary>
        public static void ClearCustomDictionary()
        {
            if (custom_dictionary == null)
            {
                custom_dictionary = new SortedList<string, string[]>();
            }
            else
            {
                custom_dictionary.Clear();
            }
        }

        /// <summary>
        /// class constructor
        /// </summary>
        public TextBoxPredictive()
        {
            InitializeComponent();
        }

        private static void SetDictionary(SortedList<string, string[]> dict)
        {
            if (dictionary != null)
            {
                dictionary.Clear();
            }
            else
            {
                dictionary = new SortedList<string, string[]>();
            }

            foreach (KeyValuePair<string, string[]> kvp in dict)
            {
                string k = kvp.Key;
                string[] v = kvp.Value;
                string[] newv = new string[v.Length];
                for (int i = 0; i < v.Length; i++)
                {
                    newv[i] = v[i];
                }
                dictionary.Add(k, newv);                
            }
        }

        /// <summary>
        /// Creates a copy of the raw dictionary, and applies the entries to the custom 
        /// dictionary over it. No predictions are available until this has been called.
        /// </summary>
        public static void MakePredictionDictionary()
        {
            if (HasEntries(custom_dictionary) && !HasEntries(raw_dictionary))
            {
                SetDictionary(custom_dictionary);
            }
            else
            {
                SetDictionary(raw_dictionary);
                if (HasEntries(custom_dictionary))
                {
                    foreach (KeyValuePair<string, string[]> kvp in custom_dictionary)
                    {
                        string k = kvp.Key;

                        if (k[0] == '-')
                        {
                            k = k.Substring(1);
                            if (dictionary.ContainsKey(k))
                            {
                                dictionary.Remove(k);
                            }
                        }
                        else
                        {
                            if (dictionary.ContainsKey(k))
                            {
                                dictionary.Remove(k);
                            }

                            string[] v = kvp.Value;
                            string[] newv = new string[v.Length];
                            for (int i = 0; i < v.Length; i++)
                            {
                                newv[i] = v[i];
                            }
                            dictionary.Add(k, newv);
                        }
                    }
                }
            }
        }

        private static bool HasEntries(SortedList<string, string[]> dict)
        {
            if (dict != null && dict.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Takes passages of text and adds the words from them to the raw dictionary
        /// </summary>
        /// <param name="passages">array of text chunks to process</param>
        public static void AddToDictionary(string[] passages)
        {
            if (raw_dictionary == null)
                raw_dictionary = new SortedList<string, string[]>();

            if (passages != null)
            {
                for (int j = 0; j < passages.Length; j++)
                {
                    AddToDictionary(passages[j]);
                }
            }
        }

        /// <summary>
        /// Takes a passage of text and adds the words from it to the raw dictionary
        /// </summary>
        /// <param name="passage">text chunk to process</param>
        public static void AddToDictionary(string passage)
        {
            passage = passage.Replace(Environment.NewLine, " ");
            passage = passage.Replace('/', ' ');
            passage = passage.Replace('\\', ' ');
            passage = passage.Replace('.', ' ');
            passage = passage.Replace('?', ' ');
            passage = passage.Replace('!', ' ');
            passage = passage.Replace(',', ' ');

            string[] words = passage.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // each word
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length < word_min_length)
                    continue;

                words[i] = words[i].Trim(TextBoxPredictive.trim_chars).ToLower();
                words[i] = words[i].Trim(Environment.NewLine.ToCharArray());

                if (words[i].Length < word_min_length) 
                    continue;

        //private static int word_stem_min_length = 3;
        //private static int word_stem_max_length = 8;
        //private static int word_min_length = 5;

                int max = (words[i].Length > word_stem_max_length)
                    ? (1 + word_stem_max_length - word_stem_min_length) : 1 + words[i].Length - word_stem_min_length;

                // fragments from 7 to 3 chars in length
                for (int k = max; k >= word_stem_min_length; k--)
                {
                    string first = words[i].Substring(0, k);
                    string last = words[i].Substring(k).ToLower();
                    string[] existing = null;

                    if (raw_dictionary.TryGetValue(first, out existing))
                    {
                        bool found = false;
                        for (int m = 0; m < existing.Length; m++)
                        {
                            if (last.CompareTo(existing[m]) == 0)
                            {
                                found = true;
                                break;
                            }
                        }

                        // replace existing key-value with new one containing new word
                        if (!found)
                        {
                            string[] combined = new string[existing.Length + 1];
                            int idx = 0;
                            bool inserted = false;
                            for (int m = 0; m < existing.Length; m++)
                            {
                                if (!inserted && last.CompareTo(existing[m]) < 0)
                                {
                                    combined[idx] = last;
                                    idx++;
                                    combined[idx] = existing[m];
                                    inserted = true;
                                }
                                else
                                {
                                    combined[idx] = existing[m];
                                }
                                idx++;
                            }

                            if (!inserted) // must be alphabetically last
                            {
                                combined[idx] = last;
                            }

                            raw_dictionary.Remove(first);
                            raw_dictionary.Add(first, combined);
                        }
                    }
                    else
                    {
                        raw_dictionary.Add(first, new string[] { last });
                    }
                }
            }
        }

        private void TextBoxPredictive_TextChanged(object sender, EventArgs e)
        {
            if (!autochange && dictionary_active)
                AutoComplete();
        }

        private void TextBoxPredictive_Leave(object sender, EventArgs e)
        {
            if (suggest_active)
            {
                RemoveSuggestion();
                suggest_active = false;
            }
        }

        /// <summary>
        /// Sets Text to value stored in internal backup generated whenever keypress processed
        /// </summary>
        public void RestorePreviousText()
        {
            if (make_backup)
                Text = text_back; 
        }

        private void TextBoxPredictive_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (suggest_active)
            {
                if (e.KeyChar == 8)
                {
                    e.Handled = true;
                }
                else
                {
                    if (e.KeyChar == 13)
                    {
                        AddSuggestion();
                        e.Handled = true;
                    }
                    else
                    {
                        SelectionStart = RemoveSuggestion();
                    }
                    suggest_active = false;
                }
            }

            text_back = Text;
        }

        private void TextBoxPredictive_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (suggest_active)
            {
                if(e.KeyCode == Keys.Back || e.KeyCode == Keys.Escape)
                {
                    RemoveSuggestion();
                    suggest_active = false;
                }

                if (e.KeyCode == Keys.Return)
                {
                    e.IsInputKey = false;
                }
            }
        }

        private void TextBoxPredictive_MouseClick(object sender, MouseEventArgs e)
        {
            if (suggest_active)
            {
                RemoveSuggestion();
                suggest_active = false;
            }
        }

        /// <summary>
        /// Processes arrow keys if a prediction is active, and prevents their
        /// normal application to the cursor location
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (suggest_active)
            {
                if (keyData == Keys.Up && suggest_idx > 0)
                {
                    suggest_idx--;
                    ChangeSuggestion();
                }
                else if (keyData == Keys.Down && suggest_idx < suggest_idx_max)
                {
                    suggest_idx++;
                    ChangeSuggestion();
                }

                if (keyData == Keys.Down || keyData == Keys.Up ||
                    keyData == Keys.PageDown || keyData == Keys.PageUp ||
                    keyData == Keys.Home || keyData == Keys.End)
                    return true;

                if (keyData == Keys.Left || keyData == Keys.Right)
                {
                    RemoveSuggestion();
                    suggest_active = false;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void AutoComplete()
        {
            if (dictionary == null || Text.Length == 0 || dictionary.Count == 0)
            {
                return;
            }
            
            int initial = SelectionStart;

            int pos = initial - 1;

            bool valid = false;
            int count = 1;
            while (pos >= 0)
            {
                if (!Char.IsLetter(Text[pos]))
                {
                    valid = true;
                    break;
                }
                count++;
                pos--;
                if (pos == -1)
                {
                    valid = true;
                    break;
                }
            }

            if (valid)
            {
                count--;
                pos++;
                string word = Text.Substring(pos, count).ToLower();
                if (count > 1)
                {
                    if (dictionary.ContainsKey(word))
                    {
                        string[] ss = dictionary[word];

                        if (ss != null)
                        {
                            suggest_idx = 0;
                            suggest_idx_max = ss.Length - 1;
                            suggest_txt = ss[0];
                            suggest_stem = word;
                            suggest_active = true;
                            suggest_following = Text.Substring(initial);
                            suggest_preceding = Text.Substring(0, initial);
                            autochange = true;
                            Text = suggest_preceding + suggest_txt + suggest_following;
                            //
                            SelectionStart = initial;
                            SelectionLength = suggest_txt.Length;
                            autochange = false; //
                            MarkSelection(true);
                        }
                    }
                }
            }
        }

        private void MarkSelection(bool on)
        {
            if (use_altered_fonts)
            {
                autochange = true;
                if (suggest_active)
                {
                    if (on)
                    {
                        if (suggest_idx_max > 0)
                        {
                            Font font = new Font(Font.FontFamily, 12, FontStyle.Italic);
                            SelectionFont = font;
                        }
                        else
                        {
                            Font font = new Font(Font.FontFamily, 12, FontStyle.Bold);
                            SelectionFont = font;
                        }
                    }
                    else
                    {
                        Font font = new Font(Font.FontFamily, 12, FontStyle.Regular);
                        SelectionFont = font;
                    }
                }

                autochange = false;
            }
        }

        private void AddSuggestion()
        {
            int initial = SelectionStart;
            autochange = true;
            Text = suggest_preceding + suggest_txt + suggest_following;
            SelectionStart = initial + suggest_txt.Length - 1;
            autochange = false;
            suggest_txt = "";
            suggest_idx = 0;
        }

        private int RemoveSuggestion()
        {
            int initial = SelectionStart;
            if (suggest_active)
            {
                autochange = true;
                //MarkSelection(false);
                Text = suggest_preceding + suggest_following;
                SelectionStart = initial;
                autochange = false;
            }
            return initial;
        }

        private void ChangeSuggestion()
        {
            if (suggest_active)
            {
                int initial = RemoveSuggestion();

                autochange = true;
                string[] ss = dictionary[suggest_stem];

                if (ss != null)
                {
                    suggest_txt = ss[suggest_idx];

                    Text = suggest_preceding + suggest_txt + suggest_following;
                    SelectionStart = initial;
                    SelectionLength = suggest_txt.Length;
                }
                MarkSelection(true);
                autochange = false;
            }
        }
    }
}
