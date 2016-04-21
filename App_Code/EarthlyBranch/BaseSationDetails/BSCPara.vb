Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports AccessLibrary
Imports SQLServerLibrary
Imports System.IO
Imports SQLServerLibrary.LoadSQLServer

Public Class BSCPara
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer


    Public Function HandelDailyAccessBSCPara(strSourceAccessDataBase As String, strSQLServerTableName As String, dateWhatTimeIsPara As Date, strConfigFile As String) As Integer
        Dim pasTmpParaAndSQL As ParameterAndSQL
        Dim tblTableBscList As TableBSCList
        Dim dtFormat As DataTable
        Dim dtData As DataTable
        Dim dttmpData As DataTable
        Dim odbcOleDBCommand As OleDbCommand
        Dim aceAccess As LoadAccess
        Dim drtmpBscListRow As DataRow
        Dim listBscList As List(Of String)
        Dim strtmpBSC As String
        Dim listBSCPara As List(Of List(Of List(Of Object)))
        Dim listtmpBSCPara As List(Of List(Of Object))
        Dim listtmpBSCParaEveryBSC As New List(Of Object)
        Dim scmdCommand As SqlCommand
        Dim dtOrgData As DataTable
        Dim listOriPara As List(Of BscOriginalPara)
        Dim boptmpBSCPara As BscOriginalPara
        Dim drtmpBSCCompareOf As DataRow
        Dim intI As Integer
        Dim sabConfigSQLSandBSCList As SQLSandBSCList
        Dim strJsonLoad As String
        Try

            listOriPara = New List(Of BscOriginalPara)
            listBscList = New List(Of String)
            listBSCPara = New List(Of List(Of List(Of Object)))

            strJsonLoad = File.ReadAllText(strConfigFile)
            sabConfigSQLSandBSCList = SimpleJson.SimpleJson.DeserializeObject(Of SQLSandBSCList)(strJsonLoad)


            scmdCommand = sqllSSLibrary.GetCommandStr("select * from " & strSQLServerTableName & ";", CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
            dtOrgData = sqllSSLibrary.GetSQLServerDataTable(scmdCommand)

            scmdCommand = sqllSSLibrary.GetCommandStr("delete from " & strSQLServerTableName & ";", CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
            sqllSSLibrary.ExecNonQuery(scmdCommand)



            dtFormat = sqllSSLibrary.ReturnFormat(strSQLServerTableName, CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
            dtFormat.Rows.Clear()
            dtData = dtFormat


            For Each drtmpBscListRow In dtOrgData.Rows
                boptmpBSCPara = New BscOriginalPara
                boptmpBSCPara.strBSCName = drtmpBscListRow(0)
                boptmpBSCPara.drParaRow = drtmpBscListRow
                listOriPara.Add(boptmpBSCPara)
            Next

            aceAccess = New LoadAccess(strSourceAccessDataBase, True)




            For Each tblTableBscList In sabConfigSQLSandBSCList.listtblBscList
                odbcOleDBCommand = New OleDbCommand("SELECT " & tblTableBscList.strBSCName & " FROM " & tblTableBscList.strTableName & " GROUP BY " & tblTableBscList.strBSCName & ";")
                dttmpData = aceAccess.GetAccessDataTable(odbcOleDBCommand)
                For Each drtmpBscListRow In dttmpData.Rows
                    listBscList.Add（drtmpBscListRow(0)）
                Next
            Next

            For Each pasTmpParaAndSQL In sabConfigSQLSandBSCList.listpasParaAndSQLS
                Try

                    odbcOleDBCommand = New OleDbCommand(pasTmpParaAndSQL.strSQLStatements)
                    dttmpData = aceAccess.GetAccessDataTable(odbcOleDBCommand)
                    listtmpBSCPara = New List(Of List(Of Object))
                    For Each drtmpBscListRow In dttmpData.Rows
                        listtmpBSCParaEveryBSC = New List(Of Object)
                        listtmpBSCParaEveryBSC.Add(drtmpBscListRow(0))
                        listtmpBSCParaEveryBSC.Add(drtmpBscListRow(1))
                        listtmpBSCPara.Add（listtmpBSCParaEveryBSC）
                    Next
                    listBSCPara.Add(listtmpBSCPara)
                Catch ex As Exception

                End Try
            Next

            aceAccess.Close()
            Dim tmplistBscList = (From tmpStr In listBscList Select tmpStr).Distinct


            For Each strtmpBSC In tmplistBscList
                drtmpBscListRow = dtFormat.NewRow
                drtmpBscListRow(0) = strtmpBSC
                For Each listtmpBSCPara In listBSCPara
                    Dim strWhichPara As String
                    strWhichPara = sabConfigSQLSandBSCList.listpasParaAndSQLS(listBSCPara.IndexOf(listtmpBSCPara)).strColName
                    For Each listtmpBSCParaEveryBSC In listtmpBSCPara
                        If ((listtmpBSCParaEveryBSC(0).ToString = strtmpBSC) And (listtmpBSCParaEveryBSC(1) IsNot Nothing)) Then
                            drtmpBscListRow(strWhichPara) = listtmpBSCParaEveryBSC(1)
                            listtmpBSCPara.Remove(listtmpBSCParaEveryBSC)

                            Exit For
                        End If
                    Next

                Next

                dtData.Rows.Add(drtmpBscListRow)
            Next

            sqllSSLibrary.BlukInsert(strSQLServerTableName, dtData, CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))

            For Each drtmpBscListRow In dtData.Rows
                For Each boptmpBSCPara In listOriPara
                    If boptmpBSCPara.strBSCName = drtmpBscListRow(0).ToString Then
                        drtmpBSCCompareOf = boptmpBSCPara.drParaRow
                        Exit For
                    End If
                Next
                If drtmpBSCCompareOf IsNot Nothing Then
                    For intI = 1 To dtData.Columns.Count - 1

                        If ((drtmpBSCCompareOf(intI) IsNot Nothing) And (drtmpBscListRow(intI) IsNot Nothing) And (drtmpBSCCompareOf(intI) IsNot DBNull.Value) And (drtmpBscListRow(intI) IsNot DBNull.Value)) Then

                            If drtmpBSCCompareOf(intI) <> drtmpBscListRow(intI) Then
                                '-----------记录修改问题
                                ChangeBSCParaLog(strSQLServerTableName, drtmpBscListRow(0).ToString, dtFormat.Columns(intI).ColumnName, drtmpBSCCompareOf(intI) & "  -->  " & drtmpBscListRow(intI), dateWhatTimeIsPara)

                            End If
                        ElseIf ((drtmpBscListRow(intI) IsNot Nothing) And (drtmpBscListRow(intI) IsNot DBNull.Value) And ((drtmpBSCCompareOf(intI) Is Nothing) Or (drtmpBSCCompareOf(intI) Is DBNull.Value))) Then
                            ChangeBSCParaLog(strSQLServerTableName, drtmpBscListRow(0).ToString, dtFormat.Columns(intI).ColumnName, "前期缺数，现网值为 -->  " & drtmpBscListRow(intI), dateWhatTimeIsPara)

                        ElseIf ((drtmpBSCCompareOf(intI) IsNot Nothing) And (drtmpBSCCompareOf(intI) IsNot DBNull.Value) And ((drtmpBscListRow(intI) Is Nothing) Or (drtmpBscListRow(intI) Is DBNull.Value))) Then
                            ChangeBSCParaLog(strSQLServerTableName, drtmpBscListRow(0).ToString, dtFormat.Columns(intI).ColumnName, "现网缺数", dateWhatTimeIsPara)
                        End If
                    Next
                    drtmpBSCCompareOf = Nothing
                Else
                    ChangeBSCParaLog(strSQLServerTableName, drtmpBscListRow(0).ToString, "", "该网元前期缺数", dateWhatTimeIsPara)
                End If
            Next



            pasTmpParaAndSQL = Nothing
            tblTableBscList = Nothing
            dtFormat.Dispose()
            dtFormat = Nothing
            dtData.Dispose()
            dtData = Nothing
            If dttmpData IsNot Nothing Then dttmpData.Dispose()
            dttmpData = Nothing
            If odbcOleDBCommand IsNot Nothing Then odbcOleDBCommand.Dispose()
            odbcOleDBCommand = Nothing
            aceAccess.Dispose()
            aceAccess = Nothing
            drtmpBscListRow = Nothing
            If listBscList.Count > 0 Then listBscList.Clear()
            listBscList = Nothing
            If listBSCPara.Count > 0 Then listBSCPara.Clear()
            listBSCPara = Nothing
            If listtmpBSCPara.Count > 0 Then listtmpBSCPara.Clear()
            listtmpBSCPara = Nothing
            If listtmpBSCParaEveryBSC.Count > 0 Then listtmpBSCParaEveryBSC.Clear()
            listtmpBSCParaEveryBSC = Nothing
            scmdCommand.Dispose()
            scmdCommand = Nothing
            dtOrgData.Dispose()
            dtOrgData = Nothing
            If listOriPara.Count > 0 Then listOriPara.Clear()
            listOriPara = Nothing
            boptmpBSCPara = Nothing
            drtmpBSCCompareOf = Nothing
            sabConfigSQLSandBSCList.Dispose()
            sabConfigSQLSandBSCList = Nothing

        Catch ex As Exception
            Throw New Exception(ex.Message, ex)

        End Try
        Return 88
    End Function


    Public Sub ChangeBSCParaLog(strTableName As String, strNetElement As String, strChangePara As String, strChangeValue As String, dateWhatTimeChange As Date)
        Dim scmdCMD As SqlCommand
        Dim spTableName As SqlParameter
        Dim spNetElement As SqlParameter
        Dim spChangePara As SqlParameter
        Dim spChangeValue As SqlParameter
        Dim spWhatTimeChange As SqlParameter
        Try
            scmdCMD = sqllSSLibrary.GetCommandProc("proc_ChangeBSCPara", CommonLibrary.GetSQLServerConnect("ConnectionLogDB"))
            spTableName = New SqlParameter("@ChangeTable", SqlDbType.VarChar, 100)
            spNetElement = New SqlParameter("@ChangeNE", SqlDbType.VarChar, 100)
            spChangePara = New SqlParameter("@ChangePara", SqlDbType.VarChar, 100)
            spChangeValue = New SqlParameter("@ChangeValue", SqlDbType.Text)
            spWhatTimeChange = New SqlParameter("@ChangeDate", SqlDbType.DateTime)
            spTableName.Value = strTableName
            spNetElement.Value = strNetElement
            spChangePara.Value = strChangePara
            spChangeValue.Value = strChangeValue
            spWhatTimeChange.Value = dateWhatTimeChange
            scmdCMD.Parameters.Add(spTableName)
            scmdCMD.Parameters.Add(spNetElement)
            scmdCMD.Parameters.Add(spChangePara)
            scmdCMD.Parameters.Add(spChangeValue)
            scmdCMD.Parameters.Add(spWhatTimeChange)
            sqllSSLibrary.ExecNonQuery(scmdCMD)
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
            scmdCMD.Dispose()
            scmdCMD = Nothing
        End Try
    End Sub


End Class
