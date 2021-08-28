using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
namespace Script.Menu
{
    public class MenuManager : MonoBehaviour
    {
        // ------------ Attributs ------------
        
        public static MenuManager Instance;
        
        // ------------ SerializeField ------------
        
        [SerializeField] private Menu[] menus;
        
        // ------------ Getter ------------
        
        private Menu GetMenu(string menuName)
        {
            int i;
            int l = menus.Length;
            for (i = 0; i < l && menus[i].menuName != menuName; i++)
            {}
            if (i == l)
                throw new Exception($"Le menu {menuName} n'existe pas");
            return menus[i];
        }
        
        // ------------ Constructor ------------
        
        private void Awake()
        {
            Instance = this;
        }
        
        // ------------ Open ------------
        
        public void OpenMenu(string menuName)
        {
            int l = menus.Length;
            for (int i = 0; i < l; i++)
            {
                if (menus[i].menuName == menuName)
                {
                    menus[i].Open();
                }
                else if (menus[i].open)
                {
                    CloseMenu(menus[i]);
                }
            }
        }
        public void OpenMenu(Menu menu)
        {
            int l = menus.Length;
            for (int i = 0; i < l; i++)
            {
                if (menus[i].open)
                {
                    CloseMenu(menus[i]);
                }
            }
        
            menu.Open();
        }
        
        public void ForceOpen(Menu menu)
        {
            menu.Open();
        }
        
        public void ForceOpenMenu(string menuName)
        {
            GetMenu(menuName).Open();
        }
        
        // ------------ Close ------------
        
        public void CloseAllMenu()
        {
            foreach (Menu menu in menus)
            {
                menu.Close();
            }
        }
    
        public void CloseMenu(Menu menu)
        {
            menu.Close();
        }
        public void CloseMenu(string menuName)
        {
            GetMenu(menuName).Close();
        }
    }
}
