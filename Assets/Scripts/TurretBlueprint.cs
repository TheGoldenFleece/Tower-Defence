using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public GameObject upgradePrefab;
    public int cost;
    public int upgradeCost;

    [SerializeField] string name;
    [HideInInspector] public bool isUpgraded;
    [SerializeField] float range;
    [SerializeField] float upgradedRange;
    [SerializeField] float damage;
    [SerializeField] float upgradedDamage;
    [SerializeField] float fireRate;
    [SerializeField] float upgradedFireRate;
    [SerializeField] float explosionRadius;
    [SerializeField] float slowDownPct;
    [SerializeField] float upgradedSlowDownPct;
    [SerializeField] float _DPS;
    [SerializeField] float upgradedDPS;
    public string Name { get => name; }
    public float ExplosionRadius { get => explosionRadius; }
    public float Range { get => isUpgraded ? upgradedRange : range; }
    public float Damage { get => isUpgraded ? upgradedDamage : damage; }
    public float SlowDownPct { get => isUpgraded ? upgradedSlowDownPct : slowDownPct; }
    public float FireRate { get => isUpgraded ? upgradedFireRate : fireRate; }
    public float DPS { get => isUpgraded ? upgradedDPS : _DPS; }

    public int SellValue
    {
        get {
            return isUpgraded ? (upgradeCost + cost) / 2 : cost / 2;
        }
    }

    public TurretBlueprint(TurretBlueprint blueprint) {
        this.prefab = blueprint.prefab;
        this.upgradePrefab = blueprint.upgradePrefab;
        this.cost = blueprint.cost;
        this.upgradeCost = blueprint.upgradeCost;
        this.name = blueprint.name;

        this.range = blueprint.range;
        this.upgradedRange = blueprint.upgradedRange;
        this.damage = blueprint.damage;
        this.upgradedDamage = blueprint.upgradedDamage;
        this.fireRate = blueprint.fireRate;
        this.upgradedFireRate = blueprint.upgradedFireRate;

        this.explosionRadius = blueprint.explosionRadius;
        this.slowDownPct = blueprint.slowDownPct;
        this.upgradedSlowDownPct = blueprint.upgradedSlowDownPct;
        this._DPS = blueprint._DPS;
        this.upgradedDPS = blueprint.upgradedDPS;
    }

    public TurretBlueprint() { }
}
