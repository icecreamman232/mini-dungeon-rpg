using UnityEngine;

namespace JustGame.Scripts.LevelElements
{
    public class Item : MonoBehaviour
    {
        protected virtual void OnPicking()
        {
            Destroy(this.gameObject);
        }
    }

}
