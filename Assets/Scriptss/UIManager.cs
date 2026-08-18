using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text InformationText;

    [SerializeField]
    private Canvas UICanvas;

    private void Start()
    {
        // Hide the Bear UI when the game starts.
        if (UICanvas != null)
        {
            UICanvas.gameObject.SetActive(false);
        }
    }

    public void ToggleText()
    {
        if (InformationText != null)
        {
            InformationText.gameObject.SetActive(
                !InformationText.gameObject.activeSelf
            );
        }
    }

    public void ShowUI()
    {
        if (UICanvas != null)
        {
            UICanvas.gameObject.SetActive(true);
        }
    }

    public void HideUI()
    {
        if (UICanvas != null)
        {
            UICanvas.gameObject.SetActive(false);
        }
    }

    // Kept because AnimalLookInteract.cs currently uses ToggleUI().
    public void ToggleUI()
    {
        if (UICanvas != null)
        {
            UICanvas.gameObject.SetActive(
                !UICanvas.gameObject.activeSelf
            );
        }
    }
}