using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineCamera myCamera;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CameraBounds"))
            SwitchCameraFocus(collision);
    }

    void SwitchCameraFocus(Collider2D collider)
    {
        if (myCamera != null)
        {
            Debug.Log($"Mudei para área: {collider.gameObject.name}");

            myCamera.transform.position = 
                new Vector3(collider.transform.position.x, 
                collider.transform.position.y,
                myCamera.transform.position.z);

            var orthoSize = Mathf.Max(collider.bounds.size.x, collider.bounds.size.y) / 2f;
            myCamera.Lens.OrthographicSize = orthoSize;
        }
    }
}
