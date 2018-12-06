using System;
using System.Linq;
using System.Xml;
using System.Collections.Generic;
using System.Xml.Linq;


namespace LinqToXml
{
    class Program
    {
        XDocument xmlDocument;
        static void Main(string[] args)
        {
            Program program = new Program();
            program.CreateDocument();
            program.containsXml();
            program.ReadingXml();
            program.bookWithHeighestPages();
            program.groupGenre();

            Console.ReadKey();
        }

        private void groupGenre()
        {

            Console.WriteLine("Grouping books by genere ***********************************************");
            IEnumerable<IGrouping<string,XElement>> bookGenre = (from book in xmlDocument.Element("root").Elements("row")
                              select book).GroupBy(ele => ele.Element("Genre").Value);
            foreach(IGrouping<string, XElement> first in bookGenre)
            {
                Console.WriteLine("Genre is {0}",first.Key);
                foreach(XElement second in first)
                {
                    Console.WriteLine("                " + second.Value);
                }
            }
        }

        private void bookWithHeighestPages()
        {
            Console.WriteLine("Book with heighest pages***************************************");

            int bookLength = (from book in xmlDocument.Element("root").Elements("row")
                         select book).Max(ele => int.Parse(ele.Element("Height").Value));

            IEnumerable<XElement> books = from book in xmlDocument.Element("root").Elements("row")
                                          where int.Parse(book.Element("Height").Value) == bookLength
                                          select book;



            foreach (XElement item in books)
            {
                Console.WriteLine(item.Element("Title").Value);
            }

        }

        private void containsXml()
        {
            Console.WriteLine("Contains India*****************************************");
            IEnumerable<XElement> books = from book in xmlDocument.Element("root").Elements("row")
                                            where ((string)book.Element("Title").Value).Contains("India")
                                            select book;

            foreach (XElement item in books)
            {
                Console.WriteLine(item.Element("Title").Value);
            }
        }

        private void CreateDocument()
        {
            xmlDocument =  XDocument.Load("read.xml");
            IEnumerable<XElement> bookname = from book in xmlDocument.Element("root").Elements("row")
                           select book;
            foreach (XElement item in bookname)
            {
                Console.WriteLine(item.Element("Title").Value);
            }
        }

        private void ReadingXml()
        {
            
        }
    }
}
