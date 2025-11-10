using UnityEngine;
using System;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    public int id;
    public CharacterEvents events;

    public List<GameObject> guns = new List<GameObject>();
    private WeaponSystem weaponSystem;
    public Transform WeaponParent;

    [Header("Interaction")]
    public Camera playerCamera;               // optional, falls back to Camera.main
    public float interactRange = 3f;
    public LayerMask interactMask = ~0;       // which layers to raycast against
    private IInteractable focusedInteractable;
    public GameObject interactionSpriteUI;
    [SerializeField] private Q3Movement.Q3PlayerController playerController;

    private bool canMove = true;

    void Awake()
    {
        weaponSystem = GetComponentInChildren<WeaponSystem>();
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
        // Also disable mouse look if applicable
        playerController.canMove = value;
    }

    void Update()
    {
        weaponSystem.weapons = guns.ToArray();
        HandleInteractionRaycast();
    }

    void HandleInteractionRaycast()
    {
        Camera cam = playerCamera != null ? playerCamera : Camera.main;
        if (cam == null)
            return;

        // Ray from screen center
        Ray ray = cam.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactRange, interactMask))
        {
            // Try to get an IInteractable on the hit object or its parents
            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                // Optionally handle focus enter/exit here (not implemented)
                focusedInteractable = interactable;

                // Show interaction UI
                if (interactionSpriteUI != null)
                    interactionSpriteUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    try
                    {
                        interactable.Interact(this);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Interaction failed: {ex}");
                    }
                }

                // debug draw
                Debug.DrawLine(ray.origin, hit.point, Color.green);
                return;
            }
        }

        // nothing hit or not interactable
        focusedInteractable = null;
        if (interactionSpriteUI != null)
            interactionSpriteUI.SetActive(false);
        Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.red);
    }

    // Optional: visualize the interaction ray in the editor
    void OnDrawGizmosSelected()
    {
        Camera cam = playerCamera != null ? playerCamera : Camera.main;
        if (cam == null) return;
        Gizmos.color = Color.yellow;
        Ray ray = cam.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * interactRange);
    }
}