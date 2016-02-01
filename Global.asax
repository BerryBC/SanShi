<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
        If ((Session("SanShiUserName") <> Nothing) Or (Session("PowerLevel") <> Nothing)) Then
            Session("SanShiUserName") = Nothing
            Session("PowerLevel") = Nothing
        End If

    End Sub


    'Public Function GetInOrGetOut() As Boolean
    '    If (Session("SanShiUserName") <> Nothing) Then
    '        If (Session("SanShiUserName").ToString <> "") Then
    '            Return True
    '        End If
    '    End If
    '    If Response.Cookies("UserInfo")("SanShiUserName") <> Nothing Then
    '        '有登陆Cookies的就登陆登陆用户


    '    End If

    '    Return False
    'End Function

</script>