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
    public partial class Billings : Form
    {
        public Billings()
        {
            InitializeComponent();
            GetCustomers();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Emiliano\Documents\PetShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        int key = 0;
        int Stock = 0;
        private void GetCustomers() 
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select CustId from CustomerTbl",con);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("CustId", typeof(int));
                dt.Load(rdr);
                CustIdCb.ValueMember = "CustId";
                CustIdCb.DataSource = dt;
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
        private void GetCustName()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from CustomerTbl where CustId = @CustId", con);
                cmd.Parameters.AddWithValue("@CustId", CustIdCb.SelectedValue);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    custNameTb.Text = dr["CustName"].ToString();
                }
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
        
    }
}
