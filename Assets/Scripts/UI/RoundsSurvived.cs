using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour
{
    [SerializeField] Text roundsSurvived;
    IEnumerator display;

    private void OnEnable()
    {
        if (display != null) StopCoroutine(display);
        display = Display();
        StartCoroutine(display);
    }

    IEnumerator Display()
    {
        yield return new WaitForSeconds(.2f);
        int rounds = 0;
        while (rounds <= PlayerStats.Rounds)
        {
            roundsSurvived.text = rounds.ToString();
            yield return new WaitForSeconds(.1f);
            rounds++;
        }
    }
}
