﻿Imports CommonLibrary

Partial Class AfterLife_NeuralNetworks_NNCheck
    Inherits System.Web.UI.Page
    Dim clCommon As CommonLibrary = New CommonLibrary

    Private Sub AfterLife_NeuralNetworks_NNCheck_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim hcToken As HttpCookie

        hcToken = New HttpCookie("TokenForNN")
        hcToken.Values.Add("Token", clCommon.StringTranscodingToMD5(Date.Now.ToShortDateString))
        hcToken.Expires = DateTime.MaxValue
        Response.AppendCookie(hcToken)
        Response.Write("")


    End Sub
End Class
