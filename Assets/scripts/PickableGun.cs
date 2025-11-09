using UnityEngine;

public class PickableGun : MonoBehaviour, IInteractable
{
    public bool IsInteractable => true;

    string IInteractable.InteractionName => "Pick up";

    string IInteractable.InteractionDescription => "Pick up the gun and add it to your inventory.";

    [SerializeField] private GameObject gunPrefab;

    public void Interact(Character interactor)
    {
        if (gunPrefab != null && interactor != null && !interactor.guns.Contains(gunPrefab))
        {
            GameObject new_weapon = Instantiate(gunPrefab, interactor.WeaponParent);
            interactor.guns.Add(new_weapon);
            interactor.events.EmitInteract();
            Destroy(gameObject);
        }
    }

    public void OnFocus(Character interactor)
    {
        throw new System.NotImplementedException();
    }

    public void OnLostFocus(Character interactor)
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
