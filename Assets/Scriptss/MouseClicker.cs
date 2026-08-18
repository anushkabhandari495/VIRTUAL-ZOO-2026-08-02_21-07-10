using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MouseClicker : MonoBehaviour
{
    [SerializeField]
    private Camera m_Camera;

    // Drag the Canvas with the Graphic Raycaster onto this field.
    [SerializeField]
    private GraphicRaycaster m_Raycaster;

    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;

    private void Start()
    {
        // Get the EventSystem in the scene.
        m_EventSystem = EventSystem.current;

        // If no camera was assigned in the Inspector,
        // automatically use the Main Camera.
        if (m_Camera == null)
        {
            m_Camera = Camera.main;
        }
    }

    private void Update()
    {
        Mouse mouse = Mouse.current;

        if (mouse == null)
        {
            return;
        }

        // Left mouse button
        if (mouse.leftButton.wasPressedThisFrame)
        {
            HandleClick(mouse.position.ReadValue());
        }

        // Right mouse button
        if (mouse.rightButton.wasPressedThisFrame)
        {
            HandleClick(mouse.position.ReadValue());
        }
    }

    private void HandleClick(Vector2 mousePosition)
    {
        if (m_Camera == null)
        {
            Debug.LogWarning("MouseClicker: Camera is not assigned.");
            return;
        }

        // --------------------------------------------------
        // 1. Check if we clicked a UI button
        // --------------------------------------------------

        if (m_Raycaster != null && m_EventSystem != null)
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(m_PointerEventData, results);

            foreach (RaycastResult result in results)
            {
                Button button = result.gameObject.GetComponent<Button>();

                if (button != null)
                {
                    Debug.Log("Clicked UI Button: " + result.gameObject.name);

                    button.onClick.Invoke();

                    // We clicked a UI button, so don't also
                    // perform the animal/world raycast.
                    return;
                }
            }
        }

        // --------------------------------------------------
        // 2. Raycast into the 3D world
        // --------------------------------------------------

        Ray ray = m_Camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Clicked on: " + hit.collider.gameObject.name);

            IPointerClickHandler clickHandler =
                hit.collider.gameObject.GetComponent<IPointerClickHandler>();

            if (clickHandler != null)
            {
                PointerEventData pointerEventData =
                    new PointerEventData(m_EventSystem);

                clickHandler.OnPointerClick(pointerEventData);
            }
        }
        else
        {
            Debug.Log("Clicked empty space.");
        }
    }
}