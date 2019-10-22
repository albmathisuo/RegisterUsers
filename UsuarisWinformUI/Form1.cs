using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using Dapper;
using MySql.Data.MySqlClient;

namespace UsuarisWinformUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private const string connectionString = "Server=localhost;Database=biblioteca;Uid=usuari;Pwd=123456";
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if (PasswordtextBox.Text == PasswordctextBox.Text)
            {
                MySqlConnection con = new MySqlConnection(connectionString);
                string sql = "SELECT MAX(ID_USR) FROM USUARIS";
                int newId;
                try
                {
                    newId = con.Query<int>(sql).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    newId = 0;
                    Console.WriteLine("Warning: Exception thrown" + ex.Message);
                }
                //FirstOrDefault --> LINQ

                string sqlInsert = $"INSERT INTO USUARIS (ID_USR, DNI_USR, NOM_USR, LLINATGE1, POB_USR, EMAIL_USR, PASSWORD) VALUES" +
                $"({newId + 1},'{NIFTextBox.Text}','{NametextBox.Text}','{SurnametextBox.Text}','{TowncomboBox.Text}','{EmailtextBox.Text}','{PasswordctextBox.Text}')";
                try
                {
                    var rowsAffected = con.Execute(sqlInsert);
                    if (rowsAffected == 1)
                    {
                        MessageBox.Show("User registered correctly", "User Manager", MessageBoxButtons.OK);
                        NIFTextBox.Text = string.Empty;
                        NametextBox.Text = string.Empty;
                        SurnametextBox.Text = string.Empty;
                        TowncomboBox.Text = string.Empty;
                        PasswordctextBox.Text = string.Empty;
                        PasswordtextBox.Text = string.Empty;
                        EmailtextBox.Text = string.Empty;
                    }
                }
                catch (MySql.Data.MySqlClient.MySqlException s)
                {
                    Console.WriteLine("Warning: Exception thrown" + s.Message);
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("User not registered correctly, Passwords not matching", "Error Display", MessageBoxButtons.OK);
                PasswordctextBox.Text = string.Empty;
                PasswordtextBox.Text = string.Empty;
            }
        }
    }
}
