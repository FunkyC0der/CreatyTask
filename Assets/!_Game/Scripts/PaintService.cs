using System;
using UnityEngine;

namespace CreatyTest
{
  public class PaintService : MonoBehaviour
  {
    public event Action OnPaintToolChanged;
    
    public Camera Camera;
    public PaintToolDesc PaintToolDesc;

    private PaintTool m_paintTool;
    
    public PaintTool PaintTool => m_paintTool;

    private void Awake() => 
      CreatePaintTool(PaintToolDesc);

    public void ChangePaintTool(PaintToolDesc paintToolDesc)
    {
      if (PaintToolDesc == paintToolDesc)
        return;
      
      Destroy(m_paintTool);
      CreatePaintTool(paintToolDesc);
    }

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

    private void CreatePaintTool(PaintToolDesc paintToolDesc)
    {
      m_paintTool = Instantiate(paintToolDesc.Prefab, transform);
      PaintToolDesc = paintToolDesc;
      
      OnPaintToolChanged?.Invoke();
    }
  }
}