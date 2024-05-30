using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    bool canDealDamage;
    bool hasDealtDamage;
    public float damage = 1;
    public string[] targetTags; 

    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (canDealDamage && !hasDealtDamage)
        {
            Debug.Log("Trigger with: " + other.gameObject.name);

            foreach (string tag in targetTags)
            {
                if (other.CompareTag(tag))
                {
                    FriendAI target = other.GetComponent<FriendAI>();
                    if (target != null)
                    {
                        target.TakeDamage(damage);
                        hasDealtDamage = true;
                        Debug.Log("Dealt " + damage + " damage to " + other.gameObject.name);
                    }
                    break;
                }
            }
        }
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage = false;
    }

    public void EndDealDamage()
    {
        canDealDamage = false;
    }
}

