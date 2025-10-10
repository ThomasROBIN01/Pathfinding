using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private InputAction interactAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        if (interactAction.WasPressedThisFrame())
        {
            InteractWithWorldObject();
        }
    }

    private void InteractWithWorldObject()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 0.5f))
        {
            IInteract interactableObject;

            if (hit.transform.gameObject.TryGetComponent<IInteract>(out interactableObject)) 
            {
                interactableObject.Activate();
            }
        }
    }
}
