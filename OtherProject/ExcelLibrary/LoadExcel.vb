Imports System.Data.OleDb
Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.Office
Imports System.Runtime.InteropServices

Public Class LoadExcel
    Implements IDisposable

    Public Property LastException As Exception

    Private strFileName As String
    Public strSheets As New List(Of String)
    Private strExceptionMessage As String



    Public Sub New()
    End Sub
    ''' <summary>
    ''' File to get information from
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <remarks>
    ''' The caller is responsible to ensure the file exists.
    ''' </remarks>
    Public Sub New(ByVal FileName As String)
        strFileName = FileName
    End Sub


    Private Function ConnectionString(ByVal FileName As String) As String
        Dim Builder As New OleDb.OleDbConnectionStringBuilder
        If IO.Path.GetExtension(FileName).ToUpper = ".XLS" Then
            Builder.Provider = "Microsoft.ACE.OLEDB.12.0"
            Builder.Add("Extended Properties", "Excel 8.0;IMEX=1;HDR=Yes;")
        ElseIf IO.Path.GetExtension(FileName).ToUpper = ".XLSX" Then
            Builder.Provider = "Microsoft.ACE.OLEDB.12.0"
            Builder.Add("Extended Properties", "Excel 12.0;IMEX=1;HDR=Yes;")
        End If

        Builder.DataSource = FileName

        Return Builder.ToString

    End Function

    Public ReadOnly Property ExceptionMessage As String
        Get
            Return strExceptionMessage
        End Get
    End Property




    ''' <summary>
    ''' Retrieve worksheet and name range names.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInformation() As Boolean
        Dim Success As Boolean = True

        If Not IO.File.Exists(strFileName) Then
            Dim ex As New Exception("Failed to locate '" & strFileName & "'")
            _LastException = ex
            Throw ex
        End If

        strSheets.Clear()


        Dim xlApp As Excel.Application = Nothing
        Dim xlWorkBooks As Excel.Workbooks = Nothing
        Dim xlWorkBook As Excel.Workbook = Nothing
        Dim xlWorkSheets As Excel.Sheets = Nothing

        Try
            xlApp = New Excel.Application
            xlApp.DisplayAlerts = False
            xlWorkBooks = xlApp.Workbooks
            xlWorkBook = xlWorkBooks.Open(strFileName)


            xlWorkSheets = xlWorkBook.Sheets

            For x As Integer = 1 To xlWorkSheets.Count
                Dim Sheet1 As Excel.Worksheet = CType(xlWorkSheets(x), Excel.Worksheet)
                strSheets.Add(Sheet1.Name)
                Runtime.InteropServices.Marshal.FinalReleaseComObject(Sheet1)
                Sheet1 = Nothing
            Next

            xlWorkBook.Close()
            xlApp.UserControl = True
            xlApp.Quit()

        Catch ex As Exception
            _LastException = ex
            Success = False
        Finally

            If Not xlWorkSheets Is Nothing Then
                Marshal.FinalReleaseComObject(xlWorkSheets)
                xlWorkSheets = Nothing
            End If

            If Not xlWorkBook Is Nothing Then
                Marshal.FinalReleaseComObject(xlWorkBook)
                xlWorkBook = Nothing
            End If

            If Not xlWorkBooks Is Nothing Then
                Marshal.FinalReleaseComObject(xlWorkBooks)
                xlWorkBooks = Nothing
            End If

            If Not xlApp Is Nothing Then
                Marshal.FinalReleaseComObject(xlApp)
                xlApp = Nothing
            End If
        End Try

        Return Success

    End Function


    Public Function GetData(ByVal strTableName As String) As DataTable
        Dim dtExl As New DataTable
        Dim strSelectStatement As String

        Dim Connection_String As String = ""
        Connection_String = ConnectionString(strFileName)
        strSelectStatement = "SELECT * FROM [" & strTableName & "$]"

        Try
            Using cn As New OleDbConnection With {.ConnectionString = Connection_String}
                Using cmd As New OleDbCommand With {.Connection = cn, .CommandText = strSelectStatement}
                    cn.Open()
                    dtExl.Load(cmd.ExecuteReader)
                End Using
            End Using
        Catch ex As Exception
            strExceptionMessage = ex.Message
        End Try

        Return dtExl
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' 检测冗余的调用

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO:  释放托管状态(托管对象)。
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
