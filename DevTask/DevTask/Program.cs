using System;
using System.Collections.Generic;
using System.IO;

namespace DevTask
{
    class Program
    {

        private string[] ReadFromFile(String in_path)
        {
            return File.ReadAllLines(in_path);
        }

        private void WriteToFile(String[] result, String out_path)
        {
            File.WriteAllLines(out_path, result);
        }

        private static string Set_IO_Path()
        {
            string currentDir = Environment.CurrentDirectory;
            //might have to change the number of  "..\" here depending on from where you run the program
            DirectoryInfo directory = new DirectoryInfo(Path.GetFullPath(Path.Combine(currentDir, @"..\..\..\..\..\")));
            return directory.ToString();
        }

        static void Main(string[] args)
        {
            //we begin by getting the path to the parent directory of where the input file is located via the Set_IO_Path function
            string base_path = Set_IO_Path();

            //we then add the specific sections leading to the desired input file and the designated outputfile
            string in_path = base_path + @"WinDevTaskA\testdata1.txt";
            string out_path = base_path + @"WinDevTaskA\outputdata.txt";

            List<String> my_output = new List<String>();
            //here we read all lines in the input file and store them in an array of strings
            string[] read_text = File.ReadAllLines(in_path);

            //here we do all the conversion of the input before putting it in output
            //first we initiate an object of the BarCode class type
            BarCode bar = new BarCode();
            //this function of the barcode class converts the barcode into numbers and stores them in a list of strings
            my_output = bar.Convert_To_Numbers(read_text);

            //Here we print the output
            File.WriteAllLines(out_path, my_output);

        }
    }
}
