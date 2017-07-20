
Partial Class ThisLife_CloudDisk_CDUploadPage
    Inherits System.Web.UI.Page
    Dim utToken As UploadToken = New UploadToken

    Private Sub ThisLife_CloudDisk_CDUploadPage_Load(sender As Object, e As EventArgs) Handles Me.Load


        utToken.AddToken(Response)


    End Sub
End Class
