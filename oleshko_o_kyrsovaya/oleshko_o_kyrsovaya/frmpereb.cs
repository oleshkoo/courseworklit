using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oleshko_o_kyrsovaya
{
    public partial class frmpereb : Form
    {
        public frmpereb()
        {
            InitializeComponent();
        }
        int[,] raba;
        int[] sumr;
        int n, m, i, j, k, nf = 1, v, l, z, x, swap, r, dls, maxs = 1000, maxpr, cvet;
        int[] a;
        int[] b;
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            n = int.Parse(textBox1.Text);
            m = int.Parse(textBox2.Text);
            raba = new int[n, m];
            if (m >= n)
            {
                MessageBox.Show("Ви маєте більшу кількість (або рівну) робіт");
            }
            else
            {
                dataGridView1.ColumnCount = m + 1;
                dataGridView1.RowCount = 1;//как сделать 0?
                for (i = 0; i < n; i++)
                {
                    dataGridView1.Rows.Add();
                }
                for (i = 0; i <= n; i++)
                {
                    for (j = 0; j <= m; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Style.Font = new Font(Name, 12, FontStyle.Regular);
                        dataGridView1.Rows[i].Cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView1.Columns[j].Width = 100;
                    }
                    dataGridView1.Rows[i].Height = 70;
                }
                j = 0;
                for(i=1;i<= n;i++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = "робітник "+i;
                }
                i = 0;
                for(j=1;j<= m;j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = "робота " + j;
                }
            }
            MessageBox.Show("Зараз введіть данні у матрицю, яка побудується: клікайте на клітинки та вводіть данні");
        }
    

        private void button2_Click(object sender, EventArgs e)
        {
            frmvhod z = new frmvhod();
            z.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (i = 1; i <= n; i++)
            {
                for (j = 1; j <= m; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                    {
                        MessageBox.Show("Ви не ввели значення часу виконання " + i + " робітником " + j + " роботи");
                        break;
                    }
                    raba[i-1, j-1] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
            string otv = "";
            a = new int[n];
            sumr = new int[m];
            //Факторіал числа n - кількість варіантів.
            for (i = 0; i < n; i++)
            {
                a[i] = i + 1;
            }
            for (i = 1; i <= n; i++)
            {
                nf = nf * i;
            }
            //Перший раз перебираю і знаходжу максимальну суму рядка для варіанту при n = 4: 1 2 3 4
            maxpr = -1;
            for (dls = 0; dls < m; dls++)
            {
                sumr[dls] = raba[(a[dls] - 1), dls];
                if (sumr[dls] > maxpr)
                {
                    maxpr = sumr[dls];
                }
            }
            if (maxs > maxpr)
            {
                otv = "";
                maxs = maxpr;
                for (int lon = 0; lon < m; lon++)
                {
                    otv = otv + " " + a[lon].ToString();
                }
            }
            //Тепер я використовую алгоритм Найрани. Короткий опис:
            for (k = 0; k < nf; k++)//Крок 1: знайти найбільший j, для якого a [j] <a [j + 1].
            {
                l = 0;
                j = 0;
                for (v = n - 2; v >= 0; v--)
                {
                    if (a[v] < a[v + 1] && v > j)
                    {
                        j = v;
                    }
                }
                for (z = j + 1; z < n; z++)//Крок 2: збільшити a [j]. Для цього потрібно знайти найбільше l, для якого a [l] > a [j]. Потім поміняти місцями a [j] і a [l].
                {
                    if (a[j] < a[z] && z > l)
                    {
                        l = z;
                    }
                }
                swap = a[j];
                a[j] = a[l];
                a[l] = swap;
                b = new int[n - (j + 1)];//Крок 3: записати a [j + 1], ..., a [n] в зворотному порядку.
                r = 0;
                for (x = j + 1; x < n; x++)
                {
                    b[r] = a[x];
                    r++;
                }
                Array.Reverse(b);
                r = 0;
                for (x = j + 1; x < n; x++)
                {
                    a[x] = b[r];
                    r++;
                }
                //Переглядаю інші варіанти перебору, як описано вище.
                maxpr = -1;
                for (dls = 0; dls < m; dls++)
                {
                    sumr[dls] = raba[(a[dls] - 1), dls];
                    if (sumr[dls] > maxpr)
                    {
                        maxpr = sumr[dls];
                    }
                }
                if (maxs > maxpr)
                {
                    otv = "";
                    maxs = maxpr;
                    for (int lon = 0; lon < m; lon++)
                    {
                        otv = otv + " " + a[lon].ToString();
                    }
                }
            }
            otv = otv.Trim();
            string[] otvet;
            otvet = otv.Split(' ');
            for (int otvi = 0; otvi < m; otvi++)
            {
                cvet = Convert.ToInt32(otvet[otvi]);
                dataGridView1.Rows[cvet].Cells[otvi+1].Style.ForeColor = Color.Green;
                dataGridView1.Rows[cvet].Cells[otvi+1].Style.BackColor = Color.Wheat;
                listBox1.Items.Add("Робота " + (otvi + 1).ToString() + " виконується працівником " + otvet[otvi]);
            }
        }
    }
}
