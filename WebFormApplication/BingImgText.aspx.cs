using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Tiu.ThirdpartyApi.Bing;

namespace WebFormApplication
{
    public partial class BingImgText : System.Web.UI.Page
    {
        public BingImageCollection Images;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Images = BingImageCollection.GetBingImages(0, 4);
            }
        }
    }
}