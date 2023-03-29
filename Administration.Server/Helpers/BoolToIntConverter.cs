namespace Administration.Server.Helpers
{
    public static class BoolToIntConverter
    {
        public static int ConvertToInt(bool value)
        {
            return value ? 1 : 0;
        }
    }
}
