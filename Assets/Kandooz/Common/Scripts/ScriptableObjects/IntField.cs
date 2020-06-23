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


        public virtual int Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;


            }
        }


    }
}