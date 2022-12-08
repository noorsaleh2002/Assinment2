using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

            fullName = fname + " " + lname;
        }
        public string GetFullName()
        {
            return fullName;
        }

    }
    [Serializable]
    class BankAccount
    {
        private Person p;
        private string cardNumber, pinCode, email;
        private int accountBalance;
        

        public BankAccount(Person p, string email, string cardNumber, string pinCode, int accountBalance)
        {
            this.p= p;
            if (cardNumber.Length == 9)
                this.cardNumber = cardNumber;
            else
            {

                Console.WriteLine(cardNumber.Length);
                Console.WriteLine("The CardNumber is wrong,it will be null");
                this.cardNumber = "";
            }
            if (pinCode.Length == 4)
                this.pinCode = pinCode;
            else
            {
                Console.WriteLine("The Pincode is wrong,it will be null");
                this.pinCode = "";
            }
            this.email = email;
            this.accountBalance = accountBalance;
        }

        public string CardNumber { get => cardNumber; set => cardNumber = value; }
        public string PinCode { get => pinCode; set => pinCode = value; }
        public string Email { get => email; set => email = value; }
        public int AccountBalance { get => accountBalance; set => accountBalance = value; }
        public string GetPersonfullName()
        {
           return p.GetFullName();
        }

        


    }
    [Serializable]
    class Bank
    {

        int bankCapacity;
        static public int NumberOfCustomers = 0;
        BankAccount[] banckAccounts;
        public Bank()
        {
            bankCapacity = 0;
        }
       

        public Bank(int bankCapacity)
        {
            this.bankCapacity = bankCapacity;
            banckAccounts = new BankAccount[bankCapacity];
        }
        public void AddNewAccount(BankAccount ba)
        {

            banckAccounts[NumberOfCustomers] = ba;
            NumberOfCustomers++;
             
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

                    return banckAccounts[i].AccountBalance;
            }
            return 0f;
        }
        public void Withdraw(BankAccount ba, int withD)
        {
            if(ba.AccountBalance>=withD)
            {
                ba.AccountBalance -= withD;
            }

        }
        public void Deposit(BankAccount ba, int deposit)
        {
            ba.AccountBalance+=deposit;


        }
        public void Save()
        {
            FileStream fs = new FileStream("Data.txt", FileMode.Create, FileAccess.Write);
            BinaryFormatter formatter = new BinaryFormatter();
            for (int i = 0; i < NumberOfCustomers; i++)
            {
                formatter.Serialize(fs, banckAccounts[i].GetPersonfullName());
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
            Person p1 = new Person("Noor", "saleh");

            string pinCode = "1234";
            string cardNumber = "123456789";
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
