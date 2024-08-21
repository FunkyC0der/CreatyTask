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
      ColorPickerView.color = m_paintTool.Color;
    }

    private void ChangeColor(Color color) => 
      m_paintTool.Color = color;
  }
}