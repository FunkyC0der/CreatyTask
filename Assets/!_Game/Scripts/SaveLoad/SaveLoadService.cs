using System;
using System.IO;
using CreatyTest.Painting.Paintables;
using CreatyTest.Painting.PaintTools;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CreatyTest.SaveLoad
{
  public static class SaveLoadService
  {
    private const string m_kPaintToolIdKey = "PaintToolId";
    private const string m_kPaintableIdKey = "PaintableId";
    
    public static void SavePaintTool(PaintToolDesc paintToolDesc) => 
      SaveAssetPath(m_kPaintToolIdKey, paintToolDesc);

    public static PaintToolDesc LoadPaintTool() => 
      LoadAssetByPath<PaintToolDesc>(m_kPaintToolIdKey);

    public static void SavePaintable(PaintableDesc paintableDesc) =>
      SaveAssetPath(m_kPaintableIdKey, paintableDesc);

    public static PaintableDesc LoadPaintable() =>
      LoadAssetByPath<PaintableDesc>(m_kPaintableIdKey);

    public static void SavePaintableTexture(PaintableDesc paintableDesc, Texture2D texture)
    {
      byte[] bytes = texture.EncodeToPNG();
      string filePath = PaintableTextureFilePath(paintableDesc);
      File.WriteAllBytes(filePath, bytes);
    }

    public static Texture2D LoadPaintableTexture(PaintableDesc paintableDesc)
    {
      string filePath = PaintableTextureFilePath(paintableDesc);
      if (!File.Exists(filePath))
        return null;

      byte[] bytes = File.ReadAllBytes(filePath);
      
      var texture = new Texture2D(1, 1);
      texture.LoadImage(bytes);
      return texture;
    }

    public static void ClearPaintableTexture(PaintableDesc paintableDesc)
    {
      string filePath = PaintableTextureFilePath(paintableDesc);
      File.Delete(filePath);
    }

    public static bool HasSavedPaintableTexture(PaintableDesc paintableDesc) => 
      File.Exists(PaintableTextureFilePath(paintableDesc));

    private static string PaintableTextureFilePath(PaintableDesc paintableDesc) => 
      Path.Combine(Application.dataPath, paintableDesc.name);

    private static void SaveAssetPath(string key, Object asset)
    {
      PlayerPrefs.SetString(key, AssetDatabase.GetAssetPath(asset));
      PlayerPrefs.Save();
    }

    private static T LoadAssetByPath<T>(string key) where T : Object
    {
      if (!PlayerPrefs.HasKey(key))
        return null;
      
      string path = PlayerPrefs.GetString(key);
      return AssetDatabase.LoadAssetAtPath<T>(path);
    }
  }
}