using System.Linq;

public static class Extensions
{
    public static bool In<T>(this T val, System.Collections.Generic.List<int> tipoChavesValues, params T[] values) where T : struct
    {
        return values.Contains(val);
    }

    public static int getInt(this object chave)
    {
        return (int) chave;
    }
}