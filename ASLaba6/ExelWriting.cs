using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using exel = Microsoft.Office.Interop.Excel;

namespace ASLaba6
{
    public class ExelWriting
    {
        private exel.Application exelapp;
        private exel.Workbook exelworkbook;
        private exel.Worksheet exelworksheet;
        private string OutputPath;
        private bool SavedOnes = false;
        public ExelWriting(string output)
        {
            exelapp = new exel.Application();

            exelworkbook = exelapp.Workbooks.Add();
            exelworksheet = (exel.Worksheet)exelworkbook.Worksheets.get_Item(1);

            OutputPath = output;
        }
        public void WriteExel(int x, int y, int value)
        {
            exelworksheet.Cells[x, y] = value;
            TrySave();
        }
        public void WriteExel(int x, int y, string value)
        {
            exelworksheet.Cells[x, y] = value;
            TrySave();
        }
        public void WriteExelFormula(int x, int y, string value)
        {
            exel.Range rng = exelworksheet.Cells[x, y];
            rng.Formula = value;
            rng.FormulaHidden = false;
            TrySave();
        }
        public string RNGName(int x, int y)
        {
            exel.Range rng = exelworksheet.Cells[x, y];
            return rng.Address;
        }
        public void WriteDiagramm(string xstart, string xend, string ystart, string yend, string name, string legend)
        {
            exel.ChartObjects chartobjects = exelworksheet.ChartObjects();
            exel.ChartObject chartobj = chartobjects.Add(50, 50, 400, 300);
            exel.Chart chart = chartobj.Chart;

            chart.ChartType = exel.XlChartType.xl3DColumn;
            exel.SeriesCollection seriesc = chart.SeriesCollection(Type.Missing);

            exel.Series series = seriesc.NewSeries();
            series.XValues = exelworksheet.get_Range(xstart, xend);
            series.Values = exelworksheet.get_Range(ystart, yend);

            chart.HasTitle = true;
            chart.ChartTitle.Text = name;

            chart.HasLegend = true;
            series.Name = legend;

            TrySave();
        }
        public void TrySave()
        {
            try
            {
                if(!SavedOnes)
                {
                    exelworkbook.SaveAs(OutputPath);
                    SavedOnes = true;
                }
                else
                {
                    exelworkbook.Save();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Close()
        {
            exelapp.Quit();
        }
    }
}
