Imports SQLServerLibrary
Imports System.Data
Imports System.Data.SqlClient

Partial Class ThisLife_CellInfo_GSMCellInfo
    Inherits System.Web.UI.Page
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary
    Dim ucUserManage As UserLibrary = New UserLibrary

    Private Sub ThisLife_CellInfo_GSMCellInfo_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            If Not IsPostBack Then
                ucUserManage.CheckPower(Session, 2, Response)
                If (Request.Params("CellName") Is Nothing) Then
                    lblError.Visible = False
                    plCell.Visible = False
                    plFindCell.Visible = True
                Else
                    plFindCell.Visible = False
                    timerGetMap.Enabled = True
                End If


            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(28, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=28&eu=" & "")
            Else
                erlErrorReport.ReportServerError(28, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=28&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub
    Private Sub BindTheData(ByVal strCell As String)
        Dim scmdCommand As SqlCommand
        Dim dtGetCDD As DataTable
        Dim dtIndexOfGSMCell As DataTable
        Dim tableHtmlTable As Table
        Dim thrHead As TableHeaderRow
        Dim thcHead As TableHeaderCell
        Dim tbrContent As TableRow
        Dim tbcContent As TableCell
        Dim intMaxDataTableCols As Integer
        Dim strSQLSHandel As String
        Dim strID As String
        Dim gsmioclLibrary As GSMIndexOfCellLibrary
        Dim dateWhatDate As Date
        Dim intI As Integer
        Dim strJSForMap As String
        Dim doubleLo As Double
        Dim doubleLa As Double
        Dim bolIsHave As Boolean

        'ScriptManager.RegisterClientScriptInclude(Me, Me.GetType, "BaiduMapSrc", "http://api.map.baidu.com/api?v=2.0&ak=C4msdAZymuN9mjstHB5H6nYT")
        'ScriptManager.RegisterClientScriptBlock(upUpdatepanel, Me.GetType, "BaiduMapSrc", "<script type=""text/javascript"" src=""http://api.map.baidu.com/api?v=2.0&ak=C4msdAZymuN9mjstHB5H6nYT""></script>", False)
        Try

            bolIsHave = False


            strJSForMap = ""

            gsmioclLibrary = New GSMIndexOfCellLibrary
            scmdCommand = sqllSSLibrary.GetCommandStr("SELECT * FROM [dbo].[dt_GSM_CDD] WHERE cell_id='" & strCell & "'", CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
            dtGetCDD = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)
            If dtGetCDD.Rows.Count > 0 Then

                lblBaseSation.Text = dtGetCDD.Rows(0).Item("基站名").ToString
                lblBSC.Text = dtGetCDD.Rows(0).Item("bsc_id").ToString
                lblEnglishName.Text = strCell
                lblGrid.Text = dtGetCDD.Rows(0).Item("网格").ToString
                lblID.Text = dtGetCDD.Rows(0).Item("ID").ToString
                lblPartition.Text = dtGetCDD.Rows(0).Item("网优九分区").ToString
                bolIsHave = True
            Else
                lblBaseSation.Text = ""
                lblBSC.Text = ""
                lblEnglishName.Text = ""
                lblGrid.Text = ""
                lblID.Text = ""
                lblPartition.Text = ""
            End If


            strID = GSMCellInfoLibrary.GetIDByEnglishName(strCell)
            '-------------不查看业务了---------------
            ' dateWhatDate = gsmioclLibrary.GetGSMIndexMaxDate

            ' strSQLSHandel = "SELECT  * FROM [SanShi_Traffic].[dbo].[dt_GSM_Daily_Grib_Traffic] Where ( [ID]='" & strID & "' and [Day]>='" & dateWhatDate.AddDays(-10).ToShortDateString & "') order by [Day] desc"
            ' scmdCommand = sqllSSLibrary.GetCommandStr(strSQLSHandel, CommonLibrary.GetSQLServerConnect("ConnectionTrafficDB"))
            ' dtIndexOfGSMCell = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)

            ' tableHtmlTable = tbOutPutIndicators
            ' thrHead = New TableHeaderRow

            ' intMaxDataTableCols = dtIndexOfGSMCell.Columns.Count

            ' If dtIndexOfGSMCell.Rows.Count > 0 Then

            '     bolIsHave = True

            '     For Each dcColoumnForHead In dtIndexOfGSMCell.Columns
            '         thcHead = New TableHeaderCell
            '         thcHead.Text = dcColoumnForHead.Caption
            '         thcHead.BorderStyle = BorderStyle.Groove
            '         thcHead.VerticalAlign = VerticalAlign.Middle
            '         thcHead.HorizontalAlign = HorizontalAlign.Left
            '         thcHead.Wrap = False

            '         thrHead.Cells.Add(thcHead)
            '     Next

            '     tableHtmlTable.Rows.AddAt(0, thrHead)


            '     For Each drLoadData In dtIndexOfGSMCell.Rows

            '         tbrContent = New TableRow
            '         For intI = 0 To intMaxDataTableCols - 1
            '             tbcContent = New TableCell
            '             tbcContent.Text = drLoadData.Item(intI).ToString
            '             tbcContent.BorderStyle = BorderStyle.Groove
            '             tbcContent.VerticalAlign = VerticalAlign.Middle
            '             tbcContent.HorizontalAlign = HorizontalAlign.Left
            '             tbcContent.Wrap = False
            '             tbrContent.Cells.Add(tbcContent)
            '         Next
            '         tableHtmlTable.Rows.Add(tbrContent)
            '     Next
            ' Else
            '     For Each dcColoumnForHead In dtIndexOfGSMCell.Columns
            '         thcHead = New TableHeaderCell
            '         thcHead.Text = dcColoumnForHead.Caption
            '         thcHead.BorderStyle = BorderStyle.Groove
            '         thcHead.VerticalAlign = VerticalAlign.Middle
            '         thcHead.HorizontalAlign = HorizontalAlign.Left
            '         thcHead.Wrap = False

            '         thrHead.Cells.Add(thcHead)
            '     Next

            '     tableHtmlTable.Rows.AddAt(0, thrHead)

            '     tbrContent = New TableRow
            '     tbcContent = New TableCell
            '     tbcContent.Text = "最近无业务量"
            '     tbcContent.BorderStyle = BorderStyle.Groove
            '     tbcContent.VerticalAlign = VerticalAlign.Middle
            '     tbcContent.HorizontalAlign = HorizontalAlign.Left
            '     tbcContent.Wrap = False
            '     tbrContent.Cells.Add(tbcContent)
            '     tableHtmlTable.Rows.Add(tbrContent)

            ' End If
            '----------------------------------------

            strSQLSHandel = "SELECT *  FROM [SanShi_BaseSationDetails].[dbo].[dt_GSMP_Cell_Daily]   WHERE Cell='" & strCell & "'"
            scmdCommand = sqllSSLibrary.GetCommandStr(strSQLSHandel, CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
            dtIndexOfGSMCell = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)


            tableHtmlTable = tbOutPutParameter
            thrHead = New TableHeaderRow

            intMaxDataTableCols = dtIndexOfGSMCell.Columns.Count

            If dtIndexOfGSMCell.Rows.Count > 0 Then

                bolIsHave = True


                thcHead = New TableHeaderCell
                thcHead.Text = "参数名字"
                thcHead.BorderStyle = BorderStyle.Groove
                thcHead.VerticalAlign = VerticalAlign.Middle
                thcHead.HorizontalAlign = HorizontalAlign.Left
                thcHead.Wrap = False

                thrHead.Cells.Add(thcHead)

                thcHead = New TableHeaderCell
                thcHead.Text = "参数配置"
                thcHead.BorderStyle = BorderStyle.Groove
                thcHead.VerticalAlign = VerticalAlign.Middle
                thcHead.HorizontalAlign = HorizontalAlign.Left
                thcHead.Wrap = False

                thrHead.Cells.Add(thcHead)


                tableHtmlTable.Rows.AddAt(0, thrHead)



                For intI = 0 To intMaxDataTableCols - 1
                    tbrContent = New TableRow
                    tbcContent = New TableCell
                    tbcContent.Text = dtIndexOfGSMCell.Columns(intI).Caption
                    tbcContent.BorderStyle = BorderStyle.Groove
                    tbcContent.VerticalAlign = VerticalAlign.Middle
                    tbcContent.HorizontalAlign = HorizontalAlign.Left
                    tbcContent.Wrap = False
                    tbrContent.Cells.Add(tbcContent)

                    tbcContent = New TableCell
                    tbcContent.Text = dtIndexOfGSMCell.Rows(0).Item(intI).ToString
                    tbcContent.BorderStyle = BorderStyle.Groove
                    tbcContent.VerticalAlign = VerticalAlign.Middle
                    tbcContent.HorizontalAlign = HorizontalAlign.Left
                    tbcContent.Wrap = False
                    tbrContent.Cells.Add(tbcContent)
                    tableHtmlTable.Rows.Add(tbrContent)

                Next
            Else
                thcHead = New TableHeaderCell
                thcHead.Text = "参数名字"
                thcHead.BorderStyle = BorderStyle.Groove
                thcHead.VerticalAlign = VerticalAlign.Middle
                thcHead.HorizontalAlign = HorizontalAlign.Left
                thcHead.Wrap = False

                thrHead.Cells.Add(thcHead)

                thcHead = New TableHeaderCell
                thcHead.Text = "参数配置"
                thcHead.BorderStyle = BorderStyle.Groove
                thcHead.VerticalAlign = VerticalAlign.Middle
                thcHead.HorizontalAlign = HorizontalAlign.Left
                thcHead.Wrap = False

                thrHead.Cells.Add(thcHead)


                tableHtmlTable.Rows.AddAt(0, thrHead)

                tbrContent = New TableRow
                tbcContent = New TableCell
                tbcContent.Text = "现网无参数配置"
                tbcContent.BorderStyle = BorderStyle.Groove
                tbcContent.VerticalAlign = VerticalAlign.Middle
                tbcContent.HorizontalAlign = HorizontalAlign.Left
                tbcContent.Wrap = False
                tbrContent.Cells.Add(tbcContent)
                tableHtmlTable.Rows.Add(tbrContent)

            End If



            strSQLSHandel = "SELECT TOP 36 [ChangePara] ,[ChangeValue] ,[ChangeDate]  FROM [SanShi_Log].[dbo].[dt_GSMP_BSC_Daily_ChangeLog]   WHERE ChangeNE = '" & strID & "'   ORDER BY ChangeDate DESC"
            scmdCommand = sqllSSLibrary.GetCommandStr(strSQLSHandel, CommonLibrary.GetSQLServerConnect("ConnectionLogDB"))
            dtIndexOfGSMCell = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)

            tableHtmlTable = tbOutputHistory
            thrHead = New TableHeaderRow

            intMaxDataTableCols = dtIndexOfGSMCell.Columns.Count

            If dtIndexOfGSMCell.Rows.Count > 0 Then

                bolIsHave = True


                For Each dcColoumnForHead In dtIndexOfGSMCell.Columns
                    thcHead = New TableHeaderCell
                    thcHead.Text = dcColoumnForHead.Caption
                    thcHead.BorderStyle = BorderStyle.Groove
                    thcHead.VerticalAlign = VerticalAlign.Middle
                    thcHead.HorizontalAlign = HorizontalAlign.Left
                    thcHead.Wrap = False

                    thrHead.Cells.Add(thcHead)
                Next

                tableHtmlTable.Rows.AddAt(0, thrHead)


                For Each drLoadData In dtIndexOfGSMCell.Rows

                    tbrContent = New TableRow
                    For intI = 0 To intMaxDataTableCols - 1
                        tbcContent = New TableCell
                        tbcContent.Text = drLoadData.Item(intI).ToString
                        tbcContent.BorderStyle = BorderStyle.Groove
                        tbcContent.VerticalAlign = VerticalAlign.Middle
                        tbcContent.HorizontalAlign = HorizontalAlign.Left
                        tbcContent.Wrap = False
                        tbrContent.Cells.Add(tbcContent)
                    Next
                    tableHtmlTable.Rows.Add(tbrContent)
                Next
            Else
                For Each dcColoumnForHead In dtIndexOfGSMCell.Columns
                    thcHead = New TableHeaderCell
                    thcHead.Text = dcColoumnForHead.Caption
                    thcHead.BorderStyle = BorderStyle.Groove
                    thcHead.VerticalAlign = VerticalAlign.Middle
                    thcHead.HorizontalAlign = HorizontalAlign.Left
                    thcHead.Wrap = False

                    thrHead.Cells.Add(thcHead)
                Next

                tableHtmlTable.Rows.AddAt(0, thrHead)

                tbrContent = New TableRow
                tbcContent = New TableCell
                tbcContent.Text = "无该小区历史参数配置修改"
                tbcContent.BorderStyle = BorderStyle.Groove
                tbcContent.VerticalAlign = VerticalAlign.Middle
                tbcContent.HorizontalAlign = HorizontalAlign.Left
                tbcContent.Wrap = False
                tbrContent.Cells.Add(tbcContent)
                tableHtmlTable.Rows.Add(tbrContent)

            End If


            strSQLSHandel = " SELECT dt_GSM_PST.经度,dt_GSM_PST.纬度 	FROM dt_GSM_PST INNER JOIN dbo.dt_GSM_CDD ON dbo.dt_GSM_CDD.cell_id=dt_GSM_PST.[CELL] WHERE dt_GSM_CDD.cell_id='" & strCell & "' "
            scmdCommand = sqllSSLibrary.GetCommandStr(strSQLSHandel, CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
            dtIndexOfGSMCell = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)

            If dtIndexOfGSMCell.Rows.Count > 0 Then

                bolIsHave = True

                doubleLo = dtIndexOfGSMCell.Rows(0).Item(0)
                doubleLa = dtIndexOfGSMCell.Rows(0).Item(1)

                strJSForMap = "var map = new BMap.Map(""CellMap"");map.centerAndZoom(new BMap.Point(" & doubleLo.ToString & ", " & doubleLa.ToString & "), 18);map.addControl(new BMap.MapTypeControl());map.setCurrentCity(""广州"");map.enableScrollWheelZoom(true);"
                strJSForMap += "var convertor = new BMap.Convertor();"

                strJSForMap += " var pointArr = [];pointArr.push(new BMap.Point(" & doubleLo.ToString & ", " & doubleLa.ToString & "));convertor.translate(pointArr, 1, 5, translateCallback = function (data){if(data.status === 0) {map.centerAndZoom(data.points[0], 18);var rectangle = new BMap.Polygon([new BMap.Point(data.points[0].lng-0.005,data.points[0].lat-0.005),new BMap.Point(data.points[0].lng+0.005,data.points[0].lat-0.005),new BMap.Point(data.points[0].lng+0.005,data.points[0].lat+0.005),new BMap.Point(data.points[0].lng-0.005,data.points[0].lat+0.005)], {strokeColor:""purple"", strokeWeight:2, strokeOpacity:0.5,fillOpacity:0.2});map.addOverlay(rectangle); }});"

            End If



            strSQLSHandel = "SELECT dt_GSM_PST.[基站名] ,dt_GSM_PST.[CELL] AS 小区名 ,dt_GSM_PST.[分区] ,'GSM' as 网络,dt_GSM_PST.[室内外] , dt_GSM_PST.[经度] , dt_GSM_PST.[纬度] , [dbo].[dt_GSM_EP].[天线方向] FROM (dt_GSM_PST  INNER JOIN ( SELECT dt_GSM_PST.经度,dt_GSM_PST.纬度 	FROM dt_GSM_PST INNER JOIN dbo.dt_GSM_CDD ON dbo.dt_GSM_CDD.cell_id=dt_GSM_PST.[CELL] WHERE dt_GSM_CDD.cell_id='" & strCell & "' ) CurrentL	ON ((dt_GSM_PST.经度> (currentl.经度-0.005) AND dt_GSM_PST.经度< (currentl.经度+0.005)) AND (dt_GSM_PST.纬度> (currentl.纬度-0.005) AND dt_GSM_PST.纬度< (currentl.纬度+0.005))) ) INNER JOIN [dbo].[dt_GSM_EP] ON [dbo].[dt_GSM_EP].[cell_id]=dt_GSM_PST.[CELL]"
            scmdCommand = sqllSSLibrary.GetCommandStr(strSQLSHandel, CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
            dtIndexOfGSMCell = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)

            tableHtmlTable = tbNeighborCell
            thrHead = New TableHeaderRow

            intMaxDataTableCols = dtIndexOfGSMCell.Columns.Count


            For intI = 0 To 3

                thcHead = New TableHeaderCell
                thcHead.Text = dtIndexOfGSMCell.Columns(intI).Caption
                thcHead.BorderStyle = BorderStyle.Groove
                thcHead.VerticalAlign = VerticalAlign.Middle
                thcHead.HorizontalAlign = HorizontalAlign.Left
                thcHead.Wrap = False

                thrHead.Cells.Add(thcHead)
            Next

            tableHtmlTable.Rows.AddAt(0, thrHead)



            If dtIndexOfGSMCell.Rows.Count > 0 Then
                bolIsHave = True

                strJSForMap += "var myIconG = new BMap.Icon(""/IMG/red-circle.png"", new BMap.Size(23, 25), {offset: new BMap.Size(10, 25),imageOffset: new BMap.Size(0, 0)});"


                For Each drLoadData In dtIndexOfGSMCell.Rows

                    tbrContent = New TableRow
                    For intI = 0 To 3
                        tbcContent = New TableCell

                        If intI = 1 Then
                            tbcContent.Text = "<a href=""http://" & Request.Url.Host & "/ThisLife/CellInfo/GSMCellInfo.aspx?CellName=" & drLoadData.Item(intI).ToString & """>" & drLoadData.Item(intI).ToString & "</a>"
                        Else
                            tbcContent.Text = drLoadData.Item(intI).ToString
                        End If
                        tbcContent.BorderStyle = BorderStyle.Groove
                        tbcContent.VerticalAlign = VerticalAlign.Middle
                        tbcContent.HorizontalAlign = HorizontalAlign.Left
                        tbcContent.Wrap = False
                        tbrContent.Cells.Add(tbcContent)

                    Next
                    tableHtmlTable.Rows.Add(tbrContent)

                    strJSForMap += " var pointArr = [];pointArr.push(new BMap.Point(" & drLoadData.Item(5).ToString & ", " & drLoadData.Item(6).ToString & "));convertor.translate(pointArr, 1, 5, translateCallback = function (data){"
                    strJSForMap += "if(data.status === 0) {var marker = new BMap.Marker(data.points[0],{icon:myIconG});map.addOverlay(marker);"
                    strJSForMap += "var label = new BMap.Label(""" & drLoadData.Item(0).ToString & """,{offset:new BMap.Size(20,-10)});"
                    strJSForMap += "marker.setLabel(label);"
                    strJSForMap += "}});"
                    If Not IsDBNull(drLoadData.Item(7)) And drLoadData.Item(4) = "室外" Then


                        strJSForMap += " var pointArr = [];pointArr.push(new BMap.Point(" & drLoadData.Item(5).ToString & ", " & drLoadData.Item(6).ToString & "),new BMap.Point(" & drLoadData.Item(5).ToString + 0.0005 * Math.Sin(2 * Math.PI / 360 * drLoadData.Item(7)) & ", " & drLoadData.Item(6).ToString + 0.0005 * Math.Cos(2 * Math.PI / 360 * drLoadData.Item(7)) & "));convertor.translate(pointArr, 1, 5, translateCallback = function (data){"
                        strJSForMap += "if(data.status === 0) {var polyline = new BMap.Polyline([data.points[0],data.points[1]], {strokeColor:""red"", strokeWeight:2, strokeOpacity:0.8}); map.addOverlay(polyline);"
                        strJSForMap += "}});"

                    End If





                Next
            Else

                tbrContent = New TableRow
                tbcContent = New TableCell
                tbcContent.Text = "无该小区2G邻区信息"
                tbcContent.BorderStyle = BorderStyle.Groove
                tbcContent.VerticalAlign = VerticalAlign.Middle
                tbcContent.HorizontalAlign = HorizontalAlign.Left
                tbcContent.Wrap = False
                tbrContent.Cells.Add(tbcContent)
                tableHtmlTable.Rows.Add(tbrContent)

            End If


            strSQLSHandel = "SELECT [SiteName] AS [基站名] ,[CellName] AS  小区名 ,[站点所属片区] AS [分区],'TD' as 网络,类型,Longitude,Latitude,Azimuth FROM [dbo].[dt_TDS_CDD] INNER JOIN ( 	SELECT dt_GSM_PST.经度,dt_GSM_PST.纬度 	FROM dt_GSM_PST INNER JOIN dbo.dt_GSM_CDD	ON dbo.dt_GSM_CDD.cell_id=dt_GSM_PST.[CELL] 	WHERE dt_GSM_CDD.cell_id='" & strCell & "' ) CurrentL	ON (([Longitude]> (currentl.经度-0.005) AND [Longitude]< (currentl.经度+0.005)) AND ([Latitude]> (currentl.纬度-0.005) AND [Latitude]< (currentl.纬度+0.005)))"
            scmdCommand = sqllSSLibrary.GetCommandStr(strSQLSHandel, CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
            dtIndexOfGSMCell = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)

            tableHtmlTable = tbNeighborCell
            thrHead = New TableHeaderRow

            intMaxDataTableCols = dtIndexOfGSMCell.Columns.Count

            If dtIndexOfGSMCell.Rows.Count > 0 Then
                bolIsHave = True

                strJSForMap += "var myIconT = new BMap.Icon(""/IMG/grn-circle.png"", new BMap.Size(23, 25), {offset: new BMap.Size(10, 25),imageOffset: new BMap.Size(0, 0)});"


                For Each drLoadData In dtIndexOfGSMCell.Rows

                    tbrContent = New TableRow
                    For intI = 0 To 3
                        tbcContent = New TableCell
                        tbcContent.Text = drLoadData.Item(intI).ToString
                        tbcContent.BorderStyle = BorderStyle.Groove
                        tbcContent.VerticalAlign = VerticalAlign.Middle
                        tbcContent.HorizontalAlign = HorizontalAlign.Left
                        tbcContent.Wrap = False
                        tbrContent.Cells.Add(tbcContent)
                    Next
                    tableHtmlTable.Rows.Add(tbrContent)


                    strJSForMap += " var pointArr = [];pointArr.push(new BMap.Point(" & drLoadData.Item(5).ToString & ", " & drLoadData.Item(6).ToString & "));convertor.translate(pointArr, 1, 5, translateCallback = function (data){"
                    strJSForMap += "if(data.status === 0) {var marker = new BMap.Marker(data.points[0],{icon:myIconT});map.addOverlay(marker);"
                    strJSForMap += "var label = new BMap.Label(""" & drLoadData.Item(0).ToString & """,{offset:new BMap.Size(20,10)});"
                    strJSForMap += "marker.setLabel(label);"
                    strJSForMap += "}});"
                    If Not IsDBNull(drLoadData.Item(7)) And drLoadData.Item(4) = "室外" Then


                        strJSForMap += " var pointArr = [];pointArr.push(new BMap.Point(" & drLoadData.Item(5).ToString & ", " & drLoadData.Item(6).ToString & "),new BMap.Point(" & drLoadData.Item(5).ToString + 0.0005 * Math.Sin(2 * Math.PI / 360 * drLoadData.Item(7)) & ", " & drLoadData.Item(6).ToString + 0.0005 * Math.Cos(2 * Math.PI / 360 * drLoadData.Item(7)) & "));convertor.translate(pointArr, 1, 5, translateCallback = function (data){"
                        strJSForMap += "if(data.status === 0) {var polyline = new BMap.Polyline([data.points[0],data.points[1]], {strokeColor:""green"", strokeWeight:2, strokeOpacity:0.8}); map.addOverlay(polyline);"
                        strJSForMap += "}});"

                    End If

                Next
            Else


                tbrContent = New TableRow
                tbcContent = New TableCell
                tbcContent.Text = "无该小区TD邻区信息"
                tbcContent.BorderStyle = BorderStyle.Groove
                tbcContent.VerticalAlign = VerticalAlign.Middle
                tbcContent.HorizontalAlign = HorizontalAlign.Left
                tbcContent.Wrap = False
                tbrContent.Cells.Add(tbcContent)
                tableHtmlTable.Rows.Add(tbrContent)

            End If



            strSQLSHandel = "SELECT [ENODEBName] AS [基站名] ,[CELLNAME] AS  小区名 ,[区域] AS [分区],'LTE' as 网络,覆盖类型,LONB,LATB,Azimuth FROM [dbo].[dt_TDL_CDD] INNER JOIN ( 	SELECT dt_GSM_PST.经度,dt_GSM_PST.纬度 	FROM dt_GSM_PST INNER JOIN dbo.dt_GSM_CDD	ON dbo.dt_GSM_CDD.cell_id=dt_GSM_PST.[CELL] 		WHERE dt_GSM_CDD.cell_id='" & strCell & "' ) CurrentL	ON (([LONB]> (currentl.经度-0.005) AND [LONB]< (currentl.经度+0.005)) AND ([LATB]> (currentl.纬度-0.005) AND [LATB]< (currentl.纬度+0.005)))"
            scmdCommand = sqllSSLibrary.GetCommandStr(strSQLSHandel, CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
            dtIndexOfGSMCell = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)

            tableHtmlTable = tbNeighborCell
            thrHead = New TableHeaderRow

            intMaxDataTableCols = dtIndexOfGSMCell.Columns.Count

            If dtIndexOfGSMCell.Rows.Count > 0 Then
                bolIsHave = True

                strJSForMap += "var myIconL = new BMap.Icon(""/IMG/purple-circle.png"", new BMap.Size(23, 25), {offset: new BMap.Size(10, 25),imageOffset: new BMap.Size(0, 0)});"


                For Each drLoadData In dtIndexOfGSMCell.Rows

                    tbrContent = New TableRow
                    For intI = 0 To 3
                        tbcContent = New TableCell
                        tbcContent.Text = drLoadData.Item(intI).ToString
                        tbcContent.BorderStyle = BorderStyle.Groove
                        tbcContent.VerticalAlign = VerticalAlign.Middle
                        tbcContent.HorizontalAlign = HorizontalAlign.Left
                        tbcContent.Wrap = False
                        tbrContent.Cells.Add(tbcContent)
                    Next
                    tableHtmlTable.Rows.Add(tbrContent)


                    strJSForMap += " var pointArr = [];pointArr.push(new BMap.Point(" & drLoadData.Item(5).ToString & ", " & drLoadData.Item(6).ToString & "));convertor.translate(pointArr, 1, 5, translateCallback = function (data){"
                    strJSForMap += "if(data.status === 0) {var marker = new BMap.Marker(data.points[0],{icon:myIconL});map.addOverlay(marker);"
                    strJSForMap += "var label = new BMap.Label(""" & drLoadData.Item(0).ToString & """,{offset:new BMap.Size(20,20)});"
                    strJSForMap += "marker.setLabel(label);"
                    strJSForMap += "}});"
                    If Not IsDBNull(drLoadData.Item(7)) And drLoadData.Item(4) = "室外" Then


                        strJSForMap += " var pointArr = [];pointArr.push(new BMap.Point(" & drLoadData.Item(5).ToString & ", " & drLoadData.Item(6).ToString & "),new BMap.Point(" & drLoadData.Item(5).ToString + 0.0005 * Math.Sin(2 * Math.PI / 360 * drLoadData.Item(7)) & ", " & drLoadData.Item(6).ToString + 0.0005 * Math.Cos(2 * Math.PI / 360 * drLoadData.Item(7)) & "));convertor.translate(pointArr, 1, 5, translateCallback = function (data){"
                        strJSForMap += "if(data.status === 0) {var polyline = new BMap.Polyline([data.points[0],data.points[1]], {strokeColor:""purple"", strokeWeight:2, strokeOpacity:0.8}); map.addOverlay(polyline);"
                        strJSForMap += "}});"

                    End If

                Next
            Else


                tbrContent = New TableRow
                tbcContent = New TableCell
                tbcContent.Text = "无该小区LTE邻区信息"
                tbcContent.BorderStyle = BorderStyle.Groove
                tbcContent.VerticalAlign = VerticalAlign.Middle
                tbcContent.HorizontalAlign = HorizontalAlign.Left
                tbcContent.Wrap = False
                tbrContent.Cells.Add(tbcContent)
                tableHtmlTable.Rows.Add(tbrContent)

            End If


            If bolIsHave Then
                ScriptManager.RegisterClientScriptBlock(upUpdatepanel, Me.GetType, "CellOnMap", strJSForMap, True)

            Else
                plCell.Visible = False
                plFindCell.Visible = True
                btnSearchCell.Enabled = True
                lblError.Visible = True
            End If
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(28, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=28&eu=" & "")
            Else
                erlErrorReport.ReportServerError(28, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=28&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub

    Private Sub btnSearchCell_Click(sender As Object, e As EventArgs) Handles btnSearchCell.Click
        plCell.Visible = True
        plFindCell.Visible = False
        BindTheData(txtCellName.Text)

    End Sub

    Private Sub btnRestart_Click(sender As Object, e As EventArgs) Handles btnRestart.Click
        plCell.Visible = False
        plFindCell.Visible = True
        lblError.Visible = False
    End Sub

    Private Sub timerGetMap_Tick(sender As Object, e As EventArgs) Handles timerGetMap.Tick
        Try

            plCell.Visible = True
            BindTheData(Request.Params("CellName"))
            timerGetMap.Enabled = False
        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(28, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=28&eu=" & "")
            Else
                erlErrorReport.ReportServerError(28, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=28&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub
End Class
