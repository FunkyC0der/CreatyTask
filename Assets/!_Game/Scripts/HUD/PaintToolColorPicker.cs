using CreatyTest.Painting;
using UnityEngine;

namespace CreatyTest.HUD
{
  public class PaintToolColorPicker : MonoBehaviour
  {
    public PaintService PaintService;
    public FlexibleColorPicker ColorPickerView;

    private PaintToolDesc m_paintTool;

    private void Start()
    {
      UpdatePaintTool();
      PaintService.OnPaintToolChanged += UpdatePaintTool;
      
      ColorPickerView.onColorChange.AddListener(ChangeColor);
    }

    private void UpdatePaintTool()
    {
      m_paintTool = PaintService.PaintToolDesc;
      
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