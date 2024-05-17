Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Imports System.IO

Public Class FormLaporanSupplier
    Private WithEvents PD As New PrintDocument
    Private initialData As DataTable ' Variabel untuk menyimpan data awal

    Private Sub FormLaporanSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler PrintDocument1.PrintPage, AddressOf Me.PrintDocument1_PrintPage
        Me.PrintPreviewDialog1.Document = Me.PrintDocument1
        ' Panggil metode untuk menampilkan data barang dan supplier
        TampilDataBarangDanSupplier()
    End Sub

    Sub Kosong()
        txtId.Clear()
        txtKeterangan.Clear()
        ' Set DateTimePicker ke tanggal saat ini
        DateTimePicker2.Value = DateTime.Now
        rbTerima.Checked = False
        rbTolak.Checked = False
        ' Set kontrol read-only/disabled secara default
        DateTimePicker2.Enabled = False
        txtKeterangan.ReadOnly = True
    End Sub

    Private Sub TampilDataBarangDanSupplier()
        ' Buat koneksi ke database
        Using connection As New MySqlConnection("server=localhost;user=root;password=;database=harrypotter")
            ' Buka koneksi
            connection.Open()

            ' Buat perintah SQL untuk mengambil data barang dan supplier dengan JOIN
            Dim query As String = "SELECT barang.idbarang, barang.namabarang, barang.stokbarang, barang.hargabarang, barang.tanggal, barang.keterangan, barang.tanggal_keluar, barang.status, supplier.id_supplier, supplier.nama_supplier, supplier.alamat FROM barang INNER JOIN supplier ON barang.id_supplier = supplier.id_supplier"

            ' Jika ada ID yang dimasukkan, tambahkan filter WHERE ke kueri
            If Not String.IsNullOrEmpty(txtId.Text) Then
                query &= " WHERE barang.idbarang = @idbarang"
            End If

            ' Buat adapter data MySql untuk menjalankan query
            Using adapter As New MySqlDataAdapter(query, connection)
                ' Jika ada ID yang dimasukkan, tentukan nilainya dalam parameter
                If Not String.IsNullOrEmpty(txtId.Text) Then
                    adapter.SelectCommand.Parameters.AddWithValue("@idbarang", txtId.Text)
                End If
                ' Buat objek DataTable untuk menampung hasil query
                Dim dataTable As New DataTable()

                ' Isi data dari adapter ke DataTable
                adapter.Fill(dataTable)

                ' Simpan data awal jika belum disimpan
                If initialData Is Nothing Then
                    initialData = dataTable.Copy()
                End If

                ' Bersihkan DataGridViewLaporan dari kolom yang ada sebelumnya
                DataGridViewLaporan.Columns.Clear()

                ' Tambahkan kolom-kolom ke DataGridView
                DataGridViewLaporan.Columns.Add("id_supplier", "ID Supplier")
                DataGridViewLaporan.Columns.Add("nama_supplier", "Nama Supplier")
                DataGridViewLaporan.Columns.Add("alamat", "Alamat")
                DataGridViewLaporan.Columns.Add("idbarang", "ID Barang")
                DataGridViewLaporan.Columns.Add("namabarang", "Nama Barang")
                DataGridViewLaporan.Columns.Add("status", "Status")
                DataGridViewLaporan.Columns.Add("stokbarang", "Stok Barang")
                DataGridViewLaporan.Columns.Add("hargabarang", "Harga Barang")
                DataGridViewLaporan.Columns.Add("tanggal", "Tanggal")
                DataGridViewLaporan.Columns.Add("tanggal_keluar", "Tanggal Keluar")
                DataGridViewLaporan.Columns.Add("keterangan", "Keterangan")

                ' Perulangan untuk menambahkan data ke DataGridView
                For Each row As DataRow In dataTable.Rows
                    ' Tambahkan baris baru ke DataGridView
                    Dim newRow As DataGridViewRow = New DataGridViewRow()

                    ' Tambahkan kolom-kolom ke dalam baris baru
                    newRow.CreateCells(DataGridViewLaporan)

                    ' Isi data ke dalam kolom-kolom sesuai dengan urutan kolom-kolom yang sudah ada
                    newRow.Cells(0).Value = row("id_supplier")
                    newRow.Cells(1).Value = row("nama_supplier")
                    newRow.Cells(2).Value = row("alamat")
                    newRow.Cells(3).Value = row("idbarang")
                    newRow.Cells(4).Value = row("namabarang")
                    newRow.Cells(5).Value = row("status")
                    newRow.Cells(6).Value = row("stokbarang")
                    newRow.Cells(7).Value = row("hargabarang")
                    newRow.Cells(8).Value = row("tanggal")
                    newRow.Cells(9).Value = row("tanggal_keluar")
                    newRow.Cells(10).Value = row("keterangan")

                    ' Tambahkan baris ke DataGridView
                    DataGridViewLaporan.Rows.Add(newRow)
                Next
            End Using
        End Using
    End Sub

    Private Sub IsiUlangDataGridView()
        ' Simpan status, tanggal keluar, dan keterangan yang ada saat ini
        Dim currentStatus As New Dictionary(Of String, String)()
        Dim currentTanggalKeluar As New Dictionary(Of String, String)()
        Dim currentKeterangan As New Dictionary(Of String, String)()

        For Each row As DataGridViewRow In DataGridViewLaporan.Rows
            Dim idBarang As String = row.Cells("idbarang").Value.ToString()
            currentStatus(idBarang) = row.Cells("status").Value.ToString()
            currentTanggalKeluar(idBarang) = If(row.Cells("tanggal_keluar").Value IsNot Nothing, row.Cells("tanggal_keluar").Value.ToString(), "")
            currentKeterangan(idBarang) = If(row.Cells("keterangan").Value IsNot Nothing, row.Cells("keterangan").Value.ToString(), "")
        Next

        ' Bersihkan DataGridView
        DataGridViewLaporan.Rows.Clear()

        ' Tambahkan kolom-kolom ke DataGridView jika belum ada
        If DataGridViewLaporan.Columns.Count = 0 Then
            DataGridViewLaporan.Columns.Add("id_supplier", "ID Supplier")
            DataGridViewLaporan.Columns.Add("nama_supplier", "Nama Supplier")
            DataGridViewLaporan.Columns.Add("alamat", "Alamat")
            DataGridViewLaporan.Columns.Add("idbarang", "ID Barang")
            DataGridViewLaporan.Columns.Add("namabarang", "Nama Barang")
            DataGridViewLaporan.Columns.Add("status", "Status")
            DataGridViewLaporan.Columns.Add("stokbarang", "Stok Barang")
            DataGridViewLaporan.Columns.Add("hargabarang", "Harga Barang")
            DataGridViewLaporan.Columns.Add("tanggal", "Tanggal")
            DataGridViewLaporan.Columns.Add("tanggal_keluar", "Tanggal Keluar")
            DataGridViewLaporan.Columns.Add("keterangan", "Keterangan")
        End If

        ' Isi DataGridView dengan data awal
        For Each row As DataRow In initialData.Rows
            ' Tambahkan baris baru ke DataGridView
            Dim newRow As DataGridViewRow = New DataGridViewRow()
            newRow.CreateCells(DataGridViewLaporan)

            ' Isi data ke dalam kolom-kolom sesuai dengan urutan kolom-kolom yang sudah ada
            newRow.Cells(0).Value = row("id_supplier")
            newRow.Cells(1).Value = row("nama_supplier")
            newRow.Cells(2).Value = row("alamat")
            newRow.Cells(3).Value = row("idbarang")
            newRow.Cells(4).Value = row("namabarang")
            newRow.Cells(5).Value = If(currentStatus.ContainsKey(row("idbarang").ToString()), currentStatus(row("idbarang").ToString()), row("status"))
            newRow.Cells(6).Value = row("stokbarang")
            newRow.Cells(7).Value = row("hargabarang")
            newRow.Cells(8).Value = row("tanggal")
            newRow.Cells(9).Value = If(currentTanggalKeluar.ContainsKey(row("idbarang").ToString()), currentTanggalKeluar(row("idbarang").ToString()), row("tanggal_keluar"))
            newRow.Cells(10).Value = If(currentKeterangan.ContainsKey(row("idbarang").ToString()), currentKeterangan(row("idbarang").ToString()), row("keterangan"))

            ' Tambahkan baris ke DataGridView
            DataGridViewLaporan.Rows.Add(newRow)
        Next
    End Sub

    Private Sub btnTampil_Click(sender As Object, e As EventArgs) Handles btnTampil.Click
        If rbTerima.Checked OrElse rbTolak.Checked Then
            If rbTolak.Checked AndAlso (String.IsNullOrEmpty(txtKeterangan.Text) OrElse DateTimePicker2.Value = Nothing) Then
                MessageBox.Show("Keterangan dan Tanggal Keluar harus diisi jika status Ditolak.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Perulangan untuk mencari barang yang sesuai dengan ID yang dimasukkan
            Dim found As Boolean = False
            For Each row As DataGridViewRow In DataGridViewLaporan.Rows
                If row.Cells("idbarang").Value.ToString() = txtId.Text Then
                    If rbTerima.Checked Then
                        ' Update status menjadi Terima
                        row.Cells("status").Value = "Terima"
                        row.Cells("tanggal_keluar").Value = DBNull.Value
                        row.Cells("keterangan").Value = DBNull.Value
                        UpdateStatusInDatabase(row.Cells("idbarang").Value.ToString(), "Terima", Nothing, Nothing)
                    ElseIf rbTolak.Checked Then
                        ' Ambil keterangan dan tanggal keluar dari kontrol yang sesuai
                        Dim keterangan As String = txtKeterangan.Text
                        Dim tanggalKeluar As String = DateTimePicker2.Value.ToString("yyyy-MM-dd")
                        row.Cells("status").Value = "Ditolak"
                        row.Cells("tanggal_keluar").Value = tanggalKeluar
                        row.Cells("keterangan").Value = keterangan
                        UpdateStatusInDatabase(row.Cells("idbarang").Value.ToString(), "Ditolak", tanggalKeluar, keterangan)
                    End If
                    found = True
                    Exit For
                End If
            Next

            If Not found Then
                MessageBox.Show("Masukkan ID barang yang sesuai.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("Pilih status terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        Kosong()
    End Sub

    Private Sub UpdateStatusInDatabase(idBarang As String, status As String, tanggalKeluar As String, keterangan As String)
        ' Buat koneksi ke database
        Using connection As New MySqlConnection("server=localhost;user=root;password=;database=harrypotter")
            ' Buka koneksi
            connection.Open()

            ' Buat perintah SQL untuk memperbarui status
            Dim query As String = "UPDATE barang SET status = @status, tanggal_keluar = @tanggal_keluar, keterangan = @keterangan WHERE idbarang = @idbarang"

            ' Buat command MySql untuk menjalankan query
            Using command As New MySqlCommand(query, connection)
                ' Tentukan nilai parameter
                command.Parameters.AddWithValue("@status", status)
                command.Parameters.AddWithValue("@tanggal_keluar", If(String.IsNullOrEmpty(tanggalKeluar), DBNull.Value, tanggalKeluar))
                command.Parameters.AddWithValue("@keterangan", If(String.IsNullOrEmpty(keterangan), DBNull.Value, keterangan))
                command.Parameters.AddWithValue("@idbarang", idBarang)

                ' Jalankan perintah
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ' Reset DataGridView ke data awal
        TampilDataBarangDanSupplier()
        ' Kosongkan kontrol
        Kosong()
    End Sub

    Private Sub BtnKembali_Click(sender As Object, e As EventArgs) Handles BtnKembali.Click
        Dim form1 As New Form1
        form1.Show()
        Me.Close()
    End Sub

    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        ' Tampilkan dialog pratinjau cetak
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub PrintDocument1_BeginPrint(sender As Object, e As PrintEventArgs) Handles PrintDocument1.BeginPrint
        ' Buat pengaturan halaman baru
        Dim pagesetup As New PageSettings
        ' Atur ukuran kertas sesuai kebutuhan, misalnya ukuran A4
        pagesetup.PaperSize = New PaperSize("A4", 827, 1500) ' A4 dalam satuan 1/100 inci
        ' Atur orientasi halaman menjadi landscape
        pagesetup.Landscape = True
        ' Tetapkan pengaturan halaman ke PrintDocument
        PrintDocument1.DefaultPageSettings = pagesetup
    End Sub


    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim f7 As New Font("Times New Roman", 7, FontStyle.Regular)
        Dim f10 As New Font("Times New Roman", 10, FontStyle.Regular)
        Dim f10b As New Font("Times New Roman", 10, FontStyle.Bold)
        Dim f12 As New Font("Times New Roman", 12, FontStyle.Bold)

        Dim leftMargin As Integer = e.MarginBounds.Left
        Dim rightMargin As Integer = e.MarginBounds.Right
        Dim topMargin As Integer = e.MarginBounds.Top
        Dim bottomMargin As Integer = e.MarginBounds.Bottom
        Dim rowHeight As Integer = 30
        Dim currentY As Integer = topMargin

        Dim headerBrush As New SolidBrush(Color.Black)
        Dim rowBrush As New SolidBrush(Color.Black)
        Dim fontHeader As New Font("Arial", 10, FontStyle.Bold)
        Dim fontRows As New Font("Arial", 7, FontStyle.Regular)

        ' Calculate the center position for the header text
        Dim headerText As String = "DATA BARANG PERNAK PERNIK TOKO WIZARDNEEDS"
        Dim headerSize As SizeF = e.Graphics.MeasureString(headerText, fontHeader)
        Dim headerX As Integer = CInt((e.MarginBounds.Width - headerSize.Width) / 2) + leftMargin

        e.Graphics.DrawString(headerText, fontHeader, headerBrush, headerX, currentY)
        currentY += 50

        ' Print header
        Dim columnPosition As Integer = leftMargin
        Dim columnWidth As Integer = (rightMargin - leftMargin) \ DataGridViewLaporan.Columns.Count ' Adjust column width to fit the page

        For Each column As DataGridViewColumn In DataGridViewLaporan.Columns
            e.Graphics.DrawString(column.HeaderText, fontHeader, headerBrush, columnPosition, currentY)
            columnPosition += columnWidth
        Next

        currentY += rowHeight

        ' Print rows
        For Each row As DataGridViewRow In DataGridViewLaporan.Rows
            columnPosition = leftMargin
            For Each cell As DataGridViewCell In row.Cells
                If cell.Value IsNot Nothing Then
                    Dim cellValue As String = cell.Value.ToString()
                    Dim cellSize As SizeF = e.Graphics.MeasureString(cellValue, fontRows)
                    ' Adjust cell value if it exceeds the column width
                    While cellSize.Width > columnWidth
                        cellValue = cellValue.Substring(0, cellValue.Length - 1)
                        cellSize = e.Graphics.MeasureString(cellValue, fontRows)
                    End While
                    e.Graphics.DrawString(cellValue, fontRows, rowBrush, columnPosition, currentY)
                End If
                columnPosition += columnWidth
            Next
            currentY += rowHeight
        Next

        Dim downloadFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\Downloads\"
        Dim imagePath As String = downloadFolder & "ttd.jpg"

        ' Periksa apakah file tanda tangan ada
        If File.Exists(imagePath) Then
            Dim signatureImage As Image = Image.FromFile(imagePath)
            Dim signatureWidth As Integer = 100 ' Lebar tetap
            Dim signatureHeight As Integer = signatureImage.Height * signatureWidth / signatureImage.Width
            Dim signatureX As Integer = e.MarginBounds.Right - signatureWidth ' Tanda tangan di sebelah kanan
            Dim signatureY As Integer = e.MarginBounds.Bottom - signatureHeight - 50 ' Sesuaikan nilai -50 untuk mengatur posisi gambar lebih tinggi atau lebih rendah

            e.Graphics.DrawImage(signatureImage, signatureX, signatureY, signatureWidth, signatureHeight)

            Dim keteranganText As String = "Yang bertanda tangan admin"
            Dim fontKeterangan As New Font("Arial", 7, FontStyle.Italic)
            Dim keteranganSize As SizeF = e.Graphics.MeasureString(keteranganText, fontKeterangan)
            Dim keteranganX As Integer = e.MarginBounds.Right - keteranganSize.Width ' Keterangan di sebelah kanan
            Dim keteranganY As Integer = signatureY + signatureHeight + 10 ' Jarak antara tanda tangan dan keterangan

            e.Graphics.DrawString(keteranganText, fontKeterangan, Brushes.Black, keteranganX, keteranganY)

        Else
            MessageBox.Show("File tanda tangan tidak ditemukan.")
        End If


        ' Indicate whether more pages need to be printed
        e.HasMorePages = currentY > e.MarginBounds.Bottom
    End Sub



    Private Sub rbTerima_CheckedChanged(sender As Object, e As EventArgs) Handles rbTerima.CheckedChanged
        If rbTerima.Checked Then
            DateTimePicker2.Enabled = False
            txtKeterangan.ReadOnly = True
        End If
    End Sub

    Private Sub rbTolak_CheckedChanged(sender As Object, e As EventArgs) Handles rbTolak.CheckedChanged
        If rbTolak.Checked Then
            DateTimePicker2.Enabled = True
            txtKeterangan.ReadOnly = False
        End If
    End Sub

    Private Sub txtId_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtId.KeyPress
        If Not Char.IsDigit(e.KeyChar) And e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

End Class
