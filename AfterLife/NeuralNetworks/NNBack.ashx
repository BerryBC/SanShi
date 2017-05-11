<%@ WebHandler Language="VB" Class="NNBack" %>

Imports System
Imports System.Web
Imports CommonLibrary
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class NNBack : Implements IHttpHandler
    Dim clCommon As CommonLibrary = New CommonLibrary

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest


        Dim hccToken As HttpCookieCollection
        Dim hcToken As HttpCookie

        Dim strJsonLoad As String

        hccToken = context.Request.Cookies
        hcToken = hccToken("TokenForNN")
        If (hcToken IsNot Nothing) Then

            If (hcToken("Token") = clCommon.StringTranscodingToMD5(Date.Now.ToShortDateString)) Then

                strJsonLoad = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("/AfterLife/NeuralNetworks/Config/DataJSON.json"))


                strJsonLoad = "{""GotError"":0," & Right(strJsonLoad, Len(strJsonLoad) - 1)
                context.Response.ContentType = "text/plain"
                context.Response.Write(strJsonLoad)
            Else
                context.Response.Write("{'GotError':1,'ErrorDsp':'验证错误'}")
            End If
        Else
            context.Response.Write("{'GotError':2,'ErrorDsp':'没有验证'}")

        End If
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class