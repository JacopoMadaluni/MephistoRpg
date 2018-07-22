using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entity : MonoBehaviour, Damageable {

    [SerializeField] protected float maxHealth = 100f;

    [SerializeField] protected float health;

    public delegate void OnHealthChange(float newHealthAsPercentage);
    public event OnHealthChange onHealthChangeObservers;


    // Use this for initialization
    void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float healthAsPercentage
    {
        get
        {
            return health / maxHealth;
        }
    }


    // For now damage is negative, heals are positive floats
    public void TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0f, maxHealth);
        print(onHealthChangeObservers == null);
        onHealthChangeObservers(health / maxHealth);

        if (this.health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
