Imports PageMessage
Imports CommonLibrary
Imports UserLibrary
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage


    Dim pmMsgClass As PageMessage = New PageMessage
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim clCommon As CommonLibrary = New CommonLibrary


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim bolIsGoInside As Boolean
        If Session("PowerLevel") IsNot Nothing Then
            If Session("PowerLevel") = "0" Then
                lblUserName.Text = "游客"
                lbExit.Text = "登陆/注册"

            End If
        End If

        If Not IsPostBack Then
            bolIsGoInside = ucUserManage.GetInOrGetOut(Session, Request.Cookies.Get("SanShiUserInfo"), Request.UserAgent.ToString, Application)
            If Not bolIsGoInside Then
                lblUserName.Text = "游客"
                Session("PowerLevel") = "0"
                Session("SanShiUserName") = "游客"
                lbExit.Text = "登陆/注册"
            Else
                lblUserName.Text = Session("SanShiUserName")

            End If
        End If

    End Sub

    Protected Sub lbExit_Click(sender As Object, e As EventArgs) Handles lbExit.Click
        If ((Session("SanShiUserName") <> Nothing) Or (Session("PowerLevel") <> Nothing)) Then

            Session("SanShiUserName") = Nothing
            Session("PowerLevel") = Nothing

        End If
        If (Response.Cookies("SanShiUserInfo") Is Nothing) Then
            Response.Cookies("SanShiUserInfo").Value = Nothing

        End If

        Response.Redirect("/Login.aspx")

    End Sub


End Class

