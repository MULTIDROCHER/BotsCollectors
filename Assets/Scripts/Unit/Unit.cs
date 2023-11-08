using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    private Base _base;
    private Flag _flag;
    private float _speed = 2;
    private bool _isFree = true;

    private UnitStateMachine _stateMachine;
    private IdleState _idleState = new IdleState();

    public bool IsFree => _isFree;
    public float Speed => _speed;
    public Flag Flag => _flag;

    private void Start()
    {
        _base = transform.GetComponentInParent<Base>();

        _stateMachine = new UnitStateMachine();
        _stateMachine.Initialize(_idleState);
    }

    public void Mine(Ore ore)
    {
        _isFree = false;
        var mining = new MiningState(this, ore);

        _stateMachine.ChangeState(mining);
        mining.OreMined += OnOreMined;
    }

    private void OnOreMined(Ore ore)
    {
        var delivering = new DeliverOreState(this, ore, _base);

        _stateMachine.ChangeState(delivering);
        _base.OnOreDelivered(ore);
        delivering.OreDelivered += GoToIdle;
    }

    public void OpenBase(Flag flag)
    {
        _isFree = false;
        _flag = flag;
        var openBase = new OpenBaseState(this, flag);

        _stateMachine.ChangeState(openBase);
        openBase.BaseOpened += SetBase;
    }

    private void GoToIdle()
    {
        _isFree = true;

        _stateMachine.ChangeState(_idleState);
    }

    private void SetBase(Base newBase)
    {
        _base = newBase;
        GoToIdle();
    }
}