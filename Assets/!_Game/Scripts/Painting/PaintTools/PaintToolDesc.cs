using CreatyTest.Painting.Paintables;
using UnityEngine;

namespace CreatyTest.Painting.PaintTools
{
  [CreateAssetMenu(fileName = "_PaintTool", menuName = "Game/PaintTool")]
  public class PaintToolDesc : ScriptableObject
  {
    private static readonly int m_sPositionPropertyId = Shader.PropertyToID("_Position");
    private static readonly int m_sColorPropertyId = Shader.PropertyToID("_Color");
    private static readonly int m_sSizePropertyId = Shader.PropertyToID("_Size");

    public Material PaintMaterial;
    
    public bool CanChangeSize = true;
    public bool CanChangeColor = true;

    public Color Color
    {
      get => PaintMaterial.GetColor(m_sColorPropertyId);
      set => PaintMaterial.SetVector(m_sColorPropertyId, value);
    }

    public float Size
    {
      get => PaintMaterial.GetFloat(m_sSizePropertyId);
      set => PaintMaterial.SetFloat(m_sSizePropertyId, value);
    }

    public void UpdatePosition(Vector2 position) => 
      PaintMaterial.SetVector(m_sPositionPropertyId, position);
    
    public virtual void Init(Paintable paintableServicePaintable) { }
  }
}