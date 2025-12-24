using UnityEngine;

public class Attack : MonoBehaviour
{
    StatusManager myStatusManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myStatusManager = GetComponent<StatusManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MyAttack(StatusManager oppositeStatusManager)
    {
        float myAttackPower = myStatusManager.GetAttackPower();
        oppositeStatusManager.ApplyDamage(myAttackPower);
    }
}

//StatusManagerをアタッチする必要がある。
