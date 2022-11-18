using BlobStorage.Data.EF;
using BlobStorage.Logica;
using Microsoft.AspNetCore.Mvc;

namespace BlobStorage.Controllers;

public class BlobStorageController : Controller
{
    private IServiceBlobStorage _serviceBlobStorage;
    private IServiceLogAlbum _serviceLogAlbum;

    public BlobStorageController(IServiceBlobStorage serviceBlobStorage, IServiceLogAlbum serviceLogAlbum)
    {
        _serviceBlobStorage = serviceBlobStorage;
        _serviceLogAlbum = serviceLogAlbum;
    }

    public async Task<IActionResult> Index()
    {
        List<string>imagenes = await _serviceBlobStorage.GetBlobAsync();
        return View(imagenes);
    }

    public IActionResult UploadImagen()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UploadImagen(string nombreImagen, string nombreUsuario, IFormFile imagenBBDD)
    {
        string extension = imagenBBDD.FileName.Split(".")[1];
        string fileName = nombreImagen.Trim() + "." + extension;
        using (Stream stream = imagenBBDD.OpenReadStream())
        {
            await _serviceBlobStorage.UploadBlobAsync(fileName, stream);
        }

        LogAlbum logAlbum = new LogAlbum();
        logAlbum.NombreUser = nombreUsuario;
        logAlbum.NombreImagen = nombreImagen;
        logAlbum.DateTime = DateTime.Now;
        
        _serviceLogAlbum.insertar(logAlbum);

        return RedirectToAction("Index");
    }

    public IActionResult Detalles()
    {
        return View();
    }

    public async Task<IActionResult> Delete(string nombre)
    {
        await _serviceBlobStorage.DeleteBlobAsync(nombre);
        return RedirectToAction("Index");
    }

    public IActionResult DeleteLog(int id)
    {
        _serviceLogAlbum.delete(id);
        return RedirectToAction("Logs");
    }

    public IActionResult Logs()
    {
        List<LogAlbum> logAlbum = _serviceLogAlbum.obtenerLogAlbumOrdenadosPorFecha();
        return View(logAlbum);
    }

}
