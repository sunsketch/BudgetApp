/*  Name:   A. Davis
 *  Class:  COSC4320 - Software Engineering
 *  Date:   October 2023
 *  Description:
 *          Class is used to represent individual expenses
 *          
 *          Enum related to Expense information, 
 *              Expense Category - options for expenses to be categorized
 */

using System;

//category that each expense can be classified as
public enum ExpenseCategory
{
    Rent, Food, Utilities, Entertainment, Income, Other
}


//Each instance of this class will hold the information associated with the individual expenditure
public class Expense
{
    //fields_______________________________________________
    private readonly int _id;                       //unique identifier for each expense


    //properties___________________________________________

    public ExpenseCategory Category { get; set; }   //category of expense. ExpenseCategory is Enum 
    public double Amount { get; set; }              //the dollar amount 
    public DateTime Time { get; set; }				//when expense occured
    public string Name { get; set; }                //Name provided by user
    public int Id { get { return _id; } }           //use to retrieve _id outside class

    public static int IdCount { get; set; }         //# of ids created--necessary for unique id creation


    //constructors_________________________________________


    public Expense(string name, ExpenseCategory category, double amount, DateTime time)
    {

        this._id = IdCount++;

        this.Category = category;
        this.Amount = amount;
        this.Time = time;
        this.Name = name;
    }

    //use when id is already known(retrieving expense from txt file, updating expense)
    public Expense(int id, string name, ExpenseCategory category, double amount, DateTime time)
    {

        this._id = id;

        this.Category = category;
        this.Amount = amount;
        this.Time = time;
        this.Name = name;
    }

    //methods______________________________________________

    //returns a string representation of expense fields, seperated by a space
    public override string ToString()
    {
        string str = _id.ToString() + " "
                    + Time.ToString() + " "
                    + Category.ToString() + " "
                    + Amount + " "
                    + Name;

        return str;
    }
}//end Expense class
