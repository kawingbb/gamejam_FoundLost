using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isMoving;
    private Vector2 _movementInput;
    private Vector2 _currFacingDirection;
    private Vector3 _currMoveTargetPos;
    private float MoveSpeed => GameManager.Instance.BlockSize / GameManager.Instance.MoveRoundDuration;

    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        FilterInput();
        
        animator.SetBool("IsMoving", isMoving);
        
        TryMoveToTarget();

        if (GameManager.Instance.ReadyToControl)
        {
            isMoving = false;
            
            if (Mathf.Abs(_movementInput.x) > 0.5f)
            {

                // move if ready and not blocked
                if (!Physics2D.OverlapCircle(GetTargetPositionByDirection(_movementInput),
                    GameManager.Instance.BlockSize / 3f, GameManager.Instance.BlockMovementLayer))
                {
                    // set currMoveTarget position and begin move
                    _currMoveTargetPos = GetTargetPositionByDirection(_movementInput);
                    isMoving = true;
                    // reset game round time
                    GameManager.Instance.ResetNextControlTime();
                    
                }
                
                // face look at direction
                UpdateAnimatorDirection();
            }
            else if (Mathf.Abs(_movementInput.y) > 0.5f)
            {
                // move if ready and not blocked
                if (!Physics2D.OverlapCircle(GetTargetPositionByDirection(_movementInput),
                    GameManager.Instance.BlockSize / 3f, GameManager.Instance.BlockMovementLayer))
                {
                    // set currMoveTarget position and begin move
                    _currMoveTargetPos = GetTargetPositionByDirection(_movementInput);
                    isMoving = true;
                    // reset game round time
                    GameManager.Instance.ResetNextControlTime();
                }
                
                // face look at direction
                UpdateAnimatorDirection();

            }
        }
    }

    private void TryMoveToTarget()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currMoveTargetPos,
                MoveSpeed * Time.deltaTime);
        }
    }

    private void FilterInput()
    {
        _movementInput.x = Input.GetAxisRaw("Horizontal");
        _movementInput.y = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(_movementInput.x) > 0.5f)
        {
            _movementInput.y = 0;
        }
    }

    private Vector3 GetTargetPositionByDirection(Vector2 direction)
    {
        Vector3 res = transform.position + new Vector3(direction.x, direction.y, 0) * GameManager.Instance.BlockSize;
        return res;
    }

    private void UpdateAnimatorDirection()
    {
        animator.SetFloat("Horizontal", _movementInput.x);
        animator.SetFloat("Vertical", _movementInput.y);
    }
}
