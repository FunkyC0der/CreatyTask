using UnityEngine;

namespace CreatyTest
{
  public class PaintTool : MonoBehaviour
  {
    private static readonly int m_sPositionPropertyId = Shader.PropertyToID("_Position");
    private static readonly int m_sColorPropertyId = Shader.PropertyToID("_Color");

    public Material PaintMaterial;

    public Color Color
    {
      get => PaintMaterial.GetColor(m_sColorPropertyId);
      set => PaintMaterial.SetVector(m_sColorPropertyId, value);
    }

    public void UpdatePosition(Vector2 position) => 
      PaintMaterial.SetVector(m_sPositionPropertyId, position);
  }
}