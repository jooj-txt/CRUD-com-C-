using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace ferragens
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }
        ClassDao dao = new ClassDao(); // instanciar
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                dao.ConsultaTabela(dataGridView1, textBox3.Text);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dao.Conecte("ferragens", "produtos");
            dao.PreencheTabela(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || richTextBox1.Text == "")
            {
                MessageBox.Show("campos em branco");
            }
            else
            {
                dao.Add(textBox1.Text, richTextBox1.Text, textBox2.Text, int.Parse(textBox4.Text), int.Parse(textBox5.Text));
                dao.PreencheTabela(dataGridView1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            dao.Att(textBox1.Text, richTextBox1.Text, textBox2.Text, int.Parse(textBox4.Text), int.Parse(textBox5.Text),int.Parse(textBox6.Text));
            dao.PreencheTabela(dataGridView1);
        }
    }
}
