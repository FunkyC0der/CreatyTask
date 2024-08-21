using UnityEditor;
using UnityEngine;

namespace CreatyTest.SaveLoad
{
  public class SaveLoadService : MonoBehaviour
  {
    private const string m_kPaintToolIdKey = "PaintToolId";

    public void SaveCurrentPaintTool(PaintToolDesc paintToolDesc)
    {
      PlayerPrefs.SetString(m_kPaintToolIdKey, AssetDatabase.GetAssetPath(paintToolDesc));
      PlayerPrefs.Save();
    }

    public PaintToolDesc LoadCurrentPaintTool()
    {
      if (!PlayerPrefs.HasKey(m_kPaintToolIdKey))
        return null;

      string path = PlayerPrefs.GetString(m_kPaintToolIdKey);
      return AssetDatabase.LoadAssetAtPath<PaintToolDesc>(path);
    }
  }
}