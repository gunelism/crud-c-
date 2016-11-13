using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace data_basa_misal.verileri_elave_etmek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            showInfo();
        }
  
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-810TJA4\\SQLEXPRESS;Initial Catalog=test;Integrated Security=True");

        private void showInfo()
        {
            listView1.Items.Clear();
            conn.Open();
            SqlCommand select = new SqlCommand("Select * from crud", conn);
            SqlDataReader read = select.ExecuteReader();

            //array qaytarir
            while (read.Read())
            {
                ListViewItem add = new ListViewItem();
                add.Text = read["id"].ToString();
                add.SubItems.Add(read["full_name"].ToString());
                add.SubItems.Add(read["email"].ToString());
                add.SubItems.Add(read["adress"].ToString());
                listView1.Items.Add(add);
            
            }
            conn.Close();
        }
        //show
        private void button1_Click(object sender, EventArgs e)
        {
 
        }
        //inserting
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                // bos buraxilmamali
                MessageBox.Show("Deyer daxil et !!");
            } 
           
            else
            {
                conn.Open();
                SqlCommand insert = new SqlCommand("insert into crud (full_name,email,adress) values ('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "')", conn);
                insert.ExecuteNonQuery();
                conn.Close();
                showInfo();
            }

            text_clear();
        }

        int id = 0;
        //delete
        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand delete = new SqlCommand("delete from crud where id =('" + id + "')", conn);
            delete.ExecuteNonQuery();
            conn.Close();

            showInfo();
            text_clear();
        }

        //update
        private void btn_update_Click_1(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand update = new SqlCommand("UPDATE crud SET full_name='" + textBox1.Text.ToString() + "' , email='" + textBox2.Text.ToString() + "' ,adress='" + textBox3.Text.ToString() + "' WHERE id=" + id, conn);
            update.ExecuteNonQuery();
            conn.Close();

            text_clear();
            showInfo();
        }

        public void text_clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void better(object sender, MouseEventArgs e)
        {

            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[3].Text;

           // MessageBox.Show(listView1.SelectedItems[1].ToString());

        }
    }
}
