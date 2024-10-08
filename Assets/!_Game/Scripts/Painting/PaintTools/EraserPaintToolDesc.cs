using CreatyTest.Painting.Paintables;
using UnityEngine;

namespace CreatyTest.Painting.PaintTools
{
  [CreateAssetMenu(menuName = "Eraser_PaintTool", fileName = "Game/EraserPaintTool")]
  public class EraserPaintToolDesc : PaintToolDesc
  {
    private static readonly int m_sTexturePropertyId = Shader.PropertyToID("_PaintTex");

    public override void Init(Paintable paintableServicePaintable)
    {
      var paintable = FindObjectOfType<Paintable>();
      PaintMaterial.SetTexture(m_sTexturePropertyId, paintable.OriginalTexture);
    }
  }
}