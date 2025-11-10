using UnityEngine;

public class ComputerInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject computerScreenUI;

    public string InteractionName => throw new System.NotImplementedException();

    public string InteractionDescription => throw new System.NotImplementedException();

    public bool IsInteractable => true;

    public void Interact(Character interactor)
    {
        computerScreenUI.SetActive(true);
        interactor.SetCanMove(false);
    }

    public void OnFocus(Character interactor)
    {
        throw new System.NotImplementedException();
    }

    public void OnLostFocus(Character interactor)
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
