using System.Numerics;
namespace DiscreteLogProblemLibrary



{
    public class BabyStep
    {
        private BigInteger modInverse(BigInteger G, BigInteger Q) // Samuel Allan's algorithm
        {
            BigInteger i = Q, v = 0, d = 1;
            while (G > 0)
            {
                BigInteger t = i / G, x = G;
                G = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= Q;
            if (v < 0) v = (v + Q) % Q;
            return v;
        }
        public long babyGiant (long g, long y, long p, bool option = false)
        {
            int k = (int)Math.Round(Math.Sqrt(p - 1));

            List <BigInteger> X = new List<BigInteger>();
            List<BigInteger> Y = new List<BigInteger>();


            foreach (int i in Enumerable.Range(0, k))
            {
                //BigInteger d = BigInteger.Pow(g, i);
                //long d2 = (long)d;
                //d2 %= p;
                //X.Add((long)Math.Pow(g, i)%p);
                BigInteger d = BigInteger.ModPow(g, i, p);
                X.Add(d);

            }

            //long inv = (long)Math.Pow(g, k * (p-2)) % p;
            //BigInteger inv = BigInteger.ModPow(g, k*(p-2), p);
            BigInteger inv = modInverse(g, k);

            if (option)
            {
                Console.WriteLine("X: ");
                foreach (int i in Enumerable.Range(0,k))
                {
                    //Console.WriteLine(X[i]);
                    Console.WriteLine($"X[{i}] = {X[i]}");
                }
            }

            foreach (int j in Enumerable.Range(0, k))
            {
                //long tmp = (long) (y * Math.Pow(inv, j) % p);
                BigInteger tmp = (y * BigInteger.Pow(inv, j)) % p;
                if (option)
                {
                    Y.Add(tmp);
                }
                if (X.Contains(tmp))
                {
                    if (option)
                    {
                        Console.WriteLine("Y: ");
                        //Console.WriteLine(Y.Take(j+1));
                        for (int i = 0; i < Y.Count; i++)
                        {
                            Console.WriteLine($"Y[{i}] = {Y[i]}");
                        }
                    }
                    return X.IndexOf(tmp) + j * k;
                }
            }


            return 0;
        }
    }
}