using AppointmentScheduler.Domain;
using Org.BouncyCastle.Asn1.IsisMtt.X509;

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
                    ["AreYouSure"] = "Warning, this action is permanent. Are you sure you want to proceed?",
                    ["InvalidId"] = "Invalid ID format. Please enter a valid integer.",
                    ["AddedAppointment"] = "Appointment added successfully!",
                    ["AppointmentOverlap"] = "The appointment times you entered overlap with an existing appointment. Please adjust the times and try again.",
                    ["AddedCustomer"] = "Customer added successfully!",
                    ["FailedToAddAppointment"] = "Failed to add appointment. Please check your input and try again.",
                    ["FailedToAddCustomer"] = "Failed to add customer. Please check your input and try again.",
                    ["UpdatedAppointment"] = "Appointment updated successfully!",
                    ["AppointmentIDMissing"] = "The Appointment ID is missing or does not exist within the database.",
                    ["UpdatedCustomer"] = "Customer updated successfully!",
                    ["CustomerIDMissing"] = "The Customer ID is missing or does not exist within the database.",
                    ["FailedToUpdateAppointment"] = "Failed to update appointment. Please check your input and try again.",
                    ["FailedToUpdateCustomer"] = "Failed to update customer. Please check your input and try again.",
                    ["DeletedAppointment"] = "Appointment deleted successfully!",
                    ["DeletedCustomer"] = "Customer deleted successfully!",
                    ["FailedToDeleteAppointment"] = "Failed to delete appointment. Please try again.",
                    ["FailedToDeleteCustomer"] = "Failed to delete customer. Please try again.",
                    ["InvalidTime"] = "Time must be between 9:00 AM and 5:00 PM, Monday through Friday. Please adjust the appointment times and try again.",
                    ["InvalidInput"] = "Invalid input. Please check your entries and try again."
                },
                ["la"] = new Dictionary<string, string>
                {
                    ["LoginSuccess"] = "Accessus prosper fuit.\nSalve, -username-!\nElectio linguae: -language-\nPatria: -country-\nZona temporis: -timezone-.\nRectene hoc sonat?",
                    ["LoginFailed"] = "Nomen usoris aut tessera invalida est.",
                    ["AreYouSure"] = "Monitio: haec actio perpetua est. Certusne es te pergere velle?",
                    ["InvalidId"] = "Forma ID invalida est. Quaeso, integerum validum insere.",
                    ["AddedAppointment"] = "Conventus feliciter additus est!",
                    ["AppointmentOverlap"] = "Tempora conventus a te inserta cum conventu iam exstante concurrunt. Quaeso tempora corrige et iterum tenta.",
                    ["AddedCustomer"] = "Cliens feliciter additus est!",
                    ["FailedToAddAppointment"] = "Conventum addere non potuit. Quaeso, data tua verifica et iterum tenta.",
                    ["FailedToAddCustomer"] = "Clientem addere non potuit. Quaeso, data tua verifica et iterum tenta.",
                    ["UpdatedAppointment"] = "Conventus feliciter renovatus est!",
                    ["AppointmentIDMissing"] = "ID conventus deest aut in tabulario non exstat.",
                    ["UpdatedCustomer"] = "Cliens feliciter renovatus est!",
                    ["CustomerIDMissing"] = "ID clientis deest aut in tabulario non exstat.",
                    ["FailedToUpdateAppointment"] = "Conventum renovare non potuit. Quaeso, data tua verifica et iterum tenta.",
                    ["FailedToUpdateCustomer"] = "Clientem renovare non potuit. Quaeso, data tua verifica et iterum tenta.",
                    ["DeletedAppointment"] = "Conventus feliciter deletus est!",
                    ["DeletedCustomer"] = "Cliens feliciter deletus est!",
                    ["FailedToDeleteAppointment"] = "Conventum delere non potuit. Quaeso iterum tenta.",
                    ["FailedToDeleteCustomer"] = "Clientem delere non potuit. Quaeso iterum tenta.",
                    ["InvalidTime"] = "Tempus inter horam IX ante meridiem et horam V post meridiem, a die Lunae ad diem Veneris, esse debet. Quaeso tempora conventus corrige et iterum tenta.",
                    ["InvalidInput"] = "Inscriptio invalida est. Quaeso, quae inseruisti verifica et iterum tenta."
                }
            };

        public static void DisplayMessage(string language, string message, MessageBoxIcon icon, string additionalInfo = "")
        {
            if (!messages.ContainsKey(language))
                language = "en";

            if (!messages[language].ContainsKey(message))
            {
                MessageBox.Show($"Message {message} not found in message list.", "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(messages[language][message], message + "\n\n" + additionalInfo, MessageBoxButtons.OK, icon);
        }

        public static bool DisplayYesOrNo(string language, string message, MessageBoxIcon icon)
        {
            if (!messages.ContainsKey(language))
                language = "en";

            if (!messages[language].ContainsKey(message))
            {
                MessageBox.Show($"Message {message} not found in message list.", "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return (MessageBox.Show(messages[language][message], message, MessageBoxButtons.YesNo, icon)) == DialogResult.Yes;
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

        public static void WriteReport<T>(string reportName, List<T> reportData, string columns)
        {
            var reportPath = "C:\\Users\\LabUser\\source\\repos\\AppointmentScheduler\\AppointmentScheduler\\Reports\\Reports.txt";
            var timeStamp = DateTime.UtcNow;
            var formattedReport = string.Join(
                Environment.NewLine, 
                reportData.Select(r => r?.ToString() ?? "MISSING DATA"));

            var message = $@"
===================================================================
                Report: {reportName}
                Generated (UTC): {timeStamp:u}
___________________________________________________________________
{ columns }
{ formattedReport }
                

                *End of Report*

                ";

            File.AppendAllText(reportPath, message);
        }
    }
}
