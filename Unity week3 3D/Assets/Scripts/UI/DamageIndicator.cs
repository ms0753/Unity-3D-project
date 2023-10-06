using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;

    private Coroutine coroutine;

    public void Flash() // 플래시를 터트리고
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        image.enabled = true;
        image.color = Color.red;
        coroutine = StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()  // 일정시간동안 감소를 하고 사라진다
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while(a > 0.0f)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(1.0f, 0.0f, 0.0f, a);
            yield return null;
        }

        image.enabled = false;
    }
}
