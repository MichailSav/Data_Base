using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DATA_C_
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }
        Point lastpoint;
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)// Возможность передвигать диологовое окно
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void guna2CircleButton1_MouseEnter(object sender, EventArgs e)
        {
            guna2CircleButton1.ForeColor = Color.Red;
        }

        private void guna2CircleButton1_MouseLeave(object sender, EventArgs e)
        {
            guna2CircleButton1.ForeColor = Color.White;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close(); //Закрытие диологовое окно
        }

        private void Products_Load(object sender, EventArgs e)
        {
            var db = DataBase.GetInstance();
            var conn = db.GetConnection();

            // Проверьте, что соединение не равно null
            if (conn != null)
            {
                // Соединение уже открыто внутри метода GetConnection
                string query = "SELECT * FROM Products";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(
                                reader["ID_product"],
                                reader["Product_name"],
                                reader["ID_renter"],
                                reader["License"],
                                reader["Valid_from"],
                                reader["Valid_untill"]
                            );
                        }
                    }
                }
                db.CloseConnection(); // Закрываем соединение после выполнения запроса
            }
            else
            {
                MessageBox.Show("Failed to establish a connection to the database.");
            }
        }
    }
}
