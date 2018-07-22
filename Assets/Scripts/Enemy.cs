using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : Entity {


    [SerializeField] float attackRadius = 2f;
    [SerializeField] float chaseRadius = 10f;
    [SerializeField] float attackDamage;
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] GameObject projectile = null;
    [SerializeField] GameObject projectileSocket;
    [SerializeField] Vector3 aimOffset = new Vector3(0, 1f, 0);

    EnemyHealthBar healthBar;

    AICharacterControl enemyAI;
    GameObject target = null;
    bool isAttacking = false;
    private State currentState;

    private IEnumerator coroutine;

    // Use this for initialization
    void Start () {
        currentState = State.still;
        target = GameObject.FindGameObjectWithTag("Player");
        enemyAI = GetComponent<AICharacterControl>();

        //coroutine = SpawnProjectile(attackCooldown);
        //StartCoroutine(coroutine);
    }
	
	// Update is called once per frame
	void Update () {
        switch (currentState) {
            case State.still:
                LookForPlayer();
                break;
            case State.chasing:
                Chase();
                break;
            case State.attacking:
                Attack();
                break;
            default:
                Debug.LogWarning("Default state in enemy Update().. fix it idiot.");
                break;
        }
	}

    private void LookForPlayer()
    {
        float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        if (distanceToTarget < chaseRadius)
        {
            //enemyAI.target = target.transform;
            currentState = State.chasing;
        }
        else
        {
            enemyAI.SetTarget(transform);
        }

    }

    private void Chase()
    {
        enemyAI.target = target.transform;
        float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        if (distanceToTarget < attackRadius)
        {
            currentState = State.attacking;
        }
    }

    private void Attack()
    {
        enemyAI.SetTarget(transform);

        if (!isAttacking)
        {
            // InvokeRepeating("SpawnProjectile", 0f, attackCooldown); // TODO change this shit
            InvokeRepeating("SpawnProjectile", 0f, attackCooldown);
            isAttacking = true;
        }
        

        float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        if (distanceToTarget > attackRadius)
        {
            isAttacking = false;
            CancelInvoke();        
            currentState = State.chasing;
        }
    }

    private void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(projectile, projectileSocket.transform.position, Quaternion.identity);
        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        projectileComponent.SetDamage(attackDamage);
        Vector3 unitVectorToTarget = (target.transform.position + aimOffset - projectileSocket.transform.position).normalized;
        newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToTarget * projectileComponent.GetSpeed();
   
        
    }

    // Not in use atm, less efficient but more readable. Your choice.
    private void UpdateState()
    {
        float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        if (distanceToTarget < attackRadius)
        {
            currentState = State.attacking;
        }else
        {
            currentState = State.chasing;
        }
    }

    private enum State
    {
        chasing,
        attacking,
        still
    }

}
