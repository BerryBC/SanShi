Imports Microsoft.VisualBasic
Imports SQLServerLibrary
Imports System.Data.SqlClient
Imports System.Data



Public Class ErrorReportLibrary

    Dim sqllSSLibrary As SQLServerLibrary

    Public Sub New()
        sqllSSLibrary = New SQLServerLibrary
    End Sub

    Public Sub ReportServerError(intErrorPage As Integer, strErrorUser As String, strErrorMsg As String, ByRef dtErrorDateTime As DateTime)
        Dim scmdCMD As SqlCommand
        Dim spErrorPage As SqlParameter
        Dim spErrorUser As SqlParameter
        Dim spErrorMsg As SqlParameter
        Dim spErrorDateTime As SqlParameter
        Try
            scmdCMD = sqllSSLibrary.GetCommandProc("proc_ServerErrorLog", "ConnectionLogDB")

            spErrorPage = New SqlParameter("@ErrorPage", SqlDbType.Int)
            spErrorUser = New SqlParameter("@ErrorUser", SqlDbType.VarChar, 40)
            spErrorMsg = New SqlParameter("@ErrorMessage", SqlDbType.Text)
            spErrorDateTime = New SqlParameter("@ErrorTime", SqlDbType.DateTime)


            spErrorPage.Value = intErrorPage
            spErrorUser.Value = strErrorUser
            spErrorMsg.Value = strErrorMsg
            spErrorDateTime.Value = dtErrorDateTime


            scmdCMD.Parameters.Add(spErrorPage)
            scmdCMD.Parameters.Add(spErrorUser)
            scmdCMD.Parameters.Add(spErrorMsg)
            scmdCMD.Parameters.Add(spErrorDateTime)

            sqllSSLibrary.ExecNonQuery(scmdCMD)



        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
            scmdCMD.Dispose()
            scmdCMD = Nothing
        End Try


    End Sub



    Public Sub ReportUserError(intErrorPage As Integer, strErrorUser As String, strErrorMsg As String, ByRef dtErrorDateTime As DateTime)
        Dim scmdCMD As SqlCommand
        Dim spErrorPage As SqlParameter
        Dim spErrorUser As SqlParameter
        Dim spErrorMsg As SqlParameter
        Dim spErrorDateTime As SqlParameter
        Try
            scmdCMD = sqllSSLibrary.GetCommandProc("proc_UserReportErrorLog", "ConnectionLogDB")

            spErrorPage = New SqlParameter("@ErrorPage", SqlDbType.Int)
            spErrorUser = New SqlParameter("@ErrorUser", SqlDbType.VarChar, 40)
            spErrorMsg = New SqlParameter("@ErrorMessage", SqlDbType.Text)
            spErrorDateTime = New SqlParameter("@ErrorTime", SqlDbType.DateTime)



            spErrorPage.Value = intErrorPage
            spErrorUser.Value = strErrorUser
            spErrorMsg.Value = strErrorMsg
            spErrorDateTime.Value = dtErrorDateTime


            scmdCMD.Parameters.Add(spErrorPage)
            scmdCMD.Parameters.Add(spErrorUser)
            scmdCMD.Parameters.Add(spErrorMsg)
            scmdCMD.Parameters.Add(spErrorDateTime)

            sqllSSLibrary.ExecNonQuery(scmdCMD)



        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
            scmdCMD.Dispose()
            scmdCMD = Nothing
        End Try


    End Sub








End Class
