using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;
                                                                                    
    [Header("Turret Cost UI")]
    public Text standardTurretCost;
    public Text missileLauncherCost;
    public Text laserBeamerCost;

    static GameObject[] TurretsToBeSelected;
    [SerializeField] GameObject standardTurretSelected;
    [SerializeField] GameObject missileLauncherSelected;
    [SerializeField] GameObject laserBeamerSelected;

    GameObject currentSelected;

    private void Awake()
    {
        standardTurretCost.text = standardTurret.cost.ToString();
        missileLauncherCost.text = missileLauncher.cost.ToString();
        laserBeamerCost.text = laserBeamer.cost.ToString();

        TurretsToBeSelected = new GameObject[]
        {
            standardTurretSelected,
            missileLauncherSelected,
            laserBeamerSelected
        };

        DeselectAll();
    }
    public void SelectStandardTurretItem()
    {
        BuildManager.instance.TurretToBuild = standardTurret;
        SelectTurret(standardTurretSelected);
    }
    public void SelectMissileLauncherItem()
    {
        BuildManager.instance.TurretToBuild = missileLauncher;
        SelectTurret(missileLauncherSelected);
    }
    public void SelectLaserBeamerItem()
    {
        BuildManager.instance.TurretToBuild = laserBeamer;
        SelectTurret(laserBeamerSelected);
    }

    void SelectTurret(GameObject select)
    {
        currentSelected = select;
        select.SetActive(true);

        for (int i = 0; i <  TurretsToBeSelected.Length; i++)
        {
            if (currentSelected != TurretsToBeSelected[i])
            {
                TurretsToBeSelected[i].SetActive(false);
            }
        }
    }

    static public void DeselectAll()
    { 
        for (int i = 0; i < TurretsToBeSelected.Length; i++)
        {
            TurretsToBeSelected[i].SetActive(false);
        }
    }
}
