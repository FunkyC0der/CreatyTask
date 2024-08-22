using UnityEngine;

namespace CreatyTest.Extensions
{
  public static class TextureExtensions
  {
    public static Texture2D CloneToTexture2D(this RenderTexture renderTexture)
    {
      RenderTexture cacheActiveRT = RenderTexture.active;
      RenderTexture.active = renderTexture;

      var texture = new Texture2D(renderTexture.width, renderTexture.height);
      texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
      texture.Apply();

      RenderTexture.active = cacheActiveRT;
      return texture;
    }

    public static RenderTexture CloneToRenderTexture(this Texture texture, int width, int height)
    {
      var renderTexture = new RenderTexture(width, height, 0);
      Graphics.Blit(texture, renderTexture);
      return renderTexture;
    }

    public static RenderTexture CloneToRenderTexture(this Texture texture, Vector2Int size) =>
      texture.CloneToRenderTexture(size.x, size.y);
    
    public static RenderTexture CloneToRenderTexture(this Texture texture) =>
      texture.CloneToRenderTexture(texture.width, texture.height);

  }
}