using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TurretUI : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField] Shop shop;
    [SerializeField] Text text;
    [SerializeField] GameObject turretInfo;
    TurretBlueprint blueprint;
    string info;
    private void Start()
    {
        turretInfo.SetActive(false);

        if (shop == null) return;
        SetValues();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        turretInfo.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        turretInfo.SetActive(false);
    }


    public void SetValues()
    {
        switch (gameObject.name)
        {
            case "StandardTurretItem":
                {
                    blueprint = shop.standardTurret;
                    info = $"Damage: {blueprint.Damage}\n" +
                           $"Range: {blueprint.Range}\n" +
                           $"FireRate: {blueprint.FireRate}\n" +
                           $"Cost: {blueprint.cost}";
                    break;
                }
            case "MissileLauncherItem":
                {
                    blueprint = shop.missileLauncher;
                    info = $"Damage: {blueprint.Damage}\n" +
                           $"Range: {blueprint.Range}\n" +
                           $"Explosion Radius: {blueprint.ExplosionRadius}\n" +
                           $"FireRate: {blueprint.FireRate}\n" +
                           $"Cost: {blueprint.cost}";
                    break;
                }
            case "LaserBeamerItem":
                {
                    blueprint = shop.laserBeamer;
                    info = $"Damage: {blueprint.DPS}\n" +
                           $"Range: {blueprint.Range}\n" +
                           $"Explosion Radius: {blueprint.SlowDownPct}\n" +
                           $"FireRate: {blueprint.FireRate}\n" +
                           $"Cost: {blueprint.cost}";
                    break;
                }
        }

        text.text = info;
    }

    public void SetValues(TurretBlueprint blueprint)
    {
        switch (blueprint.Name)
        {
            case "Turret":
                {
                    info = $"Damage: {blueprint.Damage}\n" +
                           $"Range: {blueprint.Range}\n" +
                           $"FireRate: {blueprint.FireRate}\n" +
                           $"Cost: {blueprint.cost}";
                    break;
                }
            case "MissileLauncher":
                {
                    info = $"Damage: {blueprint.Damage}\n" +
                           $"Range: {blueprint.Range}\n" +
                           $"Explosion Radius: {blueprint.ExplosionRadius}\n" +
                           $"FireRate: {blueprint.FireRate}\n" +
                           $"Cost: {blueprint.cost}";
                    break;
                }
            case "LaserBeamer":
                {
                    info = $"Damage: {blueprint.DPS}\n" +
                           $"Range: {blueprint.Range}\n" +
                           $"Explosion Radius: {blueprint.SlowDownPct}\n" +
                           $"FireRate: {blueprint.FireRate}\n" +
                           $"Cost: {blueprint.cost}";
                    break;
                }
        }

        text.text = info;
    }
}
