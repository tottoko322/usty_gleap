using UnityEngine;

[RequireComponent(typeof(FollowEnemyAnimation))]
public class EnemyFollowController : MonoBehaviour
{
    private Transform player;

    public float speed = 4f;
    public float stopDistance = 1f;

    private float pastHp = 11f;

    private StatusManager statusManager;
    private StatusActionHolder statusActionHolder;
    private FollowEnemyAnimation animation;

    // 状態管理
    private IEnemyState currentState;
    private EnemyIdleState idleState;
    private EnemyMoveState moveState;
    private EnemyHurtState hurtState;
    private EnemyDeadState deadState;
    private EnemyAttackState attackState;

    void Start()
    {
        statusManager = GetComponent<StatusManager>();
        statusActionHolder = GetComponent<StatusActionHolder>();
        animation = GetComponent<FollowEnemyAnimation>();
        player = PlayerManager.Instance.CurrentPlayer;

        // 各状態を初期化
        InitializeStates();

        // 初期状態を設定
        ChangeState(idleState);
    }

    private void InitializeStates()
    {
        idleState = new EnemyIdleState(this, animation);
        moveState = new EnemyMoveState(this, statusManager, animation, transform, stopDistance);
        hurtState = new EnemyHurtState(this, animation, statusManager);
        deadState = new EnemyDeadState(animation, gameObject);
        attackState = new EnemyAttackState(this, animation, statusActionHolder);
    }

    void Update()
    {
        // 死亡状態なら何もしない
        if (currentState is EnemyDeadState)
        {
            currentState.Update();
            return;
        }

        // 状態確認と遷移処理
        CheckDeath();
        CheckHurt();
        CheckMove();

        // 現在の状態の更新処理を実行
        currentState.Update();
    }

    private void CheckMove()
    {
        // 移動可能かつ移動状態以外の場合、移動状態に遷移
        if (!(currentState is EnemyMoveState) && 
            !(currentState is EnemyDeadState) &&
            !(currentState is EnemyAttackState))
        {
            ChangeState(moveState);
        }
    }

    private void CheckDeath()
    {
        float currentHp = statusManager.BaseStatus.CurrentHP;

        if (currentHp > 0) return;

        ChangeState(deadState);
    }

    private void CheckHurt()
    {
        if (currentState is EnemyDeadState) return;

        float currentHp = statusManager.BaseStatus.CurrentHP;

        if (currentHp >= pastHp) return;

        pastHp = currentHp;
        ChangeState(hurtState);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        attackState.SetTargetObject(collision.gameObject);
        ChangeState(attackState);
    }

    /// <summary>
    /// 状態を変更する
    /// </summary>
    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    /// <summary>
    /// 外部から状態取得用（状態クラスから利用）
    /// </summary>
    public EnemyIdleState GetIdleState() => idleState;
    public EnemyMoveState GetMoveState() => moveState;

    public Transform GetPlayerTransform() => player;
    public void SetPlayerTransform(Transform newPlayer) => player = newPlayer;
}