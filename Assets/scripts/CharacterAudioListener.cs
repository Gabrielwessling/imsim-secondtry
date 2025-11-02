using UnityEngine;

public class AudioCharacterListener : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] footstepClips;
    [SerializeField] private AudioClip[] jumpClips;
    [SerializeField] private AudioClip[] hurtClips;
    [SerializeField] private AudioClip[] deathClips;

    CharacterEvents eventsRef;

    void Awake()
    {
        eventsRef = GetComponent<CharacterEvents>();
        eventsRef.OnFootstep += HandleFootstep;
        eventsRef.OnJump += HandleJump;
        eventsRef.OnHurt += HandleHurt;
        eventsRef.OnDie += HandleDeath;
    }

    void HandleFootstep(int surface)
    {
        // por agora ignora surface e só randomiza
        if (footstepClips.Length > 0)
            source.PlayOneShot( footstepClips[ Random.Range(0, footstepClips.Length) ] );
    }

    void HandleJump()
    {
        if (jumpClips.Length > 0)
            source.PlayOneShot(jumpClips[Random.Range(0, hurtClips.Length)]);
    }

    void HandleHurt(float dmg)
    {
        if (hurtClips.Length > 0)
            source.PlayOneShot(hurtClips[Random.Range(0, hurtClips.Length)]);
    }

    void HandleDeath()
    {
        if (deathClips.Length > 0)
            source.PlayOneShot(deathClips[Random.Range(0, hurtClips.Length)]);
    }
}