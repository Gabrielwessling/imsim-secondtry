using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private AudioClip LevelMusic;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip LevelAmbient;
    [SerializeField] private AudioSource ambientSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSource.clip = LevelMusic;
        musicSource.loop = true;
        musicSource.Play();

        ambientSource.clip = LevelAmbient;
        musicSource.loop = true;
        ambientSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
