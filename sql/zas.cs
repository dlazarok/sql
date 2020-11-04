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
    public partial class zasss : Form
    {
        MySQLCon conAa;
        string sRoom;
        string idClient;
        public zasss(MySQLCon conA, string room, string id)
        {
            
            InitializeComponent();
            sRoom = room;
            conAa = conA;
            idClient = id;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //заселение людей в гостиницу
            conAa.execute("INSERT INTO `asdas`.`history` (`idemployee`, `idroom`, `FIO`, `date`, `type`) VALUES('" + idClient + "', (select idroom from room where room = '" + sRoom + "'), '"+textBox1.Text+"', NOW(), 0)");
            conAa.execute("UPDATE `asdas`.`room` SET `FIO` = '" + textBox1.Text + "' WHERE `room`.`room` = '" + sRoom + "'");
            MessageBox.Show(textBox1.Text + " успешно заселен!");
            Close();
        }
    }
}
