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
            DisplayEmployees();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Emiliano\Documents\PetShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        //listaremos los empleados una vez agreguemos uno y cuando se inicie el formulario
        private void DisplayEmployees()
        {
            try
            {
                con.Open();
                string Query = " select *" +
                               " from EmployeeTbl";
                SqlDataAdapter sda = new SqlDataAdapter(Query,con);
                SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                EmployeeDGV.DataSource = ds.Tables[0];
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (EmployeName.Text == "")
            {
                MessageBox.Show("Please Add a Name");
                return;
            }
            if (EmployePhone.Text == "")
            {
                MessageBox.Show("Please Add a Number Phone");
                return;
            }
            if (EmployeeAddress.Text == "")
            {
                MessageBox.Show("Please Add a Address");
                return;
            }
            if (EmployeePassword.Text == "")
            {
                MessageBox.Show("Please Add a Password");
                return;
            }
            else if(EmployeName.Text != "" && EmployePhone.Text != "" && EmployeeAddress.Text != "" && EmployeePassword.Text != "")
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
                    con.Close();
                    DisplayEmployees();
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
                    Clear();
                }
            }
        }
        //metodo para limpiar los campos del formulario
        private void Clear() 
        {
            EmployeName.Text = "";
            EmployeeAddress.Text = "";
            EmployePhone.Text = "";
            EmployeePassword.Text = "";
        }
    }
}
