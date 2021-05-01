using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstracle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Game.Over = true;
    }
}
