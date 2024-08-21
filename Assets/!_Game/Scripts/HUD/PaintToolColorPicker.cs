using CreatyTest.Painting.PaintTools;
using UnityEngine;
using UnityEngine.Serialization;

namespace CreatyTest.HUD
{
  public class PaintToolColorPicker : MonoBehaviour
  {
    [FormerlySerializedAs("PaintService")]
    public PaintToolService paintToolService;
    public FlexibleColorPicker ColorPickerView;

    private PaintToolDesc m_paintTool;

    private void Start()
    {
      UpdatePaintToolTool();
      paintToolService.OnPaintToolChanged += UpdatePaintToolTool;
      
      ColorPickerView.onColorChange.AddListener(ChangeColor);
    }

    private void UpdatePaintToolTool()
    {
      m_paintTool = paintToolService.PaintToolDesc;
      
      gameObject.SetActive(m_paintTool.CanChangeColor);
      ColorPickerView.color = m_paintTool.Color;
    }

    private void ChangeColor(Color color)
    {
      if(m_paintTool.CanChangeColor)
        m_paintTool.Color = color;
    }
  }
}