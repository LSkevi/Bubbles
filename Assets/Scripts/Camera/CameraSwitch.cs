using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineCamera myCamera;
    public float duration = 0.5f;
    public float magnitude = 0.5f;
    private Vector3 initialPosition;
    private float noiseSeedX;
    private float noiseSeedY;

    void Start()
    {
        if (myCamera != null)
            initialPosition = myCamera.transform.localPosition;

        noiseSeedX = Random.Range(0f, 100f);
        noiseSeedY = Random.Range(0f, 100f);
    }

    public void Shake()
    {
        if (myCamera != null)
            StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Gera deslocamentos baseados no Perlin Noise
            float offsetX = (Mathf.PerlinNoise(noiseSeedX, elapsedTime * 10f) - 0.5f) * 2f * magnitude;
            float offsetY = (Mathf.PerlinNoise(noiseSeedY, elapsedTime * 10f) - 0.5f) * 2f * magnitude;

            // Aplica os deslocamentos à posição da câmera
            myCamera.transform.localPosition = new Vector3(
                initialPosition.x + offsetX,
                initialPosition.y + offsetY,
                initialPosition.z
            );

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        myCamera.transform.localPosition = initialPosition;
    }
}



//void OnTriggerEnter2D(Collider2D collision)
//{
//    if (collision.CompareTag("CameraBounds"))
//        SwitchCameraFocus(collision);
//}

//void SwitchCameraFocus(Collider2D collider)
//{
//    if (myCamera != null)
//    {
//        Debug.Log($"Mudei para área: {collider.gameObject.name}");

//        myCamera.transform.position = 
//            new Vector3(collider.transform.position.x, 
//            collider.transform.position.y,
//            myCamera.transform.position.z);

//        var orthoSize = Mathf.Max(collider.bounds.size.x, collider.bounds.size.y) / 2f;
//        myCamera.Lens.OrthographicSize = orthoSize;
//    }
//}