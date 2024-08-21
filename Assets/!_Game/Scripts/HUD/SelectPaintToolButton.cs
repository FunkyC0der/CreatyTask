using CreatyTest.Painting.PaintTools;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CreatyTest.HUD
{
  [RequireComponent(typeof(Button))]
  public class SelectPaintToolButton : MonoBehaviour
  {
    [FormerlySerializedAs("PaintService")]
    public PaintToolService paintToolService;
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
      paintToolService.OnPaintToolChanged += UpdateView;
    }

    private void UpdateView() => 
      m_button.interactable = PaintToolDesc != paintToolService.PaintToolDesc;

    private void SelectPaintTool() => 
      paintToolService.ChangePaintTool(PaintToolDesc);
  }
}