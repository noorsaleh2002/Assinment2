
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
namespace assinment2
{
    [Serializable]
    public class Person
    {

        string fullName;
        string fname, lname;

        public Person()
        {

            this.FullName = "";
            this.fname = "";
            this.lname = "";

        }

        public Person(string fname, string lname)
        {
            this.fname = fname;
            this.lname = lname;
            FullName = fname + " " + lname;
        }

        public string Fname { get => fname; set => fname = value; }
        public string Lname { get => lname; set => lname = value; }
        public string FullName { get => fullName; set => fullName = value; }
    }
    [Serializable]
    public class BankAccount
    {
        private Person p;
        private string cardNumber, pinCode, email;
        private int accountBalance = 0;


        public BankAccount(Person p, string email, string cardNumber, string pinCode, int accountBalance)
        {
            this.p = p;
            if (cardNumber.Length == 9)
                this.cardNumber = cardNumber;
            else
            {

                Console.WriteLine("The length of the cardnumber is :{0}", cardNumber.Length);
                Console.WriteLine("The CardNumber is wrong,it will be null");
                this.cardNumber = "";
            }
            if (pinCode.Length == 4)
                this.pinCode = pinCode;
            else
            {
                Console.WriteLine("The Pincode is wrong it shoud be (4 char) so it will be null");
                this.pinCode = "";
            }
            this.email = email;
            this.accountBalance = accountBalance;
        }

        public string CardNumber { get => cardNumber; set => cardNumber = value; }
        public string PinCode { get => pinCode; set => pinCode = value; }
        public string Email { get => email; set => email = value; }
        public int AccountBalance { get => accountBalance; set => accountBalance = value; }
        public void print()
        {
            Console.WriteLine("User name: {0}", p.FullName);
            Console.WriteLine("Email: {0}", email);
            Console.WriteLine("Card number: {0}", cardNumber);
            Console.WriteLine("Pin code: {0}", pinCode);
            Console.WriteLine("Account number: {0}", accountBalance);
        }




    }
    [Serializable]
    public class Bank
    {

        int bankCapacity;
        static int  numberOfCustomers = 0;
       
        BankAccount[] banckAccounts;

       
        public Bank()
        {
            this.bankCapacity = 1;
        }


        public Bank(int bankCapacity)
        {
            this.bankCapacity = bankCapacity;
            banckAccounts = new BankAccount[this.bankCapacity];
        }
        public int NumberOfCustomers { get => numberOfCustomers; set => numberOfCustomers = value; }

        public void AddNewAccount(BankAccount ba)
        {

            banckAccounts[numberOfCustomers] = ba;
            
            numberOfCustomers++;

        }
        public bool IsBankUser(string cardNumber, string pinCode)
        {
            for (int i = 0; i < numberOfCustomers; i++)
            {
                if (banckAccounts[i].PinCode == pinCode && banckAccounts[i].CardNumber == cardNumber)

                    return true;
            }
            return false;
        }
        public float CheckBalance(string CardNumber, string PinCode)
        {
            for (int i = 0; i < numberOfCustomers; i++)
            {
                if (banckAccounts[i].PinCode == PinCode && banckAccounts[i].CardNumber == CardNumber)

                    return banckAccounts[i].AccountBalance;
            }
            return -1;
        }
        public void Withdraw(BankAccount ba, int withD)
        {
            if (ba.AccountBalance >= withD)
            {
                ba.AccountBalance -= withD;
            }

        }
        public void Deposit(BankAccount ba, int deposit)
        {
            ba.AccountBalance += deposit;


        }
        public void Save()
        {
            FileStream fs = new FileStream("data.txt", FileMode.Create, FileAccess.Write);
            BinaryFormatter formatter = new BinaryFormatter();
            
            
                for (int i =0; i<numberOfCustomers; i++)
                {
                    formatter.Serialize(fs, banckAccounts[i]);
                }
            
            
                fs.Close();
            


        }
        public void Load()
        {
            FileStream filestream = new FileStream("data.txt", FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            int i = 0;
           
                BankAccount[] ba = new BankAccount[filestream.Length];
                while (filestream.Position < filestream.Length)
                {
                    ba[i] = (BankAccount)bf.Deserialize(filestream);
                    ba[i].print();
                    i++;

                
                }
           
        }



    }



    class Program
    {
        static void Main(string[] args)
        {
            //Trying all the tests in main
            int bankCapacity = 10;
            Person p1 = new Person("Noor","Saleh");

            string pinCode = "1234";
            string cardNumber = "123456789";
            int accountBalance = 100;
            string fakeCardNumber = "1222345789";
            int withdrawAmount = 50;
            BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


            Bank testBank = new Bank(bankCapacity);

            testBank.AddNewAccount(tmpAccount);
            Console.WriteLine(testBank.NumberOfCustomers);
            Console.WriteLine(testBank.IsBankUser(cardNumber, pinCode));
            Console.WriteLine(testBank.IsBankUser(fakeCardNumber, pinCode));
            Console.WriteLine(testBank.CheckBalance(tmpAccount.CardNumber, tmpAccount.PinCode));
            testBank.Withdraw(tmpAccount, withdrawAmount);
            Console.WriteLine(testBank.CheckBalance(tmpAccount.CardNumber, tmpAccount.PinCode));
            testBank.Deposit(tmpAccount, withdrawAmount);
            Console.WriteLine(testBank.CheckBalance(tmpAccount.CardNumber, tmpAccount.PinCode));
            testBank.Save();

            Bank newTestBank = new Bank();
            newTestBank.Load();
            BankAccount bb = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);
            testBank.AddNewAccount(bb);
            Console.WriteLine(testBank.NumberOfCustomers);

        }
    }
}
