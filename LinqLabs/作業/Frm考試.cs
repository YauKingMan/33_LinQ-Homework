using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs
{
    public partial class Frm考試 : Form
    {
        public Frm考試()
        {
            InitializeComponent();

            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

                                          };
        }

        List<Student> students_scores;

        public class Student
        {
            public string Name { get; set; }
            public string Class { get;  set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get;  set; }
            public string Gender { get; set; }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            #region 搜尋 班級學生成績

            // 
            // 共幾個 學員成績 ?						
            MessageBox.Show("共"+students_scores.Count()+"個學員");

            // 找出 前面三個 的學員所有科目成績
            //var q = (from p in students_scores
            //         select p).Take(3);
            //dataGridView1.DataSource = q.ToList();

            // 找出 後面兩個 的學員所有科目成績					
            //var q = (from p in students_scores.Skip(students_scores.Count()-2)
            //         select p).Take(2);
            //dataGridView1.DataSource = q.ToList();

            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績
            //var q = from p in students_scores
            //        where p.Name == "aaa" || p.Name == "bbb" || p.Name == "ccc"
            //        select new
            //        {
            //            p.Name,
            //            p.Chi,
            //            p.Eng
            //        };
            //dataGridView1.DataSource = q.ToList();

            // 找出學員 'bbb' 的成績	                          
            //var q = from p in students_scores
            //        where p.Name == "bbb"
            //        select p;

            //dataGridView1.DataSource = q.ToList();

            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	
            //var q = from p in students_scores
            //        where p.Name != "bbb"
            //        select p;
            //dataGridView1.DataSource = q.ToList();

            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績
            //var q = from p in students_scores
            //        where p.Name == "aaa" || p.Name == "bbb" || p.Name == "ccc"
            //        select new
            //        {
            //            p.Name,
            //            p.Chi,
            //            p.Math
            //        };
            //dataGridView1.DataSource = q.ToList();

            // 數學不及格 ... 是誰 
            var q = from p in students_scores
                    where p.Math < 60
                    select p;
            dataGridView1.DataSource = q.ToList();
            #endregion

        }

        private void button37_Click(object sender, EventArgs e)
        {
            //個人 sum, min, max, avg

            //各科 sum, min, max, avg
        }
        private void button33_Click(object sender, EventArgs e)
        {
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
        }

        private void button35_Click(object sender, EventArgs e)
        {
            // 統計 :　所有隨機分數出現的次數/比率; sort ascending or descending
            // 63     7.00%
            // 100    6.00%
            // 78     6.00%
            // 89     5.00%
            // 83     5.00%
            // 61     4.00%
            // 64     4.00%
            // 91     4.00%
            // 79     4.00%
            // 84     3.00%
            // 62     3.00%
            // 73     3.00%
            // 74     3.00%
            // 75     3.00%
        }

        private void button34_Click(object sender, EventArgs e)
        {
            // 年度最高銷售金額 年度最低銷售金額
            // 那一年總銷售最好 ? 那一年總銷售最不好 ?  
            // 那一個月總銷售最好 ? 那一個月總銷售最不好 ?

            // 每年 總銷售分析 圖
            // 每月 總銷售分析 圖
        }

      
    }
}
