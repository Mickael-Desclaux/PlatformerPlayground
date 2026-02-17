using UnityEngine;

namespace Libraries.UTools
{
    public static class TimeScaler
    {
        #region Statements

        public static float Scale { get; set; } = 1f;
        
        public static ScaledDuration Duration => new(1f);

        #endregion

        #region Methods

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            Scale = Time.timeScale;
        }

        public static int GetDuration(int duration)
        {
            return Scale > 0 ? (int)(duration / Scale) : int.MaxValue;
        }

        #endregion
    }
}
