using UnityEngine;

namespace CreatyTest
{
  public class Pen : MonoBehaviour
  {
    public Camera Camera;
    
    private void Update()
    {
      if (!Input.GetMouseButton(0))
        return;

      if (!Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        return;

      var painter = hit.transform.GetComponent<Painter>();
      if(painter)
        painter.Paint(hit.textureCoord);
    }
  }
}