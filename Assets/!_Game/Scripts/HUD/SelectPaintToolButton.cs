using CreatyTest.Painting.PaintTools;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CreatyTest.HUD
{
  [RequireComponent(typeof(Button))]
  public class SelectPaintToolButton : MonoBehaviour
  {
    public PaintToolDesc PaintToolDesc;
    public PaintToolService PaintTools;

    private Button m_button;

    private void Awake()
    {
      m_button = GetComponent<Button>();
      m_button.onClick.AddListener(SelectPaintTool);
    }

    private void Start()
    {
      UpdateView(PaintTools.PaintTool);
      PaintTools.OnPaintToolChanged += UpdateView;
    }

    private void UpdateView(PaintToolDesc paintTool) => 
      m_button.interactable = PaintToolDesc != paintTool;

    private void SelectPaintTool() => 
      PaintTools.ChangePaintTool(PaintToolDesc);
  }
}