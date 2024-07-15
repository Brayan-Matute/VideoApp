using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VideoApp.Data;
using VideoApp.Models;

namespace VideoApp
{
    public partial class VideosPage : ContentPage
    {
        VideoDatabase _database;

        public ObservableCollection<VideoItem> Videos { get; set; }
        public Command<VideoItem> EliminarVideoCommand { get; }

        public VideosPage()
        {
            InitializeComponent();
            _database = new VideoDatabase(Path.Combine(FileSystem.AppDataDirectory, "videos.db3"));
            EliminarVideoCommand = new Command<VideoItem>(async (video) => await EliminarVideo(video));

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Videos = new ObservableCollection<VideoItem>(await _database.GetVideosAsync());
            videosCollectionView.ItemsSource = Videos;
        }

        private async Task EliminarVideo(VideoItem video)
        {
            try
            {
                await _database.DeleteVideoAsync(video);
                Videos.Remove(video);
                await DisplayAlert("Éxito al eliminar", $"Video: {video.Nombre} eliminado", "Aceptar");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al eliminar video: {ex.Message}", "Aceptar");
            }
        }
    }
}
