using CreatyTest.Painting.PaintTools;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace CreatyTest.HUD
{
  [RequireComponent(typeof(Button))]
  public class SelectPaintToolButton : MonoBehaviour
  {
    public PaintToolDesc PaintToolDesc;
    public PaintToolService PaintTools;

    private PaintToolService m_paintTools;
    private Button m_button;

    private PaintToolDesc PaintTool => m_paintTools.PaintTool;
    
    [Inject]
    private void Construct(PaintToolService paintTools)
    {
      m_paintTools = paintTools;
      PaintTools.OnPaintToolChanged += UpdateView;

      m_button = GetComponent<Button>();
      m_button.onClick.AddListener(SelectPaintTool);
      
      UpdateView();
    }

    private void UpdateView() => 
      m_button.interactable = PaintToolDesc != PaintTool;

    private void SelectPaintTool() => 
      PaintTools.ChangePaintTool(PaintToolDesc);
  }
}