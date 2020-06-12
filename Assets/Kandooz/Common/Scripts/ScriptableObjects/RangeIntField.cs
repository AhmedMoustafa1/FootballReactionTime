using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz
{
    [CreateAssetMenu(menuName = "Kandooz/Range Int Field")]

    public class RangeIntField : IntField
    {
        [SerializeField]
        int min;
        [SerializeField]
        int max;
        public override int Value
        {
            get
            {
                return base.Value;
            }

            set
            {
                base.Value = Mathf.Min(Mathf.Max(max,value),min);
            }
        }
        public void Add(int value){
            this.Value+=value;            
        }
        public static implicit operator int(RangeIntField b)
        {
            return b.Value;
        }
    }
}
