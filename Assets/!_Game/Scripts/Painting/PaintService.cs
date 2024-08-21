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
    public SaveLoadService SaveLoadService;

    private void Awake()
    {
      PaintToolDesc savedPaintToolDesc = SaveLoadService.LoadCurrentPaintTool();
      if (savedPaintToolDesc)
      {
        ChangePaintTool(savedPaintToolDesc);
      }
      else
      {
        PaintToolDesc.Init();
        SaveLoadService.SaveCurrentPaintTool(PaintToolDesc);
      }
    }

    public void ChangePaintTool(PaintToolDesc paintToolDesc)
    {
      if (PaintToolDesc == paintToolDesc)
        return;

      PaintToolDesc = paintToolDesc;
      PaintToolDesc.Init();
      
      SaveLoadService.SaveCurrentPaintTool(PaintToolDesc);
      
      OnPaintToolChanged?.Invoke();
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
  }
}