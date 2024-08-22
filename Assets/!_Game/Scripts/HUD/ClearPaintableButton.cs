using CreatyTest.Painting.Paintables;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace CreatyTest.HUD
{
  [RequireComponent(typeof(Button))]
  public class ClearPaintableButton : MonoBehaviour
  {
    private PaintableService m_paintableService;
    private Button m_button;

    [Inject]
    private void Construct(PaintableService paintableService)
    {
      m_paintableService = paintableService;

      m_button = GetComponent<Button>();
      m_button.onClick.AddListener(Clear);
    }

    private void Clear()
    {
      m_paintableService.Paintable.Clear();
    }
  }
}