using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject
{
    public partial class WebLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginInput_Click(object sender, EventArgs e)
        {
           
            if (UNinput.Text == "" || PWINPUT.Text == "")
            {
                Response.Write("Enter the UserName and Password");
            }
            else
            {
                if (UNinput.Text == "Admin" && PWINPUT.Text == "Admin")
                {
                    Response.Redirect("WebSeller.aspx");
                }
                else
                {
                    Response.Write("Invalid inuput");

                }
            }
        }
    }
}