using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 简单记事本
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void 打开ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void 隐藏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
        public String filepath = "";
        List<String> list = new List<string>();
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String path;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;//获取文件名
                String filename = Path.GetFileName(path);
                this.Text = filename;
                listBox1.Items.Add(filename);
                list.Add(path);
                filepath = path;
                textBox1.Text = "";
                using (FileStream fsr = new FileStream(path,
                    FileMode.OpenOrCreate,
                    FileAccess.Read))
                {
                    byte[] buffer = new byte[1024 * 1024];
                    while (true)
                    {
                        int r = fsr.Read(buffer, 0, buffer.Length);
                        textBox1.Text += Encoding.Default.GetString(buffer, 0, r);
                        if (r == 0)
                        {
                            break;
                        }
                    }
                }
            }
            else { return; }
           
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Text == "无标题.txt")
            {
                String path;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = saveFileDialog1.FileName;//获取文件名
                    using (FileStream fsw = new FileStream(path,
                    FileMode.OpenOrCreate,
                    FileAccess.Write))
                    {

                        byte[] buffer = Encoding.Default.GetBytes(textBox1.Text);
                        fsw.Write(buffer, 0, buffer.Length);
                    }
                    textBox1.Text = "";
                    this.Text = "无标题.txt";
                }
                else { return; }
                
            }
            else
            {
                FileStream fsw = new FileStream(filepath,
                FileMode.Create,
                FileAccess.Write);
                byte[] buffer = Encoding.Default.GetBytes(textBox1.Text);
                fsw.Write(buffer, 0, buffer.Length);
                textBox1.Text = "";
                fsw.Close();fsw.Dispose();
                this.Text = "无标题.txt";
            }
        }

        private void wordstrap_Click(object sender, EventArgs e)
        {
            if(wordstrap.Text == "取消自动换行")
            {
                textBox1.WordWrap = false;
                wordstrap.Text = "自动换行";
            }
            else
            {
                textBox1.WordWrap = true;
                wordstrap.Text = "取消自动换行";
            }
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            textBox1.Font = fontDialog1.Font;
        }

        private void 颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            textBox1.ForeColor = colorDialog1.Color;
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            String path = saveFileDialog1.FileName;
            if (path == "") { return; }
            using (FileStream fsw = new FileStream(path,
                FileMode.OpenOrCreate,
                FileAccess.Write))
            {
                byte[] buffer = Encoding.Default.GetBytes(textBox1.Text);
                fsw.Write(buffer, 0, buffer.Length);
            }
            textBox1.Text = "";
            this.Text = "无标题.txt";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Text == "无标题.txt"&&textBox1.Text != "")
            {
                String path;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = saveFileDialog1.FileName;//获取文件名
                    using (FileStream fsw = new FileStream(path,
                    FileMode.OpenOrCreate,
                    FileAccess.Write))
                    {

                        byte[] buffer = Encoding.Default.GetBytes(textBox1.Text);
                        fsw.Write(buffer, 0, buffer.Length);
                    }
                    textBox1.Text = "";
                    this.Text = "无标题.txt";
                }
                else { return; }
            }
            else
            {
                if(filepath == "") { return; }
                FileStream fsw = new FileStream(filepath,
                FileMode.Create,
                FileAccess.Write);
                byte[] buffer = Encoding.Default.GetBytes(textBox1.Text);
                fsw.Write(buffer, 0, buffer.Length);
                fsw.Close(); fsw.Dispose();
            }
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "无标题.txt";
            textBox1.Text = "";
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            String listpath = list[listBox1.SelectedIndex];
            this.Text = Path.GetFileName(listpath);
            textBox1.Text = "";
            using (FileStream fsr = new FileStream(listpath,
                   FileMode.OpenOrCreate,
                   FileAccess.Read))
            {
                byte[] buffer = new byte[1024 * 1024];
                while (true)
                {
                    int r = fsr.Read(buffer, 0, buffer.Length);
                    textBox1.Text += Encoding.Default.GetString(buffer, 0, r);
                    if (r == 0)
                    {
                        break;
                    }
                }
            }
            panel1.Visible = false;
        }
    }
}
