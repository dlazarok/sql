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
    public partial class checkReports : Form
    {
        MySQLCon conAa;
        public checkReports(MySQLCon conA)
        {
            conAa = conA;
            InitializeComponent();
            reload();
        }
        private void reload()
        {
            conAa.getDb().Open();//показывает все отзывы с сортировкой от нерассмотренных
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("SELECT idreport `ID отзыва`, date `Дата написания`, if(status = 1, 'Рассмотрен', 'Не рассмотрен') `Статус`, comment `Отзыв` frOM `report` order by status ASC", conAa.getDb());
            DataSet DS = new DataSet();
            mySqlDataAdapter.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            conAa.getDb().Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        { //рассмотр выбранной жалобы
            conAa.execute("UPDATE `asdas`.`report` SET `status` = '1' WHERE `report`.`idreport` = '" + dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + "'");
            reload();
        }
    }
}
