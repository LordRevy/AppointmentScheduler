namespace AppointmentScheduler.Logic
{
    public class MessageService
    {
        private static readonly Dictionary<string, Dictionary<string, string>> messages =
            new Dictionary<string, Dictionary<string, string>>
            {
                ["en"] = new Dictionary<string, string>
                {
                    ["LoginSuccess"] = "Login successful!",
                    ["InvalidCredentials"] = "Invalid username or password."
                },
                ["la"] = new Dictionary<string, string>
                {
                    ["LoginSuccess"] = "Login completus!",
                    ["InvalidCredentials"] = "Nomen usoris aut tessera invalida."
                }
            };

        public static string GetMessage(string language, string message)
        {
            if (!messages.ContainsKey(language))
                language = "en";

            if (!messages[language].ContainsKey(message))
                return $"{language} is missing message: {message}";

            return messages[language][message];
        }
    }
}
