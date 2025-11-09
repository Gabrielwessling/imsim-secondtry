using UnityEngine;
using System;

public class CharacterEvents : MonoBehaviour
{
    public Action<int> OnFootstep; // surface id
    public Action OnJump;
    public Action<float> OnHurt; // damage amount
    public Action OnDie;
    public Action OnInteract;


    // chamam isso de outros componentes
    public void EmitFootstep(int surface) => OnFootstep?.Invoke(surface);
    public void EmitJump() => OnJump?.Invoke();
    public void EmitHurt(float dmg) => OnHurt?.Invoke(dmg);
    public void EmitDie() => OnDie?.Invoke();
    public void EmitInteract() => OnInteract?.Invoke();
}