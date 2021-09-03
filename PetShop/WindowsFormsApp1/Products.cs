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
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
            DisplayCustomers();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Emiliano\Documents\PetShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        int key = 0;
        //listaremos los empleados una vez agreguemos uno y cuando se inicie el formulario
        private void DisplayCustomers()
        {
            try
            {
                con.Open();
                string Query = " select *" +
                               " from ProductTbl";
                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                ProductDGV.DataSource = ds.Tables[0];
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("There's been a problem ==>" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void Clear()
        {
            ProductName.Text = "";
            ProductoPrice.Text = "";
            ProductQuantity.Text = "";
        }
        private void Savebtn_Click(object sender, EventArgs e)
        {

        }
    }
}
