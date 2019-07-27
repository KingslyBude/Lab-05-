Option Strict On
Imports System.IO

''' <summary>
''' Title: Lab 05: Text Editor
''' Created By: Kingsly Bude
''' Created On: 2019-07-25
''' Last Modified: 2019-07-26 - Kingsly Bude
''' About: Demonstration of basic string readers as well as utilization of prebuilt .NET forms.
''' </summary>
Public Class frmLab05

    Dim filePathString As String = "" '' If this string ever changes, the file should have

    ''' <summary>
    ''' Sub Name: ExitToolStripMenuItem_Click
    ''' Event: OpenToolStripMenuItem.Click
    ''' About: Clicking "Exit" provides the user with a way to end the application
    ''' </summary>
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()

    End Sub

    ''' <summary>
    ''' Sub name: OpenToolStripMenuItem_Click
    ''' Event: OpenToolStripMenuItem.Click
    ''' About: Clicking "Open" on the menu strip opens a OpenFileDialog. The user can select a text file to open to the text editor to make adjustments.
    ''' </summary>
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        If OpenFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                ''Write the contents of the file to the text editor
                Dim fileReader As New StringReader(OpenFileDialog.FileName)
                filePathString = OpenFileDialog.FileName ''Once the file is opened, the file path is saved for later use
                txtFileContents.Text = fileReader.ReadToEnd
                fileReader.Close()

            Catch ex As Exception
                ''Catch statement to catch an ApplicationException 
                Throw New ApplicationException(ex.ToString())

            End Try
        End If
    End Sub

    ''' <summary>
    ''' Sub name: SaveAsToolStripMenuItem_Click
    ''' Event: SaveAsToolStripMenuItem.Click
    ''' About: Clicking "Save As" on the menu strip opens a SaveFileDialog. Users can save the contents of the text editor to a new or existing file.
    ''' </summary>
    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        If SaveFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                ''Write the contents of the text editor to the file
                My.Computer.FileSystem.WriteAllText(SaveFileDialog.FileName, txtFileContents.Text, False)
                filePathString = SaveFileDialog.FileName 'Once the file is saved, the file path is saved for later use

            Catch ex As Exception
                ''Catch statement to catch an ApplicationException 
                Throw New ApplicationException(ex.ToString())

            End Try
        End If
    End Sub

    ''' <summary>
    ''' Sub Name: SaveToolStripMenuItem_Click
    ''' Event: SaveToolStripMenuItem.Click
    ''' About: Using "Save" in the menu strip will attempt to save to an existing file the editor is already working on automatically.
    '''        If there is no file path, the SaveFileDialog will be used and the user can choose to create a new or replace an exsisting file.
    ''' </summary>
    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        If filePathString IsNot "" Then
            ''If the filePathString variable was changed at any time, the text editor should then be working on an exsisting file
            My.Computer.FileSystem.WriteAllText(filePathString, txtFileContents.Text, False)

        Else
            ''If the filePathString is blank, then there was never a file specified since the text editor was started, or the user hit "New" in the menu strip
            If SaveFileDialog.ShowDialog() = DialogResult.OK Then
                ''A SaveFileDialog will be used instead to save the contents of the text editor to the file
                Try
                    My.Computer.FileSystem.WriteAllText(SaveFileDialog.FileName, txtFileContents.Text, False)
                    filePathString = SaveFileDialog.FileName 'Once the file is saved, the file path is saved for later use

                Catch ex As Exception
                    ''Catch statement to catch an ApplicationException 
                    Throw New ApplicationException(ex.ToString())

                End Try
            End If

        End If
    End Sub

    ''' <summary>
    ''' Sub Name: SaveFileDialog_FileOk
    ''' Event: SaveFileDialog.FileOk
    ''' About: Sends a message when the user saves a file using the SaveFileDialog
    ''' </summary>
    Private Sub SaveFileDialog_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog.FileOk
        MessageBox.Show("Save Complete")

    End Sub

    ''' <summary>
    ''' Sub name: AboutToolStripMenuItem_Click
    ''' Event: AboutToolStripMenuItem.Click
    ''' About: Displays information after clink on "About" under "Help" in the menu strip
    ''' </summary>
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        MessageBox.Show("NETD 2202" & vbNewLine & "Lab #5" & vbNewLine & "Kingsly Bude ", "About")

    End Sub

    ''' <summary>
    ''' Sub name: NewToolStripMenuItem_Click
    ''' Event: NewToolStripMenuItem.Click
    ''' About: Displays information after clink on "About" under "Help" in the menu strip
    ''' </summary>
    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        txtFileContents.Text = ""
        filePathString = ""

    End Sub

    ''' <summary>
    ''' Sub name: CopyToolStripMenuItem_Click
    ''' Event: CopyToolStripMenuItem.Click
    ''' About: Copies the selected text in the textbox to the clipboard
    ''' </summary>
    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        My.Computer.Clipboard.SetText(txtFileContents.SelectedText)

    End Sub

    ''' <summary>
    ''' Sub name: PasteToolStripMenuItem_Click
    ''' Event: PasteToolStripMenuItem.Click
    ''' About: Pastes the contents of the clipboard to the text editor
    ''' </summary>
    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        txtFileContents.SelectedText = My.Computer.Clipboard.GetText()

    End Sub

    ''' <summary>
    ''' Sub name: CutToolStripMenuItem_Click
    ''' Event: CutToolStripMenuItem.Click
    ''' About: Copies the selected text in the textbox to the clipboard, and deletes the selected text afterwards
    ''' </summary>
    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        My.Computer.Clipboard.SetText(txtFileContents.SelectedText)
        txtFileContents.SelectedText = ""

    End Sub
End Class