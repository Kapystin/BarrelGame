using BarrelGame.Scripts.Character;
using UnityEngine;

namespace BarrelGame.Scripts.ScriptableObjectData
{
    [CreateAssetMenu(menuName = "BarrelGameData/CharacterData", fileName = "CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public CharacterManager characterManagerPrefab;
    }
}
