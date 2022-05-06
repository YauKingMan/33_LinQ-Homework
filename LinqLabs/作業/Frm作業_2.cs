using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            productPhotoTableAdapter1.Fill(awDataSet1.ProductPhoto);
            ShowYear();
        }

        private void ShowYear()
        {
            var q = from a in awDataSet1.ProductPhoto
                    group a by a.ModifiedDate.Year into newnew
                    orderby newnew.Key
                    select newnew;

            foreach (var new_key in q)
            {
                comboBox3.Items.Add(new_key.Key);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var q = from a in awDataSet1.ProductPhoto
                    orderby a.ModifiedDate
                    select a;

            dataGridView1.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime a, b;
            DateTime.TryParse(dateTimePicker1.Text, out a);
            DateTime.TryParse(dateTimePicker2.Text, out b);

            var showToView = from z in awDataSet1.ProductPhoto
                             where z.ModifiedDate >= a && z.ModifiedDate <= b
                             orderby z.ModifiedDate
                             select z;

            dataGridView1.DataSource = showToView.ToList();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int years = Convert.ToInt32(comboBox3.Text);

            var q = from a in awDataSet1.ProductPhoto
                    where a.ModifiedDate.Year == years
                    select a;

            dataGridView1.DataSource = q.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                var a = from z in awDataSet1.ProductPhoto
                        where z.ModifiedDate.Month >= 1 && z.ModifiedDate.Month <= 3
                        select z;

                dataGridView1.DataSource = a.ToList();
            }

            if (comboBox2.SelectedIndex == 1)
            {
                var b = from y in awDataSet1.ProductPhoto
                        where y.ModifiedDate.Month >= 4 && y.ModifiedDate.Month <= 6
                        select y;

                dataGridView1.DataSource = b.ToList();
            }

            if (comboBox2.SelectedIndex == 2)
            {
                var c = from x in awDataSet1.ProductPhoto
                        where x.ModifiedDate.Month >= 7 && x.ModifiedDate.Month <= 9
                        select x;

                dataGridView1.DataSource = c.ToList();
            }

            if (comboBox2.SelectedIndex == 3)
            {
                var d = from w in awDataSet1.ProductPhoto
                        where w.ModifiedDate.Month >= 10 && w.ModifiedDate.Month <= 12
                        select w;

                dataGridView1.DataSource = d.ToList();
            }
            MessageBox.Show($"共有{dataGridView1.Rows.Count}筆");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            var q = from aw in awDataSet1.ProductPhoto
                    where aw.ProductPhotoID == (int)(dataGridView1.CurrentRow.Cells[0].Value)
                    select aw.LargePhoto;

            var kit = q.FirstOrDefault();

            System.IO.MemoryStream ms = new System.IO.MemoryStream(kit);
            pictureBox1.Image = Image.FromStream(ms);
            
        }
    }
}
