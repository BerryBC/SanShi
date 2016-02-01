Imports System.Data.OleDb

Public Class LoadAccess
    Implements IDisposable
    Private strFileName As String
    Private cnConnection As OleDb.OleDbConnection
    Private bolIsAlwaysConnect As Boolean

    ''' <summary>
    ''' 创建新对象并赋予数据库路径名
    ''' </summary>
    ''' <param name="FullFileName">数据库路径及文件名</param>
    Public Sub New(ByVal FullFileName As String)
        strFileName = FullFileName
        bolIsAlwaysConnect = False
    End Sub


    ''' <summary>
    ''' 创建新对象并赋予数据库路径名
    ''' </summary>
    ''' <param name="FullFileName">数据库路径及文件名</param>
    Public Sub New(ByVal FullFileName As String, ByVal bolIsConnectNow As Boolean)
        strFileName = FullFileName
        bolIsAlwaysConnect = False
        bolIsAlwaysConnect = bolIsConnectNow
        Try
            If bolIsConnectNow Then
                cnConnection = New OleDb.OleDbConnection(ConnectString(strFileName))
                cnConnection.Open()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Sub


    ''' <summary>
    ''' 连接数据库语句
    ''' </summary>
    ''' <param name="FullFileName">连接数据库文件名</param>
    ''' <returns>连接语句</returns>
    Private Function ConnectString(ByVal FullFileName As String) As String
        Dim strConnection As String
        strConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & FullFileName & ";Persist Security Info=False;"
        Return strConnection
    End Function


    ''' <summary>
    ''' 把.Net数据类型编程OLE数据类型
    ''' </summary>
    ''' <param name="strTable"></param>
    ''' <returns>返回OLE的数据类型</returns>
    Private Function TranType2OLE(ByVal strTable As String) As OleDbType
        If strTable = "System.Int32" Then
            Return OleDb.OleDbType.Integer
        ElseIf strTable = "System.String" Then
            Return OleDb.OleDbType.VarChar
        ElseIf strTable = "System.Double" Then
            Return OleDb.OleDbType.Double
        ElseIf strTable = "System.DateTime" Then
            Return OleDb.OleDbType.Date
        ElseIf strTable = "System.Single" Then
            Return OleDb.OleDbType.Single
        End If
        Return OleDb.OleDbType.Error
    End Function

    ''' <summary>
    ''' 把数据插入数据库中
    ''' </summary>
    ''' <param name="strTable">表名</param>
    ''' <param name="dtData">数据库</param>
    Public Sub Insert(ByVal strTable As String, ByVal dtData As DataTable)
        'Dim cnConnection As OleDb.OleDbConnection
        Dim intHowManyCol As Integer = 0
        Dim strTmpValues As String = ""
        Dim adaptData As New OleDbDataAdapter
        Dim i As Integer
        Try

            '连接数据库
            If ((Not bolIsAlwaysConnect) And (cnConnection.State = ConnectionState.Closed)) Then
                cnConnection = New OleDb.OleDbConnection(ConnectString(strFileName))
                cnConnection.Open()
            End If

            '看看数据源中的列数
            intHowManyCol = dtData.Columns.Count

            '创建插入语句的语句(对应变量)
            For i = 1 To intHowManyCol
                strTmpValues = strTmpValues & " @T" & i & " ,"

            Next i
            strTmpValues = strTmpValues.Substring(0, strTmpValues.Length - 1)


            '对应数据源与SQL语句
            Dim cmd As New OleDbCommand("INSERT INTO  " & strTable & "   VALUES (" & strTmpValues & ")", cnConnection)
            With cmd
                .CommandType = CommandType.Text
                For i = 1 To intHowManyCol
                    .Parameters.Add(New OleDb.OleDbParameter("@T" & i, TranType2OLE(dtData.Columns(i - 1).DataType.ToString)))
                    .Parameters("@T" & i).SourceColumn = dtData.Columns(i - 1).ColumnName
                Next i

            End With
            '插入!!
            adaptData.InsertCommand = cmd
            Dim builder As New OleDbCommandBuilder(adaptData)
            builder.QuotePrefix = "["
            builder.QuoteSuffix = "]"
            adaptData.Update(dtData)
            If ((Not bolIsAlwaysConnect) And (cnConnection.State = ConnectionState.Open)) Then
                cnConnection.Close()
            End If
        Catch ex As Exception
            If ((Not bolIsAlwaysConnect) And (cnConnection.State = ConnectionState.Open)) Then
                cnConnection.Close()
            End If
            Throw New Exception(ex.Message, ex)

        End Try


    End Sub

    ''' <summary>
    ''' 反馈数据库表中的格式
    ''' </summary>
    ''' <param name="strTable">数据库表名</param>
    ''' <returns>返回只有一行的数据表,用作对应列头</returns>
    Public Function ReturnFormat(ByVal strTable As String) As DataTable
        'Dim cnConnection As OleDb.OleDbConnection
        Dim adaptData As New OleDbDataAdapter
        Dim dtFormatData As New DataTable
        Try

            If ((Not bolIsAlwaysConnect) And (cnConnection.State = ConnectionState.Closed)) Then
                cnConnection = New OleDb.OleDbConnection(ConnectString(strFileName))
                cnConnection.Open()
            End If

            adaptData = New OleDb.OleDbDataAdapter("SELECT top 1 * from " & strTable & ";", cnConnection)
            adaptData.Fill(dtFormatData)


            If ((Not bolIsAlwaysConnect) And (cnConnection.State = ConnectionState.Open)) Then
                cnConnection.Close()
            End If
            Return dtFormatData
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function
    Public Function GetAccessDataTable(ByRef odbcmdCmd As OleDbCommand) As DataTable
        'Dim cnConnection As OleDb.OleDbConnection
        Dim adaptData As New OleDbDataAdapter
        Dim dtData As New DataTable

        Try

            If ((Not bolIsAlwaysConnect) And (cnConnection.State = ConnectionState.Closed)) Then
                cnConnection = New OleDb.OleDbConnection(ConnectString(strFileName))
                cnConnection.Open()
            End If

            odbcmdCmd.Connection = cnConnection

            adaptData = New OleDb.OleDbDataAdapter(odbcmdCmd)
            adaptData.Fill(dtData)


            If ((Not bolIsAlwaysConnect) And (cnConnection.State = ConnectionState.Open)) Then
                cnConnection.Close()
            End If
            Return dtData
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function


    Public Function ReturnAll(ByVal strTable As String) As DataTable
        'Dim cnConnection As OleDb.OleDbConnection
        Dim adaptData As New OleDbDataAdapter
        Dim dtFormatData As New DataTable

        Try


            If ((Not bolIsAlwaysConnect) And (cnConnection.State = ConnectionState.Closed)) Then
                cnConnection = New OleDb.OleDbConnection(ConnectString(strFileName))
                cnConnection.Open()
            End If

            adaptData = New OleDb.OleDbDataAdapter("SELECT  * from " & strTable & ";", cnConnection)
            adaptData.Fill(dtFormatData)


            If ((Not bolIsAlwaysConnect) And (cnConnection.State = ConnectionState.Open)) Then
                cnConnection.Close()
            End If
            Return dtFormatData
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function


    Public Function ReturnMaxDate(ByVal strTable As String, ByVal strDateField As String) As Date
        'Dim cnConnection As OleDb.OleDbConnection
        Dim adaptData As New OleDbDataAdapter
        Dim dtMaxDateData As New DataTable

        Try

            If ((Not bolIsAlwaysConnect) And (cnConnection.State = ConnectionState.Closed)) Then
                cnConnection = New OleDb.OleDbConnection(ConnectString(strFileName))
                cnConnection.Open()
            End If

            adaptData = New OleDb.OleDbDataAdapter("SELECT top 1 Max([" & strTable & "].[" & strDateField & "])  from " & strTable & ";", cnConnection)
            adaptData.Fill(dtMaxDateData)



            If ((Not bolIsAlwaysConnect) And (cnConnection.State = ConnectionState.Open)) Then
                cnConnection.Close()
            End If
            If dtMaxDateData.Rows.Count > 0 Then
                If Not TypeOf dtMaxDateData.Rows(0).Item(0) Is Date Then
                    Return Date.MinValue
                Else
                    Return Convert.ToDateTime(dtMaxDateData.Rows(0).Item(0))
                End If
            Else
                Return Date.MinValue
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function

    Public Sub Close()
        Try

            If ((bolIsAlwaysConnect) And (cnConnection.State = ConnectionState.Open)) Then
                cnConnection.Close()
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Sub



#Region "IDisposable Support"
    Private disposedValue As Boolean ' 检测冗余的调用

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO:  释放托管状态(托管对象)。
            End If
            If cnConnection.State = ConnectionState.Open Then
                cnConnection.Close()
            End If
            ' TODO:  释放非托管资源(非托管对象)并重写下面的 Finalize()。
            ' TODO:  将大型字段设置为 null。
        End If
        Me.disposedValue = True
    End Sub

    ' TODO:  仅当上面的 Dispose(ByVal disposing As Boolean)具有释放非托管资源的代码时重写 Finalize()。
    'Protected Overrides Sub Finalize()
    '    ' 不要更改此代码。    请将清理代码放入上面的 Dispose(ByVal disposing As Boolean)中。
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic 添加此代码是为了正确实现可处置模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 不要更改此代码。    请将清理代码放入上面的 Dispose (disposing As Boolean)中。
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
