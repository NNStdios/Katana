using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class ItemManager : MonoBehaviour
    {
        private int _coins;
        public event Action OnCoinsChanged;

        public int Coins
        {
            get => _coins;
            set
            {
                if (Mathf.Approximately(_coins, value)) return;
                _coins = value;
                PlayerPrefs.SetInt("Coins", _coins);
                OnCoinsChanged?.Invoke();
            }
        }

        private Item _selectedItem;
        public event Action OnItemChanged;

        public Item SelectedItem
        {
            get => _selectedItem;
            private set
            {
                if (_selectedItem?.Name == value?.Name) return;
                _selectedItem = value;
                PlayerPrefs.SetString("SelectedItem", _selectedItem?.Name);
                OnItemChanged?.Invoke();
            }
        }

        public List<Item> Items;

        private void Awake()
        {
            Coins = PlayerPrefs.GetInt("Coins", 0);
            var itemName = PlayerPrefs.GetString("SelectedItem");
            var item = Items.FirstOrDefault(x => x.Name == itemName);
            SelectedItem = item ?? Items[0];
            PlayerPrefs.SetString("SelectedItem", itemName);
        }

        public void SelectItem(Item item)
        {
            if (!item.Unlocked)
            {
                if (item.Price > Coins) return;
                item.Unlocked =  true;
                Coins         -= item.Price;
            }

            SelectedItem = item;
        }
    }
}