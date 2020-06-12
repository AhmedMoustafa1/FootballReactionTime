using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz
{
    [CreateAssetMenu(menuName ="Kandooz/VectorField")]
    public class VectorField : ScriptableObject
    {
        public Vector3 value;



        public static implicit operator VectorField(Vector3 b)
        {
            var ff = new VectorField();
            ff.value = b;
            return ff;
        }

        public static implicit operator Vector3(VectorField b)
        {
            return b.value;

        }
    }
}