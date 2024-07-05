using UnityEngine;

namespace BarrelGame.Scripts.ScriptableObjectData
{
    [CreateAssetMenu(menuName = "BarrelGameData/LevelData", fileName = "LevelData")]
    public class LevelData : ScriptableObject
    {
        public LevelManager[] LevelManagers;

        public LevelManager GetRandomLevelManager()
        {
            return LevelManagers[Random.Range(0, LevelManagers.Length)];
        }
    }
}
