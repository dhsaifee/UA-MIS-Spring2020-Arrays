﻿using System;
using System.IO;

namespace Arrays_Coding
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = new string[100];
            string[] breeds = new string[100];            
            int[] weights = new int[100];

            //int count = GetDogData(names, breeds, weights);

            int count = ReadDogFile(names, breeds, weights);

            PrintDogs(names, breeds, weights, count);

            //SelectionSortDogsByNames(names, breeds, weights, count);
            SelectionSortDogsByBreeds(names, breeds, weights, count);
            Console.WriteLine("\nAfter sorting by breeds in ascending order: ");
            PrintDogs(names, breeds, weights, count);

            Console.WriteLine("\nSummary report of the breeds:");
            BreedSummary(names, breeds, weights, count);

            //Console.Write("\nEnter the name of dog you want to find (stop to exit): ");
            //string nameToFind = Console.ReadLine();

            //while (nameToFind.ToLower() != "stop")
            //{
            //    //int indexFound = FindDogSequential(names, count, nameToFind);
            //    int indexFound = FindDogBinary(names, count, nameToFind);

            //    if (indexFound != -1)
            //    {
            //        Console.WriteLine(names[indexFound] + " is a " + breeds[indexFound] + " and weighs " + weights[indexFound]);
            //    }
            //    else
            //    {
            //        Console.WriteLine(nameToFind + " could not be found");
            //    }

            //    Console.Write("\nEnter the name of dog you want to find (stop to exit): ");
            //    nameToFind = Console.ReadLine();
            //}

            Console.ReadKey();
        }


        static void BreedSummary(string[] names, string[] breeds, int[] weights, int count)
        {
            StreamWriter outFile = new StreamWriter("breed_summary.txt");

            string currentBreed = breeds[0];
            int breedCount = 1;
            double totalBreedWeight = weights[0];
            Console.WriteLine(names[0] + "\t" + breeds[0] + "\t" + weights[0]);
            outFile.WriteLine(names[0] + "\t" + breeds[0] + "\t" + weights[0]);

            for (int i = 1; i < count; i++)
            {
                if (breeds[i] == currentBreed)
                {
                    breedCount++;
                    totalBreedWeight += weights[i];
                }
                else
                {
                    Console.WriteLine("There are " + breedCount + " " + currentBreed +
                        " and their average weight is " + (totalBreedWeight / breedCount));
                    outFile.WriteLine("There are " + breedCount + " " + currentBreed +
                        " and their average weight is " + (totalBreedWeight / breedCount));

                    currentBreed = breeds[i];
                    breedCount = 1;
                    totalBreedWeight = weights[i];
                }

                Console.WriteLine(names[i] + "\t" + breeds[i] + "\t" + weights[i]);
                outFile.WriteLine(names[i] + "\t" + breeds[i] + "\t" + weights[i]);

            }

            Console.WriteLine("There are " + breedCount + " " + currentBreed +
                " and their average weight is " + (totalBreedWeight / breedCount));

            outFile.WriteLine("There are " + breedCount + " " + currentBreed +
                " and their average weight is " + (totalBreedWeight / breedCount));

            outFile.Close();
        }

        static int ReadDogFile(string[] names, string[] breeds, int[] weights)
        {
            int count = 0;

            if (File.Exists("dogs.txt"))
            {
                StreamReader inFile = new StreamReader("dogs.txt");

                string line = inFile.ReadLine();

                while (line != null)
                {
                    string[] tempArray = line.Split('#');
                    Console.WriteLine("The elements are: " + tempArray[0] + " " +
                        tempArray[1] + " " + tempArray[2]);

                    names[count] = tempArray[0];
                    breeds[count] = tempArray[1];
                    weights[count] = int.Parse(tempArray[2]);

                    count++;

                    line = inFile.ReadLine();
                }

                inFile.Close();
            }
            else
            {
                Console.WriteLine("dogs.txt could not be found.");
            }

            return count;
        }

        static int FindDogBinary(string[] names, int count, string searchValue)
        {
            int indexFound = -1;
            bool notFound = true;
            int first = 0;
            int last = count - 1;
            searchValue = searchValue.ToLower();

            while (notFound && first <= last)
            {
                int middle = (first + last) / 2;

                if (searchValue == names[middle].ToLower())
                {
                    notFound = false;
                    indexFound = middle;
                }
                else if (searchValue.CompareTo(names[middle].ToLower()) > 0)
                {
                    first = middle + 1;
                }
                else
                {
                    last = middle - 1;
                }
            }

            return indexFound;
        }


        static void SelectionSortDogsByNames(string[] names, string[] breeds, int[] weights, int count)
        {
            for (int i = 0; i < count - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < count; j++)
                {
                    if (names[j].CompareTo(names[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    Swap(names, minIndex, i);
                    Swap(breeds, minIndex, i);
                    Swap(weights, minIndex, i);
                }

                //Console.WriteLine();
                //PrintDogs(names, breeds, weights, count);
            }
        }

        static void SelectionSortDogsByBreeds(string[] names, string[] breeds, int[] weights, int count)
        {
            for (int i = 0; i < count - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < count; j++)
                {
                    if (breeds[j].CompareTo(breeds[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    Swap(names, minIndex, i);
                    Swap(breeds, minIndex, i);
                    Swap(weights, minIndex, i);
                }

                //Console.WriteLine();
                //PrintDogs(names, breeds, weights, count);
            }
        }

        static void Swap(string[] myArray, int x, int y)
        {
            string temp = myArray[x];
            myArray[x] = myArray[y];
            myArray[y] = temp;
        }

        static void Swap(int[] myArray, int x, int y)
        {
            int temp = myArray[x];
            myArray[x] = myArray[y];
            myArray[y] = temp;
        }


        static int FindDogSequential(string[] names, int count, string searchValue)
        {
            int indexFound = -1;

            for (int i = 0; i < count; i++)
            {
                if (searchValue.ToLower() == names[i].ToLower())
                {
                    indexFound = i;
                    return indexFound;
                }
            }

            return indexFound;
        }


        static void PrintDogs(string[] names, string[] breeds, int[] weights, int count)
        {
            Console.WriteLine("Name\tBreed\tWeight");

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(names[i] + "\t" + breeds[i] + "\t" + weights[i]);
            }
        }


        static int GetDogData(string[] myNames, string[] myBreeds, int[] myWeights)
        {
            int count = 0;

            Console.Write("Enter dog's name (stop to exit): ");
            string name = Console.ReadLine();

            while (name.ToLower() != "stop")
            {
                Console.Write("Enter dog's breed: ");
                string breed = Console.ReadLine();

                Console.Write("Enter dog's weight: ");
                int weight = int.Parse(Console.ReadLine());

                myNames[count] = name;
                myBreeds[count] = breed;
                myWeights[count] = weight;

                count++;

                Console.Write("Enter dog's name (stop to exit): ");
                name = Console.ReadLine();
            }

            return count;
        }

    }
}
