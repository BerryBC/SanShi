
Partial Class ThisLife_DistanceOfTwoSpot_DistanceOfTwoSpot
    Inherits System.Web.UI.Page

    Private Sub ThisLife_DistanceOfTwoSpot_DistanceOfTwoSpot_Load(sender As Object, e As EventArgs) Handles Me.Load
        If CType(Session("PowerLevel"), Integer) < 9 Then
            Response.Redirect("/JustBuilding.aspx", False)

        End If

    End Sub
End Class
