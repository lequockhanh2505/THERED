using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlotManager
{
    void InitializeSlots();
    List<GameObject> GetAllSlots();
    List<Slot> GetAvailableSlots();

    void LoadSlot(IInventoryLoader _loader);
}
