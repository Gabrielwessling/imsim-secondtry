using UnityEngine;

public interface IInteractable
{
    // Short label shown to the player (e.g. "Open", "Pick up")
    string InteractionName { get; }

    // Optional longer description or prompt (e.g. "Press E to open the chest")
    string InteractionDescription { get; }

    // Whether the object can currently be interacted with
    bool IsInteractable { get; }

    // Called when an interactor (player, NPC, etc.) performs the interaction
    void Interact(Character interactor);

    // Called when the interactor begins focusing/looking at this object (optional visual feedback)
    void OnFocus(Character interactor);

    // Called when the interactor stops focusing this object
    void OnLostFocus(Character interactor);
}