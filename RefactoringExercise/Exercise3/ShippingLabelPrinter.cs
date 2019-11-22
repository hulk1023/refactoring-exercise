using System;
using System.Collections.Generic;
using System.IO;

namespace RefactoringExercise.Exercise3
{
    internal interface IAddressLabelFormatter
    {
        AddressLabelFormat GetAddressLabelFormat(Address address);
    }

    class AddressLabelFormatter : IAddressLabelFormatter
    {
        public AddressLabelFormatter()
        {
        }

        public AddressLabelFormat GetAddressLabelFormat(Address address)
        {
            var addressLabelFormat = new AddressLabelFormat();

            addressLabelFormat.Font = "Times New Roman";
            addressLabelFormat.FontSize = 24;
            addressLabelFormat.LineSpacing = 6;

            string town = address.Town;
            bool printDistrict = true;
            if (address.CountryCode == "CH")
            {
                addressLabelFormat.Font = "Kai Bold";
                addressLabelFormat.FontSize = 18;
                addressLabelFormat.LineSpacing = 8;
            }

            if (address.CountryCode == "IT")
            {
                town = town.ToUpper();
            }

            if (address.CountryCode == "IR")
            {
                addressLabelFormat.RightToLeft = true;
                addressLabelFormat.FontSize = 15;
                if (address.District == address.Town)
                {
                    printDistrict = false;
                }
            }

            var lines = new List<string>();
            addressLabelFormat.Lines = lines;
            lines.Add(address.Name + ",");
            lines.Add(address.AddressLine1 + ",");
            if (!String.IsNullOrEmpty(address.AddressLine2))
                lines.Add(address.AddressLine2 + ",");
            lines.Add(town + ",");
            if (printDistrict)
                lines.Add(address.District.ToUpper());
            lines.Add(address.PostalCode);
            lines.Add(address.Country.ToUpper());
            return addressLabelFormat;
        }
    }

    class ShippingLabelPrinter
    {
        private readonly PrinterConfig printerConfig;
        private readonly IAddressLabelFormatter _addressLabelFormatter;

        public ShippingLabelPrinter(): this(new AddressLabelFormatter())
        {
            
        }

        public ShippingLabelPrinter(IAddressLabelFormatter addressLabelFormatter)
        {
            _addressLabelFormatter = addressLabelFormatter;
            printerConfig = PrinterConfig.Instance;
        }

        public void PrintLabel(Address address)
        {
            var addressLabelFormat = _addressLabelFormatter.GetAddressLabelFormat(address);

            var printer = new Printer(printerConfig.Port);
            printer.Font = addressLabelFormat.Font;
            printer.FontSize = addressLabelFormat.FontSize;
            printer.LineSpacing = addressLabelFormat.LineSpacing;
            foreach (var line in addressLabelFormat.Lines)
            {
                printer.PrintLine(line);
            }
        }
    }

    internal class AddressLabelFormat
    {
        public string Font { get; set; }
        public int FontSize { get; set; }
        public int LineSpacing { get; set; }
        public bool RightToLeft { get; set; }
        public List<string> Lines { get; set; }
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
