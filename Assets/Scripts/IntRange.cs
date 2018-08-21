using System;

// Serializable so it will show up in the inspector.
[Serializable]
public class NumberRange
{
    public float m_Min;       // The minimum value in this range.
    public float m_Max;       // The maximum value in this range.


    // Constructor to set the values.
    public NumberRange(float min, float max)
    {
        m_Min = min;
        m_Max = max;
    }


    // Get a random value from the range.
    public int RandomInt
    {
        get { return(int)( UnityEngine.Random.Range(m_Min, m_Max+1)); }
    }

    public float RandomFloat
    {
        get { return UnityEngine.Random.Range(m_Min, m_Max ); }
    }
}