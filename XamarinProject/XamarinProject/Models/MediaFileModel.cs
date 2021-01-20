using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using Xamarin.Forms;

namespace XamarinProject.Models
{
    public class MediaFileModel
    {
        public string PreviewPath { get; set; }
        public ImageSource Path { get; set; }
        public MetafileType Type { get; set; }

        public MediaFileModel()
        {
        }

        public MediaFileModel(string previewPath, string path, MetafileType type)
        {
            PreviewPath = previewPath;
            Path = path;
            Type = type;
        }

    }
}
