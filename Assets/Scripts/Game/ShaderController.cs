using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderController : MonoBehaviour
{
  public Material[] materials;
  [Range(0, 1)] public float CurveX;
  [Range(0, 1)] public float CurveY;

  private void OnValidate()
  {
    foreach (Material material in materials)
    {
      material.SetFloat("CurveX", CurveX);
      material.SetFloat("CurveY", CurveY);
    }
  }
}
