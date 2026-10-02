using System;
using UnityEngine;

namespace NTPackage.ImageHp
{
    public static class ImageHelper
    {
        public static string ConvertTo64x64Base64(byte[] imageBytes)
        {
            // 1. Load image
            Texture2D source = new Texture2D(2, 2);
            if (!source.LoadImage(imageBytes))
            {
                UnityEngine.Object.Destroy(source);
                return null;
            }

            // 2. Crop thành hình vuông ở giữa
            int cropSize = Mathf.Min(source.width, source.height);

            int startX = (source.width - cropSize) / 2;
            int startY = (source.height - cropSize) / 2;

            Color[] pixels = source.GetPixels(
                startX,
                startY,
                cropSize,
                cropSize
            );

            Texture2D cropped = new Texture2D(
                cropSize,
                cropSize,
                TextureFormat.RGBA32,
                false
            );

            cropped.SetPixels(pixels);
            cropped.Apply();

            // 3. Resize về 64x64
            Texture2D resized = ResizeTexture(cropped, 64, 64);

            // 4. Encode thành PNG
            byte[] pngBytes = resized.EncodeToPNG();

            // 5. Chuyển thành Base64 string
            string base64 = Convert.ToBase64String(pngBytes);

            UnityEngine.Object.Destroy(source);
            UnityEngine.Object.Destroy(cropped);
            UnityEngine.Object.Destroy(resized);

            return base64;
        }

        private static Texture2D ResizeTexture(
            Texture2D source,
            int width,
            int height)
        {
            RenderTexture rt = RenderTexture.GetTemporary(
                width,
                height,
                0,
                RenderTextureFormat.ARGB32
            );

            RenderTexture previous = RenderTexture.active;

            Graphics.Blit(source, rt);

            RenderTexture.active = rt;

            Texture2D result = new Texture2D(
                width,
                height,
                TextureFormat.RGBA32,
                false
            );

            result.ReadPixels(
                new Rect(0, 0, width, height),
                0,
                0
            );

            result.Apply();

            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(rt);

            return result;
        }


        public static Texture2D Base64ToTexture(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);

            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);

            return texture;
        }

        public static Sprite TextureToSprite(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }


}