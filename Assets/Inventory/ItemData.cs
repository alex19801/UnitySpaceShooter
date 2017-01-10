using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour {

    public static ItemData _ItemData; //паттерн Singelton
    public List<Item> Items = new List<Item>(); //списк в котором хранятся предметы
    // public Dictionary<int, Item> Items2 = new Dictionary<int, Item>(); //списк в котором хранятся предметы

    void Awake()
    {
        _ItemData = this;
    }

    
    //генерация предмета
    //public Item ItemGen(int win_id)
    //{
    //    Item item = new Item();
    //    item.Name = Items[win_id].Name;
    //    item.Textura = Items[win_id].Textura;
    //    return item;
    //}
}
