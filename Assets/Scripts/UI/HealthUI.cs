using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Image healthBar;
    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //LookAtCamera();
    }

    void LookAtCamera()
    {
        transform.LookAt(mainCamera.transform);
    }

    public void ChangeHealthUI(float leftHealth, float maxHealth)
    {
        healthBar.fillAmount = leftHealth/maxHealth;
    }
}
