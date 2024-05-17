Imports MySql.Data.MySqlClient
Imports System.Windows.Forms

Module Module1
    Public CONN As MySqlConnection
    Public CMD As MySqlCommand
    Public RD As MySqlDataReader
    Public DA As MySqlDataAdapter
    Public DS As DataSet

    Public Sub koneksi()
        Try
            Dim str As String = "server=localhost;userid=root;password=;database=harrypotter"

            CONN = New MySqlConnection(str)
            If CONN.State = ConnectionState.Closed Then
                CONN.Open()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Module
