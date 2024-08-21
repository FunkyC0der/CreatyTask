using UnityEngine;

namespace CreatyTest.Painting
{
  [CreateAssetMenu(fileName = "_Paintable", menuName = "Game/Paintable")]
  public class PaintableDesc : ScriptableObject
  {
    public Paintable Prefab;
  }
}