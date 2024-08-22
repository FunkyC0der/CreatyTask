using CreatyTest.Extensions;
using UnityEngine;

namespace CreatyTest.Painting.Paintables
{
  [RequireComponent(typeof(Renderer))]
  public class Paintable : MonoBehaviour
  {
    private static readonly Vector2Int m_sPaintTextureDefaultSize = new(1024, 1024);

    public Color EmptyTextureColor = Color.white;

    [HideInInspector]
    public PaintableDesc Desc;

    private Renderer m_renderer;

    private RenderTexture m_paintTexture;
    private RenderTexture m_prevPaintTexture;
    
    public Texture OriginalTexture { get; private set; }

    private void Awake()
    {
      m_renderer = GetComponent<Renderer>();

      InitPaintTextures();
    }

    public void SetTexture(Texture texture) => 
      Graphics.Blit(texture, m_paintTexture);

    public Texture2D GetTexture() => 
      m_paintTexture.CloneToTexture2D();

    public void Paint(Material paintMaterial)
    {
      Graphics.Blit(m_paintTexture, m_prevPaintTexture);
      Graphics.Blit(m_prevPaintTexture, m_paintTexture, paintMaterial);
    }

    public void Clear() => 
      SetTexture(OriginalTexture);

    private void InitPaintTextures()
    {
      Material material = m_renderer.material;

      if (material.mainTexture)
      {
        OriginalTexture = material.mainTexture;
        m_paintTexture = material.mainTexture.CloneToRenderTexture();
      }
      else
      {
        OriginalTexture = CreateSolidTexture();
        m_paintTexture = OriginalTexture.CloneToRenderTexture(m_sPaintTextureDefaultSize);
      }

      material.mainTexture = m_paintTexture;
      m_prevPaintTexture = m_paintTexture.CloneToRenderTexture();
    }

    private Texture CreateSolidTexture()
    {
      Texture2D solidTexture = new(1, 1, TextureFormat.RGB24, false);
      solidTexture.SetPixel(0, 0, EmptyTextureColor);
      solidTexture.Apply();
      return solidTexture;
    }
  }
}