<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="JustText.aspx.vb" Inherits="Test_JustText" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
<%--    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
    <asp:Button ID="Button1" runat="server" Text="Button" />--%>
<%--                <asp:LinkButton ID="LinkButton1" runat="server">whatthehell</asp:LinkButton>
    <asp:DataList ID="DataList1" runat="server" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" GridLines="Both">
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <HeaderTemplate >
            <tr >
                        <td><asp:Label ID="Label2" runat="server" Text="BSC"></asp:Label></td>
            <td><asp:Label ID="Label3" runat="server" Text="AMRFRMAXTRAFFIC_Value"></asp:Label></td>
            <td><asp:Label ID="Label4" runat="server" Text="AMRFRMAXTRAFFIC_FValue"></asp:Label></td>
            <td><asp:Label ID="Label5" runat="server" Text="AMRHRMAXTRAFFIC_Value"></asp:Label></td>
            <td><asp:Label ID="Label6" runat="server" Text="AMRHRMAXTRAFFIC_FValue"></asp:Label></td>
            <td><asp:Label ID="Label7" runat="server" Text="EGPRSBPCLIMIT_Value"></asp:Label></td>
            <td><asp:Label ID="Label8" runat="server" Text="EGPRSBPCLIMIT_FValue"></asp:Label></td>
            <td><asp:Label ID="Label9" runat="server" Text="MCTRTRX_Value"></asp:Label></td>
            <td><asp:Label ID="Label10" runat="server" Text="MCTRTRX_FValue"></asp:Label></td>
            <td><asp:Label ID="Label11" runat="server" Text="HRMAXTRAFFIC_Value"></asp:Label></td>
            <td><asp:Label ID="Label12" runat="server" Text="HRMAXTRAFFIC_FValue"></asp:Label></td>
            <td><asp:Label ID="Label13" runat="server" Text="IOEXP"></asp:Label></td>
            <td><asp:Label ID="Label14" runat="server" Text="TRA_AMRHR"></asp:Label></td>
            <td><asp:Label ID="Label15" runat="server" Text="TRA_AMRFR"></asp:Label></td>
           <td> <asp:Label ID="Label16" runat="server" Text="TRA_FR"></asp:Label></td>
           <td> <asp:Label ID="Label17" runat="server" Text="TRA_EFR"></asp:Label></td>
           <td> <asp:Label ID="Label18" runat="server" Text="TRA_HR"></asp:Label></td>
            <td><asp:Label ID="Label19" runat="server" Text="SAE1153_NI"></asp:Label></td>
            <td><asp:Label ID="Label20" runat="server" Text="SAE1153_NIU"></asp:Label></td>
            <td><asp:Label ID="Label21" runat="server" Text="NUMREQEGPRSBPC"></asp:Label></td>
            <td><asp:Label ID="Label22" runat="server" Text="MO_TRX"></asp:Label></td>
            <td><asp:Label ID="Label23" runat="server" Text="MO_TRX_Oper"></asp:Label></td>

</tr>

        </HeaderTemplate>
        <ItemStyle ForeColor="#000066" />
        <ItemTemplate>
            <tr>
            <td><asp:Label ID="Label2" runat="server" Text='<%# Eval("BSC") %>'></asp:Label></td>
            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("AMRFRMAXTRAFFIC_Value") %>'></asp:Label></td>
            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("AMRFRMAXTRAFFIC_FValue") %>'></asp:Label></td>
            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("AMRHRMAXTRAFFIC_Value") %>'></asp:Label></td>
            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("AMRHRMAXTRAFFIC_FValue") %>'></asp:Label></td>
            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("EGPRSBPCLIMIT_Value") %>'></asp:Label></td>
            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("EGPRSBPCLIMIT_FValue") %>'></asp:Label></td>
            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("MCTRTRX_Value") %>'></asp:Label></td>
            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("MCTRTRX_FValue") %>'></asp:Label></td>
            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("HRMAXTRAFFIC_Value") %>'></asp:Label></td>
            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("HRMAXTRAFFIC_FValue") %>'></asp:Label></td>
            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("IOEXP") %>'></asp:Label></td>
            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("TRA_AMRHR") %>'></asp:Label></td>
            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("TRA_AMRFR") %>'></asp:Label></td>
           <td> <asp:Label ID="Label16" runat="server" Text='<%# Eval("TRA_FR") %>'></asp:Label></td>
           <td> <asp:Label ID="Label17" runat="server" Text='<%# Eval("TRA_EFR") %>'></asp:Label></td>
           <td> <asp:Label ID="Label18" runat="server" Text='<%# Eval("TRA_HR") %>'></asp:Label></td>
            <td><asp:Label ID="Label19" runat="server" Text='<%# Eval("SAE1153_NI") %>'></asp:Label></td>
            <td><asp:Label ID="Label20" runat="server" Text='<%# Eval("SAE1153_NIU") %>'></asp:Label></td>
            <td><asp:Label ID="Label21" runat="server" Text='<%# Eval("NUMREQEGPRSBPC") %>'></asp:Label></td>
            <td><asp:Label ID="Label22" runat="server" Text='<%# Eval("MO_TRX") %>'></asp:Label></td>
            <td><asp:Label ID="Label23" runat="server" Text='<%# Eval("MO_TRX_Oper") %>'></asp:Label></td>
            </tr>


        </ItemTemplate>


        <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />


    </asp:DataList>--%>
