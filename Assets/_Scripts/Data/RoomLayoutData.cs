using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Room Layout")]
    public class RoomLayoutData : ScriptableObject
    {
        public LayoutType LayoutType;
        public GameObject RoomPrefab;
    }
}

