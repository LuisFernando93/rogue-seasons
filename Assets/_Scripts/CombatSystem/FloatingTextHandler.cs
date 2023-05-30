using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextHandler : MonoBehaviour
{
    private void Start()
    {
        transform.localPosition += new Vector3(0, 0.5f, 0);
    }
    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
