Imports System.Data
Imports System.Data.SqlClient
Imports SQLServerLibrary

Partial Class Ingress_WorkPlace_GuessWorkPlace
    Inherits System.Web.UI.Page
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer
    Dim ucUserManage As UserLibrary = New UserLibrary

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim strJSForMap As String
        Dim scmdCommand As SqlCommand
        Dim dtAllOfPoint As DataTable
        Dim strSQLSHandel As String
        Dim drTmpPoint As DataRow



        strSQLSHandel = "SELECT * FROM [Ingress_Log].[dbo].[Data]   WHERE [player_id] like '" & txtSearchWhat.Text & "' and  [time]>='2016-8-15' AND [time]<'2016-8-29' AND DATENAME(HOUR,[time])>=10 AND   DATENAME(HOUR,[time])<15 AND (DATEPART(dw,[time]) BETWEEN 2 AND 6)"
        scmdCommand = sqllSSLibrary.GetCommandStr(strSQLSHandel, CommonLibrary.GetSQLServerConnect("ConnectionIngressLog"))
        dtAllOfPoint = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)


        strJSForMap = "var convertor = new BMap.Convertor(); var points = [];var marker;"

        strJSForMap += "var map = new BMap.Map('CellMap');map.centerAndZoom(new BMap.Point(113.3, 23.15), 12);map.addControl(new BMap.MapTypeControl());map.setCurrentCity('广州');map.enableScrollWheelZoom(true);"
        strJSForMap += "var options = {size: BMAP_POINT_SIZE_SMALL,shape: BMAP_POINT_SHAPE_CIRCLE,color: '#ff0000'};"

        For Each drTmpPoint In dtAllOfPoint.Rows
            If drTmpPoint.Item(5) IsNot DBNull.Value Then

                strJSForMap += " points.push(new BMap.Point(" & drTmpPoint.Item(5) & "," & drTmpPoint.Item(4) & "));"
            End If

        Next


        strJSForMap += "var pointCollection = new BMap.PointCollection(points, options);map.addOverlay(pointCollection); "


        strSQLSHandel = "SELECT * FROM [Ingress_Log].[dbo].[LastCount]   WHERE [player_id] like '" & txtSearchWhat.Text & "' "
        scmdCommand = sqllSSLibrary.GetCommandStr(strSQLSHandel, CommonLibrary.GetSQLServerConnect("ConnectionIngressLog"))
        dtAllOfPoint = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)
        strJSForMap += "var myIconL = new BMap.Icon('/IMG/purple-circle.png', new BMap.Size(23, 25), {offset: new BMap.Size(10, 25),imageOffset: new BMap.Size(0, 0)});"
        For Each drTmpPoint In dtAllOfPoint.Rows
            If drTmpPoint.Item(5) IsNot DBNull.Value Then

                strJSForMap += "marker = new BMap.Marker(new BMap.Point(" & drTmpPoint.Item(7) & ", " & drTmpPoint.Item(6) & "),{icon:myIconL});"
                strJSForMap += "map.addOverlay(marker);"
            End If

        Next


        strSQLSHandel = "SELECT * FROM [Ingress_Log].[dbo].[EveryDayCount]   WHERE [player_id] like '" & txtSearchWhat.Text & "' "
        scmdCommand = sqllSSLibrary.GetCommandStr(strSQLSHandel, CommonLibrary.GetSQLServerConnect("ConnectionIngressLog"))
        dtAllOfPoint = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)

        For Each drTmpPoint In dtAllOfPoint.Rows
            If drTmpPoint.Item(5) IsNot DBNull.Value Then

                strJSForMap += "marker = new BMap.Marker(new BMap.Point(" & drTmpPoint.Item(6) & ", " & drTmpPoint.Item(5) & "));"
                strJSForMap += "map.addOverlay(marker);"
            End If

        Next






        ScriptManager.RegisterClientScriptBlock(upUpdatepanel, Me.GetType, "CellOnMap", strJSForMap, True)

    End Sub

    Private Sub Ingress_WorkPlace_GuessWorkPlace_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ucUserManage.CheckPower(Session, 9, Response)
            End If

        Catch ex As Exception
            If Session("SanShiUserName") Is Nothing Then
                erlErrorReport.ReportServerError(31, "", ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=31&eu=" & "")
            Else
                erlErrorReport.ReportServerError(31, Session("SanShiUserName"), ex.Message, Now)
                Response.Redirect("/ReportErrorLog.aspx?ep=31&eu=" & Session("SanShiUserName"))

            End If

        End Try

    End Sub
End Class
