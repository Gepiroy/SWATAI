using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public void SetProgress(float progress)
    {
        Vector3 scale = transform.localScale;
        scale.x = progress;
        transform.localScale = scale;
    }
}
