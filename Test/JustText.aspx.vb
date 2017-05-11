Imports UserLibrary
Imports ExcelLibrary.LoadExcel
Imports ExcelLibrary
Imports System.Data.OleDb
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Threading
Imports AccessLibrary
Imports System.Data


Partial Class Test_JustText
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary

    Private Sub Test_JustText_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Label1.Text = getIp()

        'strDataTableName As String, strUpDatePath As String, strFileSuffix As String, strIFExcelThenSheetName As String, intMultiFile As Integer, strUpdateSource As String, intNumberOfConfig As Integer


        Dim strtmpListDir As New List(Of String)
        Dim strHeadOfSource As String
        Dim strDir As New List(Of String)
        Dim intWhereYear As Integer
        Dim intWhereMonth As Integer
        Dim intWhereDay As Integer
        Dim intI As Integer
        Dim intJ As Integer
        Dim intK As Integer


        Dim strDataTableName As String = "dt_GSM_PST"
        Dim strUpDatePath As String = "\\gzdwshenji\E\个人目录\李豪生\物理站点表\"
        Dim strFileSuffix As String = "xlsx"
        Dim strIFExcelThenSheetName As String = "Sheet1"
        Dim intMultiFile As Integer = 0
        Dim strUpdateSource As String = "物理站点表%yyyy%mm%dd"

        intJ = strUpdateSource.IndexOf("*")
        intK = strUpdateSource.IndexOf("%")
        If intJ <> -1 And intK <> -1 Then
            intI = CommonLibrary.GetMinNumber(intJ, intK)
            strHeadOfSource = strUpdateSource.Substring(0, intI)
        ElseIf intJ = -1 And intK = -1 Then
            strHeadOfSource = strUpdateSource
        ElseIf intJ = -1 Then
            strHeadOfSource = strUpdateSource.Substring(0, intK)
        Else
            strHeadOfSource = strUpdateSource.Substring(0, intJ)
        End If


        If System.IO.Directory.Exists(strUpDatePath) Then
            strtmpListDir = (From T In IO.Directory.GetFiles(strUpDatePath, strHeadOfSource & "*." & strFileSuffix, IO.SearchOption.AllDirectories)).ToList
        End If
        intWhereYear = strUpdateSource.IndexOf("%yyyy")
        intWhereMonth = strUpdateSource.IndexOf("%mm") - 1
        intWhereDay = strUpdateSource.IndexOf("%dd") - 2
        strDir = CommonLibrary.GetMaxDateFile(strtmpListDir, intWhereYear, intWhereMonth, intWhereDay)

    End Sub



    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    'Dim ss As List(Of String)
    '    'Dim exlExl As LoadExcel
    '    'Dim xlApp As Excel.Application = Nothing
    '    'Dim xlWorkBooks As Excel.Workbooks = Nothing
    '    'Dim xlWorkBook As Excel.Workbook = Nothing
    '    'Dim xlWorkSheets As Excel.Sheets = Nothing


    '    'xlApp = New Excel.Application
    '    'xlApp.DisplayAlerts = False
    '    'xlWorkBooks = xlApp.Workbooks
    '    'xlWorkBook = xlWorkBooks.Open(TextBox1.Text)



    '    ''Label1.Text = System.IO.Directory.Exists(TextBox1.Text)
    '    ''If System.IO.Directory.Exists(TextBox1.Text) Then
    '    ''    ss = (From T In IO.Directory.GetFiles(TextBox1.Text, "*.*", IO.SearchOption.TopDirectoryOnly)).ToList
    '    ''End If
    '    ''For Each sst As String In ss
    '    ''    Label1.Text = Label1.Text & sst & vbCrLf
    '    ''Next
    '    'exlExl = New LoadExcel(TextBox1.Text)
    '    'exlExl.GetInformation()
    '    'Label1.Text = exlExl.strSheets.Count.ToString
    '    ''Label1.Text = xlWorkBook.Sheets.ToString


    '    'Dim srGetData As StreamReader



    '    'srGetData = New StreamReader(TextBox1.Text, System.Text.Encoding.GetEncoding("GB2312"))
    '    'srGetData.ReadLine()
    '    'srGetData.ReadLine()

    '    'Label1.Text = srGetData.ReadLine().Split(Chr(9))(0)



    '    ''Label1.Text = Asc(TextBox1.Text)
    '    'Dim aceAccess As LoadAccess
    '    'Dim dtTest As DataTable
    '    'aceAccess = New LoadAccess("C:\Users\Berry\Desktop\临时数据\0114\GSC CDD_20160114101059.mdb")
    '    'dtTest = aceAccess.ReturnAll("RACLP")
    '    'DataList1.DataSource = dtTest
    '    'DataList1.DataBind()
    '    'Dim bsdlCommonLibrary As BaseSationDetailsLibrary = New BaseSationDetailsLibrary
    '    'DataList1.DataSource = bsdlCommonLibrary.GetParameterConfig("LTE Daily Para")
    '    'DataList1.DataBind()


    '    'Dim bscpCommonLibrary As BSCPara = New BSCPara
    '    'DataList1.DataSource = bscpCommonLibrary.HandelDailyAccessBSCPara("C:\Users\Berry\Desktop\临时数据\0114\GSC CDD_20160114101059.mdb", "dt_GSMP_BSC_Daily", Date.Now, Server.MapPath("/BSDetails/BSCParaConfig.json"))
    '    'DataList1.DataBind()


    '    'SaveBSCParaConfig()
    '    'SaveCellParaConfig()


    '    'Dim gsmcGetPara As GSMCellPara = New GSMCellPara
    '    'Dim ii As Integer
    '    'Label1.text = Date.Now.ToString
    '    'ii = gsmcGetPara.HandelDailyAccessGSMCellPara("C:\Users\Berry\Desktop\临时数据\0114\GSC CDD_20160114101059.mdb", "dt_GSMP_Cell_Daily", Date.Now, Server.MapPath("/BSDetails/GSMCellParaConfig.json"), "SELECT [CELL],[ID] FROM [SanShi_BaseSationDetails].[dbo].[dt_GSM_ID]")
    '    'Label1.text = Label1.text & Date.Now.ToString



    '    'SaveBSCParaDD()
    'End Sub

    'Private Sub Test_JustText_Load(sender As Object, e As EventArgs) Handles Me.Load
    '    Dim bolIsPowerEnough As Boolean = False
    '    ucUserManage = New UserLibrary

    '    If Not IsPostBack Then
    '        Label1.Text = System.IO.Directory.Exists(TextBox1.Text)
    '        Label1.Text = Application("NowUser:" & Session("SanShiUserName")) & "---" & Request.UserAgent.ToString & "<br/>---------"

    '        bolIsPowerEnough = ucUserManage.CheckPower(Session, 2, Response)

    '    End If
    'End Sub

    'Private Sub SaveBSCParaConfig()
    '    Dim strPathConfig As String
    '    Dim swSaveStream As StreamWriter
    '    Dim strSaveJson As String
    '    Dim qq As SQLSandBSCList



    '    Try


    '        qq = New SQLSandBSCList
    '        qq.listpasParaAndSQLS = New List(Of ParameterAndSQL)
    '        qq.listtblBscList = New List(Of TableBSCList)


    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "TRX", .strSQLStatements = "SELECT RXMOP_RXOTRX.CELL, Count(RXMOP_RXOTRX.MO) AS MO之计数 FROM RXMOP_RXOTRX GROUP BY RXMOP_RXOTRX.CELL;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "NumberOfFN", .strSQLStatements = "TRANSFORM First(RLCFP.DCHNO &"" "") AS DCHNOFirst SELECT RLCFP.CELL FROM RLCFP GROUP BY RLCFP.CELL PIVOT RLCFP.CHGR;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "HR_TRX", .strSQLStatements = "SELECT RFCAP.CELL, [RFCAP]![HRCSCC]/16 AS HRCSCC FROM RFCAP GROUP BY RFCAP.CELL, [RFCAP]![HRCSCC]/16;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "AHR_TRX", .strSQLStatements = "SELECT RFCAP.CELL, [RFCAP]![HRCSCC]/16 AS HRCSCC FROM RFCAP GROUP BY RFCAP.CELL, [RFCAP]![HRCSCC]/16;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "CCCH", .strSQLStatements = "SELECT RLCRP.CELL, RLCRP.BCCH FROM RLCRP GROUP BY RLCRP.CELL, RLCRP.BCCH;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "SDCCH", .strSQLStatements = "SELECT RLCRP.CELL, RLCRP.SDCCH FROM RLCRP GROUP BY RLCRP.CELL, RLCRP.SDCCH;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "ACCMIN", .strSQLStatements = "SELECT RLSSP.CELL, RLSSP.ACCMIN FROM RLSSP GROUP BY RLSSP.CELL, RLSSP.ACCMIN;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "CRH", .strSQLStatements = "SELECT RLSSP.CELL, RLSSP.CRH FROM RLSSP GROUP BY RLSSP.CELL, RLSSP.CRH;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "PDCHPREEMPT", .strSQLStatements = "SELECT RLGSP.CELL, RLGSP.PDCHPREEMPT FROM RLGSP GROUP BY RLGSP.CELL, RLGSP.PDCHPREEMPT;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "TBFDLLIM", .strSQLStatements = "SELECT RLGSP.CELL, RLGSP.TBFDLLIM FROM RLGSP GROUP BY RLGSP.CELL, RLGSP.TBFDLLIM;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "FPDCH", .strSQLStatements = "SELECT RLGSP.CELL, RLGSP.FPDCH FROM RLGSP GROUP BY RLGSP.CELL, RLGSP.FPDCH;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "CB", .strSQLStatements = "SELECT RLSBP.CELL, RLSBP.CB FROM RLSBP GROUP BY RLSBP.CELL, RLSBP.CB;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "CBQ", .strSQLStatements = "SELECT RLSBP.CELL, RLSBP.CBQ FROM RLSBP GROUP BY RLSBP.CELL, RLSBP.CBQ;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "MAXRET", .strSQLStatements = "SELECT RLSBP.CELL, RLSBP.MAXRET FROM RLSBP GROUP BY RLSBP.CELL, RLSBP.MAXRET;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "CRO", .strSQLStatements = "SELECT RLSBP.CELL, RLSBP.CRO FROM RLSBP GROUP BY RLSBP.CELL, RLSBP.CRO;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "TO", .strSQLStatements = "SELECT RLSBP.CELL, RLSBP.TO FROM RLSBP GROUP BY RLSBP.CELL, RLSBP.TO;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "PT", .strSQLStatements = "SELECT RLSBP.CELL, RLSBP.PT FROM RLSBP GROUP BY RLSBP.CELL, RLSBP.PT;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "MSRXMIN", .strSQLStatements = "SELECT RLLOP.CELL, RLLOP.MSRXMIN FROM RLLOP GROUP BY RLLOP.CELL, RLLOP.MSRXMIN;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "MSRXSUFF", .strSQLStatements = "SELECT RLLOP.CELL, RLLOP.MSRXSUFF FROM RLLOP GROUP BY RLLOP.CELL, RLLOP.MSRXSUFF;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "BSPWRB", .strSQLStatements = "SELECT RLCPP.CELL, RLCPP.BSPWRB FROM RLCPP GROUP BY RLCPP.CELL, RLCPP.BSPWRB;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "BSPWRT", .strSQLStatements = "SELECT RLCPP.CELL, RLCPP.BSPWRT FROM RLCPP GROUP BY RLCPP.CELL, RLCPP.BSPWRT;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "MSTXPWR", .strSQLStatements = "SELECT RLCPP.CELL, RLCPP.MSTXPWR FROM RLCPP GROUP BY RLCPP.CELL, RLCPP.MSTXPWR;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "LAYER", .strSQLStatements = "SELECT RLLHP.CELL, RLLHP.LAYER FROM RLLHP GROUP BY RLLHP.CELL, RLLHP.LAYER;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "LAYERTHR", .strSQLStatements = "SELECT RLLHP.CELL, RLLHP.LAYERTHR FROM RLLHP GROUP BY RLLHP.CELL, RLLHP.LAYERTHR;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "LAYERHYST", .strSQLStatements = "SELECT RLLHP.CELL, RLLHP.LAYERHYST FROM RLLHP GROUP BY RLLHP.CELL, RLLHP.LAYERHYST;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "PSSTEMP", .strSQLStatements = "SELECT RLLHP.CELL, RLLHP.PSSTEMP FROM RLLHP GROUP BY RLLHP.CELL, RLLHP.PSSTEMP;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "PTIMTEMP", .strSQLStatements = "SELECT RLLHP.CELL, RLLHP.PTIMTEMP FROM RLLHP GROUP BY RLLHP.CELL, RLLHP.PTIMTEMP;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "FASTMSREG", .strSQLStatements = "SELECT RLLHP.CELL, RLLHP.FASTMSREG FROM RLLHP GROUP BY RLLHP.CELL, RLLHP.FASTMSREG;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "NUMREQEGPRSBPC", .strSQLStatements = "SELECT RLBDP.CELL, Sum(RLBDP.NUMREQEGPRSBPC) AS NUMREQEGPRSBPCSum FROM RLBDP GROUP BY RLBDP.CELL;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "CGI", .strSQLStatements = "SELECT RLDEP.CELL, RLDEP.CGI FROM RLDEP GROUP BY RLDEP.CELL, RLDEP.CGI;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "BSIC", .strSQLStatements = "SELECT RLDEP.CELL, RLDEP.BSIC FROM RLDEP GROUP BY RLDEP.CELL, RLDEP.BSIC;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "BCCHNO", .strSQLStatements = "SELECT RLDEP.CELL, RLDEP.BCCHNO FROM RLDEP GROUP BY RLDEP.CELL, RLDEP.BCCHNO;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "AGBLK", .strSQLStatements = "SELECT RLDEP.CELL, RLDEP.AGBLK FROM RLDEP GROUP BY RLDEP.CELL, RLDEP.AGBLK;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "MFRS", .strSQLStatements = "SELECT RLDEP.CELL, RLDEP.MFRMS FROM RLDEP GROUP BY RLDEP.CELL, RLDEP.MFRMS;"})


    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "CELL", .strTableName = "RXMOP_RXOTRX"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "CELL", .strTableName = "RLCFP"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "CELL", .strTableName = "RFCAP"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "CELL", .strTableName = "RLCRP"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "CELL", .strTableName = "RLSSP"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "CELL", .strTableName = "RLGSP"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "CELL", .strTableName = "RLSBP"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "CELL", .strTableName = "RLLOP"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "CELL", .strTableName = "RLCPP"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "CELL", .strTableName = "RLLHP"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "CELL", .strTableName = "RLBDP"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "CELL", .strTableName = "RLDEP"})








    '        If Not System.IO.Directory.Exists(Server.MapPath("/BSDetails/")) Then
    '            My.Computer.FileSystem.CreateDirectory(Server.MapPath("/BSDetails/"))
    '        End If

    '        strPathConfig = Server.MapPath("/BSDetails/GSMCellParaConfig.json")
    '        swSaveStream = New StreamWriter(File.Open(strPathConfig, FileMode.Create))
    '        strSaveJson = SimpleJson.SimpleJson.SerializeObject(qq)
    '        swSaveStream.Write(strSaveJson)
    '        swSaveStream.Flush()
    '        swSaveStream.Close()
    '        swSaveStream = Nothing
    '    Catch ex As Exception
    '        Console.WriteLine(ex)
    '    End Try



    'End Sub



    'Private Sub SaveBSCParaDD()
    '    Dim strPathConfig As String
    '    Dim swSaveStream As StreamWriter
    '    Dim strSaveJson As String
    '    Dim qq As BSCParaSingleDiagram
    '    Dim listqq As List(Of BSCParaSingleDiagram)



    '    Try

    '        listqq = New List(Of BSCParaSingleDiagram)
    '        qq = New BSCParaSingleDiagram
    '        qq.strTitle = "BSC SAE 1153定义容量"
    '        qq.strSQLS = "select * from dt_GSMP_BSC_Daily "
    '        qq.listOnRoad = New List(Of Integer)
    '        qq.listOnRoad.Add(0)
    '        qq.listOnRoad.Add(17)
    '        qq.listOnRoad.Add(18)
    '        listqq.Add(qq)


    '        qq = New BSCParaSingleDiagram
    '        qq.strTitle = "BSC SAE 1153 对应OPER载波数容量"
    '        qq.strSQLS = "select * from dt_GSMP_BSC_Daily "
    '        qq.listOnRoad = New List(Of Integer)
    '        qq.listOnRoad.Add(0)
    '        qq.listOnRoad.Add(17)
    '        qq.listOnRoad.Add(21)
    '        listqq.Add(qq)



    '        If Not System.IO.Directory.Exists(Server.MapPath("/ThisLife/BSCPara/Config/")) Then
    '            My.Computer.FileSystem.CreateDirectory(Server.MapPath("/ThisLife/BSCPara/Config/"))
    '        End If

    '        strPathConfig = Server.MapPath("/ThisLife/BSCPara/Config/GSMBSCParaSingleConfig.json")
    '        swSaveStream = New StreamWriter(File.Open(strPathConfig, FileMode.Create))
    '        strSaveJson = SimpleJson.SimpleJson.SerializeObject(listqq)
    '        swSaveStream.Write(strSaveJson)
    '        swSaveStream.Flush()
    '        swSaveStream.Close()
    '        swSaveStream = Nothing
    '    Catch ex As Exception
    '        Console.WriteLine(ex)
    '    End Try

    'End Sub


    'Private Sub SaveCellParaConfig()
    '    Dim strPathConfig As String
    '    Dim swSaveStream As StreamWriter
    '    Dim strSaveJson As String
    '    Dim qq As SQLSandBSCList



    '    Try


    '        qq = New SQLSandBSCList
    '        qq.listpasParaAndSQLS = New List(Of ParameterAndSQL)
    '        qq.listtblBscList = New List(Of TableBSCList)


    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "AMRFRMAXTRAFFIC_Value", .strSQLStatements = "SELECT DBTSP_AMRFRMAXTRAFFIC.NE, First(DBTSP_AMRFRMAXTRAFFIC.VALUE) AS OVALUE FROM DBTSP_AMRFRMAXTRAFFIC GROUP BY DBTSP_AMRFRMAXTRAFFIC.NE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "AMRFRMAXTRAFFIC_FValue", .strSQLStatements = "SELECT DBTSP_AMRFRMAXTRAFFIC.NE, First(DBTSP_AMRFRMAXTRAFFIC.FCVALUE) AS FCVALUE FROM DBTSP_AMRFRMAXTRAFFIC GROUP BY DBTSP_AMRFRMAXTRAFFIC.NE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "AMRHRMAXTRAFFIC_Value", .strSQLStatements = "SELECT DBTSP_AMRHRMAXTRAFFIC.NE, First(DBTSP_AMRHRMAXTRAFFIC.VALUE) AS OVALUE FROM DBTSP_AMRHRMAXTRAFFIC GROUP BY DBTSP_AMRHRMAXTRAFFIC.NE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "AMRHRMAXTRAFFIC_FValue", .strSQLStatements = "SELECT DBTSP_AMRHRMAXTRAFFIC.NE, First(DBTSP_AMRHRMAXTRAFFIC.FCVALUE) AS FCVALUE FROM DBTSP_AMRHRMAXTRAFFIC GROUP BY DBTSP_AMRHRMAXTRAFFIC.NE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "EGPRSBPCLIMIT_Value", .strSQLStatements = "SELECT DBTSP_CME20_EGPRS.NE, DBTSP_CME20_EGPRS.VALUE FROM DBTSP_CME20_EGPRS GROUP BY DBTSP_CME20_EGPRS.NE, DBTSP_CME20_EGPRS.VALUE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "EGPRSBPCLIMIT_FValue", .strSQLStatements = "SELECT DBTSP_CME20_EGPRS.NE, DBTSP_CME20_EGPRS.FCVALUE FROM DBTSP_CME20_EGPRS GROUP BY DBTSP_CME20_EGPRS.NE, DBTSP_CME20_EGPRS.FCVALUE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "MCTRTRX_Value", .strSQLStatements = "SELECT DBTSP_CME20_MCTRTRX.NE, DBTSP_CME20_MCTRTRX.VALUE FROM DBTSP_CME20_MCTRTRX GROUP BY DBTSP_CME20_MCTRTRX.NE, DBTSP_CME20_MCTRTRX.VALUE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "MCTRTRX_FValue", .strSQLStatements = "SELECT DBTSP_CME20_MCTRTRX.NE, DBTSP_CME20_MCTRTRX.FCVALUE FROM DBTSP_CME20_MCTRTRX GROUP BY DBTSP_CME20_MCTRTRX.NE, DBTSP_CME20_MCTRTRX.FCVALUE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "HRMAXTRAFFIC_Value", .strSQLStatements = "SELECT DBTSP_HRMAXTRAFFIC.NE, DBTSP_HRMAXTRAFFIC.VALUE FROM DBTSP_HRMAXTRAFFIC GROUP BY DBTSP_HRMAXTRAFFIC.NE, DBTSP_HRMAXTRAFFIC.VALUE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "HRMAXTRAFFIC_FValue", .strSQLStatements = "SELECT DBTSP_HRMAXTRAFFIC.NE, DBTSP_HRMAXTRAFFIC.FCVALUE FROM DBTSP_HRMAXTRAFFIC GROUP BY DBTSP_HRMAXTRAFFIC.NE, DBTSP_HRMAXTRAFFIC.FCVALUE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "IOEXP", .strSQLStatements = "SELECT IOEXP.NE, IOEXP.IDENTITY FROM IOEXP GROUP BY IOEXP.NE, IOEXP.IDENTITY;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "TRA_AMRHR", .strSQLStatements = "SELECT RRTPP_TRAALL.NE, Sum(RRTPP_TRAALL.RNOTRA) AS RNOTRAsum FROM RRTPP_TRAALL WHERE (((RRTPP_TRAALL.TRAPOOL)=""AMRHR"")) GROUP BY RRTPP_TRAALL.NE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "TRA_AMRFR", .strSQLStatements = "SELECT RRTPP_TRAALL.NE, Sum(RRTPP_TRAALL.RNOTRA) AS RNOTRAsum FROM RRTPP_TRAALL WHERE (((RRTPP_TRAALL.TRAPOOL)=""AMRFR"")) GROUP BY RRTPP_TRAALL.NE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "TRA_FR", .strSQLStatements = "SELECT RRTPP_TRAALL.NE, Sum(RRTPP_TRAALL.RNOTRA) AS RNOTRAsum FROM RRTPP_TRAALL WHERE (((RRTPP_TRAALL.TRAPOOL)=""FR"")) GROUP BY RRTPP_TRAALL.NE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "TRA_EFR", .strSQLStatements = "SELECT RRTPP_TRAALL.NE, Sum(RRTPP_TRAALL.RNOTRA) AS RNOTRAsum FROM RRTPP_TRAALL WHERE (((RRTPP_TRAALL.TRAPOOL)=""EFR"")) GROUP BY RRTPP_TRAALL.NE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "TRA_HR", .strSQLStatements = "SELECT RRTPP_TRAALL.NE, Sum(RRTPP_TRAALL.RNOTRA) AS RNOTRAsum FROM RRTPP_TRAALL WHERE (((RRTPP_TRAALL.TRAPOOL)=""HR"")) GROUP BY RRTPP_TRAALL.NE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "SAE1153_NI", .strSQLStatements = "SELECT SAAEP_SAE1153.NE, SAAEP_SAE1153.NI FROM SAAEP_SAE1153 GROUP BY SAAEP_SAE1153.NE, SAAEP_SAE1153.NI;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "SAE1153_NIU", .strSQLStatements = "SELECT SAAEP_SAE1153.NE, SAAEP_SAE1153.NIU FROM SAAEP_SAE1153 GROUP BY SAAEP_SAE1153.NE, SAAEP_SAE1153.NIU;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "NUMREQEGPRSBPC", .strSQLStatements = "SELECT RLBDP.NE, Sum(RLBDP.NUMREQEGPRSBPC) AS NUMREQEGPRSBPCsum FROM RLBDP GROUP BY RLBDP.NE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "MO_TRX", .strSQLStatements = "SELECT RXMOP_RXOTRX.NE, Count(RXMOP_RXOTRX.MO) AS MOcount FROM RXMOP_RXOTRX GROUP BY RXMOP_RXOTRX.NE;"})
    '        qq.listpasParaAndSQLS.Add(New ParameterAndSQL With {.strColName = "MO_TRX_Oper", .strSQLStatements = "SELECT RXMFP_RXOTRX.NE, Count(RXMFP_RXOTRX.MO) AS MOcount FROM RXMFP_RXOTRX WHERE (((RXMFP_RXOTRX.STATE)=""OPER"")) GROUP BY RXMFP_RXOTRX.NE;"})


    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "NE", .strTableName = "DBTSP_AMRFRMAXTRAFFIC"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "NE", .strTableName = "DBTSP_AMRHRMAXTRAFFIC"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "NE", .strTableName = "DBTSP_CME20_EGPRS"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "NE", .strTableName = "DBTSP_CME20_MCTRTRX"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "NE", .strTableName = "DBTSP_HRMAXTRAFFIC"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "NE", .strTableName = "IOEXP"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "NE", .strTableName = "RRTPP_TRAALL"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "NE", .strTableName = "SAAEP_SAE1153"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "NE", .strTableName = "RLBDP"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "NE", .strTableName = "RXMOP_RXOTRX"})
    '        qq.listtblBscList.Add(New TableBSCList With {.strBSCName = "NE", .strTableName = "RXMFP_RXOTRX"})








    '        If Not System.IO.Directory.Exists(Server.MapPath("/BSDetails/")) Then
    '            My.Computer.FileSystem.CreateDirectory(Server.MapPath("/BSDetails/"))
    '        End If

    '        strPathConfig = Server.MapPath("/BSDetails/GSMCellParaConfig.json")
    '        swSaveStream = New StreamWriter(File.Open(strPathConfig, FileMode.Create))
    '        strSaveJson = SimpleJson.SimpleJson.SerializeObject(qq)
    '        swSaveStream.Write(strSaveJson)
    '        swSaveStream.Flush()
    '        swSaveStream.Close()
    '        swSaveStream = Nothing
    '    Catch ex As Exception
    '        Console.WriteLine(ex)
    '    End Try



    'End Sub

End Class
