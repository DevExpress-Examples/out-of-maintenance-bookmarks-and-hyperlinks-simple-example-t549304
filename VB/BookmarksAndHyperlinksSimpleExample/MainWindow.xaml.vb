Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Ribbon
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.Xpf.RichEdit
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Forms
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace BookmarksAndHyperlinksSimpleExample
    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            richEdit.LoadDocument("Hyperlinks.docx")
            InsertHyperlink()
            InsertBookmark()
        End Sub

        Private Sub InsertBookmark()
'            #Region "#InsertBookmark"
            Dim document As Document = richEdit.Document
            document.BeginUpdate()
            Dim pos As DocumentPosition = document.Range.Start

            'Create a bookmark to a given position
            document.Bookmarks.Create(document.CreateRange(pos, 1), "Top")

            'Insert the hyperlink anchored to the created bookmark:
            Dim foundRanges() As DocumentRange = document.FindAll("To the Top", SearchOptions.CaseSensitive)
            If foundRanges.Length > 0 Then
                document.Hyperlinks.Create(foundRanges(0))
                document.Hyperlinks(1).Anchor = "Top"
            End If
            document.EndUpdate()
'            #End Region ' #InsertBookmark
        End Sub

        Private Sub InsertHyperlink()
'            #Region "#InsertHyperlink"
            Dim document As Document = richEdit.Document

            'Find the specific text string in a document
            Dim foundRanges() As DocumentRange = document.FindAll("DevExpress WinForms Rich Text Editor", SearchOptions.CaseSensitive)
            If foundRanges.Length > 0 Then
                'Create a hyperlink from a found range
                document.Hyperlinks.Create(foundRanges(0))

                'Set the URI and the tooltip for the created hyperlink
                document.Hyperlinks(0).NavigateUri = "https://www.devexpress.com/Products/NET/Controls/WinForms/Rich_Editor/"
                document.Hyperlinks(0).ToolTip = "WinForms Rich Text Editor"
            End If
'            #End Region ' #InsertHyperlink
        End Sub

    End Class
    #Region "#KeyConverter"
    Public NotInheritable Class KeysToModifierKeysConverter

        Private Sub New()
        End Sub

        Public Shared Function ToModifierKeys(ByVal ctrl As Boolean, ByVal alt As Boolean, ByVal shift As Boolean) As Keys
            Dim result = Keys.None
            If ctrl Then
                result = result Or Keys.Control
            End If
            If alt Then
                result = result Or Keys.Alt
            End If
            If shift Then
                result = result Or Keys.Shift
            End If
            Return result
        End Function
    End Class
    #End Region ' #KeyConverter
End Namespace

