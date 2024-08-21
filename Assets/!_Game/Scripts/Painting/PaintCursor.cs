using CreatyTest.Painting.Paintables;
using CreatyTest.Painting.PaintTools;
using UnityEngine;
using UnityEngine.Serialization;

namespace CreatyTest.Painting
{
  public class PaintCursor : MonoBehaviour
  {
    public Camera Camera;
    public PaintToolService PaintTools;
    
    private void Update()
    {
      if (!Input.GetMouseButton(0))
        return;

      if (!Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        return;

      var paintable = hit.transform.GetComponent<Paintable>();
      if (paintable == null)
        return;

      PaintTools.PaintTool.UpdatePosition(hit.textureCoord);
      paintable.Paint(PaintTools.PaintTool.PaintMaterial);
    }
  }
}