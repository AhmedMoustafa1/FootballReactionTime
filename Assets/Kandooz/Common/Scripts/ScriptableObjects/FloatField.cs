﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz
{
    [CreateAssetMenu(menuName = "Kandooz/Float Field")]
    public class FloatField : ScriptableObject
    {

        [SerializeField]
        private float value;
        public event System.Action<float> OnValueChanged;
        public float accuracy;
        public float Value
        {
            get
            {
                return value;
            }

            set
            {
                if (Mathf.Abs(value - this.value) > 0.0001)
                {
                    if (value < .001f && value > -.001f)
                    {
                        value = 0;
                    }
                    if(OnValueChanged!=null)
                        OnValueChanged(value);
                    this.value = value;

                }
            }
        }

        public static implicit operator float(FloatField b)
        {
            return b.Value;

        }
    }
}