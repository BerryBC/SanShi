<%@ Page Async="true" EnableSessionState="ReadOnly" Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BaseSationDetails.aspx.vb" Inherits="BSDetails_BaseSationDetails" %>

<%----第10号页面--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section>
                <div class="container">
                    <div class="col-sm-12" style="text-align: left;" aria-multiline="True">
                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">基站基础信息管理 ( Details of Base Sation Management )</h2>
                        </div>

                        <asp:DataList ID="dlBaseSationDetails" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" class="col-sm-12" OnItemCommand="dlBaseSationDetails_ItemCommand">
                            <ItemTemplate>
                                <td>
                                    <div class="col-sm-11" style="text-align: center; align-self: center; margin-bottom: 10px;">
                                        <asp:Button ID="btnGoModifyDataTableSet" runat="server" Text='<%# Eval("ConfigName") %>' class="form-control btn-info" CommandName="ModifyDataTableMana" ForeColor="White" Font-Bold="true" onmouseover="this.style.backgroundColor='#BEC9F6';this.style.color='buttontext';this.style.cursor='default';" onmouseout="this.style.backgroundColor='';this.style.color='';this.style.color='White'" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "DataTableID")%>' />

                                    </div>
                                </td>
                            </ItemTemplate>

                        </asp:DataList>

                    </div>
                </div>
            </section>
            <section>
                <div class="container">
                    <asp:Button ID="btnAddOneConfig" runat="server" Text="增加一个配置" class="form-control btn-success" />
                    <div class="col-sm-12" style="text-align: left;" aria-multiline="True">

                        <div class="" data-wow-delay="0.1s">
                            <h2 style="border-bottom-style: groove;"></h2>
                        </div>
                        配置编号 :<br />
                        <asp:TextBox ID="txtNumberOfConfig" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />

                        配置名 :<br />
                        <asp:TextBox ID="txtConfigName" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />

                        表名 :<br />
                        <asp:TextBox ID="txtDataTableName" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        最后更新的时间 :<br />
                        <asp:TextBox ID="txtLastUpdateTime" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        更新路径 :<br />
                        <asp:TextBox ID="txtUpDatePath" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        更新源文件名 :<br />
                        <asp:TextBox ID="txtUpdateSource" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />

                        文件后缀名 :<br />
                        <asp:TextBox ID="txtFileSuffix" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        如果是Excel那是哪个Sheet :<br />
                        <asp:TextBox ID="txtIFExcelThenSheetName" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        是否多个文件 :<br />
                        <asp:TextBox ID="txtMultiFile" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />

                        <asp:Label ID="lblWhatID" runat="server" Text="Label" Visible="false"></asp:Label>
                    </div>
                    <div style="text-align: center;" id="ModifyButtonDIV">
                        <asp:Timer ID="timerLoading" runat="server" Enabled="false" Interval="500"></asp:Timer>

                        <br />
                        <asp:Button ID="btnWantModify" runat="server" class="btn btn-info" Text="开始表格更新信息" Style="margin: 10px;" Enabled="false" />
                        <asp:Button ID="btnConfirmModify" runat="server" class="btn btn-success" Text="确定修改表格更新信息" Style="margin: 10px;" Enabled="false" />
                        <asp:Button ID="btnGoInsert" runat="server" Text="开始入数" class="btn btn-warning" Enabled="false" />
                        <asp:Button ID="btnCheckHowCount" runat="server" class="btn btn-info" Text="查询该表格现在记录数" Style="margin: 10px;" Enabled="false" /><br />
                        <asp:Button ID="btnCheckAllTheInformation" runat="server" class="btn btn-primary" Text="查询所有表格" Style="margin: 10px;" Enabled="true" /><br />

                        <asp:Panel ID="plYeahGo" runat="server" Visible="False" Style="text-align: center;">
                            <div class="alert alert-success" role="alert" style="margin-top: 5px;">
                                <asp:Label ID="lblLoading" runat="server" Text="Inserting"></asp:Label>
                            </div>
                        </asp:Panel>

                    </div>

                </div>
            </section>
            <section>
                <div class="container">
                    <div class="col-sm-12" style="text-align: left;" aria-multiline="True">

                        <div class="wow bounceInLeft" data-wow-delay="0.1s" style="text-align: left;">
                            <h2 style="border-bottom-style: groove;">当前管理操作记录 ( Log The Managerment Operation )</h2>
                        </div>

                        <div class="col-sm-12" style="text-align: center; align-self: center;">

                            <asp:TextBox ID="txtLogMessage" runat="server" class="form-control" TextMode="MultiLine" Style="overflow-y: visible;" Wrap="true" Height="300px" Enabled="false"></asp:TextBox>

                        </div>


                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

