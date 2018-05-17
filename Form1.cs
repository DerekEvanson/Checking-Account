using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* 
 * Written by Derek Evanson
 * With use of Coding Homework's educational material
 * 
 * Requirements
 * Form: Include radio buttons to indicate the type of transaction: deposit, check, or
 * service charge. A text box will allow the user to enter the amount of the 
 * transaction. Display the new balance in a ReadOnly text box or a label. Calculate 
 * the balance by adding deposits and subtracting service charges and checks. Include
 * buttons for Calculate, Clear, and Exit. 
 * 
 * Add validation to the project. Display a message box if the new balance would be a 
 * negative number. If there is not enough money to cover a check, do not deduct the
 * check amount. Ins ad, display a message box with the message "Insufficient Funds"
 * and deduct a servide charge of $10. 
 * 
 * Add a Summary button that will display the total number of deposits, the total
 * dollar amount of deposits, the number of checks, and the dollar amount of the 
 * checks. Do not include checks that were returned for insufficient funds, but do
 * include the service charges. Use a message box to display the summary information 
 */
namespace checking_account_balance
{
    public partial class Form1 : Form
    {
        private static double balance = 0;
        private static int totalDeposits = 0; // summary of all deposits 
        private static double totalDepositsAmnt = 0;  // sumary of all deposit dollar amounts
        private static int totalchecks = 0;  // sumary of all checks
        private static double totalCheckAmnt = 0; // summary of all check amounts

        public Form1()
        {
            InitializeComponent();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            transactionTextbox.Clear();
            transactionTextbox.Clear();
            depositBtn.Checked = false;
            checkBtn.Checked = false;
            serviceChargeBtn.Checked = false;
            transactionTextbox.Focus();
                

        }
        private bool validateTextBox() // return true if user enters valid number
        {
            double value;
            return Double.TryParse(transactionTextbox.Text, out value);
        }

        private void calculateBtn_Click(object sender, EventArgs e)
        {
            char transaction = ' '; // what transaction we are performing (deposit, check, or, service charge
            //find out what radio button is selected

            if (depositBtn.Checked) { transaction = 'd'; } //deposit
            else if (serviceChargeBtn.Checked) { transaction = 's'; } //service charge
            else if (checkBtn.Checked) { transaction = 'c'; } //check

            switch (transaction)
            {
                case 'd':
                    if (validateTextBox())//validate if user entered valid number into the text box
                    {
                        double deposit = Convert.ToDouble(transactionTextbox.Text);
                        depositTransaction(deposit);
                        desplayBalance();
                    }
                    else
                        errorMessage(); //else not valid
                    break;
                case 's':
                    if (validateTextBox())//validate if user entered valid number into the text box
                    {
                        double serviceCharge = Convert.ToDouble(transactionTextbox.Text);
                        serviceChargeTransaction(serviceCharge);
                        desplayBalance();
                    }
                    else
                        errorMessage(); //else not valid
                    break;
                case 'c':
                    if (validateTextBox())//validate if user entered valid number into the text box
                    {
                        double check = Convert.ToDouble(transactionTextbox.Text);
                        checkTransaction(check);
                        desplayBalance();
                    }
                    else
                        errorMessage(); //else not valid
                    break;
                default:
                    MessageBox.Show("Select a transaction");
                    break;
            }
        }

        private void checkTransaction(double c)
        {
            if (balance - c >= 0)
            {
                balance -= c;
                totalchecks += 1;
                totalCheckAmnt += c;
            }
            else
            {
                balance -= 10;
                MessageBox.Show("Insufficient funds, a $10 fee has been applied");
                totalCheckAmnt += 10;
            }
        }

        private void serviceChargeTransaction(double s)
        {
            if (balance - s >= 0) { balance -= s; }
            else
            {
                MessageBox.Show("The transaction can not be complete because the balance would be less than 0");
            }
        }

        private void errorMessage() //display error message if user dose not enter valid number into text box
        {
            MessageBox.Show("Invalide entery. Please enter a valid amount");
        }

        private void depositTransaction(double d) // deposit fucnction also calculates totals
        {
            balance += d;
            totalDeposits += 1;
            totalDepositsAmnt += d;
        }

        private void desplayBalance() //display balance to text box
        {
            balanceTextbox.Text = "$" + balance.ToString();
        }

        private void summaryBtn_Click(object sender, EventArgs e)
        {
            string message = "Total number of deposits:" + totalDeposits;
            message += "\nTotal amount of deposits: $" + totalDepositsAmnt;
            message += "\nTotal number of checks: " + totalchecks;
            message += "\nTotal amount of checks: " + totalCheckAmnt;

            MessageBox.Show(message);
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }


}
