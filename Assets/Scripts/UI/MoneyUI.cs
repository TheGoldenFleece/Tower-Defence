using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    private Text money;

    private void Update()
    {
        UpdateMoneyUI();
    }

    void UpdateMoneyUI()
    {
        money.text = '$' + PlayerStats.Money.ToString();
    }

}
