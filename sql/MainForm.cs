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

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        MySQLCon conAa;
        string clientType;
        string idClient;
        public MainForm(string id, MySQLCon conA)
        {
            InitializeComponent();
            conAa = conA;
            idClient = id;
            string [][] name = conAa.getValues("select FIO, type from employee where idemployee = '" + id + "'"); //сбор данных о клиенте
            this.Text = "Добро пожаловать, " + name[0][0] + "!"; // добро пожаловать ФИО
            clientType = name[0][1]; //должность клиента
            if (name[0][1] == "2") //доступ к функционалу работника ресепшна
            {
                label1.Text = "Должность: Работник ресепшна";
                
            }
            else if(name[0][1] == "3") //доступ к функционалу администратора
            {
                label1.Text = "Должность: Администратор";
                button7.Visible = true;
            }
            else if (name[0][1] == "1") //доступ к функционалу сервисного работника
            {
                label1.Text = "Должность: Сервисный работник";
                button2.Visible = false;
                button3.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                button1.Text = "Взять заказ";
            }
            reload();
        }
        private void updateDatagrid(string querryLeftDG, string querryRightDG)
        {
            if (querryRightDG == "") //количество аргментов определяет количество датагридов
            {
                dataGridView1.Size = new Size(667, 348);  
                dataGridView2.Visible = false;
                conAa.getDb().Open();  
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(querryLeftDG, conAa.getDb());
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);
                dataGridView1.DataSource = DS.Tables[0];

                //close connection
                conAa.getDb().Close();
            }
            else
            {
                conAa.getDb().Open();
                dataGridView2.Visible = true;
                dataGridView1.Size = new Size(371, 348);
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(querryLeftDG, conAa.getDb());
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);
                dataGridView1.DataSource = DS.Tables[0];
                
                mySqlDataAdapter = new MySqlDataAdapter(querryRightDG, conAa.getDb());
                DS = new DataSet();
                mySqlDataAdapter.Fill(DS);
                dataGridView2.DataSource = DS.Tables[0];

                //close connection
                conAa.getDb().Close();
            }
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[1].Value.ToString() == "Свободна")
            {
                new zasss(conAa, dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), idClient).ShowDialog();
                reload(); //заселение 
            }
            else
            {
                MessageBox.Show("Комната уже сдается!");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            new Бронирование(conAa, idClient).ShowDialog(); //открытие бронирования
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            reload();
        }
        private void reload()
        {
            if (clientType == "3" || clientType == "2")  //обновление таблиц
                updateDatagrid("SELECT bron.FIO `ФИО`, dstart `Заселение`, dend `Выселение`, room `Комната` FROM `bron` inner join room using(idroom)", "SELECT room `Комната`, IFNULL(FIO,'Свободна') `Гость` FROM `room`");
            else
                updateDatagrid("SELECT idservice `Номер заказа`, room `Комната`, comment `Сервис` FROM `service` inner join room using(idroom) where idemployee is NULL", "");
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((form1)Tag).Close(); //закрытие приложения
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            new Сервис(conAa).ShowDialog();//открытие сервиса
        }

        private void Button5_Click(object sender, EventArgs e)
        {//выселение 
            if (dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[1].Value.ToString() != "Свободна")
            {
                conAa.execute("INSERT INTO `asdas`.`history` (`idemployee`, `idroom`, `FIO`, `date`, `type`) VALUES('" + idClient + "', (select idroom from room where room = '" + dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + "'), (select FIO from room where room = '" + dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + "'), NOW(), '1')");
                conAa.execute("UPDATE `asdas`.`room` SET `FIO` = NULL WHERE `room`.`room` = '" + dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[0].Value.ToString()+"'");
                MessageBox.Show(dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[1].Value.ToString() + " успешно выселен!");
                reload();
            }
            else
            {
                MessageBox.Show("Комната не сдается!");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(clientType == "2" || clientType == "3")
                new report(conAa).ShowDialog(); //отзыв
            else
            {
                try
                {
                    conAa.execute("UPDATE `asdas`.`service` SET `idemployee` = '" + clientType + "' WHERE `idservice` = '" + dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + "'");
                }
                catch { }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            new checkReports(conAa).ShowDialog(); //проверка репортов
        }
    }
}
