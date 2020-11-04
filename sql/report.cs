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
    public partial class report : Form
    {
        MySQLCon conAa;
        public report(MySQLCon conA)
        {
            conAa = conA;
            InitializeComponent();
            try
            {
                comboBox1.Items.Clear();
                int n = 0;
                string[][] ka = conAa.getValues("SELECT idroom FROM `room`"); //включение всех комнат в комбо бокс
                while (true)
                    comboBox1.Items.Add(ka[n++][0]);
            }
            catch { }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex != -1)
            { //оставление отзыва
                conAa.execute("INSERT INTO `asdas`.`report` (`idroom`, `date`, `status`, `comment`) VALUES ('"+comboBox1.Text+"', NOW(), '0', '"+richTextBox1.Text+"')");
                MessageBox.Show("Отзыв успешно оставлен!");
                Close();
            }
        }
    }
}
