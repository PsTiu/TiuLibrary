<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IgWeatherTest.aspx.cs" Inherits="WebFormApplication.IgWeatherTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-size:12px;">
    <form id="form1" runat="server">
    <div>
        <asp:TextBox runat="server" ID="txtCity" />
        <asp:Button Text="Get Weather" runat="server" ID="btnGetWeather" 
            onclick="btnGetWeather_Click" />
    </div>

    <div>
        <%
            if (_weather != null)
            { 
            %>
        <ul>
            <li><h4>基本信息</h4></li>
            <li>查询城市：<%= _weather.Forecast.PostalCode %> <%= _weather.Forecast.UnitSystem %>（<%= _weather.Forecast.City %>）</li>
            <li><%= _weather.Forecast.ForecastDate%></li>
        </ul>     

        <br />

        <ul>
            <li><h4>当前天气信息</h4><img src="<%= _weather.Current.IconFullRule %>" alt="<%= _weather.Current.Condition %>" /></li>
            <li>天气清楚：<%= _weather.Current.Condition %></li>
            <li>华氏温度：<%= _weather.Current.Temp_f %> °F</li>
            <li>摄氏温度：<%= _weather.Current.Temp_c %> °C</li>
            <li>空气<%= _weather.Current.Humidity %></li>
            <li><%= _weather.Current.WindCondition %></li>
        </ul>

        <br />

            <%
                foreach (var item in _weather.ForecastConditions)
                {
                    %>
        <div>
            <ul style="display:inline-block; float:left">
                <li><h4><%= item.DayOfWeek %></h4></li>
                <li><img src="<%= item.IconFullRule %>" alt="<%= item.Condition %>" /> <%= item.Condition %></li>
                <li><span style="color:Red"><%= item.Low %> °C</span> 至 <span style="color:Green"><%= item.High %>°C</span></li>
            </ul>
        </div>
                    <%
                }
                 %>
            <%
            }
         %>
        
    </div>
    </form>
</body>
</html>
