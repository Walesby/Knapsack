using System;
using System.Collections.Generic;
using System.IO;
/*
 * Student Number: 000732130
 */ 
namespace Knapsack
{
    class Program
    {
        static readonly string desiredAttribute = "HP";

        static readonly int goldConstraint = 1000;

        public static Items[] GetItems(string pathToCSV)
        {
            using (StreamReader csvReader = new StreamReader(pathToCSV))
            {
                Items[] items = null;
                string row;
                try
                {
                    List<Items> itemList = new List<Items>();
                    while ((row = csvReader.ReadLine()) != null)
                    {
                        string[] data = row.Split(",");
                        int value = -1;

                        for (int a = 0; a < data.Length; a++)
                        {
                            if (desiredAttribute.Equals(data[a].ToUpper()))
                            {
                                value = int.Parse(data[a + 1]);
                                break;
                            }
                        }

                        if (value != -1)
                        {
                            itemList.Add(new Items(int.Parse(data[2]), value, data[1]));
                        }
                    }

                    csvReader.Close();
                    items = new Items[itemList.Count];

                    for (int i = 0; i < itemList.Count; i++)
                    {
                        items[i] = itemList[i];
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Cannot find " + pathToCSV + ". Aborting.");
                }
                catch (IOException)
                {
                    Console.WriteLine("IO Error while reading from file, aborting.");
                }
                return items;
            }
        }

        public static void Main()
        {
            string fileName = "dataFile.csv";
            string directory = Environment.CurrentDirectory;
            string path = directory + "/" + fileName;
            try
            {
                using (StreamReader csvReader = new StreamReader(path))
                {
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"File exception: File not found at {path}");
            }

            Items[] itemArray = GetItems(path);

            if (itemArray == null)
            {
                Console.WriteLine("No items found");
                Environment.Exit(255);
            }

            MemoItem[] memoItemArray = new MemoItem[goldConstraint + 1];

            for (int r = 0; r < goldConstraint + 1; r++)
            {
                memoItemArray[r] = new MemoItem(0, new List<Items>());
            }

            for (int i = 1; i <= itemArray.Length; i++)
            {
                for (int j = goldConstraint; j >= 1; j--)
                {
                    if (itemArray[i - 1].Weight > j)
                    {
                        memoItemArray[j] = memoItemArray[j];
                    }
                    // Can take item
                    else
                    {
                        // Don't take item
                        if (memoItemArray[j].Value > memoItemArray[j - itemArray[i - 1].Weight].Value + itemArray[i - 1].Value)
                        {
                            memoItemArray[j] = memoItemArray[j];
                        }
                        // Take item
                        else
                        {
                            List<Items> itemList = new List<Items>(memoItemArray[j - itemArray[i - 1].Weight].List);
                            itemList.Add(itemArray[i - 1]);
                            memoItemArray[j] = new MemoItem(memoItemArray[j - itemArray[i - 1].Weight].Value + itemArray[i - 1].Value, itemList);
                        }
                    }
                }
            }
            Console.WriteLine($"The optimal value is: {memoItemArray[goldConstraint].Value}");
            foreach (var item in memoItemArray[goldConstraint].List)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
