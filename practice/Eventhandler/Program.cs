using System;

namespace Eventhandler
{
    class Program
    {
        public delegate void Notify(string message, string address);
        public event Notify Notification;
        static void Main(string[] args)
        {
            var program = new Program();
            program.Notification += EmailNotification;
            program.Notification += SmsNotification;

            program.Notification("hello person","abcd@gmail.com");

            program.Notification -= EmailNotification;

            program.Notification("hello person", "abcd@gmail.com");

        }
 
        public static void EmailNotification (string message, string address)
        {
            Console.WriteLine($"Sending  email to {address} with message {message} ");
            
        }
        public static void SmsNotification(string message, string address)
        {
            Console.WriteLine($"Sending  sms to {address} with message {message} ");

        }
    }
}
