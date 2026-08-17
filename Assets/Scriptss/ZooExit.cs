using UnityEngine;
using TMPro;
using System.Collections;

public class ZooExit : MonoBehaviour
{
    public TextMeshProUGUI thankYouText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ShowThankYou());
        }
    }

    IEnumerator ShowThankYou()
    {
        thankYouText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        thankYouText.gameObject.SetActive(false);
    }
}