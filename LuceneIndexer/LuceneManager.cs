using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
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
            Document document = new Document();
            document.Add(new Field("Id", record[0], Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("Text", record[1], Field.Store.YES, Field.Index.ANALYZED));
            _indexWriter.AddDocument(document);
            _indexWriter.Optimize();
        }

        public TopDocs Search(string searchString)
        {
            IndexSearcher indexSearcher = new IndexSearcher(_directory);
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "Parser", _analyzer);
            Query query = parser.Parse(searchString);
            TopDocs results = indexSearcher.Search(query, 100);
            return results;
        }

        public void Destroy()
        {
            _indexWriter.Dispose();
            _directory.Dispose();
        }
    }
}
