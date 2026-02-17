namespace Libraries.UTools
{
    public struct ScaledDuration
    {
        #region Statements

        private readonly float _baseValue;

        public ScaledDuration(float value)
        {
            _baseValue = value;
        }

        #endregion

        #region Methods

        public static implicit operator int(ScaledDuration scaledDuration)
        {
            return TimeScaler.Scale > 0 ? (int)(scaledDuration._baseValue / TimeScaler.Scale) : int.MaxValue;
        }

        public static implicit operator float(ScaledDuration scaledDuration)
        {
            return TimeScaler.Scale > 0 ? scaledDuration._baseValue / TimeScaler.Scale : float.PositiveInfinity;
        }

        public static ScaledDuration operator *(float value, ScaledDuration scaledDuration)
        {
            return new ScaledDuration(value * scaledDuration._baseValue);
        }

        public static ScaledDuration operator *(int value, ScaledDuration scaledDuration)
        {
            return new ScaledDuration(value * scaledDuration._baseValue);
        }

        public static float operator /(float value, ScaledDuration scaledDuration)
        {
            return TimeScaler.Scale > 0 ? value * scaledDuration._baseValue / TimeScaler.Scale : float.PositiveInfinity;
        }

        public static int operator /(int value, ScaledDuration scaledDuration)
        {
            return TimeScaler.Scale > 0 ? (int)(value * scaledDuration._baseValue / TimeScaler.Scale) : int.MaxValue;
        }

        #endregion
    }
}