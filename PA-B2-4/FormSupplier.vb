Imports MySql.Data.MySqlClient

Public Class FormSupplier
    ' Connection string for MySQL
    Private Const ConnectionString As String = "server=localhost;user=root;database=harrypotter"

    ' Method to clear input fields
    Private Sub Kosong()
        txtId.Clear()
        txtNama.Clear()
        txtAlamat.Clear()
    End Sub

    ' Method to display suppliers in DataGridView
    Private Sub TampilSupplier()
        DataGridView1.Rows.Clear()

        Using CONN As New MySqlConnection(ConnectionString)
            Try
                CONN.Open()
                Using CMD As New MySqlCommand("SELECT * FROM supplier ORDER BY id_supplier", CONN)
                    Using RD As MySqlDataReader = CMD.ExecuteReader()
                        While RD.Read()
                            Dim row As New DataGridViewRow()
                            row.CreateCells(DataGridView1)
                            row.Cells(0).Value = RD("id_supplier")
                            row.Cells(1).Value = RD("nama_supplier")
                            row.Cells(2).Value = RD("Alamat")
                            DataGridView1.Rows.Add(row)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' Method to validate input
    Private Function IsInputValid() As Boolean
        If String.IsNullOrEmpty(txtNama.Text) Then
            MessageBox.Show("Nama Supplier tidak boleh kosong.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function

    ' Load event handler
    Private Sub FormUpdateSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilSupplier()
        Kosong()
        txtId.ReadOnly = True
    End Sub

    ' Method to get the next available ID
    Private Function GetNextID() As Integer
        Using CONN As New MySqlConnection(ConnectionString)
            Try
                CONN.Open()
                Using CMD As New MySqlCommand("SELECT IFNULL(MAX(id_supplier), 0) + 1 AS NextID FROM supplier", CONN)
                    Return Convert.ToInt32(CMD.ExecuteScalar())
                End Using
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return -1
            End Try
        End Using
    End Function

    ' Add button click event handler
    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If IsInputValid() Then
            Using CONN As New MySqlConnection(ConnectionString)
                Try
                    CONN.Open()
                    Dim nextId As Integer = GetNextID()
                    If nextId = -1 Then Return

                    Using CMD As New MySqlCommand("INSERT INTO supplier (id_supplier, nama_supplier, Alamat) VALUES (@id, @nama, @alamat)", CONN)
                        CMD.Parameters.AddWithValue("@id", nextId)
                        CMD.Parameters.AddWithValue("@nama", txtNama.Text)
                        CMD.Parameters.AddWithValue("@alamat", txtAlamat.Text)
                        CMD.ExecuteNonQuery()
                    End Using
                    MessageBox.Show("Simpan Data Sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TampilSupplier()
                    Kosong()
                Catch ex As Exception
                    MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End If
    End Sub

    ' Update button click event handler
    Private Sub btnUbah_Click(sender As Object, e As EventArgs) Handles btnUbah.Click
        If String.IsNullOrEmpty(txtId.Text) Then
            MessageBox.Show("ID Supplier belum diisi", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtId.Focus()
            Return
        End If

        Using CONN As New MySqlConnection(ConnectionString)
            Try
                CONN.Open()
                Using CMD As New MySqlCommand("SELECT COUNT(*) FROM supplier WHERE id_supplier = @id", CONN)
                    CMD.Parameters.AddWithValue("@id", txtId.Text)
                    Dim count As Integer = Convert.ToInt32(CMD.ExecuteScalar())
                    If count = 0 Then
                        MessageBox.Show("ID Supplier tidak ditemukan", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If
                End Using

                Using CMD As New MySqlCommand("UPDATE supplier SET nama_supplier = @nama, Alamat = @alamat WHERE id_supplier = @id", CONN)
                    CMD.Parameters.AddWithValue("@nama", txtNama.Text)
                    CMD.Parameters.AddWithValue("@alamat", txtAlamat.Text)
                    CMD.Parameters.AddWithValue("@id", txtId.Text)
                    CMD.ExecuteNonQuery()
                End Using
                MessageBox.Show("Data berhasil diubah!", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TampilSupplier()
                Kosong()
            Catch ex As Exception
                MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' Delete button click event handler
    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If String.IsNullOrEmpty(txtId.Text) Then
            MessageBox.Show("ID Supplier belum diisi", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtId.Focus()
            Return
        End If

        Using CONN As New MySqlConnection(ConnectionString)
            Try
                CONN.Open()
                Using CMD As New MySqlCommand("SELECT COUNT(*) FROM supplier WHERE id_supplier = @id", CONN)
                    CMD.Parameters.AddWithValue("@id", txtId.Text)
                    Dim count As Integer = Convert.ToInt32(CMD.ExecuteScalar())
                    If count = 0 Then
                        MessageBox.Show("ID Supplier tidak ditemukan", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If
                End Using

                Using CMD As New MySqlCommand("DELETE FROM supplier WHERE id_supplier = @id", CONN)
                    CMD.Parameters.AddWithValue("@id", txtId.Text)
                    CMD.ExecuteNonQuery()
                End Using
                MessageBox.Show("Data berhasil dihapus", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TampilSupplier()
                Kosong()
            Catch ex As Exception
                MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' DataGridView cell click event handler
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            txtId.Text = row.Cells(0).Value.ToString()
            txtNama.Text = row.Cells(1).Value.ToString()
            txtAlamat.Text = row.Cells(2).Value.ToString()
        End If
    End Sub

    ' Back button click event handler
    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Dim form1 As New Form1()
        form1.Show()
        Me.Close()
    End Sub

    ' Cancel button click event handler
    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Kosong()
    End Sub

End Class