using System;
using System.Collections.Generic;

namespace BlobStorage.Data.EF;

public partial class LogAlbum
{
    public int Id { get; set; }

    public string NombreImagen { get; set; } = null!;

    public string NombreUser { get; set; } = null!;

    public DateTime DateTime { get; set; }
}
