using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormApplication
{
    public partial class IgWeatherTest : System.Web.UI.Page
    {
        protected Tiu.ThirdpartyApi.Google.Weather _weather;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                txtCity.Text = "珠海";
            }
        }

        protected void btnGetWeather_Click(object sender, EventArgs e)
        {
            var place = txtCity.Text.Trim();
            _weather = Tiu.ThirdpartyApi.Google.Weather.GetWeather(place);
        }
    }
}