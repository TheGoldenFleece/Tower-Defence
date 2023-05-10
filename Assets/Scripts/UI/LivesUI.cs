using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LivesUI : MonoBehaviour
{
    [SerializeField] GameObject HPUnitPrefab;
    List<GameObject> Health;

    private void Awake()
    {
        Health = new List<GameObject>();
    }
    private void Update()
    {
        UpdateLivesUI();
    }

    void UpdateLivesUI()
    {
        if (Health.Count == PlayerStats.Lives) return;

        while(Health.Count < PlayerStats.Lives) {
            GameObject HPUnit = Instantiate(HPUnitPrefab, gameObject.transform);
            Health.Add(HPUnit);
        }

        while (Health.Count > PlayerStats.Lives)
        {
            GameObject HPUnit = Health[Health.Count - 1];
            Destroy(HPUnit);
            Health.Remove(HPUnit);
        }
    }
}
