using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScriptableItem : MonoBehaviour
{
    public int id;
    public string title;
    //public Sprite icon;
    public string anim_name;
    public GameObject equipPrefab;

    public bool enableState = false;

    public Text textComp;
    
    private GameObject togglePart;
    public UnityEvent onUse;
    private void Awake()
    {
        if (onUse==null)
            onUse = new UnityEvent();

        switch(id)
        {
            case 0:
            Debug.LogError("ID 0 mustn't be at items");
            break;
            case 1: //flashlight
            case 2: //thermometer
            case 3: //edf
            togglePart = transform.GetChild(0).gameObject;
            onUse.AddListener(ToggleUse);
            break;
            default:
            Debug.LogError("New item ID was mentioned, but in doesn't exist in SWITCH");
            break;
        }

        togglePart.SetActive(enableState);

    }

    public void SetLayer(string layerName)
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
        togglePart.layer = LayerMask.NameToLayer(layerName);
    }

    public void ToggleUse()
    {
        enableState = !enableState;
        togglePart.SetActive(enableState);
         //При выкидывании оно все равно сбросится, потому что предмет заново создается, но это можно сохранять будет потом, чтобы после создания предмета этот параметр вкидывать
    }

    

}
