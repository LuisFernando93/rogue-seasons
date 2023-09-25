using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueResponseEvents))]

//Este arquivo precisa estar em uma pasta chamada "Editor" e ter o mesmo nome do arquivo que ele vai editar, mas com um "Editor" no fim
//Objetivo disso é atualizar as opções de respostas que aparecem 

public class DialogueResponseEventsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueResponseEvents responseEvents = (DialogueResponseEvents)target;

        if (GUILayout.Button("Refresh"))
        {
            responseEvents.OnValidate();
        }
    }
}
