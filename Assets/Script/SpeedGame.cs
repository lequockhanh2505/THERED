using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedGame : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    float speedGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = speedGame;
    }
}
