using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public EnemyType myType;

    public float moveDistance = 1000f;
    private float mySpeed = 5f;

    [Header("AI Stuff")]
    private PatrolType myPatrolType;
    /// <summary>
    /// used for all Patrol movement
    /// </summary>
    [SerializeField] private Transform moveToPos;
    /// <summary>
    /// used for Loop patrol movement
    /// </summary>
    private Transform startPos;
    /// <summary>
    /// used for Loop patrol movement
    /// </summary>
    private Transform endPos;
    /// <summary>
    /// used for Loop patrol movement
    /// </summary>
    private bool reverse;
    /// <summary>
    /// used for Linear patrol movement
    /// </summary>
    private int patrolPoint;
    public bool moving;

    public float distanceBuffer = 0.3f;
    private int myHealth = 100;
    private EnemyManager _EM;
    
    public void Setup(Transform _startPos)
    {
        _EM = FindFirstObjectByType<EnemyManager>();
        switch (myType)
        {
            case EnemyType.OneHanded:
                mySpeed = 5f; 
                myHealth = 100;
                myPatrolType = PatrolType.Linear;
                break;
            case EnemyType.TwoHanded:
                mySpeed = 3f; 
                myHealth = 150;
                myPatrolType = PatrolType.Loop;
                break;
            case EnemyType.Archer:
                mySpeed = 7f;
                myHealth = 50;
                myPatrolType = PatrolType.Random;
                break;
        }

        startPos = _startPos;
        //endPos = _EM.GetRandomSpawn();
        endPos = startPos;
        moveToPos = endPos;

        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        moving = true;
        switch (myPatrolType)
        {
            case PatrolType.Linear:
                moveToPos = _EM.GetExactSpawnPoint(patrolPoint);
                patrolPoint = patrolPoint != _EM.GetSpawnPointCount()-1 ? patrolPoint+1 : 0;
                break;
            case PatrolType.Random:
                moveToPos = _EM.GetRandomSpawn();
                break;
            case PatrolType.Loop:
                moveToPos = reverse ? startPos : endPos;
                //yield return new WaitForSeconds(1);
                reverse = !reverse;
                break;
        }

        transform.LookAt(moveToPos);
        while (Vector3.Distance(transform.position, moveToPos.position) > distanceBuffer)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveToPos.position, Time.deltaTime * mySpeed);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        moving = false;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Move());
    }

    /*private IEnumerator Move()
    {
        for (int i = 0; i < moveDistance; i++)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * mySpeed);
            yield return null;
        }
        transform.Rotate(Vector3.up * 180);
        yield return new WaitForSeconds(Random.Range(1,3));
        StartCoroutine(Move());
    }*/
}
