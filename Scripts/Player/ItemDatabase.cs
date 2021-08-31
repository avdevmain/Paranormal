using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class ItemDatabase : MonoBehaviour
    {
        public static List<GameObject> items;

        private void Start()
        {
            items = new List<GameObject>();
            Object[] itemsInGame = Resources.LoadAll("Items", typeof(GameObject)); 
            foreach (var item in itemsInGame)   
                items.Add((GameObject)item);
        }
        public void ScanProjectItems(List<GameObject> _items)
        {
            items.Clear();
            
            foreach(var item in _items)
            {
                items.Add(item);
            }
        }

        public static GameObject FindItem(int id)
        {
            foreach(var item in items)
            {
                if (item.GetComponent<ScriptableItem>().id == id)
                    return item;
            }

            return null;
        }

    }
