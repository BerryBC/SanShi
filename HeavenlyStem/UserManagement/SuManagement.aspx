<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="SuManagement.aspx.vb" Inherits="UserManagement_SuManagement" %>

<%----第8号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <section style="padding-bottom: 20px; margin-left: -30px;">
        <div class="container">
            <div class="table table-responsive col-lg-12 col-md-12 col-sm-12 col-xs-12" style="align-self: center; align-items: center;">
                <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                    <h2 style="border-bottom-style: groove;">用户管理 ( User Management )</h2>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="text-align: right">
                            <asp:TextBox ID="txtSearchWhat" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" class="btn btn-success" CommandName="search" Text="GO" />
                        </div>
                        <asp:DataList ID="dlSuUserDo" runat="server" class="table-striped " align="center" BorderColor="#999999" BorderStyle="Double" Width="1200px" BorderWidth="3px" Style="text-align: left; text-indent: 5px; margin-top: 20px; margin-left: -30px;" ForeColor="Black" GridLines="Both">

                            <EditItemTemplate>
                            </EditItemTemplate>

                            <HeaderStyle BorderStyle="Double" Font-Bold="True" ForeColor="White" BackColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />


                            <HeaderTemplate>

                                <th>用户名</th>
                                <th>电话号码</th>
                                <th>中文名</th>
                                <th>公司名</th>
                                <th>E-Mail</th>
                                <th>QQ</th>
                                <th>第一次注册的时间</th>
                                <th>权限</th>
                                <th>登录次数</th>
                                <th>最后登录时间</th>
                                <th>修改</th>
                            </HeaderTemplate>

                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />

                            <ItemTemplate>

                                <td>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblPhoneNumber" runat="server" Text='<%# Eval("PhoneNumber") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblRealChineseName" runat="server" Text='<%# Eval("RealChineseName") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblEMail" runat="server" Text='<%# Eval("EMailAddress") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblQQ" runat="server" Text='<%# Eval("QQ") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblFirstSign" runat="server" Text='<%# Eval("FirstRegister") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblPowerLevel" runat="server" Text='<%# Eval("PowerLevel") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblLoginTime" runat="server" Text='<%# Eval("OnLineTime") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblLastLogin" runat="server" Text='<%# Eval("LastLogIn") %>'></asp:Label></td>

                                <td>
                                    <asp:Button ID="btnFixPassword" runat="server" Text="Fix Password" CommandName="Fix Password" class="btn btn-info" Style="padding: 2px; margin: 2px;" />
                                    <asp:Button ID="btnModifyPower" runat="server" Text="Modify Power" CommandName="Modify Power" class="btn btn-info" Style="padding: 2px; margin: 2px;" /></td>
                                </td>
                            </ItemTemplate>


                            <%--                        <SelectedItemTemplate>
                            Now Power Level is :
                            <asp:Label ID="lblNowLevel" runat="server"></asp:Label>
                            <br />
                            You want to change to :
                            <asp:TextBox ID="txtChangeLevel" runat="server"></asp:TextBox>
                            <br />
                            <span>
                            <asp:Button ID="btnChangeLevel" runat="server" class="btn btn-success" CommandName="search" Text="Change Power Level" />
                            </span>
                        </SelectedItemTemplate>--%>
                        </asp:DataList>
                        <div style="align-self: center; align-items: center;" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <asp:Button ID="btnGoFirstPage" runat="server" Text="First Page" class="btn btn-default" Style="margin: 20px;" />
                            |
            <asp:Button ID="btnGoFrontPage" runat="server" Text="Front Page" class="btn btn-default" Style="margin: 20px;" />
                            |&nbsp
            Now:<asp:Label ID="lblNow" runat="server" Text=""></asp:Label>&nbsp&nbsp in &nbsp&nbsp Total:<asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                            &nbsp |
            <asp:Button ID="btnGoNextPage" runat="server" Text="Next Page" class="btn btn-default" Style="margin: 20px;" />
                            |
            <asp:Button ID="btnGoLastPage" runat="server" Text="Last Page" class="btn btn-default" Style="margin: 20px;" />
                            <br />
                        </div>
                        <div style="text-align: center; align-items: center;" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                            <span style="margin-left: 160px" />Jump to :
                    <asp:TextBox ID="txtPage" runat="server" Width="30"></asp:TextBox>
                            <asp:Button ID="btnGoto" runat="server" class="btn btn-success" Text="GO" />
                            <asp:RegularExpressionValidator ID="revForPage" runat="server"
                                ControlToValidate="txtPage" ErrorMessage="请输入数字（除了数值0）" ForeColor="red"
                                ValidationExpression="[1-9]+(\d)*"></asp:RegularExpressionValidator>
                            <br />
                            <asp:Panel ID="plChangeLevel" runat="server" Visible="false" BorderStyle="Outset" Style="margin-top: 20px; padding-top: 5px; text-align: center; align-self: center;">
                                <span style="margin: 10px; margin-top: 10px;">Now User
                                    <asp:Label ID="lblChangeLevelUserName" runat="server" ForeColor="Red"></asp:Label>
                                    Power Level is :
                            <asp:Label ID="lblNowLevel" runat="server" Style="margin: 10px;"></asp:Label>
                                </span>
                                <br />
                                You want to change to :
                            <asp:TextBox ID="txtChangeLevel" runat="server" Style="margin: 10px;"></asp:TextBox>
                                <br />
                                <asp:Button ID="btnChangeLevel" runat="server" class="btn btn-success" Text="Change Power Level" Style="margin-bottom: 10px;" />
                                <asp:Button ID="btnBackPL" runat="server" class="btn btn-danger" Text="Back" Style="margin-bottom: 10px;" CausesValidation="false" /><br />
                                <asp:RegularExpressionValidator ID="revForPowerLevel" runat="server" ControlToValidate="txtChangeLevel" ErrorMessage="请输入数字（1-9）<br/>" ForeColor="red" ValidationExpression="[1-9]"></asp:RegularExpressionValidator><br />
                            </asp:Panel>
                            <asp:Panel ID="plFixPassword" runat="server" Visible="false" BorderStyle="Outset" Style="margin-top: 20px; padding-top: 5px; text-align: center; align-self: center;">
                                <span style="margin: 10px; margin: 10px;">Now User
                                    <asp:Label ID="lblFixPassword" runat="server" ForeColor="Red"></asp:Label>
                                    Will Fix His/Her Password to 123456 :
                                </span>
                                <br />
                                <br />
                                <asp:Button ID="btnGoFixPW" runat="server" class="btn btn-success" Text="Change Power Level" Style="margin-bottom: 10px;" />
                                <asp:Button ID="btnBackFPW" runat="server" class="btn btn-danger" Text="Back" Style="margin-bottom: 10px;" CausesValidation="false" /><br />
                                <br />
                            </asp:Panel>


                            <asp:Panel ID="plOK" runat="server" Visible="False">
                                <div id="divOK" class="alert alert-success" role="alert" style="margin-top: 5px;">
                                    <strong>OK</strong>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="plFaild" runat="server" Visible="False">
                                <div id="divFaild" class="alert alert-success" role="alert" style="margin-top: 5px;">
                                    <strong>OK</strong>
                                </div>

                            </asp:Panel>

                        </div>
                        </span>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
</asp:Content>

