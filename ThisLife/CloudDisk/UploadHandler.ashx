<%@ WebHandler Language="VB" Class="UploadHandler" %>

Imports System
Imports System.Web
Imports SQLServerLibrary
Imports System.IO
Imports System.Data.SqlClient

Public Class UploadHandler : Implements IHttpHandler, IRequiresSessionState
    Dim utToken As UploadToken = New UploadToken
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim strGoShow As String
        Dim strJsonLoad As String
        Dim strFileName As String
        Dim intI As Integer
        Dim scmdCMD As SqlCommand
        Dim strRandStr As String

        context.Response.ContentType = "text/plain"
        strGoShow = ""
        strJsonLoad = ""
        strRandStr = RandomFix(8)
        Try
            If utToken.CheckToken(context) Then
                If context.Request.Files.Count > 0 Then
                    For intI = 0 To context.Request.Files.Count - 1
                        strFileName = Now.ToString("yyyyMMddHHmmss") & "-三世云盘文件-" & context.Session("SanShiUserName") & "-" & context.Request.Files(intI).FileName

                        context.Request.Files(intI).SaveAs(context.Server.MapPath("/TmpFiles/") & strFileName)


                        scmdCMD = sqllSSLibrary.GetCommandStr("INSERT [dbo].[dt_UploadFileInfo] ([SaveTime],[UserName],[FileName],[SaveFileName],[Password]  ) VALUES ( GETDATE(), '" & context.Session("SanShiUserName") & "' , '" & context.Request.Files(intI).FileName & "' , '" & strFileName & "', '" & strRandStr & "')", CommonLibrary.GetSQLServerConnect("ConnectionLogDB"))
                        sqllSSLibrary.ExecNonQuery(scmdCMD)

                    Next intI


                    strGoShow = "'PassCode':'" & strRandStr & "'"
                End If
                strJsonLoad = "{'GotError':0," & strGoShow & "}"
            Else
                strJsonLoad = "{'GotError':1,'ErrorDsp':'验证错误'}"

            End If

        Catch ex As Exception
            strJsonLoad = "{'GotError':1,'ErrorDsp':'上传过程出错，请呼叫网管。 '}"

        End Try

        context.Response.Write(strJsonLoad)

    End Sub


    Public Function RandomFix(longN As Long) As String
        Dim strR As String
        Dim intT As Integer
        Dim intI As Integer
        Randomize()

        strR = Chr(Int(Rnd() * 26) + 65) '生成一个大写字母
        For intI = 1 To longN 'N是指定长度
            Randomize()
            intT = Int(Rnd() * 3)
            Select Case intT
                Case 0
                    Randomize()
                    strR = strR + Chr(Int(Rnd() * 26) + 65) '生成一个大写字母
                Case 1
                    Randomize()
                    strR = strR + Chr(Int(Rnd() * 26) + 97) '生成一个小写字母
                Case 2
                    Randomize()
                    strR = strR + Chr(Int(Rnd() * 10) + 48) '生成一个数字
            End Select
        Next intI
        RandomFix = strR
    End Function


    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class