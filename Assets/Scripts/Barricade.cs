using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class Barricade : MonoBehaviour
{
    [Inject] HidingPoint _hidingPoint;
    void Start()
    {
        //Debug.Log("scale = " + transform.localScale);
        /*Debug.Log("lossy = "+transform.lossyScale+"; localscale = "+ transform.localScale);
        Debug.Log(transform.rotation);
        Debug.Log(transform.localScale);
        Debug.Log(transform.localRotation);*/
        float spacing = 1.04f;

        float xLength = transform.localScale.x;
        float yLength = transform.localScale.y;
        int pointsOnXSides = (int)(transform.localScale.y/spacing);
        int pointsOnYSides = (int)(transform.localScale.x/spacing);
        Vector3 upStep = transform.up*((yLength+spacing) * 0.5f +0.1f);
        Vector3 sideStep = transform.right*((xLength+spacing) * 0.5f +0.1f);

        Debug.Log("pointsOnXSides = "+pointsOnXSides+ "; pointsOnYSides = " + pointsOnYSides);

        for (int i=0;i<pointsOnYSides;i++)
        {
            SpawnTwoHidingPoints(upStep, transform.right, i, pointsOnYSides, xLength);
            //var localSpacing = xLength / pointsOnYSides;
            //Instantiate(_hidingPoint, transform.position + upStep + transform.right * (i - (pointsOnYSides / 2.0f)+0.5f)* localSpacing, Quaternion.identity);
            //Instantiate(_hidingPoint, transform.position - upStep + transform.right * (i - (pointsOnYSides / 2.0f))*spacing, Quaternion.identity);
        }
        for (int i = 0; i < pointsOnXSides; i++)
        {
            SpawnTwoHidingPoints(sideStep, transform.up, i, pointsOnXSides, yLength);
            //Instantiate(_hidingPoint, transform.position + sideStep + transform.up * (i - (pointsOnXSides / 2.0f)) * spacing, Quaternion.identity);
            //Instantiate(_hidingPoint, transform.position - sideStep + transform.up * (i - (pointsOnXSides / 2.0f)) * spacing, Quaternion.identity);
        }

        //Instantiate(_hidingPoint, transform.position+transform.up*2, Quaternion.identity);
    }

    void SpawnTwoHidingPoints(Vector3 step, Vector3 side, int i, int pointsOnSide, float sideLength)
    {
        var localSpacing = sideLength / pointsOnSide;
        SpawnHidingPoint(step, side, i, pointsOnSide, localSpacing);
        SpawnHidingPoint(-step, side, i, pointsOnSide, localSpacing);
    }
    void SpawnHidingPoint(Vector3 step, Vector3 side, int i, int pointsOnSide, float localSpacing)
    {
        Instantiate(_hidingPoint, transform.position + step + side * (i - (pointsOnSide / 2.0f) + 0.5f) * localSpacing, Quaternion.identity);
    }


    
    void Update()
    {
        
    }
}
