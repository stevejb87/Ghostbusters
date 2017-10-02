using System;
using System.Collections.Generic;

namespace ghostbusters
{
    class Program
    {
        static void Main(string[] args)
        {
            var coords = new List<Tuple<int, int>>();
            int power = 1000000;

            Console.WriteLine("Enter the number of ghostbusters.");
            int ghostbusters = Int32.Parse(Console.ReadLine());

            for (int i = 1; i <= ghostbusters; i++)
            {
                Console.WriteLine("Enter the x and y coordinate for ghostbuster {0}", i);
                string userEntry = Console.ReadLine();
                string[] coordinates = userEntry.Split(' ');
                coords.Add(new Tuple<int, int>(Int32.Parse(coordinates[0]), Int32.Parse(coordinates[1])));
            }

            Console.Clear();
            foreach (var item in coords)
            {
                Console.WriteLine(item);
            }

            for (int i = 0; i < coords.Count - 2; i++)  //Start going through each node in the list. Objective is to find three in-line nodes parallel to the x axis (horizontal). So they all must have the same y coordinate. Stop two before the end of the list because we need three in-line nodes.
            {
                for (int j = i + 1; j < coords.Count - 1; j++)  //Go through each node starting with the one next to the i node, so we can check if they both lie in the same y coordinate in the next "if" statement. Stop one before the end of the list, because we need to find a third node later.
                {
                    if (coords[i].Item2 == coords[j].Item2) //Check if the i and j nodes have the same y coordinate.
                    {   //We now have two in-line nodes in the x direction (horizontal).
                        for (int k = j + 1; k < coords.Count; k++)  //Start looking for a third node.
                        {
                            if (coords[j].Item2 == coords[k].Item2) //See if the k and the j nodes have the same y coordinate.
                            {   //We now have three in-line nodes parallel to the x axis (horizontal).
                                for (int l = 0; l < coords.Count; l++)  //Start looking for a node that in-line to the i node parallel to the y axis (vertical). So they should both have the same x coordinate.
                                {
                                    if ((coords[l].Item1 == coords[i].Item1) && (coords[l].Item2 != coords[i].Item2))   //l and i nodes both must have the same x coordinate. Also l node must be different from the i node, so they must have different y coordinates.
                                    {
                                        for (int m = 0; m < coords.Count; m++)  //Start looking for an m node that is horizontal to the l node and vertical to the j node. The next "if" statement checks for this.
                                        {
                                            if ((coords[m].Item2 == coords[l].Item2) && (coords[m].Item1 == coords[j].Item1))
                                            {
                                                for (int n = 0; n < coords.Count; n++) //Start looking for a node horizontal to the m node and vertical to the k node. This is the 6th and final node.
                                                {
                                                    if ((coords[n].Item2 == coords[m].Item2) && (coords[n].Item1 == coords[k].Item1))
                                                    {   //Our rectangle is now complete, with nodes i,j,k,l,m and n.
                                                        int largestX = (coords[i].Item1 > coords[j].Item1 ? coords[i].Item1 : coords[j].Item1); //Looking for the largest x coordinate so we can find the horizontal edge length of the rectangle. Finding out if i or the j node has the largest x coordinate.
                                                        largestX = (largestX > coords[k].Item1 ? largestX : coords[k].Item1);   //Checking to see which is bigger, the largest value from the above line or x coordinate from the k node. Whichever is bigger here is the largest x coordinate.

                                                        int smallestX = (coords[i].Item1 < coords[j].Item1 ? coords[i].Item1 : coords[j].Item1); //Same as the above two lines, but for the smallest x coordinate.
                                                        smallestX = (smallestX < coords[k].Item1 ? smallestX : coords[k].Item1);

                                                        int xDifference = largestX - smallestX; //Find the horizontal edge length of the rectangle.

                                                        int tempPower = (xDifference % 2 == 0 ? (xDifference / 2) - 1 : xDifference / 2);   //Finding the power allowed for this rectangle. If the edge length divides by two with no remainder, divide by two and subtract one. Otherwise, judt divide by two.

                                                        if (tempPower < power) power = tempPower;   //If the power allowed for this rectangle is smaller than the smallest power value calculated so far, replace it.
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < coords.Count - 2; i++)  //Same as the above piece of code, but for the y direction instead of x, so we can find a 6 node rectangle with a 3 node edges going vertically instead of horizontally.
            {
                for (int j = i + 1; j < coords.Count - 1; j++)
                {
                    if (coords[i].Item1 == coords[j].Item1)
                    {
                        for (int k = j + 1; k < coords.Count; k++)
                        {
                            if (coords[j].Item1 == coords[k].Item1)
                            {
                                for (int l = 0; l < coords.Count; l++)
                                {
                                    if ((coords[l].Item2 == coords[i].Item2) && (coords[l].Item1 != coords[i].Item1))
                                    {
                                        for (int m = 0; m < coords.Count; m++)
                                        {
                                            if ((coords[m].Item1 == coords[l].Item1) && (coords[m].Item2 == coords[j].Item2))
                                            {
                                                for (int n = 0; n < coords.Count; n++)
                                                {
                                                    if ((coords[n].Item1 == coords[m].Item1) && (coords[n].Item2 == coords[k].Item2))
                                                    {
                                                        int largestY = (coords[i].Item2 > coords[j].Item2 ? coords[i].Item2 : coords[j].Item2);
                                                        largestY = (largestY > coords[k].Item2 ? largestY : coords[k].Item2);

                                                        int smallestY = (coords[i].Item2 < coords[j].Item2 ? coords[i].Item2 : coords[j].Item2);
                                                        smallestY = (smallestY < coords[k].Item2 ? smallestY : coords[k].Item2);

                                                        int yDifference = largestY - smallestY;

                                                        int tempPower = (yDifference % 2 == 0 ? (yDifference / 2) - 1 : yDifference / 2);

                                                        if (tempPower < power) power = tempPower;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (power == 1000000) Console.WriteLine("UNLIMITED");
            else Console.WriteLine(power);

            Console.ReadLine();
        }
    }
}
