using UnityEngine;

namespace CreatyTest.Painting
{
  [CreateAssetMenu(menuName = "Eraser_PaintTool", fileName = "EraserPaintToolDesc")]
  public class EraserPaintToolDesc : PaintToolDesc
  {
    private static readonly int m_sTexturePropertyId = Shader.PropertyToID("_PaintTex");

    public override void Init()
    {
      var paintable = FindObjectOfType<Paintable>();
      PaintMaterial.SetTexture(m_sTexturePropertyId, paintable.OriginalTexture);
    }
  }
}