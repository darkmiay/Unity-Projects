﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject inventoryUI;

    InventorySlot[] slots;

    public Inventory inventory;

    // Use this for initialization
    void Start()
    {
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
    //    if (Input.GetButtonDown("Inventory"))
   //     {
  //          inventoryUI.SetActive(!inventoryUI.activeSelf);
   //     }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.lightItems.Count)
            {
                slots[i].AddItem(inventory.lightItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
