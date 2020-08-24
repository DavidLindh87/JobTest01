using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.NetworkInformation;

namespace DevTask
{
    class BarCode
    {
        //private string Left_Guard;
        //private string Center_Guard;
        //private string Right_Guard;
        private string new_lines;


        private void Barcode()
        {
            //we initialize the variables in the constructor
            new_lines = string.Empty;
        }

        public List<String> Convert_To_Numbers(string[] input)
        {
            List<String> my_input = new List<String>();
            //we loop through each character in each string in the array and convert it into either ones or zeros
            foreach (string line in input)
            {
                foreach (char c in line)
                {
                    if(Char.IsWhiteSpace(c))
                    {
                        new_lines += "0";
                    }
                    else
                    {
                        new_lines += "1";
                    }
                }
                //Now that we have the line of ones and zeros we can sift out the Left-, Center- and Right Guards 
                //and separate the numbers in the remaining string, but lets do it in another function to keep it seperate
                new_lines = Binary_To_Integer(new_lines);
                my_input.Add(new_lines);
                new_lines=string.Empty;
            }
            return my_input;
        }

        public string Binary_To_Integer(string barcode)
        {
            string converted_string = string.Empty;
            string unguarded_string = barcode;

            //first we remove and store the guards from the binary barcode
            unguarded_string = unguarded_string.Remove(0, 3); //remove Left Guard
            unguarded_string = unguarded_string.Remove(42, 5); //remove Middle Guard
            unguarded_string = unguarded_string.Remove(84); //remove Right Guard

            //now we want to split the remaining string into substrings of size 7
            int size = 7;
            IEnumerable<string> split_string = unguarded_string.Split(size);
            //now we need to decode the barcode 
            converted_string = Decoding(split_string);
            //string test = String.Join(Environment.NewLine, split_string);           
            return converted_string;
        }

        private string Decoding(IEnumerable<string> spt_str)
        {
            IEnumerator<string> e = spt_str.GetEnumerator();
            List<string> converted = new List<string>();
            string final_str = string.Empty;
            while (e.MoveNext())
            {
                string tmp = e.Current;
                string conversion = string.Empty;
                int n = 1;
                for(int i = 0; i < tmp.Length; i++){                    
                    if ((i + 1)!=tmp.Length)
                    {
                        if (tmp[i].Equals(tmp[i + 1]))
                        {
                            n++;
                        }
                        else
                        {
                            conversion += n.ToString();
                            n = 1;
                        }
                    }
                    else
                    {
                        conversion += n.ToString();
                    }
                }
                converted.Add(conversion);
            }
            //now we use the numbers in the converted list to compare with the preexisting comparisons to numbers for Left and Right side and return it
            final_str = Final_Conversion(converted);
            return final_str;
            //return converted[0] + converted[1] + converted[2] + converted[3] + converted[4] + converted[5] + converted[6];
        }

        private string Final_Conversion(List<string> lst_str)
        {
            var output = new StringBuilder();
            for (int i = 0; i < lst_str.Count; i++)
            {
                if(lst_str[i].Equals("3211"))
                {
                    output.Append("0");
                }else if (lst_str[i].Equals("2221"))
                {
                    output.Append("1");
                }
                else if (lst_str[i].Equals("2122"))
                {
                    output.Append("2");
                }
                else if (lst_str[i].Equals("1411"))
                {
                    output.Append("3");
                }
                else if (lst_str[i].Equals("1132"))
                {
                    output.Append("4");
                }
                else if (lst_str[i].Equals("1231"))
                {
                    output.Append("5");
                }
                else if (lst_str[i].Equals("1114"))
                {
                    output.Append("6");
                }
                else if (lst_str[i].Equals("1312"))
                {
                    output.Append("7");
                }
                else if (lst_str[i].Equals("1213"))
                {
                    output.Append("8");
                }
                else if (lst_str[i].Equals("3112"))
                {
                    output.Append("9");
                }
            }
            //Console.WriteLine(output.ToString());
            return output.ToString();
        }
    }
}
