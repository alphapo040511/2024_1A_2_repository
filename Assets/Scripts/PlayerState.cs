using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState //��� �÷��̾� ������ �⺻�� �Ǵ� �߻� Ŭ����
{
    protected PlayerStateMachine stateMachine;      //���� �ӽſ� ���� ����
    protected PlayerController playerController;    //�÷��̾� ��Ʈ�ѷ��� ���� ����
    protected PlayerAnimationManager animationManager;

    public PlayerState(PlayerStateMachine stateMachine) //���� �ӽŰ� �÷��̾� ��Ʈ�ѷ� ���� �ʱ�ȭ
    {
        this.stateMachine = stateMachine;
        this.playerController = stateMachine.playerController;
        this.animationManager = stateMachine.GetComponent<PlayerAnimationManager>();
    }

    //���� �޼��� �� : ���� Ŭ�������� �ʿ信 ���� �������̵�
    public virtual void Enter() { }                 //���� ���� �� ȣ��
    public virtual void Exit() { }                  //���� ���� �� ȣ��
    public virtual void Update() { }                //�� ������ ȣ��
    public virtual void FixedUpdate() { }         //���� �ð� �������� ȣ�� (���� �����)

    //���� ��ȯ ������ üũ�ϴ� �޼���
    protected void CheckTransitions()
    {
        if(playerController.isGrounded())   //���� ���� ���� ��ȯ ����
        {
            if (Input.GetKeyDown(KeyCode.Space))                                            //���� Ű�� ������ ��
            {
                stateMachine.TransitionToState(new JumppingState(stateMachine));
            }
            else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)    //�̵� Ű�� ������ ��
            {
                stateMachine.TransitionToState(new MovingState(stateMachine));
            }
            else                                                                            //�ƹ� Ű�� ������ �ʾ��� ��
            {
                stateMachine.TransitionToState(new IdelState(stateMachine));
            }
        }
        //���߿� ���� ���� ���� ��ȯ ����
        else
        {
            if(playerController.GetVerticalVelocity() > 0)                                  //Y�� �̵� �ӵ� ���� ��� �� �� ������
            {
                stateMachine.TransitionToState(new JumppingState(stateMachine));
            }
            else                                                                            //Y�� �̵� �ӵ� ���� ���� �� �� ������
            {
                stateMachine.TransitionToState(new Fallingstate(stateMachine));
            }
        }
    }
}

//IdleState : �÷��̾ ������ �ִ� ����
public class IdelState : PlayerState
{
    public IdelState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();     //�� ������ ���� ���� ��ȯ ���� üũ
    }
}

//MovingState : �÷��̾ �̵� ���� ����
public class MovingState : PlayerState
{
    private bool isRunning;
    public MovingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        //�޸��� �Է� Ȯ��
        isRunning = Input.GetKeyDown(KeyCode.LeftShift);

        CheckTransitions();     //�� ������ ���� ���� ��ȯ ���� üũ
    }

    public override void FixedUpdate()
    {
        playerController.HandleMovement();      //���� ��� �̵� ó��
    }
}

//JumppingState : �÷��̾ ���� ���� ��
public class JumppingState : PlayerState
{
    public JumppingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();     //�� ������ ���� ���� ��ȯ ���� üũ
    }

    public override void FixedUpdate()
    {
        playerController.HandleMovement();      //���� ��� �̵� ó��
    }
}

//Fallingstate : �÷��̾ ������ ��
public class Fallingstate : PlayerState
{
    public Fallingstate(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();     //�� ������ ���� ���� ��ȯ ���� üũ
    }

    public override void FixedUpdate()
    {
        playerController.HandleMovement();      //���� ��� �̵� ó��
    }
}


