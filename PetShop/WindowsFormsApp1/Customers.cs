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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Emiliano\Documents\PetShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        int key = 0;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CustomerName.Text == "")
            {
                MessageBox.Show("Please Add a Name");
                return;
            }
            if (CustomerPhone.Text == "")
            {
                MessageBox.Show("Please Add a Number Phone");
                return;
            }
            if (CustomerAddress.Text == "")
            {
                MessageBox.Show("Please Add a Address");
                return;
            }
            else if (CustomerName.Text != "" && CustomerPhone.Text != "" && CustomerAddress.Text != "")
            {
                try
                {
                    //abrimos la conexion a la base de datos
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl (CustName,CustAdd,CustPhone) values(@CN,@CA,@CP)", con);
                    //asignamos los valores a la sentencia para evitar la concatenacion por seguridad   
                    cmd.Parameters.AddWithValue("@CN", CustomerName.Text);
                    cmd.Parameters.AddWithValue("@CA", CustomerPhone.Text);
                    cmd.Parameters.AddWithValue("@CP", CustomerAddress.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer : " + CustomerName.Text + " Added");
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
}
