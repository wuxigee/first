Imports ClsSFZCHK

Imports Microsoft.VisualBasic.FileIO
Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim sex As SFZCHK.SFZSex = SFZCHK.SFZSex.δ֪

        Dim ChkMess As SFZCHK.SFZCHKInfo = SFZCHK.SFZCHK(Me.TextBox1.Text, sex)
        MessageBox.Show(ChkMess.ToString)
        If ChkMess = SFZCHK.SFZCHKInfo.���֤����������ȷ Then
            MessageBox.Show(sex.ToString)
        End If
    End Sub

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Dim Lines As New ArrayList()
        Dim Sex As SFZCHK.SFZSex
        Dim Mess As String = "�����кţ�"

        Dim filename As String = OpenFileDialog1.FileNames(0)
        Dim fields As String()
        Dim delimiter As String = " "
        Using parser As New TextFieldParser(filename)
            parser.SetDelimiters(delimiter)
            While Not parser.EndOfData
                ' Read in the fields for the current line
                fields = parser.ReadFields()
                ' Add code here to use data in fields variable.

                Select Case fields(1)
                    Case "0"
                        Sex = SFZCHK.SFZSex.Ů
                    Case "1"
                        Sex = SFZCHK.SFZSex.��
                    Case Else
                        Sex = SFZCHK.SFZSex.δ֪
                End Select
                If SFZCHK.SFZCHK(fields(0), Sex) <> SFZCHK.SFZCHKInfo.���֤����������ȷ Then
                    Lines.Add(parser.LineNumber)
                End If
            End While
        End Using

        For Each Line As Integer In Lines
            Mess += Line & ","
        Next
        MessageBox.Show(Lines.Count)
        MessageBox.Show(Mess)
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        OpenFileDialog1.ShowDialog()
    End Sub
End Class
