using CreatyTest.Painting;
using UnityEngine;
using UnityEngine.UI;

namespace CreatyTest.HUD
{
  [RequireComponent(typeof(Button))]
  public class SelectPaintToolButton : MonoBehaviour
  {
    public PaintService PaintService;
    public PaintToolDesc PaintToolDesc;

    private Button m_button;

    private void Awake()
    {
      m_button = GetComponent<Button>();
      m_button.onClick.AddListener(SelectPaintTool);
    }

    private void Start()
    {
      UpdateView();
      PaintService.OnPaintToolChanged += UpdateView;
    }

    private void UpdateView() => 
      m_button.interactable = PaintToolDesc != PaintService.PaintToolDesc;

    private void SelectPaintTool() => 
      PaintService.ChangePaintTool(PaintToolDesc);
  }
}