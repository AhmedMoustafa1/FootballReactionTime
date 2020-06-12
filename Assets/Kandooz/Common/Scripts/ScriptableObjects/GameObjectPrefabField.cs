using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz
{
    [CreateAssetMenu(menuName = "Kandooz/GameObject Field")]
    public class GameObjectPrefabField : ScriptableObject
    {
        [SerializeField]
        private GameObject value;
        public event System.Action OnUseEvent;

        public GameObject Value
        {
            get
            {
                return value;
            }

            set
            {
                if (value != this.value)
                {
                    //if (OnValueChanged != null)
                    //    OnValueChanged(value);
                    this.value = value;

                }
            }
        }

        public static implicit operator GameObject(GameObjectPrefabField b)
        {
            return b.Value;
        }
    }
}