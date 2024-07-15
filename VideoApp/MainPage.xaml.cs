using Microsoft.Maui.Controls;
using System;
using System.IO;
using VideoApp.Models;
using VideoApp.Data;

namespace VideoApp
{
    public partial class MainPage : ContentPage
    {
        VideoDatabase _database;

        public MainPage()
        {
            InitializeComponent();
            _database = new VideoDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "videos.db3"));
        }

        private async void TomarVideo_Clicked(object sender, EventArgs e)
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.Camera>();

                if (status != PermissionStatus.Granted)
                {
                    await DisplayAlert("Permiso requerido", "Se necesita permiso para acceder a la cámara.", "OK");
                    return;
                }

                var result = await MediaPicker.CaptureVideoAsync();

                if (result?.FullPath != null)
                {
                    string rutaVideo = result.FullPath;
                    string nombreVideo = $"Video_{DateTime.Now:yyyyMMdd_HHmmss}.mp4"; 
                    await DisplayAlert("Éxito", $"Video guardado como: {nombreVideo}", "Aceptar");

                    
                    var video = new VideoItem { Nombre = nombreVideo, Ruta = rutaVideo };
                    await _database.SaveVideoAsync(video);

                 
                    mediaElement.Source = rutaVideo;
                    mediaElement.Play();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al capturar video: {ex.Message}", "Aceptar");
            }
        }

        private async void VerVideos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VideosPage());
        }
    }
}
