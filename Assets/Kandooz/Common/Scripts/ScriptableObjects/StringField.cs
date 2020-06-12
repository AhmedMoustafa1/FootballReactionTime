using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kandooz
{
    [CreateAssetMenu(menuName = "Kandooz/String Field")]

    public class StringField : ScriptableObject
    {
        public event Action onChange;
        [SerializeField]
        private String value;
        public String Value
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

