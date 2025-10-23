namespace ANSYS.Application.Utils.Constants
{
    public static class UsuariosDefaultSystem
    {
        private const string USER_DEFAULT = "99999999-9999-9999-9999-999999999999";
        private const string USER_ATUALIZACAO = "22222222-2222-2222-2222-222222222222";

        public static Guid UserDefault => GetValueInGuid(USER_DEFAULT);
        public static Guid UserAtualizacao => GetValueInGuid(USER_ATUALIZACAO);

        private static Guid GetValueInGuid(string value)
        {
            return new Guid(value);
        }
    }
}
