
Partial Class Main
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("PowerLevel") IsNot Nothing Then
                If CType(Session("PowerLevel"), Integer) >= 9 Then
                    plMana.Visible = True


                End If
            End If
        End If




    End Sub
End Class
