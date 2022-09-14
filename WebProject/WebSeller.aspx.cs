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
    public partial class WebSeller : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            poplate();
            poplate2();
            poplate3();

        }
        private void poplate()
        {
            SqlConnection con = new SqlConnection(@"Data Source=IN-WIN-PKUMAR\SQLEXPRESS;Initial Catalog=InvoiceProject;Integrated Security=True");
            con.Open();
            string query = "select Name,Price from WebProduct";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            GV2.DataSource = ds.Tables[0];
            GV2.DataBind();
            con.Close();

        }
        private void poplate2()
        {
            SqlConnection con = new SqlConnection(@"Data Source=IN-WIN-PKUMAR\SQLEXPRESS;Initial Catalog=InvoiceProject;Integrated Security=True");
            con.Open();
            string query = "select * from Webselling";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            GV3.DataSource = ds.Tables[0];
            GV3.DataBind();
            con.Close();

        }
        private void poplate3()
        {
            SqlConnection con = new SqlConnection(@"Data Source=IN-WIN-PKUMAR\SQLEXPRESS;Initial Catalog=InvoiceProject;Integrated Security=True");
            con.Open();
            string query = "select * from WebBill";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            GV4.DataSource = ds.Tables[0];
            GV4.DataBind();
            con.Close();

        }

        protected void GV1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            NAMEINPUT.Text = GV2.SelectedRow.Cells[1].Text.ToString();
            PRICEINPUT.Text = GV2.SelectedRow.Cells[2].Text.ToString();


        }

        protected void Add_Click(object sender, EventArgs e)
        {
             try
             {
               SqlConnection con = new SqlConnection(@"Data Source=IN-WIN-PKUMAR\SQLEXPRESS;Initial Catalog=InvoiceProject;Integrated Security=True");
               //step-1
               int total = Convert.ToInt32(PRICEINPUT.Text) * Convert.ToInt32(QUANTITYINPUT.Text);
               string query = "insert into WebSelling values('"+Customer.Text+"',"
                                                               +IDINPUT.Text+",'"
                                                               + NAMEINPUT.Text + "',"
                                                               + QUANTITYINPUT.Text + ","
                                                               + PRICEINPUT.Text + ","
                                                               + total + ")";
               //step-2
               SqlCommand cmd = new SqlCommand(query, con);
               con.Open();
                       cmd.ExecuteNonQuery();
                       Response.Write("Product Added Successfully");
                       con.Close();
                       poplate2();
                       ClearData();
             }
             catch (Exception ex)
             {
                 Response.Write(ex.Message);
             }
            

        }
        private void ClearData()
        {
           //IDINPUT.Text = "";
            NAMEINPUT.Text = "";
            PRICEINPUT.Text = "";
            QUANTITYINPUT.Text = "";
        }

        protected void Bill_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(IDINPUT.Text);
            double  total2 =  Total(id);
            Amount.Text = total2.ToString();
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=IN-WIN-PKUMAR\SQLEXPRESS;Initial Catalog=InvoiceProject;Integrated Security=True");
                //step-1             
                string query = "insert into WebBill values(" + IDINPUT.Text + ",'" + Customer.Text + "','" + Datet.Text + "'," + Amount.Text + ")";                                                         
                //step-2
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                Response.Write("Product Added Successfully");
                con.Close();
                poplate3();
             
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }


        }
        private double Total(int id)
        {
            double total = 0;
            
            String ConnectionString = "Data Source=IN-WIN-PKUMAR\\SQLEXPRESS;Initial Catalog=InvoiceProject;Integrated Security=True";
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            
            try
            {
                
                SqlCommand cmd = new SqlCommand("select sum(Total) from WebSelling where Id = " + id + "", conn);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                total = Convert.ToDouble(ds.Tables[0].Rows[0][0]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //Close the connection
                conn.Close();
            }

            return total;

        }
        protected void NextPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductWeb.aspx");
        }
    }
}

