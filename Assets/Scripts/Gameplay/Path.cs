using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path:MonoBehaviour
{
    int range;
    int border;
    [SerializeField] GameObject PathPrefab;
    [SerializeField] GameObject startPointPrefab;
    [SerializeField] GameObject endPointPrefab;

    Vector3 OffsetY = new Vector3(0, 1.5f, 0); 

    static public List<PathUnit> PathList;

    GameObject[,] map;

    int iterationLimit;
    Coordinates pointer;
    Coordinates endPoint;

    public bool IsGenerated { private set; get; }

    public void GeneratePath(GameObject[,] map)
    {
        this.map = map;

        GeneratePathway();
        Debug.Log("PathList Is ready");
    }

    void GeneratePathway()
    {
        IsGenerated = true;
        PathList = new List<PathUnit>();

        range = map.GetLength(0);
        border = range - 1;

        int start = Random.Range(0, range - 1);
        int end = Random.Range(0, range - 1);

        pointer = new Coordinates(0, start);
        endPoint = new Coordinates(range - 1, end);

        AddPathUnit(pointer, OffsetY, startPointPrefab);

        iterationLimit = range * range;
        int i = 0;
        while (!pointer.IsEquil(new Coordinates(endPoint.X - 1, endPoint.Z)))
        {
            if (i > iterationLimit)
            {
                IsGenerated = false;
                break;
            }
            CreateOnePathCell();

            i++;
        }

        if (!IsGenerated)
        {
            GeneratePathway();
        }
        else
        {
            AddPathUnit(endPoint, OffsetY, endPointPrefab);
            BuildPath();
        }
    }

    void BuildPath()
    {
        foreach (PathUnit path in PathList)
        {
            Destroy(map[path.Coordinates.X, path.Coordinates.Z]);
            GameObject pathObject = Instantiate(path.Prefab, path.Position, Quaternion.identity);
            pathObject.transform.SetParent(gameObject.transform);
        }
    }

    bool IsPathPart(Coordinates coordinates)
    {
        foreach (PathUnit path in PathList)
        {
            if (path.Coordinates.IsEquil(coordinates))
            {
                return true;
            }
        }

        return false;
    }

    bool IsPathPart(Coordinates[] coordinates)
    {
        foreach (Coordinates coord in coordinates)
        {
            if (IsPathPart(coord))
            {
                return true;
            }
        }

        return false;
    }

    bool CanMoveUp()
    {
        Coordinates newCoord = new Coordinates(pointer.X, pointer.Z + 1);

        bool check = !IsPathPart(new Coordinates[] {
            new Coordinates(newCoord.X, newCoord.Z),
            new Coordinates(newCoord.X, newCoord.Z + 1),
            new Coordinates(newCoord.X - 1, newCoord.Z)});

        if (newCoord.Z < border
            &&
            newCoord.X > 0
            &&
            newCoord.X + 1 != endPoint.X
            &&
            newCoord.X + 2 != endPoint.X
            &&
            check
            ||
            newCoord.X + 1 == endPoint.X
            &&
            newCoord.Z <= endPoint.Z)
        {
            return true;
        }

        return false;
    }

    bool CanMoveRight()
    {
        Coordinates newCoord = new Coordinates(pointer.X + 1, pointer.Z);

        bool check = !IsPathPart(new Coordinates[] {
            new Coordinates(newCoord.X, newCoord.Z),
            new Coordinates(newCoord.X + 1, newCoord.Z) });

        if (newCoord.X < border
            &&
            check)
        {
            return true;
        }

        return false;
    }

    bool CanMoveDown()
    {
        Coordinates newCoord = new Coordinates(pointer.X, pointer.Z - 1);

        bool check = !IsPathPart(new Coordinates[] {
            new Coordinates(newCoord.X, newCoord.Z),
            new Coordinates(newCoord.X, newCoord.Z - 1),
            new Coordinates(newCoord.X - 1, newCoord.Z) });

        if (newCoord.Z > 0
            &&
            newCoord.X > 0
            &&
            newCoord.X + 1 != endPoint.X
            &&
            newCoord.X + 1 != endPoint.X
            &&
            check
            ||
            newCoord.X + 1 == endPoint.X
            &&
            newCoord.Z >= endPoint.Z)
        {
            return true;
        }

        return false;
    }

    void CreateOnePathCell()
    {
        // 0 - Up, 1 - Right, 2 - Down
        int move = Random.Range(0, 3);
        switch (move)
        {
            case 0:
                {
                    if (CanMoveUp())
                    {
                        pointer.Z++;
                        break;
                    }

                    return;
                }
            case 1:
                {
                    if (CanMoveRight())
                    {
                        pointer.X++;
                        break;
                    }

                    return;
                }
            case 2:
                {
                    if (CanMoveDown())
                    {
                        pointer.Z--;
                        break;
                    }

                    return;
                }
        }

        AddPathUnit(pointer, PathPrefab);
    }

    void AddPathUnit(Coordinates position, GameObject prefab)
    {
        GameObject node = map[(int)position.X, (int)position.Z];
        PathList.Add(new PathUnit(prefab, node.transform.position, position));
    }

    void AddPathUnit(Coordinates position, Vector3 offset, GameObject prefab)
    {
        GameObject node = map[(int)position.X, (int)position.Z];
        PathList.Add(new PathUnit(prefab, node.transform.position + offset, position));
    }

    private void OnMouseDown()
    {
        Shop.DeselectAll();
    }

    public struct Coordinates
    {
        public int X { get; set; }
        public int Z { get; set; }

        public Coordinates(int x, int z)
        {
            X = x;
            Z = z;
        }
        public void Set(int x, int z)
        {
            X = x;
            Z = z;
        }

        public bool IsEquil(Coordinates coordinates)
        {
            return (X == coordinates.X && Z == coordinates.Z);
        }

        public bool IsEquil(int X, int Z)
        {
            return (this.X == X && this.Z == Z);
        }
    }

    public struct PathUnit
    {
        public GameObject Prefab { get; private set; }
        public Coordinates Coordinates { get; private set; }
        public Vector3 Position { get; private set; }
        public PathUnit(GameObject GameObjectPrefab, Vector3 Position, Coordinates Coordinates)
        {
            this.Prefab = GameObjectPrefab;
            this.Coordinates = Coordinates;
            this.Position = Position;
        }
    }
}