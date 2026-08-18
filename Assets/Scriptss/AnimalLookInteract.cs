using UnityEngine;
using UnityEngine.InputSystem;

// Attach this script to your MainCamera GameObject.
public class AnimalLookInteract : MonoBehaviour
{
    [SerializeField]
    private Camera m_Camera;

    [SerializeField]
    private float interactDistance = 10f;

    // The UIManager belonging to whatever animal we are currently looking at.
    private UIManager currentAnimalUI;

    void Update()
    {
        // Cast a ray from the centre of the screen (where the crosshair is)
        // every frame, so we always know what we're looking at.
        Ray ray = m_Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            // GetComponentInParent lets this work even if the collider is on
            // a child mesh and the UIManager is on the parent (e.g. the Canvas).
            currentAnimalUI = hit.collider.GetComponentInParent<UIManager>();
        }
        else
        {
            currentAnimalUI = null;
        }

        // Only open/close the panel if:
        // 1) we are currently looking at an animal that has a UIManager, AND
        // 2) the player pressed Left or Right arrow this frame.
        if (currentAnimalUI != null)
        {
            Keyboard keyboard = Keyboard.current;

            if (keyboard.rightArrowKey.wasPressedThisFrame ||
                keyboard.leftArrowKey.wasPressedThisFrame)
            {
                currentAnimalUI.ToggleUI();
            }
        }
    }
}