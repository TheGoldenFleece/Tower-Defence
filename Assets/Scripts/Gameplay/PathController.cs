using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    private void OnMouseDown()
    {
        Shop.DeselectAll();
        BuildManager.instance.TurretToBuild = null;
        AttackRadius.Node = null;
    }
}
