using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFader : MonoBehaviour
{
    public CanvasGroup uiElement;

    void Update()
    {
        StartCoroutine(FadeCG(uiElement, uiElement.alpha, 1));
        StartCoroutine(FadeCG(uiElement, uiElement.alpha, 0));
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCG(uiElement, uiElement.alpha, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCG(uiElement, uiElement.alpha, 0));
    }

    public IEnumerator FadeCG(CanvasGroup cg, float start, float end, float lerp_time = 0.5f)
    {
        float time_started_lerping = Time.time;
        float time_since_started = Time.time - time_started_lerping;
        float percentage_complete = time_since_started / lerp_time;

        while(true)
        {
            time_since_started = Time.time - time_started_lerping;
            percentage_complete = time_since_started / lerp_time;

            float current_value = Mathf.Lerp(start, end, percentage_complete);

            cg.alpha = current_value;

            if (percentage_complete >= 1)
                break;

            yield return new WaitForEndOfFrame();
        }

        print("done");
    }
}
