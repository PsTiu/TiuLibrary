<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BingImgText.aspx.cs" Inherits="WebFormApplication.BingImgText" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%
        if (IsPostBack == false)
        {
            int img_w = Tiu.ThirdpartyApi.Bing.BingImage.DEFAULT_WIDTH / 3;
            int img_h = Tiu.ThirdpartyApi.Bing.BingImage.DEFAULT_HEIGHT / 3;
            foreach (var item in this.Images)
            {
                
                %>
                <ul>
                    <li>
                    <p><%= item.StartDate %> - <%= item.EndDate %></p>
                    <p>
                        <ul>
                        <li><p><img src="<%= item.Url %>" alt="<%= item.Copyright %>" title="<%= item.Copyright %>" width="<%= img_w %>" height="<%= img_h %>" /></p></li>
                        <%
                foreach (var hs in item.HotSpots)
                {
                        %>
                            <li>· <a href="<%= hs.Link %>" target="_blank"><%= hs.Desc %></a> - (x：<%= hs.LocX %> y：<%= hs.LocY %>)</li>
                        <%
                }
                             %>
                        <li>
                            <%
                foreach (var msg in item.Messages)
                {
                            %>
                            <p><b><%= msg.Title %>：</b><a href="<%= msg.Link %>" target="_blank"><%= msg.Text %></a></p>
                            <%
                }
                             %>
                            
                        </li>
                        </ul>
                    </p>
                    <br />
                    </li>
                </ul>
                <%
            }
        }
         %>
    </div>
    </form>
</body>
</html>
