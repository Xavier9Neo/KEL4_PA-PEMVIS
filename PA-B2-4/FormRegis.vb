Imports MySql.Data.MySqlClient

Public Class FormRegis
    Private connectionString As String = "server=localhost;user=root;password=;database=harrypotter"

    Private Sub btnRegis_Click(sender As Object, e As EventArgs) Handles btnRegis.Click
        ' Logika untuk proses registrasi
        Dim username As String = tbUsername.Text.Trim()
        Dim password As String = tbPassword.Text.Trim()

        ' Validasi input kosong
        If String.IsNullOrEmpty(username) OrElse String.IsNullOrEmpty(password) Then
            MessageBox.Show("Username dan Password harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Membuat koneksi
        Using conn As New MySqlConnection(connectionString)
            conn.Open()

            ' Memulai transaksi
            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try
                ' Query untuk memeriksa apakah username sudah terdaftar
                Dim queryCheckUsername As String = "SELECT COUNT(*) FROM login WHERE Username = @username"

                ' Membuat command untuk queryCheckUsername
                Using cmdCheckUsername As New MySqlCommand(queryCheckUsername, conn, transaction)
                    cmdCheckUsername.Parameters.AddWithValue("@username", username)

                    ' Melakukan eksekusi scalar untuk memeriksa apakah username sudah terdaftar
                    Dim countUsername As Integer = Convert.ToInt32(cmdCheckUsername.ExecuteScalar())

                    ' Jika username sudah terdaftar, tampilkan pesan
                    If countUsername > 0 Then
                        MessageBox.Show("Username sudah terdaftar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End Using

                ' Query untuk menambahkan data user baru ke database
                Dim queryInsertUser As String = "INSERT INTO login (Username, Password) VALUES (@username, @password)"

                ' Membuat command untuk queryInsertUser
                Using cmdInsertUser As New MySqlCommand(queryInsertUser, conn, transaction)
                    cmdInsertUser.Parameters.AddWithValue("@username", username)
                    cmdInsertUser.Parameters.AddWithValue("@password", password)

                    ' Menjalankan query untuk menambahkan user baru ke database
                    Dim rowsAffected As Integer = cmdInsertUser.ExecuteNonQuery()

                    ' Jika berhasil menambahkan user baru, tampilkan pesan sukses
                    If rowsAffected > 0 Then
                        MessageBox.Show("Registrasi berhasil!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ' Commit transaksi
                        transaction.Commit()

                        ' Setelah registrasi berhasil, tampilkan Form Pembelian
                        Dim formuser As New FormUser()
                        formuser.Show()
                        Me.Hide() ' Sembunyikan form registrasi setelah berhasil mendaftar
                    End If
                End Using
            Catch ex As Exception
                ' Rollback transaksi jika terjadi kesalahan
                transaction.Rollback()
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub FormRegis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Dim formLogin As New FormLogin()
        formLogin.Show()
        Me.Close()
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
