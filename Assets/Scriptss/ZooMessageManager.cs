using UnityEngine;
using TMPro;
using System.Collections;

public class ZooMessageManager : MonoBehaviour
{
    public static ZooMessageManager Instance;

    [Header("UI Reference")]
    public TextMeshProUGUI messageText;

    [Header("Animation Settings")]
    public float fadeDuration = 0.5f;
    public float displayDuration = 2.5f;

    private CanvasGroup canvasGroup;
    private Coroutine currentRoutine;

    void Awake()
    {
        Instance = this;

        // Ensure CanvasGroup exists for fading
        canvasGroup = messageText.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = messageText.gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        messageText.gameObject.SetActive(false);
    }

    public void ShowMessage(string message)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowMessageRoutine(message));
    }

    private IEnumerator ShowMessageRoutine(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        // Fade in
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

        // Hold
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));

        messageText.gameObject.SetActive(false);
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = to;
    }
}