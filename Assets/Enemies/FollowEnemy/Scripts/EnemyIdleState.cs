using UnityEngine;

/// <summary>
/// 敵がアイドル状態（移動しない）
/// </summary>
public class EnemyIdleState : IEnemyState
{
    private readonly EnemyFollowController controller;
    private readonly FollowEnemyAnimation animation;

    public EnemyIdleState(EnemyFollowController controller, FollowEnemyAnimation animation)
    {
        this.controller = controller;
        this.animation = animation;
    }

    public void Enter()
    {
        animation.Idle();
    }

    public void Update()
    {
        // アイドル状態では何もしない
    }

    public void Exit()
    {
    }
}
