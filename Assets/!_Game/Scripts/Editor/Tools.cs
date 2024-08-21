using UnityEditor;
using UnityEngine;

namespace CreatyTest.__Game.Scripts.Editor
{
  public static class Tools
  {
    [MenuItem(".Tools/Clear Saves")]
    public static void ClearSaves()
    {
      PlayerPrefs.DeleteAll();
      PlayerPrefs.Save();
    }
  }
}