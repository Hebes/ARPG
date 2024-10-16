public class Incrementor
{
    public static int GetNextId() => ++_i;

    private static int _i;
}