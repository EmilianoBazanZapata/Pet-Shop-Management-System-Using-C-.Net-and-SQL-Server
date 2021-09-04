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
            DisplayProduct();
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
        private void DisplayProduct()
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
                ProductsDGV.DataSource = ds.Tables[0];
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
        private void UpdateStock()
        {
            try
            {
                int NewQty = Stock - Convert.ToInt32(QtyTb.Text);
                con.Open();
                SqlCommand cmd = new SqlCommand("Update ProductTbl set PrQty = @PQ where PrId = @PKey",con);
                cmd.Parameters.AddWithValue("@PQ", NewQty);
                cmd.Parameters.AddWithValue("@PKey", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Edited");
                con.Close();
                DisplayProduct();
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
        private void Reset() 
        {
            key = 0;
            Stock = 0;
            PrNameTb.Text = "";
            QtyTb.Text = "";
        }
        int n = 0, GrdTotal = 0;

        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "" || Convert.ToInt32(QtyTb.Text) > Stock)
            {
                MessageBox.Show("No Enough In House");
            }
            else if (QtyTb.Text == "" || key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(PrPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = PrNameTb.Text;
                newRow.Cells[2].Value = QtyTb.Text;
                newRow.Cells[3].Value = PrPriceTb.Text;
                newRow.Cells[4].Value = total;
                GrdTotal = GrdTotal + total;
                BillDGV.Rows.Add(newRow);
                n++;
                TotalLbl.Text = "$" + GrdTotal;
                UpdateStock();
                Reset();
            }
        }
        private void Deletebtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PrNameTb.Text = ProductsDGV.SelectedRows[0].Cells[1].Value.ToString();
            Stock = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[3].Value.ToString());
            PrPriceTb.Text = ProductsDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (PrNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        string prodname;
        private void CustIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustName();
        }

        private void Printbtn_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm",285,600);
            if(printPreviewDialog1.ShowDialog()==DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
    }
}
