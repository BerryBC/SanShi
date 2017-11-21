<%@ WebHandler Language="VB" Class="LTELittleTestAPI" %>

Imports System
Imports System.Web
Imports SQLServerLibrary
Imports System.Data
Imports System.IO
Imports System.Data.SqlClient

Public Class LTELittleTestAPI : Implements IHttpHandler
    Dim utToken As UploadToken = New UploadToken
    Dim sqllSSLibrary As LoadSQLServer = New LoadSQLServer
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim strGoShow As String
        Dim strJsonLoad As String
        Dim scmdCMD As SqlCommand
        Dim dtBaseSationDetailsMana As DataTable
        Dim longJ As Long
        Dim drDataInDataTable As DataRow




        context.Response.ContentType = "text/plain"
        strGoShow = ""
        strJsonLoad = ""
        Try
            If utToken.CheckToken(context) Then


                scmdCMD = sqllSSLibrary.GetCommandStr("SELECT * FROM (SELECT ExamQ,ExamA,ExamTypes,(ROW_NUMBER()over(partition BY ExamTypes ORDER by NEWID())) AS [TT1] FROM  dbo.dt_TmpExam WHERE LEN(ExamA)=1) AS Shit WHERE TT1<5 ORDER BY NEWID()", CommonLibrary.GetSQLServerConnect("ConnectionWorkOfOther"))
                dtBaseSationDetailsMana = sqllSSLibrary.GetSQLServerDataTable(scmdCMD)

                If dtBaseSationDetailsMana.Rows.Count > 0 Then

                    For longJ = 0 To dtBaseSationDetailsMana.Rows.Count - 1
                        Dim strInExam As String
                        drDataInDataTable = dtBaseSationDetailsMana.Rows.Item(longJ)
                        strInExam = "{'ExamQ':'"
                        strInExam += drDataInDataTable(0).ToString
                        strInExam += "','ExamA':'"
                        strInExam += drDataInDataTable(1).ToString
                        strInExam += "','ExamTypes':'"
                        strInExam += drDataInDataTable(2).ToString
                        strInExam += "'},"

                        strGoShow += strInExam
                    Next
                    strGoShow = Left(strGoShow, Len(strGoShow) - 1)
                    strJsonLoad = "{'GotError':0,'GoThrow':[" & strGoShow & "]}"
                Else
                    strJsonLoad = "{'GotError':1,'ErrorDsp':'没有题目啊！！'}"

                End If
            Else
                strJsonLoad = "{'GotError':1,'ErrorDsp':'验证错误'}"

            End If

        Catch ex As Exception
            strJsonLoad = "{'GotError':1,'ErrorDsp':'处理题目过程出错，请呼叫网管。 '}"

        End Try

        context.Response.Write(strJsonLoad)


    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class