using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AppEjercicio1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button btnDescargar, btnExtraer;
        ImageView Imagen;
        EditText txtNombre;
        string ruta;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            SupportActionBar.Hide();

            btnDescargar = FindViewById<Button>(Resource.Id.btnDownload);
            btnExtraer = FindViewById<Button>(Resource.Id.btnLoad);
            Imagen = FindViewById<ImageView>(Resource.Id.imagen);
            txtNombre = FindViewById<EditText>(Resource.Id.txtName);

            btnDescargar.Click += ColocarImagen;
            btnExtraer.Click += delegate
            {
                var carpeta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var archivo = txtNombre.Text + ".jpg";
                ruta = Path.Combine(carpeta, archivo);
                Android.Net.Uri rutaImagen = Android.Net.Uri.Parse(ruta);
                Imagen.SetImageURI(rutaImagen);
            };
        }

        public async Task<string> DownloadImage()
        {
            var cliente = new WebClient();
            byte[] datosImagen = await cliente.DownloadDataTaskAsync("https://imag.malavida.com/articulos/normal-size/imagen-a-jpg-6929b44b-20180916-pca.jpg");
            var carpeta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var archivo = txtNombre.Text + ".jpg";
            ruta = Path.Combine(carpeta, archivo);
            File.WriteAllBytes(ruta, datosImagen);
            return ruta;
        }

        async void ColocarImagen(object sender, EventArgs e)
        {
            var path = await DownloadImage();
            Android.Net.Uri rutaImagen = Android.Net.Uri.Parse(path);
            Imagen.SetImageURI(rutaImagen);
            Toast.MakeText(this, "Imagen Colocada",
                        ToastLength.Long).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}