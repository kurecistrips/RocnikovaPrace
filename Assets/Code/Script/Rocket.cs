using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float rocketSpeed = 7.5f;
    [SerializeField] private int rocketDamage = 10;
    [SerializeField] private float rocketRadius = 3f;

    private float timerForDestruction = 4.5f;

    private Transform target;

    public void SetTarget(Transform _target){
        target = _target;
    }

    private void Update(){
        timerForDestruction -= Time.deltaTime;
        if(timerForDestruction <= 0){
            
            Destroy(gameObject);
        }
    }

    private void FixedUpdate(){
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * rocketSpeed;
        
        float bRot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, bRot + 90);
    }
    
    private void OnCollisionEnter2D(Collision2D other){
        if (other.transform == target){
            Explode();
        }
        Destroy(gameObject);
    }

    private void Explode(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, rocketRadius);

        foreach (Collider2D collider in colliders){

            Health health = collider.GetComponent<Health>();
            if (health != null){
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                float damageMultiplier = Mathf.Clamp01(1 - (distance/rocketRadius));
                int damageAmount = Mathf.RoundToInt(rocketDamage * damageMultiplier);

                health.TakeDamage(damageAmount);

            }
        }
    }
}
