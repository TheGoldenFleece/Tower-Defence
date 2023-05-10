using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform _target;
    [Header("Common Setup Fields")]
    public GameObject hitEffectPrefab;
    public float speed = 70;
    public int damage = 50;
    [Header("Specific Bullet Fields")]    
    public float explosionRadius = 0f;
    public string enemyTag = "Enemy";

    public void Seek(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = _target.position - transform.position;
        float distanceThisFrame = Time.deltaTime * speed;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(_target);
    }

    void HitTarget()
    {
        GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position, transform.rotation);
        Destroy(hitEffect, 2f);

        if (explosionRadius == 0) {
            Damage(_target);
        }
        else {
            Explode();
        }

        Destroy(gameObject);
    }

    void Damage(Transform enemy) {

        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }

    void Explode(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in colliders) {
            if (col.CompareTag(enemyTag)) {
                Damage(col.transform);
            }
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
