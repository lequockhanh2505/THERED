using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlotSelect
{
    void SlotSelect(Slot slot);
    void UpdateUI(ItemStats itemStats);
    void ClearUI();
    Color SetAlpha(float alpha);
}
