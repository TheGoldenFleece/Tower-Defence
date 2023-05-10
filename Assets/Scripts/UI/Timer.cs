using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timer;
    [SerializeField] Animator animator;

    private void FixedUpdate()
    {
        DisplayTimer();
    }

    void DisplayTimer()
    {
        if (Spawner.Timer == 0)
        {
            animator.SetBool("Play", false);
        }
        else
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Timer"))
            {
                animator.SetBool("Play", true);
            }
        }

        timer.text = "PREPARE TO WAVE: " + string.Format(CultureInfo.InvariantCulture, "{0:00.00}", Spawner.Timer);
    }
}
