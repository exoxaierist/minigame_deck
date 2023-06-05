using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{
    public Sprite thumbnail;
    public string mapName;

    public void SetMap()
    {
        MenuHandler.main.SetMap(mapName);
        GameObject.Find("THUMBNAIL").GetComponent<Image>().sprite = thumbnail;
    }
}
