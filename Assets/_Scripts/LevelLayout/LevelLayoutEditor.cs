using JustGame.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace JustGame.Scripts.Levels
{
    /// <summary>
    /// This class is only used on editor to generate level data
    /// </summary>
    public class LevelLayoutEditor : MonoBehaviour
    {
        public string LevelName;
        public GameObject[] EnemyList;
        
        [ContextMenu("Save Layout")]
        private void SaveEnemyLayoutToData()
        {
            var newData = ScriptableObject.CreateInstance<LevelLayoutData>();
            var amount = EnemyList.Length;

            newData.EnemyData = new EnemyData[amount];
            
            for (int i = 0; i < amount; i++)
            {
                newData.EnemyData[i] = new EnemyData();
                newData.EnemyData[i].Enemy = PrefabUtility.GetCorrespondingObjectFromOriginalSource(EnemyList[i]);
                newData.EnemyData[i].SpawnPosition = EnemyList[i].transform.position;
            }
            AssetDatabase.CreateAsset(newData,$"Assets/_Data/EnemyLayout/{LevelName}.asset");
        }
    }
}

