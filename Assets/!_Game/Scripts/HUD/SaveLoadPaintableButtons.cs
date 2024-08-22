using CreatyTest.Painting.Paintables;
using CreatyTest.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace CreatyTest.HUD
{
  public class SaveLoadPaintableButtons : MonoBehaviour
  {
    public Button SaveButton;
    public Button LoadButton;
    public Button DeleteButton;

    private PaintableService m_paintableService;

    private Paintable Paintable => m_paintableService.Paintable;

    [Inject]
    private void Construct(PaintableService paintableService)
    {
      m_paintableService = paintableService;
      m_paintableService.OnPaintableChanged += UpdateView;

      SaveButton.onClick.AddListener(Save);
      LoadButton.onClick.AddListener(Load);
      DeleteButton.onClick.AddListener(Delete);

      UpdateView();
    }

    private void Save()
    {
      SaveLoadService.SavePaintableTexture(Paintable.Desc, Paintable.GetTexture());
      LoadButton.interactable = true;
    }

    private void Load() =>
      Paintable.SetTexture(SaveLoadService.LoadPaintableTexture(Paintable.Desc));

    private void Delete()
    {
      SaveLoadService.DeletePaintableTexture(Paintable.Desc);
      LoadButton.interactable = false;
    }

    private void UpdateView() => 
      LoadButton.interactable = SaveLoadService.HasSavedPaintableTexture(Paintable.Desc);
  }
}