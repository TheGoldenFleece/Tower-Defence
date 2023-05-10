using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Enemy))]

public class EnemyMovement : MonoBehaviour
{
    Enemy enemy;
    Vector3 target;
    int indexOfWaypoints = 1;
    Vector3 offset = new Vector3(0, 1.5f, 0);
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    private void Start()
    {
        ChangeToNextTarget();
    }
    private void Update()
    {
        if (Vector3.Distance(target, transform.position) <= .5f)
        {
            ChangeToNextTarget();
        }

        Vector3 dir = target - transform.position;
        dir = dir.normalized * enemy.Speed * Time.deltaTime;
        transform.Translate(dir);

        enemy.Speed = enemy.startSpeed;
    }

    void ChangeToNextTarget()
    {
        if (indexOfWaypoints > Path.PathList.Count - 1)
        {
            EndPath();
            return;
        }

        target = Path.PathList[indexOfWaypoints].Position + offset;
        indexOfWaypoints++;
    }
    void EndPath()
    {
        if (PlayerStats.Lives != 0)
        {
            PlayerStats.Lives--;
        }
        Spawner.EnemiesAlive--;

        Destroy(gameObject);
    }
}
