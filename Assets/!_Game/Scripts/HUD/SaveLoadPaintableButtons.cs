using CreatyTest.Painting.Paintables;
using CreatyTest.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace CreatyTest.HUD
{
  public class SaveLoadPaintableButtons : MonoBehaviour
  {
    public Button SaveButton;
    public Button LoadButton;
    public Button ClearButton;

    public PaintableService PaintableService;

    private Paintable Paintable => PaintableService.Paintable;

    private void Awake()
    {
      SaveButton.onClick.AddListener(Save);
      LoadButton.onClick.AddListener(Load);
      ClearButton.onClick.AddListener(Clear);

      PaintableService.OnPaintableChanged += _ => UpdateView();
    }

    private void Save()
    {
      SaveLoadService.SavePaintableTexture(Paintable.Desc, Paintable.GetTexture());
      LoadButton.interactable = true;
    }

    private void Load() =>
      Paintable.SetTexture(SaveLoadService.LoadPaintableTexture(Paintable.Desc));

    private void Clear()
    {
      Paintable.SetTexture(Paintable.OriginalTexture);
      SaveLoadService.ClearPaintableTexture(Paintable.Desc);
      LoadButton.interactable = false;
    }

    private void UpdateView() => 
      LoadButton.interactable = SaveLoadService.HasSavedPaintableTexture(Paintable.Desc);
  }
}