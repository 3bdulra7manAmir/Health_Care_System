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
    public partial class diagnostic : Form
    {
        Functions Con;
        public diagnostic()
        {
            InitializeComponent();
            Con = new Functions();
            Getpatients();
            GetTest();
            ShowDiagnosis();
        }


        private void GetCost()
        {
            string Query = "Select * TestTb1 where TestCode = {0}";
            Query = string.Format(Query, TestCb.SelectedValue.ToString());
            foreach (DataRow dr in Con.GetData(Query).Rows)
            {
                CostTb.Text = dr["TestCost"].ToString();
            }
        }

        private void ShowDiagnosis()
        {
            string Query = "select * from diagnosisTb1";
            DiagnosisList.DataSource = Con.GetData(Query);
        }

        private void Getpatients()
        {
            string Query = "Select * from Table";
            PatientCb.DisplayMember = Con.GetData(Query).Columns["PatName"].ToString();
            PatientCb.ValueMember = Con.GetData(Query).Columns["PatCode"].ToString();
            PatientCb.DataSource = Con.GetData(Query);
        }

        private void GetTest()
        {
            string Query = "Select * from TestTb1";
            TestCb.DisplayMember = Con.GetData(Query).Columns["TestName"].ToString();
            TestCb.ValueMember = Con.GetData(Query).Columns["TestCode"].ToString();
            TestCb.DataSource = Con.GetData(Query);
        }

        private void Clear()
        {
            CostTb.Text = "";
            ResultTb.Text = "";
            TestCb.SelectedIndex = -1;
            PatientCb.SelectedIndex = -1;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (PatientCb.SelectedIndex == -1 || CostTb.Text == "" || ResultTb.Text == "")
            {
                MessageBox.Show("Missing Data!!");
            }
            else
            {
                string DDate = DiagDateTb.Value.Date.ToString();
                int Patient = Convert.ToInt32(PatientCb.SelectedValue.ToString());
                int Test = Convert.ToInt32(TestCb.SelectedValue.ToString());
                int Cost = Convert.ToInt32(CostTb.Text);
                string Result = ResultTb.Text;
                string Query = "insert into diagnosisTb1 values('{0}','{1}','{2}','{3}','{4}')";
                Query = string.Format(Query, DDate, Patient, Test, Cost, Result);
                Con.SetData(Query);
                ShowDiagnosis();
                Clear();
                MessageBox.Show("Diagnosis Added!");
            }
        }

        private void TestCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCost();
        }

        int Key = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DiagDateTb.Text = DiagnosisList.SelectedRows[0].Cells[1].Value.ToString();
            PatientCb.SelectedItem = DiagnosisList.SelectedRows[0].Cells[2].Value.ToString();
            TestCb.Text = DiagnosisList.SelectedRows[0].Cells[3].Value.ToString();
            CostTb.Text = DiagnosisList.SelectedRows[0].Cells[4].Value.ToString();
            ResultTb.Text = DiagnosisList.SelectedRows[0].Cells[5].Value.ToString();
            if (CostTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(DiagnosisList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Missing Data!!");
            }
            else
            {
                string Query = "Delete from DiagnosisTb1 where DiagCode = {0}";
                Query = string.Format(Query, Key);
                Con.SetData(Query);
                ShowDiagnosis();
                Clear();
                MessageBox.Show("Diagnosis Delete!");
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (PatientCb.SelectedIndex == -1 || CostTb.Text == "" || ResultTb.Text == "")
            {
                MessageBox.Show("Missing Data!!");
            }
            else
            {
                int Patient = Convert.ToInt32(PatientCb.SelectedValue.ToString());
                int Test = Convert.ToInt32(TestCb.SelectedValue.ToString());
                int Cost = Convert.ToInt32(CostTb.Text);
                string DDate = DiagDateTb.Value.Date.ToString();
                string Result = ResultTb.Text;
                string Query = "update DiagnosisTb1 set DiagDate = '{0}',Patient = '{1}',Test = '{2}',Cost = '{3}',Result = '{4}' where DiagCode = {5}";
                Query = string.Format(Query, Patient, Test, DDate, Cost, Result, Key);
                Con.SetData(Query);
                ShowDiagnosis();
                Clear();
                MessageBox.Show("Diagnosis Updated!");
            }
        }
    }
}
