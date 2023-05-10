using UnityEngine;

public class Turret : MonoBehaviour
{
    [HideInInspector]
    public Transform target;

    [Header("Attributes")]

    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]

    public string enemyTag;

    public Transform partToRotate;
    public float turnSpeed = 7f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Laser Settings")]
    public float DPS = 30;
    public float slowDownPct = 30;
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem laserImpactEffect;
    public Light laserLight;
    private Enemy enemyScr;

    public TurretUI turretUI;
    public GameObject turretInfo;
    public Node node;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, .2f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            enemyScr = target.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        fireCountdown -= Time.deltaTime;

        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                }

                if (laserImpactEffect.isPlaying)
                {
                    laserImpactEffect.Stop();
                    laserImpactEffect.Clear();
                }
                if (laserLight.enabled)
                {
                    laserLight.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }
    }

    void Laser()
    {
        lineRenderer.enabled = true;
        if (!laserImpactEffect.isPlaying)
        {
            laserImpactEffect.Play();
        }
        laserLight.enabled = true;

        enemyScr.TakeDamage(DPS * Time.deltaTime);
        enemyScr.SlowDown(slowDownPct);

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        laserImpactEffect.transform.rotation = Quaternion.LookRotation(dir);
        laserImpactEffect.transform.position = target.position + dir.normalized;
    }
    void LockOnTarget()
    {
        Vector3 dir = target.position - partToRotate.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);
    }

    void LockOnTarget(Transform _target, Transform _rotationPart)
    {
        Vector3 dir = _target.position - _rotationPart.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(_rotationPart.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        _rotationPart.rotation = Quaternion.Euler(0, rotation.y, 0);
    }

    void Shoot()
    {
        GameObject bulletGameObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGameObj.GetComponent<Bullet>();
        bullet.Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void OnMouseEnter()
    {
        turretInfo.SetActive(true);
        node.ShowTurretRadius(range);
    }

    private void OnMouseDown()
    {
        node.OnMouseDown();
    }

    private void OnMouseExit()
    {
        if (turretInfo.activeSelf)
        {
            turretInfo.SetActive(false);
        }
        AttackRadius.Node = null;

    }
}
