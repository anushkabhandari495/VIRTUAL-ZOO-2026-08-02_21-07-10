using UnityEngine;

public class ZooExitTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ZooMessageManager.Instance.ShowMessage("Thank you for coming!");
        }
    }
}
