using UnityEngine;

namespace CreatyTest.Painting.Paintables
{
  [RequireComponent(typeof(Renderer))]
  public class Paintable : MonoBehaviour
  {
    private const RenderTextureFormat m_kRenderTextureFormat = RenderTextureFormat.ARGB32;
    
    private static readonly Vector2Int m_sPaintTextureDefaultSize = new(1024, 1024);

    public Color EmptyTextureColor = Color.white;

    [HideInInspector]
    public PaintableDesc Desc;

    private Renderer m_renderer;

    private Texture m_originalTexture;
    private RenderTexture m_paintTexture;
    private RenderTexture m_prevPaintTexture;
    
    public Texture OriginalTexture => m_originalTexture;

    private void Awake()
    {
      m_renderer = GetComponent<Renderer>();

      m_paintTexture = CreateNewRenderTexture();
      m_renderer.material.mainTexture = m_paintTexture;

      m_prevPaintTexture = new RenderTexture(m_paintTexture.descriptor);
      Graphics.CopyTexture(m_paintTexture, m_prevPaintTexture);
    }

    public void SetTexture(Texture texture) => 
      Graphics.Blit(texture, m_paintTexture);

    public Texture2D GetTexture()
    {
      RenderTexture cacheActiveRT = RenderTexture.active;
      RenderTexture.active = m_paintTexture;

      Texture2D texture = new Texture2D(m_paintTexture.width, m_paintTexture.height, TextureFormat.ARGB32, false);
      texture.ReadPixels(new Rect(0, 0, m_paintTexture.width, m_paintTexture.height), 0, 0);
      texture.Apply();

      RenderTexture.active = cacheActiveRT;
      return texture;
    }

    public void Paint(Material paintMaterial)
    {
      Graphics.Blit(m_paintTexture, m_prevPaintTexture);
      Graphics.Blit(m_prevPaintTexture, m_paintTexture, paintMaterial);
    }

    private RenderTexture CreateNewRenderTexture()
    {
      RenderTexture renderTexture;
      Material material = m_renderer.material;

      if (material.mainTexture)
      {
        renderTexture = new RenderTexture(material.mainTexture.width, material.mainTexture.height, 0, m_kRenderTextureFormat);
        Graphics.Blit(material.mainTexture, renderTexture);
      
        m_originalTexture = material.mainTexture;
      }
      else
      {
        renderTexture = new RenderTexture(
          m_sPaintTextureDefaultSize.x,
          m_sPaintTextureDefaultSize.y,
          0,
          m_kRenderTextureFormat);

        Texture2D solidTexture = new(1, 1);
        solidTexture.SetPixel(0, 0, EmptyTextureColor);
        solidTexture.Apply();
        
        Graphics.Blit(solidTexture, renderTexture);

        m_originalTexture = solidTexture;
      }

      return renderTexture;
    }
  }
}