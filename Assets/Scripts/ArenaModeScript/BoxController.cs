using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public class BoxController : MonoBehaviour
    {
        public ArenaItem[] items;

        public GameObject Mine, Enemy, Shield, Freeze;
        // Use this for initialization
        void Start()
        {
            foreach (var item in items)
            {
                switch (item.type)
                {
                    case ArenaItemType.Mine:
                        item.action = ((character) =>
                        {
                            //instasiate mine
                            //give damage
                            character.item = false;
                        });
                        break;
                    case ArenaItemType.Freeze:
                        item.action = ((character) =>
                        {

                            //instasiate object
                            //change Direction
                            //give Damage
                            character.item = false;

                        });
                        break;
                    case ArenaItemType.Enemy:
                        item.action = ((character) =>
                        {

                            //instasiate object
                            //change Direction
                            //give Damage
                            character.item = false;

                        });
                        break;
                    case ArenaItemType.Shield:
                        item.action = ((character) =>
                        {
                            //instasiate object
                            //change Character hit Delegate
                            character.item = false;

                        });
                        break;
                    default:
                        break;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    [System.Serializable]
    public class ArenaItem
    {
        public ArenaItemType type;
        public Sprite s;
        public ItemAction action;
    }
    public enum ArenaItemType
    {
        Mine,Freeze,Enemy,Shield
    }
    public delegate void ItemAction(CharacterHolderArenaMode character);
}

