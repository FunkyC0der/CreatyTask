using CreatyTest.Painting.Paintables;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace CreatyTest.HUD
{
  public class SelectPaintableButton : MonoBehaviour
  {
    public PaintableDesc PaintableDesc;

    private PaintableService m_paintableService;
    private Button m_button;

    private Paintable Paintable => m_paintableService.Paintable;

    [Inject]
    private void Construct(PaintableService paintableService)
    {
      m_paintableService = paintableService;
      m_paintableService.OnPaintableChanged += UpdateView;

      m_button = GetComponent<Button>();
      m_button.onClick.AddListener(SelectPaintable);

      UpdateView();
    }

    private void SelectPaintable() => 
      m_paintableService.ChangePaintable(PaintableDesc);

    private void UpdateView() => 
      m_button.interactable = PaintableDesc != Paintable.Desc;
  }
}