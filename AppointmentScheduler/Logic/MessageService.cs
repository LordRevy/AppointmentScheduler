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
                    ["LoginSuccess"] = "Login successful.\nWelcome -username-!\nLanguage selection: -language-\nCountry: -country-\nTimezone: -timezone-.\nDoes this sound correct?",
                    ["LoginFailed"] = "Invalid username or password.",
                    ["InvalidId"] = "Invalid ID format. Please enter a valid integer.",
                    ["AddedAppointment"] = "Appointment added successfully!",
                    ["AppointmentOverlap"] = "The appointment times you entered overlap with an existing appointment. Please adjust the times and try again.",
                    ["AddedCustomer"] = "Customer added successfully!",
                    ["FailedToAddAppointment"] = "Failed to add appointment. Please check your input and try again.\n\n-exception-",
                    ["FailedToAddCustomer"] = "Failed to add customer. Please check your input and try again.\n\n-exception-",
                    ["UpdatedAppointment"] = "Appointment updated successfully!",
                    ["AppointmentIDMissing"] = "The Appointment ID is missing or does not exist within the database.\n\n-exception-",
                    ["UpdatedCustomer"] = "Customer updated successfully!",
                    ["CustomerIDMissing"] = "The Customer ID is missing or does not exist within the database.\n\n-exception-",
                    ["FailedToUpdateAppointment"] = "Failed to update appointment. Please check your input and try again.\n\n-exception-",
                    ["FailedToUpdateCustomer"] = "Failed to update customer. Please check your input and try again.\n\n-exception-",
                    ["DeletedAppointment"] = "Appointment deleted successfully!",
                    ["DeletedCustomer"] = "Customer deleted successfully!",
                    ["FailedToDeleteAppointment"] = "Failed to delete appointment. Please try again.\n\n-exception-",
                    ["FailedToDeleteCustomer"] = "Failed to delete customer. Please try again.\n\n-exception-",
                    ["InvalidInput"] = "Invalid input. Please check your entries and try again."
                },
                ["la"] = new Dictionary<string, string>
                {
                    ["LoginSuccess"] = "Ingressus felix.\nSalve, -username-! Lingua tua est -language-, patria tua est -country-, zona temporis tua est -timezone-.\nRectene hoc sonat?",
                    ["LoginFailed"] = "Nomen usoris aut tessera non valida est.",
                    ["InvalidId"] = "Forma ID non valida. Quaeso, numerum integrum validum inseras.",
                    ["AddedAppointment"] = "Conventus feliciter additus est!",
                    ["AddedCustomer"] = "Cliens feliciter additus est!",
                    ["FailedToAddAppointment"] = "Conventum addere non potui. Quaeso, data inspice et iterum tenta.\n\n-exception-",
                    ["FailedToAddCustomer"] = "Clientem addere non potui. Quaeso, data inspice et iterum tenta.\n\n-exception-",
                    ["UpdatedAppointment"] = "Conventus feliciter renovatus est!",
                    ["UpdatedCustomer"] = "Cliens feliciter renovatus est!",
                    ["FailedToUpdateAppointment"] = "Conventum renovare non potui. Quaeso, data inspice et iterum tenta.\n\n-exception-",
                    ["FailedToUpdateCustomer"] = "Clientem renovare non potui. Quaeso, data inspice et iterum tenta.\n\n-exception-",
                    ["DeletedAppointment"] = "Conventus feliciter deletus est!",
                    ["DeletedCustomer"] = "Cliens feliciter deletus est!",
                    ["FailedToDeleteAppointment"] = "Conventum delere non potui. Quaeso, iterum tenta.\n\n-exception-",
                    ["FailedToDeleteCustomer"] = "Clientem delere non potui. Quaeso, iterum tenta era invalida.\n\n-exception-"
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

        public static void DisplayErrorMessage(string language, string message, Exception ex)
        {
            if (!messages.ContainsKey(language))
                language = "en";

            if (!messages[language].ContainsKey(message))
                MessageBox.Show($"Message {message} not found in message list.", "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            var errorMessage = messages[language][message].Replace("-exception-", ex.Message);

            MessageBox.Show(errorMessage, message, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var reportPath = "C:\\Users\\LabUser\\source\\repos\\AppointmentScheduler\\AppointmentScheduler\\Reports\\Reports.txt";
            var timeStamp = DateTime.UtcNow;
            var formattedReport = string.Join(
                Environment.NewLine, 
                reportData.Select(r => r?.ToString() ?? "MISSING DATA"));

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
