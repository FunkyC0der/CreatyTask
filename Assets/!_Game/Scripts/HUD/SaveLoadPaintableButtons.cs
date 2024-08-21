using CreatyTest.Painting.Paintables;
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

    private void Awake()
    {
      SaveButton.onClick.AddListener(() => PaintableService.Save());
      LoadButton.onClick.AddListener(() => PaintableService.Load());
      ClearButton.onClick.AddListener(() => PaintableService.Clear());

      PaintableService.OnPaintableChanged += UpdateView;
      PaintableService.SaveLoadService.OnPaintableTextureSavesChanged += UpdateView;
    }

    private void UpdateView(Paintable paintable) => 
      UpdateView();

    private void UpdateView() => 
      LoadButton.interactable = PaintableService.CanLoad();
  }
}