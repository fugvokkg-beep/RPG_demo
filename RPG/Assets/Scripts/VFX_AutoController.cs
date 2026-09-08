using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    [SerializeField] private bool autoDestory = true;
    [SerializeField] private float destoryDelay = 1;


    private void Start()
    {
        if(autoDestory)
            Destroy(gameObject,destoryDelay);
    }
}
