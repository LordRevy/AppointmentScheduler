using AppointmentScheduler.Domain;

namespace AppointmentScheduler.Logic
{
    public class MessageService
    {
        private static readonly Dictionary<string, Dictionary<string, string>> messages =
            new()
            {
                ["en"] = new Dictionary<string, string>
                {
                    ["LoginSuccess"] = "Login successful.\nWelcome -username-! Your Language is -language-, your Country is -country-, your Timezone is -timezone-.\nDoes this sound correct?",
                    ["LoginFailed"] = "Invalid username or password.",
                    ["InvalidId"] = "Invalid ID format. Please enter a valid integer."
                },
                ["la"] = new Dictionary<string, string>
                {
                    ["LoginSuccess"] = "Login completus!",
                    ["LoginFailed"] = "Nomen usoris aut tessera invalida."
                }
            };

        public static void DisplayMessage(string language, string message, MessageBoxIcon icon)
        {
            if (!messages.ContainsKey(language))
                language = "en";

            if (!messages[language].ContainsKey(message))
                MessageBox.Show($"Message {message} not found in message list.", "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            MessageBox.Show(messages[language][message], message, MessageBoxButtons.OK, icon);
        }

        public static void DisplayLoginSuccessMessage(User user)
        {
            if (!messages.ContainsKey(user.Language))
                user.Language = "en";
            var message = messages[user.Language]["LoginSuccess"]
                .Replace("-username-", user.UserName)
                .Replace("-language-", user.Language)
                .Replace("-country-", user.Country ?? "N/A")
                .Replace("-timezone-", user.Timezone ?? "N/A");

            var result = MessageBox.Show(message, "Login Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.No)
            {
                MessageBox.Show("Please update your profile information.", "Profile Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void WriteReport<T>(string reportName, List<T> reportData)
        {
            var reportPath = "Reports.txt";
            var timeStamp = DateTime.UtcNow;
            var formattedReport = string.Join(Environment.NewLine, reportData.Select(r => r.ToString()));

            var message = $@"
                ====================================================
                Report: {reportName}
                Generated (UTC): {timeStamp:u}
                ====================================================

                {formattedReport}
                
                *End of Report*
                ";

            File.AppendAllText(reportPath, message);
        }
    }
}
