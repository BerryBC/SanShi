
Partial Class KnowTheFate_LTELittleTest_LTELittleTest

    Inherits System.Web.UI.Page
    Dim utToken As UploadToken = New UploadToken

    Private Sub KnowTheFate_LTELittleTest_LTELittleTest_Load(sender As Object, e As EventArgs) Handles Me.Load
        utToken.AddToken(Response)

    End Sub
End Class
