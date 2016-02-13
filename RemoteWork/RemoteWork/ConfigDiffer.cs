using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteWork
{
    public partial class ConfigDiffer : Form
    {
        string oldText;
        string newText;
        public ConfigDiffer(string fConfig, string sConfig, string date1, string date2)
        {
            InitializeComponent();
            oldText = fConfig;
            newText = sConfig;
            label1.Text += date1;
            label2.Text += date2;
            richTextBoxFirst.Text = fConfig;
            richTextBoxSecond.Text = sConfig;
            this.Shown += Differ;
        }
        //сравнение
        private void Differ(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            List<string> inserted = new List<string>();
            List<string> deleted = new List<string>();
            List<string> modified = new List<string>();
            List<string> imaginary = new List<string>();

            var d = new DiffPlex.Differ();
            var builder = new InlineDiffBuilder(d);
            var result = builder.BuildDiffModel(oldText, newText);

            foreach (var line in result.Lines)
            {
                if (line.Type == ChangeType.Inserted)
                {
                    //Color Green
                    inserted.Add(line.Text);
                    // sb.Append("+ ");
                }
                else if (line.Type == ChangeType.Deleted)
                {
                    //Color Red
                    deleted.Add(line.Text);
                    // sb.Append("- ");
                }
                else if (line.Type == ChangeType.Modified)
                {
                    //Color Blue
                    modified.Add(line.Text);
                    // sb.Append("* ");
                }
                else if (line.Type == ChangeType.Imaginary)
                {                   
                    //Color.Orange
                    imaginary.Add(line.Text);
                    //  sb.Append("? ");
                }
                else if (line.Type == ChangeType.Unchanged)
                {
                    // sb.Append("  ");
                }

                sb.Append(line.Text + "\r\n");
            }


            string res = sb.ToString();

            richTextBoxDiff.Text = res;

            //обработка цветом
            foreach (string str in inserted)
            {
                HighlightText(richTextBoxDiff, str, Color.GreenYellow);
            }
            foreach (string str in deleted)
            {
                HighlightText(richTextBoxDiff, str, Color.IndianRed);
            }
            foreach (string str in modified)
            {
                HighlightText(richTextBoxDiff, str, Color.Blue);
            }
            foreach (string str in imaginary)
            {
                HighlightText(richTextBoxDiff, str, Color.Orange);
            }
        }
        //подсветка текста
        private void HighlightText(RichTextBox myRtb, string word, Color color)
        {

            int s_start = myRtb.SelectionStart, startIndex = 0, index;

            while ((index = myRtb.Text.IndexOf(word, startIndex)) != -1)
            {
                myRtb.Select(index, word.Length);
                myRtb.SelectionBackColor = color;
                // myRtb.SelectionColor = color;
                startIndex = index + word.Length;
            }

            myRtb.SelectionStart = s_start;
            myRtb.SelectionLength = 0;
            myRtb.SelectionColor = Color.Black;
        }
    }
}
