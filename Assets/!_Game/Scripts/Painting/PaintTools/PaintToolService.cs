using System;
using CreatyTest.Painting.Paintables;
using CreatyTest.SaveLoad;
using UnityEngine;
using UnityEngine.Serialization;

namespace CreatyTest.Painting.PaintTools
{
  public class PaintToolService : MonoBehaviour
  {
    public event Action<PaintToolDesc> OnPaintToolChanged;
    
    public PaintToolDesc PaintTool;
    public PaintableService PaintableService;
    public SaveLoadService SaveLoadService;

    private void Awake()
    {
      PaintableService.OnPaintableChanged += ReInitPaintTool;
      
      PaintToolDesc paintTool = SaveLoadService.LoadPaintTool() ?? PaintTool;
      SetPaintTool(paintTool);
    }

    public void ChangePaintTool(PaintToolDesc paintTool)
    {
      if (PaintTool == paintTool)
        return;

      SetPaintTool(paintTool);
    }

    private void SetPaintTool(PaintToolDesc paintTool)
    {
      PaintTool = paintTool;
      PaintTool.Init(PaintableService.Paintable);
      SaveLoadService.SavePaintTool(PaintTool);
      
      OnPaintToolChanged?.Invoke(paintTool);
    }

    private void ReInitPaintTool(Paintable paintable) => 
      PaintTool.Init(paintable);
  }
}