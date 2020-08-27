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
            //using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding("utf-8")))
            //{
            //    sw.WriteLine(rtTextArea.Text);
            //}
            rtTextArea.SaveFile(fileName, RichTextBoxStreamType.RichText);
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
                PasteToolStripMenuItem.Enabled = true;
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.SelectionLength > 0)
            {               
                rtTextArea.Copy();
                PasteToolStripMenuItem.Enabled = true;
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {          
                rtTextArea.Paste();                      
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.SelectionLength > 0)
            {
                rtTextArea.SelectedText = "";
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.ResetText();
        }

        private void rtTextArea_TextChanged(object sender, EventArgs e)
        {
            UndoUToolStripMenuItem.Enabled = true;   
        }

        private void rtTextArea_SelectionChanged(object sender, EventArgs e)
        {
            if (rtTextArea.SelectionLength > 0)
            {
                CopyToolStripMenuItem.Enabled = true;
                CutToolStripMenuItem.Enabled = true;
                DeleteToolStripMenuItem.Enabled = true;
            }
            else
            {
                CopyToolStripMenuItem.Enabled = false;
                CutToolStripMenuItem.Enabled = false;
                DeleteToolStripMenuItem.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                PasteToolStripMenuItem.Enabled = true;
            }          
        }

        private void NewToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (rtTextArea.Text != "")
            {
                DialogResult dr = MessageBox.Show("変更内容を保存しますか？", "メッセージ", MessageBoxButtons.YesNoCancel);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveSToolStripMenuItem_Click(sender, e);
                }
                else if (dr == System.Windows.Forms.DialogResult.No)
                {
                    rtTextArea.ResetText();
                }
                else if (dr == System.Windows.Forms.DialogResult.Cancel)
                {
                    
                }
            }      
        }


        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cdCcolor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                rtTextArea.SelectionColor = cdCcolor.Color;
            }
        }

        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fdFont.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                rtTextArea.SelectionFont = fdFont.Font;
            }
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RedoToolStripMenuItem.Enabled = rtTextArea.CanRedo;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("変更内容を保存しますか？", "メッセージ", MessageBoxButtons.YesNoCancel);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                SaveSToolStripMenuItem_Click(sender, e);
            }
            else if (dr == System.Windows.Forms.DialogResult.No)
            {
                rtTextArea.ResetText();
            }
            else if (dr == System.Windows.Forms.DialogResult.Cancel)
            {

            }
        }
    }
}
