using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebProject
{
    public partial class ProductWeb : System.Web.UI.Page
    {
        //Display Load
        private void poplate()
        {
            SqlConnection con = new SqlConnection(@"Data Source=IN-WIN-PKUMAR\SQLEXPRESS;Initial Catalog=InvoiceProject;Integrated Security=True");
            con.Open();
            string query = "select * from WebProduct";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            GV1.DataSource = ds.Tables[0];
            GV1.DataBind();
            con.Close();
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            poplate();
        }
        //Add
        protected void ADDB_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=IN-WIN-PKUMAR\SQLEXPRESS;Initial Catalog=InvoiceProject;Integrated Security=True");
                //step-1

                string query = "insert into WebProduct values('"
                                                                + NAMEINPUT.Text + "',"
                                                                + QUANTITYINPUT.Text + ","
                                                                + PRICEINPUT.Text + ")";
                                                                
                //step-2
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                Response.Write("Product Added Successfully");
                con.Close();
                poplate();
                ClearData();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        //Update
        protected void EDITB_Click(object sender, EventArgs e)
        {
            try
            {
                if (IDINPUT.Text == "" || NAMEINPUT.Text == "" || QUANTITYINPUT.Text == "" || PRICEINPUT.Text == "")
                {
                    Response.Write("Missing Information");
                }
                else
                {
                    SqlConnection con = new SqlConnection(@"Data Source=IN-WIN-PKUMAR\SQLEXPRESS;Initial Catalog=InvoiceProject;Integrated Security=True");
                    string query = "update WebProduct set Name ='"
                                                + NAMEINPUT.Text + "',Quantity="
                                                + QUANTITYINPUT.Text + ",Price="
                                                + PRICEINPUT.Text + " where Id=" + IDINPUT.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Response.Write("Product Successfully Updated");
                    con.Close();
                    poplate();
                    ClearData();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }

        //Delete
        protected void DELETEB_Click(object sender, EventArgs e)
        {
            try
            {
                if (IDINPUT.Text == "")
                {
                    Response.Write("Select The Product to Delete");
                }
                else
                {
                    SqlConnection con = new SqlConnection(@"Data Source=IN-WIN-PKUMAR\SQLEXPRESS;Initial Catalog=InvoiceProject;Integrated Security=True");
                    string query = "delete from WebProduct where Id=" + IDINPUT.Text + "";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Response.Write("Product Deleted Successfully");
                    con.Close();
                    poplate();
                    ClearData();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void GV1_SelectedIndexChanged(object sender, EventArgs e)
        {
            IDINPUT.Text        =  GV1.SelectedRow.Cells[1].Text.ToString();
            NAMEINPUT.Text      =  GV1.SelectedRow.Cells[2].Text.ToString();
            QUANTITYINPUT.Text  =  GV1.SelectedRow.Cells[3].Text.ToString();
            PRICEINPUT.Text     =  GV1.SelectedRow.Cells[4].Text.ToString();
            
        }

        protected void Seller_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebSeller.aspx");
        }
        //Clear Data
        private void ClearData()
        {           
            IDINPUT.Text       = "";
            NAMEINPUT.Text     = "";
            PRICEINPUT.Text    = "";
            QUANTITYINPUT.Text = "";
        }
    }
}