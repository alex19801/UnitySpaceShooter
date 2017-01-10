using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{

    const int INVENTORY_WINDOW_ID = 0; //id окна инвентаря
    const int INVENTORY_TEXTURE_ID = 1; //id окна с иконкой
    const int INVENTORY_SHIP_ID = 2; //id окна с иконкой
    public float ButtonWidth = 40; //высота ячейки
    public float ButtonHeight = 40; //ширина ячейки
    public bool visible;

    int invRows = 6; //количество колонок
    int invColumns = 4; //количество столбцов
    Rect inventoryWindowRect = new Rect(10, 10, 170, 265); //область окна
    Rect inventoryShip = new Rect(10, 300, 170, 70); //область окна
    // Rect inventoryBoxRect = new Rect(); //область окна с изображением иконки
    public bool isDraggable; //перемещение предмета
    Item selectItem; //вспомогательная переменная куда заносим предмет инвентаря
    Texture2D dragTexture; //текстура которая отображается при перетягивании предмета в инвентаре

    Dictionary<int, Item> InventoryPlayer = new Dictionary<int, Item>(); //словарь содержащий предметы инвентаря
    Dictionary<int, Item> ShippedItems = new Dictionary<int, Item>(); //словарь содержащий предметы инвентаря

    void Start()
    {
        List<Item> items = GetComponent<ItemData>().Items;
        //добавляем предметы в инвентарь
        for (int i = 0; i < items.Count; i++)
        {
            InventoryPlayer.Add(i, items[i]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (visible)
        {
            GUI.Window(INVENTORY_WINDOW_ID, inventoryWindowRect, firstInventory, "INVENTORY"); //создаем окно
            GUI.Window(INVENTORY_SHIP_ID, inventoryShip, insert2, "SHIPP"); //создаем окно
            if (isDraggable)
            {
                GUI.Window(INVENTORY_TEXTURE_ID, new Rect(Event.current.mousePosition.x + 1, Event.current.mousePosition.y + 1, 40, 40), insert, "", "box");
            }
        }
    }

    //окно с изображением иконки
    void insert(int id)
    {
        GUI.BringWindowToFront(INVENTORY_TEXTURE_ID);//выводим на передний план окно с иконкой
        GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 40, 40), dragTexture);//рисуем текстуру иконки
    }

    void insert2(int id)
    {
        for (int y = 0; y < 1; y++)
        {
            for (int x = 0; x < invColumns; x++)
            {
                if (ShippedItems.ContainsKey(x + y * invColumns))//проверяем содеоржится ли ключ с данным значением
                {
                    if (GUI.Button(new Rect(5 + (x * ButtonHeight), 20 + (y * ButtonHeight), ButtonWidth, ButtonHeight), new GUIContent(ShippedItems[x + y * invColumns].Textura), "button"))
                    {
                        if (!isDraggable)
                        {
                            dragTexture = ShippedItems[x + y * invColumns].Textura;//присваиваем нашой текстуре которая должна отображаться при перетаскивании, текстуру предмета
                            isDraggable = true;//возможность перемещать предмет
                            selectItem = ShippedItems[x + y * invColumns];//присваиваем вспомогательной переменной наш предмет
                            ShippedItems.Remove(x + y * invColumns);//удаляем из словаря предмет
                        }
                    }
                }
                else
                {
                    if (isDraggable)
                    {
                        if (GUI.Button(new Rect(5 + (x * ButtonHeight), 20 + (y * ButtonHeight), ButtonWidth, ButtonHeight), "", "button"))
                        {
                            ShippedItems.Add(x + y * invColumns, selectItem);//добавляем предмет который перетаскиваем в словарь
                                                                                //обнуляем переменные
                            if (x == 0 && y == 0)
                            {
                                GameObject go = GameObject.FindGameObjectWithTag("GameController");
                                GameController pc = go.GetComponent<GameController>() as GameController;
                                pc.SweachWeapon(selectItem.Name);
                            }

                            isDraggable = false;
                            selectItem = null;

                        }
                    }
                    else
                    {
                        //делаем ячейки не выделяемыми
                        GUI.Label(new Rect(5 + (x * ButtonHeight), 20 + (y * ButtonHeight), ButtonWidth, ButtonHeight), "", "button");
                    }
                }
            }
        }
        GUI.DragWindow();
    }

    //окно с инвентарем
    void firstInventory(int id)
    {
        for (int y = 0; y < invRows; y++)
        {
            for (int x = 0; x < invColumns; x++)
            {
                if (InventoryPlayer.ContainsKey(x + y * invColumns))//проверяем содеоржится ли ключ с данным значением
                {
                    if (GUI.Button(new Rect(5 + (x * ButtonHeight), 20 + (y * ButtonHeight), ButtonWidth, ButtonHeight), new GUIContent(InventoryPlayer[x + y * invColumns].Textura), "buttwon"))
                    {
                        if (!isDraggable)
                        {
                            dragTexture = InventoryPlayer[x + y * invColumns].Textura;//присваиваем нашой текстуре которая должна отображаться при перетаскивании, текстуру предмета
                            isDraggable = true;//возможность перемещать предмет
                            selectItem = InventoryPlayer[x + y * invColumns];//присваиваем вспомогательной переменной наш предмет
                            InventoryPlayer.Remove(x + y * invColumns);//удаляем из словаря предмет
                        }
                    }
                }
                else
                {
                    if (isDraggable)
                    {
                        if (GUI.Button(new Rect(5 + (x * ButtonHeight), 20 + (y * ButtonHeight), ButtonWidth, ButtonHeight), "", "button"))
                        {
                            InventoryPlayer.Add(x + y * invColumns, selectItem);//добавляем предмет который перетаскиваем в словарь
                                                                                //обнуляем переменные
                            isDraggable = false;
                            selectItem = null;
                        }
                    }
                    else
                    {
                        //делаем ячейки не выделяемыми
                        GUI.Label(new Rect(5 + (x * ButtonHeight), 20 + (y * ButtonHeight), ButtonWidth, ButtonHeight), "", "button");
                    }
                }
            }
        }
        GUI.DragWindow();
    }
}