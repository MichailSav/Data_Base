using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace DATA_C_
{
    public partial class Department : Form
    {
        public Department()
        {
            InitializeComponent();
        }
        private DataBase db;
        string id;

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2CircleButton1_MouseEnter(object sender, EventArgs e)
        {
            guna2CircleButton1.ForeColor = Color.Red;
        }

        private void guna2CircleButton1_MouseLeave(object sender, EventArgs e)
        {
            guna2CircleButton1.ForeColor = Color.White;
        }
        Point lastpoint;
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)// Возможность передвигать диологовое окно
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void Department_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var db = DataBase.GetInstance();
            var conn = db.GetConnection();

            // Проверьте, что соединение не равно null
            if (conn != null)
            {
                // Соединение уже открыто внутри метода GetConnection
                string query = "SELECT * FROM Human_Resources_Department";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        dataGridView1.Rows.Clear(); // Очистите существующие строки
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(
                                reader["ID_workers"],
                                reader["Full_Name"],
                                reader["Profession"],
                                reader["Passport_series"],
                                reader["Passport_number"]
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

        private void Ren_device_Click(object sender, EventArgs e)
        {
            Rent_device devicesForm = new Rent_device();
            devicesForm.ShowDialog();
        }

        private void Ren_position_Click(object sender, EventArgs e)
        {
            Rent_position positionForm = new Rent_position();
            positionForm.ShowDialog();
        }

        private void Ren_storage_Click(object sender, EventArgs e)
        {
            Rent_storage storageForm = new Rent_storage();
            storageForm.ShowDialog();
        }

        private void Device_Menu_Click(object sender, EventArgs e)
        {
            Rent_device deviceForm = new Rent_device(); 
            deviceForm.ShowDialog();
        }

        private void Place_menu_Click(object sender, EventArgs e)
        {
            Rent_position positionForm = new Rent_position();
            positionForm.ShowDialog();
        }

        private void Storage_Menu_Click(object sender, EventArgs e)
        {
            Rent_storage storageForm = new Rent_storage();
            storageForm.ShowDialog();
        }

        private void Renters_Menu_Click_1(object sender, EventArgs e)
        {
            Renters renterForm = new Renters();
            renterForm.ShowDialog();
        }

        private void Products_Menu_Click(object sender, EventArgs e)
        {
            Products productForm = new Products();
            productForm.ShowDialog();
        }

        private void Lessers_Menu_Click(object sender, EventArgs e)
        {
            Lessers lesserForm = new Lessers();
            lesserForm.ShowDialog();
        }

        private void SanitarControl_Menu_Click(object sender, EventArgs e)
        {
            Sanitary_control sanitarForm = new Sanitary_control();
            sanitarForm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }



        private void Add_Click(object sender, EventArgs e)
        {
            string name = guna2TextBox4.Text;
            string profes = guna2TextBox5.Text;
            string seria = guna2TextBox1.Text;
            string numbers = guna2TextBox2.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(profes) || string.IsNullOrEmpty(seria) || string.IsNullOrEmpty(numbers))
            {
                MessageBox.Show("Заполните все поля.");
            }
            else if (!Regex.IsMatch(profes, "^[а-яА-Я -]+$"))
            {
                MessageBox.Show("Заполните данные об профессии.");
            }
            else if (!Regex.IsMatch(seria, "^[а-яА-Я0-9 .-]+$"))
            {
                MessageBox.Show("Заполните серию пспорта.");
            }
            else if (!Regex.IsMatch(name, "^[а-яА-Я -]+$"))
            {
                MessageBox.Show("Заполните ФИО правильно.");
            }
            else if (!Regex.IsMatch(numbers, "^[0-9 .()+-]+$"))
            {
                MessageBox.Show("Заполните номер паспорта.");
            }
            else
            {
                db = DataBase.GetInstance();
                SQLiteConnection conn = db.GetConnection();
                string query = "INSERT INTO Human_Resources_Department (Full_Name, Profession, Passport_series, Passport_number) VALUES (@name, @profes, @seria, @numbers)";
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.Parameters.AddWithValue("@profes", profes);
                command.Parameters.AddWithValue("@seria", seria);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@numbers", numbers);
                command.ExecuteNonQuery();
                MessageBox.Show("Запись добавлена в базу!");

                LoadData();
            }

          //  Department_Load();
        }

        private void Del_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                db = DataBase.GetInstance();
                SQLiteConnection conn = db.GetConnection();
                string id = dataGridView1.SelectedRows[0].Cells["ID_workers"].Value.ToString();
                string sql = "DELETE FROM Human_Resources_Department WHERE ID_workers = @id";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                dataGridView1.Refresh();
                MessageBox.Show("Запись удалена из базы!");
                LoadData();
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления!");
            }
        }

        private void Redact_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox4.Text) || string.IsNullOrEmpty(guna2TextBox5.Text) || string.IsNullOrEmpty(guna2TextBox1.Text) || string.IsNullOrEmpty(guna2TextBox2.Text))
            {
                MessageBox.Show("Заполните редактируемые поля!");
            }
            else if (!Regex.IsMatch(guna2TextBox4.Text, "^[а-яА-Я -]+$"))
            {
                MessageBox.Show("Заполните данные ФИО правильно.");
            }
            else if (!Regex.IsMatch(guna2TextBox5.Text, "^[а-яА-Я .-]+$"))
            {
                MessageBox.Show("Заполните данные професси.");
            }
            else if (!Regex.IsMatch(guna2TextBox1.Text, "^[0-9 .-]+$"))
            {
                MessageBox.Show("Заполните серию паспорта.");
            }
            else if (!Regex.IsMatch(guna2TextBox2.Text, "^[0-9 .]+$"))
            {
                MessageBox.Show("Заполните номер паспорта.");
            }
            else
            {
                // Проверка, выбрана ли строка в DataGridView
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Получение выбранной строки из DataGridView
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    string id = selectedRow.Cells["ID_workers"].Value.ToString();

                    // Получение экземпляра базы данных и соединения
                    db = DataBase.GetInstance();
                    SQLiteConnection conn = db.GetConnection();

                    // SQL запрос для обновления данных
                    string query = "UPDATE Human_Resources_Department SET Full_Name = @name, Profession = @pos, Passport_series = @ser, Passport_number = @num WHERE ID_workers = @id";
                    SQLiteCommand command = new SQLiteCommand(query, conn);

                    // Добавление параметров
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", guna2TextBox4.Text);
                    command.Parameters.AddWithValue("@pos", guna2TextBox5.Text);
                    command.Parameters.AddWithValue("@ser", guna2TextBox1.Text);
                    command.Parameters.AddWithValue("@num", guna2TextBox2.Text);

                    // Выполнение запроса
                    command.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно изменена!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Выберите строку для редактирования.");
                }
            }

        }
    }
}
