using UnityEngine;

public class Effector : MonoBehaviour
{
    [SerializeField] private Effect effect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GiveEffect(GameObject other)
    {
        Effect copiedEffect = Instantiate(effect);
        if (other.TryGetComponent<IHasStatusManager>(out var hasStatus))
        {
            StatusManager otherStatusManager = hasStatus.Status;
            otherStatusManager.AddEffect(copiedEffect);
        }
    }
}

