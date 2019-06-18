using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Text;

namespace DilipDocuSignAssignment
{
    //This class performs proximity search based on the input provided.
    public class ProximitySearch
    {
        string keyword1, keyword2, filePath, fileName, rangeInput;
        long range;
        List<string> lines;
        List<string> buffer;        
        public ProximitySearch(string keyword1, string keyword2, string range, string fileName)
        {
            this.keyword1 = keyword1;
            this.keyword2 = keyword2;
            this.fileName = fileName;
            rangeInput = range;
            lines = new List<string>();
            buffer = new List<string>();
        }

        //Returns validation error message or the number of times the keywords exist in the document within the given range. 
        public string PerformSearch()
        {
            //Folder where the files is retrieved from app.config.
            filePath = Path.Combine(Directory.GetCurrentDirectory(), ConfigurationManager.AppSettings["FileFolderPath"], fileName);                 
            string result = ValidateInput();
            if (result == string.Empty)
            {
                ReadFile();
                long keyword1Count = 0, keyword2Count = 0, counter = 0;                
                List<string> wordsInRange = new List<string>();
                string wordFromBuffer = GetNextWord();

                /*Starting from the first occurence of one of the keywords read upto a maximum of n words and
                store the words in a list and increament the corresponding keyword counters. n = range.*/            
                while (wordFromBuffer != string.Empty)
                {
                    
                    if (wordFromBuffer == keyword1)
                        keyword1Count++;
                    else if (wordFromBuffer == keyword2)
                        keyword2Count++;

                    if(keyword1Count + keyword2Count > 0)
                    {
                        counter++;
                        wordsInRange.Add(wordFromBuffer);
                    }
                    
                    wordFromBuffer = counter<range ? GetNextWord() : string.Empty;
                }

                counter = 0;


                /*Iterate the list. if the word happens to be keyword1, increament the counter with the keyword2 count 
                and decrease keyword1 count  and vice versa. Remove the word from the list since it will go out of range.
                If buffer has a word add it to the list since it will come inside he range and increase the corresponding keyword count.*/

                while (wordsInRange.Count > 0)
                {
                    string word = wordsInRange[0];                    
                    if (word == keyword1)
                    {
                        counter += keyword2Count;
                        keyword1Count--;
                    }                        
                    else if (word == keyword2)
                    {
                        counter += keyword1Count;
                        keyword2Count--;
                    }

                    wordsInRange.RemoveAt(0);
                    wordFromBuffer = GetNextWord();
                    if (wordFromBuffer != string.Empty)
                    {
                        wordsInRange.Add(wordFromBuffer);
                        if (wordFromBuffer == keyword1)
                            keyword1Count++;
                        else if (wordFromBuffer == keyword2)
                            keyword2Count++;
                    }
                       
                }

                result = counter + "";
             
            }

            return result;
        }

        //read the file and add each line in the file to a list. 
        private void ReadFile()
        {                        
            foreach (string line in File.ReadLines(filePath, Encoding.UTF8))
            {
                if(line != null && line.Trim() != string.Empty)
                    lines.Add(line);
            }
        }

        //Returns the next word from the file to be processed if available else return an empty string. 
        private string GetNextWord()
        {
            if(buffer.Count == 0 && lines.Count != 0)
            {
                buffer.AddRange(lines[0].Split(' '));
                lines.RemoveAt(0);
            }

            string ans = string.Empty;
            if(buffer.Count > 0)
            {
                ans = buffer[0];
                buffer.RemoveAt(0);
            }
                
            return ans;
        }
      

        //validate the input provided. Range cannot be less than 2 and should be numeric. 
        private string ValidateInput()
        {
            if (!long.TryParse(rangeInput, out range) || range <= 1)
                return rangeInput + " is not a valid range. Range should be numeric and between 2 and 9223372036854775807.";

            if (!File.Exists(filePath))
                return "File '" + fileName + "' does not exist.";
            return string.Empty;
        }

    }
}



