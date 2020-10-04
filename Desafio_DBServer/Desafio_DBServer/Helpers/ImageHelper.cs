using System.Collections.Generic;
using Xamarin.Forms;

namespace Desafio_DBServer.Helpers
{
    public static class ImageHelper
    {
        private static List<string> imageNames = new List<string>();
        private static List<ImageSource> images = new List<ImageSource>();

        public static ImageSource ImgError
        {
            get
            {
                return GetImageResource("PokeBall.png");
            }
        }

        public static ImageSource GetImageResource(this string imageName)
        {
            try
            {
                imageName = "Desafio_DBServer.Resources.Images." + imageName;
                int index = imageNames.IndexOf(imageName);
                if (index >= 0)
                {
                    return images[index].CloneObject();
                }
                else
                {
                    imageNames.Add(imageName);
                    ImageSource imgSource = ImageSource.FromResource(imageName);
                    images.Add(imgSource);
                    return imgSource;
                }
            }
            catch
            {
                if (imageName.Equals("PokeBall.png"))
                    return null;

                return ImgError;
            }
        }
    }
}
