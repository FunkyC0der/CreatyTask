using CreatyTest.Painting.Paintables;
using CreatyTest.Painting.PaintTools;
using UnityEngine;
using VContainer;

namespace CreatyTest.Painting
{
  public class PaintCursor : MonoBehaviour
  {
    public Camera Camera;
    
    private PaintToolService m_paintTools;

    private PaintToolDesc PaintTool => m_paintTools.PaintTool;

    [Inject]
    private void Construct(PaintToolService paintTools) => 
      m_paintTools = paintTools;

    private void Update()
    {
      if (!Input.GetMouseButton(0))
        return;

      if (!Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        return;

      var paintable = hit.transform.GetComponent<Paintable>();
      if (paintable == null)
        return;

      PaintTool.UpdatePosition(hit.textureCoord);
      paintable.Paint(PaintTool.PaintMaterial);
    }
  }
}