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

namespace WindowsFormsApp1
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            CountDogs();
            CountCats();
            CountBirds();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Emiliano\Documents\PetShopDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void CountDogs()
        {
            string cat = "Dog";
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from ProductTbl where PrCat = '" + cat + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                DogsLbl.Text = dt.Rows[0][0].ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There's been a problem ==>" + ex.Message);
            }
            finally
            {
                //cerramos la cadena independientemente si la sentencia se ejecuta d emanera exitosa o no
                con.Close();
            }
        }
        private void CountCats()
        {
            string cat = "Cat";
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from ProductTbl where PrCat = '" + cat + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                LblCat.Text = dt.Rows[0][0].ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There's been a problem ==>" + ex.Message);
            }
            finally
            {
                //cerramos la cadena independientemente si la sentencia se ejecuta d emanera exitosa o no
                con.Close();
            }
        }
        private void CountBirds()
        {
            string cat = "Bird";
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from ProductTbl where PrCat = '" + cat + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BirdLbl.Text = dt.Rows[0][0].ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There's been a problem ==>" + ex.Message);
            }
            finally
            {
                //cerramos la cadena independientemente si la sentencia se ejecuta d emanera exitosa o no
                con.Close();
            }
        }
    }
}
