<%@ WebHandler Language="VB" Class="LoadCell" %>

Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports SQLServerLibrary
Imports System.IO
Imports System.Data
Imports CommonLibrary
Public Class LoadCell : Implements IHttpHandler
    Dim clCommon As CommonLibrary = New CommonLibrary
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim scmdCMD As SqlCommand



        Dim hccToken As HttpCookieCollection
        Dim hcToken As HttpCookie

        Dim strJsonLoad As String
        Dim doubleMaxLng As Double
        Dim doubleMinLng As Double
        Dim doubleMaxLat As Double
        Dim doubleMinLat As Double


        hccToken = context.Request.Cookies


        hcToken = hccToken("TokenForNN")
        If (hcToken IsNot Nothing) Then
            If (hcToken("Token") = clCommon.StringTranscodingToMD5(Date.Now.ToShortDateString)) Then
                If context.Request.HttpMethod = "GET" Then
                    Dim dtNNConfig As DataTable
                    Dim drtmpEverRow As DataRow
                    Dim strTrueDataGSM As String
                    Dim strTrueDataTDS As String
                    Dim strTrueDataLTE As String

                    strTrueDataGSM = ""
                    strTrueDataTDS = ""
                    strTrueDataLTE = ""

                    doubleMaxLng = Val(context.Request.QueryString(0))
                    doubleMinLng = Val(context.Request.QueryString(1))
                    doubleMaxLat = Val(context.Request.QueryString(2))
                    doubleMinLat = Val(context.Request.QueryString(3))


                    '-------有问题
                    Try
                        scmdCMD = sqllSSLibrary.GetCommandStr("SELECT cell_id,基站名,基房经度,基房纬度 FROM [SanShi_BaseSationDetails].[dbo].[dt_GSM_CDD] WHERE [基房经度]>=(" & doubleMinLng & "-0.003) AND  [基房经度]<=(" & doubleMaxLng & "+0.003) AND [基房纬度]>=(" & doubleMinLat & "-0.003) AND  [基房纬度]<=(" & doubleMaxLat & "+0.003) ", CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
                        dtNNConfig = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)

                    Catch ex As Exception
                        Throw New Exception(ex.Message, ex)
                        dtNNConfig.Dispose()
                        dtNNConfig = Nothing
                        scmdCMD.Dispose()
                        scmdCMD = Nothing
                    End Try

                    For Each drtmpEverRow In dtNNConfig.Rows
                        strTrueDataGSM = strTrueDataGSM & "['" & drtmpEverRow.Item("cell_id").ToString() & "','" & drtmpEverRow.Item("基站名").ToString() & "','" & drtmpEverRow.Item("基房经度").ToString() & "','" & drtmpEverRow.Item("基房纬度").ToString() & "'],"
                    Next
                    If dtNNConfig.Rows.Count <= 0 Then
                        strTrueDataGSM = "[],"
                    End If




                    Try
                        scmdCMD = sqllSSLibrary.GetCommandStr("SELECT CellName,CellID,RNCID,Longitude,Latitude FROM [SanShi_BaseSationDetails].[dbo].[dt_TDS_CDD] WHERE [Longitude]>=(" & doubleMinLng & "-0.003) AND  [Longitude]<=(" & doubleMaxLng & "+0.003) AND [Latitude]>=(" & doubleMinLat & "-0.003) AND  [Latitude]<=(" & doubleMaxLat & "+0.003)", CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
                        dtNNConfig = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)

                    Catch ex As Exception
                        Throw New Exception(ex.Message, ex)
                        dtNNConfig.Dispose()
                        dtNNConfig = Nothing
                        scmdCMD.Dispose()
                        scmdCMD = Nothing
                    End Try

                    For Each drtmpEverRow In dtNNConfig.Rows
                        strTrueDataTDS = strTrueDataTDS & "['" & drtmpEverRow.Item("CellName").ToString() & "','" & drtmpEverRow.Item("CellID").ToString() & "','" & drtmpEverRow.Item("RNCID").ToString() & "','" & drtmpEverRow.Item("Longitude").ToString() & "','" & drtmpEverRow.Item("Latitude").ToString() & "'],"
                    Next
                    If dtNNConfig.Rows.Count <= 0 Then
                        strTrueDataTDS = "[],"
                    End If



                    Try
                        scmdCMD = sqllSSLibrary.GetCommandStr("SELECT CELLNAME,CELLID,SectorID,ENODEBID,LONB,LATB FROM [SanShi_BaseSationDetails].[dbo].[dt_TDL_CDD] WHERE LONB>=(" & doubleMinLng & "-0.003) AND  LONB<=(" & doubleMaxLng & "+0.003) AND LATB>=(" & doubleMinLat & "-0.003) AND  LATB<=(" & doubleMaxLat & "+0.003) ", CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
                        dtNNConfig = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)

                    Catch ex As Exception
                        Throw New Exception(ex.Message, ex)
                        dtNNConfig.Dispose()
                        dtNNConfig = Nothing
                        scmdCMD.Dispose()
                        scmdCMD = Nothing
                    End Try

                    For Each drtmpEverRow In dtNNConfig.Rows
                        strTrueDataLTE = strTrueDataLTE & "['" & drtmpEverRow.Item("CELLNAME").ToString() & "','" & drtmpEverRow.Item("CELLID").ToString() & "','" & drtmpEverRow.Item("SectorID").ToString() & "','" & drtmpEverRow.Item("ENODEBID").ToString() & "','" & drtmpEverRow.Item("LONB").ToString() & "','" & drtmpEverRow.Item("LATB").ToString() & "'],"
                    Next
                    If dtNNConfig.Rows.Count <= 0 Then
                        strTrueDataLTE = "[],"
                    End If


                    strTrueDataGSM = Left(strTrueDataGSM, Len(strTrueDataGSM) - 1)
                    strTrueDataTDS = Left(strTrueDataTDS, Len(strTrueDataTDS) - 1)
                    strTrueDataLTE = Left(strTrueDataLTE, Len(strTrueDataLTE) - 1)
                    strJsonLoad = "{'GotError':0,'GSMCell':[" & strTrueDataGSM & "],'TDSCell':[" & strTrueDataTDS & "],'LTECell':[" & strTrueDataLTE & "]}"
                    context.Response.ContentType = "text/plain"
                    context.Response.Write(strJsonLoad)
                End If
            End If
        End If








    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class