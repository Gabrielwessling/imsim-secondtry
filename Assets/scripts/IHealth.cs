using UnityEngine;

public interface IHealth
{
    /// <summary>
    /// Gets the maximum health value
    /// </summary>
    float GetMaxHealth();

    /// <summary>
    /// Gets the current health value
    /// </summary>
    float GetCurrentHealth();

    /// <summary>
    /// Changes the current health value
    /// amount > 0 heals, amount < 0 damages
    /// </summary>
    /// <param name="amount">Amount to change health by</param>
    void ChangeHealth(float amount);
}