Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports SQLServerLibrary
Imports System.Data

Public Class GSMCellInfoLibrary

    Public Shared Function GetIDByEnglishName(ByVal strEnglishName As String) As String
        Dim scmdCMD As SqlCommand
        Dim spName As SqlParameter
        Dim sqllSSLibrary As LoadSQLServer
        Dim dtGetID As DataTable
        Try

            sqllSSLibrary = New LoadSQLServer
            scmdCMD = sqllSSLibrary.GetCommandProc("proc_FindGSMIDByEnglishName", CommonLibrary.GetSQLServerConnect("ConnectionBaseStationDetailsDB"))


            spName = New SqlParameter("@EnglishName", SqlDbType.VarChar, 20)

            spName.Value = strEnglishName

            scmdCMD.Parameters.Add(spName)

            dtGetID = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)

            If dtGetID.Rows.Count > 0 Then
                Return dtGetID.Rows(0).Item("ID")

            Else
                Return ""
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
            scmdCMD.Dispose()
            scmdCMD = Nothing

        End Try

    End Function
End Class
