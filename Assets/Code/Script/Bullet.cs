using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    private float timerForDestruction = 4f;  
    private Transform target;

    

    public void SetTarget(Transform _target){
        target = _target;
    }

    private void Update(){
        timerForDestruction -= Time.deltaTime;
        if (timerForDestruction <= 0){
            Destroy(gameObject);
        }
    }

    private void FixedUpdate(){
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;

        float bRot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, bRot + 90);
    }
    
    private void OnCollisionEnter2D(Collision2D other){
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject);
    }



}
