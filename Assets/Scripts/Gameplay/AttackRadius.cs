using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadius : MonoBehaviour
{
    [SerializeField] GameObject sphere;
    public static Node Node;
    private void Awake()
    {
        Node = null;
    }

    private void Update()
    {
        if (Node == null)
        {
            Hide();
        }

    }

    public void Display(float radius, Node node)
    {
        Node = node;
        sphere.SetActive(true);
        sphere.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
        sphere.transform.position = node.transform.position;
    }

    public void Hide()
    {
        sphere.SetActive(false);
    }
}
