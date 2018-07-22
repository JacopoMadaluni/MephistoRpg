using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class PlayerHealthBar : MonoBehaviour
{

    RawImage healthBarRawImage;
    Player player;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>();
        healthBarRawImage = GetComponent<RawImage>();

        player.onHealthChangeObservers += OnHealthChange;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnHealthChange(float newHealth)
    {
        float xValue = -(newHealth / 2f) - 0.5f;
        healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }
}
