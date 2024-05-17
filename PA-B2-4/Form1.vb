Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim form2 As New Form2
        form2.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim formSup As New FormSupplier
        formSup.Show()
        Me.Close()
    End Sub

    Private Sub btnLaporan_Click(sender As Object, e As EventArgs) Handles btnLaporan.Click
        Dim formLaporan As New FormLaporanSupplier
        formLaporan.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim formLogin As New FormLogin()
        formLogin.Show()
        Me.Close()
    End Sub

    'Private Sub Button3_Click(sender As Object, e As EventArgs)
    '    Dim formPembelian As New FormPembelian()
    '    formPembelian.Show()
    '    Me.Close()
    'End Sub
End Class
