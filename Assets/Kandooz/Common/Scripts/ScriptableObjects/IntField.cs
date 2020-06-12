using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz
{
    [CreateAssetMenu(menuName = "Kandooz/Int Field")]

    public class IntField : ScriptableObject
    {
        [SerializeField]
        private int value;
        //[SerializeField]
        private float lastTime=0;
        public event Action<int> OnValueChanged;

        public virtual int Value
        {
            get
            {
                return value;
            }

            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    if (OnValueChanged != null)
                        OnValueChanged(value);
                }
            }
        }
        public void Add(int value){
            this.Value+=value;            
        }
        public static implicit operator int(IntField b)
        {
            return b.Value;
        }
    }
}
