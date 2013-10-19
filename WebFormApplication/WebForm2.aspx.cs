using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormApplication
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.RequiredFieldValidator_txtName.IsValid
                && this.RequiredFieldValidator_txtNum.IsValid
                && this.RequiredFieldValidator_txtStr.IsValid
                && this.RangeValidator_txtNum.IsValid
                && this.CustomValidator_txtStr.IsValid
                )
            {
                this.labMsg.Text = "OK";
            }
        }

        protected void CustomValidator_txtStr_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = args.Value.Length <= 6;
        }
    }
}