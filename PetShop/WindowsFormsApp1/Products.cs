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
            Clear();
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
            cboProductCategory.SelectedIndex = 0;
        }
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (ProductName.Text == "")
            {
                MessageBox.Show("Please Add a Name");
                return;
            }
            if (ProductoPrice.Text == "")
            {
                MessageBox.Show("Please Add a Price");
                return;
            }
            if (ProductQuantity.Text == "")
            {
                MessageBox.Show("Please Add a Quantity");
                return;
            }
            else if (ProductName.Text != "" && ProductoPrice.Text != "" && ProductQuantity.Text != "")
            {
                try
                {
                    //abrimos la conexion a la base de datos
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ProductTbl (PrName,PrCat,PrQty,PrPrice) values(@PN,@PC,@PQ,@PP)", con);
                    //asignamos los valores a la sentencia para evitar la concatenacion por seguridad   
                    cmd.Parameters.AddWithValue("@PN", ProductName.Text);
                    cmd.Parameters.AddWithValue("@PC", cboProductCategory.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PQ", ProductQuantity.Text);
                    cmd.Parameters.AddWithValue("@PP", ProductoPrice.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product : " + ProductName.Text + " Added");
                    con.Close();
                    DisplayCustomers();
                    Clear();
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

        private void ProductDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            key = Convert.ToInt32(ProductDGV.SelectedRows[0].Cells[0].Value.ToString());
            ProductName.Text = ProductDGV.SelectedRows[0].Cells[1].Value.ToString();
            cboProductCategory.Text = ProductDGV.SelectedRows[0].Cells[2].Value.ToString();
            ProductQuantity.Text = ProductDGV.SelectedRows[0].Cells[3].Value.ToString();
            ProductoPrice.Text = ProductDGV.SelectedRows[0].Cells[4].Value.ToString();
        }
    }
}
