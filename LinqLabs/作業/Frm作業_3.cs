using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
        }
        //搖滾區
        NorthwindEntities dbContext = new NorthwindEntities();
        IEnumerable<FileInfo> so9sad;
        IEnumerable<Order> so9sad_order;
        IEnumerable<Product> so9sad_Product;
        int flag;
        void AllClean()
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            treeView1.Nodes.Clear();
        }
        private string MyNumsKey(int n)
        {
            if (n <= 4)
            {
                return "Small";
            }

            if (n <= 8)
            {
                return "Medium";
            }
            else
            {
                return "Large";
            }
        }

        private string MyUnitPriceKey(Product n)
        {
            if (n.UnitPrice <= 30)
            {
                return "Small";
            }

            if (n.UnitPrice <= 70)
            {
                return "Medium";
            }
            else
            {
                return "Large";
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            AllClean();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            TreeNode node = null;

            foreach (int n in nums)
            {
                if (treeView1.Nodes[MyNumsKey(n)] == null)
                {
                    node = treeView1.Nodes.Add(MyNumsKey(n), MyNumsKey(n));
                    node.Nodes.Add(n.ToString());
                }
                else
                {
                    node.Nodes.Add(n.ToString());
                }
            }
        }


        private string MyKey(FileInfo i)
        {
            if (i.Length < 10000)
            {
                return ("Small");
            }

            if (i.Length < 20000)
            {
                return ("Medium");
            }
            else
            {
                return ("Large");
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            flag = 1;

            var q = from p in files
                    orderby p.Length
                    group p by MyKey(p) into g
                    select new
                    {
                        MyKey = g.Key,
                        MyCount = g.Count(),
                        MyGroup = g
                    };
            dataGridView1.DataSource = q.ToList();
            so9sad = files.Where(f => MyKey(f) == dataGridView1.CurrentRow.Cells[0].Value.ToString()).OrderByDescending(f => f.Length);

            //===========================================================================
            //TreeView
            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.MyKey}({group.MyCount})";
                TreeNode node = treeView1.Nodes.Add(group.MyKey, s);

                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            flag = 1;

            var q = from p2 in files
                    orderby p2.CreationTime.Year
                    group p2 by p2.CreationTime.Year into g
                    select new
                    {
                        MyKey = g.Key,
                        MyCount = g.Count(),
                        MyGroup = g
                    };
            dataGridView1.DataSource = q.ToList();

            so9sad = files.Where(f => f.CreationTime.Year == (int)(dataGridView1.CurrentRow.Cells[0].Value)).OrderByDescending(f => f.CreationTime.Year);

            //===========================================================================
            //TreeView
            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.MyKey}({group.MyCount})";
                TreeNode node = treeView1.Nodes.Add(group.MyKey.ToString(), s);

                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
        }



        private void button8_Click(object sender, EventArgs e)
        {
            AllClean();
            flag = 2;
            //var q = dbContext.Products.AsEnumerable().GroupBy(p => MyUnitPriceKey(p)).Select(g => new
            //{
            //    Key = g.Key,
            //    Count = g.Count(),
            //    Group = g
            //});
            var q = from p in dbContext.Products.AsEnumerable()
                    group p by MyUnitPriceKey(p) into g
                    select new
                    {
                        MyKey = g.Key,
                        MyCount = g.Count(),
                        MyGroup = g
                    };
            dataGridView1.DataSource = q.ToList();
            so9sad_Product = dbContext.Products.AsEnumerable().Where(p => MyUnitPriceKey(p) == dataGridView1.CurrentRow.Cells[0].Value.ToString());
            
            //TREEVIEW
            foreach (var group in q)
            {
                string s = $"{group.MyKey}({group.MyCount})";
                TreeNode node = treeView1.Nodes.Add(group.MyKey.ToString(), s);
                foreach (var items in group.MyGroup)
                {
                    node.Nodes.Add(items.ProductName);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (flag == 1)
            {
                dataGridView2.DataSource = so9sad.ToList();
            }
            if (flag == 2)
            {
                dataGridView2.DataSource = so9sad_Product.ToList();
            }
            if (flag == 3)
            {
                dataGridView2.DataSource = so9sad_order.ToList();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            AllClean();
            flag = 3;
            var q = dbContext.Orders.AsEnumerable().GroupBy(o => o.OrderDate.Value.Year).Select(g => new
            {
                Year = g.Key,
                MyCount = g.Count(),
                MyGroup = g
            });
            dataGridView1.DataSource = q.ToList();
            
            so9sad_order = dbContext.Orders.AsEnumerable().Where(o => o.OrderDate.Value.Year == (int)(dataGridView1.CurrentRow.Cells[0].Value));
         

            foreach (var group in q)
            {
                string s = $"{group.Year}({group.MyCount})";
                TreeNode node = treeView1.Nodes.Add(group.Year.ToString(), s);
                foreach (var items in group.MyGroup)
                {
                    node.Nodes.Add(items.OrderID.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AllClean();
            //float SumTotalPrice = dbContext.Order_Details.AsEnumerable().Sum(od => (float)od.UnitPrice * od.Quantity * (1 - od.Discount));
            var q = (from p in dbContext.Order_Details.AsEnumerable()
                    select ((float)p.UnitPrice * p.Quantity * (1 - p.Discount))).Sum();                   

            MessageBox.Show($"總銷售金額{q}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AllClean();
            var q = (from p in dbContext.Order_Details.AsEnumerable()
                    group p by $"{p.Order.Employee.LastName} {p.Order.Employee.FirstName}" into g
                    let a = g.Sum(p => (float)p.UnitPrice * p.Quantity * (1 - p.Discount))
                    orderby a descending
                    select new
                    {
                        Name = g.Key,
                        TotalPrice = $"{a:C2}"
                    }).Take(5);
            dataGridView1.DataSource = q.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var q = (from p in dbContext.Products.AsEnumerable()
                    orderby p.UnitPrice descending
                    select new
                    {
                        p.Category.CategoryName,
                        p.ProductID,
                        p.ProductName
                    }).Take(5);
            dataGridView1.DataSource = q.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var q = (from p in dbContext.Products
                     where p.UnitPrice > 300
                     select p).Any();

            if (q == false)
            {
                MessageBox.Show("NW產品沒有任何一筆單價大於300");
            }
            else
            {
                MessageBox.Show("NW產品有任何一筆單價大於300");
            }
        }
    }


}
