using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System.Collections.Generic;
using static Lucene.Net.Index.IndexWriter;

namespace LuceneIndexer
{
    public class LuceneManager
    {
        private IndexWriter _indexWriter;
        private Directory _directory;
        private Analyzer _analyzer;
        private MaxFieldLength _maxFieldLength;
        private ISet<string> stopWords = new HashSet<string>() { "the", "of", "a", "an" };

        public LuceneManager()
        {
            _directory = FSDirectory.Open(@"D:\LuceneData");
            _analyzer = new StopAnalyzer(Lucene.Net.Util.Version.LUCENE_30, stopWords);
            _maxFieldLength = new MaxFieldLength(100);
            _indexWriter = new IndexWriter(_directory, _analyzer, _maxFieldLength);
        }

        public void Index(List<string> record)
        {

        }
    }
}
