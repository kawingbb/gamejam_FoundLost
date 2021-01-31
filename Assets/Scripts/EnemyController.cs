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

    public bool disable;
    public bool isMoving;
    public Vector3 nextStepPos;
    public bool isChasingPlayer;
    private float MoveSpeed => GameManager.Instance.BlockSize / GameManager.Instance.MoveRoundDuration;
    
    public Animator animator;
    public SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        disable = true;
        animator.SetBool("IsMoving", isMoving);
        renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (disable) return;
        
        renderer.enabled = true;

        animator.SetBool("IsMoving", isMoving);

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
            Debug.Log("Checking");
            // check hit player
            if (Physics2D.OverlapCircle(GetTargetPositionByDirection(new Vector2(1, 0)),
                    GameManager.Instance.BlockSize / 3f, GameManager.Instance.PlayerLayer) ||
                Physics2D.OverlapCircle(GetTargetPositionByDirection(new Vector2(-1, 0)),
                    GameManager.Instance.BlockSize / 3f, GameManager.Instance.PlayerLayer) ||
                Physics2D.OverlapCircle(GetTargetPositionByDirection(new Vector2(0, 1)),
                    GameManager.Instance.BlockSize / 3f, GameManager.Instance.PlayerLayer) ||
                Physics2D.OverlapCircle(GetTargetPositionByDirection(new Vector2(0, -1)),
                    GameManager.Instance.BlockSize / 3f, GameManager.Instance.PlayerLayer))
            {
                GameManager.Instance.LoseGame();
            }
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
        
        animator.SetFloat("Horizontal", nextStepPos.x - transform.position.x);
        animator.SetFloat("Vertical", nextStepPos.y - transform.position.y);
        
        if (Vector3.Distance(nextStepPos, PlayerController.Instance.currMoveTargetPos) <= 0.3f)
        {
            return;
        }
        
        isMoving = true;
    }
    
    private Vector3 GetTargetPositionByDirection(Vector2 direction)
    {
        Vector3 res = transform.position + new Vector3(direction.x, direction.y, 0) * GameManager.Instance.BlockSize;
        return res;
    }
}
