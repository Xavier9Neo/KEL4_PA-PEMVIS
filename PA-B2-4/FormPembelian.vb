Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing

Public Class FormPembelian
    Dim CONN As MySqlConnection
    Dim CMD As MySqlCommand
    Dim RD As MySqlDataReader
    Dim WithEvents PD As New PrintDocument
    Dim PPD As New PrintPreviewDialog

    ' Deklarasi DataTable untuk menyimpan data dari DataGridView di Form2
    Dim dataBarang As New DataTable()

    Sub koneksi()
        CONN = New MySqlConnection("server=localhost;user=root;password=;database=harrypotter")
        CONN.Open()
    End Sub

    Sub tutupKoneksi()
        If CONN IsNot Nothing AndAlso CONN.State = ConnectionState.Open Then
            CONN.Close()
        End If
    End Sub

    Sub LoadDataBarangFromForm2()
        ' Mengambil data barang dari DataGridView di Form2

        ' Koneksi ke database
        koneksi()

        ' Query untuk mengambil data barang dari Form2
        CMD = New MySqlCommand("SELECT barang.idbarang, barang.namabarang, barang.stokbarang, barang.hargabarang FROM barang", CONN)

        ' Membersihkan DataTable sebelum mengisi dengan data baru
        dataBarang.Clear()

        ' Mengisi DataTable dengan data dari hasil query
        Dim DA As New MySqlDataAdapter(CMD)
        DA.Fill(dataBarang)

        ' Menutup koneksi
        tutupKoneksi()
    End Sub

    Sub TampilkanDataBarang()
        ' Memuat data barang ke DataGridView di Form Pembelian

        ' Memastikan DataGridView kosong sebelum memuat data
        DataGridView1.Rows.Clear()

        ' Memuat data dari DataTable ke DataGridView
        For Each row As DataRow In dataBarang.Rows
            Dim idBarang As String = row("idbarang").ToString()
            Dim namaBarang As String = row("namabarang").ToString()
            Dim stokBarang As String = row("stokbarang").ToString()
            Dim hargaBarang As String = row("hargabarang").ToString()

            ' Menambahkan data ke DataGridView
            DataGridView1.Rows.Add(idBarang, namaBarang, stokBarang, hargaBarang)
        Next
    End Sub

    Private Sub FormPembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Memuat data barang dari Form2 saat Form Pembelian dimuat
        LoadDataBarangFromForm2()

        ' Menampilkan data barang di DataGridView di Form Pembelian
        TampilkanDataBarang()
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Dim form1 As New Form1()
        form1.Show()
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        txtJumlahBeli.Clear() ' untuk membersihkan jumlah 
        Try
            ' Pastikan indeks baris valid dan bukan baris kosong
            If e.RowIndex >= 0 Then
                ' Mendapatkan ID barang dari baris yang dipilih
                Dim idBarang As String = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString()

                ' Membuka koneksi
                koneksi()

                ' Memuat informasi pemasok dari tabel 'barang' berdasarkan ID barang
                CMD = New MySqlCommand("SELECT * FROM barang WHERE idbarang = @idBarang", CONN)
                CMD.Parameters.AddWithValue("@idBarang", idBarang)
                RD = CMD.ExecuteReader()

                ' Menampilkan informasi barang
                If RD.Read() Then
                    txtNama.Text = RD("namabarang").ToString()
                    txtHarga.Text = RD("hargabarang").ToString()
                End If
                RD.Close()

                ' Menutup koneksi setelah selesai
                tutupKoneksi()
            End If
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtNama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNama.KeyPress
        txtNama.MaxLength = 50
        If e.KeyChar = Chr(13) Then
            txtNama.Text = UCase(txtNama.Text)
        End If
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

    Private Sub txtJumlahBeli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJumlahBeli.KeyPress
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

    Private Sub btnBeli_Click(sender As Object, e As EventArgs) Handles btnBeli.Click
        ' Mendapatkan jumlah barang yang diinginkan dari TextBox
        Dim jumlahBeli As Integer
        If Integer.TryParse(txtJumlahBeli.Text, jumlahBeli) Then
            ' Mendapatkan stok barang dari DataGridView
            Dim stokBarang As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells(2).Value)

            ' Memeriksa apakah stok cukup
            If jumlahBeli <= stokBarang Then
                ' Jika stok cukup, lakukan pembelian
                MessageBox.Show("Pembelian berhasil dilakukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Mengurangi stok barang di database
                Try
                    koneksi()

                    ' Mendapatkan ID barang dari baris yang dipilih
                    Dim idBarang As String = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()

                    ' Mengurangi stok barang
                    CMD = New MySqlCommand("UPDATE barang SET stokbarang = stokbarang - @jumlahBeli WHERE idbarang = @idBarang", CONN)
                    CMD.Parameters.AddWithValue("@jumlahBeli", jumlahBeli)
                    CMD.Parameters.AddWithValue("@idBarang", idBarang)
                    CMD.ExecuteNonQuery()

                    ' Menutup koneksi setelah selesai
                    tutupKoneksi()

                    ' Memuat ulang data barang untuk memperbarui DataGridView
                    LoadDataBarangFromForm2()
                    TampilkanDataBarang()
                Catch ex As Exception
                    MessageBox.Show("Terjadi kesalahan saat mengurangi stok: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                ' Tampilkan preview struk pembelian
                PPD.Document = PD
                PPD.ShowDialog()
            Else
                ' Jika stok tidak cukup, tampilkan pesan
                MessageBox.Show("Stok tidak cukup.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("Masukkan jumlah beli dengan benar.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub PD_BeginPrint(sender As Object, e As PrintEventArgs) Handles PD.BeginPrint
        Dim pagesetup As New PageSettings
        pagesetup.PaperSize = New PaperSize("Custom", 280, 300)
        PD.DefaultPageSettings = pagesetup
    End Sub

    Private Sub PD_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
        Dim f9 As New Font("Times New Roman", 9, FontStyle.Regular)
        Dim f10 As New Font("Times New Roman", 10, FontStyle.Regular)
        Dim f10b As New Font("Times New Roman", 10, FontStyle.Bold)
        Dim f12 As New Font("Times New Roman", 12, FontStyle.Bold)

        Dim leftmargin As Integer = PD.DefaultPageSettings.Margins.Left - 100 ' Menggeser ke kiri 100 pixel
        Dim centermargin As Integer = PD.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PD.DefaultPageSettings.PaperSize.Width

        Dim kanan As New StringFormat
        Dim tengah As New StringFormat
        Dim kiri As New StringFormat

        kiri.Alignment = StringAlignment.Near
        kanan.Alignment = StringAlignment.Far
        tengah.Alignment = StringAlignment.Center

        Dim garis As String
        garis = "------------------------------------------------------------------------------"

        Dim tinggi As Integer = 15 ' Atur posisi vertikal awal
        e.Graphics.DrawString("WIZARDNEEDS", f12, Brushes.Black, centermargin, tinggi, tengah)
        tinggi += 20 ' Tinggi teks pertama
        e.Graphics.DrawString("Jl. Sambaliung No. 09 Gn. Kelua Samarinda", f10b, Brushes.Black, centermargin, tinggi, tengah)
        tinggi += 20 ' Tinggi teks kedua
        e.Graphics.DrawString("Hp : 0834-4356-4567", f10, Brushes.Black, centermargin, tinggi, tengah)
        tinggi += 20 ' Tinggi teks ketiga

        ' Menambahkan tanggal dan waktu saat ini
        Dim currentDateTime As String = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
        tinggi += 10
        e.Graphics.DrawString(currentDateTime, f9, Brushes.Black, leftmargin, tinggi, kiri)

        tinggi += 10
        e.Graphics.DrawString(garis, f10, Brushes.Black, 0, tinggi) ' Gambar garis

        tinggi += 25
        e.Graphics.DrawString(txtJumlahBeli.Text, f9, Brushes.Black, leftmargin, tinggi, kiri)
        e.Graphics.DrawString(txtNama.Text, f9, Brushes.Black, centermargin, tinggi, tengah)
        e.Graphics.DrawString(txtHarga.Text, f9, Brushes.Black, rightmargin, tinggi, kanan)

        tinggi += 25 ' Tinggi garis
        e.Graphics.DrawString(garis, f10, Brushes.Black, 0, tinggi)

        ' Menghitung total harga
        Dim jumlahBeli As Integer = Integer.Parse(txtJumlahBeli.Text)
        Dim hargaSatuan As Integer = Integer.Parse(txtHarga.Text)
        Dim totalHarga As Integer = jumlahBeli * hargaSatuan

        ' Cetak total harga
        tinggi += 15
        e.Graphics.DrawString("Total Harga", f10b, Brushes.Black, leftmargin, tinggi, kiri)
        e.Graphics.DrawString("Rp. " & totalHarga.ToString(), f10b, Brushes.Black, rightmargin, tinggi, kanan)

        e.Graphics.DrawString("TERIMA KASIH TELAH BELANJA", f10, Brushes.Black, centermargin, 50 + tinggi, tengah)
        e.Graphics.DrawString("DI TOKO KAMI", f10, Brushes.Black, centermargin, 75 + tinggi, tengah)
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Application.Exit()
    End Sub
End Class
