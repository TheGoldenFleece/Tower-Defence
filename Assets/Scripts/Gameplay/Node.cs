using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

public class Node : MonoBehaviour
{
    private Renderer render;
    [Header("Color Settings")]
    public Color hoverColor;
    public Color forbiddanceColor;
    private Color defaultColor;

    [Header("Main settings")]
    [SerializeField] Vector3 buildOffset;
    [SerializeField] Vector3 lineRendererOffset;
    public Vector3 BuildPosition { get => transform.position + buildOffset; }
    [HideInInspector]
    public TurretBlueprint Blueprint { private set; get;}
    GameObject _turret;
    BuildManager buildManager;

    GameObject turretInfo;

    private void Start()
    {
        render = GetComponent<Renderer>();
        defaultColor = render.material.color;
        buildManager = BuildManager.instance;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider == GetComponent<Collider>())
        {
            OnMouseEnterCheck();
        }
        else
        {
            OnMouseExitCheck();
        }

    }

    public void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (_turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
        {
            buildManager.DeselectNode();
            return;
        }

        BuildTurretOn(buildManager.TurretToBuild);
    }
    public void ShowTurretRadius()
    {
        buildManager.attackRadius.Display(buildManager.TurretToBuild.Range, this);
    }
    public void ShowTurretRadius(float range)
    {
        buildManager.attackRadius.Display(range, this);
    }
    private void OnMouseEnterCheck()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (_turret != null) return;

        if (!buildManager.CanBuild)
        {
            Shop.DeselectAll();
            return;
        }
        if (buildManager.HasMoney)
        {
            render.material.color = hoverColor;
        }
        else
        {
            render.material.color = forbiddanceColor;
        }
    }

    private void OnMouseExitCheck()
    {
        render.material.color = defaultColor;
    }

    public void BuildTurretOn(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("No enough money to build");
            return;
        }

            
        Blueprint = new TurretBlueprint(blueprint);

        if (Blueprint.isUpgraded)
        {
            Blueprint.isUpgraded = false;
        }

        _turret = Instantiate(Blueprint.prefab, BuildPosition, Quaternion.identity);
        Turret t = _turret.GetComponent<Turret>();
        t.node = this;
        t.turretUI.SetValues(Blueprint);
        turretInfo = t.turretInfo;

        PlayerStats.Money -= Blueprint.cost;

        GameObject buildEffect = Instantiate(buildManager.BuildEffectPrefab, BuildPosition, Quaternion.identity);

        Destroy(buildEffect, 2f);
    }

    public void OnMouseEnter()
    {
        if (buildManager.CanBuild && buildManager.HasMoney)
        {
            ShowTurretRadius();
        }
    }
    private void OnMouseExit()
    {
        AttackRadius.Node = null;

        if (_turret != null)
        {
            if (turretInfo.activeSelf)
            {
                turretInfo.SetActive(false);
            }
        }
    }

    public void UpgradeTurretOn()
    {
        if (PlayerStats.Money < Blueprint.upgradeCost)
        {
            Debug.Log("No enough money to build");
            return;
        }

        Blueprint.isUpgraded = true;
        Destroy(_turret);
        _turret = Instantiate(Blueprint.upgradePrefab, BuildPosition, Quaternion.identity);
        Turret t = _turret.GetComponent<Turret>();
        t.node = this;
        t.turretUI.SetValues(Blueprint);
        turretInfo = t.turretInfo;

        PlayerStats.Money -= Blueprint.upgradeCost;

        GameObject buildEffect = Instantiate(buildManager.BuildEffectPrefab, BuildPosition, Quaternion.identity);
        Destroy(buildEffect, 2f);
    }

    public void SellTurretOn()
    {
        PlayerStats.Money += Blueprint.SellValue;

        Destroy(_turret);
        Blueprint = null;

        GameObject sellEffect = Instantiate(buildManager.SellEffectPrefab, BuildPosition, Quaternion.identity);
        Destroy(sellEffect, 2f);

    }
}
