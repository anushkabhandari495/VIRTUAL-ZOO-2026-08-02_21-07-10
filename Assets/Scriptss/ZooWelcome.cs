using UnityEngine;
using TMPro;
using System.Collections;

public class ZooWelcome : MonoBehaviour
{
    public TextMeshProUGUI welcomeText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ShowWelcome());
        }
    }

    IEnumerator ShowWelcome()
    {
        welcomeText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        welcomeText.gameObject.SetActive(false);
    }
}
