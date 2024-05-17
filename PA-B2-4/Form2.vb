Imports MySql.Data.MySqlClient
Public Class Form2
    Dim CONN As MySqlConnection
    Dim CMD As MySqlCommand
    Dim RD As MySqlDataReader

    'Dim dataTable As New DataTable()
    Dim dataBarang As New DataTable()
    Dim supplierId As String

    ' Metode untuk menginisialisasi koneksi
    Sub koneksi()
        CONN = New MySqlConnection("server=localhost;user=root;password=;database=harrypotter")
        CONN.Open()
    End Sub

    ' Metode untuk menutup koneksi
    Sub tutupKoneksi()
        If CONN IsNot Nothing AndAlso CONN.State = ConnectionState.Open Then
            CONN.Close()
        End If
    End Sub

    Sub Kosong()
        txtId.Clear()
        txtNama.Clear()
        txtHarga.Clear()
        DateTimePicker1.Value = DateTime.Now
        cbSupplier.SelectedIndex = -1
        NumericUpDown1.Value = 1 ' Atur nilai NumericUpDown1 menjadi 1
    End Sub

    Sub isi()
        txtNama.Clear()
        txtNama.Focus()
    End Sub

    Sub aturGrid()
        If DataGridView1.Columns.Count >= 6 Then
            DataGridView1.Columns(0).Width = 60
            DataGridView1.Columns(1).Width = 100
            DataGridView1.Columns(2).Width = 60
            DataGridView1.Columns(3).Width = 60
            DataGridView1.Columns(4).Width = 150
            DataGridView1.Columns(5).Width = 100
            DataGridView1.Columns(0).HeaderText = "Id Barang"
            DataGridView1.Columns(1).HeaderText = "Nama Barang"
            DataGridView1.Columns(2).HeaderText = "Stok Barang"
            DataGridView1.Columns(3).HeaderText = "Harga Barang"
            DataGridView1.Columns(4).HeaderText = "Tanggal Ditambahkan"
            DataGridView1.Columns(5).HeaderText = "Nama Supplier"

        Else
            MsgBox("Jumlah kolom pada DataGridView tidak mencukupi.")
        End If
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        FillSupplierComboBox()
        LoadData()
        Kosong()
    End Sub

    Private Sub LoadData()
        CMD = New MySqlCommand("SELECT barang.idbarang, barang.namabarang, barang.stokbarang, barang.hargabarang, barang.tanggal, supplier.nama_supplier FROM barang INNER JOIN supplier ON barang.id_supplier = supplier.id_supplier", CONN)
        RD = CMD.ExecuteReader()
        DataGridView1.Rows.Clear()

        While RD.Read()
            Dim row As String() = {RD("idbarang").ToString(), RD("namabarang").ToString(), RD("stokbarang").ToString(), RD("hargabarang").ToString(), RD("tanggal").ToString(), RD("nama_supplier").ToString()}
            DataGridView1.Rows.Add(row)
        End While

        RD.Close()
    End Sub


    ' Metode untuk mengisi ComboBox Pemasok
    Public Sub FillSupplierComboBox()
        cbSupplier.Items.Clear()

        CMD = New MySqlCommand("SELECT * FROM supplier", CONN)
        RD = CMD.ExecuteReader()
        While RD.Read()
            Dim namaSupplier As String = RD("nama_supplier").ToString()
            cbSupplier.Items.Add(namaSupplier)
        End While

        RD.Close()
    End Sub

  Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        ' Memeriksa apakah semua input telah diisi
        If String.IsNullOrWhiteSpace(txtNama.Text) OrElse String.IsNullOrWhiteSpace(NumericUpDown1.Text) OrElse String.IsNullOrWhiteSpace(txtHarga.Text) OrElse String.IsNullOrWhiteSpace(DateTimePicker1.Text) Then
            MsgBox("Input tidak boleh kosong!", MsgBoxStyle.Exclamation, "Error")
        Else
            ' Memeriksa apakah pemasok yang dipilih dari combo box sudah ada
            CMD = New MySqlCommand("SELECT id_supplier FROM supplier WHERE nama_supplier = @namaSupplier", CONN)
            CMD.Parameters.AddWithValue("@namaSupplier", cbSupplier.SelectedItem.ToString())
            Dim supplierId As String = CMD.ExecuteScalar().ToString()

            If Not String.IsNullOrEmpty(supplierId) Then
                ' Menambahkan data barang baru ke tabel barang
                CMD = New MySqlCommand("INSERT INTO barang (namabarang, stokbarang, hargabarang, tanggal, id_supplier) VALUES (@namaBarang, @stokBarang, @hargaBarang, @tanggalBarang, @supplierId)", CONN)
                CMD.Parameters.AddWithValue("@namaBarang", txtNama.Text)
                CMD.Parameters.AddWithValue("@stokBarang", Convert.ToInt32(NumericUpDown1.Text))
                CMD.Parameters.AddWithValue("@hargaBarang", Convert.ToDouble(txtHarga.Text))
                CMD.Parameters.AddWithValue("@tanggalBarang", DateTimePicker1.Value)
                CMD.Parameters.AddWithValue("@supplierId", supplierId)

                CMD.ExecuteNonQuery()

                ' Memuat ulang data ke DataGridView setelah menambahkan data baru
                LoadData()

                ' Kosongkan form setelah data berhasil ditambahkan
                Kosong()
                MsgBox("Simpan Data Sukses!", MsgBoxStyle.Exclamation, "Berhasil")
                txtId.Focus()
                NumericUpDown1.Value = 1

            Else
                MsgBox("Pemasok tidak ditemukan!", MsgBoxStyle.Exclamation, "Error")
            End If
        End If
    End Sub


    Private Sub txtNama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNama.KeyPress
        txtNama.MaxLength = 50
        If e.KeyChar = Chr(13) Then
            txtNama.Text = UCase(txtNama.Text)
        End If
    End Sub

    Private Sub btnUbah_Click(sender As Object, e As EventArgs) Handles btnUbah.Click
        If txtId.Text = "" Then
            MsgBox("Id Barang belum diisi")
            txtId.Focus()
        Else
            ' Periksa apakah ID barang yang diubah ada di database
            CMD = New MySqlCommand("SELECT COUNT(*) FROM barang WHERE idbarang = @idBarang", CONN)
            CMD.Parameters.AddWithValue("@idBarang", txtId.Text)
            Dim idCount As Integer = Convert.ToInt32(CMD.ExecuteScalar())

            ' Jika ID barang ditemukan, lanjutkan dengan proses update
            If idCount > 0 Then
                ' Lanjutkan dengan proses update tanpa mengubah ID barang
                Dim ubah As String = "UPDATE barang SET namabarang = @namaBarang, stokbarang = @stokBarang, hargabarang = @hargaBarang, tanggal = @tanggalBarang WHERE idbarang = @idBarang"
                CMD = New MySqlCommand(ubah, CONN)
                CMD.Parameters.AddWithValue("@idBarang", txtId.Text)
                CMD.Parameters.AddWithValue("@namaBarang", txtNama.Text)
                CMD.Parameters.AddWithValue("@stokBarang", Convert.ToInt32(NumericUpDown1.Value))
                CMD.Parameters.AddWithValue("@hargaBarang", Convert.ToDouble(txtHarga.Text))
                CMD.Parameters.AddWithValue("@tanggalBarang", DateTimePicker1.Value)
                CMD.ExecuteNonQuery()
                MsgBox("Data berhasil diubah!", MsgBoxStyle.Exclamation, "Berhasil")

                ' Kosongkan elemen UI terkait data barang
                Kosong()

                ' Isi ulang ComboBox Pemasok dengan data aktual
                FillSupplierComboBox()

                ' Set nilai NumericUpDown kembali ke nilai awal
                NumericUpDown1.Value = 1

                ' Kosongkan DataGridView
                DataGridView1.Rows.Clear()

                ' Tampilkan kembali data dari database di DataGridView
                CMD = New MySqlCommand("SELECT barang.idbarang, barang.namabarang, barang.stokbarang, barang.hargabarang, barang.tanggal, supplier.nama_supplier FROM barang INNER JOIN supplier ON barang.id_supplier = supplier.id_supplier", CONN)
                RD = CMD.ExecuteReader()
                While RD.Read()
                    Dim row As String() = {RD("idbarang").ToString(), RD("namabarang").ToString(), RD("stokbarang").ToString(), RD("hargabarang").ToString(), RD("tanggal").ToString(), RD("nama_supplier").ToString()}
                    DataGridView1.Rows.Add(row)
                End While
                RD.Close()
            Else
                ' Jika ID barang tidak ditemukan, tampilkan pesan kesalahan "ID tidak dapat diubah"
                MsgBox("ID tidak dapat diubah!", MsgBoxStyle.Exclamation, "Error")
            End If
        End If
    End Sub

Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If String.IsNullOrWhiteSpace(txtId.Text) Then
            MsgBox("Id Barang belum diisi")
            txtId.Focus()
        Else
            CMD = New MySqlCommand("DELETE FROM barang WHERE idbarang = @idBarang", CONN)
            CMD.Parameters.AddWithValue("@idBarang", txtId.Text)
            CMD.ExecuteNonQuery()
            MsgBox("Data berhasil dihapus", MsgBoxStyle.Exclamation, "Berhasil")
            Kosong()
            LoadData()
        End If
    End Sub


    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Kosong()
        FillSupplierComboBox()
        NumericUpDown1.Value = 1
        LoadData()
    End Sub


    Private Sub txtCari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCari.KeyPress
        ' Tombol Enter ditekan
        If e.KeyChar = Chr(13) Then
            If Not String.IsNullOrWhiteSpace(txtCari.Text) Then
                Dim idBarang As String = txtCari.Text
                Dim query As String = "SELECT barang.idbarang, barang.namabarang, barang.stokbarang, barang.hargabarang, barang.tanggal, supplier.nama_supplier FROM barang INNER JOIN supplier ON barang.id_supplier = supplier.id_supplier WHERE barang.idbarang = '" & idBarang & "'"
                DA = New MySqlDataAdapter(query, CONN)
                DS = New DataSet
                DA.Fill(DS, "searchResult")

                ' Hapus kolom yang ada di DataGridView
                DataGridView1.Columns.Clear()

                ' Tambahkan hasil pencarian ke DataGridView
                DataGridView1.DataSource = DS.Tables("searchResult")
                DataGridView1.Refresh()

                ' Atur lebar dan header kolom sesuai kebutuhan
                DataGridView1.Columns(0).Width = 60
                DataGridView1.Columns(1).Width = 100
                DataGridView1.Columns(2).Width = 60
                DataGridView1.Columns(3).Width = 60
                DataGridView1.Columns(4).Width = 150
                DataGridView1.Columns(5).Width = 100
                DataGridView1.Columns(0).HeaderText = "Id Barang"
                DataGridView1.Columns(1).HeaderText = "Nama Barang"
                DataGridView1.Columns(2).HeaderText = "Stok Barang"
                DataGridView1.Columns(3).HeaderText = "Harga Barang"
                DataGridView1.Columns(4).HeaderText = "Tanggal Ditambahkan"
                DataGridView1.Columns(5).HeaderText = "Nama Supplier"

                ' Perbarui tampilan DataGridView
                DataGridView1.AutoResizeColumns()

                txtCari.Clear()
            Else
                MsgBox("Masukkan ID Barang untuk melakukan pencarian", MsgBoxStyle.Exclamation, "Peringatan")
            End If
        End If

        ' Cek apakah karakter yang dimasukkan adalah angka
        If Not Char.IsDigit(e.KeyChar) And e.KeyChar <> ControlChars.Back Then
            ' Jika bukan angka, tolak input tersebut
            e.Handled = True
        End If
    End Sub


    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        ' Pastikan indeks baris valid dan bukan baris kosong
        If e.RowIndex >= 0 Then
            ' Mendapatkan ID barang dari baris yang dipilih
            Dim idBarang As String = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString()

            ' Memuat informasi pemasok dari tabel 'barang' berdasarkan ID barang
            CMD = New MySqlCommand("SELECT * FROM barang WHERE idbarang = @idBarang", CONN)
            CMD.Parameters.AddWithValue("@idBarang", idBarang)
            RD = CMD.ExecuteReader()

            ' Menampilkan informasi barang
            If RD.Read() Then
                txtId.Text = RD("idbarang").ToString()
                txtNama.Text = RD("namabarang").ToString()
                NumericUpDown1.Value = Convert.ToInt32(RD("stokbarang"))
                txtHarga.Text = RD("hargabarang").ToString()
                DateTimePicker1.Value = Convert.ToDateTime(RD("tanggal"))

                ' Mendapatkan nama supplier dari tabel 'supplier' berdasarkan ID supplier
                Dim supplierId As String = RD("id_supplier").ToString()
                RD.Close()

                CMD = New MySqlCommand("SELECT nama_supplier FROM supplier WHERE id_supplier = @supplierId", CONN)
                CMD.Parameters.AddWithValue("@supplierId", supplierId)
                RD = CMD.ExecuteReader()

                If RD.Read() Then
                    cbSupplier.Text = RD("nama_supplier").ToString()
                End If
            End If
            RD.Close()
        End If
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Dim form1 As New Form1()
        form1.Show()
        Me.Close()
    End Sub

    Private Sub Form2_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        ' Menutup koneksi saat Form2 ditutup
        tutupKoneksi()
    End Sub

    Private Sub txtHarga_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHarga.KeyPress
        ' Mengecek apakah tombol yang ditekan adalah angka, tombol kontrol, atau tombol spasi
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            ' Jika bukan angka, kontrol, atau titik, maka tolak input
            e.Handled = True
        End If

        ' Memeriksa apakah ada lebih dari satu titik desimal dalam harga
        If e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.IndexOf("."c) > -1 Then
            ' Jika ada, tolak input
            e.Handled = True
        End If
    End Sub

    Private Sub txtId_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtId.KeyPress
        If Not Char.IsDigit(e.KeyChar) And e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub
End Class
