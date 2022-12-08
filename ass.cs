using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace assinment2
{
    [Serializable]
      class Person
    {
        
        string fullName;
        

        public Person()
        {
            
            this.fullName = "";
  
        }

        public Person(string fname, string lname)
        {
           
            fullName=fname+" "+lname;
        }
        public string GetFullName()
        {
            return fullName;
        }
        
    }
    [Serializable]
    class BankAccount
    {
         public Person p;
        public string CardNumber, PinCode;
        int accountBalance;
        public BankAccount(Person p , string email , string cardnumber,string pincode ,int accountnBalance )
        {
            this.p = p;
            
            CardNumber=cardnumber;
            PinCode=pincode;
            this.accountBalance = accountnBalance;



        }
        public int GetAccountBalance()
        {
            return accountBalance;

        }
        public void SetAccountBalance(int accountBalance)
        {
           this.accountBalance=accountBalance;
            
        }


    }
    [Serializable]
    class Bank
    {
        
        int bankCapacity ;
        public Bank()
        {
            bankCapacity = 0;
        }
        static public int NumberOfCustomers = 0;
        BankAccount[] banckAccounts ; 
        
        public Bank(int bankCapacity)
        {
            this.bankCapacity = bankCapacity;
            banckAccounts = new BankAccount[bankCapacity];
        }
        public void AddNewAccount(BankAccount ba)
        {
            
            banckAccounts[NumberOfCustomers] = ba;
            NumberOfCustomers++;
            //numbofcustomer++; 
        }
        public bool IsBankUser(string cardNumber, string pinCode)
        {
            for (int i = 0; i < NumberOfCustomers; i++)
            {
                if (banckAccounts[i].PinCode == pinCode && banckAccounts[i].CardNumber == cardNumber)

                    return true;
            }
            return false;
        }
        public float CheckBalance(string CardNumber, string PinCode)
        {
            for (int i = 0; i < NumberOfCustomers; i++)
            {
                if (banckAccounts[i].PinCode == PinCode && banckAccounts[i].CardNumber == CardNumber)

                    return banckAccounts[i].GetAccountBalance();
            }
            return 0f;
        }
        public void Withdraw(BankAccount ba,int withD)
        {
            int monye=ba.GetAccountBalance();
            if(monye>=withD)
            {
                monye-=withD;
            }
            string CardNumber=ba.CardNumber;
            string PinCode=ba.PinCode;
            for ( int i =0; i< NumberOfCustomers; i++)
            {
                if (CardNumber == banckAccounts[i].CardNumber && PinCode == banckAccounts[i].PinCode)
                {
                    banckAccounts[i].SetAccountBalance(monye);
                    
                }

            }


        }
        public void Deposit(BankAccount ba, int deposit)
        {
            int monye = ba.GetAccountBalance();
            monye += deposit;
            
            string CardNumber = ba.CardNumber;
            string PinCode = ba.PinCode;
            for (int i = 0; i < NumberOfCustomers; i++)
            {
                if (CardNumber == banckAccounts[i].PinCode && PinCode == banckAccounts[i].PinCode)
                {
                    banckAccounts[i].SetAccountBalance(monye);
                  
                }

            }


        }
        public void Save()
        {
            FileStream fs = new FileStream("Data.txt", FileMode.Create, FileAccess.Write);
            BinaryFormatter formatter = new BinaryFormatter();
            for (int i=0; i< NumberOfCustomers; i++)
            {
                formatter.Serialize(fs, banckAccounts[i].p.GetFullName());
            }
            fs.Close();


        }
        public void Load()
        {
            FileStream filestream = new FileStream("Data.txt", FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            
            while (filestream.Position < filestream.Length)
            {
                string name = (string)bf.Deserialize(filestream);
                Console.WriteLine(name);
            }
            filestream.Close();
        }



    }


    
    class Program
    {
        static void Main(string[] args)
        {
            int bankCapacity = 10;
            Person p1 = new Person("Noor","saleh");

            string pinCode = "1234";
            string cardNumber = "12345789";
            int accountBalance = 100;
            BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


            Bank testBank = new Bank(bankCapacity);

            testBank.AddNewAccount(tmpAccount);

            testBank.Save();

            Bank newTestBank = new Bank();
            newTestBank.Load();
        }
    }
}
