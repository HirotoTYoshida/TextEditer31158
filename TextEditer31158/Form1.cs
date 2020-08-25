using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditer31158
{
    public partial class Form1 : Form
    {
        private string fileName = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void ExitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //アプリケーション終了
            Application.Exit();
        }

        //
        private void OpenOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ofdFileOpen.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(ofdFileOpen.FileName,Encoding.GetEncoding("utf-8") ,false))
                {
                    rtTextArea.Text = sr.ReadToEnd();
                }
            }
        }

        //上書き保存
        private void SaveSToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if(this.fileName != "")
            {
                FileSave(fileName);
            }
            else
            {
                SaveNameAToolStripMenuItem_Click(sender, e);
            }
        }

        //名前を付けて保存
        private void SaveNameAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdFileSave.ShowDialog() == DialogResult.OK)
            {
                FileSave(sfdFileSave.FileName);
            }
        }

        private void FileSave(string fileName)
        {
            using(StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding("utf-8")))
            {
                sw.WriteLine(rtTextArea.Text);
            }
        }

        

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.CanUndo)
            {
                rtTextArea.Undo();
                rtTextArea.ClearUndo();
            }
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(rtTextArea.CanRedo)
            {
                rtTextArea.Redo();
                rtTextArea.ClearUndo();
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Copy();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Paste();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Cut();
        }

        private void DeleetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.SelectedText = "";
        }

        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(cldcolor.ShowDialog() == DialogResult.OK)
            {
                rtTextArea.SelectionColor = cldcolor.Color;
            }
        }

        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ftdfont.ShowDialog() == DialogResult.OK)
            {
                rtTextArea.SelectionFont = ftdfont.Font;
            }
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditMenuMaskCheck();
        }

        private void EditMenuMaskCheck()
        {
            DataFormats.Format myFormat = DataFormats.GetFormat(DataFormats.Rtf);
            UndoToolStripMenuItem.Enabled = rtTextArea.CanUndo ? true : false;
            RedoToolStripMenuItem.Enabled = rtTextArea.CanRedo ? true : false;
            CutToolStripMenuItem.Enabled = rtTextArea.SelectionLength > 0 ? true : false;
            CopyToolStripMenuItem.Enabled = rtTextArea.SelectionLength > 0 ? true : false;
            PasteToolStripMenuItem.Enabled = rtTextArea.CanPaste(myFormat) ? true : false;
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Text = "";
        }
    }
}
