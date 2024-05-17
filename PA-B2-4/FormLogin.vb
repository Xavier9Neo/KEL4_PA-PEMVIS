Imports MySql.Data.MySqlClient

Public Class FormLogin
    Private connectionString As String = "server=localhost;user=root;password=;database=harrypotter"

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim username As String = tbUsername.Text.Trim()
        Dim password As String = tbPassword.Text.Trim()

        If Not String.IsNullOrEmpty(username) AndAlso Not String.IsNullOrEmpty(password) Then
            If username.ToLower() = "admin" AndAlso password = "123" Then
                MessageBox.Show("Anda berhasil login sebagai admin", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ' Lanjutkan ke Form1 sebagai admin
                Dim form1 As New Form1()
                form1.Show()
                Me.Hide()
            Else
                Dim conn As New MySqlConnection(connectionString)
                Dim query As String = "SELECT * FROM login WHERE Username=@username AND Password=@password"

                Try
                    conn.Open()
                    Dim cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@username", username)
                    cmd.Parameters.AddWithValue("@password", password)

                    Dim reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        MessageBox.Show("Anda berhasil login", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ' Jika login berhasil, lanjutkan ke Form1
                        Dim formPembelian As New FormPembelian()
                        formPembelian.Show()
                        Me.Hide()
                    Else
                        MessageBox.Show("Username atau password salah!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    conn.Close()
                End Try
            End If
        Else
            MessageBox.Show("Username atau password tidak boleh kosong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        tbUsername.Focus()
        tbUsername.Select()
    End Sub

    Private Sub btnBelum_Click(sender As Object, e As EventArgs) Handles btnBelum.Click
        ' Ketika tombol registrasi diklik, buat instance dari form registrasi dan tampilkan
        Dim formRegistrasi As New FormRegis()
        formRegistrasi.Show()
        Me.Hide()
    End Sub

    Private Sub tbUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbUsername.KeyPress
        If Not Char.IsLetter(e.KeyChar) And e.KeyChar <> " " And e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub tbPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbPassword.KeyPress
        If Not Char.IsDigit(e.KeyChar) And e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            tbPassword.PasswordChar = ""
        Else
            tbPassword.PasswordChar = "*"
        End If
    End Sub

End Class