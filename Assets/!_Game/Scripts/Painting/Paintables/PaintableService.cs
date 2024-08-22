using System;
using CreatyTest.SaveLoad;
using UnityEngine;

namespace CreatyTest.Painting.Paintables
{
  public class PaintableService : MonoBehaviour
  {
    public event Action<Paintable> OnPaintableChanged;
    
    public PaintableDesc PaintableDesc;

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
        Destroy(m_paintable.gameObject);

      CreatePaintable(paintableDesc);
    }

    private void CreatePaintable(PaintableDesc paintableDesc)
    {
      PaintableDesc = paintableDesc;
      
      m_paintable = Instantiate(PaintableDesc.Prefab, transform);
      m_paintable.Desc = PaintableDesc;

      SaveLoadService.SavePaintable(PaintableDesc);
      
      OnPaintableChanged?.Invoke(m_paintable);
    }
  }
}