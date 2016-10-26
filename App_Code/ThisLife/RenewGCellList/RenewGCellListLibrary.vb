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
            scmdCMD.CommandTimeout=300
            sqllSSLibrary.ExecNonQuery(scmdCMD)

            scmdCMD = sqllSSLibrary.GetCommandProc("proc_UpdateGuaranteeCellDetails", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            spGuaranteeName = New SqlParameter("@GuaranteeName", SqlDbType.VarChar, 100)
            spUserName = New SqlParameter("@UserName", SqlDbType.VarChar, 40)
            spUserName.Value = strUserName
            spGuaranteeName.Value = strGuaranteeName
            scmdCMD.Parameters.Add(spUserName)
            scmdCMD.Parameters.Add(spGuaranteeName)
            scmdCMD.CommandTimeout=300
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

    Public Shared Function DownloadCells(ByRef dsOrg As DataSet, ByRef strUserName As String, ByRef strGuaranteeName As String) As DataSet
        Dim sqllSSLibrary As LoadSQLServer
        Dim dtData As DataTable

        Dim scmdCMD As SqlCommand

        Try


            sqllSSLibrary = New LoadSQLServer


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





    Public Shared Function DownloadCellsByHotspotName(ByRef strHotSpotName As String) As DataSet
        Dim sqllSSLibrary As LoadSQLServer
        Dim dtData As DataTable
        Dim dsData As DataSet

        Dim scmdCMD As SqlCommand

        Try

            dsData = New DataSet

            sqllSSLibrary = New LoadSQLServer


            scmdCMD = sqllSSLibrary.GetCommandStr("SELECT dt_GCL.* FROM [SanShi_Guarantee].[dbo].[dt_GSM_Guarantee_Cell_List] dt_GCL INNER JOIN ( SELECT * FROM( SELECT tmp_dt_UC.导入用户,dt_GCLM.保障类型,tmp_dt_UC.区域名称,ROW_NUMBER() over(partition by  tmp_dt_UC.区域名称  order by dt_GCLM.更新时间 DESC) as UpdateTime FROM (SELECT [导入用户],[区域名称] ,保障类型 FROM [SanShi_Guarantee].[dbo].[dt_GSM_Guarantee_Cell_List]  WHERE " & strHotSpotName & " GROUP BY [导入用户],[区域名称] ,保障类型) tmp_dt_UC INNER JOIN [SanShi_Guarantee].[dbo].[dt_GuaranteeCellListManagement] dt_GCLM ON tmp_dt_UC.导入用户=dt_GCLM.导入用户 AND dt_GCLM.保障类型 = tmp_dt_UC.保障类型 ) tmp_dt_LastTime WHERE UpdateTime=1 ) tmp_dt_NameTime ON tmp_dt_NameTime.导入用户 = dt_GCL.导入用户 AND tmp_dt_NameTime.区域名称 = dt_GCL.区域名称 AND tmp_dt_NameTime.保障类型 = dt_GCL.保障类型", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            dtData = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
            dtData.TableName = "2G"
            dtData.Columns.Remove("导入用户")
            dsData.Tables.Add(dtData)

            scmdCMD = sqllSSLibrary.GetCommandStr("SELECT dt_GCL.* FROM [SanShi_Guarantee].[dbo].[dt_TD_Guarantee_Cell_List] dt_GCL INNER JOIN ( SELECT * FROM( SELECT tmp_dt_UC.导入用户,dt_GCLM.保障类型,tmp_dt_UC.区域名称,ROW_NUMBER() over(partition by  tmp_dt_UC.区域名称  order by dt_GCLM.更新时间 DESC) as UpdateTime FROM (SELECT [导入用户],[区域名称] ,保障类型 FROM [SanShi_Guarantee].[dbo].[dt_GSM_Guarantee_Cell_List]  WHERE " & strHotSpotName & " GROUP BY [导入用户],[区域名称] ,保障类型) tmp_dt_UC INNER JOIN [SanShi_Guarantee].[dbo].[dt_GuaranteeCellListManagement] dt_GCLM ON tmp_dt_UC.导入用户=dt_GCLM.导入用户 AND dt_GCLM.保障类型 = tmp_dt_UC.保障类型 ) tmp_dt_LastTime WHERE UpdateTime=1 ) tmp_dt_NameTime ON tmp_dt_NameTime.导入用户 = dt_GCL.导入用户 AND tmp_dt_NameTime.区域名称 = dt_GCL.区域名称 AND tmp_dt_NameTime.保障类型 = dt_GCL.保障类型", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            dtData = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
            dtData.TableName = "TD"
            dtData.Columns.Remove("导入用户")
            dsData.Tables.Add(dtData)

            scmdCMD = sqllSSLibrary.GetCommandStr("SELECT dt_GCL.* FROM [SanShi_Guarantee].[dbo].[dt_LTE_Guarantee_Cell_List] dt_GCL INNER JOIN ( SELECT * FROM( SELECT tmp_dt_UC.导入用户,dt_GCLM.保障类型,tmp_dt_UC.区域名称,ROW_NUMBER() over(partition by  tmp_dt_UC.区域名称  order by dt_GCLM.更新时间 DESC) as UpdateTime FROM (SELECT [导入用户],[区域名称] ,保障类型 FROM [SanShi_Guarantee].[dbo].[dt_GSM_Guarantee_Cell_List]  WHERE " & strHotSpotName & " GROUP BY [导入用户],[区域名称] ,保障类型) tmp_dt_UC INNER JOIN [SanShi_Guarantee].[dbo].[dt_GuaranteeCellListManagement] dt_GCLM ON tmp_dt_UC.导入用户=dt_GCLM.导入用户 AND dt_GCLM.保障类型 = tmp_dt_UC.保障类型 ) tmp_dt_LastTime WHERE UpdateTime=1 ) tmp_dt_NameTime ON tmp_dt_NameTime.导入用户 = dt_GCL.导入用户 AND tmp_dt_NameTime.区域名称 = dt_GCL.区域名称 AND tmp_dt_NameTime.保障类型 = dt_GCL.保障类型", CommonLibrary.GetSQLServerConnect("ConnectionGuaranteeCellDB"))
            dtData = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
            dtData.TableName = "LTE"
            dtData.Columns.Remove("导入用户")
            dsData.Tables.Add(dtData)



        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
        Return dsData

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
