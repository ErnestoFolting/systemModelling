using Lab3.Models;
using System.Diagnostics;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            //BankModel bank = new BankModel();
            //bank.StartSimulation();

            //HospitalModel hospital = new HospitalModel();
            //hospital.StartSimulation();


            
            
            List<int> Ns = new List<int>() { 100,200,300,400,500,600,700,800,900,1000};


            Ns.ForEach(n =>
            {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                ChainModel chainModel = new ChainModel(n);
                chainModel.StartSimulation();
                stopwatch.Stop();

                long elapsedTimeMs = stopwatch.ElapsedMilliseconds;

                Console.WriteLine("Events " + (n + 1) + " execution Time: " + elapsedTimeMs + " ms");
            });

            //Ns.ForEach(n =>
            //{
            //    Stopwatch stopwatch = new Stopwatch();

            //    stopwatch.Start();
            //    BranchingModel branchingModel = new BranchingModel(n);
            //    branchingModel.StartSimulation();
            //    stopwatch.Stop();

            //    long elapsedTimeMs = stopwatch.ElapsedMilliseconds;

            //    Console.WriteLine("Events " + (n + 1) + " execution Time: " + elapsedTimeMs + " ms");
            //});

        }
    }
}