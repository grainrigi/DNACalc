using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace DNACalc {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) {
        }

        private void textBox1_Pasted(object sender, EventArgs e) {
            IDataObject data = Clipboard.GetDataObject();
            TextBox s = (TextBox)sender;

            string pasted = "";

            if (data != null) {
                object csvObj = data.GetData(DataFormats.CommaSeparatedValue);
                if(csvObj is System.IO.MemoryStream ms) {
                    pasted = System.Text.Encoding.Default.GetString(ms.ToArray().Where(v => v != 0).ToArray());
                } else if (csvObj is string str) {
                    pasted = str;
                } else {
                    object strObj = data.GetData(DataFormats.Text);
                    if (strObj is string str2) {
                        pasted = str2;
                    }
                }
            }

            int oldStart = s.SelectionStart;
            string oldText = s.Text;
            string former = oldText.Substring(0, oldStart);
            string latter = oldText.Substring(oldStart + s.SelectionLength);
            s.Text = former + pasted + latter;
            s.SelectionStart = oldStart + pasted.Length;
        }

        private async void button1_Click(object sender, EventArgs e) {
            Samples s = new Samples(Util.csv2bs(textBox1.Text));
            int h = s.Height;

            dataGridView1.Rows.Clear();
            label4.Text = "サンプル数: " + h + "\r\n項目数: " + s.Width;

            for (int i = 0; i < h; i++) {
                reportProgress(i + 1, h);
                var result = await Task.Run(delegate () {
                    return s.Calc(i + 1);
                });
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                dataGridView1.Rows[i].Cells[1].Value = result.ToString();
                dataGridView1.Rows[i].Cells[2].Value = Util.nCr(h, i + 1).ToString();
            }

            label3.Text = "";
        }

        private void reportProgress(int cur, int all) {
            label3.Text = "計算中...(" + cur + "サンプル使用中/全" + all + "サンプル中)";
        }
    }
}
