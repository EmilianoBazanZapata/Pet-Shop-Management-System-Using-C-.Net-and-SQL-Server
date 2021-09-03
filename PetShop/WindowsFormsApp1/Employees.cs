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

namespace WindowsFormsApp1
{
    public partial class Employees : Form
    {
       
        public Employees()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Emiliano\Documents\PetShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (EmployeName.Equals(""))
            {
                MessageBox.Show("Please Add a Name");
            }
            if (EmployePhone.Equals(""))
            {
                MessageBox.Show("Please Add a Number Phone");
            }
            if (EmployeeAddress.Equals(""))
            {
                MessageBox.Show("Please Add a Address");
            }
            if (EmployeePassword.Equals(""))
            {
                MessageBox.Show("Please Add a Password");
            }
            else
            {
                try
                {
                    //abrimos la conexion a la base de datos
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl (EmpName,EmpAdd,EmpDOB,EmpPhone,EmpPass) " +
                                                    "values(@EN,@EA,@ED,@EP,@EPa)",con);
                    //asignamos los valores a la sentencia para evitar la concatenacion por seguridad   
                    cmd.Parameters.AddWithValue("@EN",EmployeName.Text);
                    cmd.Parameters.AddWithValue("@EA", EmployeeAddress.Text);
                    cmd.Parameters.AddWithValue("@ED", cboDateOfBirth.Value.Date);
                    cmd.Parameters.AddWithValue("@EP", EmployePhone.Text);
                    cmd.Parameters.AddWithValue("@EPa", EmployeePassword.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee : " + EmployeName.Text + " Added");
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