<%--                <div class="container-fluid" style ="text-align :left ">

                <div class="row">
                        <div class="col-sm-3 col-md-2 sidebar">
          <ul class="nav nav-sidebar">
            <li class="active"><a href="./Dashboard Template for Bootstrap_files/Dashboard Template for Bootstrap.html">第1章绪论 <span class="sr-only">(current)</span></a></li>
            <li><a href="./Dashboard Template for Bootstrap_files/Dashboard Template for Bootstrap.html">1．1 移动通信的主要特点</a></li>
            <li><a href="./Dashboard Template for Bootstrap_files/Dashboard Template for Bootstrap.html">1．2 移动通信的发展</a></li>
            <li><a href="./Dashboard Template for Bootstrap_files/Dashboard Template for Bootstrap.html">1．3 移动通信的发展趋势与展望</a></li>
          </ul>
          <ul class="nav nav-sidebar">
            <li><a href="">第2章 无线传播与移动信道</a></li>
            <li><a href="">2．1 移动信道的特点</a></li>
            <li><a href="">2．2 3类主要快衰落</a></li>
            <li><a href="">2．3 传播类型与信道模型的定量分析</a></li>
            <li><a href="">2．4 多径信道的其他数学模型</a></li>
          </ul>
          <ul class="nav nav-sidebar">
            <li><a href="">第3章多址技术与扩频通信</a></li>
            <li><a href="">3．1 多址技术的基本概念</a></li>
            <li><a href="">3．2 移动通信中的典型多址接入方式</a></li>
          </ul>
        </div>
                <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main">
          <h1 class="page-header">第一章 绪论</h1>

          <div class="row placeholders">
            <div class="col-xs-12 col-sm-12 placeholder" style ="text-align :left ">
               &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 本书内容可以分为3部分。<br /><br /> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 第一部分(第1章一第5章)阐述通信基础知识和模拟通信原理。其中第2章(确知信号)是为了满足一些学校和专业的需要而增加的，对于许多本科通信工程专业的教学，完全可以跳过此章；第3章(随机信号)视需要情况可以作复习性讲述。<br /><br /> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 第二部分(第6章～第10章)主要论述数字通信、模拟信号的数字传输和数字信号的最佳接收原理。由于技术的不断发展和创新，数字调制和数字带通传输的内容非常丰富，将其放在一章内讲述会使篇幅过长，故分为两章(第7章和第8章)讲述，并且第8章的内容可以视需要，选用其中一部分学习，或者跳过不学，不会影响后面章节的理解。<br /><br /> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 第三部分(第11章～第14章)讨论数字通信中的编码和同步等技术，以及简要介绍诵倍网的概念。
            </div>
          </div></div> </div> 
                </div>--%>
<%--    <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple">
        <asp:ListItem Text ="asdf"></asp:ListItem>
        <asp:ListItem Text ="asdf"></asp:ListItem>
        <asp:ListItem Text ="asdf"></asp:ListItem>
        <asp:ListItem Text ="asdf"></asp:ListItem>
        <asp:ListItem Text ="asdf"></asp:ListItem>
        <asp:ListItem Text ="asdf"></asp:ListItem>
        <asp:ListItem Text ="asdf"></asp:ListItem>
    </asp:ListBox>--%>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>

