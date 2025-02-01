using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderController : MonoBehaviour
{
    //public Renderer[] renderers;

    public Material material;
    //private MaterialPropertyBlock propertyBlock;

    private const string TargetPosition = "_TargetPos";

    private void Awake()
    {
        //propertyBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        //propertyBlock.SetVector(TargetPosition, transform.position);


        //.SetPropertyBlock()

        material.SetVector(TargetPosition, transform.position);
    }
}
