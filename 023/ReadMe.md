  # Day 023 - Microsoft C# DependentTransaction Class

  ### What is DependentTransaction Class?
  
  The DependentTransaction is a clone of a Transaction object created using the DependentClone method. Its sole purpose is to allow the application to come to rest and guarantee that the transaction cannot commit while work is still being performed on the transaction (for example, on a worker thread). When the work done within the cloned transaction is finally complete and ready to be committed, it can inform the creator of the transaction using the Complete method. Thus you can preserve the consistency and correctness of data.

  ### Example
  ```c#
        using System;
        using System.Threading;
        using System.Transactions;

        namespace ConsoleApp
        {
            class Program
            {
                static void Main(string[] args)
                {
                    try
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            // Perform transactional work here.

                            //Queue work item
                            ThreadPool.QueueUserWorkItem(new WaitCallback(WorkerThread), Transaction.Current.DependentClone(DependentCloneOption.BlockCommitUntilComplete));

                            //Display transaction information
                            Console.WriteLine("Transaction information:");
                            Console.WriteLine("ID:             {0}", Transaction.Current.TransactionInformation.LocalIdentifier);
                            Console.WriteLine("status:         {0}", Transaction.Current.TransactionInformation.Status);
                            Console.WriteLine("isolationlevel: {0}", Transaction.Current.IsolationLevel);

                            //Call Complete on the TransactionScope based on console input
                            ConsoleKeyInfo c;
                            while (true)
                            {
                                Console.Write("Complete the transaction scope? [Y|N] ");
                                c = Console.ReadKey();
                                Console.WriteLine();

                                if ((c.KeyChar == 'Y') || (c.KeyChar == 'y'))
                                {
                                    //Call complete on the scope
                                    scope.Complete();
                                    break;
                                }
                                else if ((c.KeyChar == 'N') || (c.KeyChar == 'n'))
                                {
                                    break;
                                }
                            }
                        }
                    }
                    catch (System.Transactions.TransactionException ex)
                    {
                        Console.WriteLine(ex);
                    }
                    catch
                    {
                        Console.WriteLine("Cannot complete transaction");
                        throw;
                    }
                }

                private static void WorkerThread(object transaction)
                {
                    DependentTransaction dTx = (DependentTransaction)transaction;

                    Thread.Sleep(7000);

                    using (TransactionScope ts = new TransactionScope(dTx))
                    {
                        //Do some other operations here
                        //Call complete on the transaction scope
                        ts.Complete();
                    }
                    dTx.Complete();
                }
            }
        }
  ```
  ### References
  *  https://docs.microsoft.com/en-us/dotnet/api/system.transactions.dependenttransaction?redirectedfrom=MSDN&view=net-5.0