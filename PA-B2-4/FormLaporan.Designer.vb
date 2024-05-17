<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLaporanSupplier
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormLaporanSupplier))
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.BtnKembali = New System.Windows.Forms.Button()
        Me.btnCetak = New System.Windows.Forms.Button()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.DataGridViewLaporan = New System.Windows.Forms.DataGridView()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnTampil = New System.Windows.Forms.Button()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.txtKeterangan = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbTolak = New System.Windows.Forms.RadioButton()
        Me.rbTerima = New System.Windows.Forms.RadioButton()
        Me.btnBatal = New System.Windows.Forms.Button()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        CType(Me.DataGridViewLaporan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(397, 128)
        Me.txtId.Margin = New System.Windows.Forms.Padding(4)
        Me.txtId.Multiline = True
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(192, 31)
        Me.txtId.TabIndex = 0
        '
        'BtnKembali
        '
        Me.BtnKembali.Font = New System.Drawing.Font("Lucida Sans", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnKembali.Location = New System.Drawing.Point(234, 279)
        Me.BtnKembali.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BtnKembali.Name = "BtnKembali"
        Me.BtnKembali.Size = New System.Drawing.Size(115, 38)
        Me.BtnKembali.TabIndex = 15
        Me.BtnKembali.Text = "Kembali"
        Me.BtnKembali.UseVisualStyleBackColor = True
        '
        'btnCetak
        '
        Me.btnCetak.Font = New System.Drawing.Font("Lucida Sans", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCetak.Location = New System.Drawing.Point(611, 279)
        Me.btnCetak.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnCetak.Name = "btnCetak"
        Me.btnCetak.Size = New System.Drawing.Size(115, 38)
        Me.btnCetak.TabIndex = 16
        Me.btnCetak.Text = "Cetak"
        Me.btnCetak.UseVisualStyleBackColor = True
        '
        'PrintDocument1
        '
        '
        'DataGridViewLaporan
        '
        Me.DataGridViewLaporan.AllowUserToAddRows = False
        Me.DataGridViewLaporan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewLaporan.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridViewLaporan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewLaporan.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column9, Me.Column1, Me.Column5, Me.Column2, Me.Column3, Me.Column11, Me.Column4, Me.Column6, Me.Column7, Me.Column8, Me.Column10})
        Me.DataGridViewLaporan.Location = New System.Drawing.Point(11, 335)
        Me.DataGridViewLaporan.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DataGridViewLaporan.Name = "DataGridViewLaporan"
        Me.DataGridViewLaporan.ReadOnly = True
        Me.DataGridViewLaporan.RowHeadersVisible = False
        Me.DataGridViewLaporan.RowTemplate.Height = 28
        Me.DataGridViewLaporan.Size = New System.Drawing.Size(1050, 182)
        Me.DataGridViewLaporan.TabIndex = 17
        '
        'Column9
        '
        Me.Column9.HeaderText = "ID Supplier"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Nama Supplier"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "Alamat Supplier"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Id Barang"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Nama Barang"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column11
        '
        Me.Column11.HeaderText = "Status"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "Stok Barang"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.HeaderText = "Harga Barang"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.HeaderText = "Tanggal Masuk"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'Column8
        '
        Me.Column8.HeaderText = "Tanggal Keluar"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'Column10
        '
        Me.Column10.HeaderText = "Keterangan"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        '
        'btnTampil
        '
        Me.btnTampil.Font = New System.Drawing.Font("Lucida Sans", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTampil.Location = New System.Drawing.Point(422, 279)
        Me.btnTampil.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnTampil.Name = "btnTampil"
        Me.btnTampil.Size = New System.Drawing.Size(115, 38)
        Me.btnTampil.TabIndex = 18
        Me.btnTampil.Text = "Tampil"
        Me.btnTampil.UseVisualStyleBackColor = True
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Location = New System.Drawing.Point(397, 180)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(192, 22)
        Me.DateTimePicker2.TabIndex = 19
        '
        'txtKeterangan
        '
        Me.txtKeterangan.Location = New System.Drawing.Point(813, 176)
        Me.txtKeterangan.Margin = New System.Windows.Forms.Padding(4)
        Me.txtKeterangan.Multiline = True
        Me.txtKeterangan.Name = "txtKeterangan"
        Me.txtKeterangan.Size = New System.Drawing.Size(248, 31)
        Me.txtKeterangan.TabIndex = 20
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbTolak)
        Me.Panel1.Controls.Add(Me.rbTerima)
        Me.Panel1.Location = New System.Drawing.Point(813, 119)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(245, 38)
        Me.Panel1.TabIndex = 21
        '
        'rbTolak
        '
        Me.rbTolak.AutoSize = True
        Me.rbTolak.Location = New System.Drawing.Point(131, 9)
        Me.rbTolak.Name = "rbTolak"
        Me.rbTolak.Size = New System.Drawing.Size(64, 21)
        Me.rbTolak.TabIndex = 1
        Me.rbTolak.Text = "Tolak"
        Me.rbTolak.UseVisualStyleBackColor = True
        '
        'rbTerima
        '
        Me.rbTerima.AutoSize = True
        Me.rbTerima.Location = New System.Drawing.Point(28, 9)
        Me.rbTerima.Name = "rbTerima"
        Me.rbTerima.Size = New System.Drawing.Size(73, 21)
        Me.rbTerima.TabIndex = 0
        Me.rbTerima.Text = "Terima"
        Me.rbTerima.UseVisualStyleBackColor = True
        '
        'btnBatal
        '
        Me.btnBatal.Font = New System.Drawing.Font("Lucida Sans", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBatal.Location = New System.Drawing.Point(799, 279)
        Me.btnBatal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnBatal.Name = "btnBatal"
        Me.btnBatal.Size = New System.Drawing.Size(115, 38)
        Me.btnBatal.TabIndex = 22
        Me.btnBatal.Text = "Batal"
        Me.btnBatal.UseVisualStyleBackColor = True
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'FormLaporanSupplier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.POSTTEST_4.My.Resources.Resources.Cream_and_Beige_Aesthetic_Elegant_Online_Portfolio_Shop_Desktop_Prototype__3_2
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1069, 519)
        Me.Controls.Add(Me.btnBatal)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtKeterangan)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.btnTampil)
        Me.Controls.Add(Me.DataGridViewLaporan)
        Me.Controls.Add(Me.btnCetak)
        Me.Controls.Add(Me.BtnKembali)
        Me.Controls.Add(Me.txtId)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormLaporanSupplier"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormSupplier"
        CType(Me.DataGridViewLaporan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents BtnKembali As System.Windows.Forms.Button
    Friend WithEvents btnCetak As System.Windows.Forms.Button
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents DataGridViewLaporan As System.Windows.Forms.DataGridView
    Friend WithEvents btnTampil As System.Windows.Forms.Button
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtKeterangan As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnBatal As System.Windows.Forms.Button
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents rbTerima As System.Windows.Forms.RadioButton
    Friend WithEvents rbTolak As System.Windows.Forms.RadioButton
End Class
