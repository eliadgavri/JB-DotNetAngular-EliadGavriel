using System;
using System.Collections.Generic;

namespace JohnBryce
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[] answer;
            int[,] example1 = {{1,0,0,1},   // Example 1
                            {0,0,1,0},
                            {1,0,1,0},
                            {1,0,1,0}};
            answer = magic(example1);
            Console.WriteLine("Example 1:");
            foreach (int i in answer)
                Console.Write($"{i}, ");
            int[,] example2 = {{0,0,0,1,0,0,0,1},   // Example 2
                            {1,1,0,0,0,0,1,0},
                            {0,0,0,0,1,1,0,1},
                            {1,1,1,0,0,0,0,0}};
            answer = magic(example2);
            Console.WriteLine($"\nExample 2:");
            foreach (int i in answer)
                Console.Write($"{i}, ");
            int[,] example3 = {{0,0,0,0,1},     // Example 3
                            {0,1,1,1,0},
                            {0,1,0,1,0},
                            {0,1,1,1,0},
                            {1,0,0,0,0}};
            answer = magic(example3);
            Console.WriteLine($"\nExample 3:");
            foreach (int i in answer)
                Console.Write($"{i}, ");
        }

        // Input: 2D array with 0's as land and 1's as water
        // Output: A sorted array that contains river's sizes
        private static int[] magic(int[,] arr)
        {
            List<int> sizes = new List<int>();
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);
            bool[,] visited = new bool[rows, cols];
            for (int i = 0; i < rows; i++) 
            {
                for (int j = 0; j < cols; j++) 
                {
                    if (visited[i, j] == true) 
                        continue;
                    traverseNode(arr, i, j, visited, sizes);
                }
            }
            sizes.Sort();
            return sizes.ToArray();
        }

        // Input: The original 2D array, current row and col, 2D array of booleans that shows if a cell got visited and List of river's sizes
        // Output: None (by ref the current river size is been added into the sizes List)
        private static void traverseNode(int[,] arr, int i, int j, bool[,] visited, List<int> sizes)
        {
            int currentRiverSize = 0;
            Stack<Tuple<int, int>> nodesToCheck = new Stack<Tuple<int, int>>();
            nodesToCheck.Push(new Tuple<int, int>(i, j));
            Tuple<int, int> currentNode;
            List<Tuple<int, int>> UnvisitedNeighbors;
            while (nodesToCheck.Count > 0)
            {
                currentNode = nodesToCheck.Pop();
                i = currentNode.Item1;
                j = currentNode.Item2;
                if (visited[i, j] == true)
                    continue;
                visited[i, j] = true;
                if (arr[i, j] == 0)
                    continue;
                currentRiverSize++;
                UnvisitedNeighbors = getUnvisitedNeighbors(arr, i, j, visited);
                foreach (Tuple<int, int> node in UnvisitedNeighbors)
                    nodesToCheck.Push(node);
            }
            if (currentRiverSize > 0)
                sizes.Add(currentRiverSize);
        }

        // Input: The original 2D array, current row and col, 2D array of booleans that shows if a cell got visited
        // Output: List of the current cell neighbors
        private static List<Tuple<int, int>> getUnvisitedNeighbors(int[,] arr, int i, int j, bool[,] visited)
        {
            List<Tuple<int, int>> UnvisitedNeighbors = new List<Tuple<int, int>>();
            if (i > 0 && visited[i - 1, j] == false)    // Above
                UnvisitedNeighbors.Add(new Tuple<int, int>(i - 1, j));
            if(i < arr.GetLength(0) - 1 && visited[i+1, j] == false)    // Below
                UnvisitedNeighbors.Add(new Tuple<int, int>(i + 1, j));
            if (j > 0 && visited[i, j - 1] == false)    // To the left of
                UnvisitedNeighbors.Add(new Tuple<int, int>(i, j - 1));
            if (j < arr.GetLength(1) - 1 && visited[i, j + 1] == false)    // To the right of
                UnvisitedNeighbors.Add(new Tuple<int, int>(i, j + 1));
            return UnvisitedNeighbors;
        }
    }
}
