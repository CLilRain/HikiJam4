using System.Collections;
using UnityEditor;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField]
    private Agent owner;

    [SerializeField]
    private Collider interactionDetectorCollider;

    public Agent Owner => owner;
    public bool CanReceiveDamage { get; private set; } = true;

    private void Awake()
    {
        if (interactionDetectorCollider == null)
        {
            interactionDetectorCollider = GetComponent<Collider>();
        }
    }

    public virtual void Disable()
    {
        CanReceiveDamage = false;
        interactionDetectorCollider.enabled = false;
    }

    public void ReceiveDamage(Agent attacker, int damage)
    {
        owner.ReceiveDamage(attacker, damage);
    }
}