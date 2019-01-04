using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SakalliTicaret.UI.WEB.Areas.Panel
{
    public class Functions
    {
        public string ImageUpload(HttpPostedFileBase fu, int Ksize)
        {
            System.Drawing.Image orjinalFoto = null;
            HttpPostedFileBase jpeg_image_upload = fu;
            orjinalFoto = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
            ImageResize(orjinalFoto, Ksize,ImageName(fu.FileName,"jpg"));
            return ImageName(fu.FileName, "jpg");
        }
        protected static void ImageResize(System.Drawing.Image orjinalFoto, int boyut, string dosyaAdi)
        {
            System.Drawing.Bitmap islenmisFotograf = null;
            System.Drawing.Graphics grafik = null;

            int hedefGenislik = boyut;
            int hedefYukseklik = boyut;
            int new_width, new_height;

            new_height = (int)Math.Round(((float)orjinalFoto.Height * (float)boyut) / (float)orjinalFoto.Width);
            new_width = hedefGenislik;
            hedefYukseklik = new_height;
            new_width = new_width > hedefGenislik ? hedefGenislik : new_width;
            new_height = new_height > hedefYukseklik ? hedefYukseklik : new_height;

            islenmisFotograf = new System.Drawing.Bitmap(hedefGenislik, hedefYukseklik);
            grafik = System.Drawing.Graphics.FromImage(islenmisFotograf);
            grafik.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), new System.Drawing.Rectangle(0, 0, hedefGenislik, hedefYukseklik));
            int paste_x = (hedefGenislik - new_width) / 2;
            int paste_y = (hedefYukseklik - new_height) / 2;

            grafik.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            grafik.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            grafik.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;

            System.Drawing.Imaging.ImageCodecInfo codec = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()[1];
            System.Drawing.Imaging.EncoderParameters eParams = new System.Drawing.Imaging.EncoderParameters(1);
            eParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 95L);

            grafik.DrawImage(orjinalFoto, paste_x, paste_y, new_width, new_height);
            islenmisFotograf.Save(HttpContext.Current.Server.MapPath("~/Content/Images/Products/" + dosyaAdi), codec, eParams);
        }
        private string ImageName(string baslik, string uzanti)
        {

            string _Duyurubaslik = baslik.ToLower();
            string[] dizi = new string[baslik.Length];
            for (int i = 0; i < dizi.Length; i++)
            {
                dizi[i] = _Duyurubaslik[i].ToString();
            }
            for (int i = 0; i < dizi.Length; i++)
            {
                if (dizi[i] == " ")
                {
                    dizi[i] = "_";
                }
            }
            string Yeni = null;
            for (int i = 0; i < dizi.Length; i++)
            {
                Yeni = Yeni + dizi[i].ToString();
            }
            return Yeni + "_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "."+uzanti ;
        }
    }
}