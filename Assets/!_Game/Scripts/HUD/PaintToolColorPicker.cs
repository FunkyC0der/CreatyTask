using CreatyTest.Painting.PaintTools;
using UnityEngine;
using VContainer;

namespace CreatyTest.HUD
{
  public class PaintToolColorPicker : MonoBehaviour
  {
    public FlexibleColorPicker ColorPickerView;
    
    private PaintToolService m_paintToolService;

    private PaintToolDesc PaintTool => m_paintToolService.PaintTool;

    [Inject]
    private void Construct(PaintToolService paintToolService)
    {
      m_paintToolService = paintToolService;
      m_paintToolService.OnPaintToolChanged += UpdateView;
      
      UpdateView();
    }

    private void OnEnable() => 
      ColorPickerView.onColorChange.AddListener(ChangePaintToolColor);

    private void OnDisable() => 
      ColorPickerView.onColorChange.RemoveListener(ChangePaintToolColor);

    private void UpdateView()
    {
      ColorPickerView.color = PaintTool.Color;
      gameObject.SetActive(PaintTool.CanChangeColor);
    }

    private void ChangePaintToolColor(Color color) => 
      PaintTool.Color = color;
  }
}