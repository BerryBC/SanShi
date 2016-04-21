<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="GSMParaConfigPage.aspx.vb" Inherits="EarthlyBranch_BSDetails_GSMParaConfig_GSMParaConfigPage" %>

<%----第18号页面--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
            <section>
                <div class="container">
                    <div class="col-sm-12" style="text-align: left;" aria-multiline="True">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">列队插入数据库的数据 ( Which To Queue To Insert The Database )</h2>
                        </div>
                    </div>
                    <div class="col-sm-12" style="text-align: left;" aria-multiline="True">
                        <div class="col-sm-2" style="text-align: left;" aria-multiline="True"></div>
                        <div class="col-sm-3" style="text-align: left;" aria-multiline="True">
                            <asp:DropDownList ID="ddlNeOrPara" runat="server" AutoPostBack="True">
                                <asp:ListItem Text="网元列表" Value="NE" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="参数信息" Value="Para"></asp:ListItem>


                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2" style="text-align: left;" aria-multiline="True"></div>
                        <div class="col-sm-3" style="text-align: left;" aria-multiline="True">
                            <asp:DropDownList ID="ddlBSCorCell" runat="server" AutoPostBack="True">
                                <asp:ListItem Text="BSC级" Value="BSC" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Cell级" Value="Cell"></asp:ListItem>


                            </asp:DropDownList>

                        </div>
                        <div class="col-sm-2" style="text-align: left;" aria-multiline="True"></div>
                        <div class="col-sm-12" style="text-align: center;" aria-multiline="True">


                            <asp:DataList ID="dlParaConfigOfBSCorCell" runat="server" align="center" class="table-striped " Width="1200px"  OnItemCommand ="dlParaConfigOfBSCorCell_ItemCommand">
                                <HeaderStyle BorderStyle="Double" Font-Bold="True" ForeColor="White" BackColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />


                                <HeaderTemplate>
                                    <td>
                                        <asp:Label ID="lblNumber" runat="server" Text="Number" Font-Bold="true"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblNameOrNEName" runat="server" Text="" Font-Bold="true"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblSQLOrTableName" runat="server" Text="" Font-Bold="true"></asp:Label></td>
                                    <td><b>是否删除</b></td>
                                </HeaderTemplate>

                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />

                                <ItemTemplate>
                                    <td>
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Eval("Number") %>'></asp:Label>
                                    </td>

                                    <td>
                                        <asp:TextBox ID="txtFirst" runat="server" Text='<%# Eval("1st") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSecond" runat="server" Width="600px" Text='<%# Eval("2nd") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" class="form-control  btn-info" Style="padding: 2px; margin: 2px;"  CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Number")%>'  />
                                    
                                    </td>
                                </ItemTemplate>




                            </asp:DataList>
                            <br />
                            <asp:Button ID="btnAddOne" runat="server" class="btn btn-primary" Text="新增一行" Style="margin: 10px;" Enabled="true" />
                            <asp:Button ID="btnSave" runat="server" class="btn btn-warning" Text="保存" Style="margin: 10px;" Enabled="true" /><br />

                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

