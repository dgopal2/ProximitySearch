using System;
using System.Text;


namespace DilipDocuSignAssignment
{
    class Program
    {
        //Code with the assumption that only "ProximitySearch" application will only work.  More applications can be added to this code.       
        static void Main(string[] args)
        {
            string result = ValidateArguments(args);
            if(result == string.Empty && args[0] == Constants.proximitySearchApplication)
            {
                ProximitySearch proximitySearch = new ProximitySearch(args[1], args[2], args[3], args[4]);
                result = proximitySearch.PerformSearch();
            }

            Console.WriteLine("{0}", result);
            Console.ReadKey();            
        }

        //Directly validating the application name against ProximitySearch. 
        //If there are multiple application they can be stored in a hashset and validated using that to get o(1) runtime for application validation. 
        public static string ValidateArguments(string[] args)
        {
            if (args == null || args.Length == 0)
                return "No command line arguments found.";

            if (args[0] != Constants.proximitySearchApplication)
                return "Application '" + args[0] + "' cannot be found.";

            //Reason to check first argument as ProximitySearch is that if more applications are added in future each can be validated accordingly. 
            if (args[0] == Constants.proximitySearchApplication && args.Length != 5)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(args.Length < 5 ? "Fewer arguments passed. " : "Too many arguments passed. ");
                sb.Append("ProximitySearch expects the following arguments - <APPLICATION NAME> <KEYWORD1> <KEYWORD2> <RANGE> <FILE NAME>.");
                return sb.ToString();
            }
            return string.Empty;
        }

    }
}
