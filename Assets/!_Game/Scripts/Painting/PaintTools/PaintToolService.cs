using System;
using CreatyTest.Painting.Paintables;
using CreatyTest.SaveLoad;
using UnityEngine;
using VContainer;

namespace CreatyTest.Painting.PaintTools
{
  public class PaintToolService : MonoBehaviour
  {
    public event Action OnPaintToolChanged;
    
    public PaintToolDesc PaintTool;
    
    private PaintableService m_paintableService;

    [Inject]
    private void Construct(PaintableService paintableService)
    {
      m_paintableService = paintableService;
      m_paintableService.OnPaintableChanged += ReInitPaintTool;
      
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
      PaintTool.Init(m_paintableService.Paintable);
      SaveLoadService.SavePaintTool(PaintTool);
      
      OnPaintToolChanged?.Invoke();
    }

    private void ReInitPaintTool() => 
      PaintTool.Init(m_paintableService.Paintable);
  }
}