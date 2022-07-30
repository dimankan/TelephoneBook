using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelephoneBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection("Server=127.0.0.1;Database=telephonebook;Uid=root;Pwd=root;");

        private void button1_Click(object sender, EventArgs e)
        {
            using (conn)
            {
                MySqlDataAdapter SDA = new MySqlDataAdapter("select * from accounts", conn);
                DataTable dataTable = new DataTable();
                SDA.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void btAddContact_Click(object sender, EventArgs e)
        {
            using (conn)
            {
                MySqlDataAdapter SDA = new MySqlDataAdapter($"INSERT INTO `accounts`(`Name`, `Phone`) VALUES ('{tbName.Text}','{tbNumber.Text}')", conn);
                DataTable dataTable = new DataTable();
                SDA.Fill(dataTable);
            }

            button1_Click(sender, e);
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            using (conn)
            {
                MySqlDataAdapter SDA = new MySqlDataAdapter($"select * from accounts where name = '{tbFind.Text}' or Phone = '{tbFind.Text}' ", conn);
                DataTable dataTable = new DataTable();
                SDA.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Удаляем??", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if(dialog == DialogResult.OK)
            {
                using (conn)
                {
                    MySqlDataAdapter SDA = new MySqlDataAdapter($"DELETE FROM `accounts` WHERE Phone = '{tbFind.Text}' or Name = '{tbFind.Text}' ", conn);
                    DataTable dataTable = new DataTable();
                    SDA.Fill(dataTable);
                }

                button1_Click(sender, e);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            textBox1.Text = row.ToString();
            textBox2.Text = row.Cells["Name"].Value.ToString();
        }
    }


}


#region Trash
/*
 List<Model> models = new List<Model>();
            models.Add(new Model() { Id = 1, Name = "Misha"});
            models.Add(new Model() { Id = 2, Name = "Dima"});

            dataGridView1.DataSource = models;
 */

#endregion