using CreatyTest.Painting.Paintables;
using CreatyTest.Painting.PaintTools;
using UnityEditor;
using UnityEngine;

namespace CreatyTest.SaveLoad
{
  public class SaveLoadService : MonoBehaviour
  {
    private const string m_kPaintToolIdKey = "PaintToolId";
    private const string m_kPaintableIdKey = "PaintableId";

    public void SavePaintTool(PaintToolDesc paintToolDesc) => 
      SaveAssetPath(m_kPaintToolIdKey, paintToolDesc);

    public PaintToolDesc LoadPaintTool() => 
      LoadAssetByPath<PaintToolDesc>(m_kPaintToolIdKey);

    public void SavePaintable(PaintableDesc paintableDesc) =>
      SaveAssetPath(m_kPaintableIdKey, paintableDesc);

    public PaintableDesc LoadPaintable() =>
      LoadAssetByPath<PaintableDesc>(m_kPaintableIdKey);

    private void SaveAssetPath(string key, Object asset)
    {
      PlayerPrefs.SetString(key, AssetDatabase.GetAssetPath(asset));
      PlayerPrefs.Save();
    }

    private T LoadAssetByPath<T>(string key) where T : Object
    {
      if (!PlayerPrefs.HasKey(key))
        return null;
      
      string path = PlayerPrefs.GetString(key);
      return AssetDatabase.LoadAssetAtPath<T>(path);
    }
  }
}