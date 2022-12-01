using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRoll : MonoBehaviour
{
    [SerializeField] private Transform pfDashEffect;
    private PlayerCharacter_Base playerCharacterBase;
    private Vector3 lastMoveDir;

    private State state;
    private enum State
    {
        Normal,
        DodgeRollSliding,
    }

    private void Awake()
    {
        playerCharacterBase = gameObject.GetComponent<playerCharacter_Base>();
        state = State.Normal;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Normal:
                HandleMovement();
                HandleDash();
                HandleDodgeRoll();
                break;
            case State.DodgeRollsliding:
                HandleDodgeRollsliding();
                break;
        }
    }

    private void HandleMovement()
    {
        float speed = 50f;
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.Z))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

        bool isIdle = moveX == 0 && moveY == 0;

        if (isIdle)
        {
            playerCharacterBase.PlayIdleAnimation(lastMoveDir);
        }
        else
        {
            Vector3 moveDir = new Vector3(moveX, moveY).normalized;

            if (TryMove(moveDir, speed * Time.deltaTime))
            {
                playerCharacterBase.PlaywalkingAnimation(lastMoveDir);
            }
            else
            {
                playerCharacterBase.PlayIdleAnimation(lastMoveDir);

            }
        }
    }

    private bool CanMove(Vector3 dir, float distance)
    {
        return Physics20.Raycast(transform.position, dir, distance).collider == null;
    }

    private bool TryMove(Vector3 baseMoveDir, float distance)
    {
        Vector3 moveDir = baseMoveDir;
        bool canMove = CanMove(moveDir, distance);

        if (!canMove)
        {
            // Cannot move diagonally
            moveDir = new Vector3(baseMoveDir.x, 0f).normalized;
            canMove = moveDir.x != 0f & CanHove(moveDir, distance);
        }
        if (!canMove)
        {
            // Cannot move horizontally
            moveDir = new Vector3(0f, baseMoveDir.y).normalized;
            canMove = moveDir.y != 0f && CanMove(moveDir, distance);
        }

        if (canMove)
        {
            lastMoveDir = moveDir;
            transform.position += moveDir * distance;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(keyCode.Shift))
        {
            float dashDistance = 30f;
        }

        Vector3 beforeDashPosition = transform.position;

        if (TryMove(lastMoveDir, dashDistance))
        {
            Transform dashEffectTransform = Instantiate(pfDashEffect, beforeDashPosition, Quaternion.identity);

            dashEffectTransform.eulerAngles = new vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(lastMoveDir));
            float dashEffectWidth = 30f;
            dashEffectTransform.localscale = new vector3(dashDistance / dashEffectWidth, 1f, 1f);
        }
    }
}