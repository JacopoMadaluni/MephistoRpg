using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {


    [SerializeField] float attackDamage = 10f;
    [SerializeField] float attackCooldown = .5f;
    [SerializeField] float attackRange = 2f;

    CameraRaycaster caster;
    float lastHitTime = 0;


    // Getters
    public float GetAttackRange()
    {
        return attackRange;
    }





	// Use this for initialization
	void Start () {
        caster = Camera.main.GetComponent<CameraRaycaster>();
        caster.onMouseClickObservers += ProcessAttackEvent;
	}
	
	// Update is called once per frame
	void Update () {

    }

    // TODO change the numbers
    void ProcessAttackEvent(RaycastHit hit, int layer)
    {
        if (layer == 9 & Time.time - lastHitTime > attackCooldown)
        {
            Enemy currentTarget = hit.collider.gameObject.GetComponent<Enemy>();
            float distanceToEnemy = Vector3.Distance(currentTarget.transform.position, transform.position);
            if (distanceToEnemy <= attackRange)
            {
                AttackEnemy(currentTarget);
            }
            
        }
    }

    void AttackEnemy(Enemy enemy)
    {
        print("Attack");
        enemy.TakeDamage(attackDamage);
        lastHitTime = Time.time;
    }



}
