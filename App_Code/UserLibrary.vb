Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports SQLServerLibrary
Imports System.Data
Imports CommonLibrary
Imports SQLServerLibrary.LoadSQLServer

Public Class UserLibrary
    Implements IDisposable
    Dim sqllSSLibrary As LoadSQLServer
    Dim clCommon As CommonLibrary



    Public Function AddUser(strUserName As String, strPassword As String, strPhoneNumber As String, strRealChineseName As String, strCompanyName As String, strEMail As String, strQQ As String) As Integer
        Dim scmdCMD As SqlCommand
        Dim spName As SqlParameter
        Dim spPW As SqlParameter
        Dim spPhone As SqlParameter
        Dim spRealChinese As SqlParameter
        Dim spCompany As SqlParameter
        Dim spEMail As SqlParameter
        Dim spQQ As SqlParameter
        Dim spReturnValue As SqlParameter
        Try


            scmdCMD = sqllSSLibrary.GetCommandProc("proc_AddUser", CommonLibrary.GetSQLServerConnect("ConnectionUserDB"))


            spName = New SqlParameter("@UserName", SqlDbType.VarChar, 40)
            spPW = New SqlParameter("@Password", SqlDbType.VarChar, 80)
            spPhone = New SqlParameter("@PhoneNumber", SqlDbType.VarChar, 40)
            spRealChinese = New SqlParameter("@RealName", SqlDbType.VarChar, 40)
            spCompany = New SqlParameter("@CompanyName", SqlDbType.VarChar, 100)
            spEMail = New SqlParameter("@Email", SqlDbType.VarChar, 100)
            spQQ = New SqlParameter("@QQ", SqlDbType.VarChar, 50)
            spReturnValue = New SqlParameter("ReturnValue", SqlDbType.Int, 4)




            spName.Value = strUserName
            spPW.Value = clCommon.StringTranscodingToMD5(strPassword)
            spPhone.Value = strPhoneNumber
            spRealChinese.Value = strRealChineseName
            spCompany.Value = strCompanyName
            spEMail.Value = strEMail
            spQQ.Value = strQQ




            scmdCMD.Parameters.Add(spName)
            scmdCMD.Parameters.Add(spPW)
            scmdCMD.Parameters.Add(spPhone)
            scmdCMD.Parameters.Add(spRealChinese)
            scmdCMD.Parameters.Add(spCompany)
            scmdCMD.Parameters.Add(spEMail)
            scmdCMD.Parameters.Add(spQQ)
            scmdCMD.Parameters.Add(spReturnValue)
            spReturnValue.Direction = ParameterDirection.ReturnValue

            sqllSSLibrary.ExecNonQuery(scmdCMD)

            Return Convert.ToInt32(spReturnValue.Value.ToString())


        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
            scmdCMD.Dispose()
            scmdCMD = Nothing

        End Try

    End Function

    Public Function UserLoginCheck(strUserName As String, strPassword As String, strMachineName As String) As DataTable
        Dim scmdCMD As SqlCommand
        Dim spName As SqlParameter
        Dim spPW As SqlParameter
        Dim spMachineName As SqlParameter

        Dim dtUserDetail As DataTable
        Try

            scmdCMD = sqllSSLibrary.GetCommandProc("proc_LoginCheck", CommonLibrary.GetSQLServerConnect("ConnectionUserDB"))


            spName = New SqlParameter("@UserName", SqlDbType.VarChar, 40)
            spPW = New SqlParameter("@Password", SqlDbType.VarChar, 80)
            spMachineName = New SqlParameter("@MachineName", SqlDbType.VarChar, 200)

            spName.Value = strUserName
            spPW.Value = clCommon.StringTranscodingToMD5(strPassword)
            spMachineName.Value = strMachineName

            scmdCMD.Parameters.Add(spName)
            scmdCMD.Parameters.Add(spPW)
            scmdCMD.Parameters.Add(spMachineName)

            '            sqllSSLibrary.ExecNonQuery(scmdCMD)

            dtUserDetail = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)

            Return dtUserDetail
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
            dtUserDetail.Dispose()
            dtUserDetail = Nothing
            scmdCMD.Dispose()
            scmdCMD = Nothing
        End Try


    End Function




    Public Sub New()
        sqllSSLibrary = New LoadSQLServer
        clCommon = New CommonLibrary
    End Sub

    Public Function GetInOrGetOut(ByRef hssSession As HttpSessionState, ByRef hccCookies As HttpCookie, strMachineName As String, ByRef hasApplication As HttpApplicationState) As Boolean
        Dim scmdCMD As SqlCommand
        Dim spName As SqlParameter
        Dim dtUserLevelAndMachineName As DataTable
        Dim strDBMachineName As String

        Try


            If ((hssSession("SanShiUserName") <> Nothing) And (hssSession("PowerLevel") <> Nothing)) Then
                If (hasApplication("NowUser:" & hssSession("SanShiUserName")) <> Nothing) Then
                    If (hasApplication("NowUser:" & hssSession("SanShiUserName")) <> strMachineName) Then

                        Return False
                    End If
                End If
                If ((hssSession("SanShiUserName").ToString <> "") And (hssSession("PowerLevel").ToString <> "") And (hssSession("PowerLevel") > 0)) Then
                    Return True
                End If
            End If
            If hccCookies IsNot Nothing Then
                If hccCookies("SanShiUserName") IsNot Nothing Then

                    '有登陆Cookies的就登陆登陆用户
                    scmdCMD = sqllSSLibrary.GetCommandProc("proc_GetPowerLevelByUserName", CommonLibrary.GetSQLServerConnect("ConnectionUserDB"))
                    spName = New SqlParameter("@UserName", SqlDbType.VarChar, 40)
                    spName.Value = hccCookies("SanShiUserName").ToString

                    scmdCMD.Parameters.Add(spName)
                    sqllSSLibrary.ExecNonQuery(scmdCMD)
                    dtUserLevelAndMachineName = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)
                    strDBMachineName = clCommon.StringTranscodingToMD5(strMachineName)

                    If hccCookies("MachineName").ToString = strDBMachineName Then
                        hssSession("SanShiUserName") = hccCookies("SanShiUserName").ToString
                        hssSession("PowerLevel") = dtUserLevelAndMachineName.Rows(0)("PowerLevel").ToString
                        hasApplication("NowUser:" & hssSession("SanShiUserName")) = strMachineName

                        Return True
                    Else
                        hssSession("SanShiUserName") = Nothing
                        hssSession("PowerLevel") = Nothing
                        Return False
                    End If
                    dtUserLevelAndMachineName.Dispose()
                End If
            End If

            dtUserLevelAndMachineName = Nothing

            Return False


        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
            dtUserLevelAndMachineName.Dispose()
            dtUserLevelAndMachineName = Nothing
            scmdCMD.Dispose()
            scmdCMD = Nothing
        End Try



    End Function



    Public Function CheckPower(ByRef hssSession As HttpSessionState, intAccessLevel As Integer, ByRef hrResponse As HttpResponse) As Boolean
        Try


            If (hssSession("PowerLevel") Is Nothing) Then
                hrResponse.Redirect("/cannotaccess.aspx", False)
                Return False
            End If
            If hssSession("PowerLevel") >= intAccessLevel Then
                Return True
            End If


            hrResponse.Redirect("/cannotaccess.aspx", False)
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
        Return False

    End Function
    Public Function ReturnAllUserInfo(strKeyWord As String) As DataTable
        Dim scmdCMD As SqlCommand
        Dim spKeyWord As SqlParameter

        Dim dtUserDetail As DataTable
        Try

            scmdCMD = sqllSSLibrary.GetCommandProc("proc_LoadUserDataByKeyWord", CommonLibrary.GetSQLServerConnect("ConnectionUserDB"))


            spKeyWord = New SqlParameter("@KeyWord", SqlDbType.VarChar, 40)

            spKeyWord.Value = strKeyWord

            scmdCMD.Parameters.Add(spKeyWord)


            dtUserDetail = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)

            Return dtUserDetail
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
            dtUserDetail.Dispose()
            dtUserDetail = Nothing
            scmdCMD.Dispose()
            scmdCMD = Nothing
        End Try


    End Function

    Public Function ChangeUserPassword(strUserName As String, strPassword As String) As Boolean
        Dim scmdCMD As SqlCommand
        Dim spName As SqlParameter
        Dim spPW As SqlParameter
        Dim intQueryCount As Integer

        Try

            scmdCMD = sqllSSLibrary.GetCommandProc("proc_ChangePassword", CommonLibrary.GetSQLServerConnect("ConnectionUserDB"))


            spName = New SqlParameter("@UserName", SqlDbType.VarChar, 40)
            spPW = New SqlParameter("@Password", SqlDbType.VarChar, 80)


            spName.Value = strUserName
            spPW.Value = clCommon.StringTranscodingToMD5(strPassword)

            scmdCMD.Parameters.Add(spName)
            scmdCMD.Parameters.Add(spPW)


            intQueryCount = sqllSSLibrary.ExecNonQueryAndReturn(scmdCMD)
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
            scmdCMD.Dispose()
            scmdCMD = Nothing

        End Try
        If intQueryCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function ChangeUserPowerLevel(strUserName As String, intPowerLevel As Integer) As Boolean
        Dim scmdCMD As SqlCommand
        Dim spName As SqlParameter
        Dim spPowerLevel As SqlParameter
        Dim intQueryCount As Integer

        Try

            scmdCMD = sqllSSLibrary.GetCommandProc("proc_ChangePowerLevel", CommonLibrary.GetSQLServerConnect("ConnectionUserDB"))


            spName = New SqlParameter("@UserName", SqlDbType.VarChar, 40)
            spPowerLevel = New SqlParameter("@PowerLevel", SqlDbType.SmallInt)


            spName.Value = strUserName
            spPowerLevel.Value = intPowerLevel

            scmdCMD.Parameters.Add(spName)
            scmdCMD.Parameters.Add(spPowerLevel)


            intQueryCount = sqllSSLibrary.ExecNonQueryAndReturn(scmdCMD)
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
            scmdCMD.Dispose()
            scmdCMD = Nothing

        End Try
        If intQueryCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function ReturnOneUserInfoByUserName(strUserName As String) As DataTable
        Dim scmdCMD As SqlCommand
        Dim spUserName As SqlParameter

        Dim dtUserDetail As DataTable
        Try

            scmdCMD = sqllSSLibrary.GetCommandProc("proc_LoadUserDataByUserName", CommonLibrary.GetSQLServerConnect("ConnectionUserDB"))


            spUserName = New SqlParameter("@UserName", SqlDbType.VarChar, 40)

            spUserName.Value = strUserName

            scmdCMD.Parameters.Add(spUserName)


            dtUserDetail = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)

            Return dtUserDetail
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
            dtUserDetail.Dispose()
            dtUserDetail = Nothing
            scmdCMD.Dispose()
            scmdCMD = Nothing
        End Try


    End Function





    Public Function ModifyUserInformation(strUserName As String, strPhoneNumber As String, strRealChineseName As String, strCompanyName As String, strEMail As String, strQQ As String) As Integer
        Dim scmdCMD As SqlCommand
        Dim spName As SqlParameter
        Dim spPhone As SqlParameter
        Dim spRealChinese As SqlParameter
        Dim spCompany As SqlParameter
        Dim spEMail As SqlParameter
        Dim spQQ As SqlParameter
        Dim spReturnValue As SqlParameter
        Try


            scmdCMD = sqllSSLibrary.GetCommandProc("proc_ModifyUserInfo", CommonLibrary.GetSQLServerConnect("ConnectionUserDB"))


            spName = New SqlParameter("@UserName", SqlDbType.VarChar, 40)
            spPhone = New SqlParameter("@PhoneNumber", SqlDbType.VarChar, 40)
            spRealChinese = New SqlParameter("@RealName", SqlDbType.VarChar, 40)
            spCompany = New SqlParameter("@CompanyName", SqlDbType.VarChar, 100)
            spEMail = New SqlParameter("@Email", SqlDbType.VarChar, 100)
            spQQ = New SqlParameter("@QQ", SqlDbType.VarChar, 50)
            spReturnValue = New SqlParameter("ReturnValue", SqlDbType.Int, 4)




            spName.Value = strUserName
            spPhone.Value = strPhoneNumber
            spRealChinese.Value = strRealChineseName
            spCompany.Value = strCompanyName
            spEMail.Value = strEMail
            spQQ.Value = strQQ




            scmdCMD.Parameters.Add(spName)
            scmdCMD.Parameters.Add(spPhone)
            scmdCMD.Parameters.Add(spRealChinese)
            scmdCMD.Parameters.Add(spCompany)
            scmdCMD.Parameters.Add(spEMail)
            scmdCMD.Parameters.Add(spQQ)
            scmdCMD.Parameters.Add(spReturnValue)
            spReturnValue.Direction = ParameterDirection.ReturnValue

            sqllSSLibrary.ExecNonQuery(scmdCMD)

            Return Convert.ToInt32(spReturnValue.Value.ToString())


        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
            scmdCMD.Dispose()
            scmdCMD = Nothing

        End Try

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
