using System;
using System.Drawing;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ооп8
{
    public partial class Form1 : Form
    {
        string file_name = "book.xml";
        XmlTextReader xml_read;

        // Класс DataSet  представляет собой расположенный в памяти кэш данных,
        // загружаемых из источника данных 
        // Класс DataSet состоит из коллекции таблиц класса DataTable
        DataSet DataXML = new DataSet();

        // Класс DataTable представляет одну таблицу с данными в памяти
        DataTable MyDatatable = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            DataXML.ReadXml(@"book.xml");
            MyDatatable = DataXML.Tables[0];
            bindingSource1.DataMember = DataXML.Tables[0].ToString();
            bindingSource1.DataSource = DataXML.Tables[0];
            this.bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[0].HeaderText = "Название";
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[1].HeaderText = "Описание";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[3].HeaderText = "Дата";

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxName.Text = DataXML.Tables[0].Rows[e.RowIndex][0].ToString();
            textBoxDescription.Text = DataXML.Tables[0].Rows[e.RowIndex][1].ToString();
            string img = DataXML.Tables[0].Rows[e.RowIndex][2].ToString();
            textBoxDate.Text = DataXML.Tables[0].Rows[e.RowIndex][3].ToString();
            pictureBox1.Load(img);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxName.Text = DataXML.Tables[0].Rows[e.RowIndex][0].ToString();
            textBoxDescription.Text = DataXML.Tables[0].Rows[e.RowIndex][1].ToString();
            string img = DataXML.Tables[0].Rows[e.RowIndex][2].ToString();
            textBoxDate.Text = DataXML.Tables[0].Rows[e.RowIndex][3].ToString();
            pictureBox1.Load(img);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBoxName.Text = DataXML.Tables[0].Rows[e.RowIndex][0].ToString();
            textBoxDescription.Text = DataXML.Tables[0].Rows[e.RowIndex][1].ToString();
            string img = DataXML.Tables[0].Rows[e.RowIndex][2].ToString();
            textBoxDate.Text = DataXML.Tables[0].Rows[e.RowIndex][3].ToString();
            pictureBox1.Load(img);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                MessageBox.Show("Вы не ввели данные для выполнения фильтрации в базе данных!"); 
                return;
            }

            DataView dv = new DataView(DataXML.Tables[0]);
            // Если нажата радиокнопка для поиска данных по названию
            if (radioButton1.Checked == true)
            {
                string t = textBox5.Text;
                dv.RowFilter = "TITLE='" + t + "'";
            }
            // Если нажата радиокнопка для поиска данных по количеству страниц
            if (radioButton2.Checked == true)
            {
                string p = textBox5.Text;
                dv.RowFilter = "DATE='" + p + "'";
            }
            bindingSource1.DataSource = dv;
            this.bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.DataSource = bindingSource1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bindingSource1.DataSource = DataXML.Tables[0];
            this.bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
        }
    }
}
