using System;
using System.Collections.Generic;
using System.IO;

namespace RefactoringExercise.Exercise3
{
    class ShippingLabelPrinter
    {
        private readonly PrinterConfig printerConfig;

        public ShippingLabelPrinter()
        {
            printerConfig = PrinterConfig.Instance;
        }

        public void PrintLabel(Address address)
        {
            var printer = new Printer(printerConfig.Port);

            var addressFormat = new AddressFormat();
            addressFormat.Font = "Times New Roman";
            addressFormat.FontSize = 24;
            addressFormat.LineSpacing = 6;

            string town = address.Town;
            bool printDistrict = true;
            if (address.CountryCode == "CH")
            {
                addressFormat.Font = "Kai Bold";
                addressFormat.FontSize = 18;
                addressFormat.LineSpacing = 8;
            }
            if (address.CountryCode == "IT")
            {
                town = town.ToUpper();
            }
            if (address.CountryCode == "IR")
            {
                addressFormat.RightToLeft = true;
                addressFormat.FontSize = 15;
                if (address.District == address.Town)
                {
                    printDistrict = false;
                }
            }

            var lines = new List<string>();
            lines.Add(address.Name + ",");
            lines.Add(address.AddressLine1 + ",");
            if (!String.IsNullOrEmpty(address.AddressLine2))
                lines.Add(address.AddressLine2 + ",");
            lines.Add(town + ",");
            if (printDistrict)
                lines.Add(address.District.ToUpper());
            lines.Add(address.PostalCode);
            lines.Add(address.Country.ToUpper());

            foreach (var line in lines)
            {
                printer.PrintLine(line);
            }
        }
    }

    internal class AddressFormat
    {
        public string Font { get; set; }
        public int FontSize { get; set; }
        public int LineSpacing { get; set; }
        public bool RightToLeft { get; set; }
    }


    internal class PrinterConfig
    {
        private static PrinterConfig instance;
        private PrinterConfig(string configPath)
        {
            File.OpenRead(configPath);
        }

        public static PrinterConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PrinterConfig("PrintOptions.xml");
                }
                return instance;
            }
        }

        public int Port { get; set; }
    }

    internal class Printer
    {
        public Printer(int port)
        {

        }

        public void PrintLine(string line)
        {
        }

        public string Font { get; set; }
        public int FontSize { get; set; }
        public int LineSpacing { get; set; }
        public bool RightToLeft { get; set; }
    }

    class Address
    {
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
    }
}
