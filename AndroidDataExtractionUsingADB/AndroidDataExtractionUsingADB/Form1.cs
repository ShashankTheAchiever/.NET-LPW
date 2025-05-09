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

namespace AndroidDataExtractionUsingADB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\shash\\OneDrive\\Tài liệu\\AndroidDataExtraction.mdf\";Integrated Security=True;Connect Timeout=30";
            SqlConnection conn = new SqlConnection(ConnectionString);
            string query = "SELECT * FROM contactstable";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                // SqlDataReader reader = cmd.ExecuteReader();

                dataGridView1.DataSource = null; // Clear previous data 
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
                dataGridView1.DataSource = dt; // Bind the DataTable to the DataGridView


            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\shash\\OneDrive\\Tài liệu\\AndroidDataExtraction.mdf\";Integrated Security=True;Connect Timeout=30";
            SqlConnection conn = new SqlConnection(ConnectionString);
            string query = "SELECT * FROM messagestable";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                // SqlDataReader reader = cmd.ExecuteReader();

                dataGridView1.DataSource = null; // Clear previous data 
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
                dataGridView1.DataSource = dt; // Bind the DataTable to the DataGridView


            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\shash\\OneDrive\\Tài liệu\\AndroidDataExtraction.mdf\";Integrated Security=True;Connect Timeout=30";
            SqlConnection conn = new SqlConnection(ConnectionString);
            string query = "SELECT * FROM calllogstable";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                // SqlDataReader reader = cmd.ExecuteReader();

                dataGridView1.DataSource = null; // Clear previous data 
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
                dataGridView1.DataSource = dt; // Bind the DataTable to the DataGridView


            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\shash\\OneDrive\\Tài liệu\\AndroidDataExtraction.mdf\";Integrated Security=True;Connect Timeout=30";
            SqlConnection conn = new SqlConnection(ConnectionString);
            string query = "SELECT * FROM cpuinfotable";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                // SqlDataReader reader = cmd.ExecuteReader();

                dataGridView1.DataSource = null; // Clear previous data 
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
                dataGridView1.DataSource = dt; // Bind the DataTable to the DataGridView


            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\shash\\OneDrive\\Tài liệu\\AndroidDataExtraction.mdf\";Integrated Security=True;Connect Timeout=30";
            SqlConnection conn = new SqlConnection(ConnectionString);
            string query = "SELECT * FROM loginaccountstable";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                // SqlDataReader reader = cmd.ExecuteReader();

                dataGridView1.DataSource = null; // Clear previous data 
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
                dataGridView1.DataSource = dt; // Bind the DataTable to the DataGridView


            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            String ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\shash\\OneDrive\\Tài liệu\\AndroidDataExtraction.mdf\";Integrated Security=True;Connect Timeout=30";
            SqlConnection conn = new SqlConnection(ConnectionString);
            string query = "SELECT * FROM devicedetailstable";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                // SqlDataReader reader = cmd.ExecuteReader();

                dataGridView1.DataSource = null; // Clear previous data 
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
                dataGridView1.DataSource = dt; // Bind the DataTable to the DataGridView


            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
