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
        [Header("Load Level")] 
        public LevelLayoutData LevelData;
        [Header("Save Level")]
        public string LevelName;
        public List<GameObject> EnemyList;

        [ContextMenu("Load Layout")]
        private void LoadEnemyLayoutData()
        {
            EnemyList.Clear();
            for (int i = 0; i < LevelData.EnemyData.Length; i++)
            {
                var enemy = Instantiate(LevelData.EnemyData[i].Enemy, LevelData.EnemyData[i].SpawnPosition,
                    Quaternion.identity, this.transform);
                EnemyList.Add(enemy);
            }
        }
        
        [ContextMenu("Save Layout")]
        private void SaveEnemyLayoutToData()
        {
            var newData = ScriptableObject.CreateInstance<LevelLayoutData>();
            EnemyList.Clear();
            
            //Gather enemy prefabs under this game object
            for (int i = 0; i < transform.childCount; i++)
            {
                EnemyList.Add(transform.GetChild(i).gameObject);
            }
            
            
            var amount = EnemyList.Count;

            newData.EnemyData = new EnemyData[amount];
            
            for (int i = 0; i < amount; i++)
            {
                newData.EnemyData[i] = new EnemyData();
                //Assign original prefab in Asset
                newData.EnemyData[i].Enemy = PrefabUtility.GetCorrespondingObjectFromOriginalSource(EnemyList[i]);
                newData.EnemyData[i].SpawnPosition = EnemyList[i].transform.position;
            }
            AssetDatabase.CreateAsset(newData,$"Assets/_Data/EnemyLayout/{LevelName}.asset");
        }
    }
}
#endif
