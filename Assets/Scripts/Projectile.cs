using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] float speed;
    float damageCausing;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float GetSpeed()
    {
        return speed;
    }

    public void SetDamage(float damage)
    {
        this.damageCausing = damage;
    }


    private void OnTriggerEnter(Collider other)
    {
        Player target = other.gameObject.GetComponent<Player>();
        if (target != null)
        {
            target.TakeDamage(damageCausing);
        }
    }

}
