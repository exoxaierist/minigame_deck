using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPooler : MonoBehaviour
{
    private GameObject atkObj;
    private Transform atkObjHolder;

    private void Awake()
    {
        Global.atkPooler = this;
    }

    private void Start()
    {
        atkObj = Global.assets.atkObject;
        atkObjHolder = new GameObject().transform;
        atkObjHolder.SetParent(transform);
        atkObjHolder.name = "AttackObjectHolder";
    }

    public AttackInstance Get()
    {
        AttackInstance instance = null;
        for (int i = 0; i < atkObjHolder.childCount; i++)
        {
            if (atkObjHolder.GetChild(i).TryGetComponent(out instance)) break;
        }
        instance = instance == null ? CreateNew() : instance;
        instance.gameObject.SetActive(true);
        return instance;
    }

    private AttackInstance CreateNew()
    {
        return Instantiate(atkObj,atkObjHolder).GetComponent<AttackInstance>();
    }

    public void Deactivate(AttackInstance instance)
    {
        instance.transform.SetParent(atkObjHolder);
        instance.gameObject.SetActive(false);
    }
}
