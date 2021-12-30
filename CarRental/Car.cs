using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;




namespace CarRental
{
    public partial class Car : Form
    {
        public Car()
        {
            InitializeComponent();
        }


        SqlConnection Con = new SqlConnection("Data Source=DESKTOP-0OS42JP\\AA;Initial Catalog=CarRental;Integrated Security=True");
        private void populate()
        {
            Con.Open();
            string query = "select * from car";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            CarDGV.DataSource = ds.Tables[0]; 
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (RegNumTb.Text == "" || BrandTb.Text == "" || ModelTb.Text == "" || PriceTb.Text =="") 
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into car values('" + RegNumTb.Text + "','" + BrandTb.Text + "','" + ModelTb.Text + "','"+PriceTb.Text+"','"+AvaibleCb.SelectedItem.ToString()+"')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Araba Başarıyla Eklendi");
                    Con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }
        
        private void Car_Load(object sender, EventArgs e)
        {
            populate();           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (RegNumTb.Text == "")
            {
                MessageBox.Show("Eksik bilgi");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from car where kayıt_no='" + RegNumTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Araba Başarıyla Silindi");
                    Con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void CarDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RegNumTb.Text = CarDGV.SelectedRows[0].Cells[0].Value.ToString();
            BrandTb.Text = CarDGV.SelectedRows[0].Cells[1].Value.ToString();
            ModelTb.Text = CarDGV.SelectedRows[0].Cells[2].Value.ToString();
            AvaibleCb.SelectedItem = CarDGV.SelectedRows[0].Cells[3].Value.ToString();
            PriceTb.Text = CarDGV.SelectedRows[0].Cells[4].Value.ToString(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (RegNumTb.Text == "" || BrandTb.Text == "" || ModelTb.Text == "" || PriceTb.Text == "") 
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update car set brand='" + BrandTb.Text + "',model='" + ModelTb.Text + "', available = '"+AvaibleCb.SelectedItem.ToString()+"',fee="+PriceTb.Text+" where kayıt_no='" + RegNumTb.Text + "';";

                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Araba Başarıyla Güncellendi");
                    Con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void Search_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string flag = "";
            if (Search.SelectedItem.ToString() == "available")
            {
                flag = "YES";
            }
            else
            {
                flag = "NO";
            }
            Con.Open();
            string query = "select * from car where available = '"+flag+"'";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
             CarDGV.DataSource = ds.Tables[0]; 
            Con.Close();
        }

        private void PriceTb_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
