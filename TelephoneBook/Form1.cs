using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

        MySqlConnection mySqlConnection = new MySqlConnection("Server=127.0.0.1;Database=telephonebook;Uid=root;Pwd=root;");
        SqlConnection mmSqlConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private  void button1_Click(object sender, EventArgs e)
        {
            #region Old
            //using (mySqlConnection)
            //{
            //    MySqlDataAdapter SDA = new MySqlDataAdapter("select * from accounts", mySqlConnection);
            //
            //    DataTable dataTable = new DataTable();
            //    SDA.Fill(dataTable);
            //
            //    dataGridView1.DataSource = dataTable;
            //} 
            #endregion

            //await mmSqlConnection.OpenAsync();

            mmSqlConnection.Open();
            SqlCommand cmd = new SqlCommand("select * from [dbo].[TestTable]", mmSqlConnection);

            DataTable dataTable = new DataTable();
            dataTable.Load(cmd.ExecuteReader());

            dataGridView1.DataSource = dataTable;

            mmSqlConnection.Close();


        }

        private  void btAddContact_Click(object sender, EventArgs e)
        {
            #region Old
            /*
            using (mySqlConnection)
            {
                MySqlDataAdapter SDA = new MySqlDataAdapter($"INSERT INTO `accounts`(`Name`, `Phone`) VALUES ('{tbName.Text}','{tbNumber.Text}')", mySqlConnection);
                DataTable dataTable = new DataTable();
                SDA.Fill(dataTable);
            }

            button1_Click(sender, e); 
            */
            #endregion


            //await mmSqlConnection.OpenAsync();
            mmSqlConnection.Open();

            SqlCommand cmd = new SqlCommand($"insert into dbo.TestTable (name, phone, mobile) values('{tbName.Text}', '{tbNumber.Text}', '{tbMobile.Text}')", mmSqlConnection);

            //await cmd.ExecuteNonQueryAsync();
            cmd.ExecuteNonQuery();

             mmSqlConnection.Close();

            button1_Click(sender, e);
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            #region old MySql
            //using (mySqlConnection)
            //{
            //    MySqlDataAdapter SDA = new MySqlDataAdapter($"select * from accounts where name = '{tbFind.Text}' or Phone = '{tbFind.Text}' ", mySqlConnection);
            //    DataTable dataTable = new DataTable();
            //    SDA.Fill(dataTable);
            //    dataGridView1.DataSource = dataTable;
            //} 
            #endregion

            mmSqlConnection.Open();
            SqlCommand cmd = new SqlCommand($"select * from [dbo].[TestTable] where name like '%{tbFind.Text}%' or phone like '%{tbFind.Text}%' or mobile like '%{tbFind.Text}%' ", mmSqlConnection);

            DataTable dataTable = new DataTable();
            dataTable.Load(cmd.ExecuteReader());

            dataGridView1.DataSource = dataTable;

            mmSqlConnection.Close();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            #region old MySql
            /*
             DialogResult dialog = MessageBox.Show("Удаляем??", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

             if (dialog == DialogResult.OK)
             {
                 using (mySqlConnection)
                 {
                     MySqlDataAdapter SDA = new MySqlDataAdapter($"DELETE FROM `accounts` WHERE Phone = '{tbFind.Text}' or Name = '{tbFind.Text}' ", mySqlConnection);
                     DataTable dataTable = new DataTable();
                     SDA.Fill(dataTable);
                 }

                 button1_Click(sender, e);
             } 
            */
            #endregion

            DialogResult dialog = MessageBox.Show("Удаляем??", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (dialog == DialogResult.OK)
            {
                mmSqlConnection.Open();

                SqlCommand cmd = new SqlCommand($"delete from dbo.TestTable where name like '%{tbFind.Text}%' or phone like '%{tbFind.Text}%' or mobile like '%{tbFind.Text}%'", mmSqlConnection);

                //await cmd.ExecuteNonQueryAsync();
                cmd.ExecuteNonQuery();

                mmSqlConnection.Close();

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
