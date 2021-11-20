using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace DBWORK
{
    public partial class Form1 : Form
    {
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = example.accdb";
        OleDbConnection connection;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new OleDbConnection(connectionString);
            connection.Open();
        }
        private void buttonSelect_Click(object sender, EventArgs e)
        {
            string commandString = "SELECT [причина ремонта] FROM [R1] WHERE [инвентарный номер] = '1к2'";
            OleDbCommand command = new OleDbCommand(commandString, connection);
            textBoxSelect.Text = command.ExecuteScalar().ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            string commandString = "SELECT * FROM [R1]";
            OleDbCommand command = new OleDbCommand(commandString, connection);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                listBoxOut.Items.Add($"{reader[0].ToString()}\t {reader[1].ToString()}\t {reader[2].ToString()}\t" +
                    $" {reader[3].ToString()}\t {reader[4].ToString()}\t {reader[5].ToString()}");
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            try
            {
                string commandString = $"INSERT INTO [R3] ([ФИО],[должность]) VALUES ('{textBoxNameIn.Text}','{textBoxWorkIn.Text}')";
                OleDbCommand command = new OleDbCommand(commandString, connection);
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string commandString = $"UPDATE [R3] SET [должность] = '{textBoxWorkUPD.Text}' WHERE [ФИО] = '{textBoxNameUPD.Text}'";
                OleDbCommand command = new OleDbCommand(commandString, connection);
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string commandString = $"DELETE FROM [R3] WHERE [ФИО] = '{textBoxNameDel.Text}'";
                OleDbCommand command = new OleDbCommand(commandString, connection);
                command.ExecuteNonQuery();
                MessageBox.Show($"{textBoxNameDel.Text} удален");
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }

        private void buttonTableSelect_Click(object sender, EventArgs e)
        {
            string commandString = "SELECT * FROM [R1]";
            OleDbDataAdapter adapter = new OleDbDataAdapter(commandString, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
    }
}
