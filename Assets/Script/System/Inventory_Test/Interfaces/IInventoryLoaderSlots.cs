using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryLoaderSlots {
    void LoadSlots(List<GameObject> slots, int numberSlot, GameObject preSlots, GameObject position);
}
