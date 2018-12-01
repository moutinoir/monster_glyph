using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridBlock : MonoBehaviour {

    [Header("0:Up Left, 1:Up Right, 2:Low Left, 3:Low Right")]
    public int quadrant;

    GridParams myParentGrid;

    // EDITOR ONLY
    void AttachToParentGrid()
    {
        myParentGrid = GetComponentInParent<GridParams>();
    }

    // EDITOR ONLY
    void ForcePlaceCubeAtGridPosition()
    {

        Vector3 prevPos = transform.localPosition;
        Vector3 prevScale = transform.localScale;

        transform.localScale = new Vector3(myParentGrid.cubeUnitFilling, myParentGrid.cubeUnitFilling, prevScale.z);

        switch (quadrant)
        {
            case 0:
                transform.localPosition = new Vector3(-myParentGrid.gridStep * .5f, myParentGrid.gridStep * .5f, prevPos.z);
                break;

            case 1:
                transform.localPosition = new Vector3(myParentGrid.gridStep * .5f, myParentGrid.gridStep * .5f, prevPos.z);
                break;

            case 2:
                transform.localPosition = new Vector3(-myParentGrid.gridStep * .5f, -myParentGrid.gridStep * .5f, prevPos.z);
                break;

            case 3:
            default:
                transform.localPosition = new Vector3(myParentGrid.gridStep * .5f, -myParentGrid.gridStep * .5f, prevPos.z);
                break;

        }

    }

    void Update () {

        if (Application.isEditor)
        {
            if (myParentGrid == null)
            {
                AttachToParentGrid();
            } else
            {
                ForcePlaceCubeAtGridPosition();
            }
            
        }

    }
}
