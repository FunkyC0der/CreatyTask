using UnityEngine;

namespace CreatyTest
{
  [RequireComponent(typeof(Renderer))]
  public class Painter : MonoBehaviour
  {
    private static readonly Vector2Int m_sPaintTextureDefaultSize = new(1024, 1024);
    private static readonly int m_sPenPositionPropertyId = Shader.PropertyToID("_PenPosition");

    public Color EmptyTextureColor = Color.white;
    public Material PaintMaterial;

    private Renderer m_renderer;
    private RenderTexture m_paintTexture;
    private RenderTexture m_prevPaintTexture;

    private void Awake()
    {
      m_renderer = GetComponent<Renderer>();

      m_paintTexture = CreateNewRenderTexture();
      m_renderer.material.mainTexture = m_paintTexture;

      m_prevPaintTexture = new RenderTexture(m_paintTexture.descriptor);
      Graphics.CopyTexture(m_paintTexture, m_prevPaintTexture);
    }

    public void Paint(Vector2 textureCoord)
    {
      PaintMaterial.SetVector(m_sPenPositionPropertyId, textureCoord);
      Graphics.Blit(m_paintTexture, m_prevPaintTexture);
      Graphics.Blit(m_prevPaintTexture, m_paintTexture, PaintMaterial);
    }
    
    private RenderTexture CreateNewRenderTexture()
    {
      RenderTexture renderTexture;
      Material material = m_renderer.material;

      if (material.mainTexture)
      {
        renderTexture = new RenderTexture(material.mainTexture.width, material.mainTexture.height, 0, material.mainTexture.graphicsFormat);
        Graphics.Blit(material.mainTexture, renderTexture);
      }
      else
      {
        renderTexture = new RenderTexture(
          m_sPaintTextureDefaultSize.x,
          m_sPaintTextureDefaultSize.y,
          0,
          RenderTextureFormat.ARGBFloat);

        Texture2D solidTexture = new(1, 1);
        solidTexture.SetPixel(0, 0, EmptyTextureColor);
        solidTexture.Apply();
        
        Graphics.Blit(solidTexture, renderTexture);
      }

      return renderTexture;
    }
  }
}