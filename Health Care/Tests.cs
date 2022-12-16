using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Health_Care
{
    public partial class Tests : Form
    {
        Functions Con;
        public Tests()
        {
            InitializeComponent();
            Con = new Functions();
            ShowTest();
        }

        private void ShowTest()
        {
            string Query = "Select * from TestTb1";
            TestsList.DataSource = Con.GetData(Query);
        }

        private void Clear()
        {
            TNameTb.Text = "";
            TCostTb.Text = "";
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (TNameTb.Text == "" || TCostTb.Text == "")
            {
                MessageBox.Show("Missing Data!!");
            }
            else
            {
                string TName = TNameTb.Text;
                int Cost = Convert.ToInt32(TCostTb.Text);
                string Query = "insert into TestTb1 values('{0}','{1}')";
                Query = string.Format(Query, TName, Cost);
                Con.SetData(Query);
                ShowTest();
                Clear();
                MessageBox.Show("Test Added!");
            }
        }

        int Key = 0;
        private void TestsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TNameTb.Text = TestsList.SelectedRows[0].Cells[1].Value.ToString();
            TCostTb.Text = TestsList.SelectedRows[0].Cells[2].Value.ToString();
            if (TNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(TestsList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (TNameTb.Text == "" || TCostTb.Text == "")
            {
                MessageBox.Show("Missing Data!!");
            }
            else
            {
                string TName = TNameTb.Text;
                int Cost = Convert.ToInt32(TCostTb.Text);
                string Query = "Update TestTb1 set TestName = '{0}',TestCost = {1} where TestCode = {2}";
                Query = string.Format(Query, TName, Cost,Key);
                Con.SetData(Query);
                ShowTest();
                Clear();
                MessageBox.Show("Test Updated!");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Test!");
            }
            else
            {
                string TName = TNameTb.Text;
                int Cost = Convert.ToInt32(TCostTb.Text);
                string Query = "Delete from TestTb1 where TestCode = {0}";
                Query = string.Format(Query,Key);
                Con.SetData(Query);
                ShowTest();
                Clear();
                MessageBox.Show("Test Deleted!");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Patients Obj = new Patients();
            Obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Tests Obj = new Tests();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            diagnostic Obj = new diagnostic();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }
    }
}
