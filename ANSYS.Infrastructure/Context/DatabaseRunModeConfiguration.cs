namespace ANSYS.Infrastructure.Context
{
    public static class DatabaseRunModeConfiguration
    {
        public static bool IsLocalDataBase { get; private set; } = false;
        public static bool IsInitializateUsuario { get; private set; } = false; 

        public static void UseLocalDatabase(bool value)
        {
            IsLocalDataBase = value;
        }

        public static void InitializatedUsuario()
        {
            IsInitializateUsuario = true;
        }
    }
}
