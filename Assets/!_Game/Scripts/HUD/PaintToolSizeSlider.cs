using CreatyTest.Painting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CreatyTest.HUD
{
  public class PaintToolSizeSlider : MonoBehaviour
  {
    [FormerlySerializedAs("PaintService")]
    public PaintToolService paintToolService;
    public Slider Slider;

    public float MinSize;
    public float MaxSize;

    private float m_currentSize;
    private PaintToolDesc m_paintTool;

    
    private void Start()
    {
      UpdatePaintToolTool();
      paintToolService.OnPaintToolChanged += UpdatePaintToolTool;

      Slider.minValue = MinSize;
      Slider.maxValue = MaxSize;
      
      Slider.onValueChanged.AddListener(ChangeSize);
    }

    private void UpdatePaintToolTool()
    {
      m_paintTool = paintToolService.PaintToolDesc;

      gameObject.SetActive(m_paintTool.CanChangeSize);
      Slider.value = m_paintTool.Size;
    }

    private void ChangeSize(float value)
    {
      if(m_paintTool.CanChangeSize)
        m_paintTool.Size = value;
    }
  }
}