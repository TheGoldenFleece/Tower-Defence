using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    Node _node;
    [SerializeField]
    GameObject upgrade;
    [SerializeField]
    Text upgradeCost;
    [SerializeField]
    Text sellCost;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void ShowNodeUI(Node node)
    {
        if (node.Blueprint.isUpgraded)
        {
            upgrade.SetActive(false);
        }
        else
        {
            upgrade.SetActive(true);
            upgradeCost.text = "$" + node.Blueprint.upgradeCost;
        }

        sellCost.text = "$" + node.Blueprint.SellValue;
        _node = node;
        transform.position = _node.BuildPosition;

        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void HideNodeUI()
    {
        gameObject.SetActive(false);
        _node = null;
    }

    public void Upgrade()
    {
        if (_node == null) return;
        _node.UpgradeTurretOn();

        HideNodeUI();
    }

    public void Sell()
    {
        _node.SellTurretOn();
        HideNodeUI();
    }
}
