public static class Pitwall
{
    public static float ConvertStatToPerformance(float stat, float minPerformance, float maxPerformance)
    {
        return ((stat / 100) * (maxPerformance - minPerformance)) + minPerformance;
    }
}
