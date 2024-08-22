using System;
using CreatyTest.SaveLoad;
using UnityEngine;
using VContainer;

namespace CreatyTest.Painting.Paintables
{
  public class PaintableService : MonoBehaviour
  {
    public event Action OnPaintableChanged;
    
    public PaintableDesc PaintableDesc;

    public Paintable Paintable { get; private set; }

    [Inject]
    private void Construct()
    {
      PaintableDesc paintableDesc = SaveLoadService.LoadPaintable() ?? PaintableDesc;
      CreatePaintable(paintableDesc);
    }

    public void ChangePaintable(PaintableDesc paintableDesc)
    {
      if (PaintableDesc == paintableDesc)
        return;
      
      if(Paintable)
        Destroy(Paintable.gameObject);

      CreatePaintable(paintableDesc);
    }

    private void CreatePaintable(PaintableDesc paintableDesc)
    {
      PaintableDesc = paintableDesc;
      
      Paintable = Instantiate(PaintableDesc.Prefab, transform);
      Paintable.Desc = PaintableDesc;

      SaveLoadService.SavePaintable(PaintableDesc);
      
      OnPaintableChanged?.Invoke();
    }
  }
}