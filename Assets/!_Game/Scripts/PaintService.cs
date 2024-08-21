using System;
using UnityEngine;

namespace CreatyTest
{
  public class PaintService : MonoBehaviour
  {
    public Camera Camera;
    public PaintToolDesc PaintToolDesc;

    private PaintTool m_paintTool;

    private void Awake() => 
      m_paintTool = Instantiate(PaintToolDesc.Prefab, transform);

    private void Update()
    {
      if (!Input.GetMouseButton(0))
        return;

      if (!Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        return;

      var paintable = hit.transform.GetComponent<Paintable>();
      if (paintable == null)
        return;

      m_paintTool.UpdatePosition(hit.textureCoord);
      paintable.Paint(m_paintTool.PaintMaterial);
    }
  }
}