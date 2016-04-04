Imports Microsoft.VisualBasic
Imports System.Security.Cryptography
Imports System.Data
Imports System.Data.SqlClient

Public Class CommonLibrary
    Public Function StringTranscodingToMD5(strString As String) As String
        Dim strAfterTrans As String
        Dim md5MD5 As MD5
        Dim bytearrInterPro As Byte()
        Dim i As Integer

        Try

            strAfterTrans = ""
            md5MD5 = MD5.Create
            bytearrInterPro = md5MD5.ComputeHash(Encoding.UTF8.GetBytes(strString))
            bytearrInterPro.Reverse
            If bytearrInterPro.Length > 18 Then
                For i = 3 To (bytearrInterPro.Length - 3)
                    strAfterTrans = strAfterTrans & bytearrInterPro(i).ToString
                    If i = 4 Then
                        strAfterTrans = strAfterTrans & "1" & bytearrInterPro(i).ToString
                    End If
                    If i = 6 Then
                        strAfterTrans = strAfterTrans & "9" & bytearrInterPro(i).ToString
                    End If
                    If i = 11 Then
                        strAfterTrans = strAfterTrans & "8" & bytearrInterPro(i).ToString
                    End If
                    If i = 13 Then
                        strAfterTrans = strAfterTrans & "8" & bytearrInterPro(i).ToString
                    End If
                Next

            Else
                For i = 0 To (bytearrInterPro.Length - 1)
                    strAfterTrans = strAfterTrans & bytearrInterPro(i).ToString()
                Next
            End If

            Return strAfterTrans
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function

    ''' <summary>
    ''' 得到一个纯粹的日期
    ''' </summary>
    ''' <param name="intCheckYear">赋予年份</param>
    ''' <param name="intCheckMonth">赋予月份</param>
    ''' <param name="intCheckDay">赋予日</param>
    ''' <returns>反馈一个纯粹的日期</returns>
    Public Shared Function GetPureDate(intCheckYear As Integer, intCheckMonth As Integer, intCheckDay As Integer) As Date
        '创建临时对象
        Dim tmpDateCheck As Date
        Dim intTmpCheckYear As Integer
        Dim intTmpCheckMonth As Integer
        Dim intTmpCheckDay As Integer
        intTmpCheckYear = intCheckYear
        intTmpCheckMonth = intCheckMonth
        intTmpCheckDay = intCheckDay

        '传递日期/月份/年份
        If intCheckYear < 1 Then intTmpCheckYear = 1988
        If intCheckMonth < 1 Or intCheckMonth > 12 Then intTmpCheckMonth = 1
        If intCheckDay < 1 Or intCheckDay > 31 Then intTmpCheckDay = 1

        If ((intCheckMonth = 1 Or intCheckMonth = 3 Or intCheckMonth = 5 Or intCheckMonth = 7 Or intCheckMonth = 8 Or intCheckMonth = 10 Or intCheckMonth = 12) And intCheckDay >= 31) Then
            intTmpCheckDay = 31
        ElseIf intCheckDay >= 30 Then
            intTmpCheckDay = 30
        End If

        '判断是否闰年
        If ((intCheckYear Mod 4 = 0 And intCheckYear Mod 100 <> 0) Or intCheckYear Mod 400 = 0) Then
            If intCheckMonth = 2 And intCheckDay >= 29 Then
                intTmpCheckDay = 29
            End If
        Else
            If intCheckMonth = 2 And intCheckDay >= 28 Then
                intTmpCheckDay = 28
            End If
        End If
        '赋予日期数据
        tmpDateCheck = New Date(intTmpCheckYear, intTmpCheckMonth, intTmpCheckDay)
        Return tmpDateCheck

    End Function


    ''' <summary>
    ''' 得到一个纯粹的日期
    ''' </summary>
    ''' <param name="intCheckYear">赋予年份</param>
    ''' <param name="intCheckMonth">赋予月份</param>
    ''' <param name="intCheckDay">赋予日</param>
    ''' <returns>反馈一个纯粹的日期</returns>
    Public Shared Function GetPureDate(intCheckYear As Integer, intCheckMonth As Integer, intCheckDay As Integer, intCheckHour As Integer, intCheckMin As Integer, intCheckSec As Integer) As Date
        '创建临时对象
        Dim tmpDateCheck As Date
        Dim intTmpCheckYear As Integer
        Dim intTmpCheckMonth As Integer
        Dim intTmpCheckDay As Integer
        Dim intTmpCheckHour As Integer
        Dim intTmpCheckMin As Integer
        Dim intTmpCheckSec As Integer
        intTmpCheckYear = intCheckYear
        intTmpCheckMonth = intCheckMonth
        intTmpCheckDay = intCheckDay
        intTmpCheckHour = intCheckHour
        intTmpCheckMin = intCheckMin
        intTmpCheckSec = intCheckSec

        '传递日期/月份/年份
        If intCheckYear < 1 Then intTmpCheckYear = 1988
        If intCheckMonth < 1 Or intCheckMonth > 12 Then intTmpCheckMonth = 1
        If intCheckDay < 1 Or intCheckDay > 31 Then intTmpCheckDay = 1
        If intCheckHour < 0 Or intCheckHour > 23 Then intTmpCheckDay = 0
        If intCheckMin < 0 Or intCheckMin > 59 Then intTmpCheckMin = 0
        If intCheckSec < 0 Or intCheckSec > 59 Then intTmpCheckSec = 0

        If ((intCheckMonth = 1 Or intCheckMonth = 3 Or intCheckMonth = 5 Or intCheckMonth = 7 Or intCheckMonth = 8 Or intCheckMonth = 10 Or intCheckMonth = 12) And intCheckDay >= 31) Then
            intTmpCheckDay = 31
        ElseIf intCheckDay >= 30 Then
            intTmpCheckDay = 30
        End If

        '判断是否闰年
        If ((intCheckYear Mod 4 = 0 And intCheckYear Mod 100 <> 0) Or intCheckYear Mod 400 = 0) Then
            If intCheckMonth = 2 And intCheckDay >= 29 Then
                intTmpCheckDay = 29
            End If
        Else
            If intCheckMonth = 2 And intCheckDay >= 28 Then
                intTmpCheckDay = 28
            End If
        End If
        '赋予日期数据
        tmpDateCheck = New Date(intTmpCheckYear, intTmpCheckMonth, intTmpCheckDay, intTmpCheckHour, intTmpCheckMin, intTmpCheckSec)
        Return tmpDateCheck

    End Function


    ''' <summary>
    ''' 反馈最小值
    ''' </summary>
    ''' <param name="intFirst">第一个数值</param>
    ''' <param name="intSecond ">第二个数值</param>
    ''' <returns>反馈一个整形</returns>
    Public Shared Function GetMinNumber(intFirst As Integer, intSecond As Integer) As Integer
        If intFirst > intSecond Then
            Return intSecond
        Else
            Return intFirst
        End If
    End Function


    ''' <summary>
    ''' 反馈最大值
    ''' </summary>
    ''' <param name="intFirst">第一个数值</param>
    ''' <param name="intSecond ">第二个数值</param>
    ''' <returns>反馈一个整形</returns>
    Public Shared Function GetMaxNumber(intFirst As Integer, intSecond As Integer) As Integer
        If intFirst < intSecond Then
            Return intSecond
        Else
            Return intFirst
        End If
    End Function




    ''' <summary>
    ''' 根据给出的格式表返回根据格式表格式的数据
    ''' </summary>
    ''' <param name="dtData">数据表</param>
    ''' <param name="dtFormat">格式表</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ReturnNewNormalDT(ByVal dtData As DataTable, ByVal dtFormat As DataTable) As DataTable
        Dim intHowManyCol As Integer
        Dim i As Integer
        Dim j As Integer
        Dim dtDataNew As New DataTable
        Dim drTmp As DataRow
        Dim intTmpListOfTitle() As Integer
        Dim bolIsNetworkSupport As Boolean
        ReDim intTmpListOfTitle(0 To (dtData.Columns.Count - 1))
        '读取格式表的列数
        intHowManyCol = dtFormat.Columns.Count

        '创建新数据表的列标题以及数据类型
        For i = 1 To intHowManyCol
            dtDataNew.Columns.Add(New System.Data.DataColumn(dtFormat.Columns(i - 1).ColumnName, dtFormat.Columns(i - 1).DataType))
        Next i
        intHowManyCol = dtData.Columns.Count
        For i = 1 To intHowManyCol
            If dtFormat.Columns(dtData.Columns(i - 1).ColumnName) IsNot Nothing Then
                intTmpListOfTitle(i - 1) = dtFormat.Columns(dtData.Columns(i - 1).ColumnName).Ordinal
            Else
                intTmpListOfTitle(i - 1) = -1
            End If
        Next i

        If dtFormat.Columns(0).ColumnName = "msc_id" And dtFormat.Columns(3).ColumnName = "BSIC" Then
            bolIsNetworkSupport = True
        Else
            bolIsNetworkSupport = False
        End If
        '每行数据表的来读
        For i = 1 To dtData.Rows.Count
            If Not (bolIsNetworkSupport And dtData.Rows(i - 1).Item(0).ToString.Length > 10) Then
                drTmp = dtDataNew.NewRow
                For j = 1 To dtData.Columns.Count
                    Try
                        If intTmpListOfTitle(j - 1) >= 0 Then
                            If dtData.Rows(i - 1).Item(j - 1).ToString = "" Or dtData.Rows(i - 1).Item(j - 1).ToString = "#DIV/0" Or dtData.Rows(i - 1).Item(j - 1).ToString = "#N/A" Then
                                drTmp(intTmpListOfTitle(j - 1)) = DBNull.Value
                            Else
                                drTmp(intTmpListOfTitle(j - 1)) = dtData.Rows(i - 1).Item(j - 1)
                            End If
                        End If
                    Catch
                        drTmp(intTmpListOfTitle(j - 1)) = DBNull.Value
                    End Try
                Next j
                dtDataNew.Rows.Add(drTmp)
            End If
        Next i
        Return dtDataNew
    End Function

    Public Shared Function GetMaxDateFile(ByRef strlFile As List(Of String), intWhereYear As Integer, intWhereMonth As Integer, intWhereDay As Integer) As List(Of String)
        Dim strtmpFileName As String
        Dim strtmpOnlyFileName As String
        Dim strlDir As New List(Of String)
        Dim intYear As Integer
        Dim intMonth As Integer
        Dim intDay As Integer
        Dim dateSourceDate As Date
        Dim dateNowMaxDate As Date
        dateNowMaxDate = New Date(1988, 12, 21)
        If intWhereYear < 0 Then
            intWhereYear = 0
        End If
        If intWhereMonth < 0 Then
            intWhereMonth = 0
        End If
        If intWhereDay < 0 Then
            intWhereDay = 1
        End If
        intYear = 1988
        intMonth = 11
        intDay = 12

        strlDir.Clear()
        For Each strtmpFileName In strlFile
            strtmpOnlyFileName = IO.Path.GetFileName(strtmpFileName)

            If strtmpOnlyFileName.Length > (CommonLibrary.GetMaxNumber(intWhereDay + 4, CommonLibrary.GetMaxNumber(intWhereMonth + 2, intWhereYear + 2)) + 1) Then
                If (IsNumeric(strtmpOnlyFileName.Substring(intWhereYear, 4)) And IsNumeric(strtmpOnlyFileName.Substring(intWhereMonth, 2)) And IsNumeric(strtmpOnlyFileName.Substring(intWhereDay, 2))) Then
                    intYear = CType(strtmpOnlyFileName.Substring(intWhereYear, 4), Integer)
                    intMonth = CType(strtmpOnlyFileName.Substring(intWhereMonth, 2), Integer)
                    intDay = CType(strtmpOnlyFileName.Substring(intWhereDay, 2), Integer)
                End If
            End If
            dateSourceDate = CommonLibrary.GetPureDate(intYear, intMonth, intDay)
            If dateSourceDate > dateNowMaxDate Then
                strlDir.Clear()
                strlDir.Add(strtmpFileName)
                dateNowMaxDate = dateSourceDate
            End If
        Next
        Return strlDir
    End Function

    Public Shared Function GetMaxDateFile(ByRef strlFile As List(Of String), intWhereYear As Integer, intWhereMonth As Integer, intWhereDay As Integer, intWhereHour As Integer, intWhereMin As Integer, intWhereSec As Integer) As List(Of String)
        Dim strtmpFileName As String
        Dim strtmpOnlyFileName As String
        Dim strlDir As New List(Of String)
        Dim intYear As Integer
        Dim intMonth As Integer
        Dim intDay As Integer
        Dim intHour As Integer
        Dim intMin As Integer
        Dim intSec As Integer
        Dim dateSourceDate As Date
        Dim dateNowMaxDate As Date
        dateNowMaxDate = New Date(1988, 12, 21)
        If intWhereYear < 0 Then
            intWhereYear = 0
        End If
        If intWhereMonth < 0 Then
            intWhereMonth = 0
        End If
        If intWhereDay < 0 Then
            intWhereDay = 0
        End If
        intYear = 1988
        intMonth = 11
        intDay = 12
        intHour = 0
        intMin = 0
        intSec = 0

        strlDir.Clear()
        For Each strtmpFileName In strlFile
            strtmpOnlyFileName = IO.Path.GetFileName(strtmpFileName)
            If strtmpOnlyFileName.Length > (CommonLibrary.GetMaxNumber(CommonLibrary.GetMaxNumber(CommonLibrary.GetMaxNumber(CommonLibrary.GetMaxNumber(intWhereDay + 4, CommonLibrary.GetMaxNumber(intWhereMonth + 2, intWhereYear + 2)), intWhereHour + 2), intWhereMin + 2), intWhereSec + 2) + 1) Then
                If (IsNumeric(strtmpOnlyFileName.Substring(intWhereYear, 4)) And IsNumeric(strtmpOnlyFileName.Substring(intWhereMonth, 2)) And IsNumeric(strtmpOnlyFileName.Substring(intWhereDay, 2)) And IsNumeric(strtmpOnlyFileName.Substring(intWhereHour, 2)) And IsNumeric(strtmpOnlyFileName.Substring(intWhereMin, 2)) And IsNumeric(strtmpOnlyFileName.Substring(intWhereSec, 2))) Then
                    intYear = CType(strtmpOnlyFileName.Substring(intWhereYear, 4), Integer)
                    intMonth = CType(strtmpOnlyFileName.Substring(intWhereMonth, 2), Integer)
                    intDay = CType(strtmpOnlyFileName.Substring(intWhereDay, 2), Integer)
                    intHour = CType(strtmpOnlyFileName.Substring(intWhereHour, 2), Integer)
                    intMin = CType(strtmpOnlyFileName.Substring(intWhereMin, 2), Integer)
                    intSec = CType(strtmpOnlyFileName.Substring(intWhereSec, 2), Integer)
                End If
            End If
            dateSourceDate = CommonLibrary.GetPureDate(intYear, intMonth, intDay, intHour, intMin, intSec)
            If dateSourceDate > dateNowMaxDate Then
                strlDir.Clear()
                strlDir.Add(strtmpFileName)
                dateNowMaxDate = dateSourceDate
            End If
        Next
        Return strlDir
    End Function

    Public Shared Function GetAfterOneDateFile(ByRef strlFile As List(Of String), intWhereYear As Integer, intWhereMonth As Integer, intWhereDay As Integer, dateOneDate As Date) As List(Of String)
        Dim strtmpFileName As String
        Dim strtmpOnlyFileName As String
        Dim strlDir As New List(Of String)
        Dim intYear As Integer
        Dim intMonth As Integer
        Dim intDay As Integer
        Dim dateSourceDate As Date
        If intWhereYear < 0 Then
            intWhereYear = 0
        End If
        If intWhereMonth < 0 Then
            intWhereMonth = 0
        End If
        If intWhereDay < 0 Then
            intWhereDay = 1
        End If
        intYear = 1988
        intMonth = 11
        intDay = 12

        strlDir.Clear()
        For Each strtmpFileName In strlFile
            strtmpOnlyFileName = IO.Path.GetFileName(strtmpFileName)

            If strtmpOnlyFileName.Length > (CommonLibrary.GetMaxNumber(intWhereDay + 4, CommonLibrary.GetMaxNumber(intWhereMonth + 2, intWhereYear + 2)) + 1) Then
                If (IsNumeric(strtmpOnlyFileName.Substring(intWhereYear, 4)) And IsNumeric(strtmpOnlyFileName.Substring(intWhereMonth, 2)) And IsNumeric(strtmpOnlyFileName.Substring(intWhereDay, 2))) Then
                    intYear = CType(strtmpOnlyFileName.Substring(intWhereYear, 4), Integer)
                    intMonth = CType(strtmpOnlyFileName.Substring(intWhereMonth, 2), Integer)
                    intDay = CType(strtmpOnlyFileName.Substring(intWhereDay, 2), Integer)
                End If
            End If
            dateSourceDate = CommonLibrary.GetPureDate(intYear, intMonth, intDay)
            If dateSourceDate > dateOneDate Then
                strlDir.Add(strtmpFileName)
            End If
        Next
        Return strlDir
    End Function

    Public Shared Function GetSQLServerConnect(strDateBase As String) As SqlConnection
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

End Class
