using MathNet.Numerics;
using ScottPlot;

namespace GraphPlottingApp
{
    public class PlotHelper
    {

        public static List<List<(double simulationTime, double avgServingTime)>> statsPlotsData = new();
        private Plot plt;
        public PlotHelper()
        {
            plt = new ScottPlot.Plot();
        }

        public void GetPlot()
        {
            plt.XLabel("Simulation time");
            plt.YLabel("Avg serving time");


            int counter = 0;
            statsPlotsData.ForEach(el =>
            {
                double[] xValues = el.Select(pair => pair.simulationTime).ToArray();
                double[] yValues = el.Select(pair => pair.avgServingTime).ToArray();

                plt.PlotScatter(xValues, yValues, label: "Run " + counter++);
            });

            counter = 0;

            plt.Legend();

            plt.Title("");
            
            plt.SaveFig("graph.png");

            Console.WriteLine("Graph saved as graph.png");
        }
    }
}
