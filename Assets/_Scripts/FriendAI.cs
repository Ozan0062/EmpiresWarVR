using UnityEngine;
using UnityEngine.AI;

public class FriendAI : MonoBehaviour
{
    [SerializeField] float health = 1;

    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 20f;
    [SerializeField] float aggroRange = 100f;
    public float walkSpeed;

    [Header("Targeting")]
    [SerializeField] string[] targetTags;  // Array of tags to check for targets in order

    GameObject currentTarget;
    NavMeshAgent agent;
    Animator animator;
    float timePassed;
    float newDestinationCD = 0.5f;
    public bool isDead;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentTarget = FindNextTarget();
        isDead = false;
    }

    void Update()
    {
        if (currentTarget == null)
        {
            currentTarget = FindNextTarget();
            if (currentTarget == null)
                return;  // No targets available
        }

        EnemyAI enemyAI = currentTarget.GetComponent<EnemyAI>();
        float distance = Vector3.Distance(currentTarget.transform.position, transform.position);

        if (distance > aggroRange & !enemyAI.isDead)
        {
            animator.SetFloat("Speed", walkSpeed);
        }
        if (distance < aggroRange & !enemyAI.isDead)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        if (timePassed >= attackCD)
        {
            if (distance < attackRange & !enemyAI.isDead)
            {
                animator.SetTrigger("Attack");
                timePassed = 0;
            }
        }
        timePassed += Time.deltaTime;

        if (newDestinationCD <= 0 && distance <= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(currentTarget.transform.position);
        }
        newDestinationCD -= Time.deltaTime;
        transform.LookAt(currentTarget.transform);

        if (enemyAI.isDead)
        {
            currentTarget = FindNextTarget();
        }
    }

    GameObject FindNextTarget()
    {
        foreach (string tag in targetTags)
        {
            GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject target in potentialTargets)
            {
                EnemyAI enemyAI = target.GetComponent<EnemyAI>();
                if (enemyAI != null && !enemyAI.isDead)
                {
                    return target;
                }
            }
        }
        return null;  // No valid targets found
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            animator.SetTrigger("Die");
            GetComponent<Collider>().enabled = false;
            isDead = true;
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }

    public void StartDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
