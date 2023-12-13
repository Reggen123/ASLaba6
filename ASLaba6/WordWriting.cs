using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using word = Microsoft.Office.Interop.Word;

namespace ASLaba6
{
    public class WordWriting
    {
        private word.Application wordapp;
        private word.Document worddocument;
        private word.Documents worddocuments;
        private string OutputPath;
        public WordWriting(string outputpath, string template = @"C:\DOC\Shablon.doc")
        {
            OutputPath = outputpath;
            wordapp = new word.Application();
            wordapp.Visible = false;
            Object newtemplate = false;
            Object documenttype = word.WdNewDocumentType.wdNewBlankDocument;
            Object visible = false;

            wordapp.Documents.Add(template, newtemplate, ref documenttype, ref visible);

            worddocuments = wordapp.Documents;
            worddocument = worddocuments.Open(template);
            //worddocument = (word.Document)worddocuments.GetEnumerator().Current;
            worddocument.Activate();
        }

        public void Replace(string replaceword, string replacement)
        {
            word.Range range = worddocument.StoryRanges[word.WdStoryType.wdMainTextStory];
            range.Find.ClearFormatting();

            range.Find.Execute(FindText: replaceword, ReplaceWith: replacement);

            TrySave();
        }

        public void ReplaceBookmark(string replcebookmark, string replacement)
        {
            worddocument.Bookmarks[replcebookmark].Range.Text = replacement;

            TrySave();
        }

        public void TrySave()
        {
            try
            {
                worddocument.SaveAs(OutputPath, word.WdSaveFormat.wdFormatDocument);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Close()
        {
            Object saveChanges = word.WdSaveOptions.wdPromptToSaveChanges;
            Object originalFormat = word.WdOriginalFormat.wdWordDocument;
            Object routeDocument = Type.Missing;

            wordapp.Quit(ref saveChanges, ref originalFormat, ref routeDocument);
        }
    }
}
