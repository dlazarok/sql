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
    public partial class Сервис : Form
    {
        MySQLCon conAa;
        public Сервис(MySQLCon conA)
        {
            conAa = conA;
            
            InitializeComponent();
            try
            {
                comboBox1.Items.Clear();
                int n = 0; //включение комнат для сервиса
                string[][] ka = conAa.getValues("SELECT room FROM `room` WHERE FIO is not NULL");
                while (true)
                    comboBox1.Items.Add(ka[n++][0]);
            }
            catch
            {

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && richTextBox1.Text.Length > 10) { //добавление заказа на сервис
                conAa.execute("INSERT INTO `asdas`.`service` (`idemployee`, `idroom`, `comment`) VALUES(NULL, (Select idroom from room where room = '"+ comboBox1.SelectedItem.ToString()+ "'), '"+richTextBox1.Text+"')");
                MessageBox.Show("Сервис успешно добавлен!!");
                Close();
            }
        }
    }
}
