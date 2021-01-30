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
        animator.SetBool("IsMoving", isMoving);
        _movementInput = Vector2.down;
        UpdateAnimatorDirection();
    }

    // Update is called once per frame
    void Update()
    {
        FilterInput();
        
        animator.SetBool("IsMoving", isMoving);
        
        TryMoveToTarget();
        
        TryMovementControl();

        TryInteractControl();
    }

    private void TryMoveToTarget()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currMoveTargetPos,
                MoveSpeed * Time.deltaTime);
            if (transform.position == _currMoveTargetPos)
            {
                isMoving = false;
            }
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

    private void TryInteractControl()
    {
        if (GameManager.Instance.ReadyToControl && !GameManager.Instance.movementInputLock)
        {
            if (Input.GetKeyDown("space"))
            {
                var forwardObjects = Physics2D.OverlapCircleAll(GetTargetPositionByDirection(_currFacingDirection),
                    GameManager.Instance.BlockSize / 3f, GameManager.Instance.InteractTrigger);
                foreach (Collider2D forwardObject in forwardObjects)
                {
                    Trigger triggerObject = forwardObject.gameObject.GetComponent<Trigger>();
                    if (triggerObject != null && triggerObject.enableInteract)
                    {
                        triggerObject.BeginTrigger();
                    }
                }
            }
        }
    }

    private void TryMovementControl()
    {
        if (GameManager.Instance.ReadyToControl && !GameManager.Instance.movementInputLock)
        {
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

    private Vector3 GetTargetPositionByDirection(Vector2 direction)
    {
        Vector3 res = transform.position + new Vector3(direction.x, direction.y, 0) * GameManager.Instance.BlockSize;
        return res;
    }

    private void UpdateAnimatorDirection()
    {
        _currFacingDirection = _movementInput;
        animator.SetFloat("Horizontal", _movementInput.x);
        animator.SetFloat("Vertical", _movementInput.y);
    }
}
