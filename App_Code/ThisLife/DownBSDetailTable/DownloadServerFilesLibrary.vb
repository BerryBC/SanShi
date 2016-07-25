Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports SQLServerLibrary



Public Class DownloadServerFilesLibrary
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer

    Public Function ReturnServerFilePath() As DataTable
        Dim scmdCMD As SqlCommand
        Dim dtBaseSationDetailsMana As DataTable
        Try
            scmdCMD = sqllSSLibrary.GetCommandStr("select * from dt_ServerWorkFilePath", CommonLibrary.GetSQLServerConnect("ConnectionWorkOfOther"))
            dtBaseSationDetailsMana = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
            Return dtBaseSationDetailsMana
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
            dtBaseSationDetailsMana.Dispose()
            dtBaseSationDetailsMana = Nothing
            scmdCMD.Dispose()
            scmdCMD = Nothing
        End Try
    End Function

End Class
