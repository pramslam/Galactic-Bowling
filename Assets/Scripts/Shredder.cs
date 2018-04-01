using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    // Destroys Pin when leaving pinSetter box collider
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.GetComponent<PinSetter>())
        {
            DestroyObject(gameObject);
        }
    }
}