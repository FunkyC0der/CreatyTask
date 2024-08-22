using CreatyTest.Painting.PaintTools;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace CreatyTest.HUD
{
  public class PaintToolSizeSlider : MonoBehaviour
  {
    public Slider Slider;
    public float MinSize;
    public float MaxSize;

    private PaintToolService m_paintTools;
    
    private PaintToolDesc PaintTool => m_paintTools.PaintTool;
    
    [Inject]
    private void Construct(PaintToolService paintTools)
    {
      m_paintTools = paintTools;
      m_paintTools.OnPaintToolChanged += UpdateView;

      Slider.minValue = MinSize;
      Slider.maxValue = MaxSize;
      
      UpdateView();
    }

    private void OnEnable() => 
      Slider.onValueChanged.AddListener(ChangePaintToolSize);

    private void OnDisable() => 
      Slider.onValueChanged.RemoveListener(ChangePaintToolSize);

    private void UpdateView()
    {
      gameObject.SetActive(PaintTool.CanChangeSize);
      Slider.value = PaintTool.Size;
    }

    private void ChangePaintToolSize(float value) => 
      PaintTool.Size = value;
  }
}