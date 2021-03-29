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
        Samples current;

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
            using(Timer t = new Timer()) {
                Samples s = new Samples(Util.csv2bs(textBox1.Text));
                current = s;
                s.OptimzeOrder();
                int h = s.Height;
                BigInteger[] total = new BigInteger[1];

                t.Interval = 500;
                t.Tick += delegate (object timer, EventArgs ea) {
                    BigInteger result;
                    lock(s) result = s.pubResult;
                    int progress = (int)(result * 1000 / total[0]);
                    if (progress <= 1000) {
                        progressBar1.Value = progress;
                        reportProgress2(result, total[0]);
                    }
                };

                Util.CacheCombinations(h);

                dataGridView1.Rows.Clear();
                label4.Text = "サンプル数: " + h + "\r\n項目数: " + s.Width;
                button2.Enabled = true;

                t.Start();

                int ibase = (int)numericUpDown1.Value - 1;

                for(int i = ibase; i < h; i++) {
                    int idx = i - ibase;
                    reportProgress(i + 1, h);
                    total[0] = Util.nCr(h, i + 1);
                    var result = await Task.Run(delegate () {
                        return s.Calc(i + 1);
                    });
                    if(s.cancel) break;
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[idx].Cells[0].Value = i + 1;
                    dataGridView1.Rows[idx].Cells[1].Value = result.ToString();
                    dataGridView1.Rows[idx].Cells[2].Value = Util.nCr(h, i + 1).ToString();
                }

                current = null;

                button2.Enabled = false;
                label3.Text = "";
                label5.Text = "";
                t.Stop();
            }
        }

        private void reportProgress(int cur, int all) {
            label3.Text = "計算中...(" + cur + "サンプル使用中/全" + all + "サンプル中)";
        }

        private void reportProgress2(BigInteger result, BigInteger total) {
            label5.Text = result + "/" + total;
        }

        private void button2_Click_1(object sender, EventArgs e) {
            if (current != null) {
                lock(current) current.cancel = true;
            }
        }
    }
}
