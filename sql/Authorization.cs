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

namespace WindowsFormsApp1
{
    public partial class form1 : Form
    {
        MySQLCon conAa;
        public form1()
        {
            InitializeComponent();
            conAa = new MySQLCon();
            conAa.DBConnect("82.202.245.156", "asdas", "user83841", "S0n3T6n9"); //строка подключения
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text; //логин
            string password = textBox2.Text; //пароль
            string id = conAa.getValue("select ifnull((Select idauth from auth where login = '" + textBox1.Text + "' and pass = '" + textBox2.Text + "'), -1)"); 
            if (id != "-1") // -1 - такого пользователя не существует остальные значение - id клиента
            {
                MainForm ds = new MainForm(id, conAa);//создание основной комната
                ds.Tag = this; //передача текущей формы для последующего её закрытия
                ds.Show(); //показ формы
                Hide();//скрытие формы авторизации
            }
            else
                MessageBox.Show("Такого пользователя нет!");
        }
    }
}
