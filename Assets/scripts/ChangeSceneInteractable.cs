using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneInteractable : MonoBehaviour, IInteractable
{
    public bool IsInteractable => true;
    public string sceneToLoad;

    string IInteractable.InteractionName => "Change Scene";

    string IInteractable.InteractionDescription => "Change to another scene.";

    public void Interact(Character interactor)
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }

    public void OnFocus(Character interactor)
    {
        throw new System.NotImplementedException();
    }

    public void OnLostFocus(Character interactor)
    {
        throw new System.NotImplementedException();
    }
}
