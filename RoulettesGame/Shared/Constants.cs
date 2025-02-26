namespace RoulettesGame.Shared
{
    public static class Constants
    {
        public const int MIN_BET_AMMOUNT = 1;
        public const int MAX_BET_AMMOUNT = 10000;

        public const int MIN_NUMBER_RANGE = 0;
        public const int MAX_NUMBER_RANGE = 16;

        public const decimal GAIN_PER_NUMBER = 5;
        public const decimal GAIN_PER_COLOR = 1.8m;

        public const string ROULETTE_NOT_FOUND = "Ruleta no Existe";
        public const string ROULETTE_NOT_FOUND_OR_CLOSED = "Ruleta no se encuentra o esta Cerrada.";
        public const string ROULETTE_HAVE_NOT_BETS = "No hay apuestas asociadas en esta ruleta.";
        public const string INVALID_BET_AMOUNT = "Monto De Apuesta Invalido";
        public const string BET_REGISTERED = "Apuesta registrada.";
        public const string ROULETTE_OPENED_SUCCESSFULLY = "Ruleta abierta correctamente.";
        public const string ROULETTE_CREATED_SUCCESSFULLY = "Ruleta creada correctamente.";
        public const string ROULETTE_NOT_FOUND_OR_ALREADY_CLOSED = "Ruleta no existe o se encuentra cerrada.";
        public const string ROULETTE_ALREADY_OPEN = "La ruleta ha sido abierta previamente. Favor Validar.";
        public const string USER_IVALID = "Debe existir un usuario asociado.";

        public const string SERVER_ERROR = "Error Servidor.";
    }
}
