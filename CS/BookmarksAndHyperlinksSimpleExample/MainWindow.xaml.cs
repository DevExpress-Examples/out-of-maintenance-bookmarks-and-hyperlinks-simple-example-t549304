using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Ribbon;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.Xpf.RichEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookmarksAndHyperlinksSimpleExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            richEdit.LoadDocument("Hyperlinks.docx");
            InsertHyperlink();
            InsertBookmark();
        }

        private void InsertBookmark()
        {
            #region #InsertBookmark
            Document document = richEdit.Document;
            document.BeginUpdate();
            DocumentPosition pos = document.Range.Start;

            //Create a bookmark to a given position
            document.Bookmarks.Create(document.CreateRange(pos, 1), "Top");

            //Insert the hyperlink anchored to the created bookmark:
            DocumentRange[] foundRanges = document.FindAll("To the Top", SearchOptions.CaseSensitive);
            if (foundRanges.Length > 0)
            {
                document.Hyperlinks.Create(foundRanges[0]);
                document.Hyperlinks[1].Anchor = "Top";
            }
            document.EndUpdate();
            #endregion #InsertBookmark
        }

        private void InsertHyperlink()
        {
            #region #InsertHyperlink
            Document document = richEdit.Document;

            //Find the specific text string in a document
            DocumentRange[] foundRanges = document.FindAll("DevExpress WinForms Rich Text Editor",
            SearchOptions.CaseSensitive);
            if (foundRanges.Length > 0)
            {
                //Create a hyperlink from a found range
                document.Hyperlinks.Create(foundRanges[0]);

                //Set the URI and the tooltip for the created hyperlink
                document.Hyperlinks[0].NavigateUri = "https://www.devexpress.com/Products/NET/Controls/WinForms/Rich_Editor/";
                document.Hyperlinks[0].ToolTip = "WinForms Rich Text Editor";
            }
            #endregion #InsertHyperlink
        }
        
    }
    #region #KeyConverter
    public static class KeysToModifierKeysConverter
    {
        public static Keys ToModifierKeys(bool ctrl, bool alt, bool shift)
        {
            var result = Keys.None;
            if (ctrl)
                result |= Keys.Control;
            if (alt)
                result |= Keys.Alt;
            if (shift)
                result |= Keys.Shift;
            return result;
        }
    }
    #endregion #KeyConverter
}

