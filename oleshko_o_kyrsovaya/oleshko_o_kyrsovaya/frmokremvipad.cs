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
    public partial class frmokremvipad : Form
    {
        public frmokremvipad()
        {
            InitializeComponent();
        }
        int[,] raba;
        int n, m, i, j;
        int[] sumr;
        int[] a;
        int[] b;
        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            int k, nf = 1, v, l, z, x, swap, r, dls, maxs = 1000, sumstrok, cvet;
            for (i = 1; i <= n; i++)
            {
                for (j = 1; j <= m; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                }
            }
            for (i = 1; i <= n; i++)
            {
                for (j = 1; j <= m; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                    {
                        MessageBox.Show("Ви не ввели значення часу виконання " + i + " робітником " + j + " роботи");
                        break;
                    }
                    raba[i - 1, j - 1] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
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
            sumstrok = 0;
            for (dls = 0; dls < m; dls++)
            {
                sumr[dls] = raba[(a[dls] - 1), dls];
                sumstrok = sumstrok + sumr[dls];
            }
            if (maxs > sumstrok)
            {
                otv = "";
                maxs = sumstrok;
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
                sumstrok = 0;
                for (dls = 0; dls < m; dls++)
                {
                    sumr[dls] = raba[(a[dls] - 1), dls];
                    sumstrok = sumstrok + sumr[dls];
                }
                if (maxs > sumstrok)
                {
                    otv = "";
                    maxs = sumstrok;
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
                dataGridView1.Rows[cvet].Cells[otvi + 1].Style.ForeColor = Color.Green;
                dataGridView1.Rows[cvet].Cells[otvi + 1].Style.BackColor = Color.Wheat;
                listBox1.Items.Add("Робота " + (otvi + 1).ToString() + " виконується працівником " + otvet[otvi]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                for (i = 1; i <= n; i++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = "робітник " + i;
                }
                i = 0;
                for (j = 1; j <= m; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = "робота " + j;
                }
            }
            MessageBox.Show("Зараз введіть данні у матрицю, яка побудується: клікайте на клітинки та вводіть данні");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int mina = 1000, i, j, mini = 0, sumstr = 0, kolvo = 0;
            for (i = 1; i <= n; i++)
            {
                dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.White;
            }
            for (i = 1; i <= n; i++)
            {
                for (j = 1; j <= m; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                    {
                        MessageBox.Show("Ви не ввели значення часу виконання " + i + " робітником " + j + " роботи");
                        break;
                    }
                    raba[i - 1, j - 1] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
            j = 0;
            for (i = 0; i < n; i++)
            {
                if (raba[i, j] < mina)
                {
                    mina = raba[i, j];//знаходження мінімального часу виконання першої роботи (записується індекс працівника)
                    mini = i;
                }
            }
            for (j = 0; j < m; j++)
            {
                sumstr = sumstr + raba[mini, j];
            }
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < m; j++)
                {
                    if (raba[i, j] < sumstr)//рахується кількість робіт, що не перевищує суми "кращого виконавця" першої роботи
                    {
                        kolvo++;
                    }
                }
            }
            if (kolvo == m)//якщо ця сумма дроівнює m, то робітник кращий, адже m - кількість робіт у нього і він виконує їх швидше у сумі за інших
            {
                label3.Text = "Робітник " + (mini + 1).ToString() + " буде виконувати усі роботи скоріше за інших.";
                dataGridView1.Rows[mini +1].Cells[0].Style.ForeColor = Color.Green;
                dataGridView1.Rows[mini + 1].Cells[0].Style.BackColor = Color.Wheat;
            }
            else
                label3.Text = "Ніякий робітник не буде виконувати усі роботи скоріше за інших.";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmvhod z = new frmvhod();
            z.Show();
            this.Hide();
        }
    }
}
