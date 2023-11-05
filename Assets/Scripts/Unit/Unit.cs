using UnityEngine;

public class Unit : MonoBehaviour
{
    private Base _base;
    private float _speed = 2;
    private bool _isFree = true;

    private UnitStateMachine _stateMachine;
    private IdleState _idleState = new IdleState();

    public bool IsFree => _isFree;
    public float Speed => _speed;

    private void Start()
    {
        _base = FindObjectOfType<Base>();

        _stateMachine = new UnitStateMachine();
        _stateMachine.Initialize(_idleState);
    }

    public void Mine(Ore ore)
    {
        _isFree = false;
        MiningState mining = new MiningState(this, ore);

        _stateMachine.ChangeState(mining);
        mining.OreMined += OnOreMined;
    }

    private void OnOreMined(Ore ore)
    {
        DeliverOreState delivering = new DeliverOreState(this, ore, _base);

        _stateMachine.ChangeState(delivering);
        delivering.OreDelivered += GoToIdle;
    }

    private void GoToIdle()
    {
        _isFree = true;
        _stateMachine.ChangeState(_idleState);
    }
}