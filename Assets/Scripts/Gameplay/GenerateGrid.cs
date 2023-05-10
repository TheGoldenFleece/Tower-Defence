using System.Collections;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    [SerializeField] GameObject NodePrefab;
    [SerializeField] Path path;

    int range = 20;
    float gridOffset = 4.5f;

    GameObject[,] map;

    private void Start()
    {
        BuildLevel();
    }

    void BuildLevel()
    {
        map = new GameObject[range, range];

        for (int x = 0; x < range; x++)
        {
            for (int z = 0; z < range; z++)
            {
                Vector3 pos = new Vector3(
                    x * gridOffset,
                    0,
                    z * gridOffset);
                GameObject node = Instantiate(NodePrefab, pos, Quaternion.identity);

                map[x, z] = node;
                node.transform.SetParent(transform);
            }
        }

        path.GeneratePath(map);

    }

}
