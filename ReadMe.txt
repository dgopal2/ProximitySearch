Proximity Search Application.

Assumptions:
i)Application - "ProximitySearch" is the only application for which the code is written. 
ii)Range should be between 2 and 9223372036854775807. 
iii) Only files which are added to the "Library" folder can be used in the application. 

While adding new files please right click on file -> properties -> Copy To Output -> Change it to Copy Always. 

User must pass the following arguments from command line - <application_name> <keyword1> <keyword2> <range> <input_filename>

Argument Validation. 
System will thrown an error if 
i) No arguments are passed. 
ii) Application name which is the first argument is not equal to - "ProximitySearch"
iii) Greater or lesser than 5 arguments are passed. 

Range and File Validation
Once arguments are validated proximity search function will be called. This function will return the number of times the keywords exist within the given range or will thrown
an error if invalid range or filename are passed. 

Algorithm:
i) Validate Range and Filename
ii) Open file read one line at a time and save the lines in a line list. 
iii) Pick the first line from the line list, split the words in a line by space, save it in a buffer and remove the line from the line list. 
iii) Iterate the words in the buffer. If buffer becomes empty repeat step (iii) if line list is not empty. 
iv) Once the first occurence of one of the keywords is found, read upto a maximum of n words or till the buffer becomes empty whichever is earlier, one word at a time,
   store the words in a words list and if the word happens to be a keyword increament the corresponding keyword counter. n = range.
v) Iterate the words list one word at a time. if the word happens to be keyword1, increament the counter with the keyword2 count.  
  Since this word has been processed this will go out of range when we process the next word.  So remove the word from the list and 
  also decrease the coreesponding keyword count if the word is a keyword.
vi) Repeat step 5 till words list becomes empty. Return the counter. 


Runtime :
First we read the file and save it in a list. A file with m lines takes o(m) for this step. 
Once this is completed we read x words to find first occurence and the following y words. x + y being the total words read at step 4 and y = range. This is o(x+y)
After this we iterate all words in  thh file once starting from the first ocuurence. A file with n words has a worst case of o(n) for this step. 
So total time = o(m) + o(x+y) + o(n)

In a worst case scenraio x+y will be equal to n so this will become o(m) + o(2n)

the overall run time of the alogorithm will be:

 O(m+n) - m - number of lines in the file, n - number of words in the file. 


 Space : We save the lines and words in a list. So space will also be o(m+n). 




 Note : While running Nunit test cases i copied the Library folder and files to the directory where Nunit was installed.  


