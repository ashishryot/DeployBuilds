using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace DeployBuilds
{

    class MultiThread {

        string thread_name;
        string src_serv;
        string dest_serv;

        public MultiThread(string thr, string src, string dest)
        {
            this.thread_name = thr;
            this.src_serv = src;
            this.dest_serv = dest;
        }

        public void RunThread()
        {
            Console.WriteLine(this.thread_name + " : " + src_serv + " - " + dest_serv);
        }
    }


    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*****Deploy Builds*****");

            string[] dest_servs = File.ReadAllLines("servers.txt");
            ArrayList src_servs = new ArrayList();
            src_servs.Add("Main_Server");

            int dest_serv_cntr = 0;
            const int MAX_THREADS = 16;
            int cnt = 1;
            while (dest_serv_cntr < dest_servs.Length) {
                Console.WriteLine("***Iteration is: " + cnt++);
                ArrayList tmp_servs = new ArrayList();
                int thread_count = 0;
                List<MultiThread> obj_list = new List<MultiThread>();
                List<Thread> thrd_list = new List<Thread>();
                foreach (string serv in src_servs) {
                    thread_count += 1;
                    if (thread_count > MAX_THREADS)
                    {
                        // Wait for Thread Join()
                        foreach(Thread th in thrd_list) {
                            th.Join();
                        }

                         thread_count = 1;
                    }
                    //Console.WriteLine(serv + " - " + dest_servs[dest_serv_cntr]);
                    tmp_servs.Add(dest_servs[dest_serv_cntr]);

                    //  Create and start Thread
                    MultiThread mt = new MultiThread("Thread" + thread_count, serv, dest_servs[dest_serv_cntr]);
                    obj_list.Add(mt);
                    Thread thr = new Thread(new ThreadStart(mt.RunThread));
                    thr.Start();
                    thrd_list.Add(thr);

                    dest_serv_cntr += 1;
                    if(dest_serv_cntr >= dest_servs.Length)
                    {
                        break;
                    }
                }

                // Wait for Thread Join()
                foreach(Thread th in thrd_list)
                {
                    th.Join();
                }

                src_servs.AddRange(tmp_servs);
            }

            Console.ReadKey();
        }
    }
}
