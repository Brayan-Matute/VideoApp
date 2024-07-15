using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoApp.Models;

namespace VideoApp.Data
{
    public class VideoDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public VideoDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<VideoItem>().Wait();
        }

        public Task<List<VideoItem>> GetVideosAsync()
        {
            return _database.Table<VideoItem>().ToListAsync();
        }

        public Task<int> SaveVideoAsync(VideoItem video)
        {
            if (video.Id != 0)
            {
                return _database.UpdateAsync(video);
            }
            else
            {
                return _database.InsertAsync(video);
            }
        }

        public Task<int> DeleteVideoAsync(VideoItem video)
        {
            return _database.DeleteAsync(video);
        }
    }
}
