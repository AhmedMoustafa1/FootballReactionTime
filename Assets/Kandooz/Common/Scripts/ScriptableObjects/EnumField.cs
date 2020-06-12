using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kandooz
{
    [CreateAssetMenu(menuName = "Kandooz/Enum Field")]

    public class EnumField : ScriptableObject
    {
        public event Action onChange;
        [SerializeField]
        public Enum value;
        public Enum Value
        {
            get
            {

                return value;
            }

            set
            {

                this.value = value;
                if (onChange != null)
                {
                    onChange();
                }
            }
        }
    }
}