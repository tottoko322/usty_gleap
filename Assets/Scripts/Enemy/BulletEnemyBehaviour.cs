using System.Collections;
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
    private GenerateGrave generateGrave;
    private SelfStatusAction deathAction;
    private Coroutine shotRoutine;

    void Start()
    {
        statusManager = GetComponent<StatusManager>();
        generateGrave = GetComponent<GenerateGrave>();
        statusActionHolder = GetComponent<StatusActionHolder>();
        if (statusActionHolder != null)
        {
            deathAction = statusActionHolder.GetSelfStatusActionFromIndex(0);
        }

        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject != null ? playerObject.transform : null;
    }

    void Update()
    {
        if (statusManager != null)
        {
            speed = statusManager.GetSpeed();
        }

        if (player == null || isGameOver)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject == null) return;
            player = playerObject.transform;
            return;
        }

        Vector3 direction = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance > stopDistance)
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        LookAtPlayer();
        HandleShootingByDistance();

        if (statusManager != null && statusManager.BaseStatus.CurrentHP <= 0)
        {
            if (generateGrave != null)
            {
                generateGrave.Generate(transform.position);
            }
        }

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

        if (dist <= fireDistance)
        {
            if (shotRoutine == null)
            {
                shotRoutine = StartCoroutine(ShotLoop());
            }
        }
        else
        {
            if (shotRoutine != null)
            {
                StopCoroutine(shotRoutine);
                shotRoutine = null;
            }
        }
    }

    private IEnumerator ShotLoop()
    {
        while (true)
        {
            ShotBullet();
            yield return new WaitForSeconds(shotInterval);
        }
    }

    private void ShotBullet()
    {
        if (bulletPrefab == null) return;

        Vector3 spawnPos = transform.position + transform.right * spawnOffset;
        Instantiate(bulletPrefab, spawnPos, transform.rotation);
    }
}
