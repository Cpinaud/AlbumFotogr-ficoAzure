using BlobStorage.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorage.Logica
{
    public interface IServiceLogAlbum 
    {
        void delete(int id);
        void insertar(LogAlbum logAlbum);

        List<LogAlbum> obtenerLogAlbumOrdenadosPorFecha();

    }
    public class ServiceLogAlbum : IServiceLogAlbum
    {
        private BlobStorageContext _blobStorageContext;

        public ServiceLogAlbum(BlobStorageContext blobStorageContext)
        {
            _blobStorageContext = blobStorageContext;
        }

        public void insertar(LogAlbum logAlbum)
        {
            _blobStorageContext.LogAlbums.Add(logAlbum);
            _blobStorageContext.SaveChanges();
        }

        public List<LogAlbum> obtenerLogAlbumOrdenadosPorFecha()
        {
            return _blobStorageContext.LogAlbums.OrderBy(f => f.DateTime).ToList();
        }

        public void delete(int id)
        {
            LogAlbum logAlbum = _blobStorageContext.LogAlbums.Find(id);
            _blobStorageContext.LogAlbums.Remove(logAlbum);
            _blobStorageContext.SaveChanges();
        }
    }
}
