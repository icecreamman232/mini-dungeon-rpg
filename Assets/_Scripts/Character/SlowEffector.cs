
namespace JustGame.Scripts.Combat
{
    public interface SlowEffector
    {
        /// <summary>
        /// Slow down current character's speed at percentage
        /// </summary>
        /// <param name="slowPercent"></param>
        /// <param name="duration"></param>
        public void TriggerSlow(float slowPercent, float duration);
    } 
}

