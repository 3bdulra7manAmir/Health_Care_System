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
    public partial class Patients : Form
    {
        Functions Con;
        public Patients()
        {
            InitializeComponent();
            Con = new Functions();
            ShowPatients();
        }

        private void ShowPatients()
        {
            string Query = "Select * from Table";
            PatientsList.DataSource = Con.GetData(Query);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(PatNameTb.Text == "" || PatPhoneTb.Text == "" || PatAddTb.Text == "" || GenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Data!!");
            }
            else
            {
                string Patient = PatNameTb.Text;
                string Gender = GenCb.SelectedItem.ToString();
                string BDate = DOBTb.Value.Date.ToString();
                string Phone = PatPhoneTb.Text;
                string Address = PatAddTb.Text;
                string Query = "insert into Table values('{0}','{1}','{2}','{3}','{4}')";
                Query = string.Format(Query, Patient, Gender, BDate, Phone);
                Con.SetData(Query);
                ShowPatients();
                Clear();
                MessageBox.Show("Patient Added!");
            }
        }
        int Key = 0;
        private void PatientsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PatNameTb.Text = PatientsList.SelectedRows[0].Cells[1].Value.ToString();
            GenCb.SelectedItem = PatientsList.SelectedRows[0].Cells[2].Value.ToString();
            DOBTb.Text = PatientsList.SelectedRows[0].Cells[3].Value.ToString();
            PatPhoneTb.Text = PatientsList.SelectedRows[0].Cells[4].Value.ToString();
            PatAddTb.Text = PatientsList.SelectedRows[0].Cells[5].Value.ToString();
            if(PatNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(PatientsList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (PatNameTb.Text == "" || PatPhoneTb.Text == "" || PatAddTb.Text == "" || GenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Data!!");
            }
            else
            {
                string Patient = PatNameTb.Text;
                string Gender = GenCb.SelectedItem.ToString();
                string DDate = DOBTb.Value.Date.ToString();
                string Phone = PatPhoneTb.Text;
                string Address = PatAddTb.Text;
                string Query = "update Table set PatName = '{0}',PatGen = '{1}',PatDob = '{2}',PatPhone = '{3}',PatAdd = '{4}' where PatCode = {5}";
                Query = string.Format(Query, Patient, Gender, DDate, Phone, Address,Key);
                Con.SetData(Query);
                ShowPatients();
                Clear();
                MessageBox.Show("Patient Updated!");
            }
        }

        private void Clear()
        {
            PatNameTb.Text = "";
            GenCb.SelectedIndex = -1;
            PatPhoneTb.Text = "";
            PatAddTb.Text = "";
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Patient!");
            }
            else
            {
                string Query = "Delete from Table where PatCode = {0}";
                Query = string.Format(Query,Key);
                Con.SetData(Query);
                ShowPatients();
                Clear();
                MessageBox.Show("Patient Deleted!");
            }
        }
    }
}
