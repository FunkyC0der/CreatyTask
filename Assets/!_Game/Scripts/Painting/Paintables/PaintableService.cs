using System;
using CreatyTest.SaveLoad;
using UnityEngine;

namespace CreatyTest.Painting.Paintables
{
  public class PaintableService : MonoBehaviour
  {
    public event Action<Paintable> OnPaintableChanged;
    
    public PaintableDesc PaintableDesc;
    public SaveLoadService SaveLoadService;

    private Paintable m_paintable;

    public Paintable Paintable => m_paintable;

    private void Awake()
    {
      PaintableDesc paintableDesc = SaveLoadService.LoadPaintable() ?? PaintableDesc;
      CreatePaintable(paintableDesc);
    }

    public void ChangePaintable(PaintableDesc paintableDesc)
    {
      if (PaintableDesc == paintableDesc)
        return;
      
      if(m_paintable)
        Destroy(m_paintable);

      CreatePaintable(paintableDesc);
    }

    private void CreatePaintable(PaintableDesc paintableDesc)
    {
      PaintableDesc = paintableDesc;
      m_paintable = Instantiate(PaintableDesc.Prefab, transform);
      SaveLoadService.SavePaintable(PaintableDesc);
      
      OnPaintableChanged?.Invoke(m_paintable);
    }
  }
}