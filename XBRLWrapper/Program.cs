using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeffFerguson.Gepsio;

namespace XBRLWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("usage: XBRLWrapper [XBRL File]");
                return;
            }
            ProcessXBRLFile(args[0]);
        }
          
        private static void ProcessXBRLFile(string xbrlFile)
        {
            try
            {
                var xbrlDoc = new XbrlDocument();
                xbrlDoc.Load(xbrlFile);
                foreach (var fragment in xbrlDoc.XbrlFragments)
                {
                    DisplayFragmentStatistics(fragment);
                    WriteFactValues(fragment);
                    WriteUnitValues(fragment);
                    WriteContextValues(fragment);



                }
            }
            catch (Exception xbrlException)
            {
                Console.WriteLine("ERROR: {0}", xbrlException.Message);
            }
        }
             
        private static void DisplayFragmentStatistics(XbrlFragment fragment)
        {
            var facts = fragment.Facts;
            Console.WriteLine("Facts: {0}",facts.Count);
            var units = fragment.Units;
            Console.WriteLine("Units: {0}", units.Count);
            var contexts = fragment.Contexts;
            Console.WriteLine("Contexts: {0}", contexts.Count);
        }

 
        private static void WriteFactValues(XbrlFragment fragment)
        {
            foreach (var fact in fragment.Facts)
            {
                var currentFactAsItem = fact as Item;
                Console.WriteLine("FactName: {0} / Fact: {1}", fact.Name, currentFactAsItem.Value);
            }
        }

        private static void WriteUnitValues(XbrlFragment fragment)
        {
            foreach (var unit in fragment.Units)
            { 
                Console.WriteLine("Unit ID: {0} / Ratio: {1} / Region: {2}", unit.Id, unit.Ratio, unit.RegionInformation);
            }
        }
         private static void WriteContextValues(XbrlFragment fragment)
        {
            foreach (var context in fragment.Contexts)
            { 
                Console.WriteLine("Context ID: {0} / Identifier: {1} / IdentifierScheme: {2}", context.Id, context.Identifier, context.IdentifierScheme);
            }
        }
    }
}
