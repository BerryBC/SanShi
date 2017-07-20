<%@ WebHandler Language="VB" Class="NNBack" %>

Imports System
Imports System.Web
Imports CommonLibrary
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports SQLServerLibrary

Public Class NNBack : Implements IHttpHandler
    Dim clCommon As CommonLibrary = New CommonLibrary
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer
    Dim erlErrorReport As ErrorReportLibrary = New ErrorReportLibrary

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim scmdCMD As SqlCommand



        Dim hccToken As HttpCookieCollection
        Dim hcToken As HttpCookie

        Dim strJsonLoad As String
        Dim intWhatReason As String

        Try


            hccToken = context.Request.Cookies


            hcToken = hccToken("TokenForNN")
            If (hcToken IsNot Nothing) Then
                If (hcToken("Token") = clCommon.StringTranscodingToMD5(Date.Now.ToShortDateString)) Then
                    If context.Request.HttpMethod = "GET" Then
                        intWhatReason = context.Request.QueryString(0)

                        If intWhatReason = 0 Then
                            Dim strTrueData As String
                            Dim dtNNConfig As DataTable
                            Dim dtNNData As DataTable
                            Dim intWhatTemplate As Integer
                            Dim strGoShow As String
                            intWhatTemplate = context.Request.QueryString(1)


                            'strJsonLoad = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("/AfterLife/NeuralNetworks/Config/DataJSON.json"))


                            'strJsonLoad = "{""GotError"":0," & Right(strJsonLoad, Len(strJsonLoad) - 1)



                            strTrueData = ""

                            Try
                                scmdCMD = sqllSSLibrary.GetCommandStr("SELECT  dt_NetworkConfig.NumberOfNetwork,TitleForNetwork,DateRangeBegin,DateRangeEnd,AllDataNumber,NetWork,InputCompressPara,OutputCompressPara,TrainDataNumber  FROM dbo.dt_NetworkConfig INNER JOIN dbo.dt_NetworkInside ON dt_NetworkInside.NumberOfNetwork = dt_NetworkConfig.NumberOfNetwork WHERE dt_NetworkConfig.NumberOfNetwork=" & intWhatTemplate.ToString(), CommonLibrary.GetSQLServerConnect("ConnectionNeuralNetworks"))
                                dtNNConfig = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
                            Catch ex As Exception
                                Throw New Exception(ex.Message, ex)
                                dtNNConfig.Dispose()
                                dtNNConfig = Nothing
                                scmdCMD.Dispose()
                                scmdCMD = Nothing
                            End Try
                            strGoShow = "'NumberOfNetwork':" & dtNNConfig.Rows(0).Item("NumberOfNetwork").ToString() & ","
                            strGoShow = strGoShow & "'TitleForNetwork':'" & dtNNConfig.Rows(0).Item("TitleForNetwork").ToString() & "',"
                            strGoShow = strGoShow & "'DateRange': {" & dtNNConfig.Rows(0).Item("DateRangeBegin").ToString() & "," & dtNNConfig.Rows(0).Item("DateRangeEnd").ToString() & "},"
                            If dtNNConfig.Rows(0).Item("NetWork") IsNot DBNull.Value Then
                                strGoShow = strGoShow & "'NetWork':" & dtNNConfig.Rows(0).Item("NetWork").ToString() & ","
                            Else
                                strGoShow = strGoShow & "'NetWork':{},"
                            End If
                            strGoShow = strGoShow & dtNNConfig.Rows(0).Item("InputCompressPara").ToString() & ","
                            strGoShow = strGoShow & dtNNConfig.Rows(0).Item("OutputCompressPara").ToString() & ","
                            strGoShow = strGoShow & File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("/AfterLife/NeuralNetworks/Config/Festival.txt")) & ","

                            Try
                                scmdCMD = sqllSSLibrary.GetCommandStr("SELECT [DataDate],[TrueData] FROM [dt_TrainData] WHERE  [TrainDataNumber]=" & dtNNConfig.Rows(0).Item("TrainDataNumber").ToString() & " ORDER BY DataDate ASC", CommonLibrary.GetSQLServerConnect("ConnectionNeuralNetworks"))
                                dtNNData = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
                            Catch ex As Exception
                                Throw New Exception(ex.Message, ex)
                                dtNNData.Dispose()
                                dtNNData = Nothing
                                scmdCMD.Dispose()
                                scmdCMD = Nothing
                            End Try
                            For Each drtmpEverRow In dtNNData.Rows
                                strTrueData = strTrueData & "['" & drtmpEverRow.Item("DataDate").ToString() & "'," & drtmpEverRow.Item("TrueData").ToString() & "],"

                            Next
                            strTrueData = Left(strTrueData, Len(strTrueData) - 1)
                            strGoShow = strGoShow & "'TrueData':[" & strTrueData & "]}"
                            strJsonLoad = "{'GotError':0," & strGoShow
                            context.Response.ContentType = "text/plain"
                            context.Response.Write(strJsonLoad)

                        ElseIf intWhatReason = 1 Then
                            Dim dtNNConfig As DataTable
                            Dim dtNNData As DataTable
                            Dim dtNNNet As DataTable
                            Dim intMaxRow As Integer
                            Dim intNowRow As Integer
                            Dim strGoTrain As String
                            Dim drtmpEverRow As DataRow
                            Dim strTrueData As String


                            Try
                                strTrueData = ""
                                scmdCMD = sqllSSLibrary.GetCommandStr("select * from dt_NetworkConfig", CommonLibrary.GetSQLServerConnect("ConnectionNeuralNetworks"))
                                dtNNConfig = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)

                            Catch ex As Exception
                                Throw New Exception(ex.Message, ex)
                                dtNNConfig.Dispose()
                                dtNNConfig = Nothing
                                scmdCMD.Dispose()
                                scmdCMD = Nothing
                            End Try
                            Randomize()
                            intMaxRow = dtNNConfig.Rows.Count
                            intNowRow = Math.Round(Rnd() * intMaxRow * 0.9999 - 0.5, 0)
                            strGoTrain = "'NumberOfNetwork':" & dtNNConfig.Rows(intNowRow).Item("NumberOfNetwork").ToString() & ","
                            strGoTrain = strGoTrain & "'TitleForNetwork':'" & dtNNConfig.Rows(intNowRow).Item("TitleForNetwork").ToString() & "',"
                            strGoTrain = strGoTrain & "'DateRange': {" & dtNNConfig.Rows(intNowRow).Item("DateRangeBegin").ToString() & "," & dtNNConfig.Rows(intNowRow).Item("DateRangeEnd").ToString() & "},"
                            Try
                                scmdCMD = sqllSSLibrary.GetCommandStr("SELECT [NetWork],[InputCompressPara],[OutputCompressPara] FROM dbo.dt_NetworkConfig INNER JOIN dbo.dt_NetworkInside ON dt_NetworkInside.NumberOfNetwork = dt_NetworkConfig.NumberOfNetwork WHERE dt_NetworkConfig.NumberOfNetwork=" & dtNNConfig.Rows(intNowRow).Item("NumberOfNetwork").ToString(), CommonLibrary.GetSQLServerConnect("ConnectionNeuralNetworks"))
                                dtNNNet = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
                            Catch ex As Exception
                                Throw New Exception(ex.Message, ex)
                                dtNNNet.Dispose()
                                dtNNNet = Nothing
                                scmdCMD.Dispose()
                                scmdCMD = Nothing
                            End Try

                            If dtNNNet.Rows(0).Item("NetWork") IsNot DBNull.Value Then
                                strGoTrain = strGoTrain & "'NetWork':" & dtNNNet.Rows(0).Item("NetWork").ToString() & ","
                            Else
                                strGoTrain = strGoTrain & "'NetWork':{},"
                            End If

                            strGoTrain = strGoTrain & dtNNNet.Rows(0).Item("InputCompressPara").ToString() & ","
                            strGoTrain = strGoTrain & dtNNNet.Rows(0).Item("OutputCompressPara").ToString() & ","
                            strGoTrain = strGoTrain & File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("/AfterLife/NeuralNetworks/Config/Festival.txt")) & ","
                            Try

                                scmdCMD = sqllSSLibrary.GetCommandStr("SELECT [DataDate],[TrueData] FROM [dt_TrainData] WHERE  TrainDataNumber=" & dtNNConfig.Rows(intNowRow).Item("TrainDataNumber").ToString() & " ORDER BY DataDate ASC", CommonLibrary.GetSQLServerConnect("ConnectionNeuralNetworks"))
                                dtNNData = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
                            Catch ex As Exception
                                Throw New Exception(ex.Message, ex)
                                dtNNData.Dispose()
                                dtNNData = Nothing
                                scmdCMD.Dispose()
                                scmdCMD = Nothing
                            End Try
                            For Each drtmpEverRow In dtNNData.Rows
                                strTrueData = strTrueData & "['" & drtmpEverRow.Item("DataDate").ToString() & "'," & drtmpEverRow.Item("TrueData").ToString() & "],"

                            Next
                            strTrueData = Left(strTrueData, Len(strTrueData) - 1)
                            strGoTrain = strGoTrain & "'TrueData':[" & strTrueData & "]}"


                            strJsonLoad = "{'GotError':0," & strGoTrain
                            context.Response.ContentType = "text/plain"
                            context.Response.Write(strJsonLoad)
                        ElseIf intWhatReason = 3 Then
                            '---------问一下有哪些预测
                            Dim dtNNConfig As DataTable
                            Dim drtmpEverRow As DataRow
                            Dim strTrueData As String
                            strTrueData = ""
                            Try
                                scmdCMD = sqllSSLibrary.GetCommandStr("SELECT dt_NetworkConfig.[NumberOfNetwork],[TitleForNetwork] FROM  dbo.dt_NetworkConfig INNER JOIN dbo.dt_NetworkInside ON dt_NetworkInside.NumberOfNetwork = dt_NetworkConfig.NumberOfNetwork WHERE NetWork IS NOT NULL group by dt_NetworkConfig.[NumberOfNetwork],[TitleForNetwork] ", CommonLibrary.GetSQLServerConnect("ConnectionNeuralNetworks"))
                                dtNNConfig = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)

                            Catch ex As Exception
                                Throw New Exception(ex.Message, ex)
                                dtNNConfig.Dispose()
                                dtNNConfig = Nothing
                                scmdCMD.Dispose()
                                scmdCMD = Nothing
                            End Try
                            For Each drtmpEverRow In dtNNConfig.Rows
                                strTrueData = strTrueData & "[" & drtmpEverRow.Item("NumberOfNetwork").ToString() & ",'" & drtmpEverRow.Item("TitleForNetwork").ToString() & "'],"

                            Next
                            strJsonLoad = "{'GotError':0,'SeriesOfNetwork':[" & strTrueData & "]}"

                            context.Response.ContentType = "text/plain"
                            context.Response.Write(strJsonLoad)
                        ElseIf intWhatReason = 4 Then
                            '---------------验算第一步
                            Dim dtNNConfig As DataTable
                            Dim drtmpEverRow As DataRow
                            Dim strTrueData As String
                            strTrueData = ""
                            Try
                                scmdCMD = sqllSSLibrary.GetCommandStr("SELECT [tmpID] ,[NumberOfNetwork] FROM [SanShi_NeuralNetworks].[dbo].[dt_WaitToDetection] ORDER BY NumberOfNetwork asc", CommonLibrary.GetSQLServerConnect("ConnectionNeuralNetworks"))
                                dtNNConfig = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)

                            Catch ex As Exception
                                Throw New Exception(ex.Message, ex)
                                dtNNConfig.Dispose()
                                dtNNConfig = Nothing
                                scmdCMD.Dispose()
                                scmdCMD = Nothing
                            End Try
                            For Each drtmpEverRow In dtNNConfig.Rows
                                strTrueData = strTrueData & "[" & drtmpEverRow.Item("tmpID").ToString() & ",'" & drtmpEverRow.Item("NumberOfNetwork").ToString() & "'],"

                            Next
                            strJsonLoad = "{'GotError':0,'PendingDetectionNetwork':[" & strTrueData & "]}"

                            context.Response.ContentType = "text/plain"
                            context.Response.Write(strJsonLoad)
                        ElseIf intWhatReason = 5 Then
                            '---------------验算第二步
                            Dim strTrueData As String
                            Dim dtNNConfig As DataTable
                            Dim dtNNData As DataTable
                            Dim intWhatTemplate As Integer
                            Dim strGoShow As String
                            Dim intWhatTmpTemplate As Integer
                            intWhatTemplate = context.Request.QueryString(1)
                            intWhatTmpTemplate = context.Request.QueryString(2)

                            strTrueData = ""

                            Try
                                scmdCMD = sqllSSLibrary.GetCommandStr("SELECT  dt_NetworkConfig.NumberOfNetwork,TitleForNetwork,DateRangeBegin,DateRangeEnd,AllDataNumber,dt_WaitToDetection.NetWork,InputCompressPara,OutputCompressPara,TrainDataNumber  FROM dbo.dt_NetworkConfig INNER JOIN dbo.dt_NetworkInside ON dt_NetworkInside.NumberOfNetwork = dt_NetworkConfig.NumberOfNetwork INNER JOIN dbo.dt_WaitToDetection ON dt_WaitToDetection.NumberOfNetwork = dt_NetworkConfig.NumberOfNetwork WHERE dt_NetworkConfig.NumberOfNetwork=" & intWhatTemplate.ToString() & "  AND tmpID= " & intWhatTmpTemplate.ToString(), CommonLibrary.GetSQLServerConnect("ConnectionNeuralNetworks"))
                                dtNNConfig = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
                            Catch ex As Exception
                                Throw New Exception(ex.Message, ex)
                                dtNNConfig.Dispose()
                                dtNNConfig = Nothing
                                scmdCMD.Dispose()
                                scmdCMD = Nothing
                            End Try
                            strGoShow = "'NumberOfNetwork':" & dtNNConfig.Rows(0).Item("NumberOfNetwork").ToString() & ","
                            strGoShow = strGoShow & "'TitleForNetwork':'" & dtNNConfig.Rows(0).Item("TitleForNetwork").ToString() & "',"
                            strGoShow = strGoShow & "'DateRange': {" & dtNNConfig.Rows(0).Item("DateRangeBegin").ToString() & "," & dtNNConfig.Rows(0).Item("DateRangeEnd").ToString() & "},"
                            If dtNNConfig.Rows(0).Item("NetWork") IsNot DBNull.Value Then
                                strGoShow = strGoShow & "'NetWork':" & dtNNConfig.Rows(0).Item("NetWork").ToString() & ","
                            Else
                                strGoShow = strGoShow & "'NetWork':{},"
                            End If
                            strGoShow = strGoShow & dtNNConfig.Rows(0).Item("InputCompressPara").ToString() & ","
                            strGoShow = strGoShow & dtNNConfig.Rows(0).Item("OutputCompressPara").ToString() & ","
                            strGoShow = strGoShow & File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("/AfterLife/NeuralNetworks/Config/Festival.txt")) & ","

                            Try
                                scmdCMD = sqllSSLibrary.GetCommandStr("SELECT [DataDate],[TrueData] FROM [dt_TrainData] WHERE  [TrainDataNumber]=" & dtNNConfig.Rows(0).Item("TrainDataNumber").ToString() & " ORDER BY DataDate ASC", CommonLibrary.GetSQLServerConnect("ConnectionNeuralNetworks"))
                                dtNNData = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
                            Catch ex As Exception
                                Throw New Exception(ex.Message, ex)
                                dtNNData.Dispose()
                                dtNNData = Nothing
                                scmdCMD.Dispose()
                                scmdCMD = Nothing
                            End Try
                            For Each drtmpEverRow In dtNNData.Rows
                                strTrueData = strTrueData & "['" & drtmpEverRow.Item("DataDate").ToString() & "'," & drtmpEverRow.Item("TrueData").ToString() & "],"
                            Next
                            strTrueData = Left(strTrueData, Len(strTrueData) - 1)
                            strGoShow = strGoShow & "'TrueData':[" & strTrueData & "]}"
                            strJsonLoad = "{'GotError':0," & strGoShow
                            context.Response.ContentType = "text/plain"
                            context.Response.Write(strJsonLoad)

                        ElseIf intWhatReason = 6 Then
                            '---------------验算后修改原有网络
                            Dim strTrueData As String
                            Dim dtNNConfig As DataTable
                            Dim intWhatTemplate As Integer
                            Dim strGoShow As String
                            Dim intWhatTmpTemplate As Integer
                            Dim intChange As Integer
                            intWhatTemplate = context.Request.QueryString(1)
                            intWhatTmpTemplate = context.Request.QueryString(2)
                            intChange = context.Request.QueryString(3)
                            strTrueData = ""

                            If intChange = 1 Then
                                Try
                                    scmdCMD = sqllSSLibrary.GetCommandStr("UPDATE [SanShi_NeuralNetworks].[dbo].[dt_NetworkInside] SET [dt_NetworkInside].NetWork=[dt_WaitToDetection].NetWork FROM [SanShi_NeuralNetworks].[dbo].[dt_WaitToDetection] WHERE [dt_WaitToDetection].NumberOfNetwork=" & intWhatTemplate.ToString() & " AND [dt_NetworkInside].NumberOfNetwork=" & intWhatTemplate.ToString() & " AND tmpID= " & intWhatTmpTemplate.ToString(), CommonLibrary.GetSQLServerConnect("ConnectionNeuralNetworks"))
                                    dtNNConfig = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
                                    strTrueData = "更新原有网络 OK."
                                Catch ex As Exception
                                    Throw New Exception(ex.Message, ex)
                                    dtNNConfig.Dispose()
                                    dtNNConfig = Nothing
                                    scmdCMD.Dispose()
                                    scmdCMD = Nothing
                                    strTrueData = "更新原有网络 Not OK."
                                End Try
                            End If
                            Try
                                scmdCMD = sqllSSLibrary.GetCommandStr("DELETE FROM [SanShi_NeuralNetworks].[dbo].[dt_WaitToDetection]", CommonLibrary.GetSQLServerConnect("ConnectionNeuralNetworks"))
                                dtNNConfig = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
                                strTrueData = strTrueData + " 清除待检测网络 OK."
                            Catch ex As Exception
                                Throw New Exception(ex.Message, ex)
                                dtNNConfig.Dispose()
                                dtNNConfig = Nothing
                                scmdCMD.Dispose()
                                scmdCMD = Nothing
                                strTrueData = " 清除待检测网络 Not OK."
                            End Try

                            strGoShow = "'FeedBack':'" & strTrueData & "'}"
                            strJsonLoad = "{'GotError':0," & strGoShow
                            context.Response.ContentType = "text/plain"
                            context.Response.Write(strJsonLoad)


                        End If
                    ElseIf context.Request.HttpMethod = "POST" Then
                        intWhatReason = context.Request("WhatReason")

                        If intWhatReason = 2 Then
                            Dim intWhatNumberOfNetwork As Integer
                            Dim strThisNetwork As String
                            If context.Request("NumberOfNetwork") IsNot Nothing And context.Request("TheNet") IsNot Nothing Then

                                intWhatNumberOfNetwork = context.Request("NumberOfNetwork")
                                strThisNetwork = context.Request("TheNet")
                                Try

                                    scmdCMD = sqllSSLibrary.GetCommandStr("INSERT dbo.dt_WaitToDetection ( NumberOfNetwork, NetWork ) VALUES  ( " & intWhatNumberOfNetwork & ", '" & strThisNetwork & "' )", CommonLibrary.GetSQLServerConnect("ConnectionNeuralNetworks"))
                                    sqllSSLibrary.ExecNonQuery(scmdCMD)

                                Catch ex As Exception
                                    Throw New Exception(ex.Message, ex)
                                    scmdCMD.Dispose()
                                    scmdCMD = Nothing
                                End Try

                                context.Response.Write("{'GotError':0}")

                            Else
                                context.Response.Write("{'GotError':3,'ErrorDsp':'输入错误'}")

                            End If


                        End If
                    End If
                Else
                    context.Response.Write("{'GotError':1,'ErrorDsp':'验证错误'}")
                End If

            Else

                context.Response.Write("{'GotError':2,'ErrorDsp':'没有验证'}")

            End If
        Catch ex As Exception
            erlErrorReport.ReportServerError(39, "API", ex.Message, Now)


        End Try

    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class