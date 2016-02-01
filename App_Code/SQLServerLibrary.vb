Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data


Public Class SQLServerLibrary
    Implements IDisposable

    Public Function GetSQLServerConnect(strDateBase As String) As SqlConnection
        Dim scConn As SqlConnection
        Dim strWhichDB As String
        Try


            strWhichDB = ConfigurationManager.AppSettings(strDateBase).ToString()
            scConn = New SqlConnection(strWhichDB)
            Return scConn

        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try


    End Function




    Private Function TranType2OLE(strType As String) As SqlDbType
        If strType = "System.Int32" Then
            Return Data.SqlDbType.Int
        ElseIf strType = "System.String" Then
            Return Data.SqlDbType.VarChar
        ElseIf strType = "System.Double" Then
            Return Data.SqlDbType.Float
        ElseIf strType = "System.DateTime" Then
            Return Data.SqlDbType.DateTime
        End If
        Return OleDb.OleDbType.Error
    End Function



    ''' <summary>
    ''' 执行存储过程语句，返回SqlCommand类对象
    ''' </summary>
    ''' <param name="strProcName">存储过程名</param>
    ''' <param name="strDataBase">存储在Web.config内的连接符Key值</param>
    ''' <returns>返回sqlCommand类对象</returns>
    Public Function GetCommandProc(strProcName As String, strDataBase As String) As SqlCommand
        Dim scConn As SqlConnection
        Dim scmdCMD As SqlCommand
        Try


            scConn = GetSQLServerConnect(strDataBase)
            scmdCMD = New SqlCommand
            scmdCMD.Connection = scConn
            scmdCMD.CommandText = strProcName
            scmdCMD.CommandType = CommandType.StoredProcedure

            Return scmdCMD
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Function


    Public Function GetCommandStr(strSQLString As String, strDataBase As String) As SqlCommand
        Dim scConn As SqlConnection
        Dim scmdCMD As SqlCommand
        Try


            scConn = GetSQLServerConnect(strDataBase)
            scmdCMD = New SqlCommand
            scmdCMD.Connection = scConn
            scmdCMD.CommandText = strSQLString
            scmdCMD.CommandType = CommandType.Text

            Return scmdCMD
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function


    Public Sub ExecNonQuery(ByRef scmdCMD As SqlCommand)
        Try
            If scmdCMD.Connection.State <> ConnectionState.Open Then
                scmdCMD.Connection.Open()

            End If
            scmdCMD.ExecuteNonQuery()



        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            If scmdCMD.Connection.State = ConnectionState.Open Then
                scmdCMD.Connection.Close()

            End If

        End Try

    End Sub
    Public Function ExecNonQueryAndReturn(ByRef scmdCMD As SqlCommand) As Integer
        Dim intQueryCount As Integer
        intQueryCount = 0
        Try
            If scmdCMD.Connection.State <> ConnectionState.Open Then
                scmdCMD.Connection.Open()

            End If
            intQueryCount = scmdCMD.ExecuteNonQuery()



        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            If scmdCMD.Connection.State = ConnectionState.Open Then
                scmdCMD.Connection.Close()

            End If
        End Try
        Return intQueryCount

    End Function




    Public Function GetSQLServerDataTable(ByRef scmdCmd As SqlCommand) As DataTable
        Dim sdaAdapt As SqlDataAdapter
        Dim dtFromSQLServer As DataTable
        dtFromSQLServer = New DataTable

        Try
            If scmdCmd.Connection.State <> ConnectionState.Open Then
                scmdCmd.Connection.Open()
            End If
            sdaAdapt = New SqlDataAdapter(scmdCmd)
            sdaAdapt.Fill(dtFromSQLServer)
            Return dtFromSQLServer

        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            If scmdCmd.Connection.State = ConnectionState.Open Then
                scmdCmd.Connection.Close()
            End If


        End Try
    End Function





    ''' <summary>
    ''' 反馈数据库表中的格式
    ''' </summary>
    ''' <param name="strTable">数据库表名</param>
    ''' <returns>返回只有一行的数据表,用作对应列头</returns>
    ''' <remarks></remarks>
    Public Function ReturnFormat(ByVal strTable As String, strDataBase As String) As DataTable
        Dim scConn As SqlConnection
        Dim adaptData As SqlDataAdapter
        Dim dtFormatData As New DataTable


        scConn = GetSQLServerConnect(strDataBase)
        scConn.Open()

        adaptData = New SqlDataAdapter("SELECT top 1 * from " & strTable & ";", scConn)
        adaptData.Fill(dtFormatData)


        scConn.Close()
        Return dtFormatData

    End Function


    Public Function BlukInsert(ByVal strTable As String, ByVal dtData As DataTable, strDataBase As String) As Integer
        Dim scConn As SqlConnection
        Dim sbcBulkInsert As SqlBulkCopy


        scConn = GetSQLServerConnect(strDataBase)
        scConn.Open()
        sbcBulkInsert = New SqlBulkCopy(scConn)
        sbcBulkInsert.BulkCopyTimeout = 1800
        sbcBulkInsert.NotifyAfter = dtData.Rows.Count

        Try

            sbcBulkInsert.DestinationTableName = strTable
            sbcBulkInsert.WriteToServer(dtData)
            Return 88
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)

        Finally
            scConn.Close()

        End Try
        Return -88


    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' 要检测冗余调用

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)。
            End If

            ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
            ' TODO: 将大型字段设置为 null。
        End If
        disposedValue = True
    End Sub

    ' TODO: 仅当以上 Dispose(disposing As Boolean)拥有用于释放未托管资源的代码时才替代 Finalize()。
    'Protected Overrides Sub Finalize()
    '    ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic 添加此代码以正确实现可释放模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        Dispose(True)
        ' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
