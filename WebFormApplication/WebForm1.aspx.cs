using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormApplication
{
    public partial class WebForm1 : System.Web.UI.Page
    {
       

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnAddTxb_Click(object sender, EventArgs e)
        {
            this.pnlTxb.Controls.Clear();
            this.pnlTxb.Controls.Add(new TextBox() { 

            });
        }

        protected void btnShowText_Click(object sender, EventArgs e)
        {
            var re = "";
            foreach (var item in this.pnlTxb.Controls)
            {
                var txb = item as TextBox;
                if (txb != null)
                {
                    re += txb.Text + " ； ";
                }
            }
            this.labText.Text = re;
        }
    }
}