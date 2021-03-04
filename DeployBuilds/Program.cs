using System;
using System.IO;
using System.Collections;

namespace DeployBuilds
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*****Deploy Builds*****");

            string[] dest_servs = File.ReadAllLines("servers.txt");
            ArrayList src_servs = new ArrayList();
            src_servs.Add("Main_Server");

            int dest_serv_cntr = 0;

            while (dest_serv_cntr < dest_servs.Length) {
                ArrayList tmp_servs = new ArrayList();
                foreach (string serv in src_servs) {
                    Console.WriteLine(serv + " - " + dest_servs[dest_serv_cntr]);
                    tmp_servs.Add(dest_servs[dest_serv_cntr]);
                    dest_serv_cntr += 1;
                    if(dest_serv_cntr >= dest_servs.Length)
                    {
                        break;
                    }
                }
                src_servs.AddRange(tmp_servs);
            }

            Console.ReadKey();
        }
    }
}
