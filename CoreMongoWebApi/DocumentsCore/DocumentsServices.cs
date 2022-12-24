using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;


namespace DocumentsCore
{
    public class DocumentsServices : IDocumentsServices
    {

        private readonly IMongoCollection<Document> _documents;
        private readonly IMongoDatabase _database;
        public readonly int maxFileSize = 16777216; //16 MB 

        public DocumentsServices(IDbClient dbClient)
        {
            _documents = dbClient.GetDocumentsCollection();
            _database = dbClient.GetDatabase();
        }


        public Document AddDocument(Document document)
        {
            _documents.InsertOne(document);
            if (document.FileName.Length > 0)
            {
                string fname = $@"{document.FileName}";
                FileStream fileStream =  File.OpenRead(fname);
                if (fileStream.Length < maxFileSize) //16MB
                {
                    byte[] fcontent = new byte[fileStream.Length];
                    using (BinaryReader fReader = new BinaryReader(fileStream))
                    {
                        fcontent = fReader.ReadBytes((int)fileStream.Length);
                    }
                    document.FileContent = Convert.ToBase64String(fcontent);
                    //use Convert before writing to file: byte[] fcontent = Convert.FromBase64String(contentAsString);
                    _documents.ReplaceOne(b => b.Id == document.Id, document);
                }
                else //GridFS for large files
                {
                    var fs = new GridFSBucket(_database);
                    //GridFSUploadOptions options = new GridFSUploadOptions();
                    //options.ChunkSizeBytes = maxFileSize;

                    var id = fs.UploadFromStream(fname, fileStream);
                    document.FileContent = id.ToString();
                    _documents.ReplaceOne(b => b.Id == document.Id, document);
                    //download by file name: fs.DownloadToStreamByName(fname, fileStream);
                    //or by id:     fs.DownloadToStream(id, fileStream);
                }
            }
            return document;
        }

        public void DeleteDocument(string id)
        {
            _documents.DeleteOne(document => document.Id == id);
        }

        public Document GetDocument(string id) => _documents.Find(document => document.Id == id).First();

        public List<Document> GetDocuments() => _documents.Find(documents => true).ToList();

        public Document UpdateDocument(Document document)
        {
            GetDocument(document.Id);
            _documents.ReplaceOne(b => b.Id == document.Id, document);
            return document;
        }
    }
}
