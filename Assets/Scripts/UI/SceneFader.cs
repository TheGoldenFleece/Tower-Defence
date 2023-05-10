using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    [SerializeField]
    float fadeTime = 1.0f;
    [SerializeField]
    Image fadeBackground;
    [SerializeField]
    AnimationCurve curve;

    public static SceneFader instance;
    public static bool IsLoaded;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        IsLoaded = false;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = fadeTime;

        while (t > 0)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            Color fadeColor = new Color(fadeBackground.color.r, fadeBackground.color.g, fadeBackground.color.b, a);
            fadeBackground.color = fadeColor;

            yield return null;
        }

        IsLoaded = true;
    }

    IEnumerator FadeOut(string sceneName)
    {
        float t = fadeTime;
        float a;
        Color fadeColor;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            a = curve.Evaluate(t);
            fadeColor = new Color(fadeBackground.color.r, fadeBackground.color.g, fadeBackground.color.b, a);
            fadeBackground.color = fadeColor;

            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void FadeTo(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }
}
