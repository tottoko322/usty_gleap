using UnityEngine;

public class BulletEnemyBehaviour : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float stopDistance = 1f;

    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotInterval = 1f;
    [SerializeField] private float fireDistance = 5f;
    [SerializeField] private float spawnOffset = 0.5f;

    private Transform player;
    private float speed;
    private bool isGameOver = false;
    private StatusManager statusManager;
    private StatusActionHolder statusActionHolder;
    private SelfStatusAction deathAction;
    private float lastShotTime = -999f;

    void Start()
    {
        statusManager = GetComponent<StatusManager>();
        statusActionHolder = GetComponent<StatusActionHolder>();
        if (statusActionHolder != null)
        {
            deathAction = statusActionHolder.GetSelfStatusActionFromIndex(0);
        }
        SetPlayer();
    }

    void Update()
    {
        if (statusManager != null)
        {
            speed = statusManager.GetSpeed();
        }

        if (player == null || isGameOver)
        {
            SetPlayer();
            if (player == null) return;
        }

        Vector3 direction = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance > stopDistance)
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        LookAtPlayer();
        HandleShootingByDistance();

        if (deathAction != null)
        {
            deathAction.Execute(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && false)
        {
            Debug.Log("Game Over!");
            isGameOver = true;

            if (collision.gameObject.TryGetComponent<PlayerController>(out var playerScript))
            {
                playerScript.enabled = false;
            }

            this.enabled = false;
        }
    }

    private void LookAtPlayer()
    {
        if (player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void HandleShootingByDistance()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= fireDistance && Time.time - lastShotTime >= shotInterval)
        {
            ShotBullet();
            lastShotTime = Time.time;
        }
    }

    private void ShotBullet()
    {
        if (bulletPrefab == null) return;

        Vector3 spawnPos = transform.position + transform.right * spawnOffset;
        Instantiate(bulletPrefab, spawnPos, transform.rotation);
    }

    private void SetPlayer()
    {
        if (PlayerManager.Instance == null || PlayerManager.Instance.CurrentPlayer == null) return;
        player = PlayerManager.Instance.CurrentPlayer;
    }
}
