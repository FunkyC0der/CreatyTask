using UnityEngine;

namespace CreatyTest
{
  public class PaintTool : MonoBehaviour
  {
    private static readonly int m_sPositionPropertyId = Shader.PropertyToID("_Position");

    public Material PaintMaterial;

    public void UpdatePosition(Vector2 position) => 
      PaintMaterial.SetVector(m_sPositionPropertyId, position);
  }
}