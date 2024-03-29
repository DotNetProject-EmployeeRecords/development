﻿namespace Eisk.Helpers
{
    public enum MessageType { Success, Error, Notice }

    public class MessageFormatter
    {
        public static string GetFormattedSuccessMessage(string message)
        {
            return GetFormattedMessage(message, MessageType.Success);
        }

        public static string GetFormattedErrorMessage(string message)
        {
            return GetFormattedMessage(message, MessageType.Error);
        }

        public static string GetFormattedNoticeMessage(string message)
        {
            return GetFormattedMessage(message, MessageType.Notice);
        }

        public static string GetFormattedMessage(string message, MessageType messageType = MessageType.Notice)
        {
            switch (messageType)
            {
                case MessageType.Success: return "<div class='success'>" + message + "</div>";
                case MessageType.Error: return "<div class='error'>" + message + "</div>";
                default: return "<div class='notice'>" + message + "</div>";
            }
        }
        
    }
}