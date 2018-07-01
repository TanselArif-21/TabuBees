using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmReplenishment
{
    class MainMaximizer
    {
        /// <summary>
        /// Main profit calculator for a single Qijt quantity delivered instance
        /// </summary>
        /// <param name="fijt"></param>
        /// <param name="Qij_t"></param>
        /// <param name="pri"></param>
        /// <param name="unc"></param>
        /// <param name="cu"></param>
        /// <param name="cs"></param>
        /// <param name="ctf"></param>
        /// <param name="ctv"></param>
        /// <param name="distj"></param>
        /// <param name="trange"></param>
        /// <param name="chf"></param>
        /// <param name="chv"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static double CalcProfit(int[,,] fijt, int[,,] Qij_t, double[] pri, double unc, double[] cu, double cs, double ctf, double ctv, double[] distj, int trange, double chf, double[] chv, double B)
        {
            double profit = 0;
            double revenue = 0;
            double stockOut = 0;
            double purchaseCost = 0;
            double transportCost = 0;
            double holdingCost = 0;

            int n_items = fijt.GetLength(0);
            int m_stores = fijt.GetLength(1);
            int t_timebucket = fijt.GetLength(2);

            double[,,] dijt = new double[n_items, m_stores, t_timebucket];
            int[,,] Sij_t = new int[n_items, m_stores, t_timebucket];
            int[,,] Invijt = new int[n_items, m_stores, t_timebucket];
            
            for (int i = 0; i < n_items; i++)
            {
                for (int j = 0; j < m_stores; j++)
                {
                    Invijt[i, j, 0] = 0;
                }
            }

            // Calculate Purchase Costs
            purchaseCost = CalcPurchaseCost(cu, Qij_t);

            if (purchaseCost > B)
            {
                Console.Write("Error");
            }

            // Calculate demand
            CalcDemand(fijt, unc, dijt);

            // Calculate inventory for time using the inventory and quantity ordered
            for (int t = 0; t < t_timebucket; t++) {
                CalcInventory(Qij_t, Sij_t, Invijt, t, dijt);
            }
            
            // Calculate revenue
            revenue = CalcRev(Sij_t, pri);

            // Calculate Stock Out costs
            stockOut = CalcStockOutCost(cs,cu,Invijt, dijt);

            // Calculate Transport Costs
            transportCost = CalcTransportCost(ctf, ctv, distj, Qij_t);

            // Calculate Holding Costs
            holdingCost = CalcHoldingCost(chf, chv, cu, Invijt, trange);

            profit = revenue - stockOut - purchaseCost - transportCost - holdingCost;

            return profit;
        }

        /// <summary>
        /// A function to calculate the Revenue
        /// </summary>
        /// <param name="Sij_t">An n by m matrix for time t. This is the sales
        ///     of item i in store j at time t.
        /// </param>
        /// <param name="pri">This is the market price of item i</param>
        /// <returns></returns>
        private static double CalcRev(int[,,] Sijt, double[] pri)
        {
            int n_items = Sijt.GetLength(0);
            int m_stores = Sijt.GetLength(1);
            int t_timebucket = Sijt.GetLength(2);

            double rev = 0;

            for (int t = 0; t < t_timebucket; t++)
            {
                for (int i = 0; i < n_items; i++)
                {
                    for (int j = 0; j < m_stores; j++)
                    {
                        rev = rev + Sijt[i,j,t] * pri[i];
                    }
                }
            }

            return rev;
        }

        /// <summary>
        /// A function to calculate the demand
        /// </summary>
        /// <param name="fijt"></param>
        /// <param name="unc"></param>
        /// <param name="dijt"></param>
        /// <returns></returns>
        private static double[,,] CalcDemand(int[,,] fijt, double unc, double[,,] dijt)
        {
            int n_items = fijt.GetLength(0);
            int m_stores = fijt.GetLength(1);
            int t_timebucket = fijt.GetLength(2);

            double lower = 0;
            double upper = 0;
            double rev = 0;
            double thisRev = 0;

            //var r = new Random(101);
            var r = new Random();

            for (int t = 0; t < t_timebucket; t++)
            {
                for (int i = 0; i < n_items; i++)
                {
                    for (int j = 0; j < m_stores; j++)
                    {
                        lower = fijt[i, j, t] * (1 - unc);
                        upper = fijt[i, j, t] * (1 + unc);

                        dijt[i, j, t] = lower + r.NextDouble() * (upper - lower);
                        
                    }
                }
            }

            return dijt;
        }

        /// <summary>
        /// A function to calculate the inventory and sales at time t
        /// </summary>
        /// <param name="Qij_t"></param>
        /// <param name="Sij_t"></param>
        /// <param name="Invijt"></param>
        /// <param name="t"></param>
        /// <param name="dijt"></param>
        private static void CalcInventory(int[,,] Qij_t, int[,,] Sij_t, int[,,] Invijt, int t, double[,,] dijt)
        {
            int n_items = Sij_t.GetLength(0);
            int m_stores = Sij_t.GetLength(1);

            for (int i = 0; i < n_items; i++)
            {
                for (int j = 0; j < m_stores; j++)
                {
                    if (t == 0) {
                        Invijt[i, j, t] = Qij_t[i, j, t];
                        Sij_t[i, j, t] = 0;
                    }
                    else
                    {
                        Sij_t[i, j, t] = Math.Min(Convert.ToInt32(dijt[i, j, t - 1]), Convert.ToInt32(Invijt[i, j, t - 1]));
                        Invijt[i, j, t] = Qij_t[i, j, t] - Sij_t[i, j, t] + Invijt[i, j, t - 1];
                    }
                }
            }
        }

        /// <summary>
        /// A function to calculate the stock out costs
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="cu"></param>
        /// <param name="Invijt"></param>
        /// <param name="dijt"></param>
        /// <returns></returns>
        private static double CalcStockOutCost(double cs, double[] cu, int[,,] Invijt, double[,,] dijt)
        {
            int n_items = Invijt.GetLength(0);
            int m_stores = Invijt.GetLength(1);
            int t_timebuckets = Invijt.GetLength(2);

            double totalStockOut = 0;

            for (int i = 0; i < n_items; i++)
            {
                for (int j = 0; j < m_stores; j++)
                {
                    for (int t = 0; t < t_timebuckets; t++)
                    {
                        if (dijt[i,j,t] > Invijt[i,j,t])
                        {
                            totalStockOut += cs * cu[i] * (dijt[i, j, t] - Invijt[i, j, t]);
                            //Console.WriteLine("Item out alert: item: " + i.ToString() + ", at store: " + j.ToString() + ", at time t: " + t.ToString() + ". The quantity needed " + (dijt[i, j, t] - Invijt[i, j, t]).ToString());
                        }
                        else
                        {
                            totalStockOut += 0;
                        }
                    }
                }
            }

            return totalStockOut;
        }

        /// <summary>
        /// A function to calculate the purchase costs
        /// </summary>
        /// <param name="cu"></param>
        /// <param name="Qijt"></param>
        /// <returns></returns>
        private static double CalcPurchaseCost(double[] cu, int[,,] Qijt)
        {
            int n_items = Qijt.GetLength(0);
            int m_stores = Qijt.GetLength(1);
            int t_timebuckets = Qijt.GetLength(2);

            double totalPurchaseCost = 0;

            for (int i = 0; i < n_items; i++)
            {
                for (int j = 0; j < m_stores; j++)
                {
                    for (int t = 0; t < t_timebuckets; t++)
                    {
                        totalPurchaseCost += cu[i] * Qijt[i, j, t];
                    }
                }
            }

            return totalPurchaseCost;
        }

        /// <summary>
        /// A function to calculate the transport costs
        /// </summary>
        /// <param name="ctf"></param>
        /// <param name="ctv"></param>
        /// <param name="distj"></param>
        /// <param name="Qijt"></param>
        /// <returns></returns>
        private static double CalcTransportCost(double ctf, double ctv, double[] distj, int[,,] Qijt)
        {
            int n_items = Qijt.GetLength(0);
            int m_stores = Qijt.GetLength(1);
            int t_timebuckets = Qijt.GetLength(2);

            double totalTransportCost = 0;
            double fixedCost = m_stores * t_timebuckets * ctf;

            for (int i = 0; i < n_items; i++)
            {
                for (int j = 0; j < m_stores; j++)
                {
                    for (int t = 0; t < t_timebuckets; t++)
                    {
                        totalTransportCost += ctv * distj[j] * Qijt[i,j,t];
                    }
                }
            }

            return totalTransportCost + fixedCost;
        }

        /// <summary>
        /// A function to calculate the holding costs
        /// </summary>
        /// <param name="chf"></param>
        /// <param name="chv"></param>
        /// <param name="cu"></param>
        /// <param name="Invijt"></param>
        /// <param name="trange"></param>
        /// <returns></returns>
        private static double CalcHoldingCost(double chf, double[] chv, double[] cu, int[,,] Invijt, int trange)
        {
            int n_items = Invijt.GetLength(0);
            int m_stores = Invijt.GetLength(1);
            int t_timebuckets = Invijt.GetLength(2);

            double totalHoldingCost = 0;
            double fixedCost = m_stores * chf;

            for (int i = 0; i < n_items; i++)
            {
                for (int j = 0; j < m_stores; j++)
                {
                    for (int t = 0; t < t_timebuckets; t++)
                    {
                        totalHoldingCost += chv[i] * cu[j] * Invijt[i, j, t]/trange;
                    }
                }
            }

            return totalHoldingCost + fixedCost;
        }

        private static void GenerateQijt(int[,,] Qij_t, int[,,] fijt, double unc, Random r)
        {

            int n_items = Qij_t.GetLength(0);
            int m_stores = Qij_t.GetLength(1);
            int t_timebuckets = Qij_t.GetLength(2);
            double lower = 0;
            double upper = 0;

            for (int i = 0; i < n_items; i++)
            {
                for (int j = 0; j < m_stores; j++)
                {
                    for (int t = 0; t < t_timebuckets; t++)
                    {
                        lower = fijt[i, j, t] * (1 - 2.5*unc);
                        upper = fijt[i, j, t] * (1 + 2.5*unc);

                        if(lower < 0)
                        {
                            lower = 0;
                        }

                        Qij_t[i, j, t] = Convert.ToInt32(lower + r.NextDouble() * (upper - lower));
                    }
                }
            }
        }

        public static void Sort(int[][,,] sortedJaggedQ, int[][,,] jaggedQ, int n, double[] P, double[] sortedP)
        {

            double thisP = P[0];
            int thisIndex = 0;

            for(int i = 0; i < n; i++)
            {
                sortedP[i] = P[i];
                sortedJaggedQ[i] = jaggedQ[i];
            }

            for (int k = 0; k < n; k++)
            {
                double maxP = sortedP[k];
                int maxIndex = k;

                // Find max
                for(int l = k; l < n; l++)
                {
                    if(maxP < sortedP[l])
                    {
                        maxIndex = l;
                        maxP = sortedP[l];
                    }
                }
                double tempP = sortedP[maxIndex];
                int[,,] tempQ = sortedJaggedQ[maxIndex];

                sortedP[maxIndex] = sortedP[k];
                sortedJaggedQ[maxIndex] = sortedJaggedQ[k];

                sortedP[k] = tempP;
                sortedJaggedQ[k] = tempQ;
            }
        }

        public static void Search(int[,,] fijt, int[,,] Qij_t, double[] pri, double unc, double[] cu, double cs, double ctf, double ctv, double[] distj, int trange, double chf, double[] chv, double B, int n, Random r, double[] tempP, int[][,,] jaggedQ_arr)
        {
            int n_items = Qij_t.GetLength(0);
            int m_stores = Qij_t.GetLength(1);
            int t_timebuckets = Qij_t.GetLength(2);

            // Generate n different Qijt about the forecast. 
            for (int i = 0; i < n; i++)
            {
                jaggedQ_arr[i] = new int[n_items, m_stores, t_timebuckets];

                GenerateQijt(jaggedQ_arr[i], fijt, unc, r);

                // Evaluate P(Qijt) for each of these
                tempP[i] = CalcProfit(fijt, jaggedQ_arr[i], pri, unc, cu, cs, ctf, ctv, distj, trange, chf, chv, B);
            }
        }

        public static double Optimize(int[,,] fijt, int[,,] Qij_t, double[] pri, double unc, double[] cu, double cs, double ctf, double ctv, double[] distj, int trange, double chf, double[] chv, double B, int n, int e, int b, int ne, int nb)
        {
            int n_items = Qij_t.GetLength(0);
            int m_stores = Qij_t.GetLength(1);
            int t_timebuckets = Qij_t.GetLength(2);
            int[][,,] sortedJaggedQ_arr = new int[n][,,];

            double maxProfit;
            int[,,] bestQ = new int[n_items, m_stores, t_timebuckets];

            // Create profit array
            double[] P = new double[n];
            double[] sortedP = new double[n];

            // Create n versions of Qijt
            int[][,,] jaggedQ_arr = new int[n][,,];

            // Instantiated outside since for loop is too fast for a new seed
            //var r = new Random(101);
            var r = new Random();
            
            // Generate n different Qijt about the forecast. 
            for (int i = 0; i < n; i++)
            {
                jaggedQ_arr[i] = new int[n_items, m_stores, t_timebuckets];

                GenerateQijt(jaggedQ_arr[i], fijt, unc,r);

                // Evaluate P(Qijt) for each of these
                P[i] = CalcProfit(fijt, jaggedQ_arr[i], pri, unc, cu, cs, ctf, ctv, distj, trange, chf, chv, B);
            }

            maxProfit = P[0];
            bestQ = jaggedQ_arr[0];

            // Sort the n Qijt items according to P
            Sort(sortedJaggedQ_arr, jaggedQ_arr, n, P,sortedP);

            int iter = 500;
            int itr = 0;

            while (itr < iter)
            {
                // Select e elite patches and b best patches
                for(int l = 0; l < n; l++)
                {
                    if(l < e)
                    {
                        double[] tempP = new double[ne];
                        int[][,,] tempJaggedQ_arr = new int[ne][,,];

                        // Send a search party of size ne to this patch 
                        Search(fijt, sortedJaggedQ_arr[l], pri, unc, cu, cs, ctf, ctv, distj, trange, chf, chv, B, ne, r, tempP, tempJaggedQ_arr);

                        P = P.Concat(tempP).ToArray();
                        jaggedQ_arr = jaggedQ_arr.Concat(tempJaggedQ_arr).ToArray();
                    }
                    else if(l < b + e)
                    {
                        double[] tempP = new double[ne];
                        int[][,,] tempJaggedQ_arr = new int[ne][,,];

                        // Send a search party of size ne to this patch 
                        Search(fijt, sortedJaggedQ_arr[l], pri, unc, cu, cs, ctf, ctv, distj, trange, chf, chv, B, nb, r, tempP, tempJaggedQ_arr);

                        P = P.Concat(tempP).ToArray();
                        jaggedQ_arr = jaggedQ_arr.Concat(tempJaggedQ_arr).ToArray();
                    }
                }

                double[] mtempP = new double[n - e - b];
                int[][,,] mtempJaggedQ_arr = new int[n - e - b][,,];

                // Send the remaining n-e-b search party to search globally
                Search(fijt, Qij_t, pri, unc, cu, cs, ctf, ctv, distj, trange, chf, chv, B, n-e-b, r, mtempP, mtempJaggedQ_arr);

                // Add to main population
                P = P.Concat(mtempP).ToArray();
                jaggedQ_arr = jaggedQ_arr.Concat(mtempJaggedQ_arr).ToArray();

                // Sort the population
                sortedP = new double[P.Length];
                sortedJaggedQ_arr = new int[P.Length][,,];
                Sort(sortedJaggedQ_arr, jaggedQ_arr, P.Length, P, sortedP);

                // Get the best if it's better than the previous best
                if(maxProfit < sortedP[0])
                {
                    maxProfit = sortedP[0];
                    bestQ = sortedJaggedQ_arr[0];
                }

                P = new double[n];
                jaggedQ_arr = new int[n][,,];

                for (int index = 0; index < n; index++)
                {
                    P[index] = sortedP[index];
                    jaggedQ_arr[index] = sortedJaggedQ_arr[index];
                }

                itr++;
            }
            
            // Print out the optimized Q_ijt
            for (int t = 0; t < t_timebuckets; t++)
            {
                Console.WriteLine("For time = " + t.ToString());
                for (int i = 0; i < n_items; i++)
                {
                    for (int j = 0; j < m_stores; j++)
                    {
                        Console.Write(jaggedQ_arr[0][i, j, t].ToString() + "   ");
                    }
                    Console.WriteLine("");
                }
            }
            
            return maxProfit;
        }
    }
}
