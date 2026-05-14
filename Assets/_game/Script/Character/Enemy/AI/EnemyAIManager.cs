using NaughtyAttributes;
using Pathfinding;
using UnityEngine;

public class EnemyAIManager : MonoBehaviour
{
    // bây giờ cta sẽ build Finite State Machine (FSM) cho enemy, nên sẽ có 1 ScriptableObject để định nghĩa các trạng thái của enemy, và 1 class EnemyAIManager để quản lý trạng thái hiện tại của enemy và chuyển đổi giữa các trạng thái đó.

    [Expandable] public State _idleState;

    [Expandable] public State _chaseState;

    [Expandable] public State _meleeAttackState;

    [Expandable] public State _rangeAttackState;

    // cta cần 1 biến để lưu trạng thái hiện tại của enemy
    [Expandable] [SerializeField] State _currentState;


    [Header("Pathfinding")]
    public AIPath aiPath;
    public Seeker seeker;
    public AIDestinationSetter destinationSetter;

    public virtual void Awake()
    {
        // khi script sẽ tạo ra 1 bản sao của từng state
        if(_idleState !=null) _idleState = Instantiate(_idleState);
        if (_chaseState != null)  _chaseState = Instantiate(_chaseState);
        if (_meleeAttackState != null)  _meleeAttackState = Instantiate(_meleeAttackState);
        if (_rangeAttackState != null) _rangeAttackState = Instantiate(_rangeAttackState);

        // set current state ban đầu là idle
        ChangeState(_idleState);

        if (aiPath != null)  aiPath = GetComponent<AIPath>();
        if (seeker != null) seeker = GetComponent<Seeker>();
        if (destinationSetter != null) destinationSetter = GetComponent<AIDestinationSetter>();
    }


    public void SetUp(PlayerManager player)
    {
        destinationSetter.target = player.transform;
    }
    // phương thức để chuyển đổi trạng thái
    public void ChangeState(State newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit(this);
        }
        _currentState = newState;
        if (_currentState != null)
        {
            _currentState.Enter(this);
        }
    }
    // phương thức để chạy trạng thái sẽ để trong update
    public void Tick()
    {
        if (_currentState != null)
        {
            _currentState.Execute(this);
        }
    }

    public bool CanAttackRange()
    {
        if (_rangeAttackState != null)
        {
            return ((RangeAttackState)_rangeAttackState).CanAttackRange();
        }
        return false;
    }
}

public class State : ScriptableObject
{
    public virtual void Enter(EnemyAIManager enemy) { }
    public virtual void Execute(EnemyAIManager enemy) { }
    public virtual void Exit(EnemyAIManager enemy) { }
}
