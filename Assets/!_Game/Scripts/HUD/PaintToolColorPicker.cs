using CreatyTest.Painting.PaintTools;
using UnityEngine;
using UnityEngine.Serialization;

namespace CreatyTest.HUD
{
  public class PaintToolColorPicker : MonoBehaviour
  {
    public PaintToolService PaintTools;
    public FlexibleColorPicker ColorPickerView;

    private PaintToolDesc PaintTool => PaintTools.PaintTool;

    private void Start()
    {
      UpdateView(PaintTool);
      PaintTools.OnPaintToolChanged += UpdateView;
      
      ColorPickerView.onColorChange.AddListener(ChangePaintToolColor);
    }

    private void UpdateView(PaintToolDesc paintTool)
    {
      gameObject.SetActive(paintTool.CanChangeColor);
      ColorPickerView.color = paintTool.Color;
    }

    private void ChangePaintToolColor(Color color)
    {
      if(PaintTool.CanChangeColor)
        PaintTool.Color = color;
    }
  }
}