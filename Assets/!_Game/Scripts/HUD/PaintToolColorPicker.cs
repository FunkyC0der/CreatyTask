using CreatyTest.Painting.PaintTools;
using UnityEngine;
using VContainer;

namespace CreatyTest.HUD
{
  public class PaintToolColorPicker : MonoBehaviour
  {
    public FlexibleColorPicker ColorPickerView;
    
    private PaintToolService m_paintTools;

    private PaintToolDesc PaintTool => m_paintTools.PaintTool;

    [Inject]
    private void Construct(PaintToolService paintTools)
    {
      m_paintTools = paintTools;
      m_paintTools.OnPaintToolChanged += UpdateView;
      
      UpdateView();
    }

    private void OnEnable() => 
      ColorPickerView.onColorChange.AddListener(ChangePaintToolColor);

    private void OnDisable() => 
      ColorPickerView.onColorChange.RemoveListener(ChangePaintToolColor);

    private void UpdateView()
    {
      gameObject.SetActive(PaintTool.CanChangeColor);
      ColorPickerView.color = PaintTool.Color;
    }

    private void ChangePaintToolColor(Color color) => 
      PaintTool.Color = color;
  }
}