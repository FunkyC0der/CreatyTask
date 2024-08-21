using UnityEngine;

namespace CreatyTest
{
  [CreateAssetMenu(fileName = "_PaintTool", menuName = "Game/PaintTool")]
  public class PaintToolDesc : ScriptableObject
  {
    public PaintTool Prefab;
  }
}