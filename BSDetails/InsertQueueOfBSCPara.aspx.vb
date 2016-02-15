Imports System.Data

Partial Class BSDetails_InsertQueueOfBSCPara
    Inherits System.Web.UI.Page
    Dim ucUserManage As UserLibrary = New UserLibrary
    Dim bsdlCommonLibrary As BaseSationDetailsLibrary = New BaseSationDetailsLibrary

    Private Sub BSDetails_InsertQueueOfBSCPara_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim bolIsPowerEnough As Boolean = False
        Try


            bolIsPowerEnough = ucUserManage.CheckPower(Session, 9, Response)
            If Not IsPostBack Then


                BindConfigData()



            End If





        Catch ex As Exception

        End Try

    End Sub

    Private Sub BindConfigData()

        Dim dtBaseSationDetailsMana As DataTable
        Dim drEveryBaseSationDetailsConfig As DataRow

        Try

            dtBaseSationDetailsMana = bsdlCommonLibrary.ReturnBaseSationDetailsMan()

            For Each drEveryBaseSationDetailsConfig In dtBaseSationDetailsMana.Rows
                cblWhichToInsert.Items.Add(drEveryBaseSationDetailsConfig.Item("ConfigName"))
            Next

            cblWhichToInsert.DataBind()
        Catch ex As Exception

        End Try

    End Sub




End Class
