using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public Animator animator;
    public PlayerStateMachine stateMachine;

    //�ִϸ����� �Ķ���� �̸����� ����� ����
    private const string PARAM_IS_MOVEING = "IsMoving";
    private const string PARAM_IS_RUNNING = "IsRunning";
    private const string PARAM_IS_JUMPPING = "IsJumpping";
    private const string PARAM_IS_FALLING = "IsFalling";
    private const string PARAM_IS_ATTACK_TRIGGER = "Attack";

    void Update()
    {
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if(stateMachine.currentState != null)
        {
            //��� bool �Ķ���͸� �ʱ�ȭ
            ResetAllBoolParameters();

            //���� ���¿� ���� �ش��ϴ� �ִϸ��̼� �Ķ���� ����
            switch(stateMachine.currentState)
            {
                case IdelState:
                    //Idel ���´� ��� �Ķ���Ͱ� false�� ����
                    break;
                case MovingState:
                    animator.SetBool(PARAM_IS_MOVEING, true);
                    //�޸��� �Է� Ȯ��
                    if(Input.GetKey(KeyCode.LeftShift))
                    {
                        animator.SetBool(PARAM_IS_RUNNING, true);
                    }
                    break;
                case JumppingState:
                    animator.SetBool(PARAM_IS_JUMPPING, true);
                    break;
                case Fallingstate:
                    animator.SetBool(PARAM_IS_FALLING, true);
                    break;
            }
        }
    }

    //���� �ִϸ��̼� Ʈ����
    public void TirggerAttack()
    {
        animator.SetTrigger(PARAM_IS_ATTACK_TRIGGER);
    }

    //��� bool �Ķ���� �ʱ�ȭ
    private void ResetAllBoolParameters()
    {
        animator.SetBool(PARAM_IS_MOVEING, false);
        animator.SetBool(PARAM_IS_RUNNING, false);
        animator.SetBool(PARAM_IS_JUMPPING, false);
        animator.SetBool(PARAM_IS_FALLING, false);
    }
}
