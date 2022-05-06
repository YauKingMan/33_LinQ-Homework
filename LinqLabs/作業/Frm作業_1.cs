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
    public partial class Frm作業_1 : Form
    {
        //搖滾區
        int count = 0;
       

        public Frm作業_1()
        {
            InitializeComponent();
            SelectYearToCombobox();
            ordersTableAdapter1.Fill(nwDataSet1.Orders);
            order_DetailsTableAdapter1.Fill(nwDataSet1.Order_Details);
            productsTableAdapter1.Fill(nwDataSet1.Products);
        }


        private void SelectYearToCombobox()
        {
            ordersTableAdapter1.Fill(nwDataSet1.Orders);



            var q = from a in nwDataSet1.Orders
                    group a by a.OrderDate.Year into newnew
                    orderby newnew.Key
                    select newnew;

            foreach (var new_key in q)
            {
                comboBox1.Items.Add(new_key.Key);
            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        int page = 1;
        private void button13_Click(object sender, EventArgs e)
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)
            //Distinct()
            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();

            bool f = int.TryParse(textBox1.Text, out int b);
            if (!f)
            {
                MessageBox.Show("請輸入數字");
            }
            else
            {
                int show_num = Convert.ToInt32(textBox1.Text);


                var q = from a in nwDataSet1.Products.Skip(count * show_num).Take(show_num)
                            //where !a.IsShippedDateNull() /*&& !a.IsShipRegionNull()*/ && !a.IsShipPostalCodeNull()
                        select a;

                dataGridView1.DataSource = q.ToList();
                count += 1;

                int max = nwDataSet1.Products.Count;//77
                if (count > max/show_num)
                {
                    MessageBox.Show("已到最後一頁");
                }

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = nwDataSet1.Orders;
            dataGridView2.Columns.Clear();

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from b in files
                    where b.Extension.Contains("log")
                    select b;

            this.dataGridView1.DataSource = q.ToList();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = nwDataSet1.Orders;
            dataGridView2.Columns.Clear();
            //int show_num = Convert.ToInt32(textBox1.Text);
            //dataGridView2.DataSource = nwDataSet1.Order_Details;

            var q = from a in nwDataSet1.Orders/*.Take(show_num)*/
                    where !a.IsShippedDateNull() && !a.IsShipRegionNull() && !a.IsShipPostalCodeNull()
                    select a;

            //var p = from b in nwDataSet1.Order_Details
            //        select b;

            dataGridView1.DataSource = q.ToList();
            //dataGridView1.RowCount = Convert.ToInt32(textBox1.Text);
            //dataGridView2.DataSource = p.ToList();
        }


        private void button1_Click(object sender, EventArgs e)
        {
        https://drive.google.com/drive/folders/1J22H_BV6AulSmFITr46yXBOexkf0cGLr?usp=sharing//int i = comboBox1.SelectedIndex;
            //int a = Convert.ToInt32(comboBox1.Items[i]);



            var q = from b in nwDataSet1.Orders
                    where !b.IsShippedDateNull() && !b.IsShipRegionNull() && b.OrderDate.Year.ToString() == comboBox1.Text
                    select b;

            dataGridView1.DataSource = q.ToList();

            var p = from z in nwDataSet1.Order_Details
                    join or in nwDataSet1.Orders on z.OrderID equals or.OrderID
                    where !or.IsShippedDateNull() && !or.IsShipRegionNull() && or.OrderDate.Year.ToString() == comboBox1.Text
                    select z;

            dataGridView2.DataSource = p.ToList();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from b in files
                    where b.Length > 10000
                    orderby b.Length
                    select b;

            dataGridView1.DataSource = q.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from b in files
                    where b.CreationTime.Year >= 2017
                    orderby b.CreationTime.Year
                    select b;
            dataGridView1.DataSource = q.ToList();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var q = from od in nwDataSet1.Order_Details
                    where od.OrderID == (int)(dataGridView1.CurrentRow.Cells[0].Value)
                    select od;

            dataGridView2.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.Columns.Clear();
            lblMaster.Text = "產品";

            //int show_num = Convert.ToInt32(textBox1.Text);


            var q = from a in nwDataSet1.Products
                        //where !a.IsShippedDateNull() /*&& !a.IsShipRegionNull()*/ && !a.IsShipPostalCodeNull()
                    select a;

            dataGridView1.DataSource = q.ToList();
        }

        //起始條件 復歸設定
        private void button12_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();
            bool f = int.TryParse(textBox1.Text, out int b);
            if (!f || textBox1.Text == "")
            {
                MessageBox.Show("請輸入數字");
            }
            else
            {
                if (count <= 1)
                {
                    MessageBox.Show("無上一頁");
                }
                else
                {
                    count -= 1;
                    dataGridView2.Columns.Clear();
                    int show_num = Convert.ToInt32(textBox1.Text);

                    var q = from a in nwDataSet1.Products.Skip((count - 1) * show_num).Take(show_num)
                                //where !a.IsShippedDateNull() /*&& !a.IsShipRegionNull()*/ && !a.IsShipPostalCodeNull()
                            select a;

                    dataGridView1.DataSource = q.ToList();
                }                             
            }
        }
    }
}
