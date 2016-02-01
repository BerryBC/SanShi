<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="JustText.aspx.vb" Inherits="Test_JustText" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
    <asp:Button ID="Button1" runat="server" Text="Button" />
            <div class="container" style ="overflow-x:scroll;">
                <asp:LinkButton ID="LinkButton1" runat="server">whatthehell</asp:LinkButton>
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


    </asp:DataList>
                </div>
</asp:Content>

