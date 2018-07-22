using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    RawImage healthBarRawImage = null;
    Enemy enemy = null;

    private EnemyName text;

    private float healthAsPercentage;

    // Use this for initialization

    void Start()
    {
        healthBarRawImage = GetComponent<RawImage>();
    }


    public void invisible()
    {
        transform.parent.gameObject.SetActive(false);
    }



    public void UpdateBar(Enemy newEnemy)
    {
        if (enemy != null) {
            enemy.onHealthChangeObservers -= OnHealthChange; // clear old enemy observer
        }       
        enemy = newEnemy;
        enemy.onHealthChangeObservers += OnHealthChange;

        OnHealthChange(enemy.healthAsPercentage);

        transform.parent.gameObject.SetActive(true);
    }

    void OnHealthChange(float newHealth)
    {
        float xValue = -(newHealth / 2f) - 0.5f;
        healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);

    }
}
