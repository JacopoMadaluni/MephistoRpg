using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (CameraRaycaster))]
public class MyCursor : MonoBehaviour {

    [SerializeField] private Texture2D walkCursor = null;
    [SerializeField] private Texture2D attackCursor = null;
    [SerializeField] private Texture2D errorCursor = null;
    Vector2 hotspot = new Vector2(0, 0);

    private CursorMode cursorMode = CursorMode.Auto;
    
    private Camera cam;
    private CameraRaycaster caster;
    private EnemyHealthBar enemyBar;
    private Text enemyName;
    


    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        caster = GetComponent<CameraRaycaster>();
        caster.onCursorLayerChangeObservers += OnLayerChange;

        enemyBar = FindObjectOfType<EnemyHealthBar>();
        enemyName = FindObjectOfType<Text>();
    }

    private void Update()
    {

    }


    // Update is called once per frame
    void LateUpdate () {
    }

    private void OnLayerChange(int newLayer)
    {
        UpdateCursor(newLayer);
        ProcessEnemyBarChanges(newLayer);
        
    }

    private void ProcessEnemyBarChanges(int newLayer)
    {
        if (newLayer == 9)
        {
            Enemy newEnemy = caster.Hit.collider.gameObject.GetComponent<Enemy>();
            enemyBar.UpdateBar(newEnemy);
            enemyName.text = newEnemy.name;

        }
        else
        {
            //temporaneus
            enemyBar.invisible();
        }
    }


    private void UpdateCursor(int newLayer)
    {
       print(newLayer);
       switch (newLayer)
        {
            case 8:
                Cursor.SetCursor(walkCursor, hotspot, cursorMode);
                break;
            case 9:
                Cursor.SetCursor(attackCursor, hotspot, cursorMode);
                break;
            default:
                Cursor.SetCursor(errorCursor, hotspot, cursorMode);
                return;
        }
        //print("Changed cursor");
    }
}
