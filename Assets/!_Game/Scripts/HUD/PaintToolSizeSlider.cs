using UnityEngine;
using UnityEngine.UI;

namespace CreatyTest.HUD
{
  public class PaintToolSizeSlider : MonoBehaviour
  {
    public PaintService PaintService;
    public Slider Slider;

    public float MinSize;
    public float MaxSize;

    private float m_currentSize;
    private PaintToolDesc m_paintTool;

    
    private void Start()
    {
      UpdatePaintTool();
      PaintService.OnPaintToolChanged += UpdatePaintTool;

      Slider.minValue = MinSize;
      Slider.maxValue = MaxSize;
      
      Slider.onValueChanged.AddListener(ChangeSize);
    }

    private void UpdatePaintTool()
    {
      m_paintTool = PaintService.PaintToolDesc;

      gameObject.SetActive(m_paintTool.CanChangeSize);
      
      if (m_paintTool.CanChangeSize)
        Slider.value = m_paintTool.Size;
    }

    private void ChangeSize(float value) => 
      m_paintTool.Size = value;
  }
}