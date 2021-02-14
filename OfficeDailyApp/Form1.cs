using OfficeDailyApp.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OfficeDailyApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

      

        private DataTable GetDataByDate()
        {
            DataTable dtsizes = new DataTable();
            using (SqlConnection con = new SqlConnection(ApplicationSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SelectIOAsBeginDate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Tarikh", DateComboBox.SelectedValue);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dtsizes.Load(sdr);
                }
            }

            return dtsizes;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = DateCreate.Begin();
            DateComboBox.DataSource = bs.DataSource;

            dataGridView1.DataSource = GetData();
        }

        private DataTable GetData()
        {
            DataTable dtsizes = new DataTable();
            using(SqlConnection con=new SqlConnection(ApplicationSetting.ConnectionString()))
            {
                using (SqlCommand cmd=new SqlCommand("SelectAllIO", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dtsizes.Load(sdr);
                }
            }
            return dtsizes;
        }

        private void DateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDataByDate();
        }

        private void test1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetData();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult i;
            i = MessageBox.Show("Are you sure to exit ?","Exit",MessageBoxButtons.YesNo);
            if (i == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void fridayFixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FridayFix();
            
        }

        private void FridayFix()
        {


            for(int i=0; i < dataGridView1.Rows.Count; i++)
            {
                string bgtime, bgdate,enddate="",endtime="",benddate,bendtime;
                
                try
                {
                    bgtime = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    bgdate = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    if (dataGridView1.Rows[i].Cells[3].Value != DBNull.Value)
                    {
                        enddate = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    }
                    else if (dataGridView1.Rows[i].Cells[3].Value == DBNull.Value)
                    {
                        enddate = bgdate;
                    }

                    if (dataGridView1.Rows[i].Cells[4].Value != DBNull.Value)
                    {
                        endtime = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    }
                    else if (dataGridView1.Rows[i].Cells[4].Value == DBNull.Value)
                    {
                        endtime = "17:30";
                    }
                    TimeSpan duration = DateTime.Parse(endtime) - DateTime.Parse(bgtime);
                    dataGridView1.Rows[i].Cells[10].Value =duration.TotalHours*60;
                    dataGridView1.Rows[i].Cells[3].Value = enddate;
                    dataGridView1.Rows[i].Cells[4].Value = endtime;
                    benddate = enddate;
                    bendtime = endtime;
                    dataGridView1.Rows[i].Cells[6].Value = benddate;
                    dataGridView1.Rows[i].Cells[7].Value = bendtime;
                }
                catch 
                {
                } 
            }
        }
    }
}
