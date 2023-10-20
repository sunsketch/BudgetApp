/*  Name:   A. Davis
 *  Class:  COSC4320 - Software Engineering
 *  Date:   October 2023
 *  Description:
 *          Holds all expenses in user's budget.
 *          Provides methods for interacting with this data ie CRUD operations.
 *          Only class that should instantiate an ExpenseCollectionLTM object.
 */

using System;

using System.Collections.Generic;

public class ExpenseCollection
{
    //fields_________________________________________________
    private int _nElems;                            //total number of expenses 
    private Dictionary<int, Expense> expenses;      //holds all saved expenses

    //constructor____________________________________________
    public ExpenseCollection()
    {
        this.expenses = new Dictionary<int, Expense>();
    }

    //methods________________________________________________

    public override string ToString()
    {
        string str = "";

        //add the toString value of every expense together
        foreach (KeyValuePair<int, Expense> kvp in this.expenses)
        {

            str += kvp.Value.ToString();
            //new line between Expense entries
            str += "\n";
        }
        return str;
    }

    //return # of expenses in dictionary
    public int GetNElems() => _nElems;

    //return Expense object based on id 
    public Expense GetExpense(int id)
    {
        //does that expense exist??
        if (!expenses.ContainsKey(id))
        {
            return null;				//no, return null value
        }

        return expenses[id];			//yes return value
    }

    //Add a new expense to dictionary, returns Expense's unique identifier 
    public int AddExpense(string name, ExpenseCategory category, double amount, DateTime time)
    {
        //temporary variable creates new expense to generate id 
        Expense tempExpense = new Expense(name, category, amount, time);

        //Adds variable to the Dictionary with Id as key
        expenses[tempExpense.Id] = tempExpense;

        //update count of total expenses
        _nElems++;

        //send back the saved expense's unique id 
        return tempExpense.Id;
    }

    //remove expense from dictionary
    public bool DeleteExpense(int id)
    {
        //does that expense exist??
        if (!expenses.ContainsKey(id))
        { //no, nothing happens
            return false;
        }

        expenses.Remove(id);            //yes, delete expense & update count
        _nElems--;
        return true;
    }

    //update expense record
    public bool UpdateExpense(int id, string name, ExpenseCategory category, double amount, DateTime time)
    {
        //that expense does not exist, abort
        if (!expenses.ContainsKey(id))
            return false;

        //expense exists, replace with object holding updated data
        expenses[id] = new Expense(id, name, category, amount, time);

        //return success
        return true;
    }

    /*_________________,
     * File operations |
     * ---------------/
     */

    //save all expenses in dictionary to file, if unsuccessful returns false
    public bool SaveExpenses()
    {
        //is there any data to save?
        if (_nElems == 0)
        {
            return false;       //no, no data, save failed
        }

        //yes, have data. send to long-term memory
        ExpenseCollectionLTM lTMemory = new ExpenseCollectionLTM(expenses);
        lTMemory.Save(_nElems);
        return true;
    }

    //load all expenses from saved file -- unsuccessful load returns false
    public bool LoadExpenses()
    {
        //cannot load new data if data is already input into the system ((WILL NOT LOAD IF DATA IS SAVED,BUT NOT LOADED BEFORE INPUTING A NEW EXPENSE. INPUT FORM MUST REFLECT THIS))
        if (_nElems > 0)
        {
            return false;
        }

        //use long-term memory to load saved data
        ExpenseCollectionLTM lTMemory = new ExpenseCollectionLTM();
        expenses = lTMemory.Load();

        //load successful?
        if (expenses == null)   //no, there was no saved data
        {
            return false;
        }
        //yes, update variable that tracks # of expenses to reflect loaded expenses.
        _nElems = expenses.Count;
        return true;            //Send back confirmation of success
    }//end loadExpenses()
}//end ExpenseCollection class
