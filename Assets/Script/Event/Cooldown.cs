using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown
{
    public bool isCoolingDown;
    public float currentTime;
    public float maxCooldown;

    public Cooldown(float maxCooldown)
    {
        this.isCoolingDown = false;
        this.currentTime = 0f;
        this.maxCooldown = maxCooldown;
    }

    public void UpdateCooldown()
    {
        if (isCoolingDown)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= maxCooldown)
            {
                isCoolingDown = false;
                currentTime = 0f;
            }
        }
    }

    public void StartCooldown()
    {
        isCoolingDown = true;
    }
}

