using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    Animator animator;
    NavMeshAgent navMeshAgent;
    public IHealth healthComponent;

    bool isIdle = true;
    bool isWalking = false;
    bool isDead = false;
    bool isAfraid = false;

    public float walkSpeed = 2f;
    public float stopTiming = 5f;
    public float walkRadius = 10f;

    float stopTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthComponent = GetComponent<IHealth>();
    }

    void Update()
    {
        animator.SetBool("idling", isIdle);
        animator.SetBool("walking", isWalking);
        animator.SetBool("dead", isDead);
        animator.SetBool("afraid", isAfraid);

        if (healthComponent.GetCurrentHealth() <= 0f)
        {
            isDead = true;
        }

        if (isDead)
        {
            isWalking = false;
            isIdle = false;
            isAfraid = false;
            isDead = true;
            return;
        }

        if (navMeshAgent.velocity.magnitude > 0.1f)
        {
            isWalking = true;
            isIdle = false;
        }
        else
        {
            isWalking = false;
            isIdle = true;
        }

        PerformWalkCycle();
    }

    void PerformWalkCycle()
    {
        if (isWalking)
        {
            stopTimer = 0f;
            return;
        }

        stopTimer += Time.deltaTime;

        if (isIdle && stopTimer >= stopTiming)
        {
            Vector3 newPos = new Vector3(
                Random.Range(-walkRadius, walkRadius),
                0,
                Random.Range(-walkRadius, walkRadius)
            ) + transform.position;
            navMeshAgent.SetDestination(newPos);
            stopTimer = 0f;
        }
    }
}
