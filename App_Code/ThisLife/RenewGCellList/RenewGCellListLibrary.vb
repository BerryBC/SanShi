Imports Microsoft.VisualBasic
Imports System.Data
Imports ExcelLibrary
Imports SQLServerLibrary
Imports System.Data.SqlClient



Public Class RenewGCellListLibrary

    Public Shared Function GetCellDataSet(ByRef strFullFileName As String) As DataSet
        Dim exlExl As LoadExcel
        Dim dsFromExcel As DataSet
        Dim dtFromExcel As DataTable
        Try
            dsFromExcel = New DataSet
            exlExl = New LoadExcel(strFullFileName)
            exlExl.GetInformation()
            If exlExl.strSheets.Exists(Function(strSheetName) strSheetName = "2G") Then
                dtFromExcel = New DataTable
                dtFromExcel = exlExl.GetData("2G")
                dtFromExcel.TableName = "2G"
                dsFromExcel.Tables.Add(dtFromExcel)
            End If
            If exlExl.strSheets.Exists(Function(strSheetName) strSheetName = "TD") Then
                dtFromExcel = New DataTable
                dtFromExcel = exlExl.GetData("TD")
                dtFromExcel.TableName = "TD"
                dsFromExcel.Tables.Add(dtFromExcel)
            End If
            If exlExl.strSheets.Exists(Function(strSheetName) strSheetName = "LTE") Then
                dtFromExcel = New DataTable
                dtFromExcel = exlExl.GetData("LTE")
                dtFromExcel.TableName = "LTE"
                dsFromExcel.Tables.Add(dtFromExcel)
            End If
            If exlExl.strSheets.Exists(Function(strSheetName) strSheetName = "WLAN") Then
                dtFromExcel = New DataTable
                dtFromExcel = exlExl.GetData("WLAN")
                dtFromExcel.TableName = "WLAN"
                dsFromExcel.Tables.Add(dtFromExcel)
            End If







            Return dsFromExcel
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function


    Public Shared Function SaveDataSetToExcel(ByRef dsDataToSave As DataSet, ByRef strFullFileName As String) As Boolean
        Dim exlExl As LoadExcel
        Try
            exlExl = New LoadExcel(strFullFileName)
            exlExl.GetInformation()
            If exlExl.strSheets.Exists(Function(strSheetName) strSheetName = "2G") Then
                exlExl.SaveToExistsFile(dsDataToSave.Tables("2G"), "2G")
            End If
            If exlExl.strSheets.Exists(Function(strSheetName) strSheetName = "TD") Then
                exlExl.SaveToExistsFile(dsDataToSave.Tables("TD"), "TD")
            End If
            If exlExl.strSheets.Exists(Function(strSheetName) strSheetName = "LTE") Then
                exlExl.SaveToExistsFile(dsDataToSave.Tables("LTE"), "LTE")
            End If
            If exlExl.strSheets.Exists(Function(strSheetName) strSheetName = "WLAN") Then
                If dsDataToSave.Tables("WLAN") IsNot Nothing Then

                    exlExl.SaveToExistsFile(dsDataToSave.Tables("WLAN"), "WLAN")
                End If
            End If
            exlExl.Dispose()
            exlExl = Nothing
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function




    Public Shared Function LoadTheGuaranteeName(ByRef strFullFileName As String) As String
        Dim exlExl As LoadExcel

        Try
            exlExl = New LoadExcel(strFullFileName)
            exlExl.GetInformation()
            If exlExl.strSheets.Exists(Function(strSheetName) strSheetName = "2G") Then
                If exlExl.ReturnOneCell("2G", 2, 2) <> "" Then

                    Return exlExl.ReturnOneCell("2G", 2, 2)
                End If
            End If
            If exlExl.strSheets.Exists(Function(strSheetName) strSheetName = "TD") Then
                If exlExl.ReturnOneCell("TD", 2, 2) <> "" Then

                    Return exlExl.ReturnOneCell("TD", 2, 2)
                End If

            End If
            If exlExl.strSheets.Exists(Function(strSheetName) strSheetName = "LTE") Then
                If exlExl.ReturnOneCell("LTE", 2, 2) <> "" Then

                    Return exlExl.ReturnOneCell("LTE", 2, 2)
                End If
            End If


                Return ""
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function


    Public Shared Function UpdateCellsRows(ByRef dsOrg As DataSet, ByRef strUserName As String, ByRef strGuaranteeName As String) As DataSet
        Dim sqllSSLibrary As LoadSQLServer
        Dim spGuaranteeName As SqlParameter
        Dim spUserName As SqlParameter
        Dim dtData As DataTable

        Dim scmdCMD As SqlCommand

        Try


            sqllSSLibrary = New LoadSQLServer
            scmdCMD = sqllSSLibrary.GetCommandProc("proc_UpdateTheBaseSationTable", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            sqllSSLibrary.ExecNonQuery(scmdCMD)

            scmdCMD = sqllSSLibrary.GetCommandProc("proc_UpdateGuaranteeIDInfo", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            spGuaranteeName = New SqlParameter("@GuaranteeName", SqlDbType.VarChar, 100)
            spUserName = New SqlParameter("@UserName", SqlDbType.VarChar, 40)
            spUserName.Value = strUserName
            spGuaranteeName.Value = strGuaranteeName
            scmdCMD.Parameters.Add(spUserName)
            scmdCMD.Parameters.Add(spGuaranteeName)
            sqllSSLibrary.ExecNonQuery(scmdCMD)

            scmdCMD = sqllSSLibrary.GetCommandProc("proc_UpdateGuaranteeCellDetails", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            spGuaranteeName = New SqlParameter("@GuaranteeName", SqlDbType.VarChar, 100)
            spUserName = New SqlParameter("@UserName", SqlDbType.VarChar, 40)
            spUserName.Value = strUserName
            spGuaranteeName.Value = strGuaranteeName
            scmdCMD.Parameters.Add(spUserName)
            scmdCMD.Parameters.Add(spGuaranteeName)
            sqllSSLibrary.ExecNonQuery(scmdCMD)


            dsOrg.Tables.Remove("2G")
            dsOrg.Tables.Remove("TD")
            dsOrg.Tables.Remove("LTE")

            scmdCMD = sqllSSLibrary.GetCommandStr("select * from dt_GSM_Guarantee_Cell_List  where  [导入用户] ='" & strUserName & "' and 保障类型='" & strGuaranteeName & "'", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            dtData = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
            dtData.TableName = "2G"
            dtData.Columns.Remove("导入用户")
            dsOrg.Tables.Add(dtData)

            scmdCMD = sqllSSLibrary.GetCommandStr("select * from dt_TD_Guarantee_Cell_List  where  [导入用户] ='" & strUserName & "' and 保障类型='" & strGuaranteeName & "'", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            dtData = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
            dtData.TableName = "TD"
            dtData.Columns.Remove("导入用户")
            dsOrg.Tables.Add(dtData)

            scmdCMD = sqllSSLibrary.GetCommandStr("select * from dt_LTE_Guarantee_Cell_List  where  [导入用户] ='" & strUserName & "' and 保障类型='" & strGuaranteeName & "'", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            dtData = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
            dtData.TableName = "LTE"
            dtData.Columns.Remove("导入用户")
            dsOrg.Tables.Add(dtData)



        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
        Return dsOrg

    End Function

    Public Shared Sub UpdateCellsRowsByChineseName(ByRef strUserName As String, ByRef strGuaranteeName As String)
        Dim sqllSSLibrary As LoadSQLServer
        Dim spGuaranteeName As SqlParameter
        Dim spUserName As SqlParameter

        Dim scmdCMD As SqlCommand

        Try


            sqllSSLibrary = New LoadSQLServer

            scmdCMD = sqllSSLibrary.GetCommandProc("proc_UpdateGuaranteeCellByChineseName", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            spGuaranteeName = New SqlParameter("@GuaranteeName", SqlDbType.VarChar, 100)
            spUserName = New SqlParameter("@UserName", SqlDbType.VarChar, 40)
            spUserName.Value = strUserName
            spGuaranteeName.Value = strGuaranteeName
            scmdCMD.Parameters.Add(spUserName)
            scmdCMD.Parameters.Add(spGuaranteeName)
            sqllSSLibrary.ExecNonQuery(scmdCMD)



        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Sub




    '----------------------------------------------------------------------------------------------
    '----------------------------------------老方法------------------------------------------------
    '----------------------------------------------------------------------------------------------
    'Public Shared Function UpdateCellsRows(ByRef dsOrg As DataSet, bolIsGotID As Int16) As DataSet
    '    Dim dtID As DataTable
    '    Dim drTmp As DataRow
    '    Dim scmdCMD As SqlCommand
    '    Dim sqllSSLibrary As LoadSQLServer
    '    Dim dictID As Dictionary(Of String, String)
    '    Dim longTmpI As Long
    '    Dim listTmp As String()
    '    Try

    '        sqllSSLibrary = New LoadSQLServer

    '        If bolIsGotID = 1 Then
    '            '------------GSM ID对应----------------
    '            dictID = New Dictionary(Of String, String)
    '            scmdCMD = sqllSSLibrary.GetCommandStr("select * from dt_GSM_ID", CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
    '            dtID = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
    '            For Each drTmp In dtID.Rows
    '                If Not dictID.ContainsKey(drTmp.Item(0).ToString) Then dictID.Add(drTmp.Item(0).ToString, drTmp.Item(3).ToString)

    '            Next
    '            For longTmpI = 0 To (dsOrg.Tables.Item("2G").Rows.Count - 1)
    '                If Not dictID.ContainsKey(dsOrg.Tables.Item("2G").Rows(longTmpI).Item(6).ToString) Then
    '                    dsOrg.Tables.Item("2G").Rows(longTmpI).Item(5) = ""
    '                Else
    '                    dsOrg.Tables.Item("2G").Rows(longTmpI).Item(5) = dictID(dsOrg.Tables.Item("2G").Rows(longTmpI).Item(6))
    '                End If
    '            Next
    '            dictID.Clear()

    '            '------------TD ID对应----------------
    '            dictID = New Dictionary(Of String, String)
    '            scmdCMD = sqllSSLibrary.GetCommandStr("select * from dt_TDS_ID", CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
    '            dtID = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
    '            For Each drTmp In dtID.Rows
    '                If Not dictID.ContainsKey(drTmp.Item(0).ToString) Then dictID.Add(drTmp.Item(0).ToString, drTmp.Item(5).ToString)
    '            Next
    '            For longTmpI = 0 To (dsOrg.Tables.Item("TD").Rows.Count - 1)
    '                If Not dictID.ContainsKey(dsOrg.Tables.Item("TD").Rows(longTmpI).Item(8) & "-" & dsOrg.Tables.Item("TD").Rows(longTmpI).Item(6)) Then
    '                    dsOrg.Tables.Item("TD").Rows(longTmpI).Item(11) = ""
    '                Else
    '                    dsOrg.Tables.Item("TD").Rows(longTmpI).Item(11) = dictID(dsOrg.Tables.Item("TD").Rows(longTmpI).Item(8) & "-" & dsOrg.Tables.Item("TD").Rows(longTmpI).Item(6))
    '                End If
    '            Next
    '            dictID.Clear()

    '            '------------LTE ID对应----------------
    '            dictID = New Dictionary(Of String, String)
    '            scmdCMD = sqllSSLibrary.GetCommandStr("select * from dt_TDL_ID", CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
    '            dtID = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
    '            For Each drTmp In dtID.Rows
    '                If Not dictID.ContainsKey(drTmp.Item(0).ToString) Then dictID.Add(drTmp.Item(0).ToString, drTmp.Item(5).ToString)
    '            Next
    '            For longTmpI = 0 To (dsOrg.Tables.Item("LTE").Rows.Count - 1)
    '                If dictID.ContainsKey(dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(8) & "-" & dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(7)) Then
    '                    dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(20) = dictID(dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(8) & "-" & dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(7))
    '                ElseIf dictID.ContainsKey(dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(8) & "-" & dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(9)) Then
    '                    dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(20) = dictID(dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(8) & "-" & dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(9))
    '                Else
    '                    dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(20) = ""
    '                End If
    '            Next
    '            dictID.Clear()
    '        End If
    '        '--------------------GSM更新----------------------------
    '        '   --------CDD-----------
    '        dictID = New Dictionary(Of String, String)
    '        scmdCMD = sqllSSLibrary.GetCommandStr("select * from dt_GSM_CDD", CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
    '        dtID = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
    '        For Each drTmp In dtID.Rows
    '            If Not dictID.ContainsKey(drTmp.Item(3).ToString) Then dictID.Add(drTmp.Item(3).ToString, drTmp.Item(0).ToString & "/" & drTmp.Item(1).ToString & "/" & drTmp.Item(2).ToString & "/" & drTmp.Item(4).ToString & "/" & drTmp.Item(10).ToString & "/" & drTmp.Item(13).ToString & "/" & drTmp.Item(14).ToString & "/" & drTmp.Item(17).ToString)

    '        Next
    '        For longTmpI = 0 To (dsOrg.Tables.Item("2G").Rows.Count - 1)
    '            If dictID.ContainsKey(dsOrg.Tables.Item("2G").Rows(longTmpI).Item(5).ToString) Then
    '                listTmp = Regex.Split(dictID(dsOrg.Tables.Item("2G").Rows(longTmpI).Item(5)), "/")
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(4) = ForDataCTypeDynamic(listTmp(7), dsOrg.Tables.Item("2G").Rows(longTmpI).Item(4))
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(6) = ForDataCTypeDynamic(listTmp(2), dsOrg.Tables.Item("2G").Rows(longTmpI).Item(6))
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(7) = ForDataCTypeDynamic(listTmp(1), dsOrg.Tables.Item("2G").Rows(longTmpI).Item(7))
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(8) = ForDataCTypeDynamic(listTmp(0), dsOrg.Tables.Item("2G").Rows(longTmpI).Item(8))
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(11) = ForDataCTypeDynamic(listTmp(5), dsOrg.Tables.Item("2G").Rows(longTmpI).Item(11))
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(12) = ForDataCTypeDynamic(listTmp(6), dsOrg.Tables.Item("2G").Rows(longTmpI).Item(12))
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(13) = ForDataCTypeDynamic(listTmp(4), dsOrg.Tables.Item("2G").Rows(longTmpI).Item(13))
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(14) = ForDataCTypeDynamic(listTmp(3), dsOrg.Tables.Item("2G").Rows(longTmpI).Item(14))
    '            Else
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(25) += "CDD没有该小区,"
    '            End If
    '        Next
    '        dictID.Clear()

    '        '   --------轩驰表-----------
    '        dictID = New Dictionary(Of String, String)
    '        scmdCMD = sqllSSLibrary.GetCommandStr("select * from dt_GSM_XC", CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
    '        dtID = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
    '        For Each drTmp In dtID.Rows
    '            If Not dictID.ContainsKey(drTmp.Item(5).ToString) Then dictID.Add(drTmp.Item(5).ToString, drTmp.Item(9).ToString & "/" & drTmp.Item(10).ToString & "/" & drTmp.Item(11).ToString & "/" & drTmp.Item(12).ToString & "/" & drTmp.Item(19).ToString)

    '        Next
    '        For longTmpI = 0 To (dsOrg.Tables.Item("2G").Rows.Count - 1)
    '            If dictID.ContainsKey(dsOrg.Tables.Item("2G").Rows(longTmpI).Item(6).ToString) Then

    '                listTmp = Regex.Split(dictID(dsOrg.Tables.Item("2G").Rows(longTmpI).Item(6)), "/")
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(16) = ForDataCTypeDynamic(listTmp(4), dsOrg.Tables.Item("2G").Rows(longTmpI).Item(16))
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(21) = ForDataCTypeDynamic(listTmp(0), dsOrg.Tables.Item("2G").Rows(longTmpI).Item(21))
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(22) = ForDataCTypeDynamic(listTmp(1), dsOrg.Tables.Item("2G").Rows(longTmpI).Item(22))
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(23) = ForDataCTypeDynamic(listTmp(2), dsOrg.Tables.Item("2G").Rows(longTmpI).Item(23))
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(24) = ForDataCTypeDynamic(listTmp(3), dsOrg.Tables.Item("2G").Rows(longTmpI).Item(24))
    '            Else
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(16) = ""
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(21) = ""
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(22) = ""
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(23) = ""
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(24) = ""
    '                dsOrg.Tables.Item("2G").Rows(longTmpI).Item(25) += "轩驰表没有该小区,"
    '            End If

    '        Next
    '        dictID.Clear()

    '        '--------------------TD更新----------------------------
    '        '   --------TD基站信息表-----------
    '        dictID = New Dictionary(Of String, String)
    '        scmdCMD = sqllSSLibrary.GetCommandStr("select * from dt_TDS_CDD", CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
    '        dtID = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
    '        For Each drTmp In dtID.Rows
    '            If Not dictID.ContainsKey(drTmp.Item(1).ToString) Then dictID.Add(drTmp.Item(1).ToString, drTmp.Item(4).ToString & "/" & drTmp.Item(5).ToString & "/" & drTmp.Item(6).ToString & "/" & drTmp.Item(7).ToString & "/" & drTmp.Item(8).ToString & "/" & drTmp.Item(11).ToString & "/" & drTmp.Item(13).ToString & "/" & drTmp.Item(69).ToString)

    '        Next
    '        For longTmpI = 0 To (dsOrg.Tables.Item("TD").Rows.Count - 1)
    '            If dictID.ContainsKey(dsOrg.Tables.Item("TD").Rows(longTmpI).Item(11).ToString) Then
    '                listTmp = Regex.Split(dictID(dsOrg.Tables.Item("TD").Rows(longTmpI).Item(11)), "/")
    '                dsOrg.Tables.Item("TD").Rows(longTmpI).Item(4) = ForDataCTypeDynamic(listTmp(1), dsOrg.Tables.Item("TD").Rows(longTmpI).Item(4))
    '                dsOrg.Tables.Item("TD").Rows(longTmpI).Item(5) = ForDataCTypeDynamic(listTmp(2), dsOrg.Tables.Item("TD").Rows(longTmpI).Item(5))
    '                dsOrg.Tables.Item("TD").Rows(longTmpI).Item(6) = ForDataCTypeDynamic(listTmp(3), dsOrg.Tables.Item("TD").Rows(longTmpI).Item(6))
    '                dsOrg.Tables.Item("TD").Rows(longTmpI).Item(7) = ForDataCTypeDynamic(listTmp(4), dsOrg.Tables.Item("TD").Rows(longTmpI).Item(7))
    '                dsOrg.Tables.Item("TD").Rows(longTmpI).Item(8) = ForDataCTypeDynamic(listTmp(0), dsOrg.Tables.Item("TD").Rows(longTmpI).Item(8))
    '                dsOrg.Tables.Item("TD").Rows(longTmpI).Item(9) = ForDataCTypeDynamic(listTmp(5), dsOrg.Tables.Item("TD").Rows(longTmpI).Item(9))
    '                dsOrg.Tables.Item("TD").Rows(longTmpI).Item(10) = ForDataCTypeDynamic(listTmp(6), dsOrg.Tables.Item("TD").Rows(longTmpI).Item(10))
    '                dsOrg.Tables.Item("TD").Rows(longTmpI).Item(12) = ForDataCTypeDynamic(listTmp(7), dsOrg.Tables.Item("TD").Rows(longTmpI).Item(12))
    '            Else
    '                dsOrg.Tables.Item("TD").Rows(longTmpI).Item(22) += "TD基站信息表没有该小区,"
    '            End If
    '        Next
    '        dictID.Clear()
    '        '   --------TD轩驰表-----------
    '        dictID = New Dictionary(Of String, String)
    '        scmdCMD = sqllSSLibrary.GetCommandStr("select * from dt_TDS_XC", CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
    '        dtID = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
    '        For Each drTmp In dtID.Rows
    '            If Not dictID.ContainsKey(drTmp.Item(4).ToString) Then dictID.Add(drTmp.Item(4).ToString, drTmp.Item(11).ToString)

    '        Next
    '        For longTmpI = 0 To (dsOrg.Tables.Item("TD").Rows.Count - 1)
    '            If dictID.ContainsKey(dsOrg.Tables.Item("TD").Rows(longTmpI).Item(5).ToString) Then
    '                dsOrg.Tables.Item("TD").Rows(longTmpI).Item(14) = dictID(dsOrg.Tables.Item("TD").Rows(longTmpI).Item(5))
    '            Else
    '                dsOrg.Tables.Item("TD").Rows(longTmpI).Item(14) = ""
    '                dsOrg.Tables.Item("TD").Rows(longTmpI).Item(22) += "轩驰表没有该小区,"
    '            End If
    '        Next
    '        dictID.Clear()


    '        '--------------------LTE更新----------------------------
    '        '   --------CDD-----------
    '        dictID = New Dictionary(Of String, String)
    '        scmdCMD = sqllSSLibrary.GetCommandStr("select * from dt_TDL_CDD", CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
    '        dtID = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
    '        For Each drTmp In dtID.Rows
    '            If Not dictID.ContainsKey(drTmp.Item(2).ToString) Then dictID.Add(drTmp.Item(2).ToString, drTmp.Item(4).ToString & "/" & drTmp.Item(9).ToString & "/" & drTmp.Item(10).ToString & "/" & drTmp.Item(11).ToString & "/" & drTmp.Item(14).ToString & "/" & drTmp.Item(20).ToString & "/" & drTmp.Item(21).ToString & "/" & drTmp.Item(23).ToString & "/" & drTmp.Item(33).ToString & "/" & drTmp.Item(58).ToString & "/" & drTmp.Item(60).ToString)

    '        Next
    '        For longTmpI = 0 To (dsOrg.Tables.Item("LTE").Rows.Count - 1)
    '            If dictID.ContainsKey(dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(20).ToString) Then
    '                listTmp = Regex.Split(dictID(dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(20)), "/")
    '                dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(4) = ForDataCTypeDynamic(listTmp(3), dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(4))
    '                dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(5) = ForDataCTypeDynamic(listTmp(7), dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(5))
    '                dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(7) = ForDataCTypeDynamic(listTmp(6), dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(7))
    '                dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(8) = ForDataCTypeDynamic(listTmp(1), dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(8))
    '                dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(9) = ForDataCTypeDynamic(listTmp(5), dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(9))
    '                dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(10) = ForDataCTypeDynamic(listTmp(2), dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(10))
    '                dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(11) = ForDataCTypeDynamic(listTmp(0), dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(11))
    '                dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(12) = ForDataCTypeDynamic(listTmp(8), dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(12))
    '                dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(13) = ForDataCTypeDynamic(listTmp(4), dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(13))
    '                dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(18) = ForDataCTypeDynamic(listTmp(9), dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(18))
    '                dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(19) = ForDataCTypeDynamic(listTmp(10), dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(19))
    '                dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(6) = ""
    '            Else
    '                dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(27) += "CDD没有该小区,"
    '            End If
    '        Next
    '        dictID.Clear()
    '        '   --------LTE轩驰表-----------
    '        dictID = New Dictionary(Of String, String)
    '        scmdCMD = sqllSSLibrary.GetCommandStr("select * from dt_TDL_CDD", CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))
    '        dtID = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
    '        For Each drTmp In dtID.Rows
    '            If Not dictID.ContainsKey(drTmp.Item(8).ToString) Then dictID.Add(drTmp.Item(8).ToString, drTmp.Item(6).ToString)

    '        Next
    '        For longTmpI = 0 To (dsOrg.Tables.Item("LTE").Rows.Count - 1)
    '            If dictID.ContainsKey(dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(4).ToString) Then
    '                dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(16) = dictID(dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(4))
    '            Else
    '                dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(16) = ""
    '                dsOrg.Tables.Item("LTE").Rows(longTmpI).Item(27) += "CDD没有该小区,"
    '            End If
    '        Next
    '        dictID.Clear()



    '        Return dsOrg
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message, ex)
    '    End Try

    'End Function
    '----------------------------------------------------------------------------------------------
    '----------------------------------------老方法------------------------------------------------
    '----------------------------------------------------------------------------------------------
    Public Shared Function ImportGCellToSQLServer(ByRef dsDataSetOfCell As DataSet, ByRef strUserName As String, ByRef strGuaranteeName As String) As Boolean
        Dim sqllSSLibrary As LoadSQLServer
        Dim dtFormat As DataTable
        Dim dtData As DataTable
        Dim scmdCMD As SqlCommand


        Try
            sqllSSLibrary = New LoadSQLServer()


            dtFormat = sqllSSLibrary.ReturnFormat("dt_GSM_Guarantee_Cell_List", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            dtData = CommonLibrary.ReturnNewNormalDT(dsDataSetOfCell.Tables("2G"), dtFormat)
            scmdCMD = sqllSSLibrary.GetCommandStr("delete from dt_GSM_Guarantee_Cell_List where  [导入用户] ='" & strUserName & "' and 保障类型='" & strGuaranteeName & "'", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            sqllSSLibrary.ExecNonQuery(scmdCMD)
            sqllSSLibrary.BlukInsert("dt_GSM_Guarantee_Cell_List", dtData, CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            scmdCMD = sqllSSLibrary.GetCommandStr("update dt_GSM_Guarantee_Cell_List set [导入用户] ='" & strUserName & "' where  [导入用户] is null", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            sqllSSLibrary.ExecNonQuery(scmdCMD)


            dtFormat = sqllSSLibrary.ReturnFormat("dt_TD_Guarantee_Cell_List", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            dtData = CommonLibrary.ReturnNewNormalDT(dsDataSetOfCell.Tables("TD"), dtFormat)
            scmdCMD = sqllSSLibrary.GetCommandStr("delete from dt_TD_Guarantee_Cell_List where  [导入用户] ='" & strUserName & "' and 保障类型='" & strGuaranteeName & "'", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            sqllSSLibrary.ExecNonQuery(scmdCMD)
            sqllSSLibrary.BlukInsert("dt_TD_Guarantee_Cell_List", dtData, CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            scmdCMD = sqllSSLibrary.GetCommandStr("update dt_TD_Guarantee_Cell_List set [导入用户] ='" & strUserName & "' where  [导入用户] is null", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            sqllSSLibrary.ExecNonQuery(scmdCMD)

            dtFormat = sqllSSLibrary.ReturnFormat("dt_LTE_Guarantee_Cell_List", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            dtData = CommonLibrary.ReturnNewNormalDT(dsDataSetOfCell.Tables("LTE"), dtFormat)
            scmdCMD = sqllSSLibrary.GetCommandStr("delete from dt_LTE_Guarantee_Cell_List where  [导入用户] ='" & strUserName & "' and 保障类型='" & strGuaranteeName & "'", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            sqllSSLibrary.ExecNonQuery(scmdCMD)
            sqllSSLibrary.BlukInsert("dt_LTE_Guarantee_Cell_List", dtData, CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            scmdCMD = sqllSSLibrary.GetCommandStr("update dt_LTE_Guarantee_Cell_List set [导入用户] ='" & strUserName & "' where  [导入用户] is null", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            sqllSSLibrary.ExecNonQuery(scmdCMD)



            Return True


        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
        Return False
    End Function





    Public Shared Function ForDataCTypeDynamic(ByRef objGetIn As Object, ByRef objOutPut As Object) As Object
        Dim objNewOne As Object
        objNewOne = objGetIn
        If objNewOne.ToString = "" And objOutPut Is DBNull.Value Then
            objNewOne = DBNull.Value
        End If
        If objOutPut IsNot DBNull.Value Then
            objNewOne = CTypeDynamic(objGetIn, objOutPut.GetType)
        End If

        Return objNewOne
    End Function


End Class
