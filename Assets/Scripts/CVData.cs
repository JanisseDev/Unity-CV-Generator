using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CVData
{
    public string name;
    public string jobTitle;
    [Multiline] public string info;
    public List<ItemList> itemLists;
    public float spacing;

    [Serializable]
    public struct ItemList
    {
        public string title;
        public List<Item> items;
        public float spacing;

        [Serializable]
        public struct Item
        {
            public string primaryTitle;
            public string secondaryTitle;
            [Multiline] public string desc;
            [Multiline] public string leftPrimaryText;
            [Multiline] public string leftSecondaryText;
        }
    }
}
