using UnityEngine;
using UnityEngine.Serialization;

namespace CreatyTest
{
  public class CameraMove : MonoBehaviour
  {
    [Min(0)]
    public float MoveSpeed = 3;

    [Min(0)]
    public float ChangeMoveSpeedSpeed = 100;

    [Min(0)]
    public float RotateSpeed = 5;

    void Update()
    {
      UpdateMovement();
      UpdateRotation();
    }

    private void UpdateMovement()
    {
      MoveSpeed += Input.GetAxis("Mouse ScrollWheel") * ChangeMoveSpeedSpeed * Time.deltaTime;
      MoveSpeed = Mathf.Max(0, MoveSpeed);

      Vector3 move = Vector3.zero;
      if (Input.GetKey(KeyCode.W))
        move += Vector3.forward * MoveSpeed;
      if (Input.GetKey(KeyCode.S))
        move -= Vector3.forward * MoveSpeed;
      if (Input.GetKey(KeyCode.D))
        move += Vector3.right * MoveSpeed;
      if (Input.GetKey(KeyCode.A))
        move -= Vector3.right * MoveSpeed;
      if (Input.GetKey(KeyCode.E))
        move += Vector3.up * MoveSpeed;
      if (Input.GetKey(KeyCode.Q))
        move -= Vector3.up * MoveSpeed;

      transform.Translate(move * Time.deltaTime);
    }

    private void UpdateRotation()
    {
      if (!Input.GetMouseButton(1))
        return;

      float rotationY = Input.GetAxis("Mouse Y") * RotateSpeed;
      float rotationX = Input.GetAxis("Mouse X") * RotateSpeed;

      transform.Rotate(0, rotationX, 0, Space.World);
      transform.Rotate(-rotationY, 0, 0);
    }
  }
}