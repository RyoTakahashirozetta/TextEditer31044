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
using System.Windows.Forms.VisualStyles;

namespace TextEditer31044
{
    public partial class Form1 : Form
    {
        //
        private string fileName = "";//Camel形式 (⇔Pascal形式)

        public Form1()
        {
            InitializeComponent();
        }

        private void ExitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //アプリケーション終了
            Application.Exit();
        }

        

        private void OpenOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdFileOpen.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(ofdFileOpen.FileName, Encoding.GetEncoding("utf-8"),false))
                {
                    rtTextArea.Text = sr.ReadToEnd();
                    this.fileName = ofdFileOpen.FileName;
                }
            }

            
        }

        //上書き保存
        private void SaveSToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (this.fileName != "")
            {
                FileSave(fileName);
            }

            else
            {
                NameSaveAToolStripMenuItem_Click(sender, e);
            }
         
        }

        

        //名前を付けて保存
        private void NameSaveAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ダイアログの表示
            if (sfdFileSave.ShowDialog() == DialogResult.OK)
            {
                FileSave(sfdFileSave.FileName);
            }
        }

        //ファイル名を指定しデータを保存
        private void FileSave(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding("utf-8")))
            {
                sw.WriteLine(rtTextArea.Text);
            }
        }


        private void UndoUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.CanUndo == true)
            {
                rtTextArea.Undo();
            }
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.CanRedo == true)
            {
                rtTextArea.Redo();
            }
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.SelectionLength > 0)
            {
                rtTextArea.Cut();
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.SelectionLength > 0)
            {
                rtTextArea.Copy();
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data != null && data.GetDataPresent(DataFormats.Text) == true)
            {
                rtTextArea.Paste();
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.SelectionLength > 0)
            {
                
            }
        }
    }
}
