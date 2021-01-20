using System;
using App.Entities;
using App.Services;
using App.Services.Exceptions;    
using Microsoft.Extensions.Logging;

namespace App
{
    class Program
    {
        static int Main(string[] args)
        {
            StructuralData data = new StructuralData();
            IGetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            try {
                getEnvironmentVariables.Validate(data);
            }
            catch(ExceptionError ex) {
                System.Console.WriteLine(ex.errorMsg);
                return 1;
            }

            PrintData(data);

            return 0;
        }

        public static void PrintData(StructuralData data) 
        {
            System.Console.WriteLine("Language: " + data.Language);
            System.Console.WriteLine("RoutePattern: " + data.RoutePattern);
            System.Console.WriteLine("Versioned: " + data.Versioned);
            System.Console.Write("HttpVerbs: ");
            foreach (var value in data.HttpVerbs) {
                Console.Write(value + ", ");
            }
            Console.Write("\b\b \n");
            System.Console.WriteLine("PathLevels: " + data.PathLevels);
            System.Console.WriteLine("StatusCode: {");
            foreach (var value in data.StatusCode)
            {
                System.Console.Write("   {0}: ", value.Key);
                foreach (var item in value.Value)
                {
                    System.Console.Write(item + ", ");
                }
                System.Console.Write("\b\b \n");
            }
            System.Console.WriteLine("}");
            System.Console.WriteLine("BaseUrl: " + data.BaseUrl);
            System.Console.WriteLine("SwaggerPath: " + data.SwaggerPath);
        }
    }
}
