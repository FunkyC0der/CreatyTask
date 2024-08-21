using CreatyTest.Painting.Paintables;
using UnityEngine;
using UnityEngine.UI;

namespace CreatyTest.HUD
{
  public class SelectPaintableButton : MonoBehaviour
  {
    public PaintableDesc PaintableDesc;
    public PaintableService PaintableService;

    private Button m_button;

    private void Awake()
    {
      m_button = GetComponent<Button>();
      m_button.onClick.AddListener(SelectPaintable);
    }

    private void Start()
    {
      UpdateView(PaintableService.Paintable);
      PaintableService.OnPaintableChanged += UpdateView;
    }

    private void UpdateView(Paintable paintable) => 
      m_button.interactable = PaintableDesc != paintable.Desc;

    private void SelectPaintable() => 
      PaintableService.ChangePaintable(PaintableDesc);
  }
}