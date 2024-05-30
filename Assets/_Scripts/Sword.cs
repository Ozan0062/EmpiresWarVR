using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damage = 1;
    public string[] targetTags;

    void OnTriggerEnter(Collider other)
    {
            Debug.Log("Trigger with: " + other.gameObject.name);

            foreach (string tag in targetTags)
            {
                if (other.CompareTag(tag))
                {
                    EnemyAI target = other.GetComponent<EnemyAI>();
                    if (target != null)
                    {
                        target.TakeDamage(damage);
                        Debug.Log("Dealt " + damage + " damage to " + other.gameObject.name);
                    }
                    break;
                }
            }
    }
}
