using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorActuator : MonoBehaviour
{

    [SerializeField] private GameObject gridActive;
    [SerializeField] private GameObject gridDeactive;

    public void Awake()
    {
        gridActive.SetActive(true);
        gridDeactive.SetActive(false);
    }

    public void SetActive()
    {
        gridActive.SetActive(true);
        gridDeactive.SetActive(false);
    }

    public void SetDeactive()
    {
        gridActive.SetActive(false);
        gridDeactive.SetActive(true);
    }

}
