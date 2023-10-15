using Lab3.Models;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            //BankModel bank = new BankModel();
            //bank.StartSimulation();

            HospitalModel hospital = new HospitalModel();
            hospital.StartSimulation();
        }
    }
}