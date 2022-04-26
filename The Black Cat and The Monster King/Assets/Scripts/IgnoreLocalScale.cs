using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreLocalScale : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        transform.localScale = transform.parent.localScale;
    }
}
