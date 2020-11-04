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
    public partial class Бронирование : Form
    {
        MySQLCon conAa;
        string idClient;
        public Бронирование(MySQLCon conA, string id)
        {
            idClient = id;
            InitializeComponent();
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss"; //формат удобный для передачи в майскл
            dateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            conAa = conA;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                comboBox1.Items.Clear();
                int n = 0;//проверка доступных для брони номеров
                string[][] ka = conAa.getValues("SELECT idroom FROM `bron` WHERE '" + dateTimePicker1.Value + "' not BETWEEN DATE(dstart) AND DATE(dend) and '" + dateTimePicker2.Value + "' not BETWEEN DATE(dstart) AND DATE(dend)");
                while (true)
                    comboBox1.Items.Add(ka[n++][0]);
            }
            catch
            {

            }
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.SelectedIndex != -1)//если поля заполнены
            {//добавление брони
                conAa.execute("INSERT INTO `asdas`.`bron` (`idroom`, `idemployee`, `FIO`, `dstart`, `dend`) VALUES('" + comboBox1.Text + "', '" + idClient + "', '" + textBox1.Text + "', '" + dateTimePicker1.Text + "', '" + dateTimePicker2.Text + "')");
                MessageBox.Show("Место успешно забронировано!");
                Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }
    }
}
