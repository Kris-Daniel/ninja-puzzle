using NinjaPuzzle.Code.Unity.Helpers.GenericDictionary;
using UnityEngine;

namespace NinjaPuzzle.Code.Gameplay
{
	public class TestDictionary : MonoBehaviour
    {
        [SerializeField] GenericDictionary<string, int> test;

        void Awake()
        {
	        print(test["A"]);
        }
    }
}