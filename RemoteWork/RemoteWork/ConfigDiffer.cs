using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections;
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
        CustomRichTextBox richTextBoxDiff = new CustomRichTextBox();
        public ConfigDiffer(string fConfig, string sConfig, string date1, string date2)
        {
            InitializeComponent();
            oldText = fConfig;
            newText = sConfig;
            label1.Text += date1;
            label2.Text += date2;
            richTextBoxFirst.Text = fConfig;
            richTextBoxSecond.Text = sConfig;
            //создаем третий richtextbox с улучшенной производительностью
            richTextBoxDiff.ReadOnly = true;
            richTextBoxDiff.Dock = DockStyle.Fill;
            tableLayoutPanel4.Controls.Add(richTextBoxDiff);
           // this.Shown += Differ;            
        }   
        //сравнение
        private void Differ()
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
           
            //проблема с подцветкой реализовать другой алгоритм
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
        //сравнение вариант производительнее
        private void DifferTable(object sender, EventArgs e)
        {
            //DateTime start = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            Hashtable insertedTable = new Hashtable();
            Hashtable deletedTable = new Hashtable();
            Hashtable modifiedTable = new Hashtable();
            Hashtable imaginaryTable = new Hashtable();

            var d = new DiffPlex.Differ();
            var builder = new InlineDiffBuilder(d);
            var result = builder.BuildDiffModel(oldText, newText);

            foreach (var line in result.Lines)
            {
                if (line.Type == ChangeType.Inserted)
                {
                    //Color Green
                    //inserted.Add(line.Text);
                    if (!insertedTable.ContainsKey(line.Text))
                    {
                        insertedTable.Add(line.Text, "INSERTED");
                    }
                    // sb.Append("+ ");
                }
                else if (line.Type == ChangeType.Deleted)
                {
                    //Color Red
                    // deleted.Add(line.Text);
                    if (!deletedTable.ContainsKey(line.Text))
                    {
                        deletedTable.Add(line.Text, "DELETED");
                    }
                    // sb.Append("- ");
                }
                else if (line.Type == ChangeType.Modified)
                {
                    //Color Blue
                    // modified.Add(line.Text);
                    if (!modifiedTable.ContainsKey(line.Text))
                    {
                        modifiedTable.Add(line.Text, "MODIFIED");
                    }

                    // sb.Append("* ");
                }
                else if (line.Type == ChangeType.Imaginary)
                {
                    //Color.Orange
                    //imaginary.Add(line.Text);
                    if (!imaginaryTable.ContainsKey(line.Text))
                    {
                        imaginaryTable.Add(line.Text, "MODIFIED");
                    }

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

            //проблема с подцветкой реализовать другой алгоритм
            //обработка цветом


            foreach (string str in insertedTable.Keys)
            {
                HighlightText(richTextBoxDiff, str, Color.GreenYellow);
            }
            foreach (string str in deletedTable.Keys)
            {
                HighlightText(richTextBoxDiff, str, Color.IndianRed);
            }
            foreach (string str in modifiedTable.Keys)
            {
                HighlightText(richTextBoxDiff, str, Color.Blue);
            }
            foreach (string str in imaginaryTable.Keys)
            {
                HighlightText(richTextBoxDiff, str, Color.Blue);
            }
            //TimeSpan time = DateTime.Now - start;
            //MessageBox.Show(time.ToString());
        }
        //подсветка текста
        private void HighlightText(RichTextBox myRtb, string word, Color color)
        {

            int s_start = myRtb.SelectionStart, startIndex = 0, index;

            //while ((index = myRtb.Text.IndexOf(word, startIndex)) != -1)
            //{
            //    myRtb.Select(index, word.Length);
            //    myRtb.SelectionBackColor = color;
            //    // myRtb.SelectionColor = color;
            //    startIndex = index + word.Length;
            //}
            if ((index = myRtb.Text.IndexOf(word, startIndex)) != -1)
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
        //сравнить
        private void toolStripButtonCompare_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            DifferTable(sender, e);
            UseWaitCursor = false;          
        }

   
    }
}
