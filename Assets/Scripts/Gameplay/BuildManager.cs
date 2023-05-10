using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    static public BuildManager instance;
    public GameObject BuildEffectPrefab;
    public GameObject SellEffectPrefab;
    [SerializeField] NodeUI nodeUI;
    private TurretBlueprint turretToBuild;
    public TurretBlueprint TurretToBuild
    {
        get => turretToBuild;
        set
        {
            turretToBuild = value;
        }
    }

    private Node _node;
    public Node Node
    {
        get => _node;
        set
        {
            _node = value;
        }
    }

    public bool CanBuild { get => TurretToBuild != null; }
    public bool HasMoney { get => PlayerStats.Money >= TurretToBuild.cost;}

    public AttackRadius attackRadius;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    private void Start()
    {
        
    }
    public void SelectNode(Node node)
    {
        Shop.DeselectAll();

        if (Node == node)
        {
            DeselectNode();
            return;
        }
        nodeUI.ShowNodeUI(node);
        Node = node;
        TurretToBuild = null;
    }
    public void DeselectNode()
    {
        Node = null;
        nodeUI.HideNodeUI();
    }

}
