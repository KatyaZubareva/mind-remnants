using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    [SerializeField] private Light targetLight;
    [SerializeField] private float minIntensity = 0.2f;
    [SerializeField] private float maxIntensity = 1.5f;
    [SerializeField] private float speed = 20f;

    private float _noiseOffset;

    private void Awake()
    {
        if (targetLight == null)
            targetLight = GetComponent<Light>();

        _noiseOffset = Random.Range(0f, 1000f);
    }

    private void Update()
    {
        if (targetLight == null) return;

        float n = Mathf.PerlinNoise(Time.time * speed, _noiseOffset);
        targetLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, n);
    }
}