using CreatyTest.Painting.PaintTools;
using UnityEngine;
using UnityEngine.UI;

namespace CreatyTest.HUD
{
  public class PaintToolSizeSlider : MonoBehaviour
  {
    public PaintToolService PaintTools;
    public Slider Slider;

    public float MinSize;
    public float MaxSize;

    private float m_currentSize;
    private PaintToolDesc m_paintTool;

    private PaintToolDesc PaintTool => PaintTools.PaintTool;
    
    private void Start()
    {
      UpdateView(PaintTool);
      PaintTools.OnPaintToolChanged += UpdateView;

      Slider.minValue = MinSize;
      Slider.maxValue = MaxSize;
      
      Slider.onValueChanged.AddListener(ChangePaintToolSize);
    }

    private void UpdateView(PaintToolDesc paintTool)
    {
      gameObject.SetActive(paintTool.CanChangeSize);
      Slider.value = paintTool.Size;
    }

    private void ChangePaintToolSize(float value)
    {
      if(PaintTool.CanChangeSize)
        PaintTool.Size = value;
    }
  }
}