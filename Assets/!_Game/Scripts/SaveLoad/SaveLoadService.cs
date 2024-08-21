using System;
using System.IO;
using CreatyTest.Painting.Paintables;
using CreatyTest.Painting.PaintTools;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CreatyTest.SaveLoad
{
  public class SaveLoadService : MonoBehaviour
  {
    public event Action OnPaintableTextureSavesChanged;
    
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

    public void SavePaintableTexture(PaintableDesc paintableDesc, Texture2D texture)
    {
      byte[] bytes = texture.EncodeToPNG();
      string filePath = PaintableTextureFilePath(paintableDesc);
      File.WriteAllBytes(filePath, bytes);
      
      OnPaintableTextureSavesChanged?.Invoke();
      
      Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + filePath);
    }

    public Texture2D LoadPaintableTexture(PaintableDesc paintableDesc)
    {
      string filePath = PaintableTextureFilePath(paintableDesc);
      if (!File.Exists(filePath))
        return null;

      byte[] bytes = File.ReadAllBytes(filePath);
      
      var texture = new Texture2D(1, 1);
      texture.LoadImage(bytes);
      return texture;
    }

    public void ClearPaintableTexture(PaintableDesc paintableDesc)
    {
      string filePath = PaintableTextureFilePath(paintableDesc);
      File.Delete(filePath);

      OnPaintableTextureSavesChanged?.Invoke();
    }

    public bool HasSavedPaintableTexture(PaintableDesc paintableDesc) => 
      File.Exists(PaintableTextureFilePath(paintableDesc));

    private static string PaintableTextureFilePath(PaintableDesc paintableDesc) => 
      Path.Combine(Application.dataPath, paintableDesc.name);

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