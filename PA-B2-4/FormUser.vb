Public Class FormUser

    Private Sub btnPesan_Click(sender As Object, e As EventArgs) Handles btnPesan.Click
        Dim formpembelian As New FormPembelian
        formpembelian.Show()
        Me.Close()
    End Sub
End Class