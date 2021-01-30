using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    private static EnemyController _instance;
    public static EnemyController Instance{
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<EnemyController>();
            }

            return _instance;
        }
    }
    
    public bool isMoving;
    public Vector3 nextStepPos;
    public bool isChasingPlayer;
    private float MoveSpeed => GameManager.Instance.BlockSize / GameManager.Instance.MoveRoundDuration;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextStepPos,
                MoveSpeed * Time.deltaTime);
            if (transform.position == nextStepPos)
            {
                isMoving = false;
            }
        }
        else
        {
            // check hit player
        }
    }

    public void TryBeginChasePlayer()
    {
        if (!isChasingPlayer) return;
        
        // set nextStepPos
        // if already next to player's next step, then no need to move
        
        List<AStarSpot> roadPath = PathFinderManager.Instance.CreatePath(
            transform.position, PlayerController.Instance.currMoveTargetPos, 1);

        if (roadPath == null)
        {
            return;
        }
        
        nextStepPos = PathFinderManager.Instance.GridPosToWorld(roadPath[0].X, roadPath[0].Y) + 
                      new Vector3(0.5f, 0.5f, 0);
        
        if (Vector3.Distance(nextStepPos, PlayerController.Instance.currMoveTargetPos) <= 0.3f)
        {
            return;
        }
        
        isMoving = true;
    }
}
