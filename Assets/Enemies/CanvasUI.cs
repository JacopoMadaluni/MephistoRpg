using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour {

    [SerializeField] private Text enemyName;
    [SerializeField] private EnemyHealthBar enemyBar;

	// Use this for initialization
	void Start () {
        enemyName = GetComponentInChildren<Text>();
        enemyBar = GetComponentInChildren<EnemyHealthBar>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
