using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTest : MonoBehaviour
{
    float start;
    private void OnEnable()
    {
        start = Time.time;
    }

    private void Update()
    {
        if (Time.time - start > 1f) {
            gameObject.SetActive(false);
        }
    }
}
