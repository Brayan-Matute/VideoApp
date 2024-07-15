using SQLite;

namespace VideoApp.Models
{
    public class VideoItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Ruta { get; set; }
    }
}
