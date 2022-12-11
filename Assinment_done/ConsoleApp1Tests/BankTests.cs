using assinment2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ATM_TESTS
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddBankUser_AddNewUser_OneBankAccountCreated()
        {
            int bankCapacity = 10;
            Person p1 = new Person();

            string pinCode = "1234";
            string cardNumber = "123456789";
            int accountBalance = 100;
            BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


            Bank testBank = new Bank(bankCapacity);

            testBank.AddNewAccount(tmpAccount);

            Assert.AreEqual(testBank.NumberOfCustomers, 1);
            testBank.NumberOfCustomers = 0;

        }




        [TestMethod]
        public void IsBankUser_ValidAccount_AccountFound()
        {
            int bankCapacity = 10;
            Person p1 = new Person();

            string pinCode = "1234";
            string cardNumber = "123456789";
            int accountBalance = 100;
            BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


            Bank testBank = new Bank(bankCapacity);

            testBank.AddNewAccount(tmpAccount);

            Assert.IsTrue(testBank.IsBankUser(cardNumber, pinCode));
            testBank.NumberOfCustomers = 0;

        }



        [TestMethod]
        public void FindAccount_InValidAccount_AccountNotFound()
        {
            int bankCapacity = 10;
            Person p1 = new Person();

            string pinCode = "1234";
            string cardNumber = "123456789";
            int accountBalance = 100;
            BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


            Bank testBank = new Bank(bankCapacity);


            string fakeCardNumber = "1222345789";
            testBank.AddNewAccount(tmpAccount);

            Assert.IsFalse(testBank.IsBankUser(fakeCardNumber, pinCode));
            testBank.NumberOfCustomers = 0;

        }








        [TestMethod]
        public void CheckBalance_AskForBlance_GetBalanceAmount()
        {
            int bankCapacity = 10;
            Person p1 = new Person();

            string pinCode = "1234";
            string cardNumber = "123456789";
            int accountBalance = 100;
            BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


            Bank testBank = new Bank(bankCapacity);

            testBank.AddNewAccount(tmpAccount);



            Assert.AreEqual(testBank.CheckBalance(tmpAccount.CardNumber, tmpAccount.PinCode), 100);
            testBank.NumberOfCustomers = 0;

        }

        [TestMethod]
        public void Withdraw_WithVaildAmount_BalanceUpdated()
        {
            int bankCapacity = 10;
            Person p1 = new Person();

            string pinCode = "1234";
            string cardNumber = "123456789";
            int accountBalance = 100;
            BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


            Bank testBank = new Bank(bankCapacity);

            testBank.AddNewAccount(tmpAccount);

            int withdrawAmount = 50;

            testBank.Withdraw(tmpAccount, withdrawAmount);

            Assert.AreEqual(testBank.CheckBalance(tmpAccount.CardNumber, tmpAccount.PinCode), 50);
            testBank.NumberOfCustomers = 0;

        }


        [TestMethod]
        public void Deposit_WithVaildAmount_BalanceUpdated()
        {
            int bankCapacity = 10;
            Person p1 = new Person();

            string pinCode = "1234";
            string cardNumber = "123456789";
            int accountBalance = 100;
            BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


            Bank testBank = new Bank(bankCapacity);

            testBank.AddNewAccount(tmpAccount);

            int withdrawAmount = 50;

            testBank.Deposit(tmpAccount, withdrawAmount);

            Assert.AreEqual(testBank.CheckBalance(tmpAccount.CardNumber, tmpAccount.PinCode), 150);
            testBank.NumberOfCustomers = 0;

        }



        [TestMethod]
        public void SaveBankAccounts_AddOneBankAccount_DataSavedWithAccountsOnFile()
        {
            int bankCapacity = 10;
            Person p1 = new Person();

            string pinCode = "1234";
            string cardNumber = "123456789";
            int accountBalance = 100;
            BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


            Bank testBank = new Bank(bankCapacity);

            testBank.AddNewAccount(tmpAccount);

            testBank.Save();

            Bank newTestBank = new Bank();
            newTestBank.Load();

            Assert.AreEqual(testBank.NumberOfCustomers, 1);
            testBank.NumberOfCustomers = 0;


        }






    }
}
