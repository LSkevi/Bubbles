using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineCamera myCamera;
    public CinemachineBasicMultiChannelPerlin cameraNoise;
    public float amplitude;
    public float frequency;
    public float duration = 0.5f;

    private void Start()
    {
        cameraNoise.AmplitudeGain = 0f;
        cameraNoise.FrequencyGain = frequency;
    }

    public void Shake()
    {
        if (myCamera != null && cameraNoise != null)
            StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;

        cameraNoise.AmplitudeGain = amplitude;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraNoise.AmplitudeGain = 0f;
    }
}