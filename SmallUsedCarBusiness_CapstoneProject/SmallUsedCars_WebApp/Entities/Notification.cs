namespace SmallUsedCars_WebApp.Entities
{
    public class Notification
    {
        public int NotificationId { get; set; }

        // e.g., "Maintenance Reminder", "Promotional Email", etc.
        public string NotificationType { get; set; }

        // Email subject
        public string Subject { get; set; }

        // Email/message content
        public string Message { get; set; }

        // When the notification was created/sent
        public DateTime SentDate { get; set; }
    }
}
