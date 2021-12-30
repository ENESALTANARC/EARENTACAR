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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }


        SqlConnection con;

        SqlCommand cmd;
        SqlDataReader dr;
        private void button1_Click(object sender, EventArgs e)
        {
            string ad = Uname.Text;
            string sifre = PassTb.Text;
            con = new SqlConnection("Data Source=DESKTOP-0OS42JP\\AA;Initial Catalog=CarRental;Integrated Security=True");
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM  [User] where user_name='" + Uname.Text + "' AND password='" + PassTb.Text + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MainForm f2 = new MainForm();
                f2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı ya da şifre yanlış");
            }

            con.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Uname.Text = "";
            PassTb.Text = "";
        }

        
    }
}
