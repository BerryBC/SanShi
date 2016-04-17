
Partial Class ThisLife_MultipleLinearRegression_CountMultipleLinearRegression
    Inherits System.Web.UI.Page

    Private Sub btnGoCount_Click(sender As Object, e As EventArgs) Handles btnGoCount.Click
        Dim doubleBasicDataOfX(,) As Double
        Dim doubleBasicDataOfY() As Double
        Dim doubleOriPhalanx(,) As Double
        Dim doubleOriPhalanxBackup(,) As Double
        Dim doubleOriPhalanxResults(,) As Double
        Dim doubleInversePhalanx(,) As Double
        Dim doublePhalanxOfY() As Double
        Dim doubleCoefficient() As Double
        Dim strBasicDataStr As String
        Dim strEveryLine() As String
        Dim strEveryEle() As String
        Dim intOrder As Integer
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim intHowMuchLine As Integer
        Dim doubletmpEle As Double
        Dim doubleSquareE As Double

        strBasicDataStr = txtBasicData.Text
        strBasicDataStr = strBasicDataStr.Replace(Chr(10) & Chr(10), vbCrLf)
        strBasicDataStr = strBasicDataStr.Replace(Chr(10), vbCrLf)
        strBasicDataStr = strBasicDataStr.Replace(vbCrLf & vbCrLf, vbCrLf)
        If Asc(Right(strBasicDataStr, 1)) = 10 Then
            strBasicDataStr = strBasicDataStr.Substring(0, Len(strBasicDataStr) - 1)
        End If
        strEveryLine = Split(strBasicDataStr, vbCrLf)
        intOrder = Split(strEveryLine(0), ",").Count - 1
        intHowMuchLine = strEveryLine.Count - 1
        ReDim doubleBasicDataOfX(intHowMuchLine, intOrder)
        ReDim doubleBasicDataOfY(intHowMuchLine)
        ReDim doubleOriPhalanx(intOrder, intOrder)
        ReDim doublePhalanxOfY(intOrder)
        ReDim doubleInversePhalanx(intOrder, intOrder)
        ReDim doubleOriPhalanxResults(intOrder, intOrder)
        ReDim doubleOriPhalanxBackup(intOrder, intOrder)
        ReDim doubleCoefficient(intOrder)


        txtResults.Text = ""

        For i = 0 To intHowMuchLine
            doubleBasicDataOfX(i, 0) = 1
        Next
        For i = 0 To intHowMuchLine
            strEveryEle = Split(strEveryLine(i), ",")
            If IsNumeric(strEveryEle(0)) Then
                doubleBasicDataOfY(i) = CType(strEveryEle(0), Double)
            Else
                doubleBasicDataOfY(i) = 0
            End If
            For j = 1 To intOrder
                If j < strEveryEle.Count Then
                    If IsNumeric(strEveryEle(j)) Then
                        doubleBasicDataOfX(i, j) = CType(strEveryEle(j), Double)
                    Else
                        doubleBasicDataOfX(i, j) = 0
                    End If
                Else
                    doubleBasicDataOfX(i, j) = 0
                End If
            Next
        Next

        '输出刚输入的矩阵
        txtResults.Text += "---------以下为原始矩阵--------------------------" & vbCrLf
        For i = 0 To intHowMuchLine
            txtResults.Text += doubleBasicDataOfY(i).ToString & ","
            For j = 0 To intOrder
                txtResults.Text += doubleBasicDataOfX(i, j).ToString & ","
            Next
            txtResults.Text += vbCrLf
        Next

        '根本原始矩阵求出上三角矩阵
        For i = 0 To intOrder
            For j = i To intOrder
                doubleOriPhalanx(i, j) = 0
                For k = 0 To intHowMuchLine
                    doubleOriPhalanx(i, j) += doubleBasicDataOfX(k, i) * doubleBasicDataOfX(k, j)
                Next
            Next
        Next

        '输出上三角矩阵
        txtResults.Text += "----------以下为求和后的上三角矩阵--------------------" & vbCrLf
        For i = 0 To intOrder
            For j = 0 To intOrder
                txtResults.Text += doubleOriPhalanx(i, j) & ","
            Next
            txtResults.Text += vbCrLf
        Next


        '计算Y与X的相乘矩阵
        For i = 0 To intOrder
            doublePhalanxOfY(i) = 0
            For k = 0 To intHowMuchLine
                doublePhalanxOfY(i) += doubleBasicDataOfX(k, i) * doubleBasicDataOfY(k)
            Next
        Next

        '输出Y与X的相乘矩阵
        txtResults.Text += "-----------输出Y与X的总和矩阵----------------------" & vbCrLf
        For i = 0 To intOrder
            txtResults.Text += doublePhalanxOfY(i) & ","
        Next

        txtResults.Text += vbCrLf

        '补充矩阵完整性
        For i = 1 To intOrder
            For j = 0 To i
                doubleOriPhalanx(i, j) = doubleOriPhalanx(j, i)
            Next
        Next

        '输出补充完整后的矩阵
        txtResults.Text += "-----------------------------------" & vbCrLf
        For i = 0 To intOrder
            For j = 0 To intOrder
                txtResults.Text += doubleOriPhalanx(i, j) & ","
            Next
            txtResults.Text += vbCrLf
        Next


        For i = 0 To intOrder
            For j = 0 To intOrder
                doubleOriPhalanxBackup(i, j) = doubleOriPhalanx(i, j)
            Next
        Next


        '先生成单位矩阵
        For i = 0 To intOrder
            For j = 0 To intOrder
                doubleInversePhalanx(i, j) = 0
            Next
            doubleInversePhalanx(i, i) = 1
        Next

        '输出单位矩阵
        txtResults.Text += "------------以下输出单位矩阵-----------------------" & vbCrLf
        For i = 0 To intOrder
            For j = 0 To intOrder
                txtResults.Text += doubleInversePhalanx(i, j) & ","
            Next
            txtResults.Text += vbCrLf
        Next

        '计算逆阵
        For i = 0 To intOrder
            j = i
            '保证对角线上非0
            If doubleOriPhalanx(i, i) = 0 Then
                '如果对角线上元素为0即跟后面同行其他非零元素所在行对调
                For j = i + 1 To intOrder + 1
                    If j <= intOrder Then
                        If doubleOriPhalanx(i, j) ^ 2 > 0 Then
                            Exit For
                        End If
                    End If
                Next
                '如果存在同行其他非零元素，对调
                If j > intOrder Then
                    txtResults.Text += "该矩阵无解" & vbCrLf
                    Exit Sub
                Else
                    For k = 0 To intOrder
                        doubletmpEle = doubleOriPhalanx(k, i)
                        doubleOriPhalanx(k, i) = doubleOriPhalanx(k, j)
                        doubleOriPhalanx(k, j) = doubletmpEle

                        doubletmpEle = doubleInversePhalanx(k, i)
                        doubleInversePhalanx(k, i) = doubleInversePhalanx(k, j)
                        doubleInversePhalanx(k, j) = doubletmpEle
                    Next
                End If
            End If
            '如果对角线上元素非0
            For j = i + 1 To intOrder
                If doubleOriPhalanx(i, j) ^ 2 > 0 Then
                    doubletmpEle = doubleOriPhalanx(i, j)
                    For k = 0 To intOrder
                        doubleOriPhalanx(k, j) = doubleOriPhalanx(k, j) / doubletmpEle * doubleOriPhalanx(i, i) - doubleOriPhalanx(k, i)
                        doubleInversePhalanx(k, j) = doubleInversePhalanx(k, j) / doubletmpEle * doubleOriPhalanx(i, i) - doubleInversePhalanx(k, i)
                    Next
                End If
            Next
        Next
        If doubleOriPhalanx(intOrder, intOrder) = 0 Then
            txtResults.Text += "该矩阵无解" & vbCrLf
            Exit Sub
        Else
            For i = intOrder To 0 Step -1
                doubletmpEle = doubleOriPhalanx(i, i)
                doubleOriPhalanx(i, i) = 1
                For j = 0 To intOrder
                    doubleInversePhalanx(j, i) = doubleInversePhalanx(j, i) / doubletmpEle
                Next
                For j = i - 1 To 0 Step -1
                    If doubleOriPhalanx(i, j) ^ 2 > 0 Then
                        doubletmpEle = doubleOriPhalanx(i, j)
                        For k = 0 To intOrder
                            doubleOriPhalanx(k, j) = doubleOriPhalanx(k, j) / doubletmpEle - doubleOriPhalanx(k, i)
                            doubleInversePhalanx(k, j) = doubleInversePhalanx(k, j) / doubletmpEle - doubleInversePhalanx(k, i)
                        Next
                    End If
                Next
            Next
        End If
        doubletmpEle = doubleOriPhalanx(0, 0)
        doubleOriPhalanx(0, 0) = 1
        For k = 0 To intOrder
            doubleInversePhalanx(k, 0) = doubleInversePhalanx(k, 0) / doubletmpEle
        Next

        '输出求逆后的原始数据矩阵
        txtResults.Text += "------------以下输出求逆后的原始数据矩阵-----------------------" & vbCrLf
        For i = 0 To intOrder
            For j = 0 To intOrder
                txtResults.Text += doubleOriPhalanx(i, j) & ","
            Next
            txtResults.Text += vbCrLf
        Next

        '输出求逆后的逆矩阵
        txtResults.Text += "------------以下输出求逆后的逆矩阵-----------------------" & vbCrLf
        For i = 0 To intOrder
            For j = 0 To intOrder
                txtResults.Text += doubleInversePhalanx(i, j) & ","
            Next
            txtResults.Text += vbCrLf
        Next

        '逆阵乘以原阵结果
        For i = 0 To intOrder
            For j = 0 To intOrder
                doubleOriPhalanxResults(i, j) = 0
                For k = 0 To intOrder
                    doubleOriPhalanxResults(i, j) += doubleOriPhalanxBackup(i, k) * doubleInversePhalanx(k, j)

                Next
            Next

        Next
        '验证逆阵
        txtResults.Text += "------------以下输出验证逆阵-----------------------" & vbCrLf
        For i = 0 To intOrder
            For j = 0 To intOrder
                txtResults.Text += doubleOriPhalanxResults(i, j) & ","
            Next
            txtResults.Text += vbCrLf
        Next

        For i = 0 To intOrder
            doubleCoefficient(i) = 0
            For j = 0 To intOrder
                doubleCoefficient(i) += doubleInversePhalanx(i, j) * doublePhalanxOfY(j)
            Next
        Next
        txtResults.Text += "------------以下系数结果-----------------------" & vbCrLf
        For i = 0 To intOrder
            txtResults.Text += doubleCoefficient(i) & ","
        Next
        txtResults.Text += vbCrLf
        txtResults.Text += "-----------------------------------" & vbCrLf
        txtResults.Text += "Order为:" & intOrder + 1 & vbCrLf

        For i = 0 To intHowMuchLine
            doubletmpEle = 0
            For j = 0 To intOrder
                doubletmpEle = doubleBasicDataOfX(j, i) * doubleCoefficient(i)
            Next
        Next

    End Sub

    Private Sub ThisLife_MultipleLinearRegression_CountMultipleLinearRegression_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtResults.CssClass = "form-control"
            btnGoCount.CssClass = "btn btn-warning  form-control"
        End If
    End Sub
End Class
