namespace _Maze.CodeBase.GamePlay.Pause
{
    public static class SeedGenerator
    {
        public static int GenerateSeed()
        {
            int seed = System.DateTime.Now.GetHashCode();
            return seed;
        }
    }
}