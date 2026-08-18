using UnityEngine;

public class ZooEntranceTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ZooMessageManager.Instance.ShowMessage("Welcome to the Virtual Zoo!");
        }
    }
}
