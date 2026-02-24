using UnityEngine;

[CreateAssetMenu(fileName = "CircleGenerateAction", menuName = "GenerateAction/CircleGenerateAction")]
public class CircleGenerateAction : GenerateAction
{
    [Header("Effect Parameters")]
    [Min(1f)]
    [SerializeField] private float _radius;
    [Min(2f)]
    [SerializeField] private int _count;
    [SerializeField] private GameObject _objectToGenerate;
    public override void Execute(GameObject user)
    {
        for(int i = 0; i < _count; i++)
        {
            float angle = i*(360f / _count);
            float radian = angle* Mathf.Deg2Rad;
            Vector2 spawnPosition = new Vector2(
                user.transform.position.x + _radius * Mathf.Cos(radian),
                user.transform.position.y + _radius * Mathf.Sin(radian)
            );
            Instantiate(_objectToGenerate, spawnPosition, Quaternion.identity);
        }
    }
}
