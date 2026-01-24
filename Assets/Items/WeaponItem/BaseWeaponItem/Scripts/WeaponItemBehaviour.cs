using UnityEngine;

public class WeaponItemBehaviour : MonoBehaviour
{
    [Header("Weapon Core Setting")]
    [SerializeField] private GameObject[] weaponCores;
    private WeaponCoreHolder weaponCoreHolder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Playerタグのときだけ処理
        if (!other.CompareTag("Player")) return;

        // PlayerのGraveHolderにgraveを入れる
        weaponCoreHolder = other.gameObject.GetComponent<WeaponCoreHolder>();
        foreach(GameObject weaponCore in weaponCores)
        {
            weaponCoreHolder.AddWeaponCore(weaponCore);
        }
        Destroy(gameObject);
    }
}
