using System;

namespace delegates
{
    class Program
    {
        //public delegate void Perform(string message);
        //static void Main(string[] args)
        //{
        //    //DELEGATES NORMAL VERSION


        //    //    var print = new Print() ;
        //    //    var logic = new Perform(PrintingMethod);
        //    //    var logic2 = new Perform(print.PrintingMethod2);
        //    //    string input = "my message";
        //    //    MessageText(input, logic);
        //    //    MessageText(input, logic2);

          
        //}
        static void Main(string[] args)
        {
          
            string input = "10";
            Console.WriteLine(MessageText(input, PrintingMethod));
               
        }
        
            public static string PrintingMethod(string sendMessage)
        {
            return ($"##--{sendMessage}--##");
        }
        public static string MessageText(string message, Func<string,string> perform)
        {

         
               return perform(message);
            
        }
        /////use func when there is return type..last one data type is returning type 
        ///func <string,int> here int is return type
    }
}
