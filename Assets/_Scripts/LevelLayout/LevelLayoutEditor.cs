#if UNITY_EDITOR
using System.Collections.Generic;
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
        public List<GameObject> EnemyList;
        
        [ContextMenu("Save Layout")]
        private void SaveEnemyLayoutToData()
        {
            var newData = ScriptableObject.CreateInstance<LevelLayoutData>();
            EnemyList.Clear();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                EnemyList.Add(transform.GetChild(i).gameObject);
            }
            
            
            var amount = EnemyList.Count;

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
#endif
