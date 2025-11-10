using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneInteraction : MonoBehaviour, IInteractable
{
    public GameObject playerRoot;
    public Vector3 spawnPosition;
    public string sceneName;

    public string InteractionName => throw new System.NotImplementedException();

    public string InteractionDescription => throw new System.NotImplementedException();

    public bool IsInteractable => throw new System.NotImplementedException();

    public void Interact(Character interactor)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.Log("No Job Chosen.");
            return;
        }
        DontDestroyOnLoad(playerRoot);
        DontDestroyOnLoad(this.gameObject);
        
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = spawnPosition;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        this.enabled = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = GameObject.FindWithTag("Respawn").transform.position;
        }
        // Move the object to the new scene
        SceneManager.MoveGameObjectToScene(this.gameObject, scene);
        
        // Unsubscribe to prevent multiple calls
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
