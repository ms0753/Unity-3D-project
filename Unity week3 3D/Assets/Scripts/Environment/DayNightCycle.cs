using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)] // 0 ���� 1 ������ ���� ��ũ�ѷ� ������ ��
    public float time;
    public float fullDayLength;
    public float startTime = 0.4f;
    private float timeRate;
    public Vector3 noon;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;

    private void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
    }

    private void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f;
        // ���� �ٲ�� ȯ���� �ٲ�� ���� �ƴ�
        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);
        //ȯ�汤�� �ݻ籤�� Ȱ��
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
    }



    void UpdateLighting(Light lightSource, Gradient colorGradiant, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);

        // noon �� �����̰� �޺��� 90���� �شµ�, 90���� 4�� ���� ������ 4.0f �� ���ϰ�, 360���� 1�� �����Ͽ�
        // 4����� ���� ��� �޺��� ���ִ� ��õ�� 0.25f �̰� ���̶߰� �ذ� ���� ��ġ�� 0.75f �� �����Ѵ�.
        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4.0f;
        lightSource.color = colorGradiant.Evaluate(time);
        lightSource.intensity = intensity;

        GameObject go = lightSource.gameObject;
        if(lightSource.intensity == 0&&go.activeInHierarchy)
            go.SetActive(false);
        else if(lightSource.intensity > 0 && !go.activeInHierarchy)
            go.SetActive(true);
    }
}
