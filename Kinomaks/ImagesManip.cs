﻿using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace Kinomaks
{
    internal class ImagesManip
    {
        public static byte[] BitmapSourceToByteArray(BitmapSource image)
        {
            #region Кодирование картинки
            using (var stream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                return stream.ToArray();
            }
            #endregion
        }

        public static BitmapImage SelectImage()
        {
            #region Выбор картинки
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Выбрать изображение";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
                return new BitmapImage(new Uri(op.FileName));
            else
                return null;
            #endregion
        }

        public static BitmapImage NewImage(Games game)
        {
            #region Декодирование картинки
            MemoryStream ms = new MemoryStream(game.Logo);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        public static BitmapImage NewImage(Players player)
        {
            MemoryStream ms = new MemoryStream(player.Photo);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        public static BitmapImage NewImage(Teams team)
        {
            MemoryStream ms = new MemoryStream(team.Logo);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        public static BitmapImage NewImage(Tournaments tournament)
        {
            MemoryStream ms = new MemoryStream(tournament.Logo);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
            #endregion
        }
    }
}