using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveTempo : MonoBehaviour
{
    public Transform safe;
    public string safeName;

    public void SetTempo(Transform somethingToSave)
    {
        Debug.Log("Item salvo: "+somethingToSave.name);
        safe = somethingToSave;
        safeName = safe.name;
    }

    public Transform GetTempo()
    {
        return safe;
    }
}
