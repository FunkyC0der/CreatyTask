using System;
using CreatyTest.SaveLoad;
using UnityEngine;

namespace CreatyTest.Painting
{
  public class PaintService : MonoBehaviour
  {
    public event Action OnPaintToolChanged;
    
    public Camera Camera;
    public PaintToolDesc PaintToolDesc;
    public PaintableService PaintableService;
    public SaveLoadService SaveLoadService;

    private void Awake()
    {
      PaintableService.OnPaintableChanged += ReInitPaintTool;
      
      PaintToolDesc paintToolDesc = SaveLoadService.LoadPaintTool() ?? PaintToolDesc;
      SetPaintTool(paintToolDesc);
    }

    public void ChangePaintTool(PaintToolDesc paintToolDesc)
    {
      if (PaintToolDesc == paintToolDesc)
        return;

      SetPaintTool(paintToolDesc);
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

      PaintToolDesc.UpdatePosition(hit.textureCoord);
      paintable.Paint(PaintToolDesc.PaintMaterial);
    }

    private void SetPaintTool(PaintToolDesc paintToolDesc)
    {
      PaintToolDesc = paintToolDesc;
      PaintToolDesc.Init(PaintableService.Paintable);
      SaveLoadService.SavePaintTool(PaintToolDesc);
      OnPaintToolChanged?.Invoke();
    }

    private void ReInitPaintTool(Paintable paintable) => 
      PaintToolDesc.Init(paintable);
  }
}