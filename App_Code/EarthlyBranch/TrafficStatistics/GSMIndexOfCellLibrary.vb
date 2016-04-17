Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports SQLServerLibrary

Public Class GSMIndexOfCellLibrary
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer


    Public Function GetGSMIndexMaxDate() As Date
        Dim scmdCMD As SqlCommand
        Dim dtGSMIndexOfCell As DataTable
        Dim dateMaxDate As Date
        Try
            scmdCMD = sqllSSLibrary.GetCommandStr("SELECT max( [Datetime Id(GSM_CELL)])  FROM dt_GSM_Daily_Grib_Traffic", CommonLibrary.GetSQLServerConnect("ConnectionTrafficDB"))
            dtGSMIndexOfCell = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
            If (dtGSMIndexOfCell.Rows(0).Item(0) IsNot Nothing) And (dtGSMIndexOfCell.Rows(0).Item(0) IsNot DBNull.Value) Then

                dateMaxDate = CType(dtGSMIndexOfCell.Rows(0).Item(0), Date)
            Else
                dateMaxDate = New Date(1988, 12, 21)
            End If
            Return dateMaxDate
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
            dtGSMIndexOfCell.Dispose()
            dtGSMIndexOfCell = Nothing
            scmdCMD.Dispose()
            scmdCMD = Nothing
        End Try
    End Function


End Class
