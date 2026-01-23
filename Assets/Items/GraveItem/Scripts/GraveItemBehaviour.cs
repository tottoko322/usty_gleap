using UnityEngine;

public class GraveItemBehaviour : MonoBehaviour
{
    [Header("Grave Setting")]
    [SerializeField] private GameObject[] graves;
    private GraveHolder graveHolder;
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
        graveHolder = other.gameObject.GetComponent<GraveHolder>();
        foreach(GameObject grave in graves)
        {
            graveHolder.AddGrave(grave);
        }
        Destroy(gameObject);
    }
}
