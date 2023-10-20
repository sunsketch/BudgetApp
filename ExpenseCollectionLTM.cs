/*  Name:   A. Davis
 *  Class:  COSC4320 - Software Engineering
 *  Date:   October 2023
 *  Description:
 *          Budget app's long-term memory(LTM)
 *          Saves/loads ExpenseCollection's data to/from a text file
 *          in the user's 'Documents' folder
 *          Should only be instantiated in ExpenseCollection class
 */

using System;
using System.Collections.Generic;
using System.IO;

public class ExpenseCollectionLTM
{
    //fields_________________________________________
    private readonly Dictionary<int, Expense> expenseDictionary;    //Dictionary of expenses
                                                                    //text file's path
    private readonly string DOC_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    private const string FILE_NAME = "budgetAppSavedExpenses.txt";  //name text file is saved as

    //constructors____________________________________

    public ExpenseCollectionLTM(Dictionary<int, Expense> expenseDictionary)
    {
        this.expenseDictionary = expenseDictionary;

    }

    //start with an empty expense dictionary
    public ExpenseCollectionLTM()
    {
        this.expenseDictionary = new Dictionary<int, Expense>();
    }

    //methods________________________________________

    //save the expense data to a textfile
    public void Save(int nElems)
    {
        string[] expenseStrings = new string[nElems + 1];    //Holds idCount(first element) & individual expense's values in string form
        int i = 0;                                          //used to identify IdCount in foreach loop

        //add the toString value of each expense to string array
        foreach (KeyValuePair<int, Expense> kvp in this.expenseDictionary)
        {
            //is this the first element of the array?
            if (i == 0)
            {                   //yes, add the total count of Ids ever made to first element of array
                expenseStrings[i] = Expense.IdCount.ToString();
                i++;
            }

            //no, add Expense's data 
            expenseStrings[i] = kvp.Value.ToString();
        }

        //String array is saved to text file, with data from each element on seperate lines
        using (StreamWriter outputFile = new StreamWriter(Path.Combine(DOC_PATH, FILE_NAME)))
        {
            foreach (string expenseString in expenseStrings)
            {
                outputFile.WriteLine(expenseString);
            }
        }//end of using = file closed
    }//end of Save()

    //load previously saved expense data
    public Dictionary<int, Expense> Load()
    {
        using (StreamReader sr = new StreamReader(Path.Combine(DOC_PATH, FILE_NAME)))
        {
            string line;                //holds one line from file
            int count = 0;              //kepts track of 1st iteration
            int idCount = 0;            //remebers the saved count of ids

            //read one line at a time until no more
            while ((line = sr.ReadLine()) != null)
            {
                //first line?
                if (count == 0)         //yes, then data is id count
                {
                    //turn string into an int
                    idCount = int.Parse(line);

                    //update IdCount 
                    Expense.IdCount = idCount;
                    count++;
                }
                else                    //no, it's Expense data
                {
                    //divide the string into individual values
                    string[] words = line.Split(' ');
                    int id = int.Parse(words[0]);

                    //join together 3 words that represent DateTime into 1 string
                    words[1] = words[1] + " " + words[2] + " " + words[3];

                    //more than one word used in the Name? 
                    if (words.Length > 7)
                    {                  //yes, assemble into a single string
                        for (int i = 7; i < words.Length; i++)
                        {
                            words[6] += " " + words[i];
                        }
                    }

                    //instantiate Expense with values converted from string
                    Expense tempExpense = new Expense(id, words[6], (ExpenseCategory)Enum.Parse(typeof(ExpenseCategory), words[4]), Convert.ToDouble(words[5]), Convert.ToDateTime(words[1]));

                    //add expense to the dictionary
                    expenseDictionary[id] = tempExpense;
                }//end else
            }//end while loop
        }//end using
        return expenseDictionary;
    }//end load()
}//end ExpenseCollectionLTM class