using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter mainCharacter = null;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster caster = null;
    AICharacterControl characterControl = null;
    NavMeshAgent navMeshAgent = null;
    GameObject walkTarget = null;
    Vector3 currentDestination;
    Vector3 clickPoint;
    Player player;

    //EnemyHealthBar enemyBar;

    [SerializeField] private float moveStopRadius = 0.5f;
    [SerializeField] private float attackStopRadius = 5f;

    private bool isMoving = false;
    private const float destinationStopRadius = 0.1f;
    private float stopDistanceFromGround;
    
    

    private void Start()
    {
        player = GetComponent<Player>();
        //cameraRaycaster = GameObject.FindObjectOfType<CameraRaycaster>();
        caster = Camera.main.GetComponent<CameraRaycaster>();
        mainCharacter = GetComponent<ThirdPersonCharacter>();
        characterControl = GetComponent<AICharacterControl>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        stopDistanceFromGround = navMeshAgent.stoppingDistance;

        currentDestination = transform.position;
        walkTarget = new GameObject("WalkTarget");

        caster.onMouseClickObservers += ProcessMouseClick;

        //enemyBar = FindObjectOfType<EnemyHealthBar>();
    }

    void ProcessMouseClick(RaycastHit hit, int layer)
    {
        //TODO togli i numeri del cazzo
        switch (layer)
        {
            case 9:
                ChangeStoppingDistance(player.GetAttackRange());
                GameObject enemy = hit.collider.gameObject;
                characterControl.SetTarget(enemy.transform);
                break;
            case 8:
                ChangeStoppingDistance(stopDistanceFromGround);
                walkTarget.transform.position = hit.point;
                characterControl.SetTarget(walkTarget.transform);
                break;
            default:
                Debug.LogWarning("Idiot, PlayerMovement Default switch state.. fix.");
                break;
        }
    }

    private void ChangeStoppingDistance(float newDistance)
    {
        navMeshAgent.stoppingDistance = newDistance;
    }


}

